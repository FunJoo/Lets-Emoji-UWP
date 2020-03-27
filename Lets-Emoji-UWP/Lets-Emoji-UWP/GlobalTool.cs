using Lets_Emoji_UWP.Helpers;
using Lets_Emoji_UWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Lets_Emoji_UWP
{
    class GlobalTool
    {
        #region MainPage.xaml

        public static Frame FrameMain;

        public static string AppVersion = string.Format("{0}.{1}.{2}.{3}",
                    Package.Current.Id.Version.Major,
                    Package.Current.Id.Version.Minor,
                    Package.Current.Id.Version.Build,
                    Package.Current.Id.Version.Revision);

        /// <summary>
        /// 主Frame导航到页面
        /// </summary>
        /// <param name="page">typeof(Page)</param>
        /// <returns>是否成功</returns>
        public static bool FMNavigate(Type page)
        {
            try
            {
                FrameMain.Navigate(page, null, new DrillInNavigationTransitionInfo());
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 主FrameGoBack
        /// </summary>
        public static void FMGoBack()
        {
            if (FrameMain.CanGoBack)
            {
                do
                {
                    FrameMain.GoBack();
                } while (FrameMain.CanGoBack);
            }
        }

        /// <summary>
        /// 关闭App
        /// </summary>
        public static void CloseApp()
        {
            Windows.UI.Xaml.Application.Current.Exit();
        }

        /// <summary>
        /// 检查本地的Emoji文件是否存在
        /// </summary>
        /// <param name="reset">是否需要重置词库</param>
        public static void CheckLocalEmoji(bool reset = false)
        {
            Task.Factory.StartNew(() =>
            {
                FileHelper.CheckLocalEmoji(reset);
            }).Wait();
        }

        #endregion

        #region EmojiPage.xaml

        public static string EmojiJson;

        public static MyEmoji SelectedEmoji;

        public static List<MyEmoji> AllEmojis;
        public static List<string> AllGroups;

        public enum EmojiFrom
        {
            Local,
            Remote
        }

        public static async Task<bool> GetEmoji(EmojiFrom from)
        {
            switch (from)
            {
                case EmojiFrom.Local:
                    {
                        AllEmojis = new List<MyEmoji>();
                        AllGroups = new List<string>();

                        AllEmojis = await FileHelper.ReadLocalEmoji();
                        if (null == AllEmojis || 0 == AllEmojis.Count) return false;

                        foreach(var emoji in AllEmojis)
                        {
                            if (!AllGroups.Contains(emoji.Group)) AllGroups.Add(emoji.Group);
                        }
                        return true;
                    }
                case EmojiFrom.Remote:
                    {
                        var mList = await FileHelper.ReadRemoteEmoji();
                        if (null == mList || 0 == mList.Count) return false;

                        AllEmojis = new List<MyEmoji>();
                        AllGroups = new List<string>();

                        AllEmojis = mList;
                        foreach (var emoji in AllEmojis)
                        {
                            if (!AllGroups.Contains(emoji.Group)) AllGroups.Add(emoji.Group);
                        }
                        return true;
                    }
                default:
                    return false;
            }
        }

        #endregion

        /// <summary>
        /// 显示警告框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">内容</param>
        public static async void ShowDialog(string title, string msg)
        {
            ContentDialog noResultDialog = new ContentDialog
            {
                Title = title,
                Content = msg,
                CloseButtonText = "确定"
            };
            await noResultDialog.ShowAsync();
        }
    }
}
