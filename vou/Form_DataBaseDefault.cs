using MDIDemo.PublicClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDIDemo.vou
{
    public partial class Form_DataBaseDefault : DevExpress.XtraEditors.XtraForm
    {
        public Form_DataBaseDefault()
        {
            InitializeComponent();
            SetIniSkin("Metropolis Dark");
        }
        public Form_DataBaseDefault(string skinName)
        {
            InitializeComponent();
            SetIniSkin(skinName);
        }
        public Form_DataBaseDefault(Class_DataBaseConDefault class_DataBaseConDefaul)
        {
            this.class_DataBaseConDefault = class_DataBaseConDefaul;
            SetIniSkin("Metropolis Dark");
        }
        public string publicSkinName;
        public Class_DataBaseConDefault class_DataBaseConDefault;

        private void SetCompoment()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库默认连接设置";
        }
        private void SetIniSkin(string skinName)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
            SetCompoment();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}
