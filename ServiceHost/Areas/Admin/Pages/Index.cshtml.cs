using A.M.Application.AccountApp;
using AccountManagment.Application.Contract.AccountVM;
using Appliction.Construct.ViewModel.Order;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Pages
{
    public class IndexModel : PageModel
    {
       public int ordercount;
       public int Newordercount;
       public int NewUsers;
        private readonly IOrderApplication _orderApplication;
        private readonly IAccountApplication _accountApplication;
        public IndexModel(IOrderApplication orderApplication, IAccountApplication accountApplication)
        {
            _orderApplication = orderApplication;
            _accountApplication = accountApplication;

        }
        public void OnGet()
        {
            
            ordercount = _orderApplication.GetAllOrdersForAdminIndex();
            
            
            Newordercount = _orderApplication.GetNewOrdersForAdminIndex();
            NewUsers = _accountApplication.GetNewCreateUserAsync().Result;


        }
    }
}
