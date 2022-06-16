using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessObject;
using DataAccess;
using DataAccess.Enums;
using Microsoft.Extensions.Options;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public async Task<int> CreateMemberAsync(Member member, CancellationToken cancellationToken = default) {
            await Task.CompletedTask;
            return MemberDAO.Instance.AddNew(member);
        }
        public async Task<int> DeleteMemberAsync(int id, CancellationToken cancellationToken = default) {
            await Task.CompletedTask;
            return MemberDAO.Instance.Remove(id);
        }
        public async Task<int> UpdateMemberAsync(Member member, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            return MemberDAO.Instance.Update(member);
        }
        public async Task<Member?> ViewMemberAsync(int id, CancellationToken cancellationToken = default) {
            await Task.CompletedTask;
            return MemberDAO.Instance.GetMemberByID(id);
        }
        public async Task<IEnumerable<Member>> ViewMembersAsync( CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            return MemberDAO.Instance.GetMemberList;
        }
        public async Task<IEnumerable<Member>> SearchMembersAsync(string? keyword, SearchBy searchBy, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            return MemberDAO.Instance.SearchMemberList(keyword, searchBy);
        }
    }
}
