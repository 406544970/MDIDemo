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
using static MDIDemo.PublicClass.Class_UpdateAllModel;

namespace MDIDemo.vou
{
    public partial class Form_Update : DevExpress.XtraEditors.XtraForm
    {
        public Form_Update(string skinName)
        {
            _iniSelect(skinName, null);
        }
        public Form_Update(string skinName, string xmlFileName)
        {
            _iniSelect(skinName, xmlFileName);
        }

        private Class_UpdateAllModel class_UpdateAllModel;
        private IClass_InterFaceDataBase class_InterFaceDataBase;
        private List<string> myTableNameList;
        private List<string> myTableContentList;
        private Class_PublicMethod class_PublicMethod;
        private string MyXmlFileName;

        private void _iniSelect(string skinName, string xmlFileName)
        {
            InitializeComponent();
            MyXmlFileName = xmlFileName;
            publicSkinName = skinName;
            class_PublicMethod = new Class_PublicMethod();
            SetCompoment();
            this.listBoxControl1.Items.Clear();
            this.listBoxControl3.Items.Clear();
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

            class_UpdateAllModel = new Class_UpdateAllModel();
            SetSelectAllMode(xmlFileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlFileName"></param>
        public void SetSelectAllMode(string xmlFileName)
        {
            try
            {
                if (xmlFileName != null)
                    class_UpdateAllModel = class_PublicMethod.FromXmlToUpdateObject<Class_UpdateAllModel>(xmlFileName);
                if (class_UpdateAllModel == null)
                    class_UpdateAllModel = new Class_UpdateAllModel();
                switch (class_UpdateAllModel.class_SelectDataBase.databaseType)
                {
                    case "MySql":
                        class_InterFaceDataBase = new Class_MySqlDataBase(class_UpdateAllModel.class_SelectDataBase.dataSourceUrl, class_UpdateAllModel.class_SelectDataBase.dataBaseName, class_UpdateAllModel.class_SelectDataBase.dataSourceUserName, class_UpdateAllModel.class_SelectDataBase.dataSourcePassWord, class_UpdateAllModel.class_SelectDataBase.Port);
                        break;
                    case "SqlServer 2017":
                        class_InterFaceDataBase = new Class_SqlServer2017DataBase(class_UpdateAllModel.class_SelectDataBase.dataSourceUrl, class_UpdateAllModel.class_SelectDataBase.dataBaseName, class_UpdateAllModel.class_SelectDataBase.dataSourceUserName, class_UpdateAllModel.class_SelectDataBase.dataSourcePassWord);
                        break;
                    default:
                        break;
                }
                class_InterFaceDataBase.SetClass_AllModel(class_UpdateAllModel);
                this.textEdit13.Text = class_UpdateAllModel.AllPackerName;
                this.checkEdit2.Checked = class_UpdateAllModel.IsAutoWard;
                this.memoEdit12.Text = Class_Tool.UnEscapeCharacter(class_UpdateAllModel.TestUnit);
                this.textEdit21.Text = class_UpdateAllModel.TestClassName;
                this.checkEdit19.Checked = class_UpdateAllModel.ReturnStructure;
                this.checkEdit20.Checked = class_UpdateAllModel.ReadWriteSeparation;

                #region 设置上次打开的Tab
                this.xtraTabControl1.SelectedTabPageIndex = class_UpdateAllModel.class_WindowLastState.xtraTabControl1;
                this.xtraTabControl2.SelectedTabPageIndex = class_UpdateAllModel.class_WindowLastState.xtraTabControl2;
                this.xtraTabControl3.SelectedTabPageIndex = class_UpdateAllModel.class_WindowLastState.xtraTabControl3;
                this.xtraTabControl4.SelectedTabPageIndex = class_UpdateAllModel.class_WindowLastState.xtraTabControl4;
                this.xtraTabControl6.SelectedTabPageIndex = class_UpdateAllModel.class_WindowLastState.xtraTabControl6;
                this.xtraTabControl7.SelectedTabPageIndex = class_UpdateAllModel.class_WindowLastState.xtraTabControl7;
                this.xtraTabControl9.SelectedTabPageIndex = class_UpdateAllModel.class_WindowLastState.xtraTabControl9;
                this.xtraTabControl8.SelectedTabPageIndex = class_UpdateAllModel.class_WindowLastState.xtraTabControl8;
                #endregion
                int index = 0;
                #region 表
                if (class_UpdateAllModel.class_SubList.Count > index)
                {
                    this.textEdit14.Text = class_UpdateAllModel.class_SubList[index].MethodId;
                    this.textEdit15.Text = class_UpdateAllModel.class_SubList[index].MethodContent;
                    this.textEdit16.Text = class_UpdateAllModel.class_SubList[index].NameSpace;
                    this.memoEdit3.Text = Class_Tool.UnEscapeCharacter(class_UpdateAllModel.class_SubList[index].MapContent);
                    this.memoEdit4.Text = Class_Tool.UnEscapeCharacter(class_UpdateAllModel.class_SubList[index].SelectContent);
                    this.memoEdit5.Text = Class_Tool.UnEscapeCharacter(class_UpdateAllModel.class_SubList[index].ServiceInterFaceContent);
                    this.memoEdit6.Text = Class_Tool.UnEscapeCharacter(class_UpdateAllModel.class_SubList[index].ServiceImplContent);
                    this.memoEdit8.Text = Class_Tool.UnEscapeCharacter(class_UpdateAllModel.class_SubList[index].ModelContent);
                    this.memoEdit9.Text = Class_Tool.UnEscapeCharacter(class_UpdateAllModel.class_SubList[index].DTOContent);
                    this.memoEdit10.Text = Class_Tool.UnEscapeCharacter(class_UpdateAllModel.class_SubList[index].DAOContent);
                    this.memoEdit11.Text = Class_Tool.UnEscapeCharacter(class_UpdateAllModel.class_SubList[index].ControlContent);
                    this.memoEdit31.Text = Class_Tool.UnEscapeCharacter(class_UpdateAllModel.class_SubList[index].InPutParamContent);
                    this.textEdit100.Text = class_UpdateAllModel.class_SubList[index].ModelClassName;
                    this.textEdit17.Text = class_UpdateAllModel.class_Create.MethodId;
                    this.textEdit20.Text = class_UpdateAllModel.class_SubList[index].ServiceInterFaceReturnRemark;
                    this.textEdit47.Text = class_UpdateAllModel.class_SubList[index].ControlSwaggerValue;
                    this.textEdit46.Text = class_UpdateAllModel.class_SubList[index].ControlSwaggerDescription;
                    this.radioGroup9.SelectedIndex = class_UpdateAllModel.class_SubList[index].ServiceInterFaceReturnCount;
                    this.checkEdit10.Checked = class_UpdateAllModel.class_SubList[index].CreateMainCode;
                    this.panelControl4.Height = class_UpdateAllModel.class_SubList[index].PanelHeight;
                    this.memoEdit54.Text = Class_Tool.UnEscapeCharacter(class_UpdateAllModel.class_SubList[index].TestSql);

                    this.textEdit99.Text = class_UpdateAllModel.class_SubList[index].ParamClassName;
                    this.textEdit100.Text = class_UpdateAllModel.class_SubList[index].ModelClassName;
                    this.textEdit19.Text = class_UpdateAllModel.class_SubList[index].DtoClassName;
                    this.textEdit101.Text = class_UpdateAllModel.class_SubList[index].DaoClassName;
                    this.textEdit44.Text = class_UpdateAllModel.class_SubList[index].XmlFileName;
                    this.textEdit54.Text = class_UpdateAllModel.class_SubList[index].ControlRequestMapping;
                    this.textEdit102.Text = class_UpdateAllModel.class_SubList[index].ServiceInterFaceName;
                    this.textEdit103.Text = class_UpdateAllModel.class_SubList[index].ServiceClassName;

                    if (this.panelControl4.Height > this.simpleButton2.Height + 5)
                        this.simpleButton2.Text = "折叠";
                    else
                        this.simpleButton2.Text = "展开";

                }
                #endregion

                if (class_UpdateAllModel != null)
                {
                    this.propertyGridControl3.SelectedObject = class_UpdateAllModel.class_SelectDataBase;
                    this.propertyGridControl4.SelectedObject = class_UpdateAllModel.class_Create;
                    this.propertyGridControl5.SelectedObject = class_UpdateAllModel.class_MyBatisMap;
                    this.textEdit13.Text = class_UpdateAllModel.AllPackerName;
                    if (xmlFileName != null)
                    {
                        _getUseTable(false);
                        for (int i = 0; i < class_UpdateAllModel.class_SubList.Count; i++)
                        {
                            if (class_UpdateAllModel.class_SubList[i] != null)
                                this.AddUseTableData(class_UpdateAllModel.class_SubList[i].TableName, class_UpdateAllModel.class_SubList[i].AliasName, i, false);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("关系型数据库链接错误，请核对数据库链接参数！\r\n{0}", e.Message)
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public string publicSkinName;
        private void SetCompoment()
        {
            this.checkEdit8.Checked = false;

            SetIniSkin(publicSkinName);
            xtraTabControl3.SelectedTabPageIndex = 0;
            xtraTabControl4.SelectedTabPageIndex = 0;

            this.bandedGridColumn1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            myTableNameList = new List<string>();
            myTableContentList = new List<string>();

            #region ButtonEdit
            Class_SetButtonEdit class_SetButtonEdit = new Class_SetButtonEdit();
            //class_SetButtonEdit.SetButtonEdit(this.buttonEdit8);
            #endregion

            #region Grid
            GridC gridC = new GridC();
            gridC.pub_SetBandedGridViewStyle(this.bandedGridView1);
            gridC.SetGridBar(this.gridControl1);
            #endregion

            #region Bar
            Class_SetUpBar class_SetUpBar = new Class_SetUpBar();
            class_SetUpBar.setBar(this.bar2, "提示操作");
            class_SetUpBar.setBar(this.bar1, "提示操作");
            class_SetUpBar.setBar(this.bar3, "提示操作");
            class_SetUpBar.setBar(this.bar4, "提示操作");
            #endregion

            #region MemoEdit
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
            class_SetMemoEdit.SetMemoEdit(this.memoEdit31);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit54);
            #endregion

            #region TextEdit
            Class_SetTextEdit class_SetTextEdit = new Class_SetTextEdit();
            class_SetTextEdit.SetTextEdit(this.textEdit19, Color.LightGreen);
            class_SetTextEdit.SetTextEdit(this.textEdit99, Color.LightGreen);
            class_SetTextEdit.SetTextEdit(this.textEdit100, Color.LightGreen);
            class_SetTextEdit.SetTextEdit(this.textEdit101, Color.LightGreen);
            class_SetTextEdit.SetTextEdit(this.textEdit102, Color.LightGreen);
            class_SetTextEdit.SetTextEdit(this.textEdit103, Color.LightGreen);

            class_SetTextEdit.SetTextEdit(this.textEdit44);

            class_SetTextEdit.SetTextEdit(this.textEdit13, Color.Yellow);
            class_SetTextEdit.SetTextEdit(this.textEdit10, Color.Yellow);

            class_SetTextEdit.SetTextEdit(this.textEdit14, Color.LightGreen);
            class_SetTextEdit.SetTextEdit(this.textEdit15, Color.LightGreen);
            class_SetTextEdit.SetTextEdit(this.textEdit16, Color.LightBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit20, Color.LightGreen);
            class_SetTextEdit.SetTextEdit(this.textEdit46, Color.LightBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit47, Color.LightBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit54, Color.LightGreen);

            class_SetTextEdit.SetTextEdit(this.textEdit17, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit1, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit2, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit3, true, Color.GreenYellow);
            #endregion

            #region radioGroup
            this.radioGroup9.SelectedIndex = 0;
            #endregion

            this.simpleButton2.Text = "折叠";

            AddComponentType(this.repositoryItemComboBox10);
        }

        private void SetIniSkin(string skinName)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
        }

        private void Form_Select_Shown(object sender, EventArgs e)
        {
            this.dockPanel1.Size = new System.Drawing.Size(295, 288);
            this.dockPanel2.Size = new System.Drawing.Size(314, 288);
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
        private void AddComponentType(RepositoryItemComboBox repositoryItemComboBox)
        {
            IClass_CreateFrontPage class_CreateFrontPage = new Class_CreateUpdateCode();
            repositoryItemComboBox.Items.Clear();
            foreach (string row in class_CreateFrontPage.GetComponentType())
            {
                repositoryItemComboBox.Items.Add(row);
            }
        }
        private void AddComumnCombox(ComboBoxEdit comboBoxEdit, List<string> vs)
        {
            String Text = comboBoxEdit.Text;
            comboBoxEdit.Properties.Items.Clear();
            foreach (string item in vs)
            {
                comboBoxEdit.Properties.Items.Add(item);
            }
            if (Text == null && Text.Equals(""))
                comboBoxEdit.SelectedIndex = -1;
            else
                comboBoxEdit.Text = Text;
        }
        private void AddColumnRepositoryCombox(RepositoryItemComboBox repositoryItemComboBox)
        {
            repositoryItemComboBox.Items.Clear();
            foreach (string row in class_InterFaceDataBase.GetDataType())
            {
                repositoryItemComboBox.Items.Add(row);
            }
        }
        private void AddUseTableData(string TableName, string TableAlias, int PageSelectIndex, bool SelectSelectDefault)
        {
            if (this.MyXmlFileName != null)
                class_UpdateAllModel = class_PublicMethod.FromXmlToUpdateObject<Class_UpdateAllModel>(this.MyXmlFileName);
            if (class_UpdateAllModel == null)
                class_UpdateAllModel = new Class_UpdateAllModel();
            switch (class_UpdateAllModel.class_SelectDataBase.databaseType)
            {
                case "MySql":
                    class_InterFaceDataBase = new Class_MySqlDataBase(class_UpdateAllModel.class_SelectDataBase.dataSourceUrl, class_UpdateAllModel.class_SelectDataBase.dataBaseName, class_UpdateAllModel.class_SelectDataBase.dataSourceUserName, class_UpdateAllModel.class_SelectDataBase.dataSourcePassWord, class_UpdateAllModel.class_SelectDataBase.Port);
                    break;
                case "SqlServer 2017":
                    class_InterFaceDataBase = new Class_SqlServer2017DataBase(class_UpdateAllModel.class_SelectDataBase.dataSourceUrl, class_UpdateAllModel.class_SelectDataBase.dataBaseName, class_UpdateAllModel.class_SelectDataBase.dataSourceUserName, class_UpdateAllModel.class_SelectDataBase.dataSourcePassWord);
                    break;
                default:
                    break;
            }
            class_InterFaceDataBase.SetClass_AllModel(class_UpdateAllModel);
            switch (PageSelectIndex)
            {
                case 0:
                    {
                        this.gridControl1.DataSource = class_InterFaceDataBase.GetMainTableStruct<Class_UpdateAllModel>(TableName, PageSelectIndex, SelectSelectDefault);
                        textEdit1.Text = TableName;
                        if (class_UpdateAllModel.class_SubList.Count > PageSelectIndex && class_UpdateAllModel.class_SubList[PageSelectIndex].AliasName != null)
                            textEdit10.Text = class_UpdateAllModel.class_SubList[PageSelectIndex].AliasName;
                        else
                            textEdit10.Text = TableAlias.Length == 0 ? "main" : TableAlias;
                        AddColumnRepositoryCombox(this.repositoryItemComboBox2);
                        AddColumnComboxFunctionByDataType(this.repositoryItemComboBox1, "");
                        AddColumnComboxHavingFunctionByDataType(this.repositoryItemComboBox7, "");//repositoryItemComboBox10
                    }
                    break;
                default:
                    break;
            }
            if (PageSelectIndex > 0)
            {
                this.xtraTabControl8.TabPages[PageSelectIndex].Text = string.Format("表{1}：{0}", TableName, PageSelectIndex);
            }
            else
            {
                this.xtraTabControl8.TabPages[PageSelectIndex].Text = string.Format("表：{0}", TableName);
            }
        }

        private void ToPage(string TableName, int PageIndex)
        {
            try
            {
                bool IsOk = true;
                if (PageIndex > 0)
                {
                    BandedGridView bandedGridView;
                    switch (PageIndex - 1)
                    {
                        case 0:
                            bandedGridView = gridControl1.MainView as BandedGridView;
                            break;
                        default:
                            bandedGridView = gridControl1.MainView as BandedGridView;
                            break;
                    }
                    IsOk = bandedGridView.RowCount > 0 ? true : false;
                }
                if (IsOk)
                {
                    if (TableName == null)
                    {
                        if (this.listBoxControl1.SelectedIndex > -1)
                            AddUseTableData(this.listBoxControl1.Text, this.listBoxControl3.Text, PageIndex, true);
                    }
                    else
                        AddUseTableData(TableName, this.listBoxControl3.Text, PageIndex, false);
                }
                else
                {
                    if (PageIndex - 1 > 0)
                        MessageBox.Show(string.Format("从表{0}为空时，不能选择此前表！", PageIndex - 1)
                            , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("表为空时，不能选择此前表！"
                            , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ToPage(null, 0);
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
            try
            {
                List<Class_TableInfo> class_TableInfos = new List<Class_TableInfo>();
                if (ReSet)
                {
                    switch (class_UpdateAllModel.class_SelectDataBase.databaseType)
                    {
                        case "MySql":
                            class_InterFaceDataBase = new Class_MySqlDataBase(class_UpdateAllModel.class_SelectDataBase.dataSourceUrl, class_UpdateAllModel.class_SelectDataBase.dataBaseName, class_UpdateAllModel.class_SelectDataBase.dataSourceUserName, class_UpdateAllModel.class_SelectDataBase.dataSourcePassWord, class_UpdateAllModel.class_SelectDataBase.Port);
                            break;
                        case "SqlServer 2017":
                            class_InterFaceDataBase = new Class_SqlServer2017DataBase(class_UpdateAllModel.class_SelectDataBase.dataSourceUrl, class_UpdateAllModel.class_SelectDataBase.dataBaseName, class_UpdateAllModel.class_SelectDataBase.dataSourceUserName, class_UpdateAllModel.class_SelectDataBase.dataSourcePassWord);
                            break;
                        default:
                            break;
                    }
                    class_InterFaceDataBase.SetClass_AllModel(class_UpdateAllModel);
                }
                if (listBoxControl1.ItemCount > 0)
                    listBoxControl1.Items.Clear();
                if (listBoxControl2.ItemCount > 0)
                    listBoxControl2.Items.Clear();
                if (listBoxControl3.ItemCount > 0)
                    listBoxControl3.Items.Clear();
                myTableNameList.Clear();
                myTableContentList.Clear();
                class_TableInfos = class_InterFaceDataBase.GetUseTableList(null);
                myTableNameList = class_TableInfos.Select(a => a.TableName).ToList();
                myTableContentList = class_TableInfos.Select(a => a.TableComment).ToList();
                Class_SQLiteOperator class_SQLiteOperator = new Class_SQLiteOperator();
                foreach (string row in myTableNameList)
                {
                    this.listBoxControl1.Items.Add(row);
                    string AliasName = class_SQLiteOperator.GetTableAlias(class_UpdateAllModel.class_SelectDataBase.dataSourceUrl
                        , class_UpdateAllModel.class_SelectDataBase.dataBaseName, row);
                    this.listBoxControl3.Items.Add(AliasName == null ? "" : AliasName);
                }
                foreach (string row in myTableContentList)
                {
                    this.listBoxControl2.Items.Add(row);
                }

                if (class_UpdateAllModel.LastSelectTableName != null)
                {
                    int LastIndex = myTableNameList.IndexOf(class_UpdateAllModel.LastSelectTableName);
                    this.listBoxControl1.SelectedIndex = LastIndex;
                    this.listBoxControl2.SelectedIndex = LastIndex;
                }
                else
                {
                    this.listBoxControl1.SelectedIndex = -1;
                    this.listBoxControl2.SelectedIndex = -1;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("关系型数据库链接错误，请核对数据库链接参数！\r\n{0}", e.Message)
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bandedGridView">GRIDVIEW数据集</param>
        /// <param name="OutFieldName">从表关联字段</param>
        /// <param name="LinkType"></param>
        /// <param name="CountToCount"></param>
        /// <param name="TableName"></param>
        /// <param name="MainFieldName">表关联字段</param>
        /// <param name="AddPoint"></param>
        /// <param name="AliasName">别名</param>
        /// <param name="TableNo">关系表的序号</param>
        /// <param name="MapMainCode">仅生成Map层体代码</param>
        /// <param name="CreateMainCode">仅生成Control层体代码</param>
        /// <returns></returns>
        private Class_UpdateAllModel.Class_Sub DataViewIntoClass(BandedGridView bandedGridView,
            string OutFieldName, int LinkType, int CountToCount, string TableName,
            string MainFieldName, bool AddPoint, string AliasName,
            int TableNo, bool CreateMainCode)
        {
            Class_UpdateAllModel.Class_Sub class_Sub = new Class_UpdateAllModel.Class_Sub();
            List<Class_UpdateAllModel.Class_Field> class_Fields = new List<Class_UpdateAllModel.Class_Field>();
            for (int j = 0; j < bandedGridView.RowCount; j++)
            {
                DataRow dataRow = bandedGridView.GetDataRow(j);
                if (Convert.ToBoolean(dataRow["FieldIsKey"]))
                {
                    class_Sub.MainFieldName = dataRow["FieldName"].ToString();
                    class_Sub.AddPoint = class_InterFaceDataBase.IsAddPoint(dataRow["FieldType"].ToString());
                }

                Class_UpdateAllModel.Class_Field class_Field = new Class_UpdateAllModel.Class_Field();
                class_Field.FieldName = dataRow["FieldName"].ToString();
                class_Field.FieldRemark = dataRow["FieldRemark"].ToString();
                class_Field.FieldType = dataRow["FieldType"].ToString();
                if ((dataRow["FieldLength"] != null) && (dataRow["FieldLength"].ToString().Length > 0))
                    class_Field.FieldLength = Convert.ToInt32(dataRow["FieldLength"]);
                class_Field.FieldDefaultValue = dataRow["FieldDefaultValue"].ToString();
                class_Field.FieldIsNull = Convert.ToBoolean(dataRow["FieldIsNull"]);
                class_Field.FieldIsKey = Convert.ToBoolean(dataRow["FieldIsKey"]);
                class_Field.FieldIsAutoAdd = Convert.ToBoolean(dataRow["FieldIsAutoAdd"]);

                class_Field.UpdateSelect = Convert.ToBoolean(dataRow["UpdateSelect"]);//Set
                class_Field.ParaName = dataRow["ParaName"].ToString();//映射参数名
                class_Field.TrimSign = Convert.ToBoolean(dataRow["TrimSign"]);//是否去空格

                class_Field.WhereSelect = Convert.ToBoolean(dataRow["WhereSelect"]);//重复判断选择
                class_Field.WhereType = dataRow["WhereType"].ToString();//And Or
                class_Field.LogType = dataRow["LogType"].ToString();// = > < like，固定值
                class_Field.WhereValue = dataRow["WhereValue"].ToString();
                class_Field.WhereTrim = Convert.ToBoolean(dataRow["WhereTrim"]);//去空格
                class_Field.WhereIsNull = Convert.ToBoolean(dataRow["WhereIsNull"]);//Where条件是否可为空

                class_Field.FrontSelect = Convert.ToBoolean(dataRow["FrontSelect"]);//是否页面显示
                class_Field.LabelCaption = dataRow["LabelCaption"].ToString();//标签内容
                class_Field.IsMust = Convert.ToBoolean(dataRow["IsMust"]);//是否为必填项

                class_Field.CompomentType = dataRow["CompomentType"].ToString();//控件类型
                class_Field.Hint = dataRow["Hint"].ToString();//提示
                class_Field.DefaultValue = dataRow["DefaultValue"].ToString();//默认值
                class_Field.SortNo = Convert.ToInt32(dataRow["SortNo"]); ;//出现顺序
                class_Field.ValueId = dataRow["ValueId"].ToString();//值ID
                class_Field.ReadOnly = Convert.ToBoolean(dataRow["ReadOnly"]);//是否只读

                class_Field.CheckType = dataRow["CheckType"].ToString();//校验类型
                class_Field.ClassTitle = dataRow["ClassTitle"].ToString();//分类标题

                class_Fields.Add(class_Field);
            }
            class_Sub.class_Fields = class_Fields;
            class_Sub.TableName = TableName;
            class_Sub.AddPoint = false;
            class_Sub.AliasName = AliasName;
            class_Sub.MainFieldName = MainFieldName;
            class_Sub.CreateMainCode = CreateMainCode;

            return class_Sub;
        }
        /// <summary>
        /// 保存前的验证
        /// </summary>
        /// <param name="IsMongodb">是否Mongodb读写分离</param>
        /// <returns></returns>
        private bool _CheckToXml(bool IsMongodb)
        {
            WaitDialogForm waitDialogForm = new WaitDialogForm("正在玩命验证中......", "温馨提示");

            #region 移动BandedGridView焦点
            BandedGridView[] bandedGridViews = new BandedGridView[1];
            bandedGridViews[0] = gridControl1.MainView as BandedGridView;
            for (int i = 0; i < bandedGridViews.Length; i++)
            {
                BandedGridView item = bandedGridViews[i];
                if (item.RowCount > 0)
                {
                    int FocuedCount = item.FocusedRowHandle;
                    if (FocuedCount == 0)
                        item.FocusedRowHandle = item.RowCount - 1;
                    if (FocuedCount > 0 && FocuedCount < item.RowCount)
                        item.FocusedRowHandle = 0;
                    item.FocusedRowHandle = FocuedCount;
                }
            }
            #endregion

            bool IsOk = true;

            #region Mongodb参数验证----------?
            if (IsOk && IsMongodb)
            {
                IsOk = true;
            }
            #endregion

            #region 数据库参数验证
            if (IsOk)
            {
                IsOk = true;
            }
            #endregion

            #region 微服务名非空验证
            if (IsOk)
            {
                if (class_UpdateAllModel.class_Create.MicroServiceName == null || class_UpdateAllModel.class_Create.MicroServiceName.Length == 0)
                {
                    MessageBox.Show("属性->生成配置:\r\n   微服务名不能为空！", "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IsOk = false;
                }
            }
            #endregion

            #region 创建者名非空验证
            if (IsOk)
            {
                if (class_UpdateAllModel.class_Create.CreateMan == null || class_UpdateAllModel.class_Create.CreateMan.Length == 0)
                {
                    MessageBox.Show("属性->生成配置:\r\n   创建者姓名不能为空！", "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IsOk = false;
                }
            }
            #endregion

            int InsertCount = 0;
            int WhereCount = 0;
            #region Select字段非空验证
            if (IsOk)
            {
                for (int i = 0; i < bandedGridViews.Length; i++)
                {
                    BandedGridView item = bandedGridViews[i];
                    if (item.RowCount > 0)
                    {
                        for (int j = 0; j < item.RowCount; j++)
                        {
                            DataRow dataRow = item.GetDataRow(j);
                            if (Convert.ToBoolean(dataRow["UpdateSelect"]))
                                InsertCount++;
                            if (Convert.ToBoolean(dataRow["WhereSelect"]))
                                WhereCount++;
                        }
                    }
                }
                IsOk = InsertCount > 0 ? true : false;
                if (!IsOk)
                {
                    MessageBox.Show("请选择Select字段！"
                        , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.xtraTabControl3.SelectedTabPageIndex = 0;
                }
            }
            #endregion

            #region 输入参数非空验证
            if (IsOk)
            {
                BandedGridView bandedGridView = gridControl1.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    int index = 0;
                    while (IsOk && index < bandedGridView.RowCount)
                    {
                        DataRow dataRow = bandedGridView.GetDataRow(index++);
                        if (Convert.ToBoolean(dataRow["WhereSelect"]))
                        {
                            string FieldName = dataRow["FieldName"].ToString();
                            string LogType = dataRow["LogType"].ToString();
                            string WhereValue = dataRow["WhereValue"].ToString();
                            if (LogType.IndexOf("NULL") < 0 && (WhereValue == null || WhereValue.Length == 0))
                            {
                                MessageBox.Show(string.Format("表:\r\n   字段[{0}]的WHERE值不能为空！", FieldName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                            }
                        }
                    }
                }
                if (!IsOk)
                {
                    this.xtraTabControl3.SelectedTabPageIndex = 0;
                }
            }
            #endregion

            #region Param类名非空验证
            if (IsOk && WhereCount > 1)
            {
                BandedGridView bandedGridView = gridControl1.MainView as BandedGridView;
                if (bandedGridView.RowCount > 0)
                {
                    if (this.textEdit99.Text == null || this.textEdit99.Text.Length == 0)
                    {
                        MessageBox.Show(string.Format("{0}类名不能为空！", "InPutParam")
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                        this.xtraTabControl3.SelectedTabPageIndex = 1;
                        this.xtraTabControl8.SelectedTabPageIndex = 0;
                    }
                }
            }
            #endregion

            #region 全包名合法性
            if (IsOk)
            {
                if (this.textEdit13.Text == null || this.textEdit13.Text.Length == 0)
                {
                    MessageBox.Show("全局包名不能为空！", "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IsOk = false;
                    this.xtraTabControl3.SelectedTabPageIndex = 1;
                }
            }
            #endregion

            #region NameSpace及Dto类名非空验证
            if (IsOk)
            {
                int ActiveIndex = -1;
                BandedGridView bandedGridView = gridControl1.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    if (this.textEdit16.Text == null || this.textEdit16.Text.Length == 0)
                    {
                        MessageBox.Show(string.Format("{0}表:NameSpace不能为空！", "")
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (IsOk && (this.textEdit19.Text == null || this.textEdit19.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("{0}表:Dto类名不能为空！", "")
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (!IsOk)
                        ActiveIndex = 0;
                }
                if (!IsOk)
                {
                    this.xtraTabControl3.SelectedTabPageIndex = 1;
                    this.xtraTabControl8.SelectedTabPageIndex = ActiveIndex;
                }
            }
            #endregion

            #region ResultMapId、 Model、 Dao、 ServiceInterFace、 ServiceImpl、 Control、 Control层Swagger说明、 Control层Swagger描述、 方法名、方法说明、方法描述非空验证
            if (IsOk)
            {
                BandedGridView bandedGridView = gridControl1.MainView as BandedGridView;
                if (bandedGridView.RowCount > 0)
                {
                    if (IsOk && (this.textEdit100.Text == null || this.textEdit100.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("{0}类名不能为空！", "Model")
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (IsOk && (this.textEdit101.Text == null || this.textEdit101.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("{0}类名不能为空！", "Dao")
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (IsOk && (this.textEdit102.Text == null || this.textEdit102.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("{0}类名不能为空！", "ServiceInterFace")
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (IsOk && (this.textEdit103.Text == null || this.textEdit103.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("{0}类名不能为空！", "ServiceImpl")
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (IsOk && (this.textEdit54.Text == null || this.textEdit54.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("{0}类名不能为空！", "Control")
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (IsOk && (this.textEdit47.Text == null || this.textEdit47.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("{0}不能为空！", "Control层Swagger说明")
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (IsOk && (this.textEdit46.Text == null || this.textEdit46.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("{0}不能为空！", "Control层Swagger描述")
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (IsOk && (this.textEdit14.Text == null || this.textEdit14.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("{0}不能为空！", "方法名")
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (IsOk && (this.textEdit15.Text == null || this.textEdit15.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("{0}不能为空！", "方法说明")
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (IsOk && (this.textEdit20.Text == null || this.textEdit20.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("{0}不能为空！", "方法描述")
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (!IsOk)
                    {
                        this.xtraTabControl3.SelectedTabPageIndex = 1;
                        this.xtraTabControl8.SelectedTabPageIndex = 0;
                    }
                }
            }
            #endregion

            waitDialogForm.Close();
            return IsOk;
        }
        private void _SaveSelectToXml(bool IsDisplayLog)
        {
            int index = 0;
            if (this.listBoxControl1.SelectedIndex > -1)
                class_UpdateAllModel.LastSelectTableName = this.listBoxControl1.SelectedValue.ToString();
            if (class_UpdateAllModel.class_Create.MethodId == null)
            {
                class_UpdateAllModel.class_Create.MethodId = Class_Tool.getKeyId("SE");
                this.Text = string.Format("UPDATE：{0}", class_UpdateAllModel.class_Create.MethodId);
                this.textEdit17.Text = class_UpdateAllModel.class_Create.MethodId;

            }
            Class_WindowType class_WindowType = new Class_WindowType();
            class_WindowType.WindowType = "update";
            class_WindowType.XmlFileName = class_UpdateAllModel.class_Create.MethodId;
            this.Tag = class_WindowType;
            class_UpdateAllModel.class_Create.DateTime = System.DateTime.Now;
            if (this.gridControl1.MainView.RowCount > 0)
            {
                Class_Sub class_Sub = DataViewIntoClass((BandedGridView)this.gridControl1.MainView
                    , null, 0, -1, this.textEdit1.Text
                    , null, false, this.textEdit10.Text
                    , -1, this.checkEdit10.Checked);
                if (class_UpdateAllModel.class_SubList.Count > index)
                    class_UpdateAllModel.class_SubList[index] = class_Sub;
                else
                    class_UpdateAllModel.class_SubList.Add(class_Sub);
                index++;
            }

            if (index > 0)
            {
                class_UpdateAllModel.AllPackerName = this.textEdit13.Text;
                class_UpdateAllModel.IsAutoWard = this.checkEdit2.Checked;
                class_UpdateAllModel.TestUnit = Class_Tool.EscapeCharacter(this.memoEdit12.Text);
                class_UpdateAllModel.TestClassName = this.textEdit21.Text;
                class_UpdateAllModel.ReturnStructure = this.checkEdit19.Checked;
                class_UpdateAllModel.ReadWriteSeparation = this.checkEdit20.Checked;

                #region 保存上次打开的Tab
                class_UpdateAllModel.class_WindowLastState.xtraTabControl1 = this.xtraTabControl1.SelectedTabPageIndex;
                class_UpdateAllModel.class_WindowLastState.xtraTabControl2 = this.xtraTabControl2.SelectedTabPageIndex;
                class_UpdateAllModel.class_WindowLastState.xtraTabControl3 = this.xtraTabControl3.SelectedTabPageIndex;
                class_UpdateAllModel.class_WindowLastState.xtraTabControl4 = this.xtraTabControl4.SelectedTabPageIndex;
                class_UpdateAllModel.class_WindowLastState.xtraTabControl6 = this.xtraTabControl6.SelectedTabPageIndex;
                class_UpdateAllModel.class_WindowLastState.xtraTabControl7 = this.xtraTabControl7.SelectedTabPageIndex;
                class_UpdateAllModel.class_WindowLastState.xtraTabControl8 = this.xtraTabControl8.SelectedTabPageIndex;
                class_UpdateAllModel.class_WindowLastState.xtraTabControl9 = this.xtraTabControl9.SelectedTabPageIndex;
                #endregion

                #region 表
                index = 0;
                if (this.gridControl1.MainView.RowCount > 0)
                {
                    class_UpdateAllModel.class_SubList[index].MethodId = this.textEdit14.Text;
                    class_UpdateAllModel.class_SubList[index].MethodContent = this.textEdit15.Text;
                    class_UpdateAllModel.class_SubList[index].NameSpace = this.textEdit16.Text;
                    class_UpdateAllModel.class_SubList[index].MapContent = Class_Tool.EscapeCharacter(this.memoEdit3.Text);
                    class_UpdateAllModel.class_SubList[index].SelectContent = Class_Tool.EscapeCharacter(this.memoEdit4.Text);
                    class_UpdateAllModel.class_SubList[index].ServiceInterFaceContent = Class_Tool.EscapeCharacter(this.memoEdit5.Text);
                    class_UpdateAllModel.class_SubList[index].ServiceImplContent = Class_Tool.EscapeCharacter(this.memoEdit6.Text);
                    class_UpdateAllModel.class_SubList[index].ModelContent = Class_Tool.EscapeCharacter(this.memoEdit8.Text);
                    class_UpdateAllModel.class_SubList[index].DTOContent = Class_Tool.EscapeCharacter(this.memoEdit9.Text);
                    class_UpdateAllModel.class_SubList[index].DAOContent = Class_Tool.EscapeCharacter(this.memoEdit10.Text);
                    class_UpdateAllModel.class_SubList[index].ControlContent = Class_Tool.EscapeCharacter(this.memoEdit11.Text);
                    class_UpdateAllModel.class_SubList[index].InPutParamContent = Class_Tool.EscapeCharacter(this.memoEdit31.Text);
                    class_UpdateAllModel.class_SubList[index].ModelClassName = this.textEdit100.Text;
                    class_UpdateAllModel.class_SubList[index].ControlSwaggerValue = this.textEdit47.Text;
                    class_UpdateAllModel.class_SubList[index].ControlSwaggerDescription = this.textEdit46.Text;
                    class_UpdateAllModel.class_SubList[index].ServiceInterFaceReturnRemark = this.textEdit20.Text;
                    class_UpdateAllModel.class_SubList[index].ServiceInterFaceReturnCount = this.radioGroup9.SelectedIndex;
                    class_UpdateAllModel.class_SubList[index].PanelHeight = this.panelControl4.Height;
                    class_UpdateAllModel.class_SubList[index].TestSql = Class_Tool.EscapeCharacter(this.memoEdit54.Text);
                    class_UpdateAllModel.class_SubList[index].AliasName = this.textEdit10.Text;

                    class_UpdateAllModel.class_SubList[index].ParamClassName = this.textEdit99.Text;
                    class_UpdateAllModel.class_SubList[index].ModelClassName = this.textEdit100.Text;
                    class_UpdateAllModel.class_SubList[index].DtoClassName = this.textEdit19.Text;
                    class_UpdateAllModel.class_SubList[index].DaoClassName = this.textEdit101.Text;
                    class_UpdateAllModel.class_SubList[index].XmlFileName = this.textEdit44.Text;
                    class_UpdateAllModel.class_SubList[index].ControlRequestMapping = this.textEdit54.Text;
                    class_UpdateAllModel.class_SubList[index].ServiceInterFaceName = this.textEdit102.Text;
                    class_UpdateAllModel.class_SubList[index].ServiceClassName = this.textEdit103.Text;

                }
                #endregion

                if (class_PublicMethod.UpdateToXml(class_UpdateAllModel.class_Create.MethodId, class_UpdateAllModel))
                {
                    if (IsDisplayLog)
                        this.DisplayText(string.Format("已将{0}方法【{1}】，保存到本地。", class_UpdateAllModel.classType, class_UpdateAllModel.class_Create.MethodId));
                }
            }
            else
            {
                if (IsDisplayLog)
                    this.DisplayText("失败，请完成表设置。");
            }
        }
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_CheckToXml(this.checkEdit20.Checked))
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
        private List<string> GetAnyTableContent(List<string> TableNameList)
        {
            List<string> vs = new List<string>();
            return vs;
        }
        private void filterList()
        {
            this.listBoxControl1.Items.Clear();
            this.listBoxControl2.Items.Clear();
            this.listBoxControl3.Items.Clear();
            List<string> TableNameList = new List<string>();
            Class_SQLiteOperator class_SQLiteOperator = new Class_SQLiteOperator();
            foreach (string item in filterUseTable(this.searchControl1.Text.Length > 0 ? this.searchControl1.Text : null))
            {
                this.listBoxControl1.Items.Add(item);
                TableNameList.Add(item);
                this.listBoxControl3.Items.Add(class_SQLiteOperator.GetTableAlias(class_UpdateAllModel.class_SelectDataBase.dataSourceUrl
                    , class_UpdateAllModel.class_SelectDataBase.dataBaseName, item));
            }
            foreach (string item in class_InterFaceDataBase.GetUseTableList(TableNameList).Select(a => a.TableComment).ToList())
            {
                this.listBoxControl2.Items.Add(item);
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
            ToPage(null, 0);
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToPage(null, 1);
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToPage(null, 2);
        }

        private void FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e
            , DevExpress.XtraEditors.TextEdit textEdit_FieldName
            , DevExpress.XtraEditors.TextEdit textEdit_FieldRemark)
        {
            int Index = e.FocusedRowHandle;
            if (Index > -1)
            {
                DataRow row = (sender as BandedGridView).GetDataRow(Index);
                if (row != null)
                {
                    textEdit_FieldName.Text = row["FieldName"].ToString();
                    textEdit_FieldRemark.Text = row["FieldRemark"].ToString();
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
            ToPage(class_UpdateAllModel.class_SubList[PageIndex].TableName, PageIndex);
            if (PageIndex > 0)
            {
                DisplayText(String.Format("表{0}导入完成!", (PageIndex + 1).ToString()));
            }
            else
            {
                DisplayText("表导入完成!");
            }
        }
        private void DisplayText(string Content)
        {
            if (this.memoEdit1.Text.Length > 0)
                this.memoEdit1.Text = string.Format("{1}\r\n{0}:--------- -->>>{2}", System.DateTime.Now.ToLongTimeString(), this.memoEdit1.Text, Content);
            else
                this.memoEdit1.Text = string.Format("{0}:--------- -->>>{1}", System.DateTime.Now.ToLongTimeString(), Content);
            this.memoEdit1.SelectionStart = this.memoEdit1.Text.Length;
            this.memoEdit1.ScrollToCaret();
        }

        private void ClearDate(int PageIndex)
        {
            int PageMaxIndex = class_UpdateAllModel.class_SubList.Count;
            switch (PageIndex)
            {
                case 0:
                    this.gridControl1.DataSource = null;
                    break;
                default:
                    break;
            }
            this.xtraTabControl8.TabPages[PageIndex].Text = "表";
            for (int i = PageMaxIndex - 1; i > PageIndex - 1; i--)
            {
                class_UpdateAllModel.class_SubList.RemoveAt(i);
            }
            //if (class_SelectAllModel.class_SubList.Count > PageIndex)
            //    class_SelectAllModel.class_SubList[PageIndex] = new Class_UpdateAllModel.Class_Sub();
        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            class_UpdateAllModel.class_SelectDataBase.GetDataBaseDefault();
            propertyGridControl3.Refresh();
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //存入默认值
            Class_DataBaseConDefault class_DataBaseConDefault = new Class_DataBaseConDefault()
            {
                dataBaseName = class_UpdateAllModel.class_SelectDataBase.dataBaseName,
                databaseType = class_UpdateAllModel.class_SelectDataBase.databaseType,
                dataSourcePassWord = class_UpdateAllModel.class_SelectDataBase.dataSourcePassWord,
                dataSourceUrl = class_UpdateAllModel.class_SelectDataBase.dataSourceUrl,
                dataSourceUserName = class_UpdateAllModel.class_SelectDataBase.dataSourceUserName,
                Port = class_UpdateAllModel.class_SelectDataBase.Port
            };

            if (class_PublicMethod.DataBaseDefaultValueToXml("DataBaseDefaultValues", class_DataBaseConDefault))
            {
                this.DisplayText("已将数据库连接默认值，保存到本地。");
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
            if (checkEdit8.Checked)
            {
                this.textEdit99.Text = Class_Tool.GetFirstCodeUpper(string.Format("{0}UpdateInParam", (sender as TextEdit).Text));
                this.textEdit100.Text = Class_Tool.GetFirstCodeUpper(string.Format("{0}Model", (sender as TextEdit).Text));
                this.textEdit19.Text = Class_Tool.GetFirstCodeUpper(string.Format("{0}Dto", (sender as TextEdit).Text));
                this.textEdit101.Text = Class_Tool.GetFirstCodeUpper(string.Format("{0}Mapper", (sender as TextEdit).Text));
                this.textEdit44.Text = Class_Tool.GetFirstCodeUpper(string.Format("{0}Mapper.xml", (sender as TextEdit).Text));
                this.textEdit54.Text = Class_Tool.GetFirstCodeUpper(string.Format("{0}Controller", (sender as TextEdit).Text));
                this.textEdit102.Text = Class_Tool.GetFirstCodeUpper(string.Format("{0}Service", (sender as TextEdit).Text));
                this.textEdit103.Text = Class_Tool.GetFirstCodeUpper(string.Format("{0}ServiceImpl", (sender as TextEdit).Text));
            }
        }

        private void CreateCode()
        {
            if (!_CheckToXml(this.checkEdit20.Checked))
                return;
            WaitDialogForm waitDialogForm = new WaitDialogForm("正在玩命生成中......", "温馨提示");
            //1：保存到XML
            _SaveSelectToXml(true);
            //2：得到XML文件名
            string MethodId = class_UpdateAllModel.class_Create.MethodId;
            //3：初始化生成类
            IClass_InterFaceCreateCode class_InterFaceCreateCode = new Class_CreateUpdateCode(MethodId);
            //4：验证合法性
            List<string> outMessage = new List<string>();
            if (class_InterFaceCreateCode.IsCheckOk(ref outMessage))
            {
                ////加入所有外键信息
                //class_InterFaceCreateCode.AddAllOutFieldName();
                int PageIndex = 0;
                //5：生成代码
                #region 表
                if (class_UpdateAllModel.class_SubList.Count > PageIndex)
                {
                    // MAP
                    this.memoEdit3.Text = class_InterFaceCreateCode.GetMap(PageIndex);
                    // Select标签
                    this.memoEdit4.Text = class_InterFaceCreateCode.GetSql(PageIndex);
                    // ServiceInterFace
                    this.memoEdit5.Text = class_InterFaceCreateCode.GetServiceInterFace(PageIndex);
                    // ServiceImpl
                    this.memoEdit6.Text = class_InterFaceCreateCode.GetServiceImpl(PageIndex);
                    // Model
                    this.memoEdit8.Text = class_InterFaceCreateCode.GetModel(PageIndex);
                    // DTO
                    this.memoEdit9.Text = class_InterFaceCreateCode.GetDTO(PageIndex);
                    // DAO
                    this.memoEdit10.Text = class_InterFaceCreateCode.GetDAO(PageIndex);
                    // Control
                    this.memoEdit11.Text = class_InterFaceCreateCode.GetControl(PageIndex);
                    // FeignControl
                    this.memoEdit31.Text = class_InterFaceCreateCode.GetInPutParam(PageIndex);
                    //
                    this.memoEdit54.Text = class_InterFaceCreateCode.GetTestSql(PageIndex);
                }
                #endregion

                _SaveSelectToXml(false);

                this.DisplayText("代码已重新生成!");
                this.xtraTabControl3.SelectedTabPageIndex = 1;
                this.xtraTabControl8.SelectedTabPageIndex = 0;
                this.xtraTabControl6.SelectedTabPageIndex = 4;
                this.xtraTabControl7.SelectedTabPageIndex = 1;
            }
            else
            {
                if (outMessage != null && outMessage.Count > 0)
                {
                    outMessage.ForEach(a => this.DisplayText(a));
                }
            }
            outMessage.Clear();
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

        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm waitDialogForm = new WaitDialogForm("正在玩命生成中......", "温馨提示");
            try
            {
                string FileName = class_InterFaceDataBase.GetDataBaseContent();
                if (FileName != null)
                {
                    if (MessageBox.Show("数据库说明书已生成完成，是否打开？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                        System.Diagnostics.Process.Start(FileName);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                waitDialogForm.Close();
            }
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBoxControl2.SelectedIndex = this.listBoxControl1.SelectedIndex;
            this.listBoxControl3.SelectedIndex = this.listBoxControl1.SelectedIndex;
        }


        private void GetLinkField(GridView gridView, object sender)
        {
            List<Class_LinkField> class_LinkFields = new List<Class_LinkField>();
            for (int index = 0; index < gridView.RowCount; index++)
            {
                DataRow dataRow = gridView.GetDataRow(index);
                Class_LinkField class_LinkField = new Class_LinkField();
                class_LinkField.ParaName = dataRow["FieldName"].ToString();
                class_LinkField.ReturnType = dataRow["ReturnType"].ToString();
                class_LinkField.FieldRemark = dataRow["FieldRemark"].ToString();
                class_LinkFields.Add(class_LinkField);
            }
            Form_SelectField form_SelectField = new Form_SelectField(publicSkinName);
            form_SelectField.class_LinkFields = class_LinkFields;
            form_SelectField.GetData();
            if (form_SelectField.ShowDialog() == DialogResult.OK)
            {
                (sender as ButtonEdit).Text = form_SelectField.ParaName;
                (sender as ButtonEdit).Tag = form_SelectField.ReturnType;
            }
            form_SelectField.Dispose();
        }
        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            GetLinkField(this.bandedGridView1, sender);
        }

        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToPage(null, 3);
        }

        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToPage(null, 4);
        }

        private void OpenClosePanel(object sender, PanelControl panelControl)
        {
            if (panelControl.Height > (sender as SimpleButton).Height + 10)
            {
                panelControl.Height = (sender as SimpleButton).Height + 10;
                (sender as SimpleButton).Text = "展开";
            }
            else
            {
                panelControl.Height = 245;
                (sender as SimpleButton).Text = "折叠";
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            OpenClosePanel(sender, this.panelControl4);
        }

        private void textEdit101_EditValueChanged(object sender, EventArgs e)
        {
            this.textEdit44.Text = (sender as TextEdit).Text + ".xml";
        }

    }
}
