using Lets_Emoji_UWP.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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

            Window.Current.SetTitleBar(RectTitle);
        }

        /// <summary>
        /// 绑定全局
        /// </summary>
        private void BindGlobal()
        {
            GlobalTool.FrameMain = FrameMain;
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            string senderName = (sender as MenuFlyoutItem).Name;
            switch (senderName)
            {
                case "MenuUpdate":
                    
                    break;
                case "MenuExit":
                    break;
                case "MenuAbout":
                    break;
            }
        }
    }
}
