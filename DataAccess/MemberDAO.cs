using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessObject;
using DataAccess.Enums;

namespace DataAccess
{
    public class MemberDAO
    {
        private static ICollection<Member> MemberList = new List<Member>()
        {
            new Member{MemberID=3, MemberName="John", Email="John@gmail.com", Password="12345", City="New York",Country="USA"},
            new Member{MemberID=4, MemberName="Peter", Email="Peter@gmail.com", Password="12345", City="Munich",Country="Germany"},
        };
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }
        public ICollection<Member> GetMemberList => MemberList;  // GetMemberList => MemberList;
        public ICollection<Member> SearchMemberList(string? keyword, SearchBy searchBy) {
            switch (searchBy)
            {
                case SearchBy.ById:
                    return MemberList.Where(m => m.MemberID.ToString().Contains(keyword)).ToList();
                    
                case SearchBy.ByName:
                    return MemberList.Where(m => m.MemberName.ToLower().ToString().Contains(keyword?.ToLower())).ToList();
                default:
                    return MemberList;
            }
        }
        public Member? GetMemberByID(int memberID)
        {
            Member member = MemberList.FirstOrDefault(pro => pro.MemberID == memberID);
            return member;
        }
        public int AddNew(Member member)
        {
            Member pro = GetMemberByID(member.MemberID);
            if (pro == null)
            {
                MemberList.Add(member);
                return 1;
            }
            
            throw new Exception("Member is already exits.");
            
        }
        public int Update(Member member)
        {
            var mem = MemberList.FirstOrDefault(m => m.MemberID == member.MemberID);
            if(mem == null )
                throw new Exception("Member does not already exits.");
            // Note:
            // The code below should be changed based on your actual database, this is a demo
            MemberList.Remove(mem);
            MemberList.Add(member);
            return 1;
        }
        public int Remove(int MemberID)
        {
            Member m = GetMemberByID(MemberID);
            if (m != null)
            {
                MemberList.Remove(m);
                return 1;
            }
            else
            {
                throw new Exception("Member does not already exits.");
            }
        }
    }
}
