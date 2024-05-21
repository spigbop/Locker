using System;
using System.Collections.Generic;
using System.IO;
using Locker.API;
using MySql.Data.MySqlClient;

namespace Locker.Workers {
    public class ILockerSaverMysql {
        public bool CommitWithConfig() {
            var _sqlinfo = Configuration.Map.MysqlConnection;
            string _connectionString = $"Server={_sqlinfo.ServerIp};Port={_sqlinfo.Port};CharSet={_sqlinfo.Charset};Database={_sqlinfo.DatabaseName};Uid={_sqlinfo.UsernameId};Pwd={_sqlinfo.Password};";
            try {
                MySqlConnection connection = new MySqlConnection(_connectionString);
                connection.Open();
                foreach(LockerTable _table in LockerData.Structure) {
                    foreach(KeyValuePair<string,dynamic> _structure in _table.Structure) {
                        string insertQuery = $"INSERT INTO {_table.Table} (key, value) VALUES ('{_structure.Key}', '{_structure.Value}')";
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
                connection.Dispose();
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return true;
        }
    }
}
