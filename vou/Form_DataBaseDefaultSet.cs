using MDIDemo.PublicClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDIDemo.vou
{
    public partial class Form_DataBaseDefaultSet : DevExpress.XtraEditors.XtraForm
    {
        public Form_DataBaseDefaultSet(Class_DataBaseConDefault class_DataBaseConDefault)
        {
            InitializeComponent();
            this.class_DataBaseConDefault = new Class_DataBaseConDefault();
            this.class_DataBaseConDefault = class_DataBaseConDefault;
            SetCompoment();
        }
        public Class_DataBaseConDefault class_DataBaseConDefault;
        private void SetCompoment()
        {
            this.Text = "数据库默认连接设置";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.class_DataBaseConDefault.dataBaseName = this.textEdit1.Text;
            this.class_DataBaseConDefault.dataSourceUrl = this.textEdit2.Text;
            this.class_DataBaseConDefault.dataSourceUserName = this.textEdit3.Text;
            this.class_DataBaseConDefault.dataSourcePassWord = this.textEdit4.Text;
            this.class_DataBaseConDefault.Port = Convert.ToInt32(this.spinEdit1.Text);
            switch (this.radioGroup7.SelectedIndex)
            {
                case 0:
                    this.class_DataBaseConDefault.databaseType = "MySql";
                    break;
                case 1:
                    this.class_DataBaseConDefault.databaseType = "SqlServer 2017";
                    break;
                default:
                    this.class_DataBaseConDefault.databaseType = "Oracle 11g";
                    break;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void Form_DataBaseDefaultSet_Load(object sender, EventArgs e)
        {
            if (this.class_DataBaseConDefault != null)
            {
                this.textEdit1.Text = this.class_DataBaseConDefault.dataBaseName;
                this.textEdit2.Text = this.class_DataBaseConDefault.dataSourceUrl;
                this.textEdit3.Text = this.class_DataBaseConDefault.dataSourceUserName;
                this.textEdit4.Text = this.class_DataBaseConDefault.dataSourcePassWord;
                this.spinEdit1.Value = Convert.ToInt32(this.class_DataBaseConDefault.Port);
                int Index = -1;
                switch (this.class_DataBaseConDefault.databaseType)
                {
                    case "MySql":
                        Index = 0;
                        break;
                    case "SqlServer 2017":
                        Index = 1;
                        break;
                    default:
                        Index = 2;
                        break;
                }
                this.radioGroup7.SelectedIndex = Index;
            }
        }
    }
}
