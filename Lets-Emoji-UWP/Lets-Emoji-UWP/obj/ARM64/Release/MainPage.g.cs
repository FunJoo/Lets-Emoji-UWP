﻿#pragma checksum "C:\Users\huanz\source\repos\Lets-Emoji-UWP\Lets-Emoji-UWP\Lets-Emoji-UWP\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "ACFEEBE6950EB2AE2EDE6E36B326AFA2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lets_Emoji_UWP
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MainPage.xaml line 19
                {
                    this.RectTitle = (global::Windows.UI.Xaml.Shapes.Rectangle)(target);
                }
                break;
            case 3: // MainPage.xaml line 23
                {
                    this.ImageTitle = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 4: // MainPage.xaml line 65
                {
                    this.FrameMain = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            case 5: // MainPage.xaml line 30
                {
                    if (MainPage.IsApiContractPresent_Windows_Foundation_UniversalApiContract_7)
                    {
                        this.MenuBarMain = (global::Windows.UI.Xaml.Controls.MenuBar)(target);
                    }
                }
                break;
            case 6: // MainPage.xaml line 57
                {
                    this.TextBlockTitle = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 7: // MainPage.xaml line 51
                {
                    this.MenuAbout = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.MenuAbout).Click += this.MenuFlyoutItem_Click;
                }
                break;
            case 8: // MainPage.xaml line 34
                {
                    this.MenuEmoji = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.MenuEmoji).Click += this.MenuFlyoutItem_Click;
                }
                break;
            case 9: // MainPage.xaml line 37
                {
                    this.MenuEmojiUpdate = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.MenuEmojiUpdate).Click += this.MenuFlyoutItem_Click;
                }
                break;
            case 10: // MainPage.xaml line 41
                {
                    this.MenuEmojiReset = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.MenuEmojiReset).Click += this.MenuFlyoutItem_Click;
                }
                break;
            case 11: // MainPage.xaml line 45
                {
                    this.MenuExit = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)this.MenuExit).Click += this.MenuFlyoutItem_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }

        // Api Information for conditional namespace declarations
        internal static bool IsApiContractPresent_Windows_Foundation_UniversalApiContract_7 = global::Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7);
    }
}
