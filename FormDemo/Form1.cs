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
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            byte[] headerBytes = new byte[2] { 0x12, 0x34 };
            byte[] bodyBytes = new byte[2] { 0x54, 0x93 };
            byte[] buffer = CommonToolkit.CombineSendBuffer(headerBytes, bodyBytes);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("version 2.0");
        }
    }
}
