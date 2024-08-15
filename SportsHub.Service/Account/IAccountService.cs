using SportsHub.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.Service.Account
{
    public interface IAccountService
    {
        Task<AccountModel> Login(AccountModel accountModel);
    }
}
