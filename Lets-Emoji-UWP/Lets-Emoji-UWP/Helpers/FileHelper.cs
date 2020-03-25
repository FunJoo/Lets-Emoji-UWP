using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Lets_Emoji_UWP.Helpers
{
    class FileHelper
    {
        public const string EmojiPath = "Assets/config_emoji.json";

        public static async void ReadLocalEmoji()
        {
            GlobalTool.EmojiJson = string.Empty;
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///"+EmojiPath));
            GlobalTool.EmojiJson = await FileIO.ReadTextAsync(file);
        }
    }
}
