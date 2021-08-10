using _0_FrameWork.ActiveCode;
using _0_FrameWork.BaseClass;
using AccountManagment.Application.Contract.AccountVM;
using AccountManagmentDomain.AccountAgg;
using AccountManagmentDomain.RoleAgg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace A.M.Application.AccountApp
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountrepository;
        private readonly IAuthHelper _authHelper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IFileUploader _fileUploader;
        private readonly IRoleRepository _roleRepository;
        public AccountApplication(IAccountRepository accountrepository, IPasswordHasher passwordHasher
            , IFileUploader fileUploader, IAuthHelper authHelper,IRoleRepository roleRepository)
        {
            _accountrepository = accountrepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
            _roleRepository = roleRepository;
        }
        #region Password
        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var passworduser = _accountrepository.Get(command.Id);

            if (passworduser == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);
            var password = _passwordHasher.Hash(command.Password);
            passworduser.ChangePassword(password);
            _accountrepository.SaveChanges();
            return operation.Succedded();
        }

        public AccountDomain GetAcctiveCodeByMobile(string mobile)
        {
            return _accountrepository.GetByMobile(mobile);
           
        }

        public OperationResult ForgetPassword(ForgetPassword command)
        {
            var operation = new OperationResult();
            var Account = _accountrepository.GetByMobile(command.Mobile);
            if (Account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if(Account.ActiveCode!=command.ActiveCode)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);

            var password = _passwordHasher.Hash(command.Password);
            Account.ChangePassword(password);
            _accountrepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult GetActiveCode(string ActiveCode)
        {
            var operation = new OperationResult();
            var active = _accountrepository.GetByActivecode(ActiveCode);
            if (active != null)
            {
                if (active.ActiveCode != ActiveCode)
                    return operation.Failed(ApplicationMessages.RecordNotFound);
                return operation.Succedded();
            }
            return operation.Failed(ApplicationMessages.RecordNotFound);
           
        }
        #endregion
        #region Crud
        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var Account = _accountrepository.Get(command.Id);
            if (Account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_accountrepository.Exists(x => (x.Username == command.Username
             || x.Mobile == command.Mobile) && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var image = _fileUploader.Upload(command.ProfilePhoto, "ProfilePhotos");
            string activecode = ActiveCodeAccount.GenerateActiveCode();
            Account.Edit(command.Fullname, command.Username, 
                command.Mobile, command.RoleId, image,activecode,command.StreetCode,command.Address);
            _accountrepository.SaveChanges();
            return operation.Succedded();
        }

        public AccountViewModel GetAccountBy(long id)
        {

            var account = _accountrepository.Get(id);
            return new AccountViewModel()
            {
                Fullname = account.Fullname,
                Mobile = account.Mobile
            };
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _accountrepository.GetAccounts();
        }

        public EditAccount GetDetails(long id)
        {
            return _accountrepository.GetDetails(id);
        }
        #endregion
        #region Login
        public OperationResult Login(Login command)
        {

            var operation = new OperationResult();
            var account = _accountrepository.GetBy(command.Username);
            if (account == null||command.Password==null)
                return operation.Failed(ApplicationMessages.WrongUserPass);
            var result = _passwordHasher.Check(account.Password, command.Password);
            if (!result.Verified)
                return operation.Failed(ApplicationMessages.WrongUserPass);

            var permissions = _roleRepository.Get(account.RoleId)
               .Permissions
               .Select(x => x.Code)
               .ToList();

            var authViewModel = new AuthViewModel(account.Id, account.RoleId,
                account.Fullname, account.Username, account.Mobile,permissions);
            _authHelper.Signin(authViewModel);
            return operation.Succedded();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();

            if (_accountrepository.Exists(x => x.Username == command.Username || x.Mobile == command.Mobile))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var password = _passwordHasher.Hash(command.Password);
            var image = _fileUploader.Upload(command.ProfilePhoto, "ProfilePhotos");
            string activecode = ActiveCodeAccount.GenerateActiveCode();
            var Account = new AccountDomain
                (command.Fullname, command.Username, password, command.Mobile
                , command.RoleId, image,activecode,command.StreetCode,command.Address);

            _accountrepository.Create(Account);
            _accountrepository.SaveChanges();
            return operation.Succedded();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountrepository.Search(searchModel);
        }


        #endregion

        #region GetActiveCode
        public string GetActiveCodeByMobile(string mobile)
        {
            var accuntcode= _accountrepository.GetActiveCodeByMobile(mobile);
            if(accuntcode!=null)
            return accuntcode.ActiveCode;
            return "";
        }
        #endregion
    }
}
