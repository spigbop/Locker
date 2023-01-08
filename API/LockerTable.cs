using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Rocket.Unturned.Player;
using UnityEngine;

namespace Locker.API
{
    public class LockerTable
    {
        public string Table = String.Empty;
        public string AssociatedPlugin = String.Empty;

        public TableType SaveType = TableType.FILE_SYSTEM;

        public Dictionary<string,dynamic> Structure = new Dictionary<string, dynamic>();

        public void PushUpdatesAsync() {
            
        }

        private async Task<bool> StreamUpdateTask() {
            return await Task.Run(async () => {
                await Task.Delay(0);
                string _mainstreamstring = String.Empty;
                foreach(KeyValuePair<string,dynamic> _structure in Structure) {
                    _mainstreamstring += $"{_structure.Key},{_structure.Value.ToString()}\n"; 
                }
                if(SaveType == TableType.FILE_SYSTEM) {
                    try {
                        StreamWriter writer = new StreamWriter($@"..\Locker\FileSystem\{Table}");
                        writer.Write(_mainstreamstring); writer.Close(); writer.Dispose();
                        return true;
                    }
                    catch { return false; }
                }
                else {
                    //mysql stuff
                    return true;
                }
            });
        }

        public bool Exists(string key) {
            return Structure.ContainsKey(key);
        }

        public bool ExistsPlayer(string key) {
            return Structure.ContainsKey(key);
        }

        public dynamic Get(string key) {
            dynamic _dynval;
            Structure.TryGetValue(key, out _dynval);
            return _dynval;
        }

        public void Set(string key, dynamic newvalue) {
            Structure[key] = newvalue;
            PushUpdatesAsync();
        }
    }

    public enum TableType {
        FILE_SYSTEM,
        MYSQL
    }
}
