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
        public List<string> AssociatedPlugins = new List<string>();

        public LockerTable(string name) {
            Table = name;
        }

        public Dictionary<string,dynamic> Structure = new Dictionary<string, dynamic>();

        public bool Exists(string key) {
            return Structure.ContainsKey(key);
        }

        public bool ExistsPlayer(string key) {
            return Structure.ContainsKey(key);
        }

        public dynamic Get(string key) {
            dynamic _dynval;
            bool _canget = Structure.TryGetValue(key, out _dynval);
            if(!_canget) return null;
            return _dynval;
        }

        public void Set(string key, dynamic newvalue) {
            Structure[key] = newvalue;
        }
    }
}
