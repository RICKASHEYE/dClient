using System;
using System.Collections.Generic;
using System.Text;

namespace dClient.Commands
{
    public class exit : Command
    {

        public exit(string name, string desc):base(name, desc)
        {

        }

        public override void Execute(string[] commandSplit, string otherCommand)
        {
            Environment.Exit(0);
            base.Execute(commandSplit, otherCommand);
        }
    }
}
