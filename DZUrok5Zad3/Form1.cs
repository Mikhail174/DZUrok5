using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DZUrok5Zad3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=МИХАИЛ-ПК\MSSQLSERVER1;Initial Catalog=ShopDB;Integrated Security=True";
            string commandString = "SELECT City, CustomerNo FROM Customers; SELECT * FROM Orders;";
            DataSet shopDB = new DataSet("ShopDB");
            SqlDataAdapter adapter = new SqlDataAdapter(commandString, connectionString);
            adapter.Fill(shopDB);
            DataTable Customers = shopDB.Tables[0];
            DataTable Orders = shopDB.Tables[1];
            DataTable Test = new DataTable();
            shopDB.Relations.Add("Customers_Orders", Customers.Columns["CustomerNo"], Orders.Columns["CustomerNo"]);
            Customers.Columns.Add("CountSale", typeof(double), "Count(Child(Customers_Orders).CustomerNo)");
            DataColumn column1 = new DataColumn();
            column1.ColumnName = "ChildID";
            Test.Columns.Add(column1);








            foreach (DataRow customer in Customers.Rows)
            {
                if (customer.GetChildRows("Customers_Orders").Length != 0)
                {

                    foreach (DataColumn column in Customers.Columns)

                        Test.Columns.Add(column);

                }
            }
            dataGridView1.DataSource = Test;

        }
    }
}
