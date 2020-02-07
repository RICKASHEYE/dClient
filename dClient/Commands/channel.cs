using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace dClient.Commands
{
    public class channel : Command
    {
        public channel(string name, string desc):base(name, desc)
        {

        }

        public override void Execute(string[] commandSplit, string otherCommand)
        {
            Config config = Program.config;
            bool globalRead = bool.Parse(config.globalread);
            if (!globalRead)
            {
                Program.listeningServer = otherCommand;
                Console.WriteLine("Changed channel listening to: " + otherCommand, Color.Green);
                if (Program.config.customtitle == "true")
                {
                    Console.Title = Program.config._title + " - " + Program.listeningServer + " on " + Program.listeningGuild;
                }
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Global read is enabled, to turn it off please execute 'settings setglobal false'");
            }
            base.Execute(commandSplit, otherCommand);
        }
    }
}
