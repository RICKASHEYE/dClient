using DSharpPlus.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dClient
{
    public class Config
    {
        /// <summary>
        /// Your bot's token.
        /// </summary>
        [JsonProperty("token")]
        internal string Token = "Your token..";

        /// <summary>
        /// Your favourite color.
        /// </summary>
        [JsonProperty("color")]
        private string _color = "#7289DA";

        [JsonProperty("title")]
        internal string _title = "Nameless discord client title here!";

        [JsonProperty("defaultServer")]
        internal string defaultServer = "This default server will be rewritten automatically anyway!!!";

        [JsonProperty("defaultChannel")]
        internal string defaultChannel = "This default channel will be rewritten automatically anyway!!!";

        [JsonProperty("customtitle")]
        internal string customtitle = "true";

        [JsonProperty("rolecolour")]
        internal string rlecolour = "true";

        [JsonProperty("messagechecking")]
        internal string messagecheck = "true";

        [JsonProperty("savecache")]
        internal string savecache = "true";

        [JsonProperty("globalread")]
        internal string globalread = "false";

        [JsonProperty("LoadMods")]
        internal string modLoad = "false";

        [JsonProperty("ModDirectory")]
        internal string modDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Mods");

        /// <summary>
        /// Your favourite color exposed as a DiscordColor object.
        /// </summary>
        internal DiscordColor Color => new DiscordColor(_color);

        /// <summary>
        /// Loads config from a JSON file.
        /// </summary>
        /// <param name="path">Path to your config file.</param>
        /// <returns></returns>
        public static Config LoadFromFile(string path)
        {
            using (var sr = new StreamReader(path))
            {
                return JsonConvert.DeserializeObject<Config>(sr.ReadToEnd());
            }
        }

        /// <summary>
        /// Saves config to a JSON file.
        /// </summary>
        /// <param name="path"></param>
        public void SaveToFile(string path)
        {
            using (var sw = new StreamWriter(path))
            {
                sw.Write(JsonConvert.SerializeObject(this));
            }
        }
    }
}
