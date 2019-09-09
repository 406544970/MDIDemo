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
using static MDIDemo.PublicClass.Class_SelectAllModel;

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
        private IClass_InterFaceDataBase class_InterFaceDataBase;
        private List<string> myTableNameList;
        private List<string> myTableContentList;
        private Class_PublicMethod class_PublicMethod;

        private void _iniSelect(string skinName, string xmlFileName)
        {
            InitializeComponent();

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

            class_SelectAllModel = new Class_SelectAllModel();
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
                this.checkEdit17.Checked = class_SelectAllModel.PageSign;
                this.checkEdit19.Checked = class_SelectAllModel.ReturnStructure;
                this.comboBoxEdit1.SelectedIndex = class_SelectAllModel.ReturnStructureType;
                this.checkEdit20.Checked = class_SelectAllModel.ReadWriteSeparation;

                #region 设置上次打开的Tab
                this.xtraTabControl1.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl1;
                this.xtraTabControl2.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl2;
                this.xtraTabControl3.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl3;
                this.xtraTabControl4.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl4;
                this.xtraTabControl5.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl5;
                this.xtraTabControl6.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl6;
                this.xtraTabControl7.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl7;
                this.xtraTabControl9.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl9;
                this.xtraTabControl8.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl8;
                this.xtraTabControl10.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl10;
                this.xtraTabControl13.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl13;
                #endregion
                int index = 0;
                #region 主表
                if (class_SelectAllModel.class_SubList.Count > index)
                {
                    this.textEdit14.Text = class_SelectAllModel.class_SubList[index].MethodId;
                    this.textEdit15.Text = class_SelectAllModel.class_SubList[index].MethodContent;
                    this.radioGroup7.SelectedIndex = class_SelectAllModel.class_SubList[index].ResultType;
                    this.checkEdit1.Checked = class_SelectAllModel.class_SubList[index].IsAddXmlHead;
                    this.textEdit16.Text = class_SelectAllModel.class_SubList[index].NameSpace;
                    this.memoEdit3.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[index].MapContent);
                    this.memoEdit4.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[index].SelectContent);
                    this.memoEdit5.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[index].ServiceInterFaceContent);
                    this.memoEdit6.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[index].ServiceImplContent);
                    this.memoEdit8.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[index].ModelContent);
                    this.memoEdit9.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[index].DTOContent);
                    this.memoEdit10.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[index].DAOContent);
                    this.memoEdit11.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[index].ControlContent);
                    this.memoEdit31.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[index].InPutParamContent);
                    this.textEdit22.Text = class_SelectAllModel.class_SubList[index].ResultMapId;
                    this.textEdit24.Text = class_SelectAllModel.class_SubList[index].ModelClassName;
                    this.textEdit17.Text = class_SelectAllModel.class_Create.MethodId;
                    this.textEdit20.Text = class_SelectAllModel.class_SubList[index].ServiceInterFaceReturnRemark;
                    this.textEdit47.Text = class_SelectAllModel.class_SubList[index].ControlSwaggerValue;
                    this.textEdit46.Text = class_SelectAllModel.class_SubList[index].ControlSwaggerDescription;
                    this.radioGroup9.SelectedIndex = class_SelectAllModel.class_SubList[index].ServiceInterFaceReturnCount;
                    this.checkEdit10.Checked = class_SelectAllModel.class_SubList[index].ControlMainCode;
                    this.panelControl4.Height = class_SelectAllModel.class_SubList[index].PanelHeight;
                    this.memoEdit54.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[index].TestSql);

                    this.textEdit99.Text = class_SelectAllModel.class_SubList[index].ParamClassName;
                    this.textEdit100.Text = class_SelectAllModel.class_SubList[index].ModelClassName;
                    this.textEdit19.Text = class_SelectAllModel.class_SubList[index].DtoClassName;
                    this.textEdit101.Text = class_SelectAllModel.class_SubList[index].DaoClassName;
                    this.textEdit44.Text = class_SelectAllModel.class_SubList[index].XmlFileName;
                    this.textEdit54.Text = class_SelectAllModel.class_SubList[index].ControlRequestMapping;
                    this.textEdit102.Text = class_SelectAllModel.class_SubList[index].ServiceInterFaceName;
                    this.textEdit103.Text = class_SelectAllModel.class_SubList[index].ServiceClassName;

                    if (this.panelControl4.Height > this.simpleButton2.Height + 5)
                        this.simpleButton2.Text = "折叠";
                    else
                        this.simpleButton2.Text = "展开";

                }
                #endregion

                #region 表一
                index++;
                if (class_SelectAllModel.class_SubList.Count > index)
                {
                    this.textEdit31.Text = class_SelectAllModel.class_SubList[index].NameSpace;
                    this.memoEdit19.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[index].DTOContent);
                    this.radioGroup5.SelectedIndex = class_SelectAllModel.class_SubList[index].JoinType;
                    this.radioGroup1.SelectedIndex = class_SelectAllModel.class_SubList[index].InnerType;
                    this.buttonEdit1.Text = class_SelectAllModel.class_SubList[index].OutFieldName;
                    this.buttonEdit2.Text = class_SelectAllModel.class_SubList[index].MainTableFieldName;

                    this.textEdit97.Text = class_SelectAllModel.class_SubList[index].DtoClassName;
                    this.panelControl17.Height = class_SelectAllModel.class_SubList[index].PanelHeight;
                }
                #endregion

                #region 表二
                index++;
                if (class_SelectAllModel.class_SubList.Count > index)
                {
                    this.textEdit41.Text = class_SelectAllModel.class_SubList[index].NameSpace;
                    this.memoEdit28.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[index].DTOContent);

                    this.radioGroup6.SelectedIndex = class_SelectAllModel.class_SubList[index].JoinType;
                    this.radioGroup2.SelectedIndex = class_SelectAllModel.class_SubList[index].InnerType;
                    this.buttonEdit4.Text = class_SelectAllModel.class_SubList[index].OutFieldName;
                    this.buttonEdit3.Text = class_SelectAllModel.class_SubList[index].MainTableFieldName;
                    this.radioGroup29.SelectedIndex = class_SelectAllModel.class_SubList[index].TableNo;
                    this.checkEdit14.Checked = class_SelectAllModel.class_SubList[index].ControlMainCode;

                    this.textEdit95.Text = class_SelectAllModel.class_SubList[index].DtoClassName;
                    this.panelControl28.Height = class_SelectAllModel.class_SubList[index].PanelHeight;
                }
                #endregion

                #region 表三
                index++;
                if (class_SelectAllModel.class_SubList.Count > index)
                {
                    this.textEdit73.Text = class_SelectAllModel.class_SubList[index].NameSpace;
                    this.memoEdit42.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[index].DTOContent);
                    //Join方式、association、collection
                    this.radioGroup17.SelectedIndex = class_SelectAllModel.class_SubList[index].JoinType;
                    //左链接、右链接
                    this.radioGroup19.SelectedIndex = class_SelectAllModel.class_SubList[index].InnerType;
                    //外键字段名称
                    this.buttonEdit6.Text = class_SelectAllModel.class_SubList[index].OutFieldName;
                    //关联字段名称
                    this.buttonEdit5.Text = class_SelectAllModel.class_SubList[index].MainTableFieldName;
                    this.radioGroup30.SelectedIndex = class_SelectAllModel.class_SubList[index].TableNo;
                    this.checkEdit16.Checked = class_SelectAllModel.class_SubList[index].ControlMainCode;

                    this.textEdit91.Text = class_SelectAllModel.class_SubList[index].DtoClassName;
                    this.panelControl44.Height = class_SelectAllModel.class_SubList[index].PanelHeight;
                }
                #endregion

                #region 表四
                index++;
                if (class_SelectAllModel.class_SubList.Count > index)
                {
                    this.textEdit82.Text = class_SelectAllModel.class_SubList[index].NameSpace;
                    this.memoEdit52.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[index].DTOContent);
                    //Join方式、association、collection
                    this.radioGroup20.SelectedIndex = class_SelectAllModel.class_SubList[index].JoinType;
                    //左链接、右链接
                    this.radioGroup22.SelectedIndex = class_SelectAllModel.class_SubList[index].InnerType;
                    //外键字段名称
                    this.buttonEdit8.Text = class_SelectAllModel.class_SubList[index].OutFieldName;
                    //关联字段名称
                    this.buttonEdit7.Text = class_SelectAllModel.class_SubList[index].MainTableFieldName;
                    this.radioGroup31.SelectedIndex = class_SelectAllModel.class_SubList[index].TableNo;
                    this.checkEdit18.Checked = class_SelectAllModel.class_SubList[index].ControlMainCode;

                    this.textEdit93.Text = class_SelectAllModel.class_SubList[index].DtoClassName;
                    this.panelControl45.Height = class_SelectAllModel.class_SubList[index].PanelHeight;
                }
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
                        for (int i = 0; i < class_SelectAllModel.class_SubList.Count; i++)
                        {
                            if (class_SelectAllModel.class_SubList[i] != null)
                                this.AddUseTableData(class_SelectAllModel.class_SubList[i].TableName, class_SelectAllModel.class_SubList[i].AliasName, i, false);
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
            this.checkEdit3.Checked = false;
            this.checkEdit14.Checked = false;
            this.checkEdit16.Checked = false;
            this.checkEdit18.Checked = false;

            radioGroup5.SelectedIndex = 0;
            radioGroup6.SelectedIndex = 0;
            radioGroup29.SelectedIndex = 1;
            radioGroup30.SelectedIndex = 2;
            radioGroup31.SelectedIndex = 3;
            radioGroup1.SelectedIndex = 0;
            radioGroup2.SelectedIndex = 0;

            SetIniSkin(publicSkinName);
            xtraTabControl3.SelectedTabPageIndex = 0;
            xtraTabControl4.SelectedTabPageIndex = 0;
            xtraTabControl5.SelectedTabPageIndex = 0;

            this.bandedGridColumn1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            myTableNameList = new List<string>();
            myTableContentList = new List<string>();

            #region ButtonEdit
            Class_SetButtonEdit class_SetButtonEdit = new Class_SetButtonEdit();
            class_SetButtonEdit.SetButtonEdit(this.buttonEdit1);
            class_SetButtonEdit.SetButtonEdit(this.buttonEdit2);
            class_SetButtonEdit.SetButtonEdit(this.buttonEdit3);
            class_SetButtonEdit.SetButtonEdit(this.buttonEdit4);
            class_SetButtonEdit.SetButtonEdit(this.buttonEdit5);
            class_SetButtonEdit.SetButtonEdit(this.buttonEdit6);
            class_SetButtonEdit.SetButtonEdit(this.buttonEdit7);
            class_SetButtonEdit.SetButtonEdit(this.buttonEdit8);
            #endregion

            #region Grid
            GridC gridC = new GridC();
            gridC.pub_SetBandedGridViewStyle(this.bandedGridView1);
            gridC.pub_SetBandedGridViewStyle(this.bandedGridView2);
            gridC.pub_SetBandedGridViewStyle(this.bandedGridView3);
            gridC.pub_SetBandedGridViewStyle(this.bandedGridView4);
            gridC.pub_SetBandedGridViewStyle(this.bandedGridView5);
            gridC.SetGridBar(this.gridControl1);
            gridC.SetGridBar(this.gridControl2);
            gridC.SetGridBar(this.gridControl3);
            gridC.SetGridBar(this.gridControl4);
            gridC.SetGridBar(this.gridControl5);
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
            class_SetMemoEdit.SetMemoEdit(this.memoEdit19);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit28);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit31);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit42);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit52);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit54);
            #endregion

            #region TextEdit
            Class_SetTextEdit class_SetTextEdit = new Class_SetTextEdit();
            class_SetTextEdit.SetTextEdit(this.textEdit19);
            class_SetTextEdit.SetTextEdit(this.textEdit25);
            class_SetTextEdit.SetTextEdit(this.textEdit91);
            class_SetTextEdit.SetTextEdit(this.textEdit93);
            class_SetTextEdit.SetTextEdit(this.textEdit95);
            class_SetTextEdit.SetTextEdit(this.textEdit97);
            class_SetTextEdit.SetTextEdit(this.textEdit44);

            class_SetTextEdit.SetTextEdit(this.textEdit13, Color.Yellow);
            class_SetTextEdit.SetTextEdit(this.textEdit10, Color.Yellow);
            class_SetTextEdit.SetTextEdit(this.textEdit11, Color.Yellow);
            class_SetTextEdit.SetTextEdit(this.textEdit12, Color.Yellow);
            class_SetTextEdit.SetTextEdit(this.textEdit61, Color.Yellow);
            class_SetTextEdit.SetTextEdit(this.textEdit57, Color.Yellow);

            class_SetTextEdit.SetTextEdit(this.textEdit14, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit15, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit16, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit20, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit22, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit24, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit31, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit41, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit46, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit47, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit73, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit82, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit99, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit100, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit19, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit101, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit54, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit102, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit103, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit97, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit95, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit91, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit93, Color.SkyBlue);

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
            class_SetTextEdit.SetTextEdit(this.textEdit60, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit58, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit59, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit62, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit63, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit64, true, Color.GreenYellow);
            #endregion

            #region radioGroup
            this.radioGroup7.SelectedIndex = 0;
            this.radioGroup9.SelectedIndex = 0;
            #endregion

            this.xtraTabControl8.Images = this.xtraTabControl5.Images;
            for (int index = 0; index < this.xtraTabControl8.TabPages.Count; index++)
            {
                if (index < 5)
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

            this.simpleButton2.Text = "折叠";
        }

        private void SetIniSkin(string skinName)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
        }

        private void Form_Select_Shown(object sender, EventArgs e)
        {
            this.dockPanel1.Size = new System.Drawing.Size(295, 288);
            this.dockPanel2.Size = new System.Drawing.Size(314, 288);
            //this.dockPanel3.Size = new System.Drawing.Size(552, 266);
        }
        /// <summary>
        /// 加入汇总函数
        /// </summary>
        /// <param name="repositoryItemComboBox"></param>
        /// <param name="FieldType"></param>
        private void AddColumnComboxTotalFunction(RepositoryItemComboBox repositoryItemComboBox, string FieldType)
        {
            repositoryItemComboBox.Items.Clear();
            foreach (string row in class_InterFaceDataBase.GetTotalFunctionList(FieldType))
            {
                repositoryItemComboBox.Items.Add(row);
            }
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
            switch (PageSelectIndex)
            {
                case 0:
                    {
                        this.gridControl1.DataSource = class_InterFaceDataBase.GetMainTableStruct<Class_SelectAllModel>(TableName, PageSelectIndex, SelectSelectDefault);
                        textEdit1.Text = TableName;
                        if (class_SelectAllModel.class_SubList.Count > PageSelectIndex && class_SelectAllModel.class_SubList[PageSelectIndex].AliasName != null)
                            textEdit10.Text = class_SelectAllModel.class_SubList[PageSelectIndex].AliasName;
                        else
                            textEdit10.Text = TableAlias.Length == 0 ? "main" : TableAlias;
                        AddColumnRepositoryCombox(this.repositoryItemComboBox2);
                        AddColumnComboxFunctionByDataType(this.repositoryItemComboBox1, "");
                        AddColumnComboxHavingFunctionByDataType(this.repositoryItemComboBox7, "");
                        AddColumnComboxTotalFunction(this.repositoryItemComboBox41, "");
                    }
                    break;
                case 1:
                    {
                        this.gridControl2.DataSource = class_InterFaceDataBase.GetMainTableStruct<Class_SelectAllModel>(TableName, PageSelectIndex, SelectSelectDefault);
                        textEdit6.Text = TableName;
                        if (class_SelectAllModel.class_SubList.Count > PageSelectIndex && class_SelectAllModel.class_SubList[PageSelectIndex].AliasName != null)
                        {
                            textEdit11.Text = class_SelectAllModel.class_SubList[PageSelectIndex].AliasName;
                            this.radioGroup1.SelectedIndex = class_SelectAllModel.class_SubList[PageSelectIndex].LinkType;
                        }
                        else
                            textEdit11.Text = TableAlias.Length == 0 ? "Sub" + PageSelectIndex.ToString() : TableAlias;
                        AddColumnRepositoryCombox(this.repositoryItemComboBox10);
                        AddColumnComboxFunctionByDataType(this.repositoryItemComboBox10, "");
                        AddColumnComboxHavingFunctionByDataType(this.repositoryItemComboBox16, "");
                        AddColumnComboxTotalFunction(this.repositoryItemComboBox11, "");
                    }
                    break;
                case 2:
                    {
                        this.gridControl3.DataSource = class_InterFaceDataBase.GetMainTableStruct<Class_SelectAllModel>(TableName, PageSelectIndex, SelectSelectDefault);
                        textEdit9.Text = TableName;
                        if (class_SelectAllModel.class_SubList.Count > PageSelectIndex && class_SelectAllModel.class_SubList[PageSelectIndex].AliasName != null)
                        {
                            textEdit12.Text = class_SelectAllModel.class_SubList[PageSelectIndex].AliasName;
                            this.radioGroup2.SelectedIndex = class_SelectAllModel.class_SubList[PageSelectIndex].LinkType;
                        }
                        else
                            textEdit12.Text = TableAlias.Length == 0 ? "Sub" + PageSelectIndex.ToString() : TableAlias;

                        AddColumnComboxFunctionByDataType(this.repositoryItemComboBox22, "");
                        AddColumnComboxHavingFunctionByDataType(this.repositoryItemComboBox28, "");
                        AddColumnComboxTotalFunction(this.repositoryItemComboBox23, "");
                    }
                    break;
                case 3:
                    {
                        this.gridControl4.DataSource = class_InterFaceDataBase.GetMainTableStruct<Class_SelectAllModel>(TableName, PageSelectIndex, SelectSelectDefault);
                        textEdit60.Text = TableName;
                        if (class_SelectAllModel.class_SubList.Count > PageSelectIndex && class_SelectAllModel.class_SubList[PageSelectIndex].AliasName != null)
                        {
                            textEdit57.Text = class_SelectAllModel.class_SubList[PageSelectIndex].AliasName;
                            this.radioGroup19.SelectedIndex = class_SelectAllModel.class_SubList[PageSelectIndex].LinkType;
                        }
                        else
                            textEdit57.Text = TableAlias.Length == 0 ? "Sub" + PageSelectIndex.ToString() : TableAlias;

                        AddColumnComboxFunctionByDataType(this.repositoryItemComboBox34, "");
                        AddColumnComboxHavingFunctionByDataType(this.repositoryItemComboBox40, "");
                        AddColumnComboxTotalFunction(this.repositoryItemComboBox35, "");
                    }
                    break;
                case 4:
                    {
                        this.gridControl5.DataSource = class_InterFaceDataBase.GetMainTableStruct<Class_SelectAllModel>(TableName, PageSelectIndex, SelectSelectDefault);
                        textEdit64.Text = TableName;
                        if (class_SelectAllModel.class_SubList.Count > PageSelectIndex && class_SelectAllModel.class_SubList[PageSelectIndex].AliasName != null)
                        {
                            textEdit61.Text = class_SelectAllModel.class_SubList[PageSelectIndex].AliasName;
                            this.radioGroup22.SelectedIndex = class_SelectAllModel.class_SubList[PageSelectIndex].LinkType;
                        }
                        else
                            textEdit61.Text = TableAlias.Length == 0 ? "Sub" + PageSelectIndex.ToString() : TableAlias;

                        AddColumnComboxFunctionByDataType(this.repositoryItemComboBox50, "");
                        AddColumnComboxHavingFunctionByDataType(this.repositoryItemComboBox56, "");
                        AddColumnComboxTotalFunction(this.repositoryItemComboBox51, "");
                    }
                    break;
                default:
                    break;
            }
            if (PageSelectIndex > 0)
            {
                this.xtraTabControl5.TabPages[PageSelectIndex].Text = string.Format("表{1}：{0}", TableName, PageSelectIndex);
                this.xtraTabControl8.TabPages[PageSelectIndex].Text = string.Format("表{1}：{0}", TableName, PageSelectIndex);
            }
            else
            {
                this.xtraTabControl5.TabPages[PageSelectIndex].Text = string.Format("主表：{0}", TableName);
                this.xtraTabControl8.TabPages[PageSelectIndex].Text = string.Format("主表：{0}", TableName);
            }
            //if (PageSelectIndex > -1)
            //    this.xtraTabControl5.SelectedTabPageIndex = PageSelectIndex;
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
                        case 1:
                            bandedGridView = gridControl2.MainView as BandedGridView;
                            break;
                        case 2:
                            bandedGridView = gridControl3.MainView as BandedGridView;
                            break;
                        case 3:
                            bandedGridView = gridControl4.MainView as BandedGridView;
                            break;
                        case 4:
                            bandedGridView = gridControl5.MainView as BandedGridView;
                            break;
                        default:
                            bandedGridView = gridControl1.MainView as BandedGridView;
                            break;
                    }
                    IsOk = bandedGridView.RowCount > 0 ? true : false;
                }
                if (IsOk)
                {
                    this.xtraTabControl5.SelectedTabPageIndex = PageIndex;
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
                        MessageBox.Show("主表为空时，不能选择此前表！"
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
            ToPage(null, this.xtraTabControl5.SelectedTabPageIndex);
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
                    string AliasName = class_SQLiteOperator.GetTableAlias(class_SelectAllModel.class_SelectDataBase.dataSourceUrl
                        , class_SelectAllModel.class_SelectDataBase.dataBaseName, row);
                    this.listBoxControl3.Items.Add(AliasName == null ? "" : AliasName);
                }
                foreach (string row in myTableContentList)
                {
                    this.listBoxControl2.Items.Add(row);
                }

                if (class_SelectAllModel.LastSelectTableName != null)
                {
                    int LastIndex = myTableNameList.IndexOf(class_SelectAllModel.LastSelectTableName);
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
        /// <param name="MainFieldName">主表关联字段</param>
        /// <param name="AddPoint"></param>
        /// <param name="AliasName">别名</param>
        /// <param name="TableNo">关系表的序号</param>
        /// <param name="MapMainCode">仅生成Map层主体代码</param>
        /// <param name="ControlMainCode">仅生成Control层主体代码</param>
        /// <returns></returns>
        private Class_SelectAllModel.Class_Sub DataViewIntoClass(BandedGridView bandedGridView,
            string OutFieldName, int LinkType, int CountToCount, string TableName,
            string MainFieldName, bool AddPoint, string AliasName,
            int TableNo, bool ControlMainCode)
        {
            Class_SelectAllModel.Class_Sub class_Sub = new Class_SelectAllModel.Class_Sub();
            List<Class_SelectAllModel.Class_Field> class_Fields = new List<Class_SelectAllModel.Class_Field>();
            for (int j = 0; j < bandedGridView.RowCount; j++)
            {
                DataRow dataRow = bandedGridView.GetDataRow(j);
                if (Convert.ToBoolean(dataRow["FieldIsKey"]))
                {
                    class_Sub.MainFieldName = dataRow["FieldName"].ToString();
                    class_Sub.AddPoint = class_InterFaceDataBase.IsAddPoint(dataRow["FieldType"].ToString());
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
                    class_Field.TotalFunctionName = dataRow["TotalFunctionName"].ToString();
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
            class_Sub.LinkType = LinkType;
            class_Sub.OutFieldName = OutFieldName;
            class_Sub.MainFieldName = MainFieldName;
            class_Sub.ControlMainCode = ControlMainCode;

            if (TableNo > -1)
                class_Sub.TableNo = TableNo;

            if (CountToCount > -1)
            {
                class_Sub.CountToCount = CountToCount;
            }

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
            BandedGridView[] bandedGridViews = new BandedGridView[this.xtraTabControl5.TabPages.Count];
            bandedGridViews[0] = gridControl1.MainView as BandedGridView;
            bandedGridViews[1] = gridControl2.MainView as BandedGridView;
            bandedGridViews[2] = gridControl3.MainView as BandedGridView;
            bandedGridViews[3] = gridControl4.MainView as BandedGridView;
            bandedGridViews[4] = gridControl5.MainView as BandedGridView;
            for (int i = 0; i < bandedGridViews.Length; i++)
            {
                BandedGridView item = bandedGridViews[i];
                if (item.RowCount > 0)
                {
                    int FocuedCount = item.FocusedRowHandle;
                    if (FocuedCount == item.RowCount - 1)
                        item.FocusedRowHandle = 0;
                    if (FocuedCount == 0)
                        item.FocusedRowHandle = item.RowCount - 1;
                    if (FocuedCount > 0 && FocuedCount < item.RowCount - 1)
                        item.FocusedRowHandle = 0;
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
                if (class_SelectAllModel.class_Create.MicroServiceName == null || class_SelectAllModel.class_Create.MicroServiceName.Length == 0)
                {
                    MessageBox.Show("属性->生成配置:\r\n   微服务名不能为空！", "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IsOk = false;
                }
            }
            #endregion

            #region 创建者名非空验证
            if (IsOk)
            {
                if (class_SelectAllModel.class_Create.CreateMan == null || class_SelectAllModel.class_Create.CreateMan.Length == 0)
                {
                    MessageBox.Show("属性->生成配置:\r\n   创建者姓名不能为空！", "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    IsOk = false;
                }
            }
            #endregion

            int SelectCount = 0;
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
                            if (Convert.ToBoolean(dataRow["SelectSelect"]))
                                SelectCount++;
                            if (Convert.ToBoolean(dataRow["WhereSelect"]))
                                WhereCount++;
                        }
                    }
                }
                IsOk = SelectCount > 0 ? true : false;
                if (!IsOk)
                {
                    MessageBox.Show("请选择Select字段！"
                        , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.xtraTabControl3.SelectedTabPageIndex = 0;
                    this.xtraTabControl5.SelectedTabPageIndex = 0;
                }
            }
            #endregion

            #region 输入参数非空验证
            if (IsOk)
            {
                int ActiveIndex = -1;
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
                                MessageBox.Show(string.Format("主表:\r\n   字段[{0}]的WHERE值不能为空！", FieldName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                                ActiveIndex = 0;
                            }
                        }
                    }
                }
                bandedGridView = gridControl2.MainView as BandedGridView;
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
                                ActiveIndex = 1;
                                MessageBox.Show(string.Format("从表{1}:\r\n   字段[{0}]的WHERE值不能为空！"
                                    , FieldName, ActiveIndex.ToString())
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                            }
                        }
                    }
                }
                bandedGridView = gridControl3.MainView as BandedGridView;
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
                                ActiveIndex = 2;
                                MessageBox.Show(string.Format("从表{1}:\r\n   字段[{0}]的WHERE值不能为空！"
                                    , FieldName, ActiveIndex.ToString())
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                            }
                        }
                    }
                }
                bandedGridView = gridControl4.MainView as BandedGridView;
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
                                ActiveIndex = 3;
                                MessageBox.Show(string.Format("从表{1}:\r\n   字段[{0}]的WHERE值不能为空！"
                                    , FieldName, ActiveIndex.ToString())
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                            }
                        }
                    }
                }
                bandedGridView = gridControl5.MainView as BandedGridView;
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
                                ActiveIndex = 4;
                                MessageBox.Show(string.Format("从表{1}:\r\n   字段[{0}]的WHERE值不能为空！"
                                    , FieldName, ActiveIndex.ToString())
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                            }
                        }
                    }
                }
                if (!IsOk)
                {
                    this.xtraTabControl3.SelectedTabPageIndex = 0;
                    this.xtraTabControl5.SelectedTabPageIndex = ActiveIndex;
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

            #region 多表时的别名非空验证
            if (IsOk)
            {
                int ActiveIndex = -1;
                if (this.gridControl2.MainView.RowCount
                    + this.gridControl3.MainView.RowCount
                    + this.gridControl4.MainView.RowCount
                    + this.gridControl5.MainView.RowCount > 0)
                {
                    if (IsOk && this.gridControl1.MainView.RowCount > 0)
                    {
                        if (textEdit10.Text == null || textEdit10.Text.Length == 0)
                        {
                            MessageBox.Show("主表:\r\n   表别名不能为空！", "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            IsOk = false;
                            ActiveIndex = 1;
                        }
                    }
                    if (IsOk && this.gridControl2.MainView.RowCount > 0)
                    {
                        if (textEdit11.Text == null || textEdit11.Text.Length == 0)
                        {
                            MessageBox.Show("从表1:\r\n   表别名不能为空！", "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            IsOk = false;
                            ActiveIndex = 2;
                        }
                    }
                    if (IsOk && this.gridControl3.MainView.RowCount > 0)
                    {
                        if (textEdit12.Text == null || textEdit12.Text.Length == 0)
                        {
                            MessageBox.Show("从表2:\r\n   表别名不能为空！", "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            IsOk = false;
                            ActiveIndex = 3;
                        }
                    }
                    if (IsOk && this.gridControl4.MainView.RowCount > 0)
                    {
                        if (textEdit57.Text == null || textEdit57.Text.Length == 0)
                        {
                            MessageBox.Show("从表3:\r\n   表别名不能为空！", "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            IsOk = false;
                            ActiveIndex = 4;
                        }
                    }
                    if (IsOk && this.gridControl5.MainView.RowCount > 0)
                    {
                        if (textEdit61.Text == null || textEdit61.Text.Length == 0)
                        {
                            MessageBox.Show("从表4:\r\n   表别名不能为空！", "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            IsOk = false;
                            ActiveIndex = 5;
                        }
                    }
                    if (!IsOk)
                    {
                        this.xtraTabControl3.SelectedTabPageIndex = 1;
                        this.xtraTabControl5.SelectedTabPageIndex = ActiveIndex;
                    }
                }
            }
            #endregion

            #region 字段类型与函数合法性
            if (IsOk)
            {
                int ActiveIndex = -1;
                BandedGridView bandedGridView = gridControl1.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    int index = 0;
                    while (IsOk && index < bandedGridView.RowCount)
                    {
                        DataRow dataRow = bandedGridView.GetDataRow(index++);
                        if (Convert.ToBoolean(dataRow["SelectSelect"]))
                        {
                            string FieldName = dataRow["FieldName"].ToString();
                            string FieldType = dataRow["FieldType"].ToString();
                            string CaseWhen = dataRow["CaseWhen"].ToString();
                            if ((CaseWhen != null) && (CaseWhen.Length > 0))
                                FieldType = "varchar";
                            string FunctionName = dataRow["FunctionName"].ToString();
                            if (!class_InterFaceDataBase.FieldTypeAndFunction(FieldType, FunctionName))
                            {
                                MessageBox.Show(string.Format("主表:\r\n   字段[{0}]的类型[{1}],不用使用函数[{2}]！", FieldName, FieldType, FunctionName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                                ActiveIndex = 0;
                            }
                        }
                    }
                }
                bandedGridView = gridControl2.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    int index = 0;
                    while (IsOk && index < bandedGridView.RowCount)
                    {
                        DataRow dataRow = bandedGridView.GetDataRow(index++);
                        if (Convert.ToBoolean(dataRow["SelectSelect"]))
                        {
                            string FieldName = dataRow["FieldName"].ToString();
                            string FieldType = dataRow["FieldType"].ToString();
                            string FunctionName = dataRow["FunctionName"].ToString();
                            if (!class_InterFaceDataBase.FieldTypeAndFunction(FieldType, FunctionName))
                            {
                                MessageBox.Show(string.Format("从表1:\r\n   字段[{0}]的类型[{1}],不用使用函数[{2}]！", FieldName, FieldType, FunctionName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                                ActiveIndex = 1;
                            }
                        }
                    }
                }
                bandedGridView = gridControl3.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    int index = 0;
                    while (IsOk && index < bandedGridView.RowCount)
                    {
                        DataRow dataRow = bandedGridView.GetDataRow(index++);
                        if (Convert.ToBoolean(dataRow["SelectSelect"]))
                        {
                            string FieldName = dataRow["FieldName"].ToString();
                            string FieldType = dataRow["FieldType"].ToString();
                            string FunctionName = dataRow["FunctionName"].ToString();
                            if (!class_InterFaceDataBase.FieldTypeAndFunction(FieldType, FunctionName))
                            {
                                MessageBox.Show(string.Format("从表2:\r\n   字段[{0}]的类型[{1}],不用使用函数[{2}]！", FieldName, FieldType, FunctionName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                                ActiveIndex = 2;
                            }
                        }
                    }
                }
                bandedGridView = gridControl4.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    int index = 0;
                    while (IsOk && index < bandedGridView.RowCount)
                    {
                        DataRow dataRow = bandedGridView.GetDataRow(index++);
                        if (Convert.ToBoolean(dataRow["SelectSelect"]))
                        {
                            string FieldName = dataRow["FieldName"].ToString();
                            string FieldType = dataRow["FieldType"].ToString();
                            string FunctionName = dataRow["FunctionName"].ToString();
                            if (!class_InterFaceDataBase.FieldTypeAndFunction(FieldType, FunctionName))
                            {
                                MessageBox.Show(string.Format("从表3:\r\n   字段[{0}]的类型[{1}],不用使用函数[{2}]！", FieldName, FieldType, FunctionName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                                ActiveIndex = 3;
                            }
                        }
                    }
                }
                bandedGridView = gridControl5.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    int index = 0;
                    while (IsOk && index < bandedGridView.RowCount)
                    {
                        DataRow dataRow = bandedGridView.GetDataRow(index++);
                        if (Convert.ToBoolean(dataRow["SelectSelect"]))
                        {
                            string FieldName = dataRow["FieldName"].ToString();
                            string FieldType = dataRow["FieldType"].ToString();
                            string FunctionName = dataRow["FunctionName"].ToString();
                            if (!class_InterFaceDataBase.FieldTypeAndFunction(FieldType, FunctionName))
                            {
                                MessageBox.Show(string.Format("从表4:\r\n   字段[{0}]的类型[{1}],不用使用函数[{2}]！", FieldName, FieldType, FunctionName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                                ActiveIndex = 4;
                            }
                        }
                    }
                }
                if (!IsOk)
                {
                    this.xtraTabControl3.SelectedTabPageIndex = 0;
                    this.xtraTabControl5.SelectedTabPageIndex = ActiveIndex;
                }
            }
            #endregion

            #region 字段类型、Having函数与值合法性
            if (IsOk)
            {
                BandedGridView bandedGridView = gridControl1.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    int index = 0;
                    while (IsOk && index < bandedGridView.RowCount)
                    {
                        DataRow dataRow = bandedGridView.GetDataRow(index++);
                        if (Convert.ToBoolean(dataRow["HavingSelect"]))
                        {
                            string FieldName = dataRow["FieldName"].ToString();
                            string FieldType = dataRow["FieldType"].ToString();
                            string FunctionName = dataRow["HavingFunction"].ToString();
                            string HavingValue = dataRow["HavingValue"].ToString();
                            if (!class_InterFaceDataBase.FieldTypeAndFunction(FieldType, FunctionName))
                            {
                                MessageBox.Show(string.Format("主表:\r\n   字段[{0}]的类型[{1}],Having中不用使用函数[{2}]！", FieldName, FieldType, FunctionName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                            }
                            if (IsOk && (HavingValue == null || HavingValue.Length == 0))
                            {
                                MessageBox.Show(string.Format("主表:\r\n   字段[{0}]的Having函数值不能这空！", FieldName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                            }
                        }
                    }
                }
                bandedGridView = gridControl2.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    int index = 0;
                    while (IsOk && index < bandedGridView.RowCount)
                    {
                        DataRow dataRow = bandedGridView.GetDataRow(index++);
                        if (Convert.ToBoolean(dataRow["HavingSelect"]))
                        {
                            string FieldName = dataRow["FieldName"].ToString();
                            string FieldType = dataRow["FieldType"].ToString();
                            string FunctionName = dataRow["HavingFunction"].ToString();
                            string HavingValue = dataRow["HavingValue"].ToString();
                            if (!class_InterFaceDataBase.FieldTypeAndFunction(FieldType, FunctionName))
                            {
                                MessageBox.Show(string.Format("从表1:\r\n   字段[{0}]的类型[{1}],Having中不用使用函数[{2}]！", FieldName, FieldType, FunctionName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                            }
                            if (IsOk && (HavingValue == null || HavingValue.Length == 0))
                            {
                                MessageBox.Show(string.Format("从表1:\r\n   字段[{0}]的Having函数值不能这空！", FieldName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                            }
                        }
                    }
                }
                bandedGridView = gridControl3.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    int index = 0;
                    while (IsOk && index < bandedGridView.RowCount)
                    {
                        DataRow dataRow = bandedGridView.GetDataRow(index++);
                        if (Convert.ToBoolean(dataRow["HavingSelect"]))
                        {
                            string FieldName = dataRow["FieldName"].ToString();
                            string FieldType = dataRow["FieldType"].ToString();
                            string FunctionName = dataRow["HavingFunction"].ToString();
                            string HavingValue = dataRow["HavingValue"].ToString();
                            if (!class_InterFaceDataBase.FieldTypeAndFunction(FieldType, FunctionName))
                            {
                                MessageBox.Show(string.Format("从表2:\r\n   字段[{0}]的类型[{1}],Having中不用使用函数[{2}]！", FieldName, FieldType, FunctionName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                            }
                            if (IsOk && (HavingValue == null || HavingValue.Length == 0))
                            {
                                MessageBox.Show(string.Format("从表2:\r\n   字段[{0}]的Having函数值不能这空！", FieldName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                            }
                        }
                    }
                }
                bandedGridView = gridControl4.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    int index = 0;
                    while (IsOk && index < bandedGridView.RowCount)
                    {
                        DataRow dataRow = bandedGridView.GetDataRow(index++);
                        if (Convert.ToBoolean(dataRow["HavingSelect"]))
                        {
                            string FieldName = dataRow["FieldName"].ToString();
                            string FieldType = dataRow["FieldType"].ToString();
                            string FunctionName = dataRow["HavingFunction"].ToString();
                            string HavingValue = dataRow["HavingValue"].ToString();
                            if (!class_InterFaceDataBase.FieldTypeAndFunction(FieldType, FunctionName))
                            {
                                MessageBox.Show(string.Format("从表3:\r\n   字段[{0}]的类型[{1}],Having中不用使用函数[{2}]！", FieldName, FieldType, FunctionName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                            }
                            if (IsOk && (HavingValue == null || HavingValue.Length == 0))
                            {
                                MessageBox.Show(string.Format("从表3:\r\n   字段[{0}]的Having函数值不能这空！", FieldName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                            }
                        }
                    }
                }
                bandedGridView = gridControl5.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    int index = 0;
                    while (IsOk && index < bandedGridView.RowCount)
                    {
                        DataRow dataRow = bandedGridView.GetDataRow(index++);
                        if (Convert.ToBoolean(dataRow["HavingSelect"]))
                        {
                            string FieldName = dataRow["FieldName"].ToString();
                            string FieldType = dataRow["FieldType"].ToString();
                            string FunctionName = dataRow["HavingFunction"].ToString();
                            string HavingValue = dataRow["HavingValue"].ToString();
                            if (!class_InterFaceDataBase.FieldTypeAndFunction(FieldType, FunctionName))
                            {
                                MessageBox.Show(string.Format("从表4:\r\n   字段[{0}]的类型[{1}],Having中不用使用函数[{2}]！", FieldName, FieldType, FunctionName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                            }
                            if (IsOk && (HavingValue == null || HavingValue.Length == 0))
                            {
                                MessageBox.Show(string.Format("从表4:\r\n   字段[{0}]的Having函数值不能这空！", FieldName)
                                    , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                IsOk = false;
                            }
                        }
                    }
                }
            }
            #endregion

            #region 关联外键字段非空验证
            if (IsOk)
            {
                int index = 1;
                int ActiveIndex = -1;
                BandedGridView bandedGridView = gridControl2.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    if (IsOk && (this.buttonEdit1.Text == null || this.buttonEdit1.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("从表{1}:\r\n   {0}键字段不能为空！"
                            , "主", index.ToString())
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                        ActiveIndex = 1;
                    }
                    if (IsOk && (this.buttonEdit2.Text == null || this.buttonEdit2.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("从表{1}:\r\n   {0}键字段不能为空！"
                            , "外", index.ToString())
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                        ActiveIndex = 1;
                    }
                }
                index++;
                bandedGridView = gridControl3.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    if (IsOk && (this.buttonEdit4.Text == null || this.buttonEdit4.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("从表{1}:\r\n   {0}键字段不能为空！"
                            , "主", index.ToString())
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                        ActiveIndex = 2;
                    }
                    if (IsOk && (this.buttonEdit3.Text == null || this.buttonEdit3.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("从表{1}:\r\n   {0}键字段不能为空！"
                            , "外", index.ToString())
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                        ActiveIndex = 2;
                    }
                }
                index++;
                bandedGridView = gridControl4.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    if (IsOk && (this.buttonEdit6.Text == null || this.buttonEdit6.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("从表{1}:\r\n   {0}键字段不能为空！"
                            , "主", index.ToString())
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                        ActiveIndex = 3;
                    }
                    if (IsOk && (this.buttonEdit5.Text == null || this.buttonEdit5.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("从表{1}:\r\n   {0}键字段不能为空！"
                            , "外", index.ToString())
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                        ActiveIndex = 3;
                    }
                }
                index++;
                bandedGridView = gridControl5.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    if (IsOk && (this.buttonEdit8.Text == null || this.buttonEdit8.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("从表{1}:\r\n   {0}键字段不能为空！"
                            , "主", index.ToString())
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                        ActiveIndex = 4;
                    }
                    if (IsOk && (this.buttonEdit7.Text == null || this.buttonEdit7.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("从表{1}:\r\n   {0}键字段不能为空！"
                            , "外", index.ToString())
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                        ActiveIndex = 4;
                    }
                }
                if (!IsOk)
                {
                    this.xtraTabControl3.SelectedTabPageIndex = 0;
                    this.xtraTabControl5.SelectedTabPageIndex = ActiveIndex;
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
                        MessageBox.Show(string.Format("{0}表:NameSpace不能为空！", "主")
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (IsOk && (this.textEdit19.Text == null || this.textEdit19.Text.Length == 0))
                    {
                        MessageBox.Show(string.Format("{0}表:Dto类名不能为空！", "主")
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (!IsOk)
                        ActiveIndex = 0;
                }
                bandedGridView = gridControl2.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    if (this.textEdit31.Text == null || this.textEdit31.Text.Length == 0)
                    {
                        ActiveIndex = 1;
                        MessageBox.Show(string.Format("{0}表{1}:NameSpace不能为空！", "从", ActiveIndex)
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (IsOk && (this.textEdit97.Text == null || this.textEdit97.Text.Length == 0))
                    {
                        ActiveIndex = 1;
                        MessageBox.Show(string.Format("{0}表{1}:Dto类名不能为空！", "从", ActiveIndex)
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (!IsOk)
                        ActiveIndex = 1;
                }
                bandedGridView = gridControl3.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    if (this.textEdit41.Text == null || this.textEdit41.Text.Length == 0)
                    {
                        ActiveIndex = 2;
                        MessageBox.Show(string.Format("{0}表{1}:NameSpace不能为空！", "从", ActiveIndex)
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (IsOk && (this.textEdit95.Text == null || this.textEdit95.Text.Length == 0))
                    {
                        ActiveIndex = 2;
                        MessageBox.Show(string.Format("{0}表{1}:Dto类名不能为空！", "从", ActiveIndex)
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (!IsOk)
                        ActiveIndex = 2;
                }
                bandedGridView = gridControl4.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    if (this.textEdit73.Text == null || this.textEdit73.Text.Length == 0)
                    {
                        ActiveIndex = 3;
                        MessageBox.Show(string.Format("{0}表{1}:NameSpace不能为空！", "从", ActiveIndex)
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (IsOk && (this.textEdit91.Text == null || this.textEdit91.Text.Length == 0))
                    {
                        ActiveIndex = 3;
                        MessageBox.Show(string.Format("{0}表{1}:Dto类名不能为空！", "从", ActiveIndex)
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (!IsOk)
                        ActiveIndex = 3;
                }
                bandedGridView = gridControl5.MainView as BandedGridView;
                if (IsOk && bandedGridView.RowCount > 0)
                {
                    if (this.textEdit82.Text == null || this.textEdit82.Text.Length == 0)
                    {
                        ActiveIndex = 4;
                        MessageBox.Show(string.Format("{0}表{1}:NameSpace不能为空！", "从", ActiveIndex)
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (IsOk && (this.textEdit93.Text == null || this.textEdit93.Text.Length == 0))
                    {
                        ActiveIndex = 4;
                        MessageBox.Show(string.Format("{0}表{1}:Dto类名不能为空！", "从", ActiveIndex)
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                    if (!IsOk)
                        ActiveIndex = 4;
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
                    if (this.radioGroup7.SelectedIndex == 0)
                    {
                        if (this.textEdit22.Text == null || this.textEdit22.Text.Length == 0)
                        {
                            MessageBox.Show(string.Format("{0}名不能为空！", "ResultMapId")
                                , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            IsOk = false;
                        }
                    }
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

            #region Page与ResultStructure
            if (IsOk)
            {
                if ((!this.checkEdit17.Checked) && this.checkEdit19.Checked)
                {
                    int index = this.comboBoxEdit1.SelectedIndex;
                    if (index == 1 || index == 2)
                    {
                        MessageBox.Show(string.Format("未分页时，不能选择{0}返回结构体！"
                             , this.comboBoxEdit1.SelectedText)
                            , "验证信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsOk = false;
                    }
                }
            }
            #endregion

            waitDialogForm.Close();
            return IsOk;
        }
        private void _SaveSelectToXml(bool IsDisplayLog)
        {
            int[] vs = new int[this.xtraTabControl5.TabPages.Count];
            vs[0] = this.gridControl1.MainView.RowCount;
            vs[1] = this.gridControl2.MainView.RowCount;
            vs[2] = this.gridControl3.MainView.RowCount;
            vs[3] = this.gridControl4.MainView.RowCount;
            vs[4] = this.gridControl5.MainView.RowCount;
            bool OkSign = CountOk(vs);
            if (!OkSign)
            {
                if (IsDisplayLog)
                    this.DisplayText("表是不连续的，保存失败。");
                //jhkljkl
                return;
            }

            int index = 0;
            if (this.listBoxControl1.SelectedIndex > -1)
                class_SelectAllModel.LastSelectTableName = this.listBoxControl1.SelectedValue.ToString();
            if (class_SelectAllModel.class_Create.MethodId == null)
            {
                class_SelectAllModel.class_Create.MethodId = Class_Tool.getKeyId("SE");
                this.Text = string.Format("SELECT：{0}", class_SelectAllModel.class_Create.MethodId);
                this.textEdit17.Text = class_SelectAllModel.class_Create.MethodId;

            }
            Class_WindowType class_WindowType = new Class_WindowType();
            class_WindowType.WindowType = "select";
            class_WindowType.XmlFileName = class_SelectAllModel.class_Create.MethodId;
            this.Tag = class_WindowType;
            class_SelectAllModel.class_Create.DateTime = System.DateTime.Now;
            if (this.gridControl1.MainView.RowCount > 0)
            {
                Class_Sub class_Sub = DataViewIntoClass((BandedGridView)this.gridControl1.MainView
                    , null, 0, -1, this.textEdit1.Text
                    , null, false, this.textEdit10.Text
                    , -1, this.checkEdit10.Checked);
                if (class_SelectAllModel.class_SubList.Count > index)
                    class_SelectAllModel.class_SubList[index] = class_Sub;
                else
                    class_SelectAllModel.class_SubList.Add(class_Sub);
                index++;
            }
            if (index > 0 && this.gridControl2.MainView.RowCount > 0)
            {
                Class_Sub class_Sub = DataViewIntoClass((BandedGridView)this.gridControl2.MainView
                    , buttonEdit2.Text, this.radioGroup1.SelectedIndex, -1, this.textEdit6.Text
                    , buttonEdit1.Text, false, this.textEdit11.Text
                    , 0, true);//??
                OkSign = true;
                if (class_SelectAllModel.class_SubList.Count > index)
                    class_SelectAllModel.class_SubList[index] = class_Sub;
                else
                    class_SelectAllModel.class_SubList.Add(class_Sub);
                index++;
            }

            if (index > 1 && this.gridControl3.MainView.RowCount > 0)
            {
                Class_Sub class_Sub = DataViewIntoClass(
                    (BandedGridView)this.gridControl3.MainView
                    , buttonEdit3.Text, this.radioGroup2.SelectedIndex
                    , -1, this.textEdit9.Text
                    , this.buttonEdit4.Text, false, this.textEdit12.Text
                    , this.radioGroup29.SelectedIndex
                    , this.checkEdit14.Checked);
                OkSign = true;
                if (class_SelectAllModel.class_SubList.Count > index)
                    class_SelectAllModel.class_SubList[index] = class_Sub;
                else
                    class_SelectAllModel.class_SubList.Add(class_Sub);
                index++;
            }
            if (index > 2 && this.gridControl4.MainView.RowCount > 0)
            {
                Class_Sub class_Sub = DataViewIntoClass(
                    (BandedGridView)this.gridControl4.MainView
                    , buttonEdit5.Text, this.radioGroup19.SelectedIndex
                    , -1, this.textEdit60.Text
                    , buttonEdit6.Text, false, this.textEdit57.Text
                    , this.radioGroup30.SelectedIndex
                    , this.checkEdit16.Checked);
                OkSign = true;
                if (class_SelectAllModel.class_SubList.Count > index)
                    class_SelectAllModel.class_SubList[index] = class_Sub;
                else
                    class_SelectAllModel.class_SubList.Add(class_Sub);
                index++;
            }
            if (index > 3 && this.gridControl5.MainView.RowCount > 0)
            {
                Class_Sub class_Sub = DataViewIntoClass(
                    (BandedGridView)this.gridControl5.MainView
                    , buttonEdit7.Text, this.radioGroup22.SelectedIndex
                    , -1, this.textEdit64.Text
                    , buttonEdit8.Text, false, this.textEdit61.Text
                    , this.radioGroup31.SelectedIndex
                    , this.checkEdit18.Checked);
                OkSign = true;
                if (class_SelectAllModel.class_SubList.Count > index)
                    class_SelectAllModel.class_SubList[index] = class_Sub;
                else
                    class_SelectAllModel.class_SubList.Add(class_Sub);
                index++;
            }
            if (index > 0)
            {
                class_SelectAllModel.AllPackerName = this.textEdit13.Text;
                class_SelectAllModel.IsAutoWard = this.checkEdit2.Checked;
                class_SelectAllModel.TestUnit = Class_Tool.EscapeCharacter(this.memoEdit12.Text);
                class_SelectAllModel.TestClassName = this.textEdit21.Text;
                class_SelectAllModel.PageSign = this.checkEdit17.Checked;
                class_SelectAllModel.ReturnStructure = this.checkEdit19.Checked;
                class_SelectAllModel.ReturnStructureType = this.comboBoxEdit1.SelectedIndex;
                class_SelectAllModel.ReadWriteSeparation = this.checkEdit20.Checked;

                #region 保存上次打开的Tab
                class_SelectAllModel.class_WindowLastState.xtraTabControl1 = this.xtraTabControl1.SelectedTabPageIndex;
                class_SelectAllModel.class_WindowLastState.xtraTabControl2 = this.xtraTabControl2.SelectedTabPageIndex;
                class_SelectAllModel.class_WindowLastState.xtraTabControl3 = this.xtraTabControl3.SelectedTabPageIndex;
                class_SelectAllModel.class_WindowLastState.xtraTabControl4 = this.xtraTabControl4.SelectedTabPageIndex;
                class_SelectAllModel.class_WindowLastState.xtraTabControl5 = this.xtraTabControl5.SelectedTabPageIndex;
                class_SelectAllModel.class_WindowLastState.xtraTabControl6 = this.xtraTabControl6.SelectedTabPageIndex;
                class_SelectAllModel.class_WindowLastState.xtraTabControl7 = this.xtraTabControl7.SelectedTabPageIndex;
                class_SelectAllModel.class_WindowLastState.xtraTabControl8 = this.xtraTabControl8.SelectedTabPageIndex;
                class_SelectAllModel.class_WindowLastState.xtraTabControl9 = this.xtraTabControl9.SelectedTabPageIndex;
                class_SelectAllModel.class_WindowLastState.xtraTabControl10 = this.xtraTabControl10.SelectedTabPageIndex;
                class_SelectAllModel.class_WindowLastState.xtraTabControl13 = this.xtraTabControl13.SelectedTabPageIndex;
                #endregion

                #region 主表
                index = 0;
                if (this.gridControl1.MainView.RowCount > 0)
                {
                    class_SelectAllModel.class_SubList[index].MethodId = this.textEdit14.Text;
                    class_SelectAllModel.class_SubList[index].MethodContent = this.textEdit15.Text;
                    class_SelectAllModel.class_SubList[index].ResultType = this.radioGroup7.SelectedIndex;
                    class_SelectAllModel.class_SubList[index].IsAddXmlHead = this.checkEdit1.Checked;
                    class_SelectAllModel.class_SubList[index].NameSpace = this.textEdit16.Text;
                    class_SelectAllModel.class_SubList[index].MapContent = Class_Tool.EscapeCharacter(this.memoEdit3.Text);
                    class_SelectAllModel.class_SubList[index].SelectContent = Class_Tool.EscapeCharacter(this.memoEdit4.Text);
                    class_SelectAllModel.class_SubList[index].ServiceInterFaceContent = Class_Tool.EscapeCharacter(this.memoEdit5.Text);
                    class_SelectAllModel.class_SubList[index].ServiceImplContent = Class_Tool.EscapeCharacter(this.memoEdit6.Text);
                    class_SelectAllModel.class_SubList[index].ModelContent = Class_Tool.EscapeCharacter(this.memoEdit8.Text);
                    class_SelectAllModel.class_SubList[index].DTOContent = Class_Tool.EscapeCharacter(this.memoEdit9.Text);
                    class_SelectAllModel.class_SubList[index].DAOContent = Class_Tool.EscapeCharacter(this.memoEdit10.Text);
                    class_SelectAllModel.class_SubList[index].ControlContent = Class_Tool.EscapeCharacter(this.memoEdit11.Text);
                    class_SelectAllModel.class_SubList[index].InPutParamContent = Class_Tool.EscapeCharacter(this.memoEdit31.Text);
                    class_SelectAllModel.class_SubList[index].ResultMapId = this.textEdit22.Text;
                    class_SelectAllModel.class_SubList[index].ModelClassName = this.textEdit24.Text;
                    class_SelectAllModel.class_SubList[index].ControlSwaggerValue = this.textEdit47.Text;
                    class_SelectAllModel.class_SubList[index].ControlSwaggerDescription = this.textEdit46.Text;
                    class_SelectAllModel.class_SubList[index].ServiceInterFaceReturnRemark = this.textEdit20.Text;
                    class_SelectAllModel.class_SubList[index].ServiceInterFaceReturnCount = this.radioGroup9.SelectedIndex;
                    class_SelectAllModel.class_SubList[index].PanelHeight = this.panelControl4.Height;
                    class_SelectAllModel.class_SubList[index].TestSql = Class_Tool.EscapeCharacter(this.memoEdit54.Text);
                    class_SelectAllModel.class_SubList[index].AliasName = this.textEdit10.Text;

                    class_SelectAllModel.class_SubList[index].ParamClassName = this.textEdit99.Text;
                    class_SelectAllModel.class_SubList[index].ModelClassName = this.textEdit100.Text;
                    class_SelectAllModel.class_SubList[index].DtoClassName = this.textEdit19.Text;
                    class_SelectAllModel.class_SubList[index].DaoClassName = this.textEdit101.Text;
                    class_SelectAllModel.class_SubList[index].XmlFileName = this.textEdit44.Text;
                    class_SelectAllModel.class_SubList[index].ControlRequestMapping = this.textEdit54.Text;
                    class_SelectAllModel.class_SubList[index].ServiceInterFaceName = this.textEdit102.Text;
                    class_SelectAllModel.class_SubList[index].ServiceClassName = this.textEdit103.Text;

                }
                #endregion

                #region 表一
                index++;
                if (this.gridControl2.MainView.RowCount > 0)
                {
                    class_SelectAllModel.class_SubList[index].NameSpace = this.textEdit31.Text;
                    class_SelectAllModel.class_SubList[index].DTOContent = Class_Tool.EscapeCharacter(this.memoEdit19.Text);
                    class_SelectAllModel.class_SubList[index].JoinType = this.radioGroup5.SelectedIndex;
                    class_SelectAllModel.class_SubList[index].InnerType = this.radioGroup1.SelectedIndex;
                    class_SelectAllModel.class_SubList[index].OutFieldName = this.buttonEdit1.Text;
                    class_SelectAllModel.class_SubList[index].MainTableFieldName = this.buttonEdit2.Text;
                    class_SelectAllModel.class_SubList[index].DtoClassName = this.textEdit97.Text;
                    class_SelectAllModel.class_SubList[index].PanelHeight = this.panelControl17.Height;
                    class_SelectAllModel.class_SubList[index].AliasName = this.textEdit11.Text;
                }
                #endregion

                #region 表二
                index++;
                if (this.gridControl3.MainView.RowCount > 0)
                {
                    class_SelectAllModel.class_SubList[index].NameSpace = this.textEdit41.Text;
                    class_SelectAllModel.class_SubList[index].JoinType = this.radioGroup6.SelectedIndex;
                    class_SelectAllModel.class_SubList[index].InnerType = this.radioGroup2.SelectedIndex;
                    class_SelectAllModel.class_SubList[index].OutFieldName = this.buttonEdit4.Text;
                    class_SelectAllModel.class_SubList[index].MainTableFieldName = this.buttonEdit3.Text;
                    class_SelectAllModel.class_SubList[index].DtoClassName = this.textEdit95.Text;
                    class_SelectAllModel.class_SubList[index].PanelHeight = this.panelControl28.Height;
                    class_SelectAllModel.class_SubList[index].AliasName = this.textEdit12.Text;
                }
                #endregion

                #region 表三
                index++;
                if (this.gridControl4.MainView.RowCount > 0)
                {
                    class_SelectAllModel.class_SubList[index].NameSpace = this.textEdit73.Text;
                    //单条、多条
                    //Join方式、association、collection
                    class_SelectAllModel.class_SubList[index].JoinType = this.radioGroup17.SelectedIndex;
                    //左链接、右链接
                    class_SelectAllModel.class_SubList[index].InnerType = this.radioGroup19.SelectedIndex;
                    //外键字段名称
                    class_SelectAllModel.class_SubList[index].OutFieldName = this.buttonEdit6.Text;
                    //关联字段名称
                    class_SelectAllModel.class_SubList[index].MainTableFieldName = this.buttonEdit5.Text;
                    class_SelectAllModel.class_SubList[index].DtoClassName = this.textEdit91.Text;
                    class_SelectAllModel.class_SubList[index].PanelHeight = this.panelControl44.Height;
                    class_SelectAllModel.class_SubList[index].AliasName = this.textEdit57.Text;
                }
                #endregion

                #region 表四
                index++;
                if (this.gridControl5.MainView.RowCount > 0)
                {
                    class_SelectAllModel.class_SubList[index].NameSpace = this.textEdit82.Text;
                    //Join方式、association、collection
                    class_SelectAllModel.class_SubList[index].JoinType = this.radioGroup20.SelectedIndex;
                    //左链接、右链接
                    class_SelectAllModel.class_SubList[index].InnerType = this.radioGroup22.SelectedIndex;
                    //外键字段名称
                    class_SelectAllModel.class_SubList[index].OutFieldName = this.buttonEdit8.Text;
                    //关联字段名称
                    class_SelectAllModel.class_SubList[index].MainTableFieldName = this.buttonEdit7.Text;
                    class_SelectAllModel.class_SubList[index].DtoClassName = this.textEdit93.Text;
                    class_SelectAllModel.class_SubList[index].PanelHeight = this.panelControl45.Height;
                    class_SelectAllModel.class_SubList[index].AliasName = this.textEdit61.Text;
                }
                #endregion

                #region 更新多表字段名称
                class_SelectAllModel.AddLinkFieldInfo();
                class_SelectAllModel.AddAllOutFieldName();
                class_SelectAllModel.UpdateIsMultTableSign();
                class_SelectAllModel.UpdateMultFieldName();
                #endregion

                if (class_PublicMethod.SelectToXml(class_SelectAllModel.class_Create.MethodId, class_SelectAllModel))
                {
                    if (IsDisplayLog)
                        this.DisplayText(string.Format("已将{0}方法【{1}】，保存到本地。", class_SelectAllModel.classType, class_SelectAllModel.class_Create.MethodId));
                }
            }
            else
            {
                if (IsDisplayLog)
                    this.DisplayText("失败，请完成主表设置。");
            }
        }
        private bool CountOk(int[] vs)
        {
            int index = 0;
            int ZoneSgin = -1;
            bool OkSign = true;
            if (vs[0] == 0)
                return false;
            while (index < vs.Length)
            {
                if (vs[index] == 0)
                {
                    ZoneSgin = index;
                    break;
                }
                index++;
            }
            if (index < vs.Length - 1)
            {
                if (ZoneSgin > -1)
                {
                    for (int j = ZoneSgin + 1; j < vs.Length; j++)
                    {
                        OkSign = OkSign && vs[j] == 0 ? true : false;
                    }
                }
            }
            return OkSign;
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
                this.listBoxControl3.Items.Add(class_SQLiteOperator.GetTableAlias(class_SelectAllModel.class_SelectDataBase.dataSourceUrl
                    , class_SelectAllModel.class_SelectDataBase.dataBaseName, item));
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
        private void bandedGridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged(sender, e, this.textEdit8, this.textEdit7);
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
            ToPage(class_SelectAllModel.class_SubList[PageIndex].TableName, PageIndex);
            if (PageIndex > 0)
            {
                DisplayText(String.Format("表{0}导入完成!", (PageIndex + 1).ToString()));
            }
            else
            {
                DisplayText("主表导入完成!");
            }
        }
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AgainFromXmlToGrid(this.xtraTabControl5.SelectedTabPageIndex);
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
            int PageMaxIndex = class_SelectAllModel.class_SubList.Count;
            switch (PageIndex)
            {
                case 0:
                    this.gridControl1.DataSource = null;
                    this.gridControl2.DataSource = null;
                    this.gridControl3.DataSource = null;
                    this.gridControl4.DataSource = null;
                    this.gridControl5.DataSource = null;
                    break;
                case 1:
                    this.gridControl2.DataSource = null;
                    this.gridControl3.DataSource = null;
                    this.gridControl4.DataSource = null;
                    this.gridControl5.DataSource = null;
                    break;
                case 2:
                    this.gridControl3.DataSource = null;
                    this.gridControl4.DataSource = null;
                    this.gridControl5.DataSource = null;
                    break;
                case 3:
                    this.gridControl4.DataSource = null;
                    this.gridControl5.DataSource = null;
                    break;
                case 4:
                    this.gridControl5.DataSource = null;
                    break;
                default:
                    break;
            }
            this.xtraTabControl5.TabPages[PageIndex].Text = "表";
            this.xtraTabControl8.TabPages[PageIndex].Text = "表";
            for (int i = PageMaxIndex - 1; i > PageIndex - 1; i--)
            {
                class_SelectAllModel.class_SubList.RemoveAt(i);
            }
            //if (class_SelectAllModel.class_SubList.Count > PageIndex)
            //    class_SelectAllModel.class_SubList[PageIndex] = new Class_SelectAllModel.Class_Sub();
        }
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.xtraTabControl5.SelectedTabPageIndex > -1)
                ClearDate(this.xtraTabControl5.SelectedTabPageIndex);
        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            class_SelectAllModel.class_SelectDataBase.GetDataBaseDefault();
            propertyGridControl3.Refresh();
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //存入默认值
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
            this.textEdit24.Text = (sender as TextEdit).Text;
            if (checkEdit8.Checked)
            {
                this.textEdit99.Text = Class_Tool.GetFirstCodeUpper(string.Format("{0}InPutParam", (sender as TextEdit).Text));
                this.textEdit100.Text = Class_Tool.GetFirstCodeUpper((sender as TextEdit).Text);
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
            string MethodId = class_SelectAllModel.class_Create.MethodId;
            //3：初始化生成类
            IClass_InterFaceCreateCode class_InterFaceCreateCode = new Class_CreateSelectCode(MethodId);
            //4：验证合法性
            List<string> outMessage = new List<string>();
            if (class_InterFaceCreateCode.IsCheckOk(ref outMessage))
            {
                ////加入所有外键信息
                //class_InterFaceCreateCode.AddAllOutFieldName();
                int PageIndex = 0;
                //5：生成代码
                #region 主表
                if (class_SelectAllModel.class_SubList.Count > PageIndex)
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
                    //DTO
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

                #region 表1
                PageIndex++;
                if (class_SelectAllModel.class_SubList.Count > PageIndex)
                {
                    ////MAP
                    //this.memoEdit13.Text = class_InterFaceCreateCode.GetMap(PageIndex);
                    //// Select标签
                    //this.memoEdit14.Text = class_InterFaceCreateCode.GetSql(PageIndex);
                    //// ServiceInterFace
                    //this.memoEdit15.Text = class_InterFaceCreateCode.GetServiceInterFace(PageIndex);
                    //// ServiceImpl
                    //this.memoEdit16.Text = class_InterFaceCreateCode.GetServiceImpl(PageIndex);
                    //// Model
                    //this.memoEdit17.Text = class_InterFaceCreateCode.GetModel(PageIndex);
                    //DTO
                    this.memoEdit19.Text = class_InterFaceCreateCode.GetDTO(PageIndex);
                    //// DAO
                    //this.memoEdit20.Text = class_InterFaceCreateCode.GetDAO(PageIndex);
                    // Control
                    //this.memoEdit21.Text = class_InterFaceCreateCode.GetControl(PageIndex);
                }
                #endregion

                #region 表2
                PageIndex++;
                if (class_SelectAllModel.class_SubList.Count > PageIndex)
                {
                    ////MAP
                    //this.memoEdit22.Text = class_InterFaceCreateCode.GetMap(PageIndex);
                    //// Select标签
                    //this.memoEdit23.Text = class_InterFaceCreateCode.GetSql(PageIndex);
                    //// ServiceInterFace
                    //this.memoEdit24.Text = class_InterFaceCreateCode.GetServiceInterFace(PageIndex);
                    //// ServiceImpl
                    //this.memoEdit25.Text = class_InterFaceCreateCode.GetServiceImpl(PageIndex);
                    //// Model
                    //this.memoEdit26.Text = class_InterFaceCreateCode.GetModel(PageIndex);
                    //DTO
                    this.memoEdit28.Text = class_InterFaceCreateCode.GetDTO(PageIndex);
                    //// DAO
                    //this.memoEdit29.Text = class_InterFaceCreateCode.GetDAO(PageIndex);
                    // Control
                    //this.memoEdit30.Text = class_InterFaceCreateCode.GetControl(PageIndex);
                }
                #endregion

                #region 表3
                PageIndex++;
                if (class_SelectAllModel.class_SubList.Count > PageIndex)
                {
                    ////MAP
                    //this.memoEdit34.Text = class_InterFaceCreateCode.GetMap(PageIndex);
                    //// Select标签
                    //this.memoEdit35.Text = class_InterFaceCreateCode.GetSql(PageIndex);
                    //// ServiceInterFace
                    //this.memoEdit39.Text = class_InterFaceCreateCode.GetServiceInterFace(PageIndex);
                    //// ServiceImpl
                    //this.memoEdit40.Text = class_InterFaceCreateCode.GetServiceImpl(PageIndex);
                    //// Model
                    //this.memoEdit36.Text = class_InterFaceCreateCode.GetModel(PageIndex);
                    //DTO
                    this.memoEdit42.Text = class_InterFaceCreateCode.GetDTO(PageIndex);
                    //// DAO
                    //this.memoEdit38.Text = class_InterFaceCreateCode.GetDAO(PageIndex);
                    // Control
                    //this.memoEdit41.Text = class_InterFaceCreateCode.GetControl(PageIndex);
                }
                #endregion

                #region 表4
                PageIndex++;
                if (class_SelectAllModel.class_SubList.Count > PageIndex)
                {
                    ////MAP
                    //this.memoEdit44.Text = class_InterFaceCreateCode.GetMap(PageIndex);
                    //// Select标签
                    //this.memoEdit45.Text = class_InterFaceCreateCode.GetSql(PageIndex);
                    //// ServiceInterFace
                    //this.memoEdit49.Text = class_InterFaceCreateCode.GetServiceInterFace(PageIndex);
                    //// ServiceImpl
                    //this.memoEdit50.Text = class_InterFaceCreateCode.GetServiceImpl(PageIndex);
                    //// Model
                    //this.memoEdit46.Text = class_InterFaceCreateCode.GetModel(PageIndex);
                    //DTO
                    this.memoEdit52.Text = class_InterFaceCreateCode.GetDTO(PageIndex);
                    //// DAO
                    //this.memoEdit48.Text = class_InterFaceCreateCode.GetDAO(PageIndex);
                    // Control
                    //this.memoEdit51.Text = class_InterFaceCreateCode.GetControl(PageIndex);
                }
                #endregion

                _SaveSelectToXml(false);

                this.DisplayText("代码已重新生成!");
                this.xtraTabControl3.SelectedTabPageIndex = 1;
                this.xtraTabControl8.SelectedTabPageIndex = 0;
                this.xtraTabControl6.SelectedTabPageIndex = 4;
                this.xtraTabControl7.SelectedTabPageIndex = 2;
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

        private void textEdit22_EditValueChanged(object sender, EventArgs e)
        {
            this.textEdit25.Text = string.Format("{0}Map", this.textEdit22.Text);
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

        private void buttonEdit3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            GetLinkField(this.bandedGridView3, sender);
        }

        private void buttonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            GetLinkField(this.bandedGridView2, sender);
        }

        private void bandedGridView4_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int Index = e.FocusedRowHandle;
            if (Index > -1)
            {
                DataRow row = (sender as BandedGridView).GetDataRow(Index);
                if (row != null)
                {
                    this.textEdit59.Text = row["FieldName"].ToString();
                    this.textEdit58.Text = row["FieldRemark"].ToString();
                }
            }
        }

        private void bandedGridView5_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int Index = e.FocusedRowHandle;
            if (Index > -1)
            {
                DataRow row = (sender as BandedGridView).GetDataRow(Index);
                if (row != null)
                {
                    this.textEdit63.Text = row["FieldName"].ToString();
                    this.textEdit62.Text = row["FieldRemark"].ToString();
                }
            }
        }

        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToPage(null, 3);
        }

        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ToPage(null, 4);
        }

        private void buttonEdit4_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.radioGroup29.SelectedIndex == 0)
                GetLinkField(this.bandedGridView1, sender);
            if (this.radioGroup29.SelectedIndex == 1)
                GetLinkField(this.bandedGridView2, sender);

        }

        private void buttonEdit6_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.radioGroup30.SelectedIndex == 0)
                GetLinkField(this.bandedGridView1, sender);
            if (this.radioGroup30.SelectedIndex == 1)
                GetLinkField(this.bandedGridView2, sender);
            if (this.radioGroup30.SelectedIndex == 2)
                GetLinkField(this.bandedGridView3, sender);
        }

        private void buttonEdit8_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.radioGroup31.SelectedIndex == 0)
                GetLinkField(this.bandedGridView1, sender);
            if (this.radioGroup31.SelectedIndex == 1)
                GetLinkField(this.bandedGridView2, sender);
            if (this.radioGroup31.SelectedIndex == 2)
                GetLinkField(this.bandedGridView3, sender);
            if (this.radioGroup31.SelectedIndex == 3)
                GetLinkField(this.bandedGridView4, sender);
        }

        private void buttonEdit5_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            GetLinkField(this.bandedGridView4, sender);
        }

        private void buttonEdit7_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            GetLinkField(this.bandedGridView5, sender);
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

        private void textEdit31_EditValueChanged(object sender, EventArgs e)
        {
            if (checkEdit3.Checked)
                this.textEdit97.Text = Class_Tool.GetFirstCodeUpper(string.Format("{0}Dto", (sender as TextEdit).Text));
        }

        private void textEdit41_EditValueChanged(object sender, EventArgs e)
        {
            if (checkEdit14.Checked)
                this.textEdit95.Text = Class_Tool.GetFirstCodeUpper(string.Format("{0}Dto", (sender as TextEdit).Text));
        }

        private void textEdit73_EditValueChanged(object sender, EventArgs e)
        {
            if (checkEdit16.Checked)
                this.textEdit91.Text = Class_Tool.GetFirstCodeUpper(string.Format("{0}Dto", (sender as TextEdit).Text));
        }

        private void textEdit82_EditValueChanged(object sender, EventArgs e)
        {
            if (checkEdit18.Checked)
                this.textEdit93.Text = Class_Tool.GetFirstCodeUpper(string.Format("{0}Dto", (sender as TextEdit).Text));
        }
    }
}
