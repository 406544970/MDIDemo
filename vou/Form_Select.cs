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
            this.xtraTabControl11.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl11;
            this.xtraTabControl12.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl12;
            this.xtraTabControl13.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl13;
            this.xtraTabControl14.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl14;
            this.xtraTabControl15.SelectedTabPageIndex = class_SelectAllModel.class_WindowLastState.xtraTabControl15;
            #endregion

            #region 主表
            if (class_SelectAllModel.class_SubList.Count > 0)
            {
                this.textEdit14.Text = class_SelectAllModel.class_SubList[0].MethodId;
                this.textEdit15.Text = class_SelectAllModel.class_SubList[0].MethodContent;
                this.radioGroup7.SelectedIndex = class_SelectAllModel.class_SubList[0].ResultType;
                this.radioGroup8.SelectedIndex = class_SelectAllModel.class_SubList[0].ParameterType;
                this.checkEdit1.Checked = class_SelectAllModel.class_SubList[0].IsAddXmlHead;
                this.textEdit16.Text = class_SelectAllModel.class_SubList[0].NameSpace;
                this.memoEdit3.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[0].MapContent);
                this.memoEdit4.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[0].SelectContent);
                this.memoEdit5.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[0].ServiceInterFaceContent);
                this.memoEdit6.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[0].ServiceImplContent);
                this.memoEdit8.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[0].ModelContent);
                this.memoEdit9.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[0].DTOContent);
                this.memoEdit10.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[0].DAOContent);
                this.memoEdit11.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[0].ControlContent);
                this.memoEdit31.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[0].PolyControlContent);
                this.textEdit22.Text = class_SelectAllModel.class_SubList[0].ResultMapId;
                this.textEdit24.Text = class_SelectAllModel.class_SubList[0].ResultMapType;
                this.textEdit17.Text = class_SelectAllModel.class_Create.MethodId;
                this.textEdit20.Text = class_SelectAllModel.class_SubList[0].ServiceInterFaceReturnRemark;
                this.textEdit47.Text = class_SelectAllModel.class_SubList[0].ControlSwaggerValue;
                this.textEdit46.Text = class_SelectAllModel.class_SubList[0].ControlSwaggerDescription;
                this.radioGroup9.SelectedIndex = class_SelectAllModel.class_SubList[0].ServiceInterFaceReturnCount;
                this.radioGroup16.SelectedIndex = class_SelectAllModel.class_SubList[0].DtoType;
                this.textEdit34.Text = class_SelectAllModel.class_SubList[0].DtoIniClassName;
                this.textEdit19.Text = class_SelectAllModel.class_SubList[0].DtoClassName;
                this.checkEdit5.Checked = class_SelectAllModel.class_SubList[0].ExtendsSign;
            }
            #endregion

            #region 表一
            if (class_SelectAllModel.class_SubList.Count > 1)
            {
                this.textEdit33.Text = class_SelectAllModel.class_SubList[1].MethodId;
                this.textEdit32.Text = class_SelectAllModel.class_SubList[1].MethodContent;
                this.radioGroup11.SelectedIndex = class_SelectAllModel.class_SubList[1].ResultType;
                this.radioGroup10.SelectedIndex = class_SelectAllModel.class_SubList[1].ParameterType;
                this.checkEdit3.Checked = class_SelectAllModel.class_SubList[1].IsAddXmlHead;
                this.textEdit31.Text = class_SelectAllModel.class_SubList[1].NameSpace;
                this.memoEdit13.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[1].MapContent);
                this.memoEdit14.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[1].SelectContent);
                this.memoEdit15.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[1].ServiceInterFaceContent);
                this.memoEdit16.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[1].ServiceImplContent);
                this.memoEdit17.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[1].ModelContent);
                this.memoEdit19.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[1].DTOContent);
                this.memoEdit20.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[1].DAOContent);
                this.memoEdit21.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[1].ControlContent);
                this.textEdit29.Text = class_SelectAllModel.class_SubList[1].ResultMapId;
                this.textEdit28.Text = class_SelectAllModel.class_SubList[1].ResultMapType;
                this.textEdit49.Text = class_SelectAllModel.class_SubList[1].ControlSwaggerValue;
                this.textEdit48.Text = class_SelectAllModel.class_SubList[1].ControlSwaggerDescription;
                this.radioGroup5.SelectedIndex = class_SelectAllModel.class_SubList[1].JoinType;
                this.radioGroup1.SelectedIndex = class_SelectAllModel.class_SubList[1].InnerType;
                this.radioGroup3.SelectedIndex = class_SelectAllModel.class_SubList[1].OneToMult;
                //this.textEdit17.Text = class_SelectAllModel.class_Create.MethodId;
                this.textEdit35.Text = class_SelectAllModel.class_SubList[1].ServiceInterFaceReturnRemark;
                this.radioGroup12.SelectedIndex = class_SelectAllModel.class_SubList[1].ServiceInterFaceReturnCount;
                this.buttonEdit1.Text = class_SelectAllModel.class_SubList[1].OutFieldName;
                this.buttonEdit2.Text = class_SelectAllModel.class_SubList[1].MainTableFieldName;
            }
            #endregion

            #region 表二
            if (class_SelectAllModel.class_SubList.Count > 2)
            {
                this.textEdit43.Text = class_SelectAllModel.class_SubList[2].MethodId;
                this.textEdit42.Text = class_SelectAllModel.class_SubList[2].MethodContent;
                this.radioGroup14.SelectedIndex = class_SelectAllModel.class_SubList[2].ResultType;
                this.radioGroup13.SelectedIndex = class_SelectAllModel.class_SubList[2].ParameterType;
                this.checkEdit4.Checked = class_SelectAllModel.class_SubList[2].IsAddXmlHead;
                this.textEdit41.Text = class_SelectAllModel.class_SubList[2].NameSpace;
                this.memoEdit22.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[2].MapContent);
                this.memoEdit23.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[2].SelectContent);
                this.memoEdit24.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[2].ServiceInterFaceContent);
                this.memoEdit25.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[2].ServiceImplContent);
                this.memoEdit26.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[2].ModelContent);
                this.memoEdit28.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[2].DTOContent);
                this.memoEdit29.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[2].DAOContent);
                this.memoEdit30.Text = Class_Tool.UnEscapeCharacter(class_SelectAllModel.class_SubList[2].ControlContent);
                this.textEdit39.Text = class_SelectAllModel.class_SubList[2].ResultMapId;
                this.textEdit38.Text = class_SelectAllModel.class_SubList[2].ResultMapType;
                this.textEdit45.Text = class_SelectAllModel.class_SubList[2].ServiceInterFaceReturnRemark;
                this.textEdit51.Text = class_SelectAllModel.class_SubList[2].ControlSwaggerValue;
                this.textEdit50.Text = class_SelectAllModel.class_SubList[2].ControlSwaggerDescription;
                this.radioGroup15.SelectedIndex = class_SelectAllModel.class_SubList[2].ServiceInterFaceReturnCount;
                this.radioGroup6.SelectedIndex = class_SelectAllModel.class_SubList[2].JoinType;
                this.radioGroup2.SelectedIndex = class_SelectAllModel.class_SubList[2].InnerType;
                this.radioGroup4.SelectedIndex = class_SelectAllModel.class_SubList[2].OneToMult;
                this.buttonEdit4.Text = class_SelectAllModel.class_SubList[2].OutFieldName;
                this.buttonEdit3.Text = class_SelectAllModel.class_SubList[2].MainTableFieldName;
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
                            this.AddUseTableData(class_SelectAllModel.class_SubList[i].TableName, i);
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

            radioGroup5.SelectedIndex = 0;
            radioGroup6.SelectedIndex = 0;

            setIniSkin(publicSkinName);
            xtraTabControl3.SelectedTabPageIndex = 0;
            xtraTabControl4.SelectedTabPageIndex = 0;
            xtraTabControl5.SelectedTabPageIndex = 0;

            this.bandedGridColumn1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            myTableNameList = new List<string>();
            myTableContentList = new List<string>();

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
            radioGroup1.SelectedIndex = 0;
            radioGroup2.SelectedIndex = 0;
            radioGroup3.SelectedIndex = 0;
            radioGroup4.SelectedIndex = 0;

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
            class_SetMemoEdit.SetMemoEdit(this.memoEdit13);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit14);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit15);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit16);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit17);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit19);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit20);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit21);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit22);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit23);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit24);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit25);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit26);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit28);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit29);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit30);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit31);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit32);
            class_SetMemoEdit.SetMemoEdit(this.memoEdit33);
            #endregion

            #region TextEdit
            Class_SetTextEdit class_SetTextEdit = new Class_SetTextEdit();
            class_SetTextEdit.SetTextEdit(this.textEdit18);
            class_SetTextEdit.SetTextEdit(this.textEdit19);
            class_SetTextEdit.SetTextEdit(this.textEdit25);
            class_SetTextEdit.SetTextEdit(this.textEdit23);
            class_SetTextEdit.SetTextEdit(this.textEdit26);
            class_SetTextEdit.SetTextEdit(this.textEdit27);
            class_SetTextEdit.SetTextEdit(this.textEdit30);
            class_SetTextEdit.SetTextEdit(this.textEdit36);
            class_SetTextEdit.SetTextEdit(this.textEdit37);
            class_SetTextEdit.SetTextEdit(this.textEdit40);
            class_SetTextEdit.SetTextEdit(this.textEdit13, Color.Yellow);
            class_SetTextEdit.SetTextEdit(this.textEdit10, Color.Yellow);
            class_SetTextEdit.SetTextEdit(this.textEdit11, Color.Yellow);
            class_SetTextEdit.SetTextEdit(this.textEdit12, Color.Yellow);
            class_SetTextEdit.SetTextEdit(this.textEdit14, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit15, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit16, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit20, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit22, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit24, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit28, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit29, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit31, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit32, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit33, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit34, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit35, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit38, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit39, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit41, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit42, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit43, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit45, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit46, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit47, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit48, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit49, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit50, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit51, Color.SkyBlue);
            class_SetTextEdit.SetTextEdit(this.textEdit17, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit44, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit52, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit53, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit54, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit55, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit56, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit1, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit2, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit3, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit4, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit5, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit6, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit7, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit8, true, Color.GreenYellow);
            class_SetTextEdit.SetTextEdit(this.textEdit9, true, Color.GreenYellow);
            #endregion

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
        private void AddUseTableData(string TableName, int PageSelectIndex)
        {
            switch (PageSelectIndex)
            {
                case 0:
                    {
                        this.gridControl1.DataSource = class_InterFaceDataBase.GetMainTableStruct(TableName, PageSelectIndex);
                        textEdit1.Text = TableName;
                        if (class_SelectAllModel.class_SubList.Count > PageSelectIndex && class_SelectAllModel.class_SubList[PageSelectIndex].AliasName != null)
                            textEdit10.Text = class_SelectAllModel.class_SubList[PageSelectIndex].AliasName;
                        else
                            textEdit10.Text = "";
                        AddColumnRepositoryCombox(this.repositoryItemComboBox2);
                        AddColumnComboxFunctionByDataType(this.repositoryItemComboBox1, "");
                        AddColumnComboxHavingFunctionByDataType(this.repositoryItemComboBox7, "");
                    }
                    break;
                case 1:
                    {
                        this.gridControl2.DataSource = class_InterFaceDataBase.GetMainTableStruct(TableName, PageSelectIndex);
                        textEdit6.Text = TableName;
                        if (class_SelectAllModel.class_SubList.Count > PageSelectIndex && class_SelectAllModel.class_SubList[PageSelectIndex].AliasName != null)
                        {
                            textEdit11.Text = class_SelectAllModel.class_SubList[PageSelectIndex].AliasName;
                            this.radioGroup1.SelectedIndex = class_SelectAllModel.class_SubList[PageSelectIndex].LinkType;
                            this.radioGroup3.SelectedIndex = class_SelectAllModel.class_SubList[PageSelectIndex].CountToCount;
                        }
                        else
                            textEdit11.Text = "sub";
                        //this.buttonEdit2.Text = class_SelectAllModel.class_Subs.OutFieldName;
                        AddColumnRepositoryCombox(this.repositoryItemComboBox10);
                        AddColumnComboxFunctionByDataType(this.repositoryItemComboBox15, "");
                        AddColumnComboxHavingFunctionByDataType(this.repositoryItemComboBox15, "");
                    }
                    break;
                case 2:
                    {
                        this.gridControl3.DataSource = class_InterFaceDataBase.GetMainTableStruct(TableName, PageSelectIndex);
                        textEdit9.Text = TableName;
                        if (class_SelectAllModel.class_SubList.Count > PageSelectIndex && class_SelectAllModel.class_SubList[PageSelectIndex].AliasName != null)
                        {
                            textEdit12.Text = class_SelectAllModel.class_SubList[PageSelectIndex].AliasName;
                            this.radioGroup2.SelectedIndex = class_SelectAllModel.class_SubList[PageSelectIndex].LinkType;
                            this.radioGroup4.SelectedIndex = class_SelectAllModel.class_SubList[PageSelectIndex].CountToCount;
                        }
                        else
                            textEdit12.Text = "subsub";

                        //this.buttonEdit3.Text = class_SelectAllModel.class_SubSubs.OutFieldName;
                        AddColumnComboxFunctionByDataType(this.repositoryItemComboBox1, "");
                        AddColumnComboxHavingFunctionByDataType(this.repositoryItemComboBox7, "");
                    }
                    break;
                case 3:
                    {
                        this.gridControl4.DataSource = class_InterFaceDataBase.GetMainTableStruct(TableName, PageSelectIndex);
                        textEdit60.Text = TableName;
                        if (class_SelectAllModel.class_SubList.Count > PageSelectIndex && class_SelectAllModel.class_SubList[PageSelectIndex].AliasName != null)
                        {
                            textEdit57.Text = class_SelectAllModel.class_SubList[PageSelectIndex].AliasName;
                            this.radioGroup19.SelectedIndex = class_SelectAllModel.class_SubList[PageSelectIndex].LinkType;
                            this.radioGroup18.SelectedIndex = class_SelectAllModel.class_SubList[PageSelectIndex].CountToCount;
                        }
                        else
                            textEdit57.Text = "subsub";

                        //this.buttonEdit3.Text = class_SelectAllModel.class_SubSubs.OutFieldName;
                        AddColumnComboxFunctionByDataType(this.repositoryItemComboBox1, "");
                        AddColumnComboxHavingFunctionByDataType(this.repositoryItemComboBox7, "");
                    }
                    break;
                case 4:
                    {
                        this.gridControl5.DataSource = class_InterFaceDataBase.GetMainTableStruct(TableName, PageSelectIndex);
                        textEdit64.Text = TableName;
                        if (class_SelectAllModel.class_SubList.Count > PageSelectIndex && class_SelectAllModel.class_SubList[PageSelectIndex].AliasName != null)
                        {
                            textEdit61.Text = class_SelectAllModel.class_SubList[PageSelectIndex].AliasName;
                            this.radioGroup22.SelectedIndex = class_SelectAllModel.class_SubList[PageSelectIndex].LinkType;
                            this.radioGroup21.SelectedIndex = class_SelectAllModel.class_SubList[PageSelectIndex].CountToCount;
                        }
                        else
                            textEdit61.Text = "subsub";

                        //this.buttonEdit3.Text = class_SelectAllModel.class_SubSubs.OutFieldName;
                        AddColumnComboxFunctionByDataType(this.repositoryItemComboBox1, "");
                        AddColumnComboxHavingFunctionByDataType(this.repositoryItemComboBox7, "");
                    }
                    break;
                default:
                    break;
            }
            this.xtraTabControl5.TabPages[PageSelectIndex].Text = string.Format("表：{0}", TableName);
            this.xtraTabControl8.TabPages[PageSelectIndex].Text = string.Format("表：{0}", TableName);
            //if (PageSelectIndex > -1)
            //    this.xtraTabControl5.SelectedTabPageIndex = PageSelectIndex;
        }
        //private void ToMain()
        //{
        //    ToMain(null);
        //}
        //private void ToSub(string TableName)
        //{
        //    try
        //    {
        //        this.xtraTabControl5.SelectedTabPageIndex = 1;
        //        //labelControl1.Visible = false;
        //        if (TableName == null)
        //        {
        //            int Index = this.listBoxControl1.SelectedIndex;
        //            if (Index > -1)
        //            {
        //                AddUseTableData(this.listBoxControl1.Text, 1);
        //            }
        //        }
        //        else
        //        {
        //            AddUseTableData(TableName, 1);
        //        }
        //    }
        //    catch (Exception error)
        //    {
        //        MessageBox.Show(error.Message);
        //    }
        //}
        private void ToPage(string TableName, int PageIndex)
        {
            try
            {
                this.xtraTabControl5.SelectedTabPageIndex = PageIndex;
                //labelControl1.Visible = false;
                if (TableName == null)
                {
                    int Index = this.listBoxControl1.SelectedIndex;
                    if (Index > -1)
                    {
                        AddUseTableData(this.listBoxControl1.Text, PageIndex);
                    }
                }
                else
                {
                    AddUseTableData(TableName, PageIndex);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        //private void ToMain(string TableName)
        //{
        //    try
        //    {
        //        if (TableName == null)
        //        {
        //            int Index = this.listBoxControl1.SelectedIndex;
        //            if (Index > -1)
        //            {
        //                AddUseTableData(this.listBoxControl1.Text, 0);
        //            }
        //        }
        //        else
        //        {
        //            AddUseTableData(TableName, 0);
        //        }
        //    }
        //    catch (Exception error)
        //    {
        //        MessageBox.Show(error.Message);
        //    }
        //}
        //private void ToSub()
        //{
        //    ToSub(null);
        //}
        //private void ToSubSub()
        //{
        //    ToSubSub(null);
        //}
        //private void ToSubSub(string TableName)
        //{
        //    try
        //    {
        //        this.xtraTabControl5.SelectedTabPageIndex = 2;
        //        //labelControl1.Visible = false;
        //        if (TableName == null)
        //        {
        //            int Index = this.listBoxControl1.SelectedIndex;
        //            if (Index > -1)
        //            {
        //                AddUseTableData(this.listBoxControl1.Text, 2);
        //            }
        //        }
        //        else
        //        {
        //            AddUseTableData(TableName, 1);
        //        }
        //    }
        //    catch (Exception error)
        //    {
        //        MessageBox.Show(error.Message);
        //    }
        //}
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
            myTableNameList.Clear();
            myTableContentList.Clear();
            class_TableInfos = class_InterFaceDataBase.GetUseTableList(null);
            myTableNameList = class_TableInfos.Select(a => a.TableName).ToList();
            myTableContentList = class_TableInfos.Select(a => a.TableComment).ToList();
            foreach (string row in myTableNameList)
            {
                this.listBoxControl1.Items.Add(row);
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
                return;
            }

            int index = 0;
            if (this.listBoxControl1.SelectedIndex > -1)
                class_SelectAllModel.LastSelectTableName = this.listBoxControl1.SelectedValue.ToString();
            if (class_SelectAllModel.class_Create.MethodId == null)
            {
                class_SelectAllModel.class_Create.MethodId = Class_Tool.getKeyId("SE");
                this.Text = string.Format("SELECT：{0}", class_SelectAllModel.class_Create.MethodId);
                this.Tag = class_SelectAllModel.class_Create.MethodId;
            }
            class_SelectAllModel.class_Create.DateTime = System.DateTime.Now;
            if (this.gridControl1.MainView.RowCount > 0)
            {
                Class_Sub class_Sub = DataViewIntoClass((BandedGridView)this.gridControl1.MainView
                    , null, 0, -1, this.textEdit1.Text
                    , this.MainKeyFieldName, false, this.textEdit10.Text);
                if (class_SelectAllModel.class_SubList.Count > index)
                    class_SelectAllModel.class_SubList[index] = class_Sub;
                else
                    class_SelectAllModel.class_SubList.Add(class_Sub);
                index++;
            }
            if (this.gridControl2.MainView.RowCount > 0)
            {
                Class_Sub class_Sub = DataViewIntoClass((BandedGridView)this.gridControl2.MainView
                    , buttonEdit2.Text, this.radioGroup1.SelectedIndex, this.radioGroup3.SelectedIndex, this.textEdit6.Text
                    , null, false, this.textEdit11.Text);
                OkSign = true;
                if (class_SelectAllModel.class_SubList.Count > index)
                    class_SelectAllModel.class_SubList[index] = class_Sub;
                else
                    class_SelectAllModel.class_SubList.Add(class_Sub);
                index++;
            }

            if (this.gridControl3.MainView.RowCount > 0)
            {
                Class_Sub class_Sub = DataViewIntoClass((BandedGridView)this.gridControl3.MainView
                    , buttonEdit3.Text, this.radioGroup2.SelectedIndex, this.radioGroup4.SelectedIndex, this.textEdit9.Text
                    , null, false, this.textEdit12.Text);
                OkSign = true;
                if (class_SelectAllModel.class_SubList.Count > index)
                    class_SelectAllModel.class_SubList[index] = class_Sub;
                else
                    class_SelectAllModel.class_SubList.Add(class_Sub);
                index++;
            }
            class_SelectAllModel.AllPackerName = this.textEdit13.Text;
            class_SelectAllModel.IsAutoWard = this.checkEdit2.Checked;
            class_SelectAllModel.TestUnit = Class_Tool.EscapeCharacter(this.memoEdit12.Text);
            class_SelectAllModel.TestClassName = this.textEdit21.Text;

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
            class_SelectAllModel.class_WindowLastState.xtraTabControl11 = this.xtraTabControl11.SelectedTabPageIndex;
            class_SelectAllModel.class_WindowLastState.xtraTabControl12 = this.xtraTabControl12.SelectedTabPageIndex;
            class_SelectAllModel.class_WindowLastState.xtraTabControl13 = this.xtraTabControl13.SelectedTabPageIndex;
            class_SelectAllModel.class_WindowLastState.xtraTabControl14 = this.xtraTabControl14.SelectedTabPageIndex;
            class_SelectAllModel.class_WindowLastState.xtraTabControl15 = this.xtraTabControl15.SelectedTabPageIndex;
            #endregion

            #region 主表
            if (this.gridControl1.MainView.RowCount > 0)
            {
                class_SelectAllModel.class_SubList[0].MethodId = this.textEdit14.Text;
                class_SelectAllModel.class_SubList[0].MethodContent = this.textEdit15.Text;
                class_SelectAllModel.class_SubList[0].ResultType = this.radioGroup7.SelectedIndex;
                class_SelectAllModel.class_SubList[0].ParameterType = this.radioGroup8.SelectedIndex;
                class_SelectAllModel.class_SubList[0].IsAddXmlHead = this.checkEdit1.Checked;
                class_SelectAllModel.class_SubList[0].NameSpace = this.textEdit16.Text;
                class_SelectAllModel.class_SubList[0].MapContent = Class_Tool.EscapeCharacter(this.memoEdit3.Text);
                class_SelectAllModel.class_SubList[0].SelectContent = Class_Tool.EscapeCharacter(this.memoEdit4.Text);
                class_SelectAllModel.class_SubList[0].ServiceInterFaceContent = Class_Tool.EscapeCharacter(this.memoEdit5.Text);
                class_SelectAllModel.class_SubList[0].ServiceImplContent = Class_Tool.EscapeCharacter(this.memoEdit6.Text);
                class_SelectAllModel.class_SubList[0].ModelContent = Class_Tool.EscapeCharacter(this.memoEdit8.Text);
                class_SelectAllModel.class_SubList[0].DTOContent = Class_Tool.EscapeCharacter(this.memoEdit9.Text);
                class_SelectAllModel.class_SubList[0].DAOContent = Class_Tool.EscapeCharacter(this.memoEdit10.Text);
                class_SelectAllModel.class_SubList[0].ControlContent = Class_Tool.EscapeCharacter(this.memoEdit11.Text);
                class_SelectAllModel.class_SubList[0].PolyControlContent = Class_Tool.EscapeCharacter(this.memoEdit31.Text);
                class_SelectAllModel.class_SubList[0].ResultMapId = this.textEdit22.Text;
                class_SelectAllModel.class_SubList[0].ResultMapType = this.textEdit24.Text;
                class_SelectAllModel.class_SubList[0].ControlSwaggerValue = this.textEdit47.Text;
                class_SelectAllModel.class_SubList[0].ControlSwaggerDescription = this.textEdit46.Text;
                class_SelectAllModel.class_SubList[0].ServiceInterFaceReturnRemark = this.textEdit20.Text;
                class_SelectAllModel.class_SubList[0].ServiceInterFaceReturnCount = this.radioGroup9.SelectedIndex;
                class_SelectAllModel.class_SubList[0].DtoType = this.radioGroup16.SelectedIndex;
                class_SelectAllModel.class_SubList[0].DtoIniClassName = this.textEdit34.Text;
                class_SelectAllModel.class_SubList[0].DtoClassName = this.textEdit19.Text;
                class_SelectAllModel.class_SubList[0].ExtendsSign = this.checkEdit5.Checked;
            }
            #endregion

            #region 表一
            if (this.gridControl2.MainView.RowCount > 0)
            {
                class_SelectAllModel.class_SubList[1].MethodId = this.textEdit33.Text;
                class_SelectAllModel.class_SubList[1].MethodContent = this.textEdit32.Text;
                class_SelectAllModel.class_SubList[1].ResultType = this.radioGroup11.SelectedIndex;
                class_SelectAllModel.class_SubList[1].ParameterType = this.radioGroup10.SelectedIndex;
                class_SelectAllModel.class_SubList[1].IsAddXmlHead = this.checkEdit3.Checked;
                class_SelectAllModel.class_SubList[1].NameSpace = this.textEdit31.Text;
                class_SelectAllModel.class_SubList[1].MapContent = Class_Tool.EscapeCharacter(this.memoEdit13.Text);
                class_SelectAllModel.class_SubList[1].SelectContent = Class_Tool.EscapeCharacter(this.memoEdit14.Text);
                class_SelectAllModel.class_SubList[1].ServiceInterFaceContent = Class_Tool.EscapeCharacter(this.memoEdit15.Text);
                class_SelectAllModel.class_SubList[1].ServiceImplContent = Class_Tool.EscapeCharacter(this.memoEdit16.Text);
                class_SelectAllModel.class_SubList[1].ModelContent = Class_Tool.EscapeCharacter(this.memoEdit17.Text);
                class_SelectAllModel.class_SubList[1].DTOContent = Class_Tool.EscapeCharacter(this.memoEdit19.Text);
                class_SelectAllModel.class_SubList[1].DAOContent = Class_Tool.EscapeCharacter(this.memoEdit20.Text);
                class_SelectAllModel.class_SubList[1].ControlContent = Class_Tool.EscapeCharacter(this.memoEdit21.Text);
                class_SelectAllModel.class_SubList[1].ResultMapId = this.textEdit29.Text;
                class_SelectAllModel.class_SubList[1].ResultMapType = this.textEdit28.Text;
                class_SelectAllModel.class_SubList[1].ControlSwaggerValue = this.textEdit49.Text;
                class_SelectAllModel.class_SubList[1].ControlSwaggerDescription = this.textEdit48.Text;
                class_SelectAllModel.class_SubList[1].ServiceInterFaceReturnRemark = this.textEdit35.Text;
                class_SelectAllModel.class_SubList[1].ServiceInterFaceReturnCount = this.radioGroup12.SelectedIndex;
                class_SelectAllModel.class_SubList[1].JoinType = this.radioGroup5.SelectedIndex;
                class_SelectAllModel.class_SubList[1].InnerType = this.radioGroup1.SelectedIndex;
                class_SelectAllModel.class_SubList[1].OneToMult = this.radioGroup3.SelectedIndex;
                class_SelectAllModel.class_SubList[1].OutFieldName = this.buttonEdit1.Text;
                class_SelectAllModel.class_SubList[1].MainTableFieldName = this.buttonEdit2.Text;
            }
            #endregion

            #region 表二
            if (this.gridControl3.MainView.RowCount > 0)
            {
                class_SelectAllModel.class_SubList[2].MethodId = this.textEdit43.Text;
                class_SelectAllModel.class_SubList[2].MethodContent = this.textEdit42.Text;
                class_SelectAllModel.class_SubList[2].ResultType = this.radioGroup14.SelectedIndex;
                class_SelectAllModel.class_SubList[2].ParameterType = this.radioGroup13.SelectedIndex;
                class_SelectAllModel.class_SubList[2].IsAddXmlHead = this.checkEdit4.Checked;
                class_SelectAllModel.class_SubList[2].NameSpace = this.textEdit41.Text;
                class_SelectAllModel.class_SubList[2].MapContent = Class_Tool.EscapeCharacter(this.memoEdit22.Text);
                class_SelectAllModel.class_SubList[2].SelectContent = Class_Tool.EscapeCharacter(this.memoEdit23.Text);
                class_SelectAllModel.class_SubList[2].ServiceInterFaceContent = Class_Tool.EscapeCharacter(this.memoEdit24.Text);
                class_SelectAllModel.class_SubList[2].ServiceImplContent = Class_Tool.EscapeCharacter(this.memoEdit25.Text);
                class_SelectAllModel.class_SubList[2].ModelContent = Class_Tool.EscapeCharacter(this.memoEdit26.Text);
                class_SelectAllModel.class_SubList[2].DTOContent = Class_Tool.EscapeCharacter(this.memoEdit28.Text);
                class_SelectAllModel.class_SubList[2].DAOContent = Class_Tool.EscapeCharacter(this.memoEdit29.Text);
                class_SelectAllModel.class_SubList[2].ControlContent = Class_Tool.EscapeCharacter(this.memoEdit30.Text);
                class_SelectAllModel.class_SubList[2].ResultMapId = this.textEdit39.Text;
                class_SelectAllModel.class_SubList[2].ResultMapType = this.textEdit38.Text;
                class_SelectAllModel.class_SubList[2].ControlSwaggerValue = this.textEdit51.Text;
                class_SelectAllModel.class_SubList[2].ControlSwaggerDescription = this.textEdit50.Text;
                class_SelectAllModel.class_SubList[2].ServiceInterFaceReturnRemark = this.textEdit45.Text;
                class_SelectAllModel.class_SubList[2].ServiceInterFaceReturnCount = this.radioGroup15.SelectedIndex;
                class_SelectAllModel.class_SubList[2].JoinType = this.radioGroup6.SelectedIndex;
                class_SelectAllModel.class_SubList[2].InnerType = this.radioGroup2.SelectedIndex;
                class_SelectAllModel.class_SubList[2].OneToMult = this.radioGroup4.SelectedIndex;
                class_SelectAllModel.class_SubList[2].OutFieldName = this.buttonEdit4.Text;
                class_SelectAllModel.class_SubList[2].MainTableFieldName = this.buttonEdit3.Text;
            }
            #endregion

            if (class_PublicMethod.SelectToXml(class_SelectAllModel.class_Create.MethodId, class_SelectAllModel))
            {
                if (IsDisplayLog)
                    this.DisplayText(string.Format("已将{0}方法【{1}】，保存到本地。", class_SelectAllModel.classType, class_SelectAllModel.class_Create.MethodId));
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
        private List<string> GetAnyTableContent(List<string> TableNameList)
        {
            List<string> vs = new List<string>();
            return vs;
        }
        private void filterList()
        {
            this.listBoxControl1.Items.Clear();
            this.listBoxControl2.Items.Clear();
            List<string> TableNameList = new List<string>();
            foreach (string item in filterUseTable(this.searchControl1.Text.Length > 0 ? this.searchControl1.Text : null))
            {
                this.listBoxControl1.Items.Add(item);
                TableNameList.Add(item);
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
            //ToMain();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //ToSub();
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //ToSubSub();
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
            this.textEdit34.Text = (sender as TextEdit).Text;
            this.textEdit44.Text = string.Format("{0}Mapper.xml", (sender as TextEdit).Text);
            this.textEdit54.Text = string.Format("{0}Controller", (sender as TextEdit).Text);
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
            IClass_InterFaceCreateCode class_InterFaceCreateCode = new Class_CreateSelectCode(MethodId);
            //4：验证合法性
            if (class_InterFaceCreateCode.IsCheckOk())
            {
                //5：生成代码
                #region 主表
                if (class_SelectAllModel.class_SubList.Count > 0)
                {
                    // MAP
                    this.memoEdit3.Text = class_InterFaceCreateCode.GetMainMap();
                    // Select标签
                    this.memoEdit4.Text = class_InterFaceCreateCode.GetMainMapLable();
                    // ServiceInterFace
                    this.memoEdit5.Text = class_InterFaceCreateCode.GetMainServiceInterFace();
                    // ServiceImpl
                    this.memoEdit6.Text = class_InterFaceCreateCode.GetMainServiceImpl();
                    // Model
                    this.memoEdit8.Text = class_InterFaceCreateCode.GetMainModel();
                    //DTO
                    this.memoEdit9.Text = class_InterFaceCreateCode.GetMainDTO();
                    // DAO
                    this.memoEdit10.Text = class_InterFaceCreateCode.GetMainDAO();
                    // Control
                    this.memoEdit11.Text = class_InterFaceCreateCode.GetMainControl();
                    // FeignControl
                    this.memoEdit31.Text = class_InterFaceCreateCode.GetMainFeignControl();
                }
                #endregion

                #region 表一
                if (class_SelectAllModel.class_SubList.Count > 1)
                {
                    //MAP
                    this.memoEdit13.Text = class_InterFaceCreateCode.GetSubOneMap();
                    // Select标签
                    this.memoEdit14.Text = class_InterFaceCreateCode.GetSubOneMapLable();
                    // ServiceInterFace
                    this.memoEdit15.Text = class_InterFaceCreateCode.GetSubOneServiceInterFace();
                    // ServiceImpl
                    this.memoEdit16.Text = class_InterFaceCreateCode.GetSubOneServiceImpl();
                    // Model
                    this.memoEdit17.Text = class_InterFaceCreateCode.GetSubOneModel();
                    //DTO
                    //this.memoEdit19.Text = class_InterFaceCreateCode.GetMainDTO();
                    // DAO
                    this.memoEdit20.Text = class_InterFaceCreateCode.GetSubOneDAO();
                    // Control
                    this.memoEdit21.Text = class_InterFaceCreateCode.GetSubOneControl();
                }
                #endregion

                #region 表二
                if (class_SelectAllModel.class_SubList.Count > 2)
                {
                    //MAP
                    this.memoEdit22.Text = class_InterFaceCreateCode.GetSubTwoMap();
                    // Select标签
                    this.memoEdit23.Text = class_InterFaceCreateCode.GetSubTwoMapLable();
                    // ServiceInterFace
                    this.memoEdit24.Text = class_InterFaceCreateCode.GetSubTwoServiceInterFace();
                    // ServiceImpl
                    this.memoEdit25.Text = class_InterFaceCreateCode.GetSubTwoServiceImpl();
                    // Model
                    this.memoEdit26.Text = class_InterFaceCreateCode.GetSubTwoModel();
                    //DTO
                    //this.memoEdit28.Text = class_InterFaceCreateCode.GetMainDTO();
                    // DAO
                    this.memoEdit29.Text = class_InterFaceCreateCode.GetSubTwoDAO();
                    // Control
                    this.memoEdit30.Text = class_InterFaceCreateCode.GetSubTwoControl();
                }
                #endregion

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
        }

        private void textEdit31_EditValueChanged(object sender, EventArgs e)
        {
            this.textEdit30.Text = string.Format("{0}.dao.{1}Mapper", this.textEdit13.Text, this.textEdit31.Text);
            this.textEdit28.Text = (sender as TextEdit).Text;
            this.textEdit52.Text = string.Format("{0}Mapper.xml", (sender as TextEdit).Text);
            this.textEdit55.Text = string.Format("{0}Controller", (sender as TextEdit).Text);
        }

        private void textEdit29_EditValueChanged(object sender, EventArgs e)
        {
            this.textEdit26.Text = string.Format("{0}Map", this.textEdit29.Text);
        }

        private void textEdit28_EditValueChanged(object sender, EventArgs e)
        {
            this.textEdit27.Text = string.Format("{0}.model.{1}", this.textEdit13.Text, this.textEdit28.Text);
        }

        private void textEdit41_EditValueChanged(object sender, EventArgs e)
        {
            this.textEdit40.Text = string.Format("{0}.dao.{1}Mapper", this.textEdit13.Text, this.textEdit41.Text);
            this.textEdit38.Text = (sender as TextEdit).Text;
            this.textEdit53.Text = string.Format("{0}Mapper.xml", (sender as TextEdit).Text);
            this.textEdit56.Text = string.Format("{0}Controller", (sender as TextEdit).Text);
        }

        private void textEdit39_EditValueChanged(object sender, EventArgs e)
        {
            this.textEdit36.Text = string.Format("{0}Map", this.textEdit39.Text);
        }

        private void textEdit38_EditValueChanged(object sender, EventArgs e)
        {
            this.textEdit37.Text = string.Format("{0}.model.{1}", this.textEdit13.Text, this.textEdit38.Text);
        }

        private void radioGroup5_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.radioGroup1.Enabled = radioGroup5.SelectedIndex > 0 ? false : true;
            this.radioGroup3.Enabled = !this.radioGroup1.Enabled;
        }

        private void radioGroup6_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.radioGroup2.Enabled = radioGroup6.SelectedIndex > 0 ? false : true;
            this.radioGroup4.Enabled = !this.radioGroup2.Enabled;
        }

        private void textEdit34_EditValueChanged(object sender, EventArgs e)
        {
            this.textEdit19.Text = String.Format("{0}Dto", (sender as TextEdit).Text);
        }
        private void GetLinkField(GridView gridView, object sender)
        {
            List<Class_LinkField> class_LinkFields = new List<Class_LinkField>();
            for (int index = 0; index < gridView.RowCount; index++)
            {
                DataRow dataRow = gridView.GetDataRow(index);
                Class_LinkField class_LinkField = new Class_LinkField();
                class_LinkField.ParaName = dataRow["ParaName"].ToString();
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
    }
}
