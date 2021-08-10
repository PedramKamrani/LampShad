using _0_FrameWork.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisCountDoamin.ColleagueDiscountAgg
{
   public class ColleagueDiscount:EntityBase
    {
        public long ProductId { get; private set; }
        public int DiscountRate { get; private set; }
        public bool IsRemved { get; private set; }

        public ColleagueDiscount(long productId, int discountRate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            IsRemved = false;
            Creation = DateTime.Now;
        }


        public void Edit(long productId, int discountRate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            Creation = DateTime.Now;
        }

        public void Remove(long id)
        {
            this.IsRemved = true;
        }

        public void Restore(long id)
        {
            this.IsRemved = false;
        }
    }
}
