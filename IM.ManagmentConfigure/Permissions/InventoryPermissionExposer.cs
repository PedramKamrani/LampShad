using _0_FrameWork.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM.ManagmentConfigure.Permissions
{
    public class InventoryPermissionExposer : IPermissonExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Inventory", new List<PermissionDto>
                    {
                        new PermissionDto(InventoryPermissions.ListInventory, "ListInventory"),
                        new PermissionDto(InventoryPermissions.SearchInventory, "SearchInventory"),
                        new PermissionDto(InventoryPermissions.CreateInventory, "CreateInventory"),
                        new PermissionDto(InventoryPermissions.EditInventory, "EditInventory"),
                        new PermissionDto(InventoryPermissions.Increase, "Increase"),
                        new PermissionDto(InventoryPermissions.Reduce, "Reduce"),
                        new PermissionDto(InventoryPermissions.OperationLog, "OperationLog")
                    }
                }
            };
        }
    }
}
