using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountManagment.Application.Contract.AccountVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ChangePasswordUserModel : PageModel
    {
        public string ActiveCode;
        private readonly IAccountApplication _accountApplication;
        public ChangePasswordUserModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }
        public void OnGet(string Activecode)
        {
            
        }

        public IActionResult OnPostChangePasswordUser(ForgetPassword command)
        {
           
            _accountApplication.ForgetPassword(command);
            return RedirectToPage("/Index");
        }
    }
}
