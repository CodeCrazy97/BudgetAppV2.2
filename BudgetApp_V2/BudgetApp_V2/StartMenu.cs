/*
 * Budget App, Version 2.
 * Created: 12/15/2018.
 * By Ethan Vaughan
 * 
 * 02/14/2019 - Added functionality to allow user to place a transaction after viewing report form.
 * 
 */

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace BudgetApp_V2
{
    public partial class StartMenu : Form
    {
        public string connStr = new MySQLConnection().connection;
        LinkedList<string> categories = new LinkedList<string>();   //Holds the categories of spending.


        public StartMenu()
        {
            //Start mysqld
            if (!isMysqldRunning())
            {
                var deleteAriaLogFile = Process.Start("C:\\Users\\Ethan_2\\Documents\\Projects\\Batch\\remove_aria_log_file.bat");
                
                try  //Try to start mysqld.exe
                {
                    var mysqld = Process.Start("C:\\xampp\\mysql\\bin\\mysqld.exe");
                    Thread.Sleep(6500);  //Give mysqld.exe time to start.
                }
                catch (System.ComponentModel.Win32Exception ex3)  //Unable to start the process (could be that the executable is located in a different folder path).
                {
                    Console.WriteLine("Error message: " + ex3);
                    MessageBox.Show("Unable to start the process mysqld.exe.");
                    Application.Exit();
                }
            }

            InitializeComponent();

        }

        private void StartMenu_Load(object sender, EventArgs e)
        {
            this.Text = "Budget App, Version 2.2";

            categories = new MySQLConnection().GetCategories();

            //Fill combo box with the categories.
            for (int i = 0; i < categories.Count; i++)
            {
                categoryComboBox.Items.Add(categories.ElementAt(i));
            }
            // add the other earnings category
            categoryComboBox.Items.Add("Other Earnings");

            //Set the width of the grid view columns.
            dataGridView1.Columns[0].Width = 90;  
            dataGridView1.Columns[1].Width = 450;
            dataGridView1.Columns[1].Width = dataGridView1.Width - dataGridView1.Columns[0].Width - dataGridView1.Columns[0].Width;

            //Show the last five transactions that were recorded.
            SetLastFiveTransactions();

            //Select the first item in the combobox.
            categoryComboBox.SelectedIndex = 0;

            //Set the cursor to blinking in the text box.
            transactionDescriptionTextBox.Select();
        }

        private void SetLastFiveTransactions()
        {
            //Remove the previous showing of the last five transactions (necessary because this method could be called after submitting a new transaction, which would then need to be added to the last five transactions list)
            dataGridView1.Rows.Clear();

            LinkedList<String[]> lastFiveTransactions = new MySQLConnection().GetLastFiveTransactions();

            for (int i = 0; i < lastFiveTransactions.Count; i++)  //Even though there should be five transactions in the linked list, there might not be if the database has been swiped of data.
            {
                dataGridView1.Rows.Add(lastFiveTransactions.ElementAt(i)[0], lastFiveTransactions.ElementAt(i)[1], lastFiveTransactions.ElementAt(i)[2]);
            }
        }

        void startMenuFormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (submitButton.Text.Equals("Submit"))
            {
                try
                {
                    //Make sure the amount entered is valid.
                    double amt = Convert.ToDouble(new MySQLConnection().FixStringForMySQL(transactionAmountTextBox.Text));

                    confirmLabel.Visible = true;
                    submitButton.Text = "Confirm";
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("Error: " + fe);
                    MessageBox.Show("Invalid entry for amount.");
                    Clear();
                }
            }
            else
            {
                //Place the transaction in the database.
                string connStr = new MySQLConnection().connection;

                //Step 2: Create a connection.       
                MySqlConnection connection = new MySqlConnection(connStr);

                //Step 3: Create the SQL statement that deletes the notice.
                string date = transactionDateTimePicker.Value.Year + "-" + transactionDateTimePicker.Value.Month + "-" + transactionDateTimePicker.Value.Day;
                string description = new MySQLConnection().FixStringForMySQL(transactionDescriptionTextBox.Text);

                double amt = Convert.ToDouble(new MySQLConnection().FixStringForMySQL(transactionAmountTextBox.Text));
                if (amt != 0.0)
                {
                    //Figure out which category this transaction fits in.
                    bool[] vals = new bool[categories.Count];
                    amt = Math.Abs(amt);  // make a positive number. The old version insisted that expenses be negative; however, under the new design, it is implied that anything in the expense table is negative

                    //Build the INSERT string. Place each possible category into it.
                    Console.WriteLine("selected item = " + categoryComboBox.SelectedItem);
                    string sql = "";
                    if (String.Equals(categoryComboBox.SelectedItem.ToString(), "Other Earnings"))
                    {
                        sql = "INSERT INTO otherearnings (earning_date, description, amount) VALUES ('" + date + "', '" + description + "', " + amt + ");";
                    }
                    else
                    {
                        sql = "INSERT INTO expenses (trans_date, description, amount, expensetype) VALUES ('" + date + "', '" + description + "', " + amt + ", '" + categoryComboBox.SelectedItem.ToString().ToLower() + "');";
                    }
                    Console.WriteLine("sql: " + sql);
                    connection = new MySqlConnection(connStr);    //create the new connection using the parameters of connStr
                    try
                    {
                        connection.Open();                            //open the connection
                        var cmd = new MySqlCommand(sql, connection);  //create an executable command
                        var reader = cmd.ExecuteNonQuery();             //execute the command
                        Console.WriteLine("working!!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("Amount must be nonzero.");
                }
                //Reset items.
                Clear();

                //Set cursor to blinking in the description text box.
                transactionDescriptionTextBox.Select();

                //Add this item to the last five transactions.
                SetLastFiveTransactions();
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            transactionDateTimePicker.Value = DateTime.Today;
            transactionDescriptionTextBox.Text = "";
            transactionAmountTextBox.Text = "";
            categoryComboBox.SelectedIndex = 0;
            confirmLabel.Visible = false;
            submitButton.Text = "Submit";
        }

        private void budgetReportButton_Click(object sender, EventArgs e)
        {
            ReportForm reportForm = new ReportForm();
            reportForm.FormClosed += new FormClosedEventHandler(startMenuFormClosed);
            reportForm.categories = categories;
            reportForm.Text = "Report";
            reportForm.Show();
            categories = new MySQLConnection().GetCategories();  // Reset the categories. This gets reset to zero after viewing the report form.
            transactionDescriptionTextBox.Select();  // Set cursor to blinking in the description text box.
            this.Hide();
        }


        public bool isMysqldRunning()
        {
            //Loop over all running processes.
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ToString().Equals("System.Diagnostics.Process (mysqld)"))
                {
                    return true;
                }
            }
            //Mysqld.exe is not running; return a false
            return false;
        }

    }
}