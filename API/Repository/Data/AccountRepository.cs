using API.Context;
using API.Models;
using API.Models.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
