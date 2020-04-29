using Lets_Emoji_UWP.Helpers;
using Lets_Emoji_UWP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Lets_Emoji_UWP.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EmojiPage : Page
    {
        private List<MyEmoji> ListShowEmoji;

        public EmojiPage()
        {
            this.InitializeComponent();
            StartFunc();
        }

        private async void StartFunc()
        {
            if (!await GlobalTool.GetEmoji(GlobalTool.EmojiFrom.Local)) return;
            foreach (var i in GlobalTool.AllGroups) ComboBoxChoose.Items.Add(i);
            ComboBoxChoose.SelectionChanged += ComboBox_SelectionChanged;
            ComboBoxChoose.SelectedIndex = 1;
        }

        private void ChangeListItemSource()
        {
            GridViewEmoji.SelectionChanged -= EmojiChanged;
            GridViewEmoji.ItemsSource = ListShowEmoji;
            GridViewEmoji.SelectionChanged += EmojiChanged;
            GridViewEmoji.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string senderName = (sender as ComboBox).Name;
            switch (senderName)
            {
                case "ComboBoxChoose":
                    {
                        if ((sender as ComboBox).SelectedIndex == 0) return;
                        string s = e.AddedItems[0].ToString();
                        if (GlobalTool.AllGroups.Contains(s))
                        {
                            ListShowEmoji = new List<MyEmoji>();
                            foreach (var emo in GlobalTool.AllEmojis) if (s == emo.Group) ListShowEmoji.Add(emo);
                            ChangeListItemSource();
                        }
                    }
                    break;
            }
        }

        private void EmojiChanged(object sender, SelectionChangedEventArgs e)
        {
            GlobalTool.SelectedEmoji = e.AddedItems[0] as MyEmoji;
            GridInfo.DataContext = GlobalTool.SelectedEmoji;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string senderName = (sender as Button).Name;
            switch (senderName)
            {
                case "ButtonSearch":
                    {
                        if (null == GlobalTool.AllEmojis || new List<MyEmoji>() == GlobalTool.AllEmojis) return;
                        if (string.IsNullOrWhiteSpace(TextBoxSearch.Text))
                        {
                            FlyoutBase.ShowAttachedFlyout(TextBoxSearch);
                            return;
                        }
                        var newList = new List<MyEmoji>();
                        foreach (var emoji in GlobalTool.AllEmojis) if (emoji.Note.Contains(TextBoxSearch.Text.Trim())) newList.Add(emoji);
                        if (0 == newList.Count) GlobalTool.ShowDialog("注意", "无搜索结果！");
                        else
                        {
                            ListShowEmoji = new List<MyEmoji>();
                            foreach (var emoji in newList) ListShowEmoji.Add(emoji);
                            ComboBoxChoose.SelectedIndex = 0;
                            ChangeListItemSource();
                        }

                    }
                    break;
                case "ButtonCopy":
                    {
                        DataPackage package = new DataPackage();
                        package.SetText(GlobalTool.SelectedEmoji.Text);
                        Clipboard.SetContent(package);
                        FlyoutBase.ShowAttachedFlyout(ButtonCopy);
                    }
                    break;
                case "ButtonSave":
                    //SaveWindow(); //使用Xaml保存PNG的方法，现弃用，但代码保留以备不时之需。
                    FileHelper.ExportPngAsync(); //使用Win2D保存PNG，方法抄自Edi Wang。
                    break;
            }
        }

        /// <summary>
        /// XAML绘图使能够保存图片
        /// </summary>
        private async void SaveWindow()
        {
            CoreApplicationView newView = CoreApplication.CreateNewView();
            int newViewId = 0;
            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame frame = new Frame();
                frame.Navigate(typeof(TextPage), null);
                Window.Current.Content = frame;
                // You have to activate the window in order to show it later.
                Window.Current.Activate();

                newViewId = ApplicationView.GetForCurrentView().Id;
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);

            await ApplicationViewSwitcher.SwitchAsync(GlobalTool.MainViewID, newViewId, ApplicationViewSwitchingOptions.ConsolidateViews);

            FlyoutBase.ShowAttachedFlyout(ButtonSave);
        }

        private void TextBoxSearch_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter) Button_Click(ButtonSearch, null);
        }
    }
}
