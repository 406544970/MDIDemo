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
    public partial class Form_SelectCaseWhen : DevExpress.XtraEditors.XtraForm
    {
        public Form_SelectCaseWhen()
        {
            InitializeComponent();
            SetCompoment();
        }
        public string CaseWhenId;
        private void SetCompoment()
        {
            setIniSkin("Metropolis Dark");
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            GridC gridC = new GridC();
            gridC.SetGridBar(this.gridControl1);
            gridC.SetGridBar(this.gridControl2);
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.simpleButton1.Text = "确定";
            this.simpleButton2.Text = "取消";
            CaseWhenId = null;

            Class_CaseWhen class_CaseWhen = new Class_CaseWhen();
            this.gridControl1.DataSource = class_CaseWhen.GetCaseMainList();
        }
        private void setIniSkin(string skinName)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int Index = this.gridView1.FocusedRowHandle;
            if (Index > -1)
            {
                DataRow row = this.gridView1.GetDataRow(Index);
                CaseWhenId = row["id"].ToString();
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("请选择CASEWHEN编号");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
