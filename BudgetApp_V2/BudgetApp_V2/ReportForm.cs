﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace BudgetApp_V2
{
    public partial class ReportForm : Form
    {
        public LinkedList<string> categories = new LinkedList<string>();  //Holds all categories.
        public LinkedList<double> totals = new LinkedList<double>();   //Holds the total amount spent for each category.
        public SQLite sqlite = null;

        public ReportForm()
        {
            this.sqlite = new SQLite();
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
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
            categories = this.sqlite.GetCategories();
            totals.Clear();
            dataGridView1.Rows.Clear();

            //Grab totals from the database.

            //Get the selected dates and place them in proper MySQL format.
            DateTime dt1 = Convert.ToDateTime(monthCalendar1.SelectionRange.Start.ToString());
            DateTime dt2 = Convert.ToDateTime(monthCalendar2.SelectionRange.Start.ToString());

            // Use the "yyyy-MM-dd" format to ensure zero-padding for month and day
            string date1 = dt1.ToString("yyyy-MM-dd");
            string date2 = dt2.ToString("yyyy-MM-dd");

            // Display the date as text.
            date1Label.Text = dt1.Month + "/" + dt1.Day + "/" + dt1.Year;
            date2Label.Text = dt2.Month + "/" + dt2.Day + "/" + dt2.Year;

            if (!(dt1 > dt2))
            {
                warningLabel.Visible = false;

                for (int i = 0; i < categories.Count; i++)  //Loop through all categories, getting totals for each.
                {

                    using (var selectCommand = this.sqlite.connection_object.CreateCommand())
                    {
                        if (categories.ElementAt(i).ToLower() == "tithe")
                        {
                            // Negative amount for tithe means it was an expense (a positive amount means it's a tithe that is earmarked)
                            selectCommand.CommandText = @"SELECT SUM(amount) FROM expenses WHERE expense_type = @expense_type AND amount < 0 AND trans_date BETWEEN @date1 AND @date2; ";
                        } else
                        {
                            selectCommand.CommandText = @"SELECT SUM(amount) FROM expenses WHERE expense_type = @expense_type AND trans_date BETWEEN @date1 AND @date2; ";
                        }
                        selectCommand.Parameters.AddWithValue("@expense_type", categories.ElementAt(i).ToLower());
                        selectCommand.Parameters.AddWithValue("@date1", date1);
                        selectCommand.Parameters.AddWithValue("@date2", date2);

                        using (var reader = selectCommand.ExecuteReader())
                        {
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
                }

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
                        if (percent.Equals("") || Convert.ToDouble(percent) < 1)  //Add a zero in front.
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
            int size = (21 * dataGridView1.RowCount) + 7;
            dataGridView1.SetBounds(260, 408, dataGridView1.Width, size);
            if (dataGridView1.Height > 217) // Prevent the height from being more than 217 (otherwise, the datagridview will overlap)
            {
                dataGridView1.Height = 217;
                dataGridView1.ScrollBars = ScrollBars.Vertical;  // create vertical scrollbars so user can see all transaction overviews
            } else  // remove the vertical scrollbars (may have been created earlier when there was need for them)
            {
                dataGridView1.ScrollBars = ScrollBars.None;
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