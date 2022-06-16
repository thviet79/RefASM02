using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessObject;
using DataAccess.Enums;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        Task<int> CreateMemberAsync(Member member, CancellationToken cancellationToken = default);
        Task<int> DeleteMemberAsync(int id, CancellationToken cancellationToken = default);
        Task<int> UpdateMemberAsync(Member member, CancellationToken cancellationToken = default);
        Task<Member?> ViewMemberAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Member>> ViewMembersAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Member>> SearchMembersAsync(string? keyword, SearchBy searchBy, CancellationToken cancellationToken = default);
    }
}
