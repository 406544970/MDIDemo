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
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.class_AllParamSetUp.AllPackageName = this.textEdit1.Text;
            this.class_AllParamSetUp.RemoteAddress = this.textEdit2.Text;
            this.class_AllParamSetUp.RemotePort = (int)this.spinEdit1.Value;

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
            }
        }
    }
}
