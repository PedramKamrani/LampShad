using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagment.Application.Contract.AccountVM
{
    public class EditAccount : RegisterAccount
    {
        public long Id { get; set; }
    }
}
