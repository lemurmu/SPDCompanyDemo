using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WLIDAR.BackEnd.UI;

namespace FormDemo
{
    public partial class LoginFrm : DevExpress.XtraEditors.XtraForm
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

            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string userName = user_txt.Text;
            string password = password_txt.Text;
            if (userName == "admin"&& password=="12345678")
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
