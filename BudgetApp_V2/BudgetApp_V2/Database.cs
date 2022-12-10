using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace BudgetApp_V2
{
    public class Database
    {
        public string connectionStr = "server=localhost;user=root;database=budget;port=3306;password=;";
        public SQLiteConnection sqliteconnection;

        public Database()
        {
            if ( sqliteconnection == null)
            {
                SQLiteConnection sqlite_conn;
                this.CreateConnection();
                this.CheckTables();
            }
        }

        public void CreateCharityTable()
        {
            SQLiteCommand sqlite_cmd;
            string createsql = "CREATE TABLE IF NOT EXISTS `charity` (`trans_date` date DEFAULT NULL, `description` text DEFAULT NULL, `amount` double DEFAULT NULL, `trans_id` INTEGER PRIMARY KEY); ";
            this.sqliteconnection.Open();
            sqlite_cmd = this.sqliteconnection.CreateCommand();
            sqlite_cmd.CommandText = createsql;
            sqlite_cmd.ExecuteNonQuery();
            this.sqliteconnection.Close();
        }

        public void CheckTables()
        {
            this.CheckAndCreateTable("expense_types");
            this.CheckAndCreateTable("expenses");
            this.CheckAndCreateTable("charity");
            this.CheckAndCreateTable("jobs");
            this.CheckAndCreateTable("tax_types");
            this.CheckAndCreateTable("other_earnings");

        }

        public void CheckAndCreateTable(string table)
        {
            try
            {
                SQLiteDataReader sqlite_datareader;
                SQLiteCommand sqlite_cmd;
                this.sqliteconnection.Open();
                sqlite_cmd = this.sqliteconnection.CreateCommand();
                sqlite_cmd.CommandText = "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='" + table + "'; ";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    int cnt = sqlite_datareader.GetInt16(0);
                    if (cnt == 0)
                    {
                        sqlite_datareader.Close();
                        this.sqliteconnection.Close();
                        switch (table)
                        {
                            case "expense_types":
                                this.CreateExpenseTypesTable();
                                break;
                            case "expenses":
                                this.CreateExpensesTable();
                                break;
                            case "charity":
                                this.CreateCharityTable();
                                break;
                            case "jobs":
                                this.CreateJobsTable();
                                break;
                            case "tax_types":
                                this.CreateTaxTypesTable();
                                break;
                            case "other_earnings":
                                this.CreateOtherEarningsTable();
                                break;
                            default:
                                throw new Exception("Table type not implemented.");
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                this.sqliteconnection.Close();
            }
        }

        public void CreateExpensesTable()
        {
            SQLiteCommand sqlite_cmd;
            string createsql = "CREATE TABLE IF NOT EXISTS `expenses` (  `trans_date` date NOT NULL,  `description` text NOT NULL,  `amount` double NOT NULL,  `trans_id` INTEGER PRIMARY KEY,  `expense_type` varchar(25) DEFAULT NULL, FOREIGN KEY(`expense_type`) REFERENCES `expense_types` (`type_name`))";
            this.sqliteconnection.Open();
            SQLiteCommand command = new SQLiteCommand(createsql, this.sqliteconnection);
            command.ExecuteNonQuery();
            this.sqliteconnection.Close();
        }

        public void CreateExpenseTypesTable()
        {
            try
            {

                string createsql = "CREATE TABLE IF NOT EXISTS `expense_types` (  `type_name` varchar(25) NOT NULL,  PRIMARY KEY(`type_name`)); ";
                this.sqliteconnection.Open();
                SQLiteCommand command = new SQLiteCommand(createsql, this.sqliteconnection);
                try
                {
                    int rows = command.ExecuteNonQuery();
                }
                catch (Exception ex2)
                {
                    Console.WriteLine(ex2.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.sqliteconnection.Close();
            }
        }

        public void CreateJobsTable()
        {
            SQLiteCommand sqlite_cmd;
            string createsql = "CREATE TABLE IF NOT EXISTS `jobs` (  `name` varchar(20) NOT NULL,  PRIMARY KEY(`name`))";
            this.sqliteconnection.Open();
            sqlite_cmd = this.sqliteconnection.CreateCommand();
            sqlite_cmd.CommandText = createsql;
            sqlite_cmd.ExecuteNonQuery();
            this.sqliteconnection.Close();
        }

        public void CreateOtherEarningsTable()
        {
            SQLiteCommand sqlite_cmd;
            string createsql = "CREATE TABLE IF NOT EXISTS `other_earnings` (  `earning_date` date NOT NULL,  `description` text NOT NULL,  `amount` double NOT NULL,  `earning_id` INTEGER PRIMARY KEY); ";
            this.sqliteconnection.Open();
            sqlite_cmd = this.sqliteconnection.CreateCommand();
            sqlite_cmd.CommandText = createsql;
            sqlite_cmd.ExecuteNonQuery();
            this.sqliteconnection.Close();
        }


        public void CreateTaxTypesTable()
        {
            SQLiteCommand sqlite_cmd;
            string createsql = "CREATE TABLE IF NOT EXISTS `tax_types` (  `name` varchar(20) NOT NULL,  PRIMARY KEY(`name`))";
            this.sqliteconnection.Open();
            sqlite_cmd = this.sqliteconnection.CreateCommand();
            sqlite_cmd.CommandText = createsql;
            sqlite_cmd.ExecuteNonQuery();
            this.sqliteconnection.Close();
        }


        public void CreateConnection()
        {
            string dbfilepath = this.GetSQLiteDBFilePath();
            if (!File.Exists(dbfilepath))
            {
                SQLiteConnection.CreateFile(dbfilepath);
            }
            this.sqliteconnection = new SQLiteConnection("Data Source=" + dbfilepath + ";Version=3;");
        }

        public string GetSQLiteDBFilePath()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            return projectDirectory + "\\budget.sqlite";
        }

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

            try
            {
                SQLiteDataReader sqlite_datareader;
                SQLiteCommand sqlite_cmd;
                this.sqliteconnection.Open();
                sqlite_cmd = this.sqliteconnection.CreateCommand();
                sqlite_cmd.CommandText = "SELECT trans_date, description, amount, trans_id, expense_type FROM expenses WHERE YEAR(trans_date) = YEAR(NOW()) AND MONTH(trans_date) = MONTH(NOW()) ORDER BY trans_date DESC; ";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    String[] currentTransaction = new String[5];
                    DateTime dt = sqlite_datareader.GetDateTime(0);  //Get the date.
                    string dtString = dt.Month + "/" + dt.Day + "/" + dt.Year;
                    currentTransaction[1] = sqlite_datareader.GetString(1);
                    currentTransaction[2] = sqlite_datareader.GetString(2);
                    currentTransaction[3] = sqlite_datareader.GetString(3);
                    currentTransaction[4] = sqlite_datareader.GetString(4);
                    transactions.AddLast(currentTransaction);
                }
            }
            catch (System.Data.SQLite.SQLiteException ex)
            {
                if (ex.Message.Contains("no such table: expenses"))
                {
                    this.sqliteconnection.Close();
                    this.CreateExpensesTable();
                    this.GetCurrentMonthsTransactions(); // call it again, hopefully the table is created by now!
                }
                Console.WriteLine(ex.ToString());
            }

            this.sqliteconnection.Close();
            return transactions;
        }


        public void ModifyCharityBalance(string transDate, string description, double amount)
        {
            SQLiteCommand sqlite_cmd;
            this.sqliteconnection.Open();
            sqlite_cmd = this.sqliteconnection.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO charity (trans_date, description, amount) VALUES ('" + transDate + "', '" + description + "', " + amount + ");";
            sqlite_cmd.ExecuteNonQuery();
            this.sqliteconnection.Close();
        }


        public bool UpdateEntry(String trans_date, String description, double amount, int trans_id, String expense_type)
        {

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

            bool success = true;
            try 
            {
                SQLiteCommand command;
                this.sqliteconnection.Open();
                command = this.sqliteconnection.CreateCommand();
                command.CommandText = "UDPATE expenses set trans_date = :trans_date, amount = :amount, description = :description, expense_type = :expense_type where trans_id = :trans_id";
                command.Parameters.Add("trans_date", DbType.String).Value = mySqlFixedDate;
                command.Parameters.Add("amount", DbType.String).Value = amount;
                command.Parameters.Add("description", DbType.String).Value = description;
                command.Parameters.Add("trans_id", DbType.String).Value = trans_id;
                command.Parameters.Add("expense_type", DbType.String).Value = expense_type;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                success = false;
                MessageBox.Show("Error trying to update transaction(s): " + ex.Message);
            }
            finally
            {
                this.sqliteconnection.Close();
            }

            return success;
        }


        public double GetCharityBalance()
        {
            try
            {
                SQLiteDataReader sqlite_datareader;
                SQLiteCommand sqlite_cmd;
                this.sqliteconnection.Open();
                sqlite_cmd = this.sqliteconnection.CreateCommand();
                sqlite_cmd.CommandText = "SELECT SUM(amount) FROM charity; ";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    return sqlite_datareader.GetDouble(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                this.sqliteconnection.Close();
            }
            throw new Exception("Could not fetch amount from charity.");
        }

        public LinkedList<string> GetCategories()
        {
            LinkedList<string> categories = new LinkedList<string>();

            try
            {
                SQLiteDataReader sqlite_datareader;
                SQLiteCommand sqlite_cmd;
                this.sqliteconnection.Open();
                sqlite_cmd = this.sqliteconnection.CreateCommand();
                sqlite_cmd.CommandText = "SELECT type_name FROM expense_types ORDER BY type_name; ";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    string currentCategory = sqlite_datareader.GetString(0);
                    currentCategory = currentCategory.Substring(0, 1).ToUpper() + currentCategory.Substring(1); //Make the first character of the category in upper case.
                    categories.AddLast(currentCategory);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            this.sqliteconnection.Close();
            return categories;
        }

        public bool DeleteTransaction(int trans_id)
        {

            int count = 0;
            try
            {

                SQLiteCommand command;
                this.sqliteconnection.Open();
                command = this.sqliteconnection.CreateCommand();
                command.CommandText = "DELETE FROM expenses WHERE trans_id = :trans_id; ";
                command.Parameters.Add("trans_id", DbType.String).Value = trans_id;
                count = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception trying to delete transaction " + trans_id + " : " + ex.ToString());
                return false;
            }
            this.sqliteconnection.Close();
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

            try
            {
                SQLiteDataReader sqlite_datareader;
                SQLiteCommand sqlite_cmd;
                this.sqliteconnection.Open();
                sqlite_cmd = this.sqliteconnection.CreateCommand();
                sqlite_cmd.CommandText = "SELECT type_name FROM expense_types ORDER BY type_name; ";

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    string currentCategory = sqlite_datareader.GetString(0);
                    currentCategory = currentCategory.Substring(0, 1).ToUpper() + currentCategory.Substring(1); //Make the first character of the category in upper case.
                    categories.AddLast(currentCategory);
                }

            }
            catch (System.Data.SQLite.SQLiteException ex)
            {
                if (ex.Message.Contains("no such table: expenses"))
                {
                    this.CreateExpensesTable();
                }
                Console.WriteLine(ex.ToString());
            }
            this.sqliteconnection.Close();
            return categories;
        }
    }
}