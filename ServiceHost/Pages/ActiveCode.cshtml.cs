using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountManagment.Application.Contract.AccountVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ActiveCodeModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        public ActiveCodeModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }
        public void OnGet(string Activecode)
        {
            
        }

        public IActionResult OnPostActiveCode(string Activecode,string mobile)
        {
            var Acctive = _accountApplication.GetActiveCode(Activecode);
           
            if (!Acctive.Sussecced)
                return RedirectToPage("/AccessDenied");
            return RedirectToPage("/ChangePasswordUser",Activecode,mobile);
        }
    }
}
