
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessObject.Helpers
{
    public interface IPasswordHasher
    {
        bool VerifyPassword(string rawPassword, string encryptedPasssword);
    }
}
