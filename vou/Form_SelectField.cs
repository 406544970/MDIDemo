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
    public partial class Form_SelectField : DevExpress.XtraEditors.XtraForm
    {
        public Form_SelectField(string skinName)
        {
            InitializeComponent();
            SetCompoment(skinName);
        }
        ~Form_SelectField()
        {
            dataTable.Dispose();
            class_LinkFields.Clear();
        }
        public string ParaName;
        public string ReturnType;
        public List<Class_LinkField> class_LinkFields;

        private DataTable dataTable;

        public void GetData()
        {
            dataTable = new DataTable();
            DataColumn ParaName = new DataColumn("ParaName", typeof(string));
            DataColumn ReturnType = new DataColumn("ReturnType", typeof(string));
            DataColumn FieldRemark = new DataColumn("FieldRemark", typeof(string));
            dataTable.Columns.Add(ParaName);
            dataTable.Columns.Add(ReturnType);
            dataTable.Columns.Add(FieldRemark);
            foreach(Class_LinkField row in class_LinkFields)
            {
                DataRow newRow = dataTable.NewRow();
                newRow["ParaName"] = row.ParaName.ToString();
                newRow["ReturnType"] = row.ReturnType.ToString();
                newRow["FieldRemark"] = row.FieldRemark.ToString();
                dataTable.Rows.Add(newRow);
            }
            this.gridControl1.DataSource = dataTable;
        }
        private void SetCompoment(string skinName)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
            this.StartPosition = FormStartPosition.CenterScreen;
            GridC gridC = new GridC();
            gridC.SetGridBar(this.gridControl1);
            GridViewC gridViewC = new GridViewC();
            gridViewC.SetGridView(this.gridView1);
            class_LinkFields = new List<Class_LinkField>();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int Index = this.gridView1.FocusedRowHandle;
            if (Index > -1)
            {
                DataRow row = this.gridView1.GetDataRow(Index);
                if (row != null)
                {
                    ParaName = row["ParaName"].ToString();
                    ReturnType = row["ReturnType"].ToString();
                }
                this.DialogResult = DialogResult.OK;
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
