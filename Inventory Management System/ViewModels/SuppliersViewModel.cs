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
            _db = new InventoryDbContext("server=localhost;database=InventoryDB;user=root;password=yourpassword;");
            Suppliers = new ObservableCollection<Supplier>(_db.GetSuppliers());

            AddSupplierCommand = new RelayCommand(AddSupplier);
            EditSupplierCommand = new RelayCommand(EditSupplier);
            RemoveSupplierCommand = new RelayCommand(RemoveSupplier);
            SearchCommand = new RelayCommand(SearchSuppliers);
        }

        private void AddSupplier(object obj) { /* insert logic */ }
        private void EditSupplier(object obj) { /* update logic */ }
        private void RemoveSupplier(object obj) { /* delete logic */ }
        private void SearchSuppliers(object obj) { /* filter logic */ }
    }
}
