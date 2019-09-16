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
            this.class_DataBaseConDefault = new Class_DataBaseConDefault();
            this.class_DataBaseConDefault = class_DataBaseConDefaul;
            SetIniSkin("Metropolis Dark");
        }
        public string publicSkinName;
        public Class_DataBaseConDefault class_DataBaseConDefault;

        private void SetCompoment()
        {
            this.Text = "数据库默认连接设置";
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
        private void SetIniSkin(string skinName)
        {
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
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

        private void Form_DataBaseDefault_Load(object sender, EventArgs e)
        {
            //if (this.class_DataBaseConDefault != null)
            //{
            //    this.textEdit1.Text = this.class_DataBaseConDefault.dataBaseName;
            //    this.textEdit2.Text = this.class_DataBaseConDefault.dataSourceUrl;
            //    this.textEdit3.Text = this.class_DataBaseConDefault.dataSourceUserName;
            //    this.textEdit4.Text = this.class_DataBaseConDefault.dataSourcePassWord;
            //    this.spinEdit1.Value = Convert.ToInt32(this.class_DataBaseConDefault.Port);
            //}
        }
    }
}
