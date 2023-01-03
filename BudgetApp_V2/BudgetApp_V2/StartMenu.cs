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
 * 03/02/2020 - Math.Ceiling for charity balance increase.
 * 03/28/2020 - Rounds to two decimal places for amount.
 * 04/05/2020 - Charity balance rounded to two decimal places.
 * 07/24/2020 - Calculate the amount in the amount text field if using mathematical expressions and display beside text field.
 * 07/25/2020 - Added tax and wage overview form.
 * 09/03/2020 - Fixed bug with Submit button - if invalid amount is entered, first Submit click was throwing an error. 
 * 09/17/2020 - Fixed bug with submitting a charity donation (was throwing System.InvalidCastException)
 * 12/24/2020 - Added Open DB button to allow me to quickly get into the database and edit a transaction.
 * 01/15/2021 - Fixed bug with really low amount spent on a category (causing it to register as empty string for percentage).
 * 01/16/2021 - Fixed ReportForm datagridview to show a scrollbar for all categories. Also, set its max height so it doesn't overflow off the screen.
 * 02/13/2021 - Round total earnings to 2 decimal places.
 * 04/17/2021 - Better positioning for taxes, wages tables.
 * 04/24/2021 - Show popup with previous and current charity balance updates when charity balance is changed.
 * 07/10/2021 - Bug fix for old and new charity balances not appearing after submitting other earnings.
 * 11/26/2021 - Added ability to modify and delete entries.
 * 11/28/2021 - Resize transaction gridview on start menu.
 * 12/25/2021 - Show success message popups after deleting/updating transactions.
 * 02/25/2022 - Added category column to current month's transactions table.
 * 12/9/2022  - Migrating over to SQLite database.
 * 1/2/2023   - Fixed issue with not inserting date correctly into SQLite.
 */

using System.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.IO;
using System.ServiceProcess;
using System.Data.SQLite;

namespace BudgetApp_V2
{
    public partial class StartMenu : Form
    {
        //public string connStr = new MySQLConnection().connection;
        LinkedList<string> categories = new LinkedList<string>();   //Holds the categories of spending.
        Database db;

        public StartMenu()
        {

            this.db = new Database();

            /*
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
            */

            InitializeComponent();

        }

        private void StartMenu_Load(object sender, EventArgs e)
        {

            dataGridView1.Visible = false;
            label1.Visible = false;
            amountCalculatedLabel.Visible = false;
            this.Text = "Budget App, Version 2.3";
            WindowState = FormWindowState.Maximized;
            categories = db.GetCategories();

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

            // Fill in category column
            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            //col.ValueMember = "Value";
            //col.DisplayMember = "Name";
            col.DataPropertyName = "5";
            col.HeaderText = "Category";
            col.DataSource = GetChoices();
            col.MaxDropDownItems = 3;
            dataGridView1.Columns.Add(col);
            
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

            updateDbButton.Visible = false;
            cancelUpdateButton.Visible = false;
            unselectItems();
        }

        public List<string> GetChoices()
        {
            List<string> categories = new List<string>();
            LinkedList<string> categoriesLinkedList = this.db.GetCategories();
            foreach (var item in categoriesLinkedList)
            {
                string itemLowercased = item.ToLower();
                categories.Add(itemLowercased);
            }
            return categories;
        }

        private void DisplayMonthTransactions()
        {
            //Remove the previous showing of this month's transactions (necessary because this method could be called after submitting a new transaction, which would then need to be added to the transactions list)
            dataGridView1.Rows.Clear();

            LinkedList<String[]> monthsTransactions = db.GetCurrentMonthsTransactions();

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

            for (int i = 0; i < monthsTransactions.Count; i++)  //Even though there should be five transactions in the linked list, there might not be if the database has been swiped of data.
            {
                dataGridView1.Rows.Add(monthsTransactions.ElementAt(i)[0], monthsTransactions.ElementAt(i)[1], monthsTransactions.ElementAt(i)[2], monthsTransactions.ElementAt(i)[3]);
                (dataGridView1.Rows[i].Cells[4] as DataGridViewComboBoxCell).Value = monthsTransactions.ElementAt(i)[4];
            }

            Console.WriteLine("Height: " + dataGridView1.Height);
            // Show the first 10 transactions. If fewer, then resize the gridview so a bunch of empty space isn't shown at bottom.
            if (dataGridView1.RowCount >= 10)
            {
                if (dataGridView1.RowCount == 10)  // Last row can be displayed without need for scrollbar.
                {
                    dataGridView1.ScrollBars = ScrollBars.None;
                }
                dataGridView1.Height = 285;
            } else
            {
                dataGridView1.ScrollBars = ScrollBars.None;
                dataGridView1.Height = 28 + (int)(25.7 * dataGridView1.RowCount);
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
            double amount = 0.0;
            try
            {
                amount = Math.Round(Convert.ToDouble(dt.Compute(transactionAmountTextBox.Text, "")), 2);  // Round to two decimal places. Don't want an amount such as $4.5061 in the database. Would instead have $4.51.
            }
            catch (SyntaxErrorException see)
            {
                throw see;
            } 
            catch (InvalidCastException ice) 
            {
                // this exception is usually thrown when Clear() method is called 
                Console.WriteLine("Invalid cast exception (code 158): " + ice);
            }
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
                catch (Exception e2)
                {
                    // show what the unknown exception was...
                    amountCalculatedLabel.Visible = true;
                    amountCalculatedLabel.Text = "ERROR 182: " + e2.Message;
                }
            }
            else
            {
                try
                {
                    //Place the transaction in the database.
                    //string connStr = new MySQLConnection().connection;

                    //Step 3: Create the SQL statement that deletes the notice.
                    // month / day less than 10 - pad with zer
                    string month = "";
                    string day = "";
                    if (transactionDateTimePicker.Value.Month < 10)
                    {
                        month = "0" + transactionDateTimePicker.Value.Month;
                    } else
                    {
                        month = transactionDateTimePicker.Value.Month.ToString();
                    }
                    
                    if (transactionDateTimePicker.Value.Day < 10)
                    {
                        day = "0" + transactionDateTimePicker.Value.Day;
                    }
                    else
                    {
                        day = transactionDateTimePicker.Value.Day.ToString();
                    }
                    string date = transactionDateTimePicker.Value.Year + "-" + month + "-" + day;
                    string description = db.FixStringForMySQL(transactionDescriptionTextBox.Text);

                    // Calculate math expressions for the amount given. For example: users can enter 12.34+.87. The amount in this case would equal $13.21.
                    double amount = getAmount();
                    if (amount != 0.0)
                    {
                        //Figure out which category this transaction fits in.
                        bool[] vals = new bool[categories.Count];
                        amount = Math.Abs(amount);  // make a positive number. The old version insisted that expenses be negative; however, under the new design, it is implied that anything in the expense table is negative

                        //Build the INSERT string. Place each possible category into it.
                        string sql = "";
                        double oldCharityBalance = 0.0;
                        bool showCharityBalanceChanges = false;
                        if (String.Equals(categoryComboBox.SelectedItem.ToString(), "Other Earnings"))  // Type = Other Earnings
                        {
                            sql = "INSERT INTO other_earnings (earning_date, description, amount) VALUES ('" + date + "', '" + description + "', " + amount + ");";

                            // Check if user wants to apply 10% (rounded up) towards charity balance.   
                            if (checkBox.Checked)
                            {
                                oldCharityBalance = db.GetCharityBalance();
                                showCharityBalanceChanges = true;

                                amount = Math.Ceiling(amount * 0.1);
                                db.ModifyCharityBalance(date, description + " (10+% applied to charity)", amount);
                            }

                            executeSql(sql);
                        }
                        else if (String.Equals(categoryComboBox.SelectedItem.ToString(), "Charity"))  // Type = Charity
                        {
                            oldCharityBalance = db.GetCharityBalance();
                            if (!checkBox.Checked)  // This is a decrease to the charity balance.
                            {
                                amount = -amount;  //make negative
                            }
                            // Increase to charity balance.
                            else
                            {
                                amount = Math.Ceiling(amount);
                            }
                            // Will need to decrease charity balance, since used some of the charity.
                            db.ModifyCharityBalance(date, description, amount); // turn amount into a negative (decrease in charity balance)
                            showCharityBalanceChanges = true;
                        }
                        else  // Some other expense type
                        {
                            sql = "INSERT INTO expenses (trans_date, description, amount, expense_type) VALUES ('" + date + "', '" + description + "', " + amount + ", '" + categoryComboBox.SelectedItem.ToString().ToLower() + "');";

                            executeSql(sql);
                        }

                        //Reset items.
                        Clear();

                        //Show previous charity balance and new charity balance.
                        if (showCharityBalanceChanges)
                        {
                            double charityBalance = db.GetCharityBalance();
                            MessageBox.Show("Old charity balance: " + Math.Round(oldCharityBalance, 2) +
                                "\nNew charity balance: " + Math.Round(charityBalance, 2), "Charity Balance was Updated Successfully!");
                        }

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
                catch (Exception e2)
                {
                    MessageBox.Show("EXCEPTION CAUGHT (code 263): " + e2.Message);
                }
            }
            unselectItems();
        }

        private void executeSql(string sql)
        {
            //MySqlConnection connection = new MySqlConnection(connStr);    //create the new connection using the parameters of connStr
            try
            {

                SQLiteCommand command = new SQLiteCommand(sql, db.sqliteconnection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                

                //db.sqliteconnection.Open();
                //SQLiteCommand sqlite_cmd;
                //sqlite_cmd = db.sqliteconnection.CreateCommand();
                //var reader = sqlite_cmd.ExecuteNonQuery();             //execute the command
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create expense entry: " + ex.Message);
            }
            db.sqliteconnection.Close();
        }

        

        private void clearButton_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            amountCalculatedLabel.Text = "";
            amountCalculatedLabel.Visible = false;
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
            categories = db.GetCategories();  // Reset the categories. This gets reset to zero after viewing the report form.
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
                double charityBalance = db.GetCharityBalance();
                charityBalanceLabel.Text = "Current charity balance: $" + Math.Round(charityBalance, 2);
            }
            catch (Exception e2)
            {
                Console.WriteLine(e2);
            }
        }

        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (categoryComboBox.SelectedItem.ToString().Equals("Charity") && checkBox.Checked)
                {  // round up for charity contributions on earnings
                    amountCalculatedLabel.Text = "" + Math.Ceiling(getAmount());
                }
                else // not a charity increase
                {
                    amountCalculatedLabel.Text = "" + getAmount();
                }
            } catch (Exception e2)
            {
                Console.WriteLine("Exception: " + e2);
            }
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

        private void transactionAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            if (transactionAmountTextBox.Text.IndexOfAny("*/+-".ToCharArray()) != -1)  // if we're using some sort of math expression in the amount text field, then calculate it and display it
            {
                try
                {
                    if (checkBox.Checked)
                    {
                        amountCalculatedLabel.Text = "$" + Math.Ceiling(getAmount());

                    }
                    else
                    {
                        amountCalculatedLabel.Text = "$" + getAmount();
                    }
                    amountCalculatedLabel.Visible = true;
                }
                catch (Exception e2)
                {
                    amountCalculatedLabel.Visible = false;
                    Console.WriteLine(e2);
                }
            } else // not using any math expressions in text field - no point in displaying the amount, as user already sees it in the text field
            {
                amountCalculatedLabel.Visible = false;
            }
        }

        private void wagesAndTaxesButton_Click(object sender, EventArgs e)
        {
            EarningsOverview earningsOverview = new EarningsOverview();
            earningsOverview.FormClosed += new FormClosedEventHandler(startMenuFormClosed);
            earningsOverview.Text = "Wages and Taxes";
            earningsOverview.Show();
            categories = db.GetCategories();  // Reset the categories. This gets reset to zero after viewing the report form.
            transactionDescriptionTextBox.Select();  // Set cursor to blinking in the description text box.
            this.Hide();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (categoryComboBox.SelectedItem.ToString().Equals("Charity") && checkBox.Checked)
                {  // round up for charity contributions on earnings
                    amountCalculatedLabel.Text = "" + Math.Ceiling(getAmount());
                }
                else  // charity decrease - use exact amount
                {
                    amountCalculatedLabel.Text = "" + getAmount();
                }
            }
            catch (SyntaxErrorException see)
            {
                amountCalculatedLabel.Text = "Invalid amount!";
            }
        }

        private void editTransactionButton1_Click(object sender, EventArgs e)
        {
            try  //Try to start heidisql
            {
                var mysqld = Process.Start("C:\\Program Files\\HeidiSQL\\heidisql.exe");
            }
            catch (System.ComponentModel.Win32Exception ex3)  //Unable to start the process (could be that the executable is located in a different folder path).
            {
                Console.WriteLine("Error message: " + ex3);
                MessageBox.Show("Unable to start the process C:\\Program Files\\HeidiSQL\\heidisql.exe.");
                Application.Exit();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            deleteTransactionButton1.Visible = true;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            cancelUpdateButton.Show();
            updateDbButton.Show();
        }

        private void cancelUpdateButton_Click(object sender, EventArgs e)
        {
            DisplayMonthTransactions();
            unselectItems();
        }

        private void updateDbButton_Click(object sender, EventArgs e)
        {
            bool success = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {

                    // check for invalid data
                    if (dataGridView1.Rows[row.Index].Cells[0].Value?.ToString().Length == 0 ||
                        dataGridView1.Rows[row.Index].Cells[1].Value?.ToString().Length == 0 ||
                        dataGridView1.Rows[row.Index].Cells[2].Value?.ToString().Length == 0 ||
                        dataGridView1.Rows[row.Index].Cells[3].Value?.ToString().Length == 0)
                    {
                        continue;
                    }

                    int trans_id = Int16.Parse(dataGridView1.Rows[row.Index].Cells[3].Value?.ToString());
                    String trans_date = dataGridView1.Rows[row.Index].Cells[0].Value?.ToString();
                    double amount = Double.Parse(dataGridView1.Rows[row.Index].Cells[2].Value?.ToString());
                    String description = dataGridView1.Rows[row.Index].Cells[1].Value?.ToString();
                    String expense_type = dataGridView1.Rows[row.Index].Cells[4].Value?.ToString();

                    // TODO: Need more input validation
                    
                    
                    success = db.UpdateEntry(trans_date, description, amount, trans_id, expense_type);
                } catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : " + ane.Message);
                }
            }
            if (success)
            {
                cancelUpdateButton.Hide();
                updateDbButton.Hide();
                MessageBox.Show("Transaction(s) were successfully updated.");
            }
        }

        private void deleteTransactionButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                int trans_id = Int16.Parse(Convert.ToString(selectedRow.Cells["id"].Value));
                string description = Convert.ToString(selectedRow.Cells["description"].Value);
                bool successful = db.DeleteTransaction(trans_id);

                DisplayMonthTransactions();

                unselectItems();

                if (successful)
                {
                    MessageBox.Show("The transaction \"" + description + "\" was successfully deleted.");
                } else
                {
                    MessageBox.Show("There was an error deleting the transaction \"" + description + "\"");
                }
            }
        }

        private void unselectItems()
        {
            // unselect items in datagridview
            dataGridView1.ClearSelection();
            deleteTransactionButton1.Visible = false;
            updateDbButton.Hide();
            cancelUpdateButton.Hide();
            deleteTransactionButton1.Hide();
        }
    }
}
