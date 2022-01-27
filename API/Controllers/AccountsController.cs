using API.Base;
using API.Models;
using API.Models.ViewModel;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository1;
        public AccountsController(AccountRepository accountRepository) : base(accountRepository)
        {
            this.accountRepository1 = accountRepository;
        }

        [Route("login")]
        [HttpGet]

        public ActionResult Login(RegisterVM registerVM)
        {
            var result = accountRepository1.Login(registerVM);
            if (result > 0)
            {
                if (result == 1)
                {
                    return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Berhasil Login!" });
                }
                else
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Password Salah!" });
                }
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "No Account Detected!" });
            }
        }
    }
}
