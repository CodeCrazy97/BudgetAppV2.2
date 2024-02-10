using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp_V2
{
    class Expense
    {
        private string expense_type;
        private string description;
        private double amount;
        private DateTime trans_date;
        private int trans_id;
        private MySqlConnection connection = null;

        public string Expense_type { get => expense_type; set => expense_type = value; }
        public string Descrption { get => description; set => description = value; }
        public double Amount { get => amount; set => amount = value; }
        public DateTime Trans_date { get => trans_date; set => trans_date = value; }
        public int Trans_id { get => trans_id; set => trans_id = value; }

        public bool save()
        {
            openConnection();
            try
            {
                openConnection();
                var cmd = new MySqlCommand("INSERT INTO expenses (trans_date, description, amount, expense_type) VALUES ('" + trans_date.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + description + "', " + amount + ", '" + expense_type + "');", connection);
                var reader = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        public bool update()
        {
            // TODO
            return true;
        }

        public bool delete()
        {
            // TODO
            return true;
        }
        
        private void openConnection()
        {
            connection = new MySqlConnection(new MySQLConnection().connection);
            connection.Open();
        }

        public Expense(string description, string expense_type, double amount, DateTime trans_date)
        {
            Descrption = description;
            Expense_type = expense_type.ToLower();
            Amount = amount;
            Trans_date = trans_date;
        }

        public Expense(int id)
        {
            try
            {
                openConnection();
                var cmd = new MySqlCommand("SELECT * FROM expenses WHERE trans_id = @trans_id", connection);  //create an executable command
                cmd.Parameters.AddWithValue("@trans_id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Expense_type = reader.GetString("expense_type");
                    Descrption = reader.GetString("description");
                    Trans_date= reader.GetDateTime("trans_date");
                    Amount = reader.GetDouble("amount");
                    Trans_id = reader.GetInt16("trans_id");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            } 
            finally
            {
                connection.Close();
            }
        }
    }
}
