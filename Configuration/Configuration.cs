using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.Reflection;

namespace Locker {
    public static class Configuration {
        public static ConfigMappings Map;

        public static string _EnvoPath;
        public static string _LockPath;
        public static string _ConfigPath;

        public static bool LoadConfig() {
            _EnvoPath = Environment.CurrentDirectory;
            _LockPath = $@"{_EnvoPath}\Plugins\Locker";
            _ConfigPath = $@"{_LockPath}\config.yml";

            try {
                //var _MappingsSafe = Map;

                //Map.FileSaveType = null;

                if(!File.Exists(_ConfigPath)) {
                    WriteDefaults();
                }
            
                MapConfigsWithFixes();
                
                //if(Map.FileSaveType == null) { 
                //    Map = _MappingsSafe;
                //    return false;
                //}

                return true;
            } catch {
                WriteDefaults();
                MapConfigsWithFixes();
                return false;
            }
        }

        private static ConfigMappings ParseTagMaps(string contents) {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(HyphenatedNamingConvention.Instance)
                .Build();
            return deserializer.Deserialize<ConfigMappings>(contents);
        }

        private static void MapConfigsWithFixes() {
            Map = ParseTagMaps(File.ReadAllText(_ConfigPath));
            Map.LocalfilesSavepath = Map.LocalfilesSavepath.Replace("..", _LockPath);
        }

        private static void WriteDefaults() {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Locker.Configuration.ConfigDefaults.yml");
            StreamReader reader = new StreamReader(stream);
            string ConfigDefaults = reader.ReadToEnd();
            reader.Close(); reader.Dispose();
            stream.Close(); stream.Dispose();

            StreamWriter writer = new StreamWriter(_ConfigPath, false);
            writer.Write($"{ConfigDefaults}\n# Locker Configuration file generated {DateTime.Now.ToString()}\n");
            writer.Close(); writer.Dispose();
        }
    }

    public class ConfigMappings {
        public string FileSaveType { get; set; }
        public AutoSaverConfig AutoSaver { get; set; }
        public string LocalfilesSavepath { get; set; }
        public MySqlInfo MysqlConnection { get; set; }
        public bool PluginsCanForceSaves { get; set; }
    }

    public class AutoSaverConfig {
        public bool Enable { get; set; }
        public int Interval { get; set; }
        public bool ConsoleSendMessage { get; set; }
    }

    public class MySqlInfo {
        public string ServerIp { get; set; }
        public string UsernameId { get; set; }
        public string Port { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
        public string Charset { get; set; }
    }
}
