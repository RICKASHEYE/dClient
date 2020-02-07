using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace dClient.Commands
{
    public class guilds : Command
    {

        public guilds(string name, string desc):base(name, desc)
        {

        }

        public override void Execute(string[] commandSplit, string otherCommand)
        {
            Config config = Program.config;
            bool globalChat = bool.Parse(config.globalread);
            if (!globalChat)
            {
                foreach (string m in API.guilds())
                {
                    Console.WriteLine(m, Color.Gray);
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
