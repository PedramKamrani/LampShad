using _0_FrameWork.ActiveCode;
using _0_FrameWork.BaseClass;
using AccountManagmentDomain.RoleAgg;
using Ganss.Xss;
using System;

namespace AccountManagmentDomain.AccountAgg
{
    public class AccountDomain:EntityBase
    {

        public string Fullname { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Mobile { get; private set; }
        public string ActiveCode { get; private set; }
        public string StreetCode { get; private set; }
        public string Address { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }
        public string ProfilePhoto { get; private set; }

        public AccountDomain()
        {

        }
        public AccountDomain(string fullname, string username, string password, string mobile,
            long roleId, string profilePhoto,string activecode,string streetcode,string address)
        {
            var htmlsantizer = new HtmlSanitizer();
            Fullname =fullname;
            Username = username;
            Password = password;
            Mobile = mobile;
            StreetCode = streetcode;
            Address = address;
            RoleId = roleId;
            if (roleId == 0)
                RoleId = 3;

            ProfilePhoto = htmlsantizer.Sanitize(profilePhoto);
            Creation = DateTime.Now;
            ActiveCode = activecode;
           
        }

        public void Edit(string fullname, string username, string mobile,
            long roleId, string profilePhoto,string activecode, string streetcode, string address)
        {
            var htmlsantizer = new HtmlSanitizer();
            Fullname = fullname;
            Username = username;
            Mobile =mobile;
            StreetCode = streetcode;
            Address = address;
            RoleId = roleId;
            ActiveCode =activecode ;

            if (!string.IsNullOrWhiteSpace(profilePhoto))
                ProfilePhoto = profilePhoto;
        }

        public void ChangePassword(string password)
        {
            Password = password;
            ActiveCode = ActiveCodeAccount.GenerateActiveCode();
        }
    }
}

