using System;
using Locker.Workers;

namespace Locker {
    public class LockerStream {
        public static bool Save() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"[•] Saving Locker data to {Configuration.Map.FileSaveType}.\n");
            Console.ForegroundColor = ConsoleColor.White;
            switch(Configuration.Map.FileSaveType) {
                case "mysql":
                    var _mysqlsaver = new ILockerSaverMysql();
                    _mysqlsaver.CommitWithConfig();
                    return false;

                default:
                    var _filesaver = new ILockerSaverFilesystem(Configuration.Map.LocalfilesSavepath);
                    return _filesaver.Commit();
            }
        }

        public static bool Load() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"[•] Loading Locker data from {Configuration.Map.FileSaveType}.\n");
            Console.ForegroundColor = ConsoleColor.White;
            switch(Configuration.Map.FileSaveType) {
                case "mysql":
                    return false;
                
                default:
                    try {
                        var _loader = new ILockerLoaderFilesystem(Configuration.Map.LocalfilesSavepath);
                        _loader.Commit();
                    } catch { return false; }
                    return true;
            }
        }
    }
}
