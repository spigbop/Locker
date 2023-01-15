using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using UnityEngine;
using Locker.API;
using Locker.Workers;

namespace Locker.Commands
{
    public class LockerCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "locker";
        public string Help => "Main command for Locker and Locker API.";
        public string Syntax => "[sub-command] [params ...]";
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
                    UnturnedChat.Say(caller, "[•] Reloading Locker configurations...", Color.yellow);
                    LockerPlugin.Reload(caller);
                    break;
                
                case "saveall":
                    UnturnedChat.Say(caller, "[•] Successfully pushed all saves.", Color.yellow);
                    break;

                case "test":
                    var _table = LockerData.Table("testtable");
                    _table.Set("testkey", 300);
                    UnturnedChat.Say(caller, $"{_table.Get("testkey").ToString()}", Color.yellow);
                    break;
                
                case "testvar":
                    var _serialize = new IDataTypeSerializer();
                    if(arguments[1] == null) return;
                    dynamic _dyn = _serialize.Serialize(arguments[1]);
                    UnturnedChat.Say(caller, $"[•] Returns: {_dyn.ToString()} ({_dyn.Type.ToString()})", Color.yellow);
                    break;

                default:
                    UnturnedChat.Say(caller, "[•] This server is running Locker version-1.0.0 (Pineleaf)", Color.yellow);
                    break;
            }
        }
    }
}
