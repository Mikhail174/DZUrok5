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

namespace DZUrok5Zad4
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
            string commandString = "SELECT CustomerNo FROM Customers; SELECT CustomerNo,EmployeeID FROM Orders; SELECT EmployeeID FROM Employees";
            DataSet shopDB = new DataSet("ShopDB");
            SqlDataAdapter adapter = new SqlDataAdapter(commandString, connectionString);
            adapter.Fill(shopDB);
            DataTable Customers = shopDB.Tables[0];
            DataTable Orders = shopDB.Tables[1];
            DataTable Employees = shopDB.Tables[2];
            shopDB.Relations.Add("Orders_Customers", Customers.Columns["CustomerNo"], Orders.Columns["CustomerNo"]);
            shopDB.Relations.Add("Orders_Employees", Employees.Columns["EmployeeID"], Orders.Columns["EmployeeID"]);
            Orders.Columns.Add("NameCustomer", typeof(String), "Parent(Orders_Employees).EmployeeID");
            // dataGridView1.DataSource = Orders;
            //DataGridViewTextBoxColumn[] column = new DataGridViewTextBoxColumn[Customers.Columns.Count];
            //for (int i = 0; i < Customers.Columns.Count; i++)
            //{


            //    column[i] = new DataGridViewTextBoxColumn();
            //    column[i].HeaderText = Customers.Columns[i].Caption;
            //    column[i].Name = "Header" + i;


            //}
            //this.dataGridView1.Columns.AddRange(column);

            //dataGridView1.Columns[0].Visible = false;
            //DataGridViewTextBoxColumn dgvAge = new DataGridViewTextBoxColumn();
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "dgvAge", HeaderText = "CustomerNo", Width = 100 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "dgvAge", HeaderText = "EmployeeID", Width = 100 });


            foreach (DataRow customer in Orders.Rows)
            {
                // if (customer.GetParentRows("Orders_Employees").Length != 0)
                // {
                DataRow[] n = customer.GetParentRows("Orders_Employees");
                
                    dataGridView1.Rows.Add(n);
                    // dataGridView1.Rows.Add(customer[2]);
              // }
            }

        }


    }
}
