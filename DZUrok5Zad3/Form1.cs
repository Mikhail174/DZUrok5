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

        BindingSource bindingSource1 = new BindingSource();

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=МИХАИЛ-ПК\MSSQLSERVER1;Initial Catalog=ShopDB;Integrated Security=True";
            string commandString = "SELECT CustomerNo,City FROM Customers; SELECT * FROM Orders;";
            DataSet shopDB = new DataSet("ShopDB");
            SqlDataAdapter adapter = new SqlDataAdapter(commandString, connectionString);
            adapter.Fill(shopDB);
            DataTable Customers = shopDB.Tables[0];
            DataTable Orders = shopDB.Tables[1];
            shopDB.Relations.Add("Customers_Orders", Customers.Columns["CustomerNo"], Orders.Columns["CustomerNo"]);
            Customers.Columns.Add("CountSale", typeof(double), "Count(Child(Customers_Orders).CustomerNo)");

            for (int i = 0; i < Customers.Columns.Count; i++)
            {
                // dataGridView1.Columns.Add("" ,"");
                dataGridView1.Columns.Add(new DataColumn(Customers.Columns[i].Caption, typeof(string)));
                MessageBox.Show(Customers.Columns[i].Caption);


            }
            dataGridView1.Columns[0].Visible = false;

            foreach (DataRow customer in Customers.Rows)
            {
                if (customer.GetChildRows("Customers_Orders").Length != 0)
                {
                   
                    dataGridView1.Rows.Add(customer.ItemArray);
                   // dataGridView1.Rows.Add(customer[2]);
                }
            }

        }


    }
}
