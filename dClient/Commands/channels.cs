using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace dClient.Commands
{
    public class channels : Command
    {
        public channels(string name, string desc):base(name, desc)
        {

        }

        public override void Execute(string[] commandSplit, string otherCommand)
        {
            foreach (DiscordChannel channel in API.returnDiscordGuild(API.retrieveDiscordGuildID(Program.listeningGuild)).Channels)
            {
                Console.WriteLine(channel.Name, Color.Gray);
                Console.WriteLine(" - > " + channel.Topic, Color.LightGray);
                Console.WriteLine(" - > is NSFW? = " + channel.IsNSFW, Color.LightGray);
            }
            Console.WriteLine("");
            base.Execute(commandSplit, otherCommand);
        }
    }
}
