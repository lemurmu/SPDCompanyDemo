namespace FormDemo
{
    partial class LoginFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginFrm));
            this.user_txt = new DevExpress.XtraEditors.TextEdit();
            this.password_txt = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.user_txt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.password_txt.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // user_txt
            // 
            this.user_txt.EditValue = "admin";
            this.user_txt.Location = new System.Drawing.Point(200, 88);
            this.user_txt.Name = "user_txt";
            this.user_txt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.user_txt.Size = new System.Drawing.Size(164, 18);
            this.user_txt.TabIndex = 0;
            // 
            // password_txt
            // 
            this.password_txt.EditValue = "12345678";
            this.password_txt.Location = new System.Drawing.Point(200, 124);
            this.password_txt.Name = "password_txt";
            this.password_txt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.password_txt.Properties.PasswordChar = '*';
            this.password_txt.Size = new System.Drawing.Size(164, 18);
            this.password_txt.TabIndex = 1;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(198, 158);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(61, 25);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "登录";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2016 Colorful";
            // 
            // LoginFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.ClientSize = new System.Drawing.Size(541, 255);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.password_txt);
            this.Controls.Add(this.user_txt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginFrm";
            this.Text = "myself智能办公平台";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.user_txt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.password_txt.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit user_txt;
        private DevExpress.XtraEditors.TextEdit password_txt;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
    }
}

