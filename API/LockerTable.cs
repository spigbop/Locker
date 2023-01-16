using System;
using System.Collections.Generic;

namespace Locker.API
{
    public class LockerTable
    {
        public string Table = String.Empty;
        public List<string> AssociatedPlugins = new List<string>();
        public long LastEdited;

        public LockerTable(string name) {
            Table = name;
            UpdateEdited();
        }

        public Dictionary<string,dynamic> Structure = new Dictionary<string, dynamic>();

        public bool Exists(string key) {
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
            var _this = LockerData.Structure.Find(LockerData => LockerData.Table == Table);
            if(_this != null) LockerData.Structure.Remove(_this);
            LockerData.Structure.Add(_this);
            UpdateEdited();
        }

        private void UpdateEdited() {
            TimeSpan _t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int _epoch = (int)_t.TotalSeconds;
            LastEdited = _epoch;
        }
    }
}
