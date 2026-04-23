using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Models
{
    internal class Sale
    {
        public int SaleID { get; set; }         // Primary key
        public int ProductID { get; set; }      // Foreign key to Product
        public int QuantitySold { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
