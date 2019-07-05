using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace dClient
{
    public class API
    {
        public static bool stringtobool(string boolean)
        {
            if (boolean == "true") { return true; } else if (boolean == "false") { return false; } else { return false; }
        }

        public static Color FromHex(string hex)
        {
            int argb = Int32.Parse(hex.Replace("#", ""), NumberStyles.HexNumber);
            Color c = Color.FromArgb(argb);

            return c;
        }

        public static void SaveConfig(string customMessage)
        {
            Program.config.SaveToFile("config.json");
            Console.WriteLine(customMessage, Color.Yellow);
        }

        public static DiscordUser GetUserAsync(ulong id)
        {
            DiscordUser user = Program.client.GetUserAsync(id).Result;
            return user;
        }

        public static DiscordUser returnUserByName(string name)
        {
            DiscordUser user = null;
            foreach(DiscordMember ids in Program.client.GetGuildAsync(retrieveDiscordGuildID(Program.listeningGuild)).Result.Members)
            {
                if (ids.Username == name)
                {
                    user = ids; 
                }
            }
            return user;
        }

        public static void SaveConfig()
        {
            Program.config.SaveToFile("config.json");
            Console.WriteLine("Saved to config!!!", Color.Yellow);
        }

        public static string[] guilds()
        {
            List<string> guildsBuilt = new List<string>();
            IReadOnlyDictionary<ulong, DiscordGuild> guilds = Program.client.Guilds;
            foreach (ulong longs in guilds.Keys)
            {
                guildsBuilt.Add(guilds[longs].Name);
            }
            return guildsBuilt.ToArray();
        }

        public static ulong retrieveDiscordGuildID(string name)
        {
            ulong retrievedID = 00;
            IReadOnlyDictionary<ulong, DiscordGuild> guilds = Program.client.Guilds;
            foreach (ulong longs in guilds.Keys)
            {
                if (guilds[longs].Name == name)
                {
                    retrievedID = longs;
                }
            }
            return retrievedID;
        }

        public static DiscordGuild returnDiscordGuild(ulong id)
        {
            DiscordGuild guild = null;
            IReadOnlyDictionary<ulong, DiscordGuild> guilds = Program.client.Guilds;
            foreach (ulong longs in guilds.Keys)
            {
                if (guilds[longs].Id == id)
                {
                    guild = guilds[longs];
                }
            }
            return guild;
        }

        public static async Task<DiscordColor> returnDiscordRoleColourAsync(string username)
        {
            DiscordColor colorDiscord = DiscordColor.White;
            DiscordGuild guild = returnDiscordGuild(retrieveDiscordGuildID(Program.listeningGuild));
            IReadOnlyList<DiscordMember> users = await guild.GetAllMembersAsync();
            foreach (DiscordMember user in users)
            {
                if (user.Username == username)
                {
                    colorDiscord = user.Color;
                }
            }
            return colorDiscord;
        }

        public static DiscordChannel returnDiscordChannel(string name)
        {
            DiscordChannel returnedChannel = null;
            IReadOnlyList<DiscordChannel> channels = returnDiscordGuild(retrieveDiscordGuildID(Program.listeningGuild)).Channels;
            foreach (DiscordChannel channel in channels)
            {
                if (channel.Name == name)
                {
                    returnedChannel = channel;
                }
            }
            return returnedChannel;
        }
    }
}
