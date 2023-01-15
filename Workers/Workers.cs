using System;
using System.Collections.Generic;
using System.IO;
using Locker.API;

namespace Locker.Workers {
    public class IDataTypeSerializer {
        public dynamic Serialize(string obsolete) {
            switch(obsolete) {
                case "null":
                case "nil":
                case "None":
                    return null;
                
                case "true":
                case "True":
                    return true;

                case "false":
                case "False":
                    return false;

                default:
                    int _intvar = 0;
                    bool _isend = int.TryParse(obsolete, out _intvar);
                    if(_isend) { return _intvar; }

                    float _floatvar = 0f;
                    _isend = float.TryParse(obsolete, out _floatvar);
                    if(_isend) { return _floatvar; }

                    return obsolete;
            }
        }
    }

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
                    var _writer = new StreamWriter(_path);
                    _writer.Write(_savedump);
                    _writer.Close(); _writer.Dispose();
                }
                
            } catch { return false; }
            return true;
        }
    }

    public class ILockerLoaderFilesystem {
        private string _path;

        public ILockerLoaderFilesystem(string folder) {
            _path = folder;
        }

        public bool Commit() {
            var _dir = new DirectoryInfo(_path);
            var _serializer = new IDataTypeSerializer();
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
