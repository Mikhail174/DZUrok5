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

namespace DZUrok5Zad2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=WKS456\SQLEXPRESS;Initial Catalog=ShopDB;Integrated Security=True";
            string commandString = "SELECT EmployeeID FROM Employees; SELECT * FROM Orders;";
            DataSet shopDB = new DataSet("ShopDB");
            SqlDataAdapter adapter = new SqlDataAdapter(commandString, connectionString);
            adapter.Fill(shopDB);
            DataTable Employees = shopDB.Tables[0];
            DataTable orders = shopDB.Tables[1];
            shopDB.Relations.Add("Employees_Orders", Employees.Columns["EmployeeID"], orders.Columns["EmployeeID"]);
             Employees.Columns.Add("CountSale", typeof(double), "Count(Child(Employees_Orders).EmployeeID)");
            

            foreach (DataRow EmployeeRow in Employees.Rows)
            {
                // метод GetChaildRows получает дочерние строки в виде массива DataRow[]
                DataRow[] chilRows = EmployeeRow.GetChildRows("Employees_Orders");

            //    if (chilRows.Length != 0) // если существуют дочерние записи
              //  {


                dataGridView1.DataSource = Employees;
                ;


                foreach (DataRow ordersRow in chilRows)
                     Console.WriteLine("\tOrderId: {0}, OrderDate: {1};", ordersRow["OrderID"], ordersRow["OrderDate"]);


                      Console.WriteLine();
                //}
            }
        }
    }
}
