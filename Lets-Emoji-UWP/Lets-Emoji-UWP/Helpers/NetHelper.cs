using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace Lets_Emoji_UWP.Helpers
{
    class NetHelper
    {
        public const string EmojiLink = "http://funjoo.fun/emoji/";

        /// <summary>
        /// 获取Emoji的JSON
        /// </summary>
        /// <returns>JSON</returns>
        public async static Task<string> GetEmojis()
        {
            using(HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(new Uri(EmojiLink));
                    if(null!=response&& response.StatusCode == HttpStatusCode.Ok)
                    {
                        using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                        {
                            await response.Content.WriteToStreamAsync(stream);
                            stream.Seek(0);
                            Windows.Storage.Streams.Buffer buffer = new Windows.Storage.Streams.Buffer((uint)stream.Size);
                            await stream.ReadAsync(buffer, (uint)stream.Size, Windows.Storage.Streams.InputStreamOptions.Partial);
                            using (DataReader reader = DataReader.FromBuffer(buffer))
                            {
                                return reader.ReadString((uint)stream.Size);
                            }
                        }
                    }
                }catch(Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return string.Empty;
                }
                return string.Empty;
            }
        }
    }
}
