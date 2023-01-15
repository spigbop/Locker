using System;
using Locker.Workers;

namespace Locker {
    public class LockerStream {
        public static void Save() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"[•] Saving Locker data to {Configuration.Map.FileSaveType}.");
            switch(Configuration.Map.FileSaveType) {
                case "mysql":
                    
                    break;
                
                default:
                    var _saver = new ILockerSaverFilesystem(Configuration.Map.LocalfilesSavepath);
                    _saver.Commit();
                    break;
            }
        }

        public static void Load() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"[•] Loading Locker data from {Configuration.Map.FileSaveType}.");
            switch(Configuration.Map.FileSaveType) {
                case "mysql":
                    
                    break;
                
                default:
                    var _loader = new ILockerLoaderFilesystem(Configuration.Map.LocalfilesSavepath);
                    _loader.Commit();
                    break;
            }
        }
    }
}
