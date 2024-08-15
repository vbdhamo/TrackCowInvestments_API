using SportsHub.Data.ExcelUpload;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.Service.ExcelUpload
{
    public class ExcelUploadService:IExcelUploadService
    {
        private readonly IExcelUploadRepository _repository;
        public ExcelUploadService(IExcelUploadRepository repository)
        {
            _repository = repository; 
        }
        public async Task<dynamic>Get()
        {
            return await _repository.Get();
        }
    }
}
