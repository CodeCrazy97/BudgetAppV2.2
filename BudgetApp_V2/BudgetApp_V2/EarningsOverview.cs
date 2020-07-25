using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BudgetApp_V2
{
    public partial class EarningsOverview : Form
    {
        public EarningsOverview()
        {
            InitializeComponent();
        }

        private void EarningsOverview_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = getTaxAndWageData();  // fill the gridview with the tax and wage info

            //Set the width of the grid view columns.
            dataGridView1.Columns[0].Width = 90;
            dataGridView1.Columns[1].Width = 450;
            dataGridView1.Columns[1].Width = dataGridView1.Width - dataGridView1.Columns[0].Width - dataGridView1.Columns[0].Width;

            //datagrid has calculated it's widths so we can store them
            for (int i = 0; i <= dataGridView1.Columns.Count - 1; i++)
            {
                //store autosized widths
                int colw = dataGridView1.Columns[i].Width;
                //remove autosizing
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //set width to calculated by autosize
                dataGridView1.Columns[i].Width = colw;
            }
            fitRows(); // make the datagridview fit
        }

        private DataTable getTaxAndWageData()
        {
            DataTable dt = new DataTable();
            string connString = new MySQLConnection().connection;
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT gw.wage_year AS 'Year', ROUND(SUM(gw.amount), 0) AS 'Total Taxable Income', ROUND(IFNULL(total_tax.Taxes_Paid, 0) + IFNULL(tl.amount, 0), 2) AS 'Total Taxes Paid', IFNULL(ss_total_tax.amount, 0) AS 'Social Security', IFNULL(federal.amount, 0) AS 'Federal', IFNULL(state.amount, 0) AS 'State', IFNULL(tax_local.amount, 0) AS 'Local', ROUND(((IFNULL(tl.amount, 0) + IFNULL(total_tax.Taxes_Paid, 0) - IFNULL(tr.amount, 0))/SUM(gw.amount))*100, 2) AS 'Tax Rate' FROM gross_wages gw LEFT JOIN ( SELECT transaction_year, ROUND(SUM(amount), 2) AS 'Taxes_Paid' FROM taxation GROUP BY transaction_year) total_tax ON gw.wage_year = total_tax.transaction_year LEFT JOIN ( SELECT transaction_year, ROUND(SUM(amount), 2) AS 'amount' FROM taxation WHERE tax_type_name = 'Social Security' GROUP BY transaction_year) ss_total_tax ON gw.wage_year = ss_total_tax.transaction_year LEFT JOIN ( SELECT transaction_year, ROUND(SUM(amount), 2) AS 'amount' FROM taxation WHERE tax_type_name = 'Federal' GROUP BY transaction_year) federal ON gw.wage_year = federal.transaction_year LEFT JOIN ( SELECT transaction_year, ROUND(SUM(amount), 2) AS 'amount' FROM taxation WHERE tax_type_name = 'State' GROUP BY transaction_year) state ON gw.wage_year = state.transaction_year LEFT JOIN ( SELECT transaction_year, ROUND(SUM(amount), 2) AS 'amount' FROM taxation WHERE tax_type_name = 'Local' GROUP BY transaction_year) tax_local ON gw.wage_year = tax_local.transaction_year LEFT JOIN ( SELECT tax_year, ROUND(SUM(amount), 2) AS 'amount' FROM tax_return GROUP BY tax_year) tr ON tr.tax_year = gw.wage_year LEFT JOIN ( SELECT tax_year, ROUND(SUM(amount), 2) AS 'amount' FROM tax_liability GROUP BY tax_year) tl ON tl.tax_year = gw.wage_year GROUP BY gw.wage_year;", con))
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);
                }
            }
            return dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int GetDataGridViewHeight(DataGridView dataGridView)
        {
            var sum = (dataGridView.ColumnHeadersVisible ? dataGridView.ColumnHeadersHeight : 0) +
                      dataGridView.Rows.OfType<DataGridViewRow>().Where(r => r.Visible).Sum(r => r.Height);

            return sum;
        }

        // Make the rows fit the dataGridView size
        private void fitRows()
        {
            var height = this.GetDataGridViewHeight(dataGridView1);
            dataGridView1.Height = height;
            Height = height + Padding.Top + Padding.Bottom;
        }
    }
}
