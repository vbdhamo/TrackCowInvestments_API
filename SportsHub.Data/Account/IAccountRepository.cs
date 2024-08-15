using SportsHub.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.Data.Account
{
    public interface IAccountRepository
    {
        Task<AccountModel> Login(AccountModel accountModel);
    }
}
