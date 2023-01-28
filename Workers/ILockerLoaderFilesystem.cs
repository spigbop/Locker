using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.IO;
using Locker.API;

namespace Locker.Workers {
    public class ILockerLoaderFilesystem {
        private string _path;

        public ILockerLoaderFilesystem(string folder) {
            _path = folder;
        }

        public bool Commit() {
            var _dir = new DirectoryInfo(_path);
            DirectorySecurity dSecurity = _dir.GetAccessControl();
            var _serializer = new IDataTypeSerializer();
            LockerData.Structure.Clear();
            try {
                foreach(var _content in _dir.GetFiles()) {
                    string _rawtext = String.Empty;
                    var reader = new StreamReader(_path);
                    _rawtext = reader.ReadToEnd();
                    reader.Close(); reader.Dispose();

                    var _dict = new Dictionary<string,dynamic>();
                    foreach(string _line in _rawtext.Split('\n')) {
                        if(_line.Contains(",")) { 
                            string[] _arydict = _line.Split(',');
                            _dict[_arydict[0]] = _serializer.Serialize(_arydict[1]);
                        }
                    }

                    var _locktable = new LockerTable(_content.Name);
                    _locktable.Structure = _dict;

                    LockerData.Structure.Add(_locktable);
                }
                return true;
            }
            catch(Exception ex) { 
                Console.ForegroundColor = ConsoleColor.Red;
                if(ex.GetType() == typeof(System.IO.DirectoryNotFoundException)) {
                    Console.Write($"[•] Locker has no permission to read from this file. Unable to load :(\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }
                Console.Write($"[•] Locker Load Panic => {ex.ToString()}   === Unable to load :(\n");
                Console.ForegroundColor = ConsoleColor.White;
                return false; }
        }
    }
}
