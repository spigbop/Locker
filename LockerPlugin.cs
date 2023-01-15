using System;
using System.IO;
using System.Collections.Generic;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.API;
using UnityEngine;
using Locker.API;

namespace Locker
{
    public class LockerPlugin : RocketPlugin
    {
        public static LockerPlugin Instance;
        
        protected override void Load() {
            Instance = this;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[•] Locker Loaded. Messages starting with [•] Locker] are sent from Locker API.");
            if(!InitializeResources()) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[•] Locker configuration couldn't be loaded! Reverting to defaults.");
            }
        }

        protected override void Unload()
        {
            LockerStream.Save();
            base.Unload();
        }

        public static void Reload(IRocketPlayer caller) {
            Console.Write("[•] Locker Reloaded.");
            if(InitializeResources()) {
                UnturnedChat.Say(caller, "[•] Reloaded Locker configuration!", Color.yellow);
            }
            else {
                UnturnedChat.Say(caller, "[•] An error has occured while reloading!", Color.red);
            }
        }

        public static bool InitializeResources() {
            bool _overall = true;
            while(_overall == true) {
                _overall = Configuration.LoadConfig();
                //_overall = Second Resource
            }
            return _overall;
        }
    }
}
