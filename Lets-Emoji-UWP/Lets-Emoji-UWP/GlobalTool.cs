using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Lets_Emoji_UWP
{
    class GlobalTool
    {
        public static string EmojiJson;

        #region MainPage.xaml

        public static Frame FrameMain;
        
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

        #endregion


        #region FileHelper.cs



        #endregion
    }
}
