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
    public partial class Form_UpdatePass : DevExpress.XtraEditors.XtraForm
    {
        public Form_UpdatePass()
        {
            InitializeComponent();
            SetCompoment();
        }
        public string PassWord;
        private void SetCompoment()
        {
            this.Text = "修改密码";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string One = this.textEdit1.Text.Trim();
            string Two = this.textEdit2.Text.Trim();
            if (One == null || One.Length != 6)
            {
                MessageBox.Show("请输入6位密码！"
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Two == null || Two.Length != 6)
            {
                MessageBox.Show("请输入6位确认密码！"
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (One == Two)
            {
                PassWord = One;
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("相同的密码，请输入两次！"
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void Form_DataBaseDefaultSet_Load(object sender, EventArgs e)
        {
            this.textEdit1.Properties.PasswordChar = '*';
            this.textEdit2.Properties.PasswordChar = '*';
            this.textEdit1.Text = null;
            this.textEdit2.Text = null;
        }
    }
}
