using System;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Locker.Commands
{
    public class LockerCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "locker";
        public string Help => "Main command for Locker.";
        public string Syntax => "[sub-command] [params ...]";
        public List<string> Aliases => new List<string>() {  };
        public List<string> Permissions => new List<string>
        { "locker" };

        public void Execute(IRocketPlayer caller, string[] arguments)
        {
            if(arguments.Length == 0) {
                UnturnedChat.Say(caller, "[•] This server is running Locker version-1.0.0 (Pineleaf)", Color.yellow);
                return;
            }
            
            switch(arguments[0]) {
                case "reload":
                    if(!caller.HasPermission("locker.reload")) {
                        UnturnedChat.Say(caller, "[•] You don't have permission to reload.", Color.red);
                        return;
                    }
                    LockerPlugin.Reload(caller);
                    break;
                
                case "save":
                    if(!LockerStream.Save()) { UnturnedChat.Say(caller, "[•] Something went wrong while saving!", Color.red); return; }
                    UnturnedChat.Say(caller, "[•] Successfully pushed all saves.", Color.yellow);
                    break;

                case "load":
                    if(!LockerStream.Load()) { UnturnedChat.Say(caller, "[•] Something went wrong while saving!", Color.red); return; }
                    UnturnedChat.Say(caller, "[•] Successfully pushed all saves.", Color.yellow);
                    break;

                case "version":
                    UnturnedChat.Say(caller, "[•] This server is running Locker version-1.0.0 (Pineleaf)", Color.yellow);
                    break;

                case "website":
                    var _uplayer = caller as UnturnedPlayer;
                    if(_uplayer == null) {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("[•] https://lockermod.github.io");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else {
                        _uplayer.Player.sendBrowserRequest("Locker Website", "https://lockermod.github.io");
                    }
                    break;

                default:
                    UnturnedChat.Say(caller, $"[•] '{arguments[0]} is not recognized as a subcommand.'", Color.yellow);
                    UnturnedChat.Say(caller, "[•] Available Subcommands: 'version', 'reload', 'website', 'save', 'load'", Color.yellow);
                    break;
            }
        }
    }
}
