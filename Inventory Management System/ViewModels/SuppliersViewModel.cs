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
            _db = new InventoryDbContext();
            Suppliers = new ObservableCollection<Supplier>(_db.GetSuppliers());

            AddSupplierCommand = new RelayCommand(AddSupplier);
            EditSupplierCommand = new RelayCommand(EditSupplier);
            RemoveSupplierCommand = new RelayCommand(RemoveSupplier);
            SearchCommand = new RelayCommand(SearchSuppliers);
        }

        private void AddSupplier(object obj)
        {
            if (obj is Supplier newSupplier)
            {
                _db.AddSupplier(newSupplier);
                Suppliers.Add(newSupplier);
            }
            else
            {
                // Example default supplier if no object passed
                var defaultSupplier = new Supplier
                {
                    SupplierName = "New Supplier",
                    ContactName = "Contact Person",
                    Phone = "000-000-0000",
                    Email = "supplier@example.com"
                };
                _db.AddSupplier(defaultSupplier);
                Suppliers.Add(defaultSupplier);
            }
        }

        private void EditSupplier(object obj)
        {
            if (obj is Supplier supplier)
            {
                _db.UpdateSupplier(supplier);

                var existing = Suppliers.FirstOrDefault(s => s.SupplierID == supplier.SupplierID);
                if (existing != null)
                {
                    var index = Suppliers.IndexOf(existing);
                    Suppliers[index] = supplier;
                }
            }
        }

        private void RemoveSupplier(object obj)
        {
            if (obj is Supplier supplier)
            {
                _db.DeleteSupplier(supplier.SupplierID);
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
