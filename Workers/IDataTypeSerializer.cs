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
}