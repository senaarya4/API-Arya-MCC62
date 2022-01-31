using API.Base;
using API.Context;
using API.Models;
using API.Models.ViewModel;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository1;
        public IConfiguration _configuration;
        public MyContext context;
        public AccountsController(AccountRepository accountRepository, IConfiguration configuration, MyContext context) : base(accountRepository)
        {
            this.accountRepository1 = accountRepository;
            this._configuration = configuration;
            this.context = context;
        }

        [Route("login")]
        [HttpGet]

        public ActionResult<RegisterVM> Login(RegisterVM registerVM)
        {
            var result = accountRepository1.Login(registerVM);
            if (result > 0)
            {
                if (result == 1)
                {
                    var getUserData = context.Employees.Where(e => e.Email == registerVM.Email || e.Phone == registerVM.Phone).FirstOrDefault();//email, role
                    var account = context.Accounts.Where(a => a.NIK == getUserData.NIK).FirstOrDefault();
                    var role = context.AccountRoles.Where(r => r.AccountId == account.NIK).FirstOrDefault();

                    var claims = new List<Claim>
                    {
                        new Claim("Email", getUserData.Email),//payload
                        new Claim("Roles", role.Role.RoleName)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//header
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn
                        );

                    var idtoken = new JwtSecurityTokenHandler().WriteToken(token);//gen token
                    claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
                    
                    return StatusCode(200, new { status = HttpStatusCode.OK, idtoken, message = "Berhasil Login!" });
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

        [Authorize]
        [HttpGet("TestJwt")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT berhasil");
        }

        [Route("forget")]
        [HttpPut]

        public ActionResult SendOTP(RegisterVM registerVM)
        {
            var result = accountRepository1.SendOTP(registerVM.Email);
           
            if (result == 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Email Terkirim!" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Email Tidak Ditemukan!" });
            }
        }

        [Route("changepass")]
        [HttpPut]

        public ActionResult<ChgPasswdVM> ChangePass(ChgPasswdVM chgPasswdVM)
        {
            var result = accountRepository1.ChgPassword(chgPasswdVM);

            if (result == 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Berhasil ubah password!" });
            }
            else if (result == 2)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Password Konfirmasi tidak sesuai!" });
            }
            else if (result == 3)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "OTP sudah dipakai!" });
            }
            else if (result == 4)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Kode OTP tidak sesuai!" });
            }
            else if (result == 5)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Kode OTP sudah expired!" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Email tidak terdaftar!" });
            }
        }
    }
}
