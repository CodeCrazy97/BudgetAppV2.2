/*
 * Budget App, Version 2.
 * Created: 12/15/2018.
 * By Ethan Vaughan
 * 
 * 02/14/2019 - Added functionality to allow user to place a transaction after viewing report form.
 * 05/13/2019 - Simplified app to only report expenditures. Use database once a year to record gross wages.
 * 05/14/2019 - Added README file.
 * 07/01/2019 - Maximized the start screen. Disabled the process that deletes aria log file.
 * 01/03/2020 - Displays "NA" for total amount spent when invalid dates are selected.
 * 01/04/2020 - Starts MySQL correctly on my new computer. 
 * 01/05/2020 - Resize data grid view on report form based on number of expense types returned.
 * 01/12/2020 - Show charity balance on Start Menu. Also, increase/decrease balance accoring to contributions and earnings.
 * 01/25/2020 - Display current month's transactions on start screen; hide charity balance message except when selected transactions as charity/other earnings.
 * 02/01/2020 - Using charity table; display expenses descending.
 * 02/01/2020 - Hide current month's expenses on start menu load (will unhide if anything exists for current month).
 * 02/19/2020 - System can now calculate math expressions for the amount.
 */

using System.Data;
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
                // The below line of code may not be needed, as some users do not experience database connectivity issues due to the aria log file.
                //var deleteAriaLogFile = Process.Start("C:\\Users\\Ethan\\Documents\\Projects\\Batch\\remove_aria_log_file.bat");

                try  //Try to start mysqld.exe
                {
                    var mysqld = Process.Start("C:\\xampp\\mysql_start2.bat");
                    Thread.Sleep(2000);  //Give mysqld.exe time to start.
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
            dataGridView1.Visible = false;
            label1.Visible = false;
            this.Text = "Budget App, Version 2.3";
            WindowState = FormWindowState.Maximized;
            categories = new MySQLConnection().GetCategories();

            categoryComboBox.Items.Add("Charity");
            //Fill combo box with the categories.
            for (int i = 0; i < categories.Count; i++)
            {
                categoryComboBox.Items.Add(categories.ElementAt(i));
            }
            // add the other earnings and charity categories (these are not in the database
            categoryComboBox.Items.Add("Other Earnings");
            
            //Set the width of the grid view columns.
            dataGridView1.Columns[0].Width = 90;
            dataGridView1.Columns[1].Width = 450;
            dataGridView1.Columns[1].Width = dataGridView1.Width - dataGridView1.Columns[0].Width - dataGridView1.Columns[0].Width;

            //Show the current month's transactions.
            DisplayMonthTransactions();

            //Select the first item in the combobox.
            categoryComboBox.SelectedIndex = 0;

            //Set the cursor to blinking in the text box.
            transactionDescriptionTextBox.Select();
            
            // don't want to display charity balance when program starts (privacy concern)
            if (categoryComboBox.Items.Count > 1 && categoryComboBox.SelectedItem.Equals("Charity"))
            {
                categoryComboBox.SelectedIndex = 1;
            }
        }

        private void DisplayMonthTransactions()
        {
            //Remove the previous showing of this month's transactions (necessary because this method could be called after submitting a new transaction, which would then need to be added to the transactions list)
            dataGridView1.Rows.Clear();

            LinkedList<String[]> monthsTransactions = new MySQLConnection().GetCurrentMonthsTransactions();

            if(monthsTransactions.Count == 0)  // No transactions for the current month.
            {
                dataGridView1.Visible = false;
                label1.Visible = false;
            }
            else
            {
                dataGridView1.Visible = true;
                label1.Visible = true;
            }

            dataGridView1.Height = 28;
            dataGridView1.Height += monthsTransactions.Count * 21;
            for (int i = 0; i < monthsTransactions.Count; i++)  //Even though there should be five transactions in the linked list, there might not be if the database has been swiped of data.
            {
                dataGridView1.Rows.Add(monthsTransactions.ElementAt(i)[0], monthsTransactions.ElementAt(i)[1], monthsTransactions.ElementAt(i)[2]);
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

        private double getAmount()
        {
            // Calculate math expressions for the amount given. For example: users can enter 12.34+.87. The amount in this case would equal $13.21.
            DataTable dt = new DataTable();
            double amount = Convert.ToDouble(dt.Compute(transactionAmountTextBox.Text, ""));
            return amount;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (submitButton.Text.Equals("Submit"))
            {
                try
                {
                    //Make sure the amount entered is valid.
                    double amount = getAmount();

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

                //Step 3: Create the SQL statement that deletes the notice.
                string date = transactionDateTimePicker.Value.Year + "-" + transactionDateTimePicker.Value.Month + "-" + transactionDateTimePicker.Value.Day;
                string description = new MySQLConnection().FixStringForMySQL(transactionDescriptionTextBox.Text);

                // Calculate math expressions for the amount given. For example: users can enter 12.34+.87. The amount in this case would equal $13.21.
                double amount = getAmount();
                Console.WriteLine(amount);
                if (amount != 0.0)
                {
                    //Figure out which category this transaction fits in.
                    bool[] vals = new bool[categories.Count];
                    amount = Math.Abs(amount);  // make a positive number. The old version insisted that expenses be negative; however, under the new design, it is implied that anything in the expense table is negative

                    //Build the INSERT string. Place each possible category into it.
                    string sql = "";
                    if (String.Equals(categoryComboBox.SelectedItem.ToString(), "Other Earnings"))  // Type = Other Earnings
                    {
                        sql = "INSERT INTO other_earnings (earning_date, description, amount) VALUES ('" + date + "', '" + description + "', " + amount + ");";

                        // Check if user wants to apply 10% (rounded up) towards charity balance.   
                        if (checkBox.Checked)
                        {
                            amount = Math.Ceiling(amount * 0.1);
                            new MySQLConnection().ModifyCharityBalance(date, description + " (10+% applied to charity)", amount);
                        }

                        executeSql(sql);
                    }
                    else if (String.Equals(categoryComboBox.SelectedItem.ToString(), "Charity"))  // Type = Charity
                    {
                        if (!checkBox.Checked)  // This is a decrease to the charity balance.
                        {
                            amount = -amount;  //make negative
                        }
                        // Will need to decrease charity balance, since used some of the charity.
                        new MySQLConnection().ModifyCharityBalance(date, description, amount); // turn amount into a negative (decrease in charity balance)
                    }
                    else  // Some other expense type
                    {
                        sql = "INSERT INTO expenses (trans_date, description, amount, expense_type) VALUES ('" + date + "', '" + description + "', " + amount + ", '" + categoryComboBox.SelectedItem.ToString().ToLower() + "');";

                        executeSql(sql);
                    }

                    //Reset items.
                    Clear();

                    //Set cursor to blinking in the description text box.
                    transactionDescriptionTextBox.Select();

                    //Add this item to the last five transactions.                    
                    DisplayMonthTransactions();

                    if (categoryComboBox.SelectedItem.Equals("Charity") || categoryComboBox.SelectedItem.Equals("Other Earnings"))
                    {
                        displayCharityBalanceMessage();
                    }
                }
                else
                {
                    MessageBox.Show("Amount must be nonzero.");
                }                
            }
        }

        private void executeSql(string sql)
        {
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
            checkBox.Checked = false;
            categoryComboBox.SelectedIndex = 0;

            // don't want to display charity balance when program starts (privacy concern)
            if (categoryComboBox.Items.Count > 1 && categoryComboBox.SelectedItem.Equals("Charity"))
            {
                categoryComboBox.SelectedIndex = 1;
            }

            //Set the cursor to blinking in the text box.
            transactionDescriptionTextBox.Select();
        }

        private void budgetReportButton_Click(object sender, EventArgs e)
        {
            ReportForm reportForm = new ReportForm();
            reportForm.FormClosed += new FormClosedEventHandler(startMenuFormClosed);
            reportForm.categories = categories;  // the report will show spending based on the different categories of spending
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

        // Show what the charity balance is.
        private void displayCharityBalanceMessage()
        {
            try
            {
                // Show the charity budget.
                double charityBalance = new MySQLConnection().GetCharityBalance();
                charityBalanceLabel.Text = "Current charity balance: $" + charityBalance;
            }
            catch (Exception e2)
            {
                Console.WriteLine(e2);
            }
        }

        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryComboBox.SelectedItem.ToString().Equals("Other Earnings")|| categoryComboBox.SelectedItem.ToString().Equals("Charity"))
            {
                checkBox.Visible = true;
                checkBox.Checked = false;
                charityBalanceLabel.Visible = true;
                displayCharityBalanceMessage();

                submitButton.SetBounds(submitButton.Location.X, 425, submitButton.Width, submitButton.Height);
                clearButton.SetBounds(clearButton.Location.X, 425, clearButton.Width, clearButton.Height);
                if (categoryComboBox.SelectedItem.ToString().Equals("Other Earnings"))
                {
                    checkBox.Text = "Apply 10+% towards charity balance?";
                }
                else
                {
                    checkBox.Text = "Select if this was a charity balance INCREASE";
                }
            }
            else
            {
                checkBox.Visible = false;
                charityBalanceLabel.Visible = false;
                submitButton.SetBounds(submitButton.Location.X, 400, submitButton.Width, submitButton.Height);
                clearButton.SetBounds(clearButton.Location.X, 400, clearButton.Width, clearButton.Height);
            }
        }
    }
}
