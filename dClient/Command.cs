using System;
using System.Collections.Generic;
using System.Text;

namespace dClient
{
    public class Command
    {
        public string name;
        public string description;

        public Command(string name_, string description_)
        {
            name = name_;
            description = description_;
        }

        public virtual void Execute(string[] commandSplit, string otherCommand)
        {

        }
    }
}
