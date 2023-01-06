using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Bank.DB
{
    class DBConnection
    {
        private string _server = "localhost";
        private string _databaseName = "TestBank";
        private string _username = "";
        private string _password = "";

        private static DBConnection _instance = null;

        public MySqlConnection mysqlConnection;

        //constructor is private; Singleton
        private DBConnection()
        {
            string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3};", _server, _databaseName, _username, _password);
            mysqlConnection = new MySqlConnection(connstring);
            mysqlConnection.Open();
        }

        public static DBConnection GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DBConnection();
            }
            return _instance;
        }
    }
}
