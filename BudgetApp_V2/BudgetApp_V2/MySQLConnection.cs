using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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

        public LinkedList<String[]> GetLastFiveTransactions()
        {
            LinkedList<String[]> transactions = new LinkedList<String[]>();

            
            string connStr = new MySQLConnection().connection;

            MySqlConnection connection = new MySqlConnection(connStr);

            string sql = "SELECT trans_date, description, amount FROM expenses ORDER BY trans_date DESC LIMIT 5; ";

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

        public void ModifyCharityBalance(double amount)
        {
            //Place the transaction in the database.
            string connStr = new MySQLConnection().connection;

            string sql = "";
            //Build the INSERT string.
            sql = "UPDATE charity_balance SET amount = amount + " + amount + ";";
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

            string sql = "SELECT amount FROM charity_balance; ";

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
            throw new Exception("Could not fetch amount from charity_balance.");
        }

        public LinkedList<string> GetCategories()
        {
            LinkedList<string> categories = new LinkedList<string>();

            string connStr = new MySQLConnection().connection;

            MySqlConnection connection = new MySqlConnection(connStr);

            string sql = "SELECT typeName FROM expensetypes ORDER BY typeName; ";

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
