using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace dClient.Commands
{
    public class stats : Command
    {
        public stats(string name, string desc):base(name, desc)
        {

        }

        public override void Execute(string[] commandSplit, string otherCommand)
        {
            Config config = Program.config;
            bool globalread = bool.Parse(config.globalread);
            switch (commandSplit[1].ToLower())
            {
                case "rolecolour":
                    //Return your rolecolour
                    Console.WriteLine("Role Colour: " + API.returnDiscordRoleColourAsync(otherCommand).Result, API.FromHex(API.returnDiscordRoleColourAsync(otherCommand).Result.ToString()));
                    break;
                case "username":
                    Console.WriteLine("Your username is : " + Program.client.CurrentUser.Username, Color.Yellow);
                    break;
                case "currentchannel":
                    if (!globalread)
                    {
                        Console.WriteLine("The channel your in is : " + Program.listeningServer, Color.Yellow);
                    }
                    else
                    {
                        Console.WriteLine("Global read is enabled, to turn it off please execute 'settings setglobal false'");
                    }
                    break;
                case "currentguild":
                    if (!globalread)
                    {
                        Console.WriteLine("The server your in is : " + Program.listeningGuild, Color.Yellow);
                    }
                    else
                    {
                        Console.WriteLine("Global read is enabled, to turn it off please execute 'settings setglobal false'");
                    }
                    break;
                case "help":
                default:
                    Console.WriteLine("username - to get the current username you have in the current chat");
                    if (!globalread)
                    {
                        Console.WriteLine("rolecolour - to get the current role colour you have in the current chat");
                        Console.WriteLine("currentchannel - to get the current channel you have in the guild you are in");
                        Console.WriteLine("currentguild - to get the current guild you are in");
                    }
                    else
                    {
                        Console.WriteLine("Global read is enabled some commands may be hidden. to turn it off please execute 'settings setglobal false'");
                    }
                    break;
            }
            base.Execute(commandSplit, otherCommand);
        }
    }
}
