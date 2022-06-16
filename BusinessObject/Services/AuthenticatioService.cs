using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessObject.Helpers;
using BusinessObject.Interfaces;
using Microsoft.Extensions.Options;

namespace BusinessObject.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Member> _members;
        private readonly IPasswordHasher _passwordHasher;
        public Member? CurrentMember { get; set; }
        public AuthenticationService(IOptions<List<Member>> memberList, IPasswordHasher passwordHasher)
        {
            _members = memberList.Value;
            _passwordHasher = passwordHasher;
        }
        public async Task<bool> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            var isAuthenticated = _members.Any(member => member.Email.Equals(email) && _passwordHasher.VerifyPassword(password, member.Password));
            if (isAuthenticated)
            {
                CurrentMember = _members.First(member => member.Email.Equals(email));
            }
            return isAuthenticated;
        }

        public Member CurrentUser() => CurrentMember;
        public bool IsAdmin() => CurrentMember!=null && CurrentMember.Email.Equals("admin@fstore.com") && CurrentMember.Password.Equals("admin@@");
    }
}
