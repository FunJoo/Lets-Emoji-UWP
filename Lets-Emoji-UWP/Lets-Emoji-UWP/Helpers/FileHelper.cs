using Lets_Emoji_UWP.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
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

                JsonArray array = JsonArray.Parse(GlobalTool.EmojiJson);
                List<MyEmoji> list = new List<MyEmoji>();
                foreach (IJsonValue jsonValue in array.GetArray())
                {
                    if (jsonValue.ValueType == JsonValueType.Object)
                    {
                        list.Add(new MyEmoji(jsonValue.GetObject()));
                    }
                }
                return list;
            }
            catch(Exception e)
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
            if (string.IsNullOrEmpty(json) || !json.StartsWith("[")) return null;
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
    }
}
