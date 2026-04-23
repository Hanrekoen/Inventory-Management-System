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
            _connectionString = "Server=MSI\\UDEMYMASTERSQL;Database=InventoryDB;User Id=sa;Password=20050615;";

        }

        // ---------------- PRODUCTS ----------------
        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            using (var conn = new SqlConnection(_connectionString))
            {
                var adapter = new SqlDataAdapter("SELECT * FROM Products", conn);
                var table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    products.Add(new Product
                    {
                        ProductID = (int)row["ProductID"],
                        SupplierID = (int)row["SupplierID"],
                        ProductName = row["ProductName"].ToString(),
                        Category = row["Category"].ToString(),
                        Quantity = (int)row["Quantity"],
                        Price = (decimal)row["Price"]
                    });
                }
            }
            return products;
        }

        public void AddProduct(Product product)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(
                    "INSERT INTO Products (SupplierID, ProductName, Category, Quantity, Price) VALUES (@SupplierID, @Name, @Category, @Quantity, @Price)", conn);
                cmd.Parameters.AddWithValue("@SupplierID", product.SupplierID);
                cmd.Parameters.AddWithValue("@Name", product.ProductName);
                cmd.Parameters.AddWithValue("@Category", product.Category);
                cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                cmd.Parameters.AddWithValue("@Price", product.Price);

                var adapter = new SqlDataAdapter { InsertCommand = cmd };
                conn.Open();
                adapter.InsertCommand.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(
                    "UPDATE Products SET SupplierID=@SupplierID, ProductName=@Name, Category=@Category, Quantity=@Quantity, Price=@Price WHERE ProductID=@ProductID", conn);
                cmd.Parameters.AddWithValue("@SupplierID", product.SupplierID);
                cmd.Parameters.AddWithValue("@Name", product.ProductName);
                cmd.Parameters.AddWithValue("@Category", product.Category);
                cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@ProductID", product.ProductID);

                var adapter = new SqlDataAdapter { UpdateCommand = cmd };
                conn.Open();
                adapter.UpdateCommand.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int productId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("DELETE FROM Products WHERE ProductID=@ProductID", conn);
                cmd.Parameters.AddWithValue("@ProductID", productId);

                var adapter = new SqlDataAdapter { DeleteCommand = cmd };
                conn.Open();
                adapter.DeleteCommand.ExecuteNonQuery();
            }
        }

        // ---------------- SUPPLIERS ----------------
        public List<Supplier> GetSuppliers()
        {
            var suppliers = new List<Supplier>();
            using (var conn = new SqlConnection(_connectionString))
            {
                var adapter = new SqlDataAdapter("SELECT * FROM Suppliers", conn);
                var table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    suppliers.Add(new Supplier
                    {
                        SupplierID = (int)row["SupplierID"],
                        SupplierName = row["SupplierName"].ToString(),
                        ContactName = row["ContactName"].ToString(),
                        Phone = row["Phone"].ToString(),
                        Email = row["Email"].ToString()
                    });
                }
            }
            return suppliers;
        }

        public void AddSupplier(Supplier supplier)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(
                    "INSERT INTO Suppliers (SupplierName, ContactName, Phone, Email) VALUES (@Name, @Contact, @Phone, @Email)", conn);
                cmd.Parameters.AddWithValue("@Name", supplier.SupplierName);
                cmd.Parameters.AddWithValue("@Contact", supplier.ContactName);
                cmd.Parameters.AddWithValue("@Phone", supplier.Phone);
                cmd.Parameters.AddWithValue("@Email", supplier.Email);

                var adapter = new SqlDataAdapter { InsertCommand = cmd };
                conn.Open();
                adapter.InsertCommand.ExecuteNonQuery();
            }
        }

        public void UpdateSupplier(Supplier supplier)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(
                    "UPDATE Suppliers SET SupplierName=@Name, ContactName=@Contact, Phone=@Phone, Email=@Email WHERE SupplierID=@SupplierID", conn);
                cmd.Parameters.AddWithValue("@Name", supplier.SupplierName);
                cmd.Parameters.AddWithValue("@Contact", supplier.ContactName);
                cmd.Parameters.AddWithValue("@Phone", supplier.Phone);
                cmd.Parameters.AddWithValue("@Email", supplier.Email);
                cmd.Parameters.AddWithValue("@SupplierID", supplier.SupplierID);

                var adapter = new SqlDataAdapter { UpdateCommand = cmd };
                conn.Open();
                adapter.UpdateCommand.ExecuteNonQuery();
            }
        }

        public void DeleteSupplier(int supplierId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("DELETE FROM Suppliers WHERE SupplierID=@SupplierID", conn);
                cmd.Parameters.AddWithValue("@SupplierID", supplierId);

                var adapter = new SqlDataAdapter { DeleteCommand = cmd };
                conn.Open();
                adapter.DeleteCommand.ExecuteNonQuery();
            }
        }

        // ---------------- SALES ----------------
        public List<Sale> GetSales()
        {
            var sales = new List<Sale>();
            using (var conn = new SqlConnection(_connectionString))
            {
                var adapter = new SqlDataAdapter("SELECT * FROM Sales", conn);
                var table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    sales.Add(new Sale
                    {
                        SaleID = (int)row["SaleID"],
                        ProductID = (int)row["ProductID"],
                        QuantitySold = (int)row["QuantitySold"],
                        SaleDate = (DateTime)row["SaleDate"],
                        TotalAmount = (decimal)row["TotalAmount"],
                    });
                }
            }
            return sales;
        }

        public void AddSale(Sale sale)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(
                    "INSERT INTO Sales (ProductID, QuantitySold, SaleDate, TotalAmount) VALUES (@ProductID, @QuantitySold, @SaleDate, @TotalAmount)", conn);
                cmd.Parameters.AddWithValue("@ProductID", sale.ProductID);
                cmd.Parameters.AddWithValue("@QuantitySold", sale.QuantitySold);
                cmd.Parameters.AddWithValue("@SaleDate", sale.SaleDate);
                cmd.Parameters.AddWithValue("@TotalAmount", sale.TotalAmount);

                var adapter = new SqlDataAdapter { InsertCommand = cmd };
                conn.Open();
                adapter.InsertCommand.ExecuteNonQuery();
            }
        }

        public void UpdateSale(Sale sale)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(
                    "UPDATE Sales SET ProductID=@ProductID, QuantitySold=@QuantitySold, SaleDate=@SaleDate, TotalAmount=@TotalAmountWHERE SaleID=@SaleID", conn);
                cmd.Parameters.AddWithValue("@ProductID", sale.ProductID);
                cmd.Parameters.AddWithValue("@QuantitySold", sale.QuantitySold);
                cmd.Parameters.AddWithValue("@SaleDate", sale.SaleDate);
                cmd.Parameters.AddWithValue("@TotalAmount", sale.TotalAmount);
                cmd.Parameters.AddWithValue("@SaleID", sale.SaleID);
                var adapter = new SqlDataAdapter { UpdateCommand = cmd };
                conn.Open();
                adapter.UpdateCommand.ExecuteNonQuery();
            }
        }

        public void DeleteSale(int saleId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("DELETE FROM Sales WHERE SaleID=@SaleID", conn);
                cmd.Parameters.AddWithValue("@SaleID", saleId);

                var adapter = new SqlDataAdapter { DeleteCommand = cmd };
                conn.Open();
                adapter.DeleteCommand.ExecuteNonQuery();
            }
        }
    }
}
