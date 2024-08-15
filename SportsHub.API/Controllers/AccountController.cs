using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.OpenXmlFormats.Dml;
using SportsHub.Model;
using SportsHub.Service.Account;
using System;
using System.Threading.Tasks;

namespace SportsHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController:ControllerBase
    {
        private readonly IAccountService _service;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountService service, ILogger<AccountController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult>Login(AccountModel accountModel)
        {
            try
            {
                var login = await _service.Login(accountModel);
                if(login == null)
                {
                    return BadRequest(new { message = "Invalid Username and Password."});
                }
                return Ok(login);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace);
                return BadRequest(e.Message);
            }
        }
    }
}
