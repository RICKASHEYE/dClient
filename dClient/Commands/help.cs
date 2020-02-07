using System;
using System.Collections.Generic;
using System.Text;

namespace dClient.Commands
{
    public class help : Command
    {

        Command[] commands_;

        public help(string name, string desc, Command[] commands):base(name, desc)
        {
            commands_ = commands;
        }

        public override void Execute(string[] commandSplit, string otherCommand)
        {
            try
            {
                foreach (Command command in commands_)
                {
                    Console.WriteLine(command.name + " - " + command.description);
                }
            }
            catch (Exception e)
            {
                //unsupported issue
                Console.WriteLine("Error: " + e);
            }
            base.Execute(commandSplit, otherCommand);
        }
    }
}
