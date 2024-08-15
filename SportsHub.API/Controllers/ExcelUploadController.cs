using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
using SportsHub.Service.ExcelUpload;

namespace SportsHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelUploadController:ControllerBase
    {
        private readonly IExcelUploadService _service;
        private readonly ILogger<ExcelUploadController> _logger;
        public ExcelUploadController(IExcelUploadService service,ILogger<ExcelUploadController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost]
        [Route("ExcelUpload")]
        public async Task<IActionResult>ExcelUpload()
        {
            try
            {
                var read_exceldata = await ReadExcelKuccpsAndReturnDatatableNPOI(Request);
                var get = await _service.Get();
                return Ok(read_exceldata);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace);
                return BadRequest(e.Message);
            }

        }
        [NonAction]
        public async Task<DataTable> ReadExcelKuccpsAndReturnDatatableNPOI(Microsoft.AspNetCore.Http.HttpRequest request)
        {
            try
            {

                

                bool isLocalhot = HttpContext.Request.Host.Value.IndexOf("localhost") > 0;

                var files = Request.Form.Files;

                string ext = string.Empty; string filePath = string.Empty; string filename = String.Empty;



                if (Request.Form.Files.Count() > 0)
                {
                    string subDirectory = "KuccpsDataExcelFiles";
                    var file = Request.Form.Files[0];

                    var fileExtension = Path.GetExtension(file.FileName).ToUpper();

                    var fileSizes = new List<long>();

                    foreach (var file1 in files)
                    {
                        long fileSize = file1.Length; // Get the size of the current file in bytes
                        fileSizes.Add(fileSize);
                    }


                    ext = System.IO.Path.GetExtension(file.FileName);
                    string _name = subDirectory;

                    string directoryName = String.Empty;
                    string URL = String.Empty;
                    //var path = Directory.GetCurrentDirectory() + "/home/shiksion";
                    var path = isLocalhot ? Directory.GetCurrentDirectory() : "/home/KNA";
                    //var path = "/home/shiksion";

                    string pdfName = $"{_name}_{Guid.NewGuid().ToString()}_{file.FileName}";
                    //path = "/Home/RegistrationFiles";
                    filePath = $"{path}/{subDirectory}/{pdfName}";
                    directoryName = System.IO.Path.Combine(path, subDirectory);
                    filename = System.IO.Path.Combine(directoryName, pdfName);

                    if (System.IO.File.Exists(filename))
                    {
                        System.IO.File.Delete(filename);
                    }

                    try
                    {
                        // Create a sub directory
                        if (!Directory.Exists(directoryName))
                        {
                            Directory.CreateDirectory(directoryName);
                        }
                    }
                    catch (Exception e)
                    {
                        throw;
                    }

                    using (var stream = new FileStream(filename, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                }

                DataTable dt = new DataTable();

                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook;
                    // Choose the appropriate workbook class based on the file extension
                    if (Path.GetExtension(filePath) == ".xls")
                    {
                        workbook = new HSSFWorkbook(file); // For .xls files
                    }
                    else if (Path.GetExtension(filePath) == ".xlsx")
                    {
                        workbook = new XSSFWorkbook(file); // For .xlsx files
                    }
                    else
                    {
                        throw new Exception("Unsupported file format");
                    }

                    ISheet sheet = workbook.GetSheetAt(0); // Assuming you're reading from the first sheet

                    // Iterate through rows and columns to populate DataTable
                    IRow headerRow = sheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum;

                    // Create columns in DataTable
                    for (int i = 0; i < cellCount; i++)
                    {
                        DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                        dt.Columns.Add(column);
                    }

                    // Populate DataTable with data from Excel
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        DataRow dataRow = dt.NewRow();

                        if (row != null)
                        {
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                {
                                    dataRow[j] = row.GetCell(j).ToString();
                                }
                            }
                            dt.Rows.Add(dataRow);
                        }
                        else
                        {
                            continue;
                        }

                    }
                }

                return dt;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace);
                return null;
            }
        }
    }
}
