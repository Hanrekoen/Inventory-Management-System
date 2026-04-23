using Inventory_Management_System.Data;
using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Inventory_Management_System.Helpers;


namespace Inventory_Management_System.ViewModels
{
    public class ProductsViewModel
    {
        private readonly InventoryDbContext _db;
        public ObservableCollection<Product> Products { get; set; }
        public string SearchText { get; set; }

        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand SearchCommand { get; }

        public ProductsViewModel()
        {
            // Uses InventoryDbContext (connection string handled inside)
            _db = new InventoryDbContext();
            Products = new ObservableCollection<Product>(_db.GetProducts());

            AddProductCommand = new RelayCommand(AddProduct);
            EditProductCommand = new RelayCommand(EditProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct);
            SearchCommand = new RelayCommand(SearchProducts);
        }

        private void AddProduct(object obj)
        {
            if (obj is Product newProduct)
            {
                _db.AddProduct(newProduct);
                Products.Add(newProduct);
            }
            else
            {
                // Example default product if no object passed
                var defaultProduct = new Product
                {
                    SupplierID = 1,
                    ProductName = "New Item",
                    Category = "General",
                    Quantity = 10,
                    Price = 99.99m
                };
                _db.AddProduct(defaultProduct);
                Products.Add(defaultProduct);
            }
        }

        private void EditProduct(object obj)
        {
            if (obj is Product product)
            {
                _db.UpdateProduct(product);

                // Refresh collection: replace the edited product
                var existing = Products.FirstOrDefault(p => p.ProductID == product.ProductID);
                if (existing != null)
                {
                    var index = Products.IndexOf(existing);
                    Products[index] = product;
                }
            }
        }

        private void DeleteProduct(object obj)
        {
            if (obj is Product product)
            {
                _db.DeleteProduct(product.ProductID);
                Products.Remove(product);
            }
        }

        private void SearchProducts(object obj)
        {
            Products.Clear();
            foreach (var p in _db.GetProducts())
            {
                if (string.IsNullOrEmpty(SearchText) || p.ProductName.Contains(SearchText))
                {
                    Products.Add(p);
                }
            }
        }
    }
}