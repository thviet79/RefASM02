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
using DataAccess.Repository;

namespace MyStoreWinApp
{
    public partial class frmMemberDetails : Form
    {


        private readonly IMemberRepository _memberRepository;
        public frmMemberManagement FrmMemberManagement { get; set; }
        public int MemberId { set; get; }
        public bool InsertOrUpdate { get; set; }
        public frmMemberDetails(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
            InitializeComponent();
        }

        private void frmMemberDetails_Load(object sender, EventArgs e)
        {
            var member = _memberRepository.ViewMemberAsync(MemberId).Result;
                //cmbManufacturer.SelectedIndex = 0;
            txtMemberID.Enabled = !InsertOrUpdate;
            if (InsertOrUpdate == true && member != null)
            {
                txtMemberID.Text = member.MemberID.ToString();
                txtMemberName.Text = member.MemberName;
                txtEmail.Text = member.Email.ToString();
                txtPassword.Text = member.Password.ToString();
                txtCity.Text = member.City.ToString();
                txtCountry.Text = member.Country.ToString();

            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var member = new Member
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text
                };
                if (InsertOrUpdate == false)
                {
                    _ = _memberRepository.CreateMemberAsync(member).Result;
                    FrmMemberManagement.LoadMemberList();
                    return;
                }
                
                _ = _memberRepository.UpdateMemberAsync(member).Result;
                FrmMemberManagement.LoadMemberList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a new Member" : "Update a member");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();


        
    }
}
