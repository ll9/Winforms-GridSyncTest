using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformsSqlSyncTest.Utils;

namespace WinformsSqlSyncTest
{
    public partial class Form1 : Form
    {
        private SqliteHandler _sqliteService = SqliteFactory.CreateInstance();
        private DataTable _dataTable = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _sqliteService.CreateTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _sqliteService.InsertRandomPerson();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _sqliteService.DropTable();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _dataTable = _sqliteService.GetDataTable();
            dataGridView1.DataSource = _dataTable;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            _sqliteService.UpdateDataTable(_dataTable);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _sqliteService.DeleteOneRow();
        }
    }
}
