using Lets_Emoji_UWP.Helpers;
using Lets_Emoji_UWP.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Lets_Emoji_UWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SetTitleBar();
            BindGlobal();

            GlobalTool.CheckLocalEmoji();
            GlobalTool.FMNavigate(typeof(EmojiPage));
        }

        /// <summary>
        /// 设置自定义标题栏
        /// </summary>
        private void SetTitleBar()
        {
            var coreTitleBar = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().TitleBar;
            var appTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;

            coreTitleBar.ExtendViewIntoTitleBar = true;
            appTitleBar.ButtonBackgroundColor = Colors.Transparent;
            //按钮非活动
            appTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            Window.Current.SetTitleBar(RectTitle);

            //设置窗口最小值
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Windows.Foundation.Size(500, 500));
        }

        /// <summary>
        /// 绑定全局
        /// </summary>
        private void BindGlobal()
        {
            GlobalTool.FrameMain = FrameMain;
        }

        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            string senderName = (sender as MenuFlyoutItem).Name;
            switch (senderName)
            {
                case "MenuEmoji":
                    GlobalTool.FMGoBack();
                    GlobalTool.FMNavigate(typeof(EmojiPage));
                    break;
                case "MenuEmojiUpdate":
                    if (await GlobalTool.GetEmoji(GlobalTool.EmojiFrom.Remote)) GlobalTool.FMNavigate(typeof(EmojiPage));
                    else GlobalTool.ShowDialog("警告", "联网获取词库失败");
                    break;
                case "MenuEmojiReset":
                    GlobalTool.CheckLocalEmoji(true);
                    GlobalTool.FMNavigate(typeof(EmojiPage));
                    break;
                case "MenuExit":
                    GlobalTool.CloseApp();
                    break;
                case "MenuAbout":
                    GlobalTool.ShowDialog("关于", 
                        "Let's Emoji v"
                        + GlobalTool.AppVersion
                        +"\n\n作者：FunJoo\n联系方式：huanzhuzai@outlook.com");
                    break;
            }
        }
    }
}
