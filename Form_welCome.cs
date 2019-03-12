using DevExpress.DXperience.Demos;
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
    public partial class Form_welCome : DevExpress.XtraEditors.XtraForm
    {
        public Form_welCome(string skinName)
        {
            InitializeComponent();

            publicSkinName = skinName;
            SetCompoment();
        }
        public string publicSkinName;
        public DevExpress.XtraBars.Demos.MDIDemo.frmMain mainPage;
        private void SetCompoment()
        {
            mainPage = this.MdiParent as DevExpress.XtraBars.Demos.MDIDemo.frmMain;
            setIniSkin(publicSkinName);
        }
        private void setIniSkin(string skinName)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
        }

        private void Form_welCome_Load(object sender, EventArgs e)
        {

            //mainPage.displayState("asdfasdfas");
        }
    }
}
