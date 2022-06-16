using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessObject.Interfaces
{
    public interface IMemberService
    {
        Task<int> CreateMemberAsync(Member member, CancellationToken cancellationToken = default);
        Task<int> DeleteMemberAsync(int id, CancellationToken cancellationToken = default);
        Task<Member> UpdateMemberAsync(Member member, CancellationToken cancellationToken = default);
        Task<Member> ViewMemberAsync(int id, CancellationToken cancellationToken = default);
        Task<ICollection<Member>> ViewMembersAsync(string? keyword, CancellationToken cancellationToken = default);
    }
}
