using System;
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
            if(!InitializeResources()) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"[•] Locker {TagResourcesFail} couldn't be loaded! Reverting to defaults.\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            if(LockerData.Structure.Count == 0) {
                Console.Write("  _                _              Welcome to Locker 1.0.0-pineleaf\n");
                Console.Write(" | |              | |              ~ A plugin by Xpoxy.\n");
                Console.Write(" | |     ___   ___| | _____ _ __  To start use: /locker\n");
                Console.Write(" | |    / _ \ / __| |/ / _ \ '__| This huge text will not appear\n");
                Console.Write(" | |___| (_) | (__|   <  __/ |    after locker saves some data!\n");
                Console.Write(" |______\___/ \___|_|\_\___|_|    For more visit: lockermod.github.io\n");
            }
            Console.Write("[•] Locker Loaded. Messages starting with [•] are sent from Locker API.\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        protected override void Unload() {
            if(!LockerStream.Save()) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[•] Locker was unable to save! Try checking if the folder is readonly or the mysql is set up properly.\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            base.Unload();
        }

        public static void Reload(IRocketPlayer caller) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[•] Reloading Locker...\n");
            Console.ForegroundColor = ConsoleColor.White;
            if(InitializeResources()) {
                UnturnedChat.Say(caller, "[•] Reloaded Locker configuration and database!\n", Color.green);
            }
            else {
                UnturnedChat.Say(caller, "[•] An error has occured while reloading Locker!\n", Color.red);
            }
        }

        private static string TagResourcesFail;

        public static bool InitializeResources() {
            if(!Configuration.LoadConfig()) { TagResourcesFail = "configuration"; return false; }
            if(!LockerStream.Load()) { TagResourcesFail = "database"; return false; }
            return true;
        }

        public IEnumerator AutoSaver() {
            while(true) {
                if(!Configuration.Map.AutoSaver.EnableAutoSaver) yield break;
                yield return new WaitForSeconds(Configuration.Map.AutoSaver.AutoSaverInterval);
                if(!LockerStream.Save()) {
                    yield break;
                }
            }
        }
    }
}
