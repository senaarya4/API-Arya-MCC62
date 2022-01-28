using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class ChgPasswdVM
    {
        public int OTP { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public String conPassword { get; set; }
    }
}
