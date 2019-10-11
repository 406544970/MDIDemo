using MDIDemo.PublicClass;
using MDIDemo.PublicSetUp;
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
    public partial class Form_AllSetUp : DevExpress.XtraEditors.XtraForm
    {
        public Form_AllSetUp(Class_AllParamSetUp class_AllParamSetUp)
        {
            InitializeComponent();
            this.class_AllParamSetUp = new Class_AllParamSetUp();
            if (class_AllParamSetUp != null)
                this.class_AllParamSetUp = class_AllParamSetUp;
            SetCompoment();
        }
        public Class_AllParamSetUp class_AllParamSetUp;
        private void SetCompoment()
        {
            this.Text = "综合参数设置";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            Class_SetButtonEdit class_SetButtonEdit = new Class_SetButtonEdit();
            class_SetButtonEdit.SetButtonEdit(this.buttonEdit1);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.class_AllParamSetUp.AllPackageName = this.textEdit1.Text;
            this.class_AllParamSetUp.RemoteAddress = this.textEdit2.Text;
            this.class_AllParamSetUp.RemotePort = (int)this.spinEdit1.Value;
            this.class_AllParamSetUp.HttpSign = this.radioGroup1.SelectedIndex == 0 ? true : false;
            this.class_AllParamSetUp.OutFileFolder = this.buttonEdit1.Text;

            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void Form_DataBaseDefaultSet_Load(object sender, EventArgs e)
        {
            if (this.class_AllParamSetUp != null)
            {
                this.textEdit1.Text = this.class_AllParamSetUp.AllPackageName;
                this.textEdit2.Text = this.class_AllParamSetUp.RemoteAddress;
                this.spinEdit1.Value = Convert.ToInt32(this.class_AllParamSetUp.RemotePort);
                this.radioGroup1.SelectedIndex = this.class_AllParamSetUp.HttpSign ? 0 : 1;
                if (this.class_AllParamSetUp.OutFileFolder != null && this.class_AllParamSetUp.OutFileFolder.Length > 0)
                {
                    this.buttonEdit1.Text = this.class_AllParamSetUp.OutFileFolder;
                    this.folderBrowserDialog1.SelectedPath = this.class_AllParamSetUp.OutFileFolder;
                }
                else
                {
                    this.buttonEdit1.Text = @"c:\";
                    this.folderBrowserDialog1.SelectedPath = @"c:\";
                }
            }
            else
            {
                this.spinEdit1.Value = 0;
                this.radioGroup1.SelectedIndex = 0;
            }
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.buttonEdit1.Text = this.folderBrowserDialog1.SelectedPath;
            }
            
        }
    }
}
