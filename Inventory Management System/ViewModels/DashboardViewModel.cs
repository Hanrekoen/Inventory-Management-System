using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory_Management_System.Helpers;


namespace Inventory_Management_System.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _totalProducts;
        public int TotalProducts
        {
            get => _totalProducts;
            set { _totalProducts = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalProducts))); }
        }

        private int _lowStockCount;
        public int LowStockCount
        {
            get => _lowStockCount;
            set { _lowStockCount = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LowStockCount))); }
        }

        private decimal _monthlySales;
        public decimal MonthlySales
        {
            get => _monthlySales;
            set { _monthlySales = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MonthlySales))); }
        }

        private int _activeSuppliers;
        public int ActiveSuppliers
        {
            get => _activeSuppliers;
            set { _activeSuppliers = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActiveSuppliers))); }
        }

        // Load dashboard stats from DB here
        public DashboardViewModel()
        {
            // Example placeholder values
            TotalProducts = 100;
            LowStockCount = 5;
            MonthlySales = 25000m;
            ActiveSuppliers = 12;
        }
    }
}
