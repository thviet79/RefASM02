using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessObject.Interfaces;

namespace BusinessObject.Services
{
    public class MemberService : IMemberService
    {
        public Task<int> CreateMemberAsync(Member member, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task<int> DeleteMemberAsync(int id, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task<Member> UpdateMemberAsync(Member member, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task<Member> ViewMemberAsync(int id, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task<ICollection<Member>> ViewMembersAsync(string keyword, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    }
}
