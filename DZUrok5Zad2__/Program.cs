using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DZUrok5Zad2__
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=WKS456\SQLEXPRESS;Initial Catalog=ShopDB;Integrated Security=True";
            string commandString = "SELECT * FROM Employees; SELECT * FROM Orders;";
            DataSet shopDB = new DataSet("ShopDB");
            SqlDataAdapter adapter = new SqlDataAdapter(commandString, connectionString);
            adapter.Fill(shopDB);
            DataTable Employees = shopDB.Tables[0];
            DataTable orders = shopDB.Tables[1];
            shopDB.Relations.Add("Employees_Orders", Employees.Columns["EmployeeID"], orders.Columns["EmployeeID"]);
            // Employees.Columns.Add("CountSale", typeof(double), "SUM(Child(Employees_Orders).EmployeeID)");

            foreach (DataRow EmployeeRow in Employees.Rows)
            {
                // метод GetChaildRows получает дочерние строки в виде массива DataRow[]
                DataRow[] chilRows = EmployeeRow.GetChildRows("Employees_Orders");

                // if (chilRows.Length != 0) // если существуют дочерние записи
                {
                    Console.WriteLine("{0} {1} {2}", EmployeeRow[2], EmployeeRow[1], EmployeeRow[3]);





                    foreach (DataRow ordersRow in chilRows)
                        Console.WriteLine("\tOrderId: {0}, OrderDate: {1};", ordersRow["OrderID"], ordersRow["OrderDate"]);

                    Console.WriteLine();
                    // }
                }
            }
        }
    }
}

