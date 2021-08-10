using _0_FrameWork.BaseClass;
using InventoryApplicationContract.InventoryViewModel;
using inventoryManagmentDomain.InventoryAgg;
using System.Collections.Generic;

namespace InventoryApplication
{
    public class InventoryApp : IInventoryApplication
    {
        private readonly IInventoryRepository _repository;
        private readonly IAuthHelper _authHelper;
        public InventoryApp(IInventoryRepository repository, IAuthHelper authHelper)
        {
            _repository = repository;
            _authHelper = authHelper;
        }
        public OperationResult Create(CreateInventory command)
        {
            var oprition = new OperationResult();
            if (_repository.Exists(x => x.ProductId == command.ProductId))
                return oprition.Failed(ApplicationMessages.DuplicatedRecord);
            var inventory = new InventoryDM(command.ProductId, command.UnitPrice);
            _repository.Create(inventory);
            _repository.SaveChanges();
            return oprition.Succedded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var oprition = new OperationResult();
            var inventorys = _repository.Get(command.Id);
            if (inventorys == null)
                return oprition.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x => x.ProductId == command.ProductId&&x.Id!=command.Id))
                return oprition.Failed(ApplicationMessages.DuplicatedRecord);
            inventorys.Edit(command.ProductId, command.UnitPrice);
            _repository.SaveChanges();
            return oprition.Succedded();
        }

        public EditInventory GetDetails(long id)
        {
          return  _repository.GetDetails(id);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            return _repository.GetOperationLog(inventoryId);
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var oprition = new OperationResult();
            var inventorys = _repository.Get(command.InventoryId);
            if (inventorys == null)
                return oprition.Failed(ApplicationMessages.RecordNotFound);

            const long operatorid = 1;
            inventorys.InCrease(command.Count, operatorid, command.Description);
            _repository.SaveChanges();
            return oprition.Succedded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var oprition = new OperationResult();
            var inventorys = _repository.Get(command.InventoryId);
            if (inventorys == null)
                return oprition.Failed(ApplicationMessages.RecordNotFound);

           var operatorid = _authHelper.CurrentAccountId();
            inventorys.Reduce(command.Count,operatorid, command.Description, 0);
            _repository.SaveChanges();
            return oprition.Succedded();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var oprition = new OperationResult();
            var operatorid = _authHelper.CurrentAccountId();
            foreach (var item in command)
            {
                var inventory = _repository.GetBy(item.ProductId);

                inventory.Reduce(item.Count, operatorid, item.Description, item.OrderId);
            }
            _repository.SaveChanges();
            return oprition.Succedded();
        }

        public List<InventoryViewmodel> Search(InventorySearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}
