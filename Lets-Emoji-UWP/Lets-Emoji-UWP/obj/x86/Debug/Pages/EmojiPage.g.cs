﻿#pragma checksum "C:\Users\huanz\source\repos\Lets-Emoji-UWP\Lets-Emoji-UWP\Lets-Emoji-UWP\Pages\EmojiPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B8A2AF5EC3A16C4A227C785F4EBD8E9A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lets_Emoji_UWP.Pages
{
    partial class EmojiPage : 
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
            case 2: // Pages\EmojiPage.xaml line 23
                {
                    this.GridTop = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // Pages\EmojiPage.xaml line 57
                {
                    this.ListViewEmoji = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 5: // Pages\EmojiPage.xaml line 27
                {
                    this.ComboBoxChoose = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.ComboBoxChoose).SelectionChanged += this.ComboBox_SelectionChanged;
                }
                break;
            case 6: // Pages\EmojiPage.xaml line 44
                {
                    this.TextBoxSearch = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7: // Pages\EmojiPage.xaml line 47
                {
                    this.ButtonSearch = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.ButtonSearch).Click += this.Button_Click;
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
    }
}

