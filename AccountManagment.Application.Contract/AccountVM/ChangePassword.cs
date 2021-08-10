using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagment.Application.Contract.AccountVM
{
    public class ChangePassword
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }


    public class ForgetPassword
    {
        public string Mobile { get; set; }
        public string ActiveCode { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
