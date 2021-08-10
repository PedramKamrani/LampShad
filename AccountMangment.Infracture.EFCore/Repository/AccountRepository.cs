using _0_FrameWork.BaseClass;
using _0_FrameWork.RepositoryBase;
using AccountManagment.Application.Contract.AccountVM;
using AccountManagmentDomain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMangment.Infracture.EFCore.Repository
{
   public class AccountRepository:RepositoryBaseClass<long,AccountDomain>,IAccountRepository
    {
        private readonly AccountContext _context;
        public AccountRepository(AccountContext context) :base(context)
        {
            _context = context;
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _context.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                Fullname = x.Fullname
            }).ToList();
        }

        public AccountDomain GetBy(string username)
        {
            return _context.Accounts.FirstOrDefault(x => x.Username == username);
        }

        public AccountDomain GetByMobile(string Mobile)
        {
            return _context.Accounts.FirstOrDefault(x=>x.Mobile==Mobile);
        }
        public AccountDomain GetByActivecode(string ActiveCode)
        {
            return _context.Accounts.Where(x=>x.ActiveCode==ActiveCode).FirstOrDefault();
        }
        public EditAccount GetDetails(long id)
        {
            return _context.Accounts.Select(x => new EditAccount
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Mobile = x.Mobile,
                RoleId = x.RoleId,
                Username = x.Username
            }).FirstOrDefault(x => x.Id==id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {

            var query = _context.Accounts.Include(x=>x.Role)
                .Select(x => new AccountViewModel 
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Mobile = x.Mobile,
                ProfilePhoto = x.ProfilePhoto,
                Role = x.Role.Name,
                RoleId = x.RoleId,
                Username = x.Username,
                CreationDate = x.Creation.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Username))
                query = query.Where(x=>EF.Functions.Like(x.Username,$"%{searchModel.Username}%"));
                //query = query.Where(x => x.Username.Contains(searchModel.Username));

            if (!string.IsNullOrWhiteSpace(searchModel.Fullname))
                query = query.Where(x => EF.Functions.Like(x.Fullname,$"%{searchModel.Fullname}%"));
                //query = query.Where(x => x.Fullname.Contains(searchModel.Fullname));

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
                query = query.Where(x => EF.Functions.Like(x.Mobile,$"%{x.Mobile}%"));
               // query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));

            if (searchModel.RoleId > 0)
                query = query.Where(x => x.RoleId == searchModel.RoleId);

            return query.OrderByDescending(x => x.Id).ToList();
        }


        public ForgetPassword GetActiveCodeByMobile(string mobile)
        {
            var activecode= _context.Accounts.Select(a => new ForgetPassword {
                Mobile=a.Mobile,
                ActiveCode=a.ActiveCode,
              
            }).FirstOrDefault(a=>a.Mobile==mobile);
            return activecode;
        }
    }
}
