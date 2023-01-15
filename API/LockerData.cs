using System;
using System.Collections.Generic;

namespace Locker.API {
    public static class LockerData {
        public static List<LockerTable> Structure = new List<LockerTable>();

        public static LockerTable Table(string tablename) {
            return new LockerTable("test");
        }
    }
}
