using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;

namespace FormDemo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();

            bool flag = false;
            System.Threading.Mutex hMutex = new System.Threading.Mutex(true, Application.ProductName, out flag);//进程间的同步
            bool b = hMutex.WaitOne(0, false);
            if (!flag)
            {
                MessageBox.Show("当前程序已在运行，请勿重复运行。");
                Environment.Exit(1);//退出程序 
            }
            try
            {
                //System.Diagnostics.Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory);
                LoginFrm loginFrm = new LoginFrm();
                loginFrm.StartPosition = FormStartPosition.CenterScreen;
                loginFrm.ShowDialog();

                if (loginFrm.DialogResult == DialogResult.OK)
                {
                    Application.Run(new FrmMain { WindowState=FormWindowState.Maximized});
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
    }
}
