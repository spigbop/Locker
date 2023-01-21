using System;
using System.Collections.Generic;
using System.IO;
using Locker.API;

namespace Locker.Workers {
    public class ILockerSaverFilesystem {
        private string _path;
        
        public ILockerSaverFilesystem(string path) {
            _path = path;
        }
        
        public bool Commit() {
            try {
                if(!Directory.Exists(_path)) { 
                    Directory.CreateDirectory(_path);
                }
                foreach(LockerTable _table in LockerData.Structure) {
                    string _savedump = String.Empty;
                    foreach(KeyValuePair<string,dynamic> _structure in _table.Structure) {
                        _savedump = $"{_savedump}{_structure.Key},{_structure.Value}\n";
                    }
                    string _tablepath = Path.Combine(_path, $"{_table.Table}.locker");
                    var _writer = new StreamWriter(_tablepath);
                    _writer.Write(_savedump);
                    _writer.Close(); _writer.Dispose();
                    DateTime _edited = DateTimeOffset.FromUnixTimeSeconds(_table.LastEdited).DateTime;
                    File.SetLastWriteTime(_tablepath, _edited);
                }
                
            } catch(Exception ex) { 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"[•] Locker Save Panic => {ex.ToString()}   === Unable to save\n");
                Console.ForegroundColor = ConsoleColor.White;
                return false; }
            return true;
        }
    }
}
