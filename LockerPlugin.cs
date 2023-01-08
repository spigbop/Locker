using System;
using System.Collections.Generic;
using Rocket.Core.Plugins;

namespace Locker
{
    public class LockerPlugin : RocketPlugin<Configuration>
    {
        public static LockerPlugin Instance;
        
        protected override void Load()
        {
            Instance = this;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[•] Locker] Locker Loaded. Messages starting with [•] Locker] are sent from Locker API.");
        }

        public static Configuration C()
        {
            return Instance.Configuration.Instance;
        }
    }
}
