using Rocket.API;
using Locker.API;

namespace Locker
{
    public class Configuration : IRocketPluginConfiguration
    {
        public string FileSystemSavePath;
        public bool UseMySQL;
        //public API.SQLConnect MySQL;

        public void LoadDefaults()
        {
            FileSystemSavePath = @"..\Locker\data.txt";
            UseMySQL = false;
            //MySQL = new API.SQLConnect
            //{
            //    SERVER = "127.0.0.1",
            //    DATABASE = "locker-unturned",
            //    PASSWORD = "",
            //    PORT = "3306",
            //    charset = "utf8"
            //};
        }
    }
}
