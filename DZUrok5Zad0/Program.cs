using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZUrok5Zad0
{
    class Program
    {
        static void Main(string[] args) {
            string connectionString = @"Data Source=МИХАИЛ-ПК\MSSQLSERVER1;Initial Catalog=ShopDB;Integrated Security=True";
            string commandString = "SELECT * FROM Products; SELECT * FROM OrderDetails;";

            DataSet shopDB = new DataSet("ShopDB");

            SqlDataAdapter adapter = new SqlDataAdapter(commandString, connectionString);

            adapter.Fill(shopDB);
            DataTable products = shopDB.Tables[0];
            DataTable orderdetails = shopDB.Tables[1];

            shopDB.Relations.Add("Products_OrderDetails", products.Columns["ProdID"], orderdetails.Columns["ProdID"]);

            foreach (DataRow orderdetailsRow in orderdetails.Rows)
            {
                var productsRow = orderdetailsRow.GetParentRow("Products_OrderDetails"); // метод GetParrentRow возвращает одну строку

                Console.WriteLine("TotalPrice: " + orderdetailsRow["TotalPrice"] + "\n" +
                                   
                                  "Product: " + productsRow[1] );

                Console.WriteLine();
            }

        }
    }
}
