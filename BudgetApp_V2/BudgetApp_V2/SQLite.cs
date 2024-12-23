﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SQLitePCL;
using Microsoft.Data.Sqlite;
using System.IO;

namespace BudgetApp_V2
{
    public class SQLite
    {
        public string connection = null;
        public SqliteConnection connection_object = null;

        public SQLite()
        {
            Batteries.Init();
            this.connection = "Data Source=" + getBaseDirectory() + "\\budget.db";
            this.connection_object = new SqliteConnection(connection);
            this.connection_object.Open();
        }

        /**
         * Returns the base directory of the project.
         */
        public string getBaseDirectory()
        {
            string workingDirectory = Environment.CurrentDirectory;
            return Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
        }

        public LinkedList<String[]> GetTransactionsBetweenDates(DateTime startDate, DateTime endDate, string category)
        {
            LinkedList<String[]> transactions = new LinkedList<String[]>();

            using (var selectCommand = this.connection_object.CreateCommand())
            {

                category = category.ToLower();
                if (category == "all")
                {
                    category = "";
                }
                string extra_where = "";
                if (category.Length > 0)
                {
                    extra_where = "AND expense_type = $expense_type ";
                }
                selectCommand.CommandText = @"SELECT trans_date, description, amount, trans_id, expense_type 
                    FROM expenses 
                    WHERE trans_date BETWEEN $start_date AND $end_date " + extra_where +
                    "ORDER BY trans_date DESC; ";
                selectCommand.Parameters.AddWithValue("$start_date", startDate.ToString("yyyy-MM-dd"));
                selectCommand.Parameters.AddWithValue("$end_date", endDate.ToString("yyyy-MM-dd"));
                if (category.Length > 0)
                {
                    selectCommand.Parameters.AddWithValue("$expense_type", category);
                }

                using (var reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String[] currentTransaction = new String[5];
                        DateTime dt = reader.GetDateTime(0);  //Get the date.
                        string dtString = dt.Month + "/" + dt.Day + "/" + dt.Year;
                        currentTransaction[0] = dtString;
                        currentTransaction[1] = reader.GetString(1);  //Get the description.
                        currentTransaction[2] = reader.GetString(2);  //Get the amount.
                        currentTransaction[3] = reader.GetString(3);   //Get the trans_id.
                        currentTransaction[4] = reader.GetString(4);   //Get the expense_type.
                        transactions.AddLast(currentTransaction);
                    }
                    reader.Close();
                }
            }

            return transactions;
        }

        public bool UpdateEntry(String trans_date, String description, double amount, int trans_id, String expense_type)
        {
            String mySqlFixedDate = "";
            DateTime newDate = DateTime.Now; // the now value won't actually be used
            try
            {
                if (trans_date[1] == '/')  // Check m/dd/yyyy or m/d/yyyy
                {
                    if (trans_date[4] == '/') // m/dd/yyyy
                    {
                        newDate = DateTime.ParseExact(trans_date, "M/dd/yyyy", null);
                        mySqlFixedDate = newDate.ToString("yyyy-MM-dd");
                    }
                    else if (trans_date[3] == '/') // m/d/yyyy
                    {
                        newDate = DateTime.ParseExact(trans_date, "M/d/yyyy", null);
                        mySqlFixedDate = newDate.ToString("yyyy-MM-dd");
                    }
                }
                else if (trans_date[2] == '/') // Check mm/dd/yyyy or mm/d/yyyy
                {
                    if (trans_date[5] == '/') // mm/dd/yyyy
                    {
                        newDate = DateTime.ParseExact(trans_date, "MM/dd/yyyy", null);
                        mySqlFixedDate = newDate.ToString("yyyy-MM-dd");
                    }
                    else if (trans_date[4] == '/') // mm/d/yyyy
                    {
                        newDate = DateTime.ParseExact(trans_date, "MM/d/yyyy", null);
                        mySqlFixedDate = newDate.ToString("yyyy-MM-dd");
                    }
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show("The date " + trans_date + " could not be formatted correctly for the database: " + fe.Message);
            }

            if (mySqlFixedDate.Equals(""))
            {
                MessageBox.Show("The date " + trans_date + " could not be converted to a MySQL date.");
            }

            Expense expense = new Expense(trans_id);
            expense.Trans_date = newDate;
            expense.Amount = amount;
            expense.Descrption = description;
            expense.Expense_type = expense_type;
            try
            {
                return expense.save();
            } catch (Exception ex)
            {
                MessageBox.Show("Error trying to update transaction(s): " + ex.Message);
                return false;
            }
        }

        public double GetTitheBalance()
        {
            using (var selectCommand = this.connection_object.CreateCommand())
            {
                selectCommand.CommandText = @"SELECT SUM(amount) FROM expenses WHERE expense_type = 'tithe'; ";

                double balance = 0;
                using (var reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        balance = reader.GetDouble(0);
                    }
                    reader.Close();
                    return balance;
                }
            }
            throw new Exception("Could not fetch tithe amount.");
        }

        public LinkedList<string> GetCategories()
        {
            LinkedList<string> categories = new LinkedList<string>();

            using (var selectCommand = this.connection_object.CreateCommand())
            {
                selectCommand.CommandText = @"SELECT type_name FROM expense_types ORDER BY type_name; ";

                using (var reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string currentCategory = reader.GetString(0);
                        currentCategory = currentCategory.Substring(0, 1).ToUpper() + currentCategory.Substring(1); //Make the first character of the category in upper case.
                        categories.AddLast(currentCategory);
                    }
                    reader.Close();
                }
            }
            return categories;
        }

        public bool AddToOtherEarnings(String date, String description, double amount)
        {
            int count;

            using (var selectCommand = this.connection_object.CreateCommand())
            {
                selectCommand.CommandText = @"INSERT INTO other_earnings (earning_date, description, amount) VALUES (@date, @description, @amount);";
                selectCommand.Parameters.AddWithValue("@date", date);
                selectCommand.Parameters.AddWithValue("@description", description);
                selectCommand.Parameters.AddWithValue("@amount", amount);

                try
                {
                    count = selectCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception adding the transaction: " + ex.ToString());
                    return false;
                }

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
        public bool CheckIfTransactionExists(SQLite connection, String trans_date, String description, double amount, String expense_type)
        {
            int count = 0;
            using (var selectCommand = this.connection_object.CreateCommand())
            {
                if (expense_type == "other earnings")
                {
                    selectCommand.CommandText = @"SELECT COUNT(*) AS `count` FROM other_earnings WHERE trans_date = @trans_date AND description = @description AND amount = @amount; ";
                }
                else
                {
                    selectCommand.CommandText = @"SELECT COUNT(*) AS `count` FROM expenses WHERE trans_date = @trans_date AND description = @description AND amount = @amount; ";
                }
                selectCommand.Parameters.AddWithValue("@trans_date", trans_date);
                selectCommand.Parameters.AddWithValue("@description", description);
                selectCommand.Parameters.AddWithValue("@amount", amount);

                using (var reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                    reader.Close();
                }

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
