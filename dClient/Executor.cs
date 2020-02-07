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
        //Register all of the commands
        Command[] commands = { new dClient.Commands.channel("channel", "Change channel by giving it a channel name"),
                               new dClient.Commands.channels("channels", "Get all discord channels"),
                               new dClient.Commands.exit("exit", "Exit the current session"),
                               new dClient.Commands.guild("guild", "Change the current selected guild by giving the name as a argument"),
                               new dClient.Commands.guilds("guilds", "Get all of the discord guilds"),
                               new dClient.Commands.settings("settings", "Gives you an entire variety of commands to modify the config at runtime"),
                               new dClient.Commands.stats("stats", "Gives you a variety of commands to execute to get stats of the current session") };

        bool executedCommand = false;
        foreach(Command com in commands)
        {
            if(commandSplit[0] == com.name.ToLower())
            {
                //Execute this command
                com.Execute(commandSplit, otherCommand);
                executedCommand = true;
            }
        }

        if(executedCommand == false)
        {
            string[] commandSplitM = command.Split(' ');
            for (int i = 0; i < commandSplitM.Length; i++)
            {
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
        }
        ExecuteCommand();
    }
}
