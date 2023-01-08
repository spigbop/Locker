using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using UnityEngine;
using Locker.API;

namespace Locker.Commands
{
    public class LockerCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "locker";
        public string Help => "Main command for Locker and Locker API.";
        public string Syntax => "[sub-command] [params ...]";
        public List<string> Permissions => new List<string>
        { "locker.info" };

        public void Execute(IRocketPlayer caller, string[] arguments)
        {
            switch(arguments[0]) {
                case "test":
                    UnturnedChat.Say(caller, $"{LockerData.Structure}", Color.yellow);
                    break;
                
                default:
                    UnturnedChat.Say(caller, "[•] This server is running Locker version-1.0.0 (Pineleaf)", Color.yellow);
                    break;
            }
        }
    }
}
