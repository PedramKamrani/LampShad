using Appliction.Construct.ViewModel.Order;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Pages
{
    public class IndexModel : PageModel
    {
       public int ordercount;
       public int Newordercount;
        private readonly IOrderApplication _orderApplication;
        public IndexModel(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }
        public void OnGet()
        {
            
            ordercount = _orderApplication.GetAllOrdersForAdminIndex();
            
            
            Newordercount = _orderApplication.GetNewOrdersForAdminIndex();
        }
    }
}
