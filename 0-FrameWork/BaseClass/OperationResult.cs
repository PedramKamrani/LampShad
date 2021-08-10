using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_FrameWork.BaseClass
{
   public class OperationResult
    {
        public OperationResult()
        {
            Sussecced = false;
        }
        public bool Sussecced { get; set; }
        public string Message { get; set; }

        public OperationResult Succedded(string message = "عملیات با موفقیت انجام شد")
        {
            Sussecced = true;
           Message= message;
            return this;
        }

        public OperationResult Failed(string message)
        {
            Sussecced = false;
            Message = message;
            return this;
        }
    }
}
