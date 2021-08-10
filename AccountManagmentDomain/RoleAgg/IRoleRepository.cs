

using _0_FrameWork.BaseClass;
using AccountManagment.Application.Contract.RoleVM;
using System.Collections.Generic;

namespace AccountManagmentDomain.RoleAgg
{
   public interface IRoleRepository:IRepositoryBaseClass<long,Role>
    {
        List<RoleViewModel> List();
        EditRole GetDetails(long id);
    }
}
