using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Inventory_Management_System.Helpers;
using Inventory_Management_System.Data;
using Inventory_Management_System.Models;
using System.Collections.ObjectModel;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ClosedXML.Excel;


namespace Inventory_Management_System.ViewModels
{
    public class ReportsViewModel
    {
        private readonly InventoryDbContext _db;
        public ObservableCollection<Sale> Sales { get; set; }

        public ICommand ExportPdfCommand { get; }
        public ICommand ExportExcelCommand { get; }

        public ReportsViewModel()
        {
            _db = new InventoryDbContext();
            Sales = new ObservableCollection<Sale>(_db.GetSales());

            ExportPdfCommand = new RelayCommand(ExportPdf);
            ExportExcelCommand = new RelayCommand(ExportExcel);
        }

        private void ExportPdf(object obj)
        {
            try
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SalesReport.pdf");

                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (Document doc = new Document(PageSize.A4))
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    doc.Add(new Paragraph("Sales Report"));
                    doc.Add(new Paragraph($"Generated on {DateTime.Now}\n\n"));

                    PdfPTable table = new PdfPTable(4);
                    table.AddCell("Product ID");
                    table.AddCell("Quantity Sold");
                    table.AddCell("Sale Date");
                    table.AddCell("Total Amount");

                    foreach (var sale in Sales)
                    {
                        table.AddCell(sale.ProductID.ToString());
                        table.AddCell(sale.QuantitySold.ToString());
                        table.AddCell(sale.SaleDate.ToShortDateString());
                        table.AddCell(sale.TotalAmount.ToString("C"));
                    }

                    doc.Add(table);
                    doc.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle/log error
                Console.WriteLine("PDF export failed: " + ex.Message);
            }
        }

        private void ExportExcel(object obj)
        {
            try
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SalesReport.xlsx");

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sales Report");
                    worksheet.Cell(1, 1).Value = "Product ID";
                    worksheet.Cell(1, 2).Value = "Quantity Sold";
                    worksheet.Cell(1, 3).Value = "Sale Date";
                    worksheet.Cell(1, 4).Value = "Total Amount";

                    int row = 2;
                    foreach (var sale in Sales)
                    {
                        worksheet.Cell(row, 1).Value = sale.ProductID;
                        worksheet.Cell(row, 2).Value = sale.QuantitySold;
                        worksheet.Cell(row, 3).Value = sale.SaleDate;
                        worksheet.Cell(row, 4).Value = sale.TotalAmount;
                        row++;
                    }

                    workbook.SaveAs(filePath);
                }
            }
            catch (Exception ex)
            {
                // Handle/log error
                Console.WriteLine("Excel export failed: " + ex.Message);
            }
        }
    }
}