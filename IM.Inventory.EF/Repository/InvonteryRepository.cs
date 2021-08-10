

using _0_FrameWork.BaseClass;
using _0_FrameWork.RepositoryBase;
using Infractrucuer.EFCore;
using InventoryApplicationContract.InventoryViewModel;
using inventoryManagmentDomain.InventoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IM.InventoryEF.Repository
{
  public class InvonteryRepository:RepositoryBaseClass<long ,InventoryDM>, IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly ShopContext _ShopContext;
        public InvonteryRepository(InventoryContext context, ShopContext ShopContext) :base(context)
        {
            _context = context;
            _ShopContext = ShopContext;
        }

        public InventoryDM GetBy(long productId)
        {
            return _context.Inventories.FirstOrDefault(q=>q.ProductId==productId);
        }

        public EditInventory GetDetails(long id)
        {
            return _context.Inventories.Select(p => new EditInventory
            {
                Id=p.Id,
                ProductId=p.ProductId,
                UnitPrice=p.unitePrice
            }).FirstOrDefault(p => p.Id == id);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            //var accounts = _accountContext.Accounts.Select(x => new { x.Id, x.Fullname }).ToList();
            var inventory = _context.Inventories.FirstOrDefault(x => x.Id == inventoryId);
            var operations = inventory.Operations.Select(x => new InventoryOperationViewModel
            {
                Id = x.Id,
                Count = x.Count,
                CurrentCount = x.CurrentCount,
                Description = x.Description,
                Operation = x.Operation,
                OperationDate = x.OperationDate.ToFarsi(),
               // OperatorId = x.OperatorId,
                OperatorId = 1,
                OrderId = x.OrderId
            }).OrderByDescending(x => x.Id).ToList();

            //foreach (var operation in operations)
            //{
            //    operation.Operator = accounts.FirstOrDefault(x => x.Id == operation.OperatorId)?.Fullname;
            //}

            return operations;
        }

        public List<InventoryViewmodel> Search(InventorySearchModel searchModel)
        {
            var productss = _ShopContext.Products.Select(p => new { p.Id, p.Name }).ToList();
            var query = _context.Inventories.Select(p => new InventoryViewmodel
            {
                Id=p.Id,
                UnitPrice=p.unitePrice,
                CreationDate=p.Creation.ToFarsi(),
                InStock=p.InStock,
                ProductId=p.ProductId,
                CurrentCount=p.CalcualateCurrentInventoryStock(),
                
            });

            if (searchModel.ProductId > 0)
                query = query.Where(p => p.ProductId == searchModel.ProductId);

            if (searchModel.InStock)
                query = query.Where(x => !x.InStock );

            var inventory = query.OrderBy(x => x.Id).ToList();
            inventory.ForEach(item => item.Product = productss.FirstOrDefault(p => p.Id == item.ProductId)?.Name);
            return inventory;
        }
    }
}
