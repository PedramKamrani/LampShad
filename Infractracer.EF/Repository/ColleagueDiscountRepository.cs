using _0_FrameWork.BaseClass;
using _0_FrameWork.RepositoryBase;
using Application.Contract.ViewModel.ColleagueDiscountVM;
using DisCountDoamin.ColleagueDiscountAgg;
using Infractrucuer.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infractracer.EF.Repository
{
    public class ColleagueDiscountRepository : RepositoryBaseClass<long, ColleagueDiscount>, IColleagueRepository
    {
        private readonly DisCountContext _context;
        private readonly ShopContext _Shopcontext;
        public ColleagueDiscountRepository(DisCountContext context, ShopContext shopcontext) :base(context)
        {
            _context = context;
            _Shopcontext = shopcontext;
        }
        public EditcolleagueDiscountViewModel GetDetails(long id)
        {
            return _context.ColleagueDiscounts.Select(c => new EditcolleagueDiscountViewModel
            {
                Id=c.Id,
                DiscountRate=c.DiscountRate,
                ProductId=c.ProductId,
                
            }).FirstOrDefault(p=>p.Id==id);
        }

        public List<ColleagueDiscounViewModel> search(ColleagueSearchModel searchModel)
        {
            var products =_Shopcontext.Products.Select(p=>new {p.Id,p.Name }).ToList();
            var query = _context.ColleagueDiscounts.Select(p => new ColleagueDiscounViewModel
            {
                ProductId=p.ProductId,
                Id=p.Id,
                CreationDate=p.Creation.ToFarsi(),
                DiscountRate=p.DiscountRate,
                IsRemoved=p.IsRemved,
            });

            if (searchModel.ProductId > 0)
                query = query.Where(p => p.ProductId == searchModel.ProductId);
            var discount = query.OrderBy(p => p.Id).ToList();

            discount.ForEach(c => c.Product = products.FirstOrDefault(p => p.Id == c.ProductId)?.Name);
           return discount.ToList();
        }
    }
}
