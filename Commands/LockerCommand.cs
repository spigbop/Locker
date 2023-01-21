using System;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using UnityEngine;
using Locker.API;
using Locker.Workers;
using System.Linq;

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
                
                case "saveall":
                    if(!LockerStream.Save()) { UnturnedChat.Say(caller, "[•] Something went wrong while saving!", Color.red); return; }
                    UnturnedChat.Say(caller, "[•] Successfully pushed all saves.", Color.yellow);
                    break;

                case "version":
                    UnturnedChat.Say(caller, "[•] This server is running Locker version-1.0.0 (Pineleaf)", Color.yellow);
                    break;

                case "dbgconfig":
                    UnturnedChat.Say(caller, $"[•] Path: {Configuration.Map.LocalfilesSavepath}", Color.yellow);
                    break;

                case "dbgsettable":
                    LockerData.Table("debug").Set("test", "example");
                    break;

                case "dbgtable":
                    UnturnedChat.Say(caller, $"[•] Debug Table Name: {LockerData.Table("debug").Table}", Color.yellow);
                    UnturnedChat.Say(caller, $"[•] 'test' Structure: {LockerData.Table("debug").Get("test")}", Color.yellow);
                    break;

                default:
                    UnturnedChat.Say(caller, $"[•] '{arguments[0]} is not recognized as a subcommand.' ", Color.yellow);
                    UnturnedChat.Say(caller, "[•] Available Subcommands: 'version', 'reload', 'saveall'", Color.yellow);
                    break;
            }
        }
    }
}
