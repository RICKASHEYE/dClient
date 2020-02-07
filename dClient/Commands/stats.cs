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
                    Console.WriteLine("The channel your in is : " + Program.listeningServer, Color.Yellow);
                    break;
                case "currentguild":
                    Console.WriteLine("The server your in is : " + Program.listeningGuild, Color.Yellow);
                    break;
            }
            base.Execute(commandSplit, otherCommand);
        }
    }
}
