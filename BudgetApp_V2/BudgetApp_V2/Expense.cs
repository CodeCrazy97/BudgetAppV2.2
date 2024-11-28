using System;

namespace BudgetApp_V2
{
    class Expense
    {
        private string expense_type;
        private string description;
        private double amount;
        private DateTime trans_date;
        private int trans_id;
        //private MySqlConnection connection = null;
        private SQLite sqlite = null;

        public string Expense_type { get => expense_type; set => expense_type = value; }
        public string Descrption { get => description; set => description = value; }
        public double Amount { get => amount; set => amount = value; }
        public DateTime Trans_date { get => trans_date; set => trans_date = value; }
        public int Trans_id { get => trans_id; set => trans_id = value; }

        public bool save()
        {
            using (var selectCommand = this.sqlite.connection_object.CreateCommand())
            {
                if (trans_id == 0) // Creating a new transaction.
                {
                    selectCommand.CommandText = @"INSERT INTO expenses(trans_date, description, amount, expense_type) VALUES(@trans_date, @description, @amount, @expense_type); ";
                    selectCommand.Parameters.AddWithValue("@trans_date", trans_date.ToString("yyyy-MM-dd"));
                    selectCommand.Parameters.AddWithValue("@description", description);
                    selectCommand.Parameters.AddWithValue("@amount", amount);
                    selectCommand.Parameters.AddWithValue("@expense_type", expense_type);
                } else // Updating existing transaction.
                {
                    selectCommand.CommandText = @"UPDATE expenses SET trans_date = @trans_date, description = @description, amount = @amount, expense_type = @expense_type WHERE trans_id = @trans_id; ";
                    selectCommand.Parameters.AddWithValue("@trans_date", trans_date.ToString("yyyy-MM-dd"));
                    selectCommand.Parameters.AddWithValue("@description", description);
                    selectCommand.Parameters.AddWithValue("@amount", amount);
                    selectCommand.Parameters.AddWithValue("@expense_type", expense_type);
                    selectCommand.Parameters.AddWithValue("@trans_id", trans_id);
                }

                if (selectCommand.ExecuteNonQuery() > 0)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }

        public bool delete()
        {
            int count;

            using (var selectCommand = this.sqlite.connection_object.CreateCommand())
            {
                selectCommand.CommandText = @"DELETE FROM expenses WHERE trans_id = @trans_id; ";
                selectCommand.Parameters.AddWithValue("@trans_id", trans_id);

                try
                {
                    count = selectCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception trying to delete transaction " + trans_id + " : " + ex.ToString());
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
        
        private void openConnection()
        {
            this.sqlite = new SQLite();
        }

        public Expense(string description, string expense_type, double amount, DateTime trans_date)
        {
            this.openConnection();

            Descrption = description;
            Expense_type = expense_type.ToLower();
            Amount = amount;
            Trans_date = trans_date;
        }

        public Expense(int id)
        {
            this.openConnection();

            using (var selectCommand = this.sqlite.connection_object.CreateCommand())
            {
                selectCommand.CommandText = @"SELECT expense_type, description, trans_date, amount, trans_id FROM expenses WHERE trans_id = @trans_id";
                selectCommand.Parameters.AddWithValue("@trans_id", id);

                using (var reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Expense_type = reader.GetString(0);
                        Descrption = reader.GetString(1);
                        Trans_date = reader.GetDateTime(2);
                        Amount = reader.GetDouble(3);
                        Trans_id = reader.GetInt16(4);
                    }
                    reader.Close();
                }
            }
        }
    }
}
