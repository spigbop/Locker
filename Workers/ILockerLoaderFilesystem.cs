using System;
using System.Collections.Generic;
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
                        string[] _arydict = _line.Split(',');
                        _dict[_arydict[0]] = _serializer.Serialize(_arydict[1]);
                    }

                    var _locktable = new LockerTable(_content.Name);
                    _locktable.Structure = _dict;

                    LockerData.Structure.Add(_locktable);
                }
                return true;
            }
            catch { return false; }
        }
    }
}