using ClosedXML.Excel;
using InsuranceManagementSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceManagmentSystem.Controllers
{
    public class ExcelReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExcelReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Customer Report
        public async Task<IActionResult> CustomerReport()
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Customers");

            worksheet.Cell(1, 1).Value = "Customer ID";
            worksheet.Cell(1, 2).Value = "Name";
            worksheet.Cell(1, 3).Value = "Date of Birth";
            worksheet.Cell(1, 4).Value = "Gender";
            worksheet.Cell(1, 5).Value = "Mobile";
            worksheet.Cell(1, 6).Value = "Email";
            worksheet.Cell(1, 7).Value = "Address";

            int row = 2;

            foreach (var customer in customers)
            {
                worksheet.Cell(row, 1).Value = customer.CustomerID;
                worksheet.Cell(row, 2).Value = customer.Name;
                worksheet.Cell(row, 3).Value = customer.DateOfBirth;
                worksheet.Cell(row, 4).Value = customer.Gender;
                worksheet.Cell(row, 5).Value = customer.Mobile;
                worksheet.Cell(row, 6).Value = customer.Email;
                worksheet.Cell(row, 7).Value = customer.Address;

                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Customer_Report.xlsx"
            );
        }
    }
}