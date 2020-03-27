using Lets_Emoji_UWP.Models;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Text;

namespace Lets_Emoji_UWP.Helpers
{
    class FileHelper
    {
        public const string EmojiFileName = "config_emoji.json";

        /// <summary>
        /// 检查本地文件夹有没有config_emoji.json这个文件，没有就创建
        /// </summary>
        /// <param name="reset">是否重置本地词库，默认为非重置</param>
        public static async void CheckLocalEmoji(bool reset = false)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;

            //StorageFile file = await folder.GetFileAsync(EmojiFileName);
            StorageFile file =(StorageFile) await folder.TryGetItemAsync(EmojiFileName);
            
            //await file.DeleteAsync(); //调试删除文件用

            if (file != null && !reset) return;

            file = await folder.CreateFileAsync(EmojiFileName, CreationCollisionOption.ReplaceExisting);
            StorageFile emojiFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets/config_emoji.json"));
            string txt = await FileIO.ReadTextAsync(emojiFile);

            await FileIO.WriteTextAsync(file, txt);
        }

        /// <summary>
        /// 读取本地Emoji词库
        /// </summary>
        /// <returns>返回词库List</returns>
        public static async Task<List<MyEmoji>> ReadLocalEmoji()
        {
            GlobalTool.EmojiJson = string.Empty;
            try
            {
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile file = await folder.GetFileAsync(EmojiFileName);
                GlobalTool.EmojiJson = await FileIO.ReadTextAsync(file);
                return JsonConvert.DeserializeObject<List<MyEmoji>>(GlobalTool.EmojiJson);
            }catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// 读取网络词库
        /// </summary>
        /// <returns>返回词库List</returns>
        public static async Task<List<MyEmoji>> ReadRemoteEmoji()
        {
            string json = await NetHelper.GetEmojis();
            if (string.IsNullOrEmpty(json) || !json.StartsWith('[')) return null;
            try
            {
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile file = await folder.GetFileAsync(EmojiFileName);
                await FileIO.WriteTextAsync(file, json);
                return await ReadLocalEmoji();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        


        #region 输出PNG
        private static async Task<StorageFile> PickFileAsync(string fileName, string key, IList<string> values)
        {
            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            savePicker.FileTypeChoices.Add(key, values);
            savePicker.SuggestedFileName = fileName;

            try
            {
                return await savePicker.PickSaveFileAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async void ExportPngAsync(CanvasTypography typography)
        {
            try
            {
                string name = "123.png";
                if (await PickFileAsync(name, "PNG Image", new[] { ".png" }) is StorageFile file)
                {
                    CachedFileManager.DeferUpdates(file);
                    
                    var device = CanvasDevice.GetSharedDevice();
                    var localDpi = 96; //Windows.Graphics.Display.DisplayInformation.GetForCurrentView().LogicalDpi;
                    
                    var canvasH = (float)2048;
                    var canvasW = (float)2048;
                    
                    using (var renderTarget = new CanvasRenderTarget(device, canvasW, canvasH, localDpi))
                    {
                        using (var ds = renderTarget.CreateDrawingSession())
                        {
                            ds.Clear(Colors.Transparent);
                            double d = 2048;
                            double r = 2048 / 2;

                            var textColor = Colors.White;
                            var fontSize = (float)d;

                            using (CanvasTextLayout layout = new CanvasTextLayout(device, $"{GlobalTool.SelectedEmoji.Text}", new CanvasTextFormat
                            {
                                FontSize = fontSize,
                                FontFamily = "Segoe UI Emoji",
                                FontStretch = FontStretch.Undefined,
                                FontWeight = new FontWeight { Weight = 20 },
                                FontStyle = FontStyle.Normal,
                                HorizontalAlignment = CanvasHorizontalAlignment.Center,
                                Options = CanvasDrawTextOptions.EnableColorFont
                            }, canvasW, canvasH))
                                {
                                    layout.Options = CanvasDrawTextOptions.EnableColorFont;

                                    layout.SetTypography(0, 1, typography);

                                    var db = layout.DrawBounds;
                                    double scale = Math.Min(1, Math.Min(canvasW / db.Width, canvasH / db.Height));
                                    var x = -db.Left + ((canvasW - (db.Width * scale)) / 2d);
                                    var y = -db.Top + ((canvasH - (db.Height * scale)) / 2d);

                                    ds.Transform =
                                        Matrix3x2.CreateTranslation(new Vector2((float)x, (float)y))
                                        * Matrix3x2.CreateScale(new Vector2((float)scale));

                                    ds.DrawTextLayout(layout, new Vector2(0), textColor);
                                }
                            }

                            using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                            {
                                fileStream.Size = 0;
                                await renderTarget.SaveAsync(fileStream, CanvasBitmapFileFormat.Png, 1f);
                            }
                        }
                    

                    await CachedFileManager.CompleteUpdatesAsync(file);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
