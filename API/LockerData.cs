using System;
using System.Collections.Generic;

namespace Locker.API {
    public static class LockerData {
        public static List<LockerTable> Structure = new List<LockerTable>();

        public static LockerTable Table(string tablename) {
            var _table = Structure.Find(LockerTable => LockerTable.Table == tablename);
            if(_table == null) {
                _table = new LockerTable(tablename);
                Structure.Add(_table);
                return _table;
            }
            else { return _table; }
        }
    }
}
