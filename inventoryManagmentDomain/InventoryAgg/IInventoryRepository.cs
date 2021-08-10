using _0_FrameWork.BaseClass;
using InventoryApplicationContract.InventoryViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventoryManagmentDomain.InventoryAgg
{
   public interface IInventoryRepository: IRepositoryBaseClass<long,InventoryDM>
    {
        EditInventory GetDetails(long id);
        InventoryDM GetBy(long productId);
        List<InventoryViewmodel> Search(InventorySearchModel searchModel);
        List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
    }
}
