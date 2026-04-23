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
    public class SuppliersViewModel
    {
        private readonly InventoryDbContext _db;
        public ObservableCollection<Supplier> Suppliers { get; set; }
        public string SearchText { get; set; }

        public ICommand AddSupplierCommand { get; }
        public ICommand EditSupplierCommand { get; }
        public ICommand RemoveSupplierCommand { get; }
        public ICommand SearchCommand { get; }

        public SuppliersViewModel()
        {
            // Use App.config connection string (InventoryDbContext reads it automatically)
            _db = new InventoryDbContext();
            Suppliers = new ObservableCollection<Supplier>(_db.GetSuppliers());

            AddSupplierCommand = new RelayCommand(AddSupplier);
            EditSupplierCommand = new RelayCommand(EditSupplier);
            RemoveSupplierCommand = new RelayCommand(RemoveSupplier);
            SearchCommand = new RelayCommand(SearchSuppliers);
        }

        private void AddSupplier(object obj)
        {
            var newSupplier = new Supplier
            {
                SupplierName = "New Supplier",
                ContactName = "Contact Person",
                Phone = "000-000-0000",
                Email = "supplier@example.com"
            };

            // Implement AddSupplier in InventoryDbContext
            // _db.AddSupplier(newSupplier);
            Suppliers.Add(newSupplier);
        }

        private void EditSupplier(object obj)
        {
            if (obj is Supplier supplier)
            {
                // Implement UpdateSupplier in InventoryDbContext
                // _db.UpdateSupplier(supplier);
                // Refresh collection if needed
            }
        }

        private void RemoveSupplier(object obj)
        {
            if (obj is Supplier supplier)
            {
                // Implement DeleteSupplier in InventoryDbContext
                // _db.DeleteSupplier(supplier.SupplierID);
                Suppliers.Remove(supplier);
            }
        }

        private void SearchSuppliers(object obj)
        {
            Suppliers.Clear();
            foreach (var s in _db.GetSuppliers())
            {
                if (string.IsNullOrEmpty(SearchText) || s.SupplierName.Contains(SearchText))
                {
                    Suppliers.Add(s);
                }
            }
        }
    }
}
