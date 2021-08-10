using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_FrameWork.BaseClass.Sms;
using AccountManagment.Application.Contract.AccountVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ForgetpasswordModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        private readonly ISmsService _smsservice;
        public ForgetpasswordModel(IAccountApplication accountApplication, ISmsService smsservice)
        {
            _accountApplication = accountApplication;
            _smsservice = smsservice;
        }
        public void OnGet()
        {
        }


        public IActionResult OnPostForgetPassword(string Mobile)
        {
            var active = _accountApplication.GetActiveCodeByMobile(Mobile);
            var command = new ForgetPassword
            {
                Mobile = Mobile,
                ActiveCode =active
            };
            var MobileSccusee = _accountApplication.GetActiveCodeByMobile(Mobile);
            if (!String.IsNullOrWhiteSpace(MobileSccusee))
            {
               _smsservice.Send(Mobile, $"{command.ActiveCode}:کد تایید شما مشتری{Mobile}");
                return RedirectToPage("/ActiveCode", Mobile);
            }
            return RedirectToPage("./ForgetPassword");
        }
    }
}
