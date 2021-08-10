using _0_FrameWork.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventoryManagmentDomain.InventoryAgg
{
    public class InventoryDM : EntityBase
    {
        
       
        public long ProductId { get; set; }
        public double unitePrice { get; set; }
        public bool InStock { get; set; }
        public List<InventoryOperation> Operations { get; set; }
        protected InventoryDM() { }
        public InventoryDM(long productid, double UnitePrice)
        {
            ProductId = productid;
            unitePrice = UnitePrice;
            InStock = false;
            Creation = DateTime.Now;

        }


        public void Edit(long productid, double UnitPrice)
        {
            ProductId = productid;
            unitePrice = UnitPrice;
            Creation = DateTime.Now;
        }

        public long CalcualateCurrentInventoryStock()
        {
            var plus = Operations.Where(c => c.Operation).Sum(x => x.Count);
            var minus = Operations.Where(c=>!c.Operation).Sum(x=>x.Count);
            return plus - minus;
        }

        public void InCrease(long count,long operetorId,string description)
        {
            long CurrentCount = CalcualateCurrentInventoryStock() + count;
            var opration = new InventoryOperation(true, count, operetorId, CurrentCount, description,0,Id);
            Operations.Add(opration);
            InStock = CurrentCount > 0;
        }

        public void Reduce(long count,long operationid,string description,long orderid)
        {
            long currentcount = CalcualateCurrentInventoryStock() - count;
            var opreration = new InventoryOperation(false, count, operationid, currentcount, description, orderid, Id);
            Operations.Add(opreration);
            InStock = currentcount > 0;
        }
    }


}
