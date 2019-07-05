using dClient;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

public class Executor
{
    public static void ExecuteCommand()
    {
        string command = Console.ReadLine();
        string[] commandSplit = command.Split(' ');
        List<string> listCommand = new List<string>();
        for (int i = 1; i < commandSplit.Length; i++)
        {
            listCommand.Add(commandSplit[i]);
        }
        string otherCommand = String.Join(' ', listCommand);
        switch (commandSplit[0].ToLower())
        {
            case "guild":
                //Change the guild
                //Add everything to a list to make it easier
                List<string> guildM = new List<string>();
                foreach (string m in API.guilds())
                {
                    guildM.Add(m);
                }

                if (guildM.Contains(otherCommand))
                {
                    Program.listeningGuild = otherCommand;
                    Console.WriteLine("Changed guild listening to: " + otherCommand, Color.Green);
                    //Changing the channel to the default
                    Program.listeningServer = API.returnDiscordGuild(API.retrieveDiscordGuildID(otherCommand)).GetDefaultChannel().Name;
                    Console.WriteLine("Changed Listening channel to the default channel of: " + Program.listeningServer, Color.Green);
                    if (Program.config.customtitle == "true")
                    {
                        Console.Title = Program.config._title + " - " + Program.listeningServer + " on " + Program.listeningGuild;
                    }
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("That guild does not exist!", Color.Red);
                }
                break;
            case "channel":
                //Change the listening channel
                Program.listeningServer = otherCommand;
                Console.WriteLine("Changed channel listening to: " + otherCommand, Color.Green);
                if (Program.config.customtitle == "true")
                {
                    Console.Title = Program.config._title + " - " + Program.listeningServer + " on " + Program.listeningGuild;
                }
                Console.WriteLine("");
                break;
            case "guilds":
                //List all of the guilds!
                foreach (string m in API.guilds())
                {
                    Console.WriteLine(m, Color.Gray);
                    if (commandSplit.Length > 0)
                    {
                        if (commandSplit[1] == "-c")
                        {
                            foreach (DiscordChannel channel in API.returnDiscordGuild(API.retrieveDiscordGuildID(m)).Channels)
                            {
                                Console.WriteLine(" - > " + channel.Name, Color.LightGray);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("TIP : use -c as a argument after this command to get all the channels along with the guilds!", Color.BlueViolet);
                    }
                }
                Console.WriteLine("");
                break;
            case "channels":
                foreach (DiscordChannel channel in API.returnDiscordGuild(API.retrieveDiscordGuildID(Program.listeningGuild)).Channels)
                {
                    Console.WriteLine(channel.Name, Color.Gray);
                    if (commandSplit.Length > 0)
                    {
                        if (commandSplit[1] == "-c")
                        {
                            //Display the context of the channel
                            Console.WriteLine(" - > " + channel.Topic, Color.LightGray);
                            Console.WriteLine(" - > is NSFW? = " + channel.IsNSFW, Color.LightGray);
                        }
                    }
                    else
                    {
                        Console.WriteLine("TIP : use -c to as an argument to get if the channel is NSFW and the topic of the channel!", Color.BlueViolet);
                    }
                }
                Console.WriteLine("");
                break;
            default:
                //Chat to that person!
                string[] commandSplitM = command.Split(' ');
                for(int i = 0; i < commandSplitM.Length; i++)
                {
                    /*if (commandSplitM[i].Contains("<@"))
                    {
                        commandSplitM[i] = commandSplitM[i].Replace('<', ' ');
                        commandSplitM[i] = commandSplitM[i].Replace('>', ' ');
                        commandSplitM[i] = commandSplitM[i].Replace('@', ' ');
                        DiscordUser user = API.GetUserAsync(ulong.Parse(commandSplitM[i]));
                        commandSplitM[i] = "@" + user.Username;
                        indexMention = i;
                    }else */
                    if (commandSplitM[i].Contains("@"))
                    {
                        ulong result = 0;
                        bool tried = ulong.TryParse(commandSplitM[i], out result);
                        if (tried == true)
                        {
                            commandSplitM[i] = commandSplitM[i].Replace('@', ' ');
                            DiscordUser user = API.GetUserAsync(ulong.Parse(commandSplitM[i]));
                        } 
                    }
                }
                DiscordMessage message = API.returnDiscordChannel(Program.listeningServer).SendMessageAsync(string.Join(' ', commandSplitM)).Result;
                break;
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
            case "exit":
                Environment.Exit(0);
                break;
            case "settings":
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
                }
                break;
            case "dm":
                //DM a user
                
                break;
        }
        ExecuteCommand();
    }
}
