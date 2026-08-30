using Inventory_Management_System.Data;
using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Inventory_Management_System.ViewModels
{
    public class ProductsViewModel : INotifyPropertyChanged
    {
        private readonly InventoryDbContext _db;

        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Supplier> Suppliers { get; set; }

        // ---- fields bound to the "add product" form ----
        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; OnPropertyChanged(); }
        }

        private string _category;
        public string Category
        {
            get { return _category; }
            set { _category = value; OnPropertyChanged(); }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged(); }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }

        private Supplier _selectedSupplier;
        public Supplier SelectedSupplier
        {
            get { return _selectedSupplier; }
            set { _selectedSupplier = value; OnPropertyChanged(); }
        }

        // ---- the row currently highlighted in the grid ----
        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; OnPropertyChanged(); }
        }

        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand SearchCommand { get; }

        public ProductsViewModel()
        {
            _db = new InventoryDbContext();

            Products = new ObservableCollection<Product>();
            Suppliers = new ObservableCollection<Supplier>(_db.GetSuppliers());

            LoadProducts();

            AddProductCommand = new RelayCommand(AddProduct);
            EditProductCommand = new RelayCommand(EditProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct);
            SearchCommand = new RelayCommand(SearchProducts);
        }

        private void LoadProducts()
        {
            Products.Clear();
            foreach (var p in _db.GetProducts())
                Products.Add(p);
        }

        private void AddProduct(object obj)
        {
            if (string.IsNullOrWhiteSpace(ProductName))
            {
                MessageBox.Show("Enter a product name.", "Add product",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedSupplier == null)
            {
                MessageBox.Show("Choose a supplier.", "Add product",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _db.AddProduct(new Product
            {
                SupplierID = SelectedSupplier.SupplierID,
                ProductName = ProductName,
                Category = Category,
                Quantity = Quantity,
                Price = Price
            });

            // reload so the new row carries the ProductID the database assigned
            LoadProducts();

            ProductName = string.Empty;
            Category = string.Empty;
            Quantity = 0;
            Price = 0m;
            SelectedSupplier = null;
        }

        private void EditProduct(object obj)
        {
            var product = (obj as Product) ?? SelectedProduct;
            if (product == null)
            {
                MessageBox.Show("Select a product to edit.", "Edit product",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _db.UpdateProduct(product);
            LoadProducts();
        }

        private void DeleteProduct(object obj)
        {
            var product = (obj as Product) ?? SelectedProduct;
            if (product == null)
            {
                MessageBox.Show("Select a product to delete.", "Delete product",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Delete \"" + product.ProductName + "\"?", "Delete product",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm != MessageBoxResult.Yes) return;

            _db.DeleteProduct(product.ProductID);
            Products.Remove(product);
            SelectedProduct = null;
        }

        private void SearchProducts(object obj)
        {
            Products.Clear();
            foreach (var p in _db.GetProducts())
            {
                if (string.IsNullOrWhiteSpace(SearchText) ||
                    (p.ProductName != null &&
                     p.ProductName.IndexOf(SearchText, System.StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    Products.Add(p);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
