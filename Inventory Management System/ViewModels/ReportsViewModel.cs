using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Inventory_Management_System.Helpers;


namespace Inventory_Management_System.ViewModels
{
    public class ReportsViewModel
    {
        public ICommand ExportPdfCommand { get; }
        public ICommand ExportExcelCommand { get; }

        public ReportsViewModel()
        {
            ExportPdfCommand = new RelayCommand(ExportPdf);
            ExportExcelCommand = new RelayCommand(ExportExcel);
        }

        private void ExportPdf(object obj)
        {
            // implement PDF export logic
        }

        private void ExportExcel(object obj)
        {
            // implement Excel export logic
        }
    }
}
