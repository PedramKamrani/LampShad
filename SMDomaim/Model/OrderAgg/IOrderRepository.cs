using _0_FrameWork.BaseClass;
using Appliction.Construct.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDomaim.Model.OrderAgg
{
   public interface IOrderRepository:IRepositoryBaseClass<long,Order>
    {
        double GetAmountBy(long id);
        List<OrderItemViewModel> GetItems(long orderId);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
        int GetAllOrdersForAdminIndex();
        int GetNewOrdersForAdminIndex();
    }
}
