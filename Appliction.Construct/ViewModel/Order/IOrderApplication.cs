using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Construct.ViewModel.Order
{
   public interface IOrderApplication
    {

        long PlaceOrder(Cart cart);
        double GetAmountBy(long id);
        void Cancel(long id);
        string PaymentSucceeded(long orderId, long refId);
        List<OrderItemViewModel> GetItems(long orderId);
        List<OrderViewModel> Search(OrderSearchModel searchModel);

        int GetAllOrdersForAdminIndex();
        int GetNewOrdersForAdminIndex();

    }
}
