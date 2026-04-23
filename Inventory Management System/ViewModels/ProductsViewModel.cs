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
            _db = new InventoryDbContext("server=localhost;database=InventoryDB;user=root;password=yourpassword;");
            Products = new ObservableCollection<Product>(_db.GetProducts());

            AddProductCommand = new RelayCommand(AddProduct);
            EditProductCommand = new RelayCommand(EditProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct);
            SearchCommand = new RelayCommand(SearchProducts);
        }

        private void AddProduct(object obj) { /* call _db.AddProduct(product) */ }
        private void EditProduct(object obj) { /* update logic */ }
        private void DeleteProduct(object obj) { /* delete logic */ }
        private void SearchProducts(object obj) { /* filter logic */ }
    }
}

