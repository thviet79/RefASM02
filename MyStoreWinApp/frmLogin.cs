using System;
using System.Threading;
using System.Windows.Forms;
using BusinessObject.Interfaces;

namespace MyStoreWinApp
{
    public partial class frmLogin : Form
    {
        private readonly IAuthenticationService _authenticationService;
        private CancellationTokenSource cancellationSourceToken;
        private readonly frmMemberManagement _frmMemberManagement;

        public frmLogin(IAuthenticationService authenticationService, frmMemberManagement frmMemberManagement)
        {
            _authenticationService = authenticationService;
            InitializeComponent();
            _frmMemberManagement = frmMemberManagement;
        }

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtMemberEmail.Text.Trim();
            var password = txtPassword.Text.Trim();
            cancellationSourceToken = new CancellationTokenSource();
            var cancellationToken = cancellationSourceToken.Token;
            var isAuthenticated = _authenticationService.LoginAsync(email, password, cancellationToken).Result;
            if (!isAuthenticated)
            {
                MessageBox.Show("Incorrect email or password");
                return;
            }
            Hide();
            _frmMemberManagement.FrmLogin = this;
            _frmMemberManagement.ShowDialog();


        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (cancellationSourceToken != null && cancellationSourceToken.Token.CanBeCanceled)
            {
                cancellationSourceToken.Cancel();
            }
            Dispose();
        }
    }
}
