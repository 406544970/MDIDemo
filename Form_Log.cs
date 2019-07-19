using MDIDemo.PublicClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDIDemo
{
    public partial class Form_Log : DevExpress.XtraEditors.XtraForm
    {
        public Form_Log()
        {
            InitializeComponent();
            Ini();
        }
        private void setIniSkin(string skinName)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
            //barManager1.GetController().PaintStyleName = "Skin";
        }

        private void Ini()
        {
            //this.textEdit1.Text = "";
            //this.textEdit2.Text = "";
            setIniSkin("Metropolis Dark");
        }

        private void LogOk()
        {
            Class_UseInfo.UserId = "No1";
            Class_UseInfo.UserName = "梁昊";
            Class_UseInfo.UserClass = 3;
            this.DialogResult = DialogResult.OK;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LogOk();
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.textEdit2.Focus();
            }
        }

        private void textEdit2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                LogOk();
            }
        }
    }
}
