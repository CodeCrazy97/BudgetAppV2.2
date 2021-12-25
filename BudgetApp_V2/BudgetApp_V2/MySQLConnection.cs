using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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

            string sql = "SELECT trans_date, description, amount, trans_id FROM expenses WHERE YEAR(trans_date) = YEAR(NOW()) AND MONTH(trans_date) = MONTH(NOW()) ORDER BY trans_date DESC; ";

            connection = new MySqlConnection(connStr);    //create the new connection using the parameters of connStr
            try
            {
                connection.Open();                            //open the connection
                var cmd = new MySqlCommand(sql, connection);  //create an executable command
                var reader = cmd.ExecuteReader();             //execute the command

                while (reader.Read())
                {
                    String[] currentTransaction = new String[4];
                    DateTime dt = reader.GetDateTime(0);  //Get the date.
                    string dtString = dt.Month + "/" + dt.Day + "/" + dt.Year;
                    currentTransaction[0] = dtString;
                    currentTransaction[1] = reader.GetString(1);  //Get the description.
                    currentTransaction[2] = reader.GetString(2);  //Get the amount.
                    currentTransaction[3] = reader.GetString(3);   //Get the trans_id.
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

            MySqlConnection connection = new MySqlConnection(connStr);    //create the new connection using the parameters of connStr

            string sql = "";
            //Build the INSERT string.
            sql = "INSERT INTO charity (trans_date, description, amount) VALUES ('" + transDate + "', '" + description + "', " + amount + ");";

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


        public bool UpdateEntry(String trans_date, String description, double amount, int trans_id)
        {
            string connStr = new MySQLConnection().connection;

            MySqlConnection connection = new MySqlConnection(connStr);

            String mySqlFixedDate = "";
            try
            {
                if (trans_date[1] == '/')  // Check m/dd/yyyy or m/d/yyyy
                {
                    if (trans_date[4] == '/') // m/dd/yyyy
                    {
                        DateTime newDate = DateTime.ParseExact(trans_date, "M/dd/yyyy", null);
                        mySqlFixedDate = newDate.ToString("yyyy-MM-dd");
                    }
                    else if (trans_date[3] == '/') // m/d/yyyy
                    {
                        DateTime newDate = DateTime.ParseExact(trans_date, "M/d/yyyy", null);
                        mySqlFixedDate = newDate.ToString("yyyy-MM-dd");
                    }
                }
                else if (trans_date[2] == '/') // Check mm/dd/yyyy or mm/d/yyyy
                {
                    if (trans_date[5] == '/') // mm/dd/yyyy
                    {
                        DateTime newDate = DateTime.ParseExact(trans_date, "MM/dd/yyyy", null);
                        mySqlFixedDate = newDate.ToString("yyyy-MM-dd");
                    }
                    else if (trans_date[4] == '/') // mm/d/yyyy
                    {
                        DateTime newDate = DateTime.ParseExact(trans_date, "MM/d/yyyy", null);
                        mySqlFixedDate = newDate.ToString("yyyy-MM-dd");
                    }
                }
            } catch (FormatException fe)
            {
                MessageBox.Show("The date " + trans_date + " could not be formatted correctly for the database: " + fe.Message);
            }

            if (mySqlFixedDate.Equals(""))
            {
                MessageBox.Show("The date " + trans_date + " could not be converted to a MySQL date.");
            }

            string sql = "UPDATE expenses SET trans_date = @trans_date, amount = @amount, description = @description WHERE trans_id = @trans_id;";
            connection = new MySqlConnection(connStr);    //create the new connection using the parameters of connStr

            bool success = true;
            try
            {
                connection.Open();                            //open the connection
                var cmd = new MySqlCommand(sql, connection);  //create an executable command
                cmd.Parameters.AddWithValue("@trans_date", mySqlFixedDate);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@trans_id", trans_id);
                var reader = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                success = false;
                MessageBox.Show("Error trying to update transaction(s): " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return success;
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

        public bool DeleteTransaction(int trans_id)
        {

            int count = 0;
            string connStr = new MySQLConnection().connection;

            MySqlConnection connection = new MySqlConnection(connStr);

            string sql = "DELETE FROM expenses WHERE trans_id = @trans_id; ";

            connection = new MySqlConnection(connStr);    //create the new connection using the parameters of connStr
            try
            {
                connection.Open();                            //open the connection
                var cmd = new MySqlCommand(sql, connection);  //create an executable command
                cmd.Parameters.AddWithValue("@trans_id", trans_id);
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception trying to delete transaction " + trans_id + " : " + ex.ToString());
                return false;
            }
            connection.Close();
            if (count > 0)
            {
                return true;
            } else
            {
                return false;
            }
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