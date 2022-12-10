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

            dataGridView2.DataSource = getTotalWages();  // fill the gridview with the tax and wage info

            //Set the width of the grid view columns.
            dataGridView2.Columns[0].Width = 90;
            dataGridView2.Columns[1].Width = 450;
            dataGridView2.Columns[1].Width = dataGridView2.Width - dataGridView2.Columns[0].Width - dataGridView2.Columns[0].Width;

            //datagrid has calculated it's widths so we can store them
            for (int i = 0; i <= dataGridView2.Columns.Count - 1; i++)
            {
                //store autosized widths
                int colw = dataGridView2.Columns[i].Width;
                //remove autosizing
                dataGridView2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //set width to calculated by autosize
                dataGridView2.Columns[i].Width = colw;
            }
            fitRows(); // make the datagridview fit
        }

        private DataTable getTaxAndWageData()
        {
            return null;
        }

        private DataTable getTotalWages()
        {
            return null;
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

            var height2 = this.GetDataGridViewHeight(dataGridView2);
            dataGridView2.Height = height2;
            Height = height2 + Padding.Top + Padding.Bottom;
        }
    }
}
