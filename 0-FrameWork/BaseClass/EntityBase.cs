using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_FrameWork.BaseClass
{
   public class EntityBase
    {
        public long Id { get; set; }
        public DateTime Creation { get; set; }
        public EntityBase()
        {
            Creation = DateTime.Now;
        }
    }
}
