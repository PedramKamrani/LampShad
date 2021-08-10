using _0_FrameWork.RepositoryBase;
using System.Collections.Generic;

namespace AccountManagment.Application.Contract.RoleVM
{
    public class EditRole : CreateRole
    {
        public long Id { get; set; }
        public List<PermissionDto> MappedPermissions { get; set; }

        public EditRole()
        {
            Permissions = new List<int>();
        }
    }
}
