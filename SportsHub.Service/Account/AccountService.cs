using SportsHub.Data.Account;
using SportsHub.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.Service.Account
{
    public class AccountService:IAccountService
    {
        private readonly IAccountRepository _repository;
        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }
        public async Task<AccountModel>Login(AccountModel accountModel)
        {
            return await _repository.Login(accountModel);
        }
    }
}
