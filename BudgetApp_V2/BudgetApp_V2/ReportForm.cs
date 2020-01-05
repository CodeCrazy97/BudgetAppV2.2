using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BudgetApp_V2
{
    public partial class ReportForm : Form
    {
        public LinkedList<string> categories = new LinkedList<string>();  //Holds all categories.
        public LinkedList<double> totals = new LinkedList<double>();   //Holds the total amount spent for each category.

        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            DateTime dtSelected = Convert.ToDateTime(monthCalendar1.SelectionRange.Start.ToString());
            WindowState = FormWindowState.Maximized;
            //Select the first day of the first month of the current year for calendar 1.
            monthCalendar1.SetDate(Convert.ToDateTime(1 + "/" + 1 + "/" + DateTime.Today.Year));

            // Display the dates as text.
            date1Label.Text = "1/1/" + DateTime.Today.Year;
            date2Label.Text = DateTime.Today.Month + "/" + DateTime.Today.Day + "/" + DateTime.Today.Year;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DisplayTransactionsSummary()
        {
            categories.Clear();
            categories = new MySQLConnection().GetCategories();
            totals.Clear();
            dataGridView1.Rows.Clear();

            //Grab totals from the database.

            //Get the selected dates and place them in proper MySQL format.
            DateTime dt1 = Convert.ToDateTime(monthCalendar1.SelectionRange.Start.ToString());
            DateTime dt2 = Convert.ToDateTime(monthCalendar2.SelectionRange.Start.ToString());
            string date1 = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;
            string date2 = dt2.Year + "-" + dt2.Month + "-" + dt2.Day;

            // Display the date as text.
            date1Label.Text = dt1.Month + "/" + dt1.Day + "/" + dt1.Year;
            date2Label.Text = dt2.Month + "/" + dt2.Day + "/" + dt2.Year;

            if (!(dt1 > dt2))
            {
                warningLabel.Visible = false;

                string connStr = new MySQLConnection().connection;
                MySqlConnection conn = new MySqlConnection(connStr);
                try
                {
                    conn.Open();
                    for (int i = 0; i < categories.Count; i++)  //Loop through all categories, getting totals for each.
                    {
                        string sql = "SELECT SUM(amount) FROM expenses WHERE expensetype = '" + categories.ElementAt(i) + "' AND trans_date BETWEEN '" + date1 + "' AND '" + date2 + "'; ";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        var reader = cmd.ExecuteReader();             //execute the command
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(0))
                            {
                                totals.AddLast(0);
                            }
                            else
                            {
                                totals.AddLast(Math.Abs(reader.GetDouble(0)));
                            }
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                conn.Close();
                                
                
                //Get the total amount spent.
                double total = 0.0;
                for (int i = 0; i < totals.Count; i++)
                {
                    total += totals.ElementAt(i);
                }

                //Finally, display total amounts spent for each category on the gridview.
                for (int i = 0; i < categories.Count; i++)
                {
                    if (!(totals.ElementAt(i) == 0))  //Only show the category if it is nonzero.
                    {
                        //Calculate the percent weight of each transaction, put into format: XX.X%
                        string percent = ((totals.ElementAt(i) / total) * 100).ToString("#.#");
                        if (Convert.ToDouble(percent) < 1)  //Add a zero in front.
                        {
                            percent = "0" + percent;
                        }
                        else if (Convert.ToDouble(percent) == Math.Round(Convert.ToDouble(percent)))  //Add a zero to the end to make it look professional.
                        {
                            percent += ".0";
                        }
                        dataGridView1.Rows.Add(categories.ElementAt(i), Math.Round(totals.ElementAt(i)), percent);
                    }
                }
                //Display expenses.
                totalSpentLabel.Text = "$" + Math.Round(total);
            }
            else
            {
                warningLabel.Visible = true;
            }

            // Dynamically resize the data grid view, based on how many rows are in it (we don't want there to be unnecessary extra whitespace)
            if (dataGridView1.RowCount > 3) // Need a smaller base multiple to make a larger number of rows fit better in the data grid view.
            {
                dataGridView1.SetBounds(260, 408, dataGridView1.Width, 23 * dataGridView1.RowCount);
            } else
            {
                dataGridView1.SetBounds(260, 408, dataGridView1.Width, 25 * dataGridView1.RowCount);
            }
            
            
            

            //Sort the transactions according to amount, in descending order.
            this.dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            totalSpentLabel.Text = "N/A";
            DisplayTransactionsSummary();
        }

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {
            DisplayTransactionsSummary();
        }

    }
}