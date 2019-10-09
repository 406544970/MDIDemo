using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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

namespace MDIDemo
{
    public partial class Form_WindowSelect : DevExpress.XtraEditors.XtraForm
    {
        public Form_WindowSelect()
        {
            InitializeComponent();
            this.SelectDes = false;
        }
        public Form_WindowSelect(bool SelectDes)
        {
            InitializeComponent();
            this.SelectDes = SelectDes;
        }

        public string NewPageType;
        private bool SelectDes;
        private const string FileFullName = "SystemDefault";
        public string PageKey;
        public string PageType;
        public string OperateType;
        private Class_PublicMethod class_PublicMethod;
        private void SetCompoment()
        {
            this.panelControl2.Visible = this.SelectDes;
            class_PublicMethod = new Class_PublicMethod();
            this.Text += "---" + OperateType;
            memoEdit1.ReadOnly = true;
            GridC gridC = new GridC();
            gridC.SetGridBar(this.gridControl1);
            gridC.SetGridBar(this.gridControl2);
            gridC.SetGridBar(this.gridControl3);
            gridC.SetGridBar(this.gridControl4);
            foreach (GridColumn grid in this.gridView1.Columns)
            {
                GridColumn gridColumn = new GridColumn();
                gridColumn = grid;
                this.gridView2.Columns.Add(gridColumn);
                this.gridView3.Columns.Add(gridColumn);
                this.gridView4.Columns.Add(gridColumn);
            }
            GridViewC gridViewC = new GridViewC();
            gridViewC.SetGridView(this.gridView1, true);
            gridViewC.SetGridView(this.gridView2, true);
            gridViewC.SetGridView(this.gridView3, true);
            gridViewC.SetGridView(this.gridView4, true);

            Class_SQLiteOperator class_SQLiteOperator = new Class_SQLiteOperator();
            DataSet dataSet = new DataSet();
            dataSet = class_SQLiteOperator.GetAllWindowInfomation();
            this.gridControl1.DataSource = dataSet.Tables[0];
            this.gridControl2.DataSource = dataSet.Tables[1];
            this.gridControl3.DataSource = dataSet.Tables[2];
            this.gridControl4.DataSource = dataSet.Tables[3];
            this.xtraTabControl1.SelectedTabPageIndex = 0;
            this.gridView1.Focus();

            Class_SystemDefault class_SystemDefaul = class_PublicMethod.FromXmlToSystemDefaultObject<Class_SystemDefault>(FileFullName);
            if (class_SystemDefaul != null)
                this.xtraTabControl1.SelectedTabPageIndex = class_SystemDefaul.SelectOpenWindowIndex;
        }
        private void SelectPageKey()
        {
            GridView gridView;
            switch (this.xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    PageType = "select";
                    gridView = this.gridView1;
                    break;
                case 1:
                    PageType = "insert";
                    gridView = this.gridView2;
                    break;
                case 2:
                    PageType = "update";
                    gridView = this.gridView3;
                    break;
                case 3:
                    PageType = "delete";
                    gridView = this.gridView4;
                    break;
                default:
                    PageType = "select";
                    gridView = this.gridView1;
                    break;
            }
            if (gridView.RowCount > 0)
            {
                int Index = gridView.FocusedRowHandle;
                if (Index > -1)
                {
                    PageKey = gridView.GetRowCellValue(Index, "pageKey").ToString();
                    Class_SystemDefault class_SystemDefault = new Class_SystemDefault();
                    class_SystemDefault.SelectOpenWindowIndex = this.xtraTabControl1.SelectedTabPageIndex;
                    if (class_PublicMethod.SystemDefaultValueToXml(FileFullName, class_SystemDefault))
                    {
                        switch (this.radioGroup1.SelectedIndex)
                        {
                            case 0:
                                this.NewPageType = "select";
                                break;
                            case 1:
                                this.NewPageType = "insert";
                                break;
                            case 2:
                                this.NewPageType = "update";
                                break;
                            case 3:
                                this.NewPageType = "delete";
                                break;
                            default:
                                this.NewPageType = "select";
                                break;
                        }
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                    MessageBox.Show("请选择一条数据", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("无删除数据！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SelectPageKey();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.Cancel;
        }

        private void Form_WindowSelect_Load(object sender, EventArgs e)
        {
            SetCompoment();
        }
        private void DisplayRemark(object sender)
        {
            GridView gridView = sender as GridView;
            int Index = gridView.FocusedRowHandle;
            if (Index > -1)
            {
                DataRow dr = gridView.GetDataRow(Index);
                memoEdit1.Text = dr["methodRemark"].ToString();
            }
            else
                memoEdit1.Text = null;
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayRemark(sender);
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (this.SelectDes)
            {
                this.radioGroup1.SelectedIndex = xtraTabControl1.SelectedTabPageIndex;
            }
        }
    }
}
