using _01_QueryLamshade.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryQuery _inventoryQuery;
        public InventoryController(IInventoryQuery inventoryQuery)
        {
            _inventoryQuery = inventoryQuery;
        }
        [HttpPost]
        public StockStatus CheckStock([FromBody]IsInStock command)
        {
            return _inventoryQuery.CheckStock(command);
        }
    }
}
