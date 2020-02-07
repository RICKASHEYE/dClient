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

            foreach (string m in API.guilds())
            {
                Console.WriteLine(m, Color.Gray);
            }
            Console.WriteLine("");
            base.Execute(commandSplit, otherCommand);
        }
    }
}
