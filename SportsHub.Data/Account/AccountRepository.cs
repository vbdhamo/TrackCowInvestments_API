using Dapper;
using SportsHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.Data.Account
{
    public class AccountRepository:BaseRepository,IAccountRepository
    {
        public AccountRepository(PostgreSQLConnectionProvider postgreSQLConnectionProvider) : base(postgreSQLConnectionProvider)
        {

        }
        public async Task<AccountModel> Login(AccountModel accountModel)
        {
            using (var connection = await PostgreSQLConnectionProvider.GetPostgreSQLConnectionAsync())
            {
                var result = await connection.QueryAsync<AccountModel>(@"SELECT * FROM USERPROFILE WHERE LOWER(USERNAME) = LOWER(@USERNAME) AND PASSWORD = @PASSWORD AND STATUS = TRUE", new { USERNAME = accountModel.USERNAME, PASSWORD = accountModel.PASSWORD });
                return result.FirstOrDefault();
            }
        }
    }
}
