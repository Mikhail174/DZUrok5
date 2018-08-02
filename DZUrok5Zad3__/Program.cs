using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DZUrok5Zad3__
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=WKS456\SQLEXPRESS;Initial Catalog=ShopDB;Integrated Security=True";
            string commandString = "SELECT City, CustomerNo FROM Customers; SELECT * FROM Orders;";
            DataSet shopDB = new DataSet("ShopDB");
            SqlDataAdapter adapter = new SqlDataAdapter(commandString, connectionString);
            adapter.Fill(shopDB);
            DataTable Customers = shopDB.Tables[0];
            DataTable Orders = shopDB.Tables[1];
            shopDB.Relations.Add("Customers_Orders", Customers.Columns["CustomerNo"], Orders.Columns["CustomerNo"]);
            Customers.Columns.Add("CountSale", typeof(double), "Count(Child(Customers_Orders).CustomerNo)");
            foreach (DataRow customer in Customers.Rows)
            {
                if (customer.GetChildRows("Customers_Orders").Length != 0)
                {
                    Console.WriteLine("{0} {1}", customer[0], customer[2]);
                    Console.WriteLine();
                   
               }
            }
        }
    }
}
