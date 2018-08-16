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
            string commandString = "SELECT LName,FName,CustomerNo FROM Customers; SELECT CustomerNo,EmployeeID FROM Orders; SELECT EmployeeID,LName,FName FROM Employees";
            DataSet shopDB = new DataSet("ShopDB");
            SqlDataAdapter adapter = new SqlDataAdapter(commandString, connectionString);
            adapter.Fill(shopDB);
            DataTable Customers = shopDB.Tables[0];
            DataTable Orders = shopDB.Tables[1];
            DataTable Employees = shopDB.Tables[2];
            shopDB.Relations.Add("Orders_Customers", Customers.Columns["CustomerNo"], Orders.Columns["CustomerNo"]);
            shopDB.Relations.Add("Orders_Employees", Employees.Columns["EmployeeID"], Orders.Columns["EmployeeID"]);
            Orders.Columns.Add("SurnameEmployee", typeof(String), "Parent(Orders_Employees).Fname");
            Orders.Columns.Add("NameEmployee", typeof(String), "Parent(Orders_Employees).Lname");
           // Orders.Columns.Add(new DataColumn("1", typeof(String), "dsfdsfsfd"));
            Orders.Columns.Add("SurnameCustomer", typeof(String), "Parent(Orders_Customers).Fname");
            Orders.Columns.Add("NameCustomer", typeof(String), "Parent(Orders_Customers).Lname");

            dataGridView1.DataSource = Orders;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            //DataGridViewTextBoxColumn[] column = new DataGridViewTextBoxColumn[Customers.Columns.Count];
            //for (int i = 0; i < Orders.Columns.Count; i++)
            //{


            //    column[i] = new DataGridViewTextBoxColumn();
            //    column[i].HeaderText = Orders.Columns[i].Caption;
            //    column[i].Name = "Header" + i;


            //    //}
            //    //this.dataGridView1.Columns.AddRange(column);

            //    //dataGridView1.Columns[0].Visible = false;
            //    //DataGridViewTextBoxColumn dgvAge = new DataGridViewTextBoxColumn();
            //    //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "dgvAge", HeaderText = "CustomerNo", Width = 100 });
            //    //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "dgvAge", HeaderText = "EmployeeID", Width = 100 });
            //    //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "dgvAge", HeaderText = "Fname", Width = 100 });
            //    //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "dgvAge", HeaderText = "Lname", Width = 100 });



            //    //foreach (DataRow order in Orders.Rows)
            //    //{
            //    //    if (order.GetParentRows("Orders_Employees").Length != 0)
            //    //    {
            //    //        dataGridView1.Rows.Add(order.ItemArray);
            //    //    }
            //    //}

            //}


        }
    }
}
