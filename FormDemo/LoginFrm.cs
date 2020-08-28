using DevExpress.XtraEditors;
using System;
using System.Configuration;
using System.Windows.Forms;
using WLIDAR.BackEnd.UI;

namespace FormDemo
{
    public partial class LoginFrm : XtraForm
    {
        public LoginFrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            byte[] headerBytes = new byte[2] { 0x12, 0x34 };
            byte[] bodyBytes = new byte[2] { 0x54, 0x93 };
            byte[] buffer = CommonToolkit.CombineSendBuffer(headerBytes, bodyBytes);

            //获取当前程序的版本
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            //-----------先执行这个生成salt和hash
            //byte[] salt = UserWrap.GenerateSalt();

            //Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //configuration.AppSettings.Settings["salt"].Value= Convert.ToBase64String(salt);
            //configuration.AppSettings.Settings["hash"].Value = Convert.ToBase64String(UserWrap.ComputeHash("12345678", salt));
            //configuration.Save();
            //ConfigurationManager.RefreshSection("appSettings");

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string userName = user_txt.Text;
            string password = password_txt.Text;
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            byte[] passwordSalt = Convert.FromBase64String(configuration.AppSettings.Settings["salt"].Value);
            byte[] passwordHash = Convert.FromBase64String(configuration.AppSettings.Settings["hash"].Value);

            bool flag = UserWrap.VerifyPassword(password, passwordSalt, passwordHash);
            if (userName == "admin" && flag)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.None;
                XtraMessageBox.Show("用户名或密码错误!");
                return;
            }
        }
    }
}
