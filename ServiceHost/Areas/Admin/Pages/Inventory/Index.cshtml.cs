using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_FrameWork.RepositoryBase;
using Application.Contract.ViewModel.ColleagueDiscountVM;
using Appliction.Construct.ViewModel.ProductVM;
using IM.ManagmentConfigure.Permissions;
using InventoryApplicationContract.InventoryViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Admin.Pages.Inventory
{
    public class IndexModel : PageModel
    {
       public List<InventoryViewmodel> Inventorys;
       public SelectList Products;
       public InventorySearchModel SearchModel;

        private readonly IInventoryApplication _application;
        private readonly IProductAppliction _productApp;
        public IndexModel(IInventoryApplication application, IProductAppliction productApp)
        {
            _productApp = productApp;
            _application = application;
        }
     
        public void OnGet(InventorySearchModel searchModel)
        {
            Products =new SelectList(_productApp.GetAllProduct(),"Id","Name");
            Inventorys = _application.Search(searchModel);
        }
        //[NeedsPermission(InventoryPermissions.CreateInventory)]
        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory
            {
                Products = _productApp.GetAllProduct()
            };

            return Partial("./Create", command);
        }
        //[NeedsPermission(InventoryPermissions.CreateInventory)]
        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _application.Create(command);

            return new JsonResult(result);
        }
       // [NeedsPermission(InventoryPermissions.EditInventory)]
        public IActionResult OnGetEdit(long id)
        {

            var inventory = _application.GetDetails(id);
           inventory.Products = _productApp.GetAllProduct();
            return Partial("./Edit", inventory);
        }
       // [NeedsPermission(InventoryPermissions.EditInventory)]
        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _application.Edit(command);

            return new JsonResult(result);
        }
       // [NeedsPermission(InventoryPermissions.Increase)]
        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory
            {
                InventoryId = id
            };
            
            return Partial("./Increase", command);
        }
       // [NeedsPermission(InventoryPermissions.Increase)]
        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var result = _application.Increase(command);

            return new JsonResult(result);
        }

        //[NeedsPermission(InventoryPermissions.Reduce)]
        public IActionResult OnGetReduce(long id)
        {

            var command = new ReduceInventory
            {
                InventoryId = id
            };
            return Partial("./Reduce", command);
        }
       // [NeedsPermission(InventoryPermissions.Reduce)]
        public JsonResult OnPostReduce(ReduceInventory command)
        {
            var result = _application.Reduce(command);

            return new JsonResult(result);
        }

       // [NeedsPermission(InventoryPermissions.OperationLog)]
        public IActionResult OnGetOperationLog(long id)
        {

            var log = _application.GetOperationLog(id);
            return Partial("./OperationLog", log);
        }
    }
}
