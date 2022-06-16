using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessObject;
using BusinessObject.Interfaces;
using DataAccess.Enums;
using DataAccess.Repository;
using Microsoft.Extensions.Options;

namespace MyStoreWinApp
{
   
    public partial class frmMemberManagement : Form
    {
        private readonly IMemberService _memberService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMemberRepository _memberRepository;
        private readonly List<Member> _systemMembers;
        private readonly frmMemberDetails _frmMemberDetail;
        public frmLogin FrmLogin { get; set; }

        private int _rowIndex = 0;
        private SearchBy _searchBy = SearchBy.ById;



        IMemberRepository memberRepository = new MemberRepository();
        BindingSource source;
        public frmMemberManagement(IMemberService memberService, IAuthenticationService authenticationService, IMemberRepository memberRepository, IOptions<List<Member>> members, frmMemberDetails frmMemberDetails)
        {
            _memberService = memberService;
            _authenticationService = authenticationService;
            _memberRepository = memberRepository;
            InitializeComponent();
            _systemMembers = members.Value;
            _frmMemberDetail = frmMemberDetails;
        }
        private void frmMemberManagement_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
        }
        private void dgvMemberList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex == 3 && e.Value != null)
            {
                dgvMemberList.Rows[e.RowIndex].Tag = e.Value;
                e.Value = new String('\u25CF', e.Value.ToString().Length);
                return;
            }
        }
        private void ClearText()
        {
            txtMemberID.Text = string.Empty;
            txtMemberName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtCountry.Text = string.Empty;
        }
        private Member GetMemberObject()
        {
            Member member = null;
            try
            {
                member = new Member
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Member");
            }
            return member;
        }
        public void LoadMemberList()
        {

            // If Current user is admin (email contain admin@fstore.com and pwd contain admin@@)
            if (_authenticationService.IsAdmin())
            {
                foreach (var sysMem in _systemMembers)
                {
                    _memberRepository.CreateMemberAsync(sysMem);
                }

            }
            var members = _memberRepository.ViewMembersAsync().Result.ToList();
            members = members.OrderBy(x => x.MemberID).ToList();
            try
            {
                source = new BindingSource();
                source.DataSource = members;
                txtMemberID.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                txtCountry.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtCity.DataBindings.Add("Text", source, "City");
                txtCountry.DataBindings.Add("Text", source, "Country");

                dgvMemberList.DataSource = null;
                dgvMemberList.DataSource = source;                
                if (members.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadMemberList();
        }
        
        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void frmMemberManagement_Shown(object sender, EventArgs e)
        {
            LoadMemberList();            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            _frmMemberDetail.InsertOrUpdate = false;
            _frmMemberDetail.Text = "Add Member";
            //_frmMemberDetail.MemberId = ((Member)source[e.RowIndex]).MemberID;
            _frmMemberDetail.FrmMemberManagement = this;
            if (_frmMemberDetail.ShowDialog() == DialogResult.OK)
            {
                //LoadMemberList();
                source.Position = source.Count - 1;
            }
        }

        private void dgvMemberList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            _frmMemberDetail.InsertOrUpdate = true;
            _frmMemberDetail.Text = "Update Member";
            _frmMemberDetail.MemberId = ((Member)source[e.RowIndex]).MemberID;
            _frmMemberDetail.FrmMemberManagement = this;
            if (_frmMemberDetail.ShowDialog() == DialogResult.OK)
            {
                //LoadMemberList();
                source.Position = source.Count - 1;
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Close();
            Dispose();
            FrmLogin.Dispose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var result = _memberRepository.DeleteMemberAsync(_rowIndex).Result;
                if (result == 1)
                    LoadMemberList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void dgvMemberList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _rowIndex = e.RowIndex;
        }

        private void rdMemberID_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMemberID.Checked)
            {
                _searchBy = SearchBy.ById;
            }
        }

        private void rdMemberName_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMemberName.Checked)
            {
                _searchBy = SearchBy.ByName;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var keyword = txtSearch.Text.Trim();
            if (keyword.Length > 0)
            {
                SearchMemberList(keyword, _searchBy);
            }
        }
        public void SearchMemberList(string? keyword, SearchBy searchBy)
        {
            // If Current user is admin (email contain admin@fstore.com and pwd contain admin@@)
            if (_authenticationService.IsAdmin())
            {
                foreach (var sysMem in _systemMembers)
                {
                    _memberRepository.CreateMemberAsync(sysMem);
                }

            }
            var members = _memberRepository.SearchMembersAsync(keyword, searchBy).Result.ToList();
            members = members.OrderBy(x => x.MemberID).ToList();
            try
            {
                source = new BindingSource();
                source.DataSource = members;
                txtMemberID.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                txtCountry.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtCity.DataBindings.Add("Text", source, "City");
                txtCountry.DataBindings.Add("Text", source, "Country");

                dgvMemberList.DataSource = null;
                dgvMemberList.DataSource = source;
                if (members.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }
    }
}
