using _0_FrameWork.BaseClass;
using _0_FrameWork.RepositoryBase;
using AccountMangment.Infracture.EFCore;
using Appliction.Construct.ViewModel;
using Appliction.Construct.ViewModel.Order;
using Microsoft.EntityFrameworkCore;
using SMDomaim.Model.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infractrucuer.EFCore.Repository
{

    public class OrderRepository : RepositoryBaseClass<long, Order>, IOrderRepository
    {
        private readonly ShopContext _context;
        private readonly AccountContext _accountContext;

        public OrderRepository(ShopContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        public double GetAmountBy(long id)
        {
            var order = _context.Orders
                .Select(x => new { x.PayAmount, x.Id })
                .FirstOrDefault(x => x.Id == id);
            if (order != null)
                return order.PayAmount;
            return 0;
        }

        public List<OrderItemViewModel> GetItems(long orderId)
        {
            var products = _context.Products.Select(x => new { x.Id, x.Name }).ToList();
            var order = _context.Orders.Include(x=>x.Items).Where(x => x.Id ==orderId).FirstOrDefault();
            if (order == null)
                return new List<OrderItemViewModel>();

            var items = order.Items.Select(x => new OrderItemViewModel
            {
                Id = x.Id,
                Count = x.Count,
                DiscountRate = x.DiscountRate,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).ToList();

            foreach (var item in items)
            {
                item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            }

            return items;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var accounts = _accountContext.Accounts.Select(x => new { x.Id, x.Fullname,x.StreetCode,x.Address }).ToList();
            var query = _context.Orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                DiscountAmount = x.DiscountAmount,
                IsCanceled = x.IsCanceled,
                IsPaid = x.IsPaid,
                IssueTrackingNo = x.IssueTrackingNo,
                PayAmount = x.PayAmount,
                PaymentMethodId = x.PaymentMethod,
                RefId = x.RefId,
                TotalAmount = x.TotalAmount,
                CreationDate = x.Creation.ToFarsi()
            });

            query = query.Where(x => x.IsCanceled == searchModel.IsCanceled);

            if (searchModel.AccountId > 0) query = query.Where(x => x.AccountId == searchModel.AccountId);

            var orders = query.OrderByDescending(x => x.Id).ToList();
            foreach (var order in orders)
            {
                order.AccountFullName = accounts.FirstOrDefault(x => x.Id == order.AccountId)?.Fullname;
                order.AccountAddress = accounts.FirstOrDefault(x => x.Id == order.AccountId)?.Address;
                order.AccountStreeCode = accounts.FirstOrDefault(x => x.Id == order.AccountId)?.StreetCode;
                order.PaymentMethod = PaymentMethod.GetBy(order.PaymentMethodId).Name;
            }

            return orders;
        }

        public int GetAllOrdersForAdminIndex()
        {
            var order = _context.Orders.Select(o => new OrderViewModel
            {
                Id=o.Id,
                
            }).Count();
            return order;
        }

        public int GetNewOrdersForAdminIndex()
        {
            var order = _context.Orders.Where(o => o.Creation > DateTime.Now.AddDays(-1)&& o.Creation>DateTime.Now.AddDays(-2))
                .Select(o =>new  { o.Id,
                    o.Creation }).ToList();
            int Count= order.Count();
            return Count;
        }
    }
}
