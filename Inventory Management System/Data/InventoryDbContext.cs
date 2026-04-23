using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Inventory_Management_System.Data
{
    public class InventoryDbContext
    {
        private readonly string _connectionString;

        public InventoryDbContext()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["InventoryDB"].ConnectionString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // ---------------- PRODUCTS ----------------
        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Products", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                            SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            Category = reader.GetString(reader.GetOrdinal("Category")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price"))
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
                var cmd = new SqlCommand(
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
                var cmd = new SqlCommand("SELECT * FROM Suppliers", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        suppliers.Add(new Supplier
                        {
                            SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),
                            SupplierName = reader.GetString(reader.GetOrdinal("SupplierName")),
                            ContactName = reader.GetString(reader.GetOrdinal("ContactName")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            Email = reader.GetString(reader.GetOrdinal("Email"))
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
                var cmd = new SqlCommand("SELECT * FROM Sales", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sales.Add(new Sale
                        {
                            SaleID = reader.GetInt32(reader.GetOrdinal("SaleID")),
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                            QuantitySold = reader.GetInt32(reader.GetOrdinal("QuantitySold")),
                            SaleDate = reader.GetDateTime(reader.GetOrdinal("SaleDate")),
                            TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
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
                var cmd = new SqlCommand(
                    "INSERT INTO Sales (ProductID, QuantitySold, SaleDate, TotalAmount, RecordedBy) VALUES (@ProductID, @QuantitySold, @SaleDate, @TotalAmount, @RecordedBy)", conn);
                cmd.Parameters.AddWithValue("@ProductID", sale.ProductID);
                cmd.Parameters.AddWithValue("@QuantitySold", sale.QuantitySold);
                cmd.Parameters.AddWithValue("@SaleDate", sale.SaleDate);
                cmd.Parameters.AddWithValue("@TotalAmount", sale.TotalAmount);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
