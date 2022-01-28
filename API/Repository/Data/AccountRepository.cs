using API.Context;
using API.Models;
using API.Models.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using API.ViewModels;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext Context1;

        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.Context1 = myContext;
        }

        public int Login(RegisterVM registerVM)
        {
            var checkEmailPhone = Context1.Employees.Where(e => e.Email == registerVM.Email || e.Phone == registerVM.Email).FirstOrDefault();
            if (checkEmailPhone != null)
            {
                var getPw = Context1.Accounts.Where(e => e.NIK == checkEmailPhone.NIK).FirstOrDefault();
                if(BCrypt.Net.BCrypt.Verify(registerVM.Password, getPw.Password))
                {
                    return 1;//login success
                }
                else
                {
                    return 2;//wrong pw
                }
            }
            else
            {
                return 0;//not found
            }
        }

        public int SendOTP(string email)
        {
            var emp = Context1.Employees.Where(e => e.Email == email).FirstOrDefault();
            if (emp != null)
            {
                Random generator = new Random();
                int OTP = generator.Next(100000, 999999);

                var acc = Context1.Accounts.Where(e => e.NIK == emp.NIK).FirstOrDefault();
                acc.OTP = OTP;
                acc.ExpiredToken = DateTime.Now.AddMinutes(5);
                acc.isUsed = false;

                Context1.Entry(acc).State = EntityState.Modified;//here
                Context1.SaveChanges();//here

                var fromAddress = new MailAddress("jualakunak@gmail.com", "Customer Service");
                var toAddress = new MailAddress(email, "Client");
                const string fromPassword = "iloveyou3000";
                string subject = "OTP Reset Password " + DateTime.Now.ToString();
                string body = "Kode OTP Anda: "+OTP.ToString()+
                    "\n\n\n\nE-Mail ini dihasilkan otomatis oleh sistem, " +
                    "harap untuk tidak membalas.";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return 1;
            }
            else
            {
                return 0;//not found
            }
        }

        public int ChgPassword(ChgPasswdVM chgPasswdVM)
        {
            var checkEmail = Context1.Employees.Where(e => e.Email == chgPasswdVM.email).FirstOrDefault();
            if (checkEmail != null)
            {
                var acc = Context1.Accounts.Where(n => n.NIK == checkEmail.NIK).FirstOrDefault();
                if (DateTime.Now < acc.ExpiredToken)
                {
                    if (acc.OTP == chgPasswdVM.OTP)
                    {
                        if (acc.isUsed == false)
                        {
                            if (chgPasswdVM.password == chgPasswdVM.conPassword)
                            {
                                acc.Password = BCrypt.Net.BCrypt.HashPassword(chgPasswdVM.conPassword);
                                acc.isUsed = true;
                                Context1.Entry(acc).State = EntityState.Modified;
                                Context1.SaveChanges();
                                return 1;
                            }
                            else
                            {
                                return 2;
                            }
                        }
                        else
                        {
                            return 3;
                        }
                    }
                    return 4;
                }
                else
                {
                    return 5;
                }
            }
            else
            {
                return 0;
            }
        }

    }
}
