using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.Data.ExcelUpload
{
    public interface IExcelUploadRepository
    {
        Task<dynamic> Get();
    }
}
