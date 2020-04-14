using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Lets_Emoji_UWP.Models
{
    class MyEmoji
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Note { get; set; }
        public string Group { get; set; }
        public string Subgroup { get; set; }
        public string Unicode { get; set; }

        public MyEmoji(JsonObject jsonObject)
        {
            if (null != jsonObject)
            {
                Name = jsonObject.GetNamedString("name", "");
                Text = jsonObject.GetNamedString("text", "");
                Note = jsonObject.GetNamedString("note", "");
                Group = jsonObject.GetNamedString("group", "");
                Subgroup = jsonObject.GetNamedString("subgroup", "");
                Unicode = jsonObject.GetNamedString("unicode", "");
            }
        }
    }
}
