using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace dClient.Commands
{
    public class guild : Command
    {

        public guild(string name, string desc):base(name, desc)
        {

        }

        public override void Execute(string[] commandSplit, string otherCommand)
        {
            Config config = Program.config;
            bool globalRead = bool.Parse(config.globalread);
            if (!globalRead)
            {
                //Change the guild
                //Add everything to a list to make it easier
                List<string> guildM = new List<string>();
                foreach (string m in API.guilds())
                {
                    guildM.Add(m);
                }

                if (guildM.Contains(otherCommand))
                {
                    Program.listeningGuild = otherCommand;
                    Console.WriteLine("Changed guild listening to: " + otherCommand, Color.Green);
                    //Changing the channel to the default
                    Program.listeningServer = API.returnDiscordGuild(API.retrieveDiscordGuildID(otherCommand)).GetDefaultChannel().Name;
                    Console.WriteLine("Changed Listening channel to the default channel of: " + Program.listeningServer, Color.Green);
                    if (Program.config.customtitle == "true")
                    {
                        Console.Title = Program.config._title + " - " + Program.listeningServer + " on " + Program.listeningGuild;
                    }
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("That guild does not exist!", Color.Red);
                }
            }
            else
            {
                Console.WriteLine("Global read is enabled, to turn it off please execute 'settings setglobal false'");
            }
            base.Execute(commandSplit, otherCommand);
        }
    }
}
