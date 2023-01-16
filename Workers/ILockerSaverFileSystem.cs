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
                foreach(LockerTable _table in LockerData.Structure) {
                    string _savedump = String.Empty;
                    foreach(KeyValuePair<string,dynamic> _structure in _table.Structure) {
                        _savedump = $"{_savedump}{_structure.Key},{_structure.Value}\n";
                    }
                    string _tablepath = Path.Combine(_path, $"{_table.Table}.txt");
                    var _writer = new StreamWriter(_tablepath);
                    _writer.Write(_savedump);
                    _writer.Close(); _writer.Dispose();
                    DateTime _edited = DateTimeOffset.FromUnixTimeSeconds(_table.LastEdited).DateTime;
                    File.SetLastWriteTime(_tablepath, _edited);
                }
                
            } catch { return false; }
            return true;
        }
    }
}
