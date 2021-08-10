using _0_FrameWork.BaseClass;
using AccountManagment.Application.Contract.AccountVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagmentDomain.AccountAgg
{
   public interface IAccountRepository:IRepositoryBaseClass<long,AccountDomain>
    {
        AccountDomain GetBy(string username);
        AccountDomain GetByMobile(string Mobile);
        AccountDomain GetByActivecode(string ActiveCode);
        EditAccount GetDetails(long id);
        List<AccountViewModel> GetAccounts();
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        ForgetPassword GetActiveCodeByMobile(string mobile);
    }
}
