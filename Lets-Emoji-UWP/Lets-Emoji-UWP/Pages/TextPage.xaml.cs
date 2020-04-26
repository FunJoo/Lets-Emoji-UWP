using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
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
    public sealed partial class TextPage : Page
    {
        public TextPage()
        {
            this.InitializeComponent();
            TextBlockEmoji.Text = GlobalTool.SelectedEmoji.Text;
            GoRun();
        }

        private async void GoRun()
        {
            RenderTargetBitmap bmp = new RenderTargetBitmap();
            await bmp.RenderAsync(GridEmoji);
            IBuffer buffer = await bmp.GetPixelsAsync();

            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".png";

            StorageFolder savedPics = KnownFolders.SavedPictures;
            // 创建新文件
            StorageFile newFile = await savedPics.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            // 获取文件流
            IRandomAccessStream streamOut = await newFile.OpenAsync(FileAccessMode.ReadWrite);
            // 实例化编码器
            BitmapEncoder pngEncoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, streamOut);
            // 写入像素数据
            byte[] data = buffer.ToArray();
            pngEncoder.SetPixelData(BitmapPixelFormat.Bgra8,
                                    BitmapAlphaMode.Straight,
                                    (uint)bmp.PixelWidth,
                                    (uint)bmp.PixelHeight,
                                    96d, 96d, data);
            await pngEncoder.FlushAsync();

            streamOut.Dispose();
        }
    }
}
