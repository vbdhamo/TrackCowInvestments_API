using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.Service.ExcelUpload
{
    public interface IExcelUploadService
    {
        Task<dynamic> Get();
    }
}
