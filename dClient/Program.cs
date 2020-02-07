using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace dClient
{
    public class Program
    {
        public static DiscordClient client;
        public static Config config;
        public static string listeningGuild;
        public static string listeningServer;
        public static Cache cache;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Checking for details...");
                try
                {
                    AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProcessExit);
                    //Load the config file
                    Console.WriteLine("Checking for config");
                    if (!File.Exists("config.json"))
                    {
                        //Load a new config
                        Console.Clear();
                        Console.WriteLine("Creating config as it doesnt exist!!!");
                        new Config().SaveToFile("config.json");
                        Console.WriteLine("Press any key to close...");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }

                    if (!File.Exists("cache.json"))
                    {
                        Console.Clear();
                        Console.WriteLine("Creating cache file as it doesnt exist!!!");
                        new Cache().SaveToFile("cache.json");
                        Console.WriteLine("Written file for cache");
                    }

                    config = Config.LoadFromFile("config.json");
                    cache = Cache.LoadFromFile("cache.json");
                    listeningGuild = config.defaultServer;
                    listeningServer = config.defaultChannel;
                    if (config.customtitle == "true")
                    {
                        Console.Title = config._title + " - " + listeningServer + " on " + listeningGuild;
                    }
                    client = new DiscordClient(new DiscordConfiguration()
                    {
                        AutoReconnect = true,
                        EnableCompression = true,
                        LogLevel = LogLevel.Error,
                        Token = config.Token,
                        TokenType = TokenType.User,
                        UseInternalLogHandler = true
                    });
                    Console.WriteLine("Trying to connect");
                    StartAsync().Wait();
                    Console.WriteLine("Connected!!!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine("Finished initialisation!");
                Console.WriteLine("READY");
                Console.WriteLine("get started with the command 'help'!");
                client.MessageCreated += async e =>
                {
                    if (e.Guild.Name == listeningGuild)
                    {
                        if (e.Channel.Name == listeningServer)
                        {
                            if (config.rlecolour == "true")
                            {
                                string author = e.Author.Username;
                                Color chosenColour = Color.White;
                                try
                                {
                                    if (config.savecache == "true")
                                    {
                                        try
                                        {
                                            bool saved = false;
                                            //Save the chosen colour into an array!!!
                                            foreach (string name in Cache.colours.Keys)
                                            {
                                                if (name == author)
                                                {
                                                    chosenColour = Cache.colours[name];
                                                    saved = true;
                                                }
                                            }

                                            if (saved == false)
                                            {
                                                Color colorUsed = API.FromHex(API.returnDiscordRoleColourAsync(e.Author.Username).Result.Value.ToString());
                                                //Save the array
                                                Cache.colours.Add(author, colorUsed);
                                                chosenColour = colorUsed;
                                                cache.SaveToFile("cache.json");
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            chosenColour = Color.White;
                                        }
                                    }
                                    else if (config.savecache == "false")
                                    {
                                        chosenColour = API.FromHex(API.returnDiscordRoleColourAsync(e.Author.Username).Result.Value.ToString());
                                    }

                                    string messageContent = e.Message.Content;
                                    if (messageContent.Contains("<@"))
                                    {
                                        string[] commandSplitM = messageContent.Split(' ');
                                        for (int i = 0; i < commandSplitM.Length; i++)
                                        {
                                            if (commandSplitM[i].Contains('@'))
                                            {
                                                commandSplitM[i] = commandSplitM[i].Replace('<', ' ');
                                                commandSplitM[i] = commandSplitM[i].Replace('>', ' ');
                                                commandSplitM[i] = commandSplitM[i].Replace('@', ' ');
                                                DiscordUser user = API.GetUserAsync(ulong.Parse(commandSplitM[i]));
                                                commandSplitM[i] = "@" + user.Username;
                                            }
                                        }
                                        messageContent = string.Join(' ', commandSplitM);
                                    }
                                    Console.WriteLine("<" + author + "> ", chosenColour);
                                    Console.WriteLine("> " + string.Join(' ', messageContent));
                                    Console.WriteLine("");
                                }
                                catch (Exception error)
                                {
                                    //Write the message that was given an error
                                    Console.WriteLine("Error: " + error);
                                }
                            }
                            else
                            {
                                Console.WriteLine("<" + e.Author.Username + "> ");
                                Console.WriteLine("> " + e.Message.Content);
                                Console.WriteLine("");
                            }
                        }
                    }
                };

                if (config.messagecheck == "true")
                {
                    client.MessageUpdated += async e =>
                    {
                        if (e.Guild.Name == listeningGuild)
                        {
                            if (e.Channel.Name == listeningServer)
                            {
                                Console.WriteLine("(UPDATED) <" + e.Author.Username + "> " + e.Message.Content);
                            }
                        }
                    };

                    client.MessageDeleted += async e =>
                    {
                        if (e.Guild.Name == listeningGuild)
                        {
                            if (e.Channel.Name == listeningServer)
                            {
                                Console.WriteLine("(DELETED) " + e.Message.Content);
                            }
                        }
                    }; 
                }

                if (config.customtitle == "true")
                {
                    client.TypingStarted += async e =>
                    {
                        if (e.Channel.Name == listeningServer)
                        {
                            if (e.Channel.Guild.Name == listeningGuild)
                            {
                            Console.Title = config._title + " - " + e.User.Username + " is typing on " + listeningGuild + " in " + listeningServer;
                            }
                        }
                    }; 
                }

                Executor.ExecuteCommand();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.Write("An External error occured");
                Console.Write(e);
            }
        }

        static void ProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("*Farts* exit...", Color.Green);
            //Save the config here but add a few new entries
            Program.config.defaultChannel = Program.listeningServer;
            Program.config.defaultServer = Program.listeningGuild;
            API.SaveConfig();
        }

        public static Task StartAsync() => client.ConnectAsync();

        public static Task StopAsync() => client.DisconnectAsync();
    }
}
