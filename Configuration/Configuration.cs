using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Locker {
    public static class Configuration {
        public static ConfigMappings Map;

// Ik. This is a very shitty way of doing it. I will include reading from the embed file later.
public const string ConfigDefaults = @"
#  _                _             
# | |              | |            
# | |     ___   ___| | _____ _ __ 
# | |    / _ \ / __| |/ / _ \ '__|
# | |___| (_) | (__|   <  __/ |   
# |______\___/ \___|_|\_\___|_|   

# Lines starting with # are comments.

# There are two save types: 'localfiles' or 'mysql'
file-save-type: localfiles

auto-saver:
  # Locker saves data before the server shuts down. But will need a proper shutdown and
  # if your server crashes it probably wont save! Enable auto saver to backup your data.
  enable-auto-saver: false
  # Time between each save in seconds
  auto-saver-interval: 3600

# Won't be used if 'file-save-type' is 'mysql'
localfiles-savepath: ..\Locker\Database\

# Won't be used if 'file-save-type' is 'localfiles'
mysql-connection:
  server-ip: 127.0.0.1
  port: 3306
  password: 3306
  database-name: locker-unturned
  # Supported charsets:
  charset: utf-8

# Setting this to 'false' won't fill your console with red error texts.
plugins-can-force-saves: true

# Lockermod Page: https://lockermod.github.io
";

        public static string _EnvoPath;
        public static string _LockPath;
        public static string _ConfigPath;

        public static bool LoadConfig() {
            _EnvoPath = Environment.CurrentDirectory;
            _LockPath = $@"{_EnvoPath}\Rocket\Plugins\Locker";
            _ConfigPath = $@"{_LockPath}\config.yml";

            try {
                var _MappingsSafe = Map;

                Map.FileSaveType = null;

                if(!File.Exists(_ConfigPath)) {
                    StreamWriter writer = new StreamWriter(_ConfigPath);
                    writer.Write(ConfigDefaults);
                    writer.Close(); writer.Dispose();
                }
            
                MapConfigsWithFixes();
                
                if(Map.FileSaveType == null) { 
                    Map = _MappingsSafe;
                    return false;
                }

                return true;
            } catch {
                StreamWriter writer = new StreamWriter(_ConfigPath);
                writer.Write(ConfigDefaults);
                writer.Close(); writer.Dispose();
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
    }

    public class ConfigMappings {
        public string FileSaveType { get; set; }
        public AutoSaverConfig AutoSaver { get; set; }
        public string LocalfilesSavepath { get; set; }
        public MySqlInfo MysqlConnection { get; set; }
        public bool PluginsCanForceSaves { get; set; }
    }

    public class AutoSaverConfig {
        public bool EnableAutoSaver { get; set; }
        public int AutoSaverInterval { get; set; }
    }

    public class MySqlInfo {
        public string ServerIp { get; set; }
        public string Port { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
        public string Charset { get; set; }
    }
}
