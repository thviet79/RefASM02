using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessObject.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
        Member? CurrentUser();

        bool IsAdmin();
    }
}
