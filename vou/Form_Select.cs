using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using MDIDemo.PublicClass;
using MDIDemo.PublicSetUp;
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
    public partial class Form_Select : DevExpress.XtraEditors.XtraForm
    {
        public Form_Select(string skinName)
        {
            _iniSelect(skinName, null);
        }
        public Form_Select(string skinName, string xmlFileName)
        {
            _iniSelect(skinName, xmlFileName);
        }

        private Class_SelectAllModel class_SelectAllModel;
        private Class_InterFaceDataBase class_InterFaceDataBase;
        private List<string> myTableNameList;
        private Class_PublicMethod class_PublicMethod;
        private string MainKeyFieldName;

        private void _iniSelect(string skinName, string xmlFileName)
        {
            InitializeComponent();

            publicSkinName = skinName;
            class_PublicMethod = new Class_PublicMethod();
            SetCompoment();
            this.listBoxControl1.Items.Clear();
            #region mybatisMap文件配置
            this.propertyGridControl5.OptionsBehavior.UseDefaultEditorsCollection = true;
            this.propertyGridControl5.LayoutChanged();
            this.propertyDescriptionControl5.PropertyGrid = this.propertyGridControl5;
            #endregion

            #region 数据库配置
            this.propertyGridControl3.OptionsBehavior.UseDefaultEditorsCollection = true;
            this.propertyGridControl3.LayoutChanged();
            this.propertyDescriptionControl3.PropertyGrid = this.propertyGridControl3;
            #endregion

            #region 生成配置
            this.propertyGridControl4.OptionsBehavior.UseDefaultEditorsCollection = true;
            this.propertyGridControl4.LayoutChanged();
            this.propertyDescriptionControl4.PropertyGrid = this.propertyGridControl4;
            #endregion

            class_SelectAllModel = new Class_SelectAllModel();
            SetSelectAllMode(xmlFileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlFileName"></param>
        public void SetSelectAllMode(string xmlFileName)
        {
            if (xmlFileName != null)
                class_SelectAllModel = class_PublicMethod.FromXmlToSelectObject<Class_SelectAllModel>(xmlFileName);
            if (class_SelectAllModel == null)
                class_SelectAllModel = new Class_SelectAllModel();
            switch (class_SelectAllModel.class_SelectDataBase.databaseType)
            {
                case "MySql":
                    class_InterFaceDataBase = new Class_MySqlDataBase(class_SelectAllModel.class_SelectDataBase.dataSourceUrl, class_SelectAllModel.class_SelectDataBase.dataBaseName, class_SelectAllModel.class_SelectDataBase.dataSourceUserName, class_SelectAllModel.class_SelectDataBase.dataSourcePassWord, class_SelectAllModel.class_SelectDataBase.Port);
                    break;
                case "SqlServer 2017":
                    class_InterFaceDataBase = new Class_SqlServer2017DataBase(class_SelectAllModel.class_SelectDataBase.dataSourceUrl, class_SelectAllModel.class_SelectDataBase.dataBaseName, class_SelectAllModel.class_SelectDataBase.dataSourceUserName, class_SelectAllModel.class_SelectDataBase.dataSourcePassWord);
                    break;
                default:
                    break;
            }
            class_InterFaceDataBase.SetClass_AllModel(class_SelectAllModel);
            this.textEdit13.Text = class_SelectAllModel.AllPackerName;
            this.checkEdit2.Checked = class_SelectAllModel.IsAutoWard;
            this.memoEdit12.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.TestUnit);
            this.textEdit21.Text = class_SelectAllModel.TestClassName;

            this.xtraTabControl1.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl1;
            this.xtraTabControl3.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl3;
            this.xtraTabControl5.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl5;
            this.xtraTabControl8.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl8;
            this.xtraTabControl6.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl6;
            this.xtraTabControl7.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl7;
            this.xtraTabControl9.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl9;

            #region 主表
            this.textEdit14.Text = class_SelectAllModel.class_Main.MethodId;
            this.textEdit15.Text = class_SelectAllModel.class_Main.MethodContent;
            this.radioGroup7.SelectedIndex = class_SelectAllModel.class_Main.ResultType;
            this.radioGroup8.SelectedIndex = class_SelectAllModel.class_Main.ParameterType;
            this.checkEdit1.Checked = class_SelectAllModel.class_Main.IsAddXmlHead;
            this.textEdit16.Text = class_SelectAllModel.class_Main.NameSpace;
            this.memoEdit3.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_Main.MapContent);
            this.memoEdit4.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_Main.SelectContent);
            this.memoEdit5.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_Main.ServiceInterFaceContent);
            this.memoEdit6.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_Main.ServiceImplContent);
            this.memoEdit8.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_Main.ModelContent);
            this.memoEdit9.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_Main.DTOContent);
            this.memoEdit10.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_Main.DAOContent);
            this.memoEdit11.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_Main.ControlContent);
            this.textEdit22.Text = class_SelectAllModel.class_Main.ResultMapId;
            this.textEdit24.Text = class_SelectAllModel.class_Main.ResultMapType;
            this.textEdit17.Text = class_SelectAllModel.class_Create.MethodId;
            this.textEdit20.Text = class_SelectAllModel.class_Main.ServiceInterFaceReturnRemark;
            this.radioGroup9.SelectedIndex = class_SelectAllModel.class_Main.ServiceInterFaceReturnCount;
            #endregion

            if (class_SelectAllModel != null)
            {
                this.propertyGridControl3.SelectedObject = class_SelectAllModel.class_SelectDataBase;
                this.propertyGridControl4.SelectedObject = class_SelectAllModel.class_Create;
                this.propertyGridControl5.SelectedObject = class_SelectAllModel.class_MyBatisMap;
                this.textEdit13.Text = class_SelectAllModel.AllPackerName;
                if (xmlFileName != null)
                {
                    _getUseTable(false);
                    if (class_SelectAllModel.class_Main != null)
                    {
                        this.AddUseTableData(class_SelectAllModel.class_Main.TableName, 0);
                    }
                    if (class_SelectAllModel.class_Subs != null)
                    {
                        this.AddUseTableData(class_SelectAllModel.class_Subs.TableName, 1);
                    }
                    if (class_SelectAllModel.class_SubSubs != null)
                    {
                        this.AddUseTableData(class_SelectAllModel.class_SubSubs.TableName, 2);
                    }
                }
            }
        }
        public string publicSkinName;
        private void SetCompoment()
        {
            Class_SetUpBar class_SetUpBar = new Class_SetUpBar();
            class_SetUpBar.setBar(this.bar2, "提示操作");
            class_SetUpBar.setBar(this.bar1, "提示操作");
            class_SetUpBar.setBar(this.bar3, "提示操作");
            class_SetUpBar.setBar(this.bar4, "提示操作");

            setIniSkin(publicSkinName);
            xtraTabControl3.SelectedTabPageIndex = 0;
            xtraTabControl4.SelectedTabPageIndex = 0;
            xtraTabControl5.SelectedTabPageIndex = 0;

            this.bandedGridColumn1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            myTableNameList = new List<string>();

            GridC gridC = new GridC();
            gridC.pub_SetBandedGridViewStyle(this.bandedGridView1);
            gridC.pub_SetBandedGridViewStyle(this.bandedGridView2);
            gridC.pub_SetBandedGridViewStyle(this.bandedGridView3);
            gridC.SetGridBar(this.gridControl1);
            gridC.SetGridBar(this.gridControl2);
            gridC.SetGridBar(this.gridControl3);
            radioGroup1.SelectedIndex = 0;
            radioGroup2.SelectedIndex = 0;
            radioGroup3.SelectedIndex = 0;
            radioGroup4.SelectedIndex = 0;

            Class_SetMemoEdit class_SetMemoEdit = new Class_SetMemoEdit();
            class_SetMemoEdit.SetMemoEdit(this.memoEdit1);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit2);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit3);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit4);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit5);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit6);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit7);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit8);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit9);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit10);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit11);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit12);

            Class_SetTextEdit class_SetTextEdit = new Class_SetTextEdit();
            class_SetTextEdit.SetTextEdit(this.textEdit18);
            class_SetTextEdit.SetTextEdit(this.textEdit25);
            class_SetTextEdit.SetTextEdit(this.textEdit23);
            class_SetTextEdit.SetTextEdit(this.textEdit13, Color.Yellow);
            class_SetTextEdit.SetTextEdit(this.textEdit10, Color.Yellow);
            class_SetTextEdit.SetTextEdit(this.textEdit11, Color.Yellow);
            class_SetTextEdit.SetTextEdit(this.textEdit12, Color.Yellow);
            class_SetTextEdit.SetTextEdit(this.textEdit14, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit15, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit16, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit19, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit20, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit22, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit24, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit17, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit1, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit2, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit3, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit4, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit5, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit6, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit7, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit8, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit9, true, Color.GreenYellow);

            this.xtraTabControl8.Images = this.xtraTabControl5.Images;
            for (int index = 0; index < this.xtraTabControl8.TabPages.Count; index++)
            {
                if (index < 3)
                {
                    this.xtraTabControl8.TabPages[index].Text = this.xtraTabControl5.TabPages[index].Text;
                    this.xtraTabControl8.TabPages[index].ImageIndex = this.xtraTabControl5.TabPages[index].ImageIndex;
                }
                else
                {
                    this.xtraTabControl8.TabPages[index].Text = "测试单元";
                    this.xtraTabControl8.TabPages[index].ImageIndex = 374;
                }
            }

        }

        private void setIniSkin(string skinName)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
        }

        private void Form_Select_Shown(object sender, EventArgs e)
        {
            this.dockPanel1.Size = new System.Drawing.Size(295, 288);
            this.dockPanel2.Size = new System.Drawing.Size(314, 288);
            //this.dockPanel3.Size = new System.Drawing.Size(552, 266);
        }
        private void AddColumnComboxHavingFunctionByDataType(RepositoryItemComboBox repositoryItemComboBox, string FieldType)
        {
            repositoryItemComboBox.Items.Clear();
            foreach (string row in class_InterFaceDataBase.GetHavingFuctionList(FieldType))
            {
                repositoryItemComboBox.Items.Add(row);
            }
        }
        private void AddColumnComboxFunctionByDataType(RepositoryItemComboBox repositoryItemComboBox, string FieldType)
        {
            repositoryItemComboBox.Items.Clear();
            foreach (string row in class_InterFaceDataBase.GetFunctionList(FieldType))
            {
                repositoryItemComboBox.Items.Add(row);
            }
        }
        private void AddComumnCombox(ComboBoxEdit comboBoxEdit, List<string> vs)
        {
            comboBoxEdit.Properties.Items.Clear();
            foreach (string item in vs)
            {
                comboBoxEdit.Properties.Items.Add(item);
            }
            comboBoxEdit.SelectedIndex = -1;
        }
        private void AddColumnRepositoryCombox(RepositoryItemComboBox repositoryItemComboBox)
        {
            repositoryItemComboBox.Items.Clear();
            foreach (string row in class_InterFaceDataBase.GetDataType())
            {
                repositoryItemComboBox.Items.Add(row);
            }
        }
        private void AddUseTableData(string TableName, int PageSelectIndex)
        {
            switch (PageSelectIndex)
            {
                case 0:
                    {
                        this.gridControl1.DataSource = class_InterFaceDataBase.GetMainTableStruct(TableName, PageSelectIndex);
                        int KeyIndex = 0;
                        while (KeyIndex < this.bandedGridView1.RowCount)
                        {
                            DataRow row = this.bandedGridView1.GetDataRow(KeyIndex++);
                            if (Convert.ToBoolean(row["FieldIsKey"]))
                            {
                                if (this.MainKeyFieldName != null)
                                    this.MainKeyFieldName = row["FieldName"].ToString();
                                break;
                            }
                        }
                        textEdit1.Text = TableName;
                        if (class_SelectAllModel.class_Main.AliasName != null)
                            textEdit10.Text = class_SelectAllModel.class_Main.AliasName;
                        else
                            textEdit10.Text = "main";
                        AddColumnRepositoryCombox(this.repositoryItemComboBox2);
                        AddColumnComboxFunctionByDataType(this.repositoryItemComboBox1, "");
                        AddColumnComboxHavingFunctionByDataType(this.repositoryItemComboBox7, "");
                        this.xtraTabControl5.TabPages[PageSelectIndex].Text = string.Format("主表：{0}", TableName);
                        this.xtraTabControl8.TabPages[PageSelectIndex].Text = string.Format("主表：{0}", TableName);
                    }
                    break;
                case 1:
                    {
                        this.gridControl2.DataSource = class_InterFaceDataBase.GetMainTableStruct(TableName, PageSelectIndex);
                        int KeyIndex = 0;
                        List<string> vs = new List<string>();
                        while (KeyIndex < this.bandedGridView2.RowCount)
                        {
                            DataRow row = this.bandedGridView2.GetDataRow(KeyIndex++);
                            vs.Add(row["FieldName"].ToString());
                        }
                        if (vs != null)
                            AddComumnCombox(this.comboBoxEdit1, vs);
                        vs.Clear();
                        textEdit6.Text = TableName;
                        if (class_SelectAllModel.class_Subs.AliasName != null)
                            textEdit11.Text = class_SelectAllModel.class_Subs.AliasName;
                        else
                            textEdit11.Text = "sub";
                        this.radioGroup1.SelectedIndex = class_SelectAllModel.class_Subs.LinkType;
                        this.radioGroup3.SelectedIndex = class_SelectAllModel.class_Subs.CountToCount;
                        this.comboBoxEdit1.Text = class_SelectAllModel.class_Subs.OutFieldName;
                        AddColumnRepositoryCombox(this.repositoryItemComboBox10); 
                        AddColumnComboxFunctionByDataType(this.repositoryItemComboBox15, "");
                        AddColumnComboxHavingFunctionByDataType(this.repositoryItemComboBox15, "");
                        this.xtraTabControl5.TabPages[PageSelectIndex].Text = string.Format("从表一：{0}", TableName);
                        this.xtraTabControl8.TabPages[PageSelectIndex].Text = string.Format("从表一：{0}", TableName);
                    }
                    break;
                case 2:
                    {
                        this.gridControl3.DataSource = class_InterFaceDataBase.GetMainTableStruct(TableName, PageSelectIndex);
                        int KeyIndex = 0;
                        List<string> vs = new List<string>();
                        while (KeyIndex < this.bandedGridView3.RowCount)
                        {
                            DataRow row = this.bandedGridView3.GetDataRow(KeyIndex++);
                            vs.Add(row["FieldName"].ToString());
                        }
                        if (vs != null)
                            AddComumnCombox(this.comboBoxEdit2, vs);
                        vs.Clear();
                        textEdit9.Text = TableName;
                        if (class_SelectAllModel.class_SubSubs.AliasName != null)
                            textEdit12.Text = class_SelectAllModel.class_SubSubs.AliasName;
                        else
                            textEdit12.Text = "subsub";

                        this.radioGroup2.SelectedIndex = class_SelectAllModel.class_SubSubs.LinkType;
                        this.radioGroup4.SelectedIndex = class_SelectAllModel.class_Subs.CountToCount;
                        this.comboBoxEdit2.Text = class_SelectAllModel.class_SubSubs.OutFieldName;
                        AddColumnComboxFunctionByDataType(this.repositoryItemComboBox1, "");
                        AddColumnComboxHavingFunctionByDataType(this.repositoryItemComboBox7, "");
                        this.xtraTabControl5.TabPages[PageSelectIndex].Text = string.Format("从表二：{0}", TableName);
                        this.xtraTabControl8.TabPages[PageSelectIndex].Text = string.Format("从表二：{0}", TableName);
                    }
                    break;
                default:
                    break;
            }
            //if (PageSelectIndex > -1)
            //    this.xtraTabControl5.SelectedTabPageIndex = PageSelectIndex;
        }
        private void ToMain()
        {
            ToMain(null);
        }
        private void ToMain(string TableName)
        {
            try
            {
                if (TableName == null)
                {
                    int Index = this.listBoxControl1.SelectedIndex;
                    if (Index > -1)
                    {
                        AddUseTableData(this.listBoxControl1.Text, 0);
                    }
                }
                else
                {
                    AddUseTableData(TableName, 0);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void ToSub()
        {
            ToSub(null);
        }
        private void ToSub(string TableName)
        {
            try
            {
                this.xtraTabControl5.SelectedTabPageIndex = 1;
                //labelControl1.Visible = false;
                if (TableName == null)
                {
                    int Index = this.listBoxControl1.SelectedIndex;
                    if (Index > -1)
                    {
                        AddUseTableData(this.listBoxControl1.Text, 1);
                    }
                }
                else
                {
                    AddUseTableData(TableName, 1);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void ToSubSub()
        {
            ToSubSub(null);
        }
        private void ToSubSub(string TableName)
        {
            try
            {
                this.xtraTabControl5.SelectedTabPageIndex = 2;
                //labelControl1.Visible = false;
                if (TableName == null)
                {
                    int Index = this.listBoxControl1.SelectedIndex;
                    if (Index > -1)
                    {
                        AddUseTableData(this.listBoxControl1.Text, 2);
                    }
                }
                else
                {
                    AddUseTableData(TableName, 1);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void ToUseTable()
        {
            WaitDialogForm waitDialogForm = new WaitDialogForm("正在玩命加载中......", "温馨提示");
            switch (this.xtraTabControl5.SelectedTabPageIndex)
            {
                case 0:
                    ToMain();
                    break;
                case 1:
                    ToSub();
                    break;
                case 2:
                    ToSubSub();
                    break;
                default:
                    break;
            }
            waitDialogForm.Close();
        }
        private void listBoxControl1_DoubleClick(object sender, EventArgs e)
        {
            ToUseTable();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.memoEdit1.Text = null;
        }
        private void _getUseTable(bool ReSet)
        {
            if (ReSet)
            {
                switch (class_SelectAllModel.class_SelectDataBase.databaseType)
                {
                    case "MySql":
                        class_InterFaceDataBase = new Class_MySqlDataBase(class_SelectAllModel.class_SelectDataBase.dataSourceUrl, class_SelectAllModel.class_SelectDataBase.dataBaseName, class_SelectAllModel.class_SelectDataBase.dataSourceUserName, class_SelectAllModel.class_SelectDataBase.dataSourcePassWord, class_SelectAllModel.class_SelectDataBase.Port);
                        break;
                    case "SqlServer 2017":
                        class_InterFaceDataBase = new Class_SqlServer2017DataBase(class_SelectAllModel.class_SelectDataBase.dataSourceUrl, class_SelectAllModel.class_SelectDataBase.dataBaseName, class_SelectAllModel.class_SelectDataBase.dataSourceUserName, class_SelectAllModel.class_SelectDataBase.dataSourcePassWord);
                        break;
                    default:
                        break;
                }
                class_InterFaceDataBase.SetClass_AllModel(class_SelectAllModel);
            }
            if (listBoxControl1.ItemCount > 0)
                listBoxControl1.Items.Clear();
            myTableNameList.Clear();
            myTableNameList = class_InterFaceDataBase.GetUseTableList();
            foreach (string row in myTableNameList)
            {
                this.listBoxControl1.Items.Add(row);
            }
            if (class_SelectAllModel.LastSelectTableName != null)
            {
                this.listBoxControl1.SelectedIndex = myTableNameList.IndexOf(class_SelectAllModel.LastSelectTableName);
            }
            else
            {
                this.listBoxControl1.SelectedIndex = -1;
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.simpleButton1.Enabled = false;
            _getUseTable(true);
            this.simpleButton1.Enabled = true;
        }

        private void bandedGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int Index = e.FocusedRowHandle;
            if (Index > -1)
            {
                DataRow row = (sender as BandedGridView).GetDataRow(Index);
                if (row != null)
                {
                    this.textEdit2.Text = row["FieldName"].ToString();
                    this.textEdit3.Text = row["FieldRemark"].ToString();
                }
            }
        }
        private void SelectCaseWhen(object sender)
        {
            Form_SelectCaseWhen form_SelectCaseWhen = new Form_SelectCaseWhen();
            if (form_SelectCaseWhen.ShowDialog() == DialogResult.OK)
            {
                if (form_SelectCaseWhen.CaseWhenId != null)
                {
                    GridView gridView;
                    switch ((sender as ButtonEdit).Name)
                    {
                        case "repositoryItemButtonEdit1":
                            gridView = this.gridControl1.DefaultView as GridView;
                            break;
                        default:
                            gridView = this.gridControl1.DefaultView as GridView;
                            break;
                    }
                    gridView.SetFocusedRowCellValue("CaseWhen", form_SelectCaseWhen.CaseWhenId);
                }
            }
            form_SelectCaseWhen.Close();
            form_SelectCaseWhen.Dispose();
        }
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SelectCaseWhen(sender);
        }
        private Class_SelectAllModel.Class_Sub DataViewIntoClass(BandedGridView bandedGridView,
            string OutFieldName, int LinkType, int CountToCount, string TableName,
            string MainFieldName, bool AddPoint, string AliasName)
        {
            Class_SelectAllModel.Class_Sub class_Sub = new Class_SelectAllModel.Class_Sub();
            List<Class_SelectAllModel.Class_Field> class_Fields = new List<Class_SelectAllModel.Class_Field>();
            for (int j = 0; j < bandedGridView.RowCount; j++)
            {
                DataRow dataRow = bandedGridView.GetDataRow(j);
                if (Convert.ToBoolean(dataRow["FieldIsKey"]))
                {
                    class_SelectAllModel.class_Main.MainFieldName = dataRow["FieldName"].ToString();
                    class_SelectAllModel.class_Main.AddPoint = class_InterFaceDataBase.IsAddPoint(dataRow["FieldType"].ToString());
                }
                bool SelectSelect = Convert.ToBoolean(dataRow["SelectSelect"]);
                bool WhereSelect = Convert.ToBoolean(dataRow["WhereSelect"]);
                bool OrderSelect = Convert.ToBoolean(dataRow["OrderSelect"]);
                bool GroupSelect = Convert.ToBoolean(dataRow["GroupSelect"]);
                bool HavingSelect = Convert.ToBoolean(dataRow["HavingSelect"]);

                if (SelectSelect || WhereSelect || OrderSelect || GroupSelect || HavingSelect)
                {
                    Class_SelectAllModel.Class_Field class_Field = new Class_SelectAllModel.Class_Field();
                    class_Field.FieldName = dataRow["FieldName"].ToString();
                    class_Field.FieldRemark = dataRow["FieldRemark"].ToString();
                    class_Field.FieldType = dataRow["FieldType"].ToString();
                    if ((dataRow["FieldLength"] != null) && (dataRow["FieldLength"].ToString().Length > 0))
                        class_Field.FieldLength = Convert.ToInt32(dataRow["FieldLength"]);
                    class_Field.FieldDefaultValue = dataRow["FieldDefaultValue"].ToString();
                    class_Field.FieldIsNull = Convert.ToBoolean(dataRow["FieldIsNull"]);
                    class_Field.FieldIsKey = Convert.ToBoolean(dataRow["FieldIsKey"]);
                    class_Field.FieldIsAutoAdd = Convert.ToBoolean(dataRow["FieldIsAutoAdd"]);
                    class_Field.SelectSelect = SelectSelect;
                    class_Field.ParaName = dataRow["ParaName"].ToString();
                    if ((dataRow["MaxLegth"] != null) && (dataRow["MaxLegth"].ToString().Length > 0))
                        class_Field.MaxLegth = Convert.ToInt32(dataRow["MaxLegth"]);
                    class_Field.CaseWhen = dataRow["CaseWhen"].ToString();
                    class_Field.ReturnType = dataRow["ReturnType"].ToString();
                    class_Field.TrimSign = Convert.ToBoolean(dataRow["TrimSign"]);
                    class_Field.FunctionName = dataRow["FunctionName"].ToString();
                    class_Field.WhereSelect = WhereSelect;
                    class_Field.WhereType = dataRow["WhereType"].ToString();
                    class_Field.LogType = dataRow["LogType"].ToString();
                    class_Field.WhereValue = dataRow["WhereValue"].ToString();
                    class_Field.WhereTrim = Convert.ToBoolean(dataRow["WhereTrim"]);
                    class_Field.WhereIsNull = Convert.ToBoolean(dataRow["WhereIsNull"]);
                    class_Field.OrderSelect = OrderSelect;
                    class_Field.SortType = dataRow["SortType"].ToString();
                    class_Field.SortNo = Convert.ToInt32(dataRow["SortNo"]);
                    class_Field.GroupSelect = GroupSelect;
                    class_Field.HavingSelect = HavingSelect;
                    class_Field.HavingFunction = dataRow["HavingFunction"].ToString();
                    class_Field.HavingCondition = dataRow["HavingCondition"].ToString();
                    class_Field.HavingValue = dataRow["HavingValue"].ToString();

                    class_Fields.Add(class_Field);
                }
            }
            class_Sub.class_Fields = class_Fields;
            class_Sub.TableName = TableName;
            class_Sub.AddPoint = false;
            class_Sub.AliasName = AliasName;
            class_Sub.OutFieldName = OutFieldName;
            class_Sub.LinkType = LinkType;
            if (CountToCount > -1)
            {
                class_Sub.CountToCount = CountToCount;
            }
            class_Sub.MainFieldName = MainFieldName;


            return class_Sub;
        }
        private bool _CheckToXml()
        {
            return true;
        }
        private void _SaveSelectToXml(bool IsDisplayLog)
        {
            if (this.listBoxControl1.SelectedIndex > -1)
                class_SelectAllModel.LastSelectTableName = this.listBoxControl1.SelectedValue.ToString();
            if (class_SelectAllModel.class_Create.MethodId == null)
            {
                class_SelectAllModel.class_Create.MethodId = Class_Tool.getKeyId("MM");
                this.Text = string.Format("SELECT：{0}", class_SelectAllModel.class_Create.MethodId);
                this.Tag = class_SelectAllModel.class_Create.MethodId;
            }
            class_SelectAllModel.class_Create.DateTime = System.DateTime.Now;
            if (this.gridControl1.MainView.RowCount > 0)
                class_SelectAllModel.class_Main = DataViewIntoClass((BandedGridView)this.gridControl1.MainView
                    , null, 0, -1, this.textEdit1.Text
                    , this.MainKeyFieldName, false, this.textEdit10.Text);
            if (this.gridControl2.MainView.RowCount > 0)
                class_SelectAllModel.class_Subs = DataViewIntoClass((BandedGridView)this.gridControl2.MainView
                    , comboBoxEdit1.Text, this.radioGroup1.SelectedIndex, this.radioGroup3.SelectedIndex, this.textEdit6.Text
                    , null, false, this.textEdit11.Text);
            if (this.gridControl3.MainView.RowCount > 0)
                class_SelectAllModel.class_SubSubs = DataViewIntoClass((BandedGridView)this.gridControl3.MainView
                    , comboBoxEdit2.Text, this.radioGroup2.SelectedIndex, this.radioGroup4.SelectedIndex, this.textEdit9.Text
                    , null, false, this.textEdit12.Text);
            class_SelectAllModel.AllPackerName = this.textEdit13.Text;
            class_SelectAllModel.IsAutoWard = this.checkEdit2.Checked;
            class_SelectAllModel.TestUnit = Class_Tool.EscapeCharacter(this.memoEdit12.Text);
            class_SelectAllModel.TestClassName = this.textEdit21.Text;

            class_SelectAllModel.class_WindowLastState.xtraTabControl1 = this.xtraTabControl1.SelectedTabPageIndex;
            class_SelectAllModel.class_WindowLastState.xtraTabControl3 = this.xtraTabControl3.SelectedTabPageIndex;
            class_SelectAllModel.class_WindowLastState.xtraTabControl5 = this.xtraTabControl5.SelectedTabPageIndex;
            class_SelectAllModel.class_WindowLastState.xtraTabControl8 = this.xtraTabControl8.SelectedTabPageIndex;
            class_SelectAllModel.class_WindowLastState.xtraTabControl6 = this.xtraTabControl6.SelectedTabPageIndex;
            class_SelectAllModel.class_WindowLastState.xtraTabControl7 = this.xtraTabControl7.SelectedTabPageIndex;
            class_SelectAllModel.class_WindowLastState.xtraTabControl9 = this.xtraTabControl9.SelectedTabPageIndex;

            #region 主表
            class_SelectAllModel.class_Main.MethodId = this.textEdit14.Text;
            class_SelectAllModel.class_Main.MethodContent = this.textEdit15.Text;
            class_SelectAllModel.class_Main.ResultType = this.radioGroup7.SelectedIndex;
            class_SelectAllModel.class_Main.ParameterType = this.radioGroup8.SelectedIndex;
            class_SelectAllModel.class_Main.IsAddXmlHead = this.checkEdit1.Checked;
            class_SelectAllModel.class_Main.NameSpace = this.textEdit16.Text;
            class_SelectAllModel.class_Main.MapContent = Class_Tool.EscapeCharacter(this.memoEdit3.Text);
            class_SelectAllModel.class_Main.SelectContent = Class_Tool.EscapeCharacter(this.memoEdit4.Text);
            class_SelectAllModel.class_Main.ServiceInterFaceContent = Class_Tool.EscapeCharacter(this.memoEdit5.Text);
            class_SelectAllModel.class_Main.ServiceImplContent = Class_Tool.EscapeCharacter(this.memoEdit6.Text);
            class_SelectAllModel.class_Main.ModelContent = Class_Tool.EscapeCharacter(this.memoEdit8.Text);
            class_SelectAllModel.class_Main.DTOContent = Class_Tool.EscapeCharacter(this.memoEdit9.Text);
            class_SelectAllModel.class_Main.DAOContent = Class_Tool.EscapeCharacter(this.memoEdit10.Text);
            class_SelectAllModel.class_Main.ControlContent = Class_Tool.EscapeCharacter(this.memoEdit11.Text);
            class_SelectAllModel.class_Main.ResultMapId = this.textEdit22.Text;
            class_SelectAllModel.class_Main.ResultMapType = this.textEdit24.Text;
            class_SelectAllModel.class_Main.ServiceInterFaceReturnRemark = this.textEdit20.Text;
            class_SelectAllModel.class_Main.ServiceInterFaceReturnCount = this.radioGroup9.SelectedIndex;

            #endregion

            if (class_PublicMethod.SelectToXml(class_SelectAllModel.class_Create.MethodId, class_SelectAllModel))
            {
                if (IsDisplayLog)
                    this.DisplayText(string.Format("已将{0}方法【{1}】，保存到本地。", class_SelectAllModel.classType, class_SelectAllModel.class_Create.MethodId));
            }
        }
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_CheckToXml())
                _SaveSelectToXml(true);
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SetSelectAllMode("MM20190220175003765");
        }

        private void listBoxControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.listBoxControl1.SelectedIndex > -1)
                {
                    this.popupMenu1.ShowPopup(this.listBoxControl1.PointToScreen(e.Location));
                }
            }
        }
        private void filterList()
        {
            this.listBoxControl1.Items.Clear();
            foreach (string item in filterUseTable(this.searchControl1.Text.Length > 0 ? this.searchControl1.Text : null))
            {
                this.listBoxControl1.Items.Add(item);
            }
            if (this.listBoxControl1.ItemCount > 0)
            {
                this.listBoxControl1.SelectedIndex = 0;
                this.listBoxControl1.Focus();
            }

        }
        private List<string> filterUseTable(string FilterText)
        {
            if (FilterText == null)
                return myTableNameList;
            else
            {
                List<string> vs = new List<string>();
                vs = myTableNameList.FindAll(a => a.ToUpper().IndexOf(FilterText.ToUpper().Trim()) > -1);
                return vs;
            }
        }
        private void searchControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filterList();
            }
        }

        private void searchControl1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            _getUseTable(false);
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToMain();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToSub();
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToSubSub();
        }

        private void bandedGridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int Index = e.FocusedRowHandle;
            if (Index > -1)
            {
                DataRow row = (sender as BandedGridView).GetDataRow(Index);
                if (row != null)
                {
                    this.textEdit5.Text = row["FieldName"].ToString();
                    this.textEdit4.Text = row["FieldRemark"].ToString();
                }
            }
        }

        private void bandedGridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int Index = e.FocusedRowHandle;
            if (Index > -1)
            {
                DataRow row = (sender as BandedGridView).GetDataRow(Index);
                if (row != null)
                {
                    this.textEdit8.Text = row["FieldName"].ToString();
                    this.textEdit7.Text = row["FieldRemark"].ToString();
                }
            }
        }

        private void listBoxControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ToUseTable();
            }
        }
        private void AgainFromXmlToGrid(int PageIndex)
        {
            switch (PageIndex)
            {
                case 0:
                    ToMain(class_SelectAllModel.class_Main.TableName);
                    DisplayText("主表导入完成!");
                    break;
                case 1:
                    ToSub(class_SelectAllModel.class_Subs.TableName);
                    DisplayText("从表导入完成!");
                    break;
                case 2:
                    ToSubSub(class_SelectAllModel.class_SubSubs.TableName);
                    DisplayText("从从表导入完成!");
                    break;
                default:
                    break;
            }
        }
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AgainFromXmlToGrid(this.xtraTabControl5.SelectedTabPageIndex);
        }
        private void DisplayText(string Content)
        {
            if (this.memoEdit1.Text.Length > 0)
                this.memoEdit1.Text = string.Format("{1}\r\n{0}:----------->>>{2}", System.DateTime.Now.ToLongTimeString(), this.memoEdit1.Text, Content);
            else
                this.memoEdit1.Text = string.Format("{0}:----------->>>{1}", System.DateTime.Now.ToLongTimeString(), Content);
            this.memoEdit1.SelectionStart = this.memoEdit1.Text.Length;
            this.memoEdit1.ScrollToCaret();
        }

        private void xtraTabControl5_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.listBoxControl1.SelectedIndex > -1)
                {
                    this.popupMenu2.ShowPopup(this.xtraTabControl5.PointToScreen(e.Location));
                }
            }
        }
        private void ClearDate(int PageIndex)
        {
            switch (PageIndex)
            {
                case 0:
                    this.gridControl1.DataSource = null;
                    break;
                case 1:
                    this.gridControl2.DataSource = null;
                    break;
                case 2:
                    this.gridControl3.DataSource = null;
                    break;
                default:
                    break;
            }
        }
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.xtraTabControl5.SelectedTabPageIndex > -1)
                ClearDate(this.xtraTabControl5.SelectedTabPageIndex);
        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Class_DataBaseConDefault class_DataBaseConDefault = new Class_DataBaseConDefault()
            {
                dataBaseName = class_SelectAllModel.class_SelectDataBase.dataBaseName,
                databaseType = class_SelectAllModel.class_SelectDataBase.databaseType,
                dataSourcePassWord = class_SelectAllModel.class_SelectDataBase.dataSourcePassWord,
                dataSourceUrl = class_SelectAllModel.class_SelectDataBase.dataSourceUrl,
                dataSourceUserName = class_SelectAllModel.class_SelectDataBase.dataSourceUserName,
                Port = class_SelectAllModel.class_SelectDataBase.Port
            };

            if (class_PublicMethod.DataBaseDefaultValueToXml("DataBaseDefaultValues", class_DataBaseConDefault))
            {
                this.DisplayText("已数据库连接默认值，保存到本地。");
            }
        }

        private void propertyGridControl3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.popupMenu3.ShowPopup(this.propertyGridControl3.PointToScreen(e.Location));
            }
        }

        private void textEdit16_EditValueChanged(object sender, EventArgs e)
        {
            this.textEdit18.Text = string.Format("{0}.dao.{1}Mapper", this.textEdit13.Text, this.textEdit16.Text);
            this.textEdit24.Text = (sender as TextEdit).Text;
        }

        private void textEdit13_EditValueChanged(object sender, EventArgs e)
        {
            this.textEdit18.Text = string.Format("{0}.dao.{1}Mapper", this.textEdit13.Text, this.textEdit16.Text);
            this.textEdit23.Text = string.Format("{0}.model.{1}", this.textEdit13.Text, this.textEdit24.Text);
            //this.textEdit25.Text = string.Format("{0}.{1}Map", this.textEdit13.Text, this.textEdit22.Text);
        }

        private void textEdit24_EditValueChanged(object sender, EventArgs e)
        {
            this.textEdit23.Text = string.Format("{0}.model.{1}", this.textEdit13.Text, this.textEdit24.Text);
        }

        private void CreateCode()
        {
            WaitDialogForm waitDialogForm = new WaitDialogForm("正在玩命生成中......", "温馨提示");
            //1：保存到XML
            _SaveSelectToXml(false);
            //2：得到XML文件名
            string MethodId = class_SelectAllModel.class_Create.MethodId;
            //3：初始化生成类
            Class_InterFaceCreateCode class_InterFaceCreateCode = new Class_CreateSelectCode(MethodId);
            //4：验证合法性
            if (class_InterFaceCreateCode.IsCheckOk())
            {
                //5：生成代码
                //MAP
                this.memoEdit3.Text = class_InterFaceCreateCode.GetMainMap();
                //Select标签
                this.memoEdit4.Text = class_InterFaceCreateCode.GetMainMapLable();
                //ServiceInterFace
                this.memoEdit5.Text = class_InterFaceCreateCode.GetMainServiceInterFace();
                this.DisplayText("代码已重新生成!");
            }
            _SaveSelectToXml(false);
            waitDialogForm.Close();
        }
        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CreateCode();
        }

        private void memoEdit3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9 && e.Shift)
            {
                CreateCode();
            }
        }

        private void textEdit22_EditValueChanged(object sender, EventArgs e)
        {
            this.textEdit25.Text = string.Format("{0}Map", this.textEdit22.Text);
            //this.textEdit24.Text = (sender as TextEdit).Text;
        }
    }
}
