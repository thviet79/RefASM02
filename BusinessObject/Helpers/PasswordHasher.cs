
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessObject.Helpers
{
    public class PasswordHasher : IPasswordHasher
    {
        public bool VerifyPassword(string rawPassword, string encryptedPasssword)
        {
            // Todo: Just a simplifed biz logic to decrypt the passwooord
            return rawPassword.Equals(encryptedPasssword);
        }
    }
}
