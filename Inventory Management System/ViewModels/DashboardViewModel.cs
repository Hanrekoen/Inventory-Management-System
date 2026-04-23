using Inventory_Management_System.Data;
using Inventory_Management_System.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Inventory_Management_System.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly InventoryDbContext _db;

        private int _totalProducts;
        public int TotalProducts
        {
            get => _totalProducts;
            set { _totalProducts = value; OnPropertyChanged(nameof(TotalProducts)); }
        }

        private int _lowStockCount;
        public int LowStockCount
        {
            get => _lowStockCount;
            set { _lowStockCount = value; OnPropertyChanged(nameof(LowStockCount)); }
        }

        private decimal _monthlySales;
        public decimal MonthlySales
        {
            get => _monthlySales;
            set { _monthlySales = value; OnPropertyChanged(nameof(MonthlySales)); }
        }

        private int _activeSuppliers;
        public int ActiveSuppliers
        {
            get => _activeSuppliers;
            set { _activeSuppliers = value; OnPropertyChanged(nameof(ActiveSuppliers)); }
        }

        public DashboardViewModel()
        {
            // Use the App.config connection string (InventoryDbContext reads it automatically)
            _db = new InventoryDbContext();
            try { LoadDashboardData(); }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dashboard data: {ex.Message}");
            }

        }

        private void LoadDashboardData()
        {
            // Total products
            var products = _db.GetProducts();
            TotalProducts = products.Count;

            // Low stock (example threshold: < 5 units)
            LowStockCount = products.FindAll(p => p.Quantity < 5).Count;

            // Monthly sales (sum of TotalAmount for current month)
            var sales = _db.GetSales();
            decimal monthlyTotal = 0;
            foreach (var sale in sales)
            {
                if (sale.SaleDate.Month == DateTime.Now.Month && sale.SaleDate.Year == DateTime.Now.Year)
                {
                    monthlyTotal += sale.TotalAmount;
                }
            }
            MonthlySales = monthlyTotal;

            // Active suppliers
            ActiveSuppliers = _db.GetSuppliers().Count;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}