using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BudgetApp_V2
{
    public class MySQLConnection
    {
        public string connection = "server=localhost;user=root;database=budget;port=3306;password=;";


        public string FixStringForMySQL(string str)
        {
            //Method to allow single quotes and newline characters in MySQL statements
            string newStr = "";

            for (int i = 0; i < str.Length; i++)
            {
                //Allow insertion of apostrophes and newline characters.
                if (str[i] == (char)39)
                {
                    newStr += "\\'";
                }
                else if (str[i].Equals("\n"))
                {
                    newStr += "\\n";
                }
                else
                {
                    newStr = newStr + str[i];
                }
            }
            return newStr;
        }

        

        public LinkedList<String[]> GetCurrentMonthsTransactions()
        {
            LinkedList<String[]> transactions = new LinkedList<String[]>();


            string connStr = new MySQLConnection().connection;

            MySqlConnection connection = new MySqlConnection(connStr);

            string sql = "SELECT trans_date, description, amount FROM expenses WHERE YEAR(trans_date) = YEAR(NOW()) AND MONTH(trans_date) = MONTH(NOW()) ORDER BY trans_date DESC; ";

            connection = new MySqlConnection(connStr);    //create the new connection using the parameters of connStr
            try
            {
                connection.Open();                            //open the connection
                var cmd = new MySqlCommand(sql, connection);  //create an executable command
                var reader = cmd.ExecuteReader();             //execute the command

                while (reader.Read())
                {
                    String[] currentTransaction = new String[3];
                    DateTime dt = reader.GetDateTime(0);  //Get the date.
                    string dtString = dt.Month + "/" + dt.Day + "/" + dt.Year;
                    currentTransaction[0] = dtString;
                    currentTransaction[1] = reader.GetString(1);  //Get the description.
                    currentTransaction[2] = reader.GetString(2);  //Get the amount.
                    transactions.AddLast(currentTransaction);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            connection.Close();
            return transactions;
        }

        
        public void ModifyCharityBalance(string transDate, string description, double amount)
        {
            //Place the transaction in the database.
            string connStr = new MySQLConnection().connection;

            string sql = "";
            //Build the INSERT string.
            sql = "INSERT INTO charity (trans_date, description, amount) VALUES ('" + transDate + "', '" + description + "', " + amount + ");";
            MySqlConnection connection = new MySqlConnection(connStr);    //create the new connection using the parameters of connStr
            try
            {
                connection.Open();                            //open the connection
                var cmd = new MySqlCommand(sql, connection);  //create an executable command
                var reader = cmd.ExecuteNonQuery();             //execute the command
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            connection.Close();
        }
        

        public double GetCharityBalance()
        {
            string connStr = new MySQLConnection().connection;

            MySqlConnection connection = new MySqlConnection(connStr);

            string sql = "SELECT SUM(amount) FROM charity; ";

            connection = new MySqlConnection(connStr);    //create the new connection using the parameters of connStr
            try
            {
                connection.Open();                            //open the connection
                var cmd = new MySqlCommand(sql, connection);  //create an executable command
                var reader = cmd.ExecuteReader();             //execute the command

                while (reader.Read())
                {
                    return reader.GetDouble(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            throw new Exception("Could not fetch amount from charity.");
        }

        public LinkedList<string> GetCategories()
        {
            LinkedList<string> categories = new LinkedList<string>();

            string connStr = new MySQLConnection().connection;

            MySqlConnection connection = new MySqlConnection(connStr);

            string sql = "SELECT type_name FROM expense_types ORDER BY type_name; ";

            connection = new MySqlConnection(connStr);    //create the new connection using the parameters of connStr
            try
            {
                connection.Open();                            //open the connection
                var cmd = new MySqlCommand(sql, connection);  //create an executable command
                var reader = cmd.ExecuteReader();             //execute the command

                while (reader.Read())
                {
                    string currentCategory = reader.GetString(0);
                    currentCategory = currentCategory.Substring(0, 1).ToUpper() + currentCategory.Substring(1); //Make the first character of the category in upper case.
                    categories.AddLast(currentCategory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            connection.Close();
            return categories;
        }


        public LinkedList<string> GetBudgetReportCategories()
        {
            LinkedList<string> categories = new LinkedList<string>();

            string connStr = new MySQLConnection().connection;

            MySqlConnection connection = new MySqlConnection(connStr);

            string sql = "SELECT type_name FROM expense_types ORDER BY type_name; ";

            connection = new MySqlConnection(connStr);    //create the new connection using the parameters of connStr
            try
            {
                connection.Open();                            //open the connection
                var cmd = new MySqlCommand(sql, connection);  //create an executable command
                var reader = cmd.ExecuteReader();             //execute the command

                while (reader.Read())
                {
                    string currentCategory = reader.GetString(0);
                    currentCategory = currentCategory.Substring(0, 1).ToUpper() + currentCategory.Substring(1); //Make the first character of the category in upper case.
                    categories.AddLast(currentCategory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            connection.Close();
            return categories;
        }
    }
}