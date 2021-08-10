using _0_FrameWork.BaseClass;
using _0_FrameWork.RepositoryBase;
using Application.Contract.ViewModel.CustomerDiscountVM;
using DisCountDoamin.CustomerDisountAgg;
using Infractrucuer.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infractracer.EF.Repository
{
   public class CustomerDiscountRepository:RepositoryBaseClass<long,CustomerDiscount>, ICustomerDiscountRepository
    {
        public DisCountContext _context;
        public ShopContext _Shopcontext;
        public CustomerDiscountRepository(DisCountContext context, ShopContext Shopcontext) :base(context)
        {
            _context = context;
            _Shopcontext = Shopcontext;
        }

        public EditCustoemrDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(p => new EditCustoemrDiscount
            {
                Id=p.Id,
                DiscountRate=p.DiscountRate,
                EndDate=p.EndDate.ToString(),
                ProductId=p.ProductId,
                Reason=p.Reason,
                StartDate=p.StartDate.ToString(),
            }).FirstOrDefault(p=>p.Id==id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _Shopcontext.Products.Select(p =>new
            {
                p.Id,
                p.Name
            }).ToList();

            var query = _context.CustomerDiscounts.Select(p => new CustomerDiscountViewModel
            {
                StartDate=p.StartDate.ToFarsi(),
                CreationDate=p.Creation.ToFarsi(),
                DiscountRate=p.DiscountRate,
                EndDate=p.EndDate.ToFarsi(),
                Id=p.Id,
                Reason=p.Reason,
                ProductId=p.ProductId,

            });
            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                query = query.Where(x => x.StartDateGr > searchModel.StartDate.ToGeorgianDateTime());
            }

            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
            {
                query = query.Where(x => x.EndDateGr < searchModel.EndDate.ToGeorgianDateTime());
            }

            var discounts = query.OrderByDescending(x => x.Id).ToList();

            discounts.ForEach(discount =>
                discount.Product = products.FirstOrDefault(x => x.Id == discount.ProductId)?.Name);

            return discounts;
        }
    }
}
