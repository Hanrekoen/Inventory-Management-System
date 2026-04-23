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
        public ICommand EditSaleCommand { get; }
        public ICommand DeleteSaleCommand { get; }
        public ICommand FilterSalesCommand { get; }

        public SalesViewModel()
        {
            _db = new InventoryDbContext();

            SalesHistory = new ObservableCollection<Sale>(_db.GetSales());
            Products = new ObservableCollection<Product>(_db.GetProducts());

            AddSaleCommand = new RelayCommand(AddSale);
            EditSaleCommand = new RelayCommand(EditSale);
            DeleteSaleCommand = new RelayCommand(DeleteSale);
            FilterSalesCommand = new RelayCommand(FilterSales);
        }

        private void AddSale(object obj)
        {
            if (SelectedProduct == null || QuantitySold <= 0) return;

            var newSale = new Sale
            {
                ProductID = SelectedProduct.ProductID,
                QuantitySold = QuantitySold,
                SaleDate = SaleDate,
                TotalAmount = SelectedProduct.Price * QuantitySold
            };

            _db.AddSale(newSale);
            SalesHistory.Add(newSale);

            // Reset fields
            QuantitySold = 0;
            SaleDate = DateTime.Now;
        }

        private void EditSale(object obj)
        {
            if (obj is Sale sale)
            {
                _db.UpdateSale(sale);

                var existing = SalesHistory.FirstOrDefault(s => s.SaleID == sale.SaleID);
                if (existing != null)
                {
                    var index = SalesHistory.IndexOf(existing);
                    SalesHistory[index] = sale;
                }
            }
        }

        private void DeleteSale(object obj)
        {
            if (obj is Sale sale)
            {
                _db.DeleteSale(sale.SaleID);
                SalesHistory.Remove(sale);
            }
        }

        private void FilterSales(object obj)
        {
            SalesHistory.Clear();
            foreach (var sale in _db.GetSales())
            {
                bool matchesDate = (!StartDate.HasValue || sale.SaleDate >= StartDate.Value) &&
                                   (!EndDate.HasValue || sale.SaleDate <= EndDate.Value);

                bool matchesProduct = FilterProduct == null || sale.ProductID == FilterProduct.ProductID;

                if (matchesDate && matchesProduct)
                {
                    SalesHistory.Add(sale);
                }
            }
        }
    }
}
