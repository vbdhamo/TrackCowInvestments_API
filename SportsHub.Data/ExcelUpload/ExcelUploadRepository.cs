using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.Data.ExcelUpload
{
    public class ExcelUploadRepository:BaseRepository, IExcelUploadRepository
    {
        public ExcelUploadRepository(PostgreSQLConnectionProvider postgreSQLConnectionProvider) : base(postgreSQLConnectionProvider)
        {

        }
        public async Task<dynamic> Get()
        {
            using (var connection = await PostgreSQLConnectionProvider.GetPostgreSQLConnectionAsync())
            {
                var result = await connection.QueryAsync<dynamic>(@"select * from userprofile");
                return result;
            }
        }
    }
}
