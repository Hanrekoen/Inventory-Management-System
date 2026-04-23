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
    public class SalesViewModel
    {
        private readonly InventoryDbContext _db;
        public ObservableCollection<Sale> SalesHistory { get; set; }
        public ObservableCollection<Product> Products { get; set; }

        public Product SelectedProduct { get; set; }
        public int QuantitySold { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.Now;

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Product FilterProduct { get; set; }

        public ICommand AddSaleCommand { get; }
        public ICommand FilterSalesCommand { get; }

        public SalesViewModel()
        {
            _db = new InventoryDbContext("server=localhost;database=InventoryDB;user=root;password=yourpassword;");
            SalesHistory = new ObservableCollection<Sale>(_db.GetSales());
            Products = new ObservableCollection<Product>(_db.GetProducts());

            AddSaleCommand = new RelayCommand(AddSale);
            FilterSalesCommand = new RelayCommand(FilterSales);
        }

        private void AddSale(object obj) { /* call _db.AddSale(sale) */ }
        private void FilterSales(object obj) { /* filter logic */ }
    }
}
