using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Bank.DB;

namespace Bank.BL
{
    public class Customers
    {
        public int ID = 0;
        public string name = string.Empty;
        public double balance = 0;

        public DataTable GetAll()
        {
            DataTable dataTable = new DataTable();
            string query = "select * from Customers";
            //You can pass connection as parameter
            MySqlCommand selectCmd = new MySqlCommand(query, DBConnection.GetInstance().mysqlConnection);
            MySqlDataAdapter da = new MySqlDataAdapter(selectCmd);
            da.Fill(dataTable);
            return dataTable;
        }

        public DataTable DeleteAll()
        {
            DataTable dataTable = new DataTable();
            string query = "delete from Customers";
            //You can pass connection as parameter
            MySqlCommand selectCmd = new MySqlCommand(query, DBConnection.GetInstance().mysqlConnection);
            MySqlDataAdapter da = new MySqlDataAdapter(selectCmd);
            da.Fill(dataTable);
            return dataTable;
        }

        public void Insert()
        {
            var insertCmd = new MySqlCommand("INSERT INTO Customers(name, balance) VALUES(@name, @balance); SELECT LAST_INSERT_ID();");
            insertCmd.Connection = DBConnection.GetInstance().mysqlConnection;

            //Add Parameters
            insertCmd.Parameters.Add("name", MySqlDbType.VarChar, 255).Value = this.name;
            insertCmd.Parameters.Add("balance", MySqlDbType.Double).Value = this.balance;
            this.ID = int.Parse(insertCmd.ExecuteScalar().ToString());
        }

        public void Update()
        {
            var updateCmd = new MySqlCommand("Update Customers SET name=@name, balance=@balance where id=@id");
            updateCmd.Connection = DBConnection.GetInstance().mysqlConnection;

            //Add Parameters
            updateCmd.Parameters.Add("name", MySqlDbType.VarChar, 255).Value = this.name;
            updateCmd.Parameters.Add("balance", MySqlDbType.Double).Value = this.balance;
            updateCmd.Parameters.Add("id", MySqlDbType.VarChar, 255).Value = this.ID;
            updateCmd.ExecuteNonQuery();
        }

        public DataTable GetByName_Like(string p_search)
        {
            DataTable dataTable = new DataTable();
            string query = "select Name from Customers where Name LIKE concat('%', @search, '%')";
            //You can pass connection as parameter
            MySqlCommand selectCmd = new MySqlCommand(query, DBConnection.GetInstance().mysqlConnection);
            selectCmd.Parameters.Add("search", MySqlDbType.VarString).Value = p_search;
            MySqlDataAdapter da = new MySqlDataAdapter(selectCmd);
            da.Fill(dataTable);
            return dataTable;
        }
    }
}
