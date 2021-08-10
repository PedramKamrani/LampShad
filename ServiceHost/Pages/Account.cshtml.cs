using System.Threading.Tasks;
using _0_FrameWork.BaseClass;
using AccountManagment.Application.Contract.AccountVM;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        [TempData]
        public string resultMessage { get; set; }
        [TempData]
        public string RegisterMessage { get; set; }

        private readonly IAccountApplication _accountApplication;


        public AccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;

        }
        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(Login command)
        {
            HtmlSanitizer htmlsantaizer = new HtmlSanitizer();
            Login logingsafe = new Login
            {
                Username = htmlsantaizer.Sanitize(command.Username),
                Password = htmlsantaizer.Sanitize(command.Password)
            };
            OperationResult result = _accountApplication.Login(logingsafe);
            if (result.Sussecced)
                return RedirectToPage("/Index");

            resultMessage = result.Message;
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("/Index");
        }
        public IActionResult OnPostRegister(RegisterAccount command)
        {
            HtmlSanitizer htmlsantaizer = new HtmlSanitizer();
            RegisterAccount registersafe = new RegisterAccount
            {
                Username = htmlsantaizer.Sanitize(command.Username),
                Fullname = htmlsantaizer.Sanitize(command.Fullname),
                Mobile = htmlsantaizer.Sanitize(command.Mobile),
                Password = htmlsantaizer.Sanitize(command.Password),
                Address = htmlsantaizer.Sanitize(command.Address),
                StreetCode = htmlsantaizer.Sanitize(command.StreetCode)

            };
            OperationResult result = _accountApplication.Register(registersafe);
            if (result.Sussecced)
            {
                RegisterMessage = result.Message;
                return Page();
            }


            RegisterMessage = result.Message;
            return Page();
        }
    }
}
