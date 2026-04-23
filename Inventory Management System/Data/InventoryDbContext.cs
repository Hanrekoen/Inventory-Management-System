using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory_Management_System.Models;
using System.Data;
using System.Data.SqlClient;


namespace Inventory_Management_System.Data
{
    public class InventoryDbContext 
    {
        private readonly string _connectionString = "server=MSI\\UDEMYMASTERSQL;database=InventoryDB;user=sa;password=20050615;";

        public InventoryDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        // ---------------- PRODUCTS ----------------
        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM Products", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductID = reader.GetInt32("ProductID"),
                            ProductName = reader.GetString("ProductName"),
                            Category = reader.GetString("Category"),
                            Quantity = reader.GetInt32("Quantity"),
                            Price = reader.GetDecimal("Price")
                        });
                    }
                }
            }
            return products;
        }

        public void AddProduct(Product product)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO Products (SupplierID, ProductName, Category, Quantity, Price) VALUES (@SupplierID, @Name, @Category, @Quantity, @Price)", conn);
                cmd.Parameters.AddWithValue("@SupplierID", product.SupplierID);
                cmd.Parameters.AddWithValue("@Name", product.ProductName);
                cmd.Parameters.AddWithValue("@Category", product.Category);
                cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.ExecuteNonQuery();
            }
        }

        // ---------------- SUPPLIERS ----------------
        public List<Supplier> GetSuppliers()
        {
            var suppliers = new List<Supplier>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM Suppliers", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        suppliers.Add(new Supplier
                        {
                            SupplierID = reader.GetInt32("SupplierID"),
                            SupplierName = reader.GetString("SupplierName"),
                            ContactName = reader.GetString("ContactName"),
                            Phone = reader.GetString("Phone"),
                            Email = reader.GetString("Email")
                        });
                    }
                }
            }
            return suppliers;
        }

        // ---------------- SALES ----------------
        public List<Sale> GetSales()
        {
            var sales = new List<Sale>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM Sales", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sales.Add(new Sale
                        {
                            SaleID = reader.GetInt32("SaleID"),
                            ProductID = reader.GetInt32("ProductID"),
                            QuantitySold = reader.GetInt32("QuantitySold"),
                            SaleDate = reader.GetDateTime("SaleDate"),
                            TotalAmount = reader.GetDecimal("TotalAmount")
                        });
                    }
                }
            }
            return sales;
        }

        public void AddSale(Sale sale)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO Sales (ProductID, QuantitySold, SaleDate, TotalAmount) VALUES (@ProductID, @QuantitySold, @SaleDate, @TotalAmount)", conn);
                cmd.Parameters.AddWithValue("@ProductID", sale.ProductID);
                cmd.Parameters.AddWithValue("@QuantitySold", sale.QuantitySold);
                cmd.Parameters.AddWithValue("@SaleDate", sale.SaleDate);
                cmd.Parameters.AddWithValue("@TotalAmount", sale.TotalAmount);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
