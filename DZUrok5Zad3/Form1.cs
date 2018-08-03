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

        private BindingSource binding1;

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=WKS456\SQLEXPRESS;Initial Catalog=ShopDB;Integrated Security=True";
            string commandString = "SELECT City, CustomerNo FROM Customers; SELECT * FROM Orders;";

            binding1 = new BindingSource();

            DataSet shopDB = new DataSet("ShopDB");
            SqlDataAdapter adapter = new SqlDataAdapter(commandString, connectionString);
            adapter.Fill(shopDB);
            DataTable Customers = shopDB.Tables[0];
            DataTable Orders = shopDB.Tables[1];
            shopDB.Relations.Add("Customers_Orders", Customers.Columns["CustomerNo"], Orders.Columns["CustomerNo"]);
            Customers.Columns.Add("CountSale", typeof(double), "Count(Child(Customers_Orders).CustomerNo)");
            




            /*foreach (DataColumn customer in Customers.Columns)
            {
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "A",
                    HeaderText = "Заголовок 1"
                });
               


                // if (customer.GetChildRows("Customers_Orders").Length != 0)
                // {

                //  dataGridView1.Rows.Add(customer);

                // }
            }
            foreach (DataRow rows in Customers.Rows)
                dataGridView1.Rows.Add(rows);*/

            binding1.DataSource = Customers;
            dataGridView1.DataSource =binding1;


        }
    }
}
