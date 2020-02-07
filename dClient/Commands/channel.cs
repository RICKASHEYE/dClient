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
            Program.listeningServer = otherCommand;
            Console.WriteLine("Changed channel listening to: " + otherCommand, Color.Green);
            if (Program.config.customtitle == "true")
            {
                Console.Title = Program.config._title + " - " + Program.listeningServer + " on " + Program.listeningGuild;
            }
            Console.WriteLine("");
            base.Execute(commandSplit, otherCommand);
        }
    }
}
