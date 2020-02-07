using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace dClient.Commands
{
    public class settings : Command
    {
        public settings(string name, string desc):base(name, desc)
        {

        }

        public override void Execute(string[] commandSplit, string otherCommand)
        {
            switch (commandSplit[1].ToLower())
            {
                case "rolecolours":
                    bool changeColours = API.stringtobool(commandSplit[2].ToLower());
                    Console.WriteLine("Setting role colours was set to: " + changeColours, Color.Green);
                    Program.config.rlecolour = commandSplit[2].ToLower();
                    API.SaveConfig();
                    break;
                case "customtitle":
                    bool customTitle = API.stringtobool(commandSplit[2].ToLower());
                    Console.WriteLine("Setting customizable title was set to: " + customTitle, Color.Green);
                    Program.config.customtitle = commandSplit[2].ToLower();
                    if (customTitle == false)
                    {
                        Console.Title = Environment.SystemDirectory;
                    }
                    else
                    {
                        Console.Title = Program.config._title + " - " + Program.listeningServer + " on " + Program.listeningGuild;
                    }
                    API.SaveConfig();
                    break;
                case "messagechecking":
                    bool mcheck = API.stringtobool(commandSplit[2].ToLower());
                    Console.WriteLine("Setting message checking was set to: " + mcheck, Color.Green);
                    Program.config.messagecheck = commandSplit[2].ToLower();
                    API.SaveConfig();
                    break;
                case "savecache":
                    //Saving the cache can bring up performance
                    bool sccheck = API.stringtobool(commandSplit[2].ToLower());
                    Console.WriteLine("Setting saving cache was set to: " + sccheck, Color.Green);
                    Program.config.savecache = commandSplit[2].ToLower();
                    API.SaveConfig();
                    break;
                case "setglobal":
                    //Set to global connection
                    bool pcheck = API.stringtobool(commandSplit[2].ToLower());
                    Console.WriteLine("Setting global read was set to: " + pcheck, Color.Green);
                    Program.config.savecache = commandSplit[2].ToLower();
                    API.SaveConfig();
                    break;
                case "help":
                default:
                    //Display help for all settings commands
                    Console.WriteLine("rolecolours > toggle if you want role colours on or off");
                    Console.WriteLine("customtitle > toggle on or off if you want the title to follow whats in config");
                    Console.WriteLine("messagechecking > honestly dont know what this does");
                    Console.WriteLine("savecache > to speed things up in chat");
                    Console.WriteLine("setglobal > to set chat to read globally, you will be able to chat with 'globalchat (guildname) (message)");
                    break;
            }
            base.Execute(commandSplit, otherCommand);
        }
    }
}
