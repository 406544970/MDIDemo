using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTabbedMdi;
using MDIDemo.vou;
using MDIDemo.PublicSetUp;
using MDIDemo;
using DevExpress.XtraTab.ViewInfo;
using System.Text;
using System.Collections.Generic;
using MDIDemo.PublicClass;
using DevExpress.Utils;
using System.Threading;
using System.IO;

namespace DevExpress.XtraBars.Demos.MDIDemo
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain(string VersionNo)
        {
            this.Version = VersionNo;
            InitializeComponent();
            InitTabbedMDI();
            SetCompoment();
        }
        private string mySkinName;
        private const string _Text = "myBatis 粘子层代码生成器";
        bool IsTabbedMdi { get { return biTabbedMDI.Down; } }
        private string Version;

        private void SetCompoment()
        {
            Class_SetUpBar class_SetUpBar = new Class_SetUpBar();
            class_SetUpBar.setBar(this.bar2, "窗体功能");
            class_SetUpBar.setBar(this.bar3, "显示方式");
            class_SetUpBar.setBar(this.bar4, "团队");
            class_SetUpBar.setBar(this.bar6, "个人设置");
            mySkinName = "Metropolis Dark";
            setIniSkin(mySkinName);
            this.barEditItem2.Visibility = BarItemVisibility.Never;
            this.WindowState = FormWindowState.Maximized;
            this.alertControl1.AllowHtmlText = true;
            this.alertControl1.ControlBoxPosition = Alerter.AlertFormControlBoxPosition.Top;
            this.alertControl1.FormLocation = Alerter.AlertFormLocation.BottomRight;

            displayState(string.Format("{0}，您好！", Class_MyInfo.UseNameValue));
        }

        /// <summary>
        /// 显示状态栏
        /// </summary>
        /// <param name="content"></param>
        public void displayState(String content)
        {
            iDocName.Caption = content == null ? "" : content;
        }
        private Form ActiveMDIForm
        {
            get { return this.ActiveMdiChild; }
        }
        public void OpenDeleteWin(string xmlFileName)
        {
            IClass_InterFaceDataBase class_InterFaceDataBase;
            Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
            Class_DeleteAllModel class_DeleteAllModel = new Class_DeleteAllModel();
            Class_DeleteAllModel.Class_InsertDataBase class_DeleteDataBase = new Class_DeleteAllModel.Class_InsertDataBase();
            try
            {
                Class_WindowType class_WindowType = new Class_WindowType();
                class_WindowType.WindowType = "delete";
                Form_Delete form;
                if (xmlFileName != null)
                {
                    if (File.Exists(string.Format("{0}\\delete\\{1}.xml", Application.StartupPath, xmlFileName)))
                    {

                        class_DeleteAllModel = class_PublicMethod.FromXmlToDeleteObject<Class_DeleteAllModel>(xmlFileName);
                        class_DeleteDataBase = class_DeleteAllModel.class_SelectDataBase;
                        switch (class_DeleteDataBase.databaseType)
                        {
                            case "MySql":
                                class_InterFaceDataBase = new Class_MySqlDataBase(class_DeleteDataBase.dataSourceUrl, class_DeleteDataBase.dataBaseName, class_DeleteDataBase.dataSourceUserName, class_DeleteDataBase.dataSourcePassWord, class_DeleteDataBase.Port);
                                break;
                            case "SqlServer 2017":
                                class_InterFaceDataBase = new Class_SqlServer2017DataBase(class_DeleteDataBase.dataSourceUrl, class_DeleteDataBase.dataBaseName, class_DeleteDataBase.dataSourceUserName, class_DeleteDataBase.dataSourcePassWord);
                                break;
                            default:
                                class_InterFaceDataBase = new Class_MySqlDataBase(class_DeleteDataBase.dataSourceUrl, class_DeleteDataBase.dataBaseName, class_DeleteDataBase.dataSourceUserName, class_DeleteDataBase.dataSourcePassWord, class_DeleteDataBase.Port);
                                break;
                        }
                        class_WindowType.XmlFileName = xmlFileName;
                        form = new Form_Delete(mySkinName, xmlFileName);
                        form.Text = string.Format("DELETE：{0}", xmlFileName);
                        form.Tag = class_WindowType;
                        OpenSubForm(form);
                    }
                    else
                        MessageBox.Show(string.Format("文件【{0}.xml】不存在，无法打开界面！", xmlFileName)
                        , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    form = new Form_Delete(mySkinName);
                    form.Text = "新DELETE";
                    form.Tag = class_WindowType;
                    OpenSubForm(form);
                }
            }
            catch (Exception e)
            {
                if (xmlFileName != null)
                    MessageBox.Show(string.Format("数据库[{0}:{3}]：Url[{1}],端口[{2}]，连接失败，无法打开该界面！\r\n异常：{4}。"
                    , class_DeleteDataBase.databaseType
                    , class_DeleteDataBase.dataSourceUrl
                    , class_DeleteDataBase.Port
                    , class_DeleteDataBase.dataBaseName
                    , e.Message)
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(string.Format("异常：{0}。", e.Message)
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void OpenUpdateWin(string xmlFileName)
        {
            IClass_InterFaceDataBase class_InterFaceDataBase;
            Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
            Class_UpdateAllModel class_UpdateAllModel = new Class_UpdateAllModel();
            Class_UpdateAllModel.Class_InsertDataBase class_UpdateDataBase = new Class_UpdateAllModel.Class_InsertDataBase();
            try
            {
                Class_WindowType class_WindowType = new Class_WindowType();
                class_WindowType.WindowType = "update";
                Form_Update form;
                if (xmlFileName != null)
                {
                    if (File.Exists(string.Format("{0}\\update\\{1}.xml", Application.StartupPath, xmlFileName)))
                    {
                        class_UpdateAllModel = class_PublicMethod.FromXmlToUpdateObject<Class_UpdateAllModel>(xmlFileName);
                        class_UpdateDataBase = class_UpdateAllModel.class_SelectDataBase;
                        switch (class_UpdateDataBase.databaseType)
                        {
                            case "MySql":
                                class_InterFaceDataBase = new Class_MySqlDataBase(class_UpdateDataBase.dataSourceUrl, class_UpdateDataBase.dataBaseName, class_UpdateDataBase.dataSourceUserName, class_UpdateDataBase.dataSourcePassWord, class_UpdateDataBase.Port);
                                break;
                            case "SqlServer 2017":
                                class_InterFaceDataBase = new Class_SqlServer2017DataBase(class_UpdateDataBase.dataSourceUrl, class_UpdateDataBase.dataBaseName, class_UpdateDataBase.dataSourceUserName, class_UpdateDataBase.dataSourcePassWord);
                                break;
                            default:
                                class_InterFaceDataBase = new Class_MySqlDataBase(class_UpdateDataBase.dataSourceUrl, class_UpdateDataBase.dataBaseName, class_UpdateDataBase.dataSourceUserName, class_UpdateDataBase.dataSourcePassWord, class_UpdateDataBase.Port);
                                break;
                        }
                        class_WindowType.XmlFileName = xmlFileName;
                        form = new Form_Update(mySkinName, xmlFileName);
                        form.Text = string.Format("UPDATE：{0}", xmlFileName);
                        form.Tag = class_WindowType;
                        OpenSubForm(form);
                    }
                    else
                        MessageBox.Show(string.Format("文件【{0}.xml】不存在，无法打开界面！", xmlFileName)
                        , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    form = new Form_Update(mySkinName);
                    form.Text = "新UPDATE";
                    form.Tag = class_WindowType;
                    OpenSubForm(form);
                }
            }
            catch (Exception e)
            {
                if (xmlFileName != null)
                    MessageBox.Show(string.Format("数据库[{0}:{3}]：Url[{1}],端口[{2}]，连接失败，无法打开该界面！\r\n异常：{4}。"
                    , class_UpdateDataBase.databaseType
                    , class_UpdateDataBase.dataSourceUrl
                    , class_UpdateDataBase.Port
                    , class_UpdateDataBase.dataBaseName
                    , e.Message)
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(string.Format("异常：{0}。", e.Message)
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void OpenInsertWin(string xmlFileName)
        {
            IClass_InterFaceDataBase class_InterFaceDataBase;
            Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
            Class_InsertAllModel class_InsertAllModel = new Class_InsertAllModel();
            Class_InsertAllModel.Class_InsertDataBase class_InsertDataBase = new Class_InsertAllModel.Class_InsertDataBase();
            try
            {
                Class_WindowType class_WindowType = new Class_WindowType();
                class_WindowType.WindowType = "insert";
                Form_Insert form;
                if (xmlFileName != null)
                {
                    if (File.Exists(string.Format("{0}\\insert\\{1}.xml", Application.StartupPath, xmlFileName)))
                    {
                        class_InsertAllModel = class_PublicMethod.FromXmlToInsertObject<Class_InsertAllModel>(xmlFileName);
                        class_InsertDataBase = class_InsertAllModel.class_SelectDataBase;
                        switch (class_InsertDataBase.databaseType)
                        {
                            case "MySql":
                                class_InterFaceDataBase = new Class_MySqlDataBase(class_InsertDataBase.dataSourceUrl, class_InsertDataBase.dataBaseName, class_InsertDataBase.dataSourceUserName, class_InsertDataBase.dataSourcePassWord, class_InsertDataBase.Port);
                                break;
                            case "SqlServer 2017":
                                class_InterFaceDataBase = new Class_SqlServer2017DataBase(class_InsertDataBase.dataSourceUrl, class_InsertDataBase.dataBaseName, class_InsertDataBase.dataSourceUserName, class_InsertDataBase.dataSourcePassWord);
                                break;
                            default:
                                class_InterFaceDataBase = new Class_MySqlDataBase(class_InsertDataBase.dataSourceUrl, class_InsertDataBase.dataBaseName, class_InsertDataBase.dataSourceUserName, class_InsertDataBase.dataSourcePassWord, class_InsertDataBase.Port);
                                break;
                        }
                        class_WindowType.XmlFileName = xmlFileName;
                        form = new Form_Insert(mySkinName, xmlFileName);
                        form.Text = string.Format("INSERT：{0}", xmlFileName);
                        form.Tag = class_WindowType;
                        OpenSubForm(form);
                    }
                    else
                        MessageBox.Show(string.Format("文件【{0}.xml】不存在，无法打开界面！", xmlFileName)
                        , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    form = new Form_Insert(mySkinName);
                    form.Text = "新INSERT";
                    form.Tag = class_WindowType;
                    OpenSubForm(form);
                }
            }
            catch (Exception e)
            {
                if (xmlFileName != null)
                    MessageBox.Show(string.Format("数据库[{0}:{3}]：Url[{1}],端口[{2}]，连接失败，无法打开该界面！\r\n异常：{4}。"
                    , class_InsertDataBase.databaseType
                    , class_InsertDataBase.dataSourceUrl
                    , class_InsertDataBase.Port
                    , class_InsertDataBase.dataBaseName
                    , e.Message)
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(string.Format("异常：{0}。", e.Message)
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OpenSelectWin(string xmlFileName)
        {
            IClass_InterFaceDataBase class_InterFaceDataBase;
            Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
            Class_SelectAllModel class_SelectAllModel = new Class_SelectAllModel();
            Class_SelectAllModel.Class_SelectDataBase class_SelectDataBase = new Class_SelectAllModel.Class_SelectDataBase();
            try
            {
                Class_WindowType class_WindowType = new Class_WindowType();
                class_WindowType.WindowType = "select";
                Form_Select form;
                if (xmlFileName != null)
                {
                    if (File.Exists(string.Format("{0}\\select\\{1}.xml", Application.StartupPath, xmlFileName)))
                    {
                        class_SelectAllModel = class_PublicMethod.FromXmlToSelectObject<Class_SelectAllModel>(xmlFileName);
                        class_SelectDataBase = class_SelectAllModel.class_SelectDataBase;
                        switch (class_SelectDataBase.databaseType)
                        {
                            case "MySql":
                                class_InterFaceDataBase = new Class_MySqlDataBase(class_SelectDataBase.dataSourceUrl, class_SelectDataBase.dataBaseName, class_SelectDataBase.dataSourceUserName, class_SelectDataBase.dataSourcePassWord, class_SelectDataBase.Port);
                                break;
                            case "SqlServer 2017":
                                class_InterFaceDataBase = new Class_SqlServer2017DataBase(class_SelectDataBase.dataSourceUrl, class_SelectDataBase.dataBaseName, class_SelectDataBase.dataSourceUserName, class_SelectDataBase.dataSourcePassWord);
                                break;
                            default:
                                class_InterFaceDataBase = new Class_MySqlDataBase(class_SelectDataBase.dataSourceUrl, class_SelectDataBase.dataBaseName, class_SelectDataBase.dataSourceUserName, class_SelectDataBase.dataSourcePassWord, class_SelectDataBase.Port);
                                break;
                        }
                        class_WindowType.XmlFileName = xmlFileName;
                        form = new Form_Select(mySkinName, xmlFileName);
                        form.Text = string.Format("SELECT：{0}", xmlFileName);
                        form.Tag = class_WindowType;
                        OpenSubForm(form);
                    }
                    else
                        MessageBox.Show(string.Format("文件【{0}.xml】不存在，无法打开界面！", xmlFileName)
                        , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    form = new Form_Select(mySkinName);
                    form.Text = "新SELECT";
                    form.Tag = class_WindowType;
                    OpenSubForm(form);
                }
            }
            catch (Exception e)
            {
                if (xmlFileName != null)
                    MessageBox.Show(string.Format("数据库[{0}:{3}]：Url[{1}],端口[{2}]，连接失败，无法打开该界面！\r\n异常：{4}。"
                    , class_SelectDataBase.databaseType
                    , class_SelectDataBase.dataSourceUrl
                    , class_SelectDataBase.Port
                    , class_SelectDataBase.dataBaseName
                    , e.Message)
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(string.Format("异常：{0}。", e.Message)
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OpenHistoryWin()
        {
            List<Class_WindowType> class_WindowTypes = new List<Class_WindowType>();
            Class_SQLiteOperator class_SQLiteOperator = new Class_SQLiteOperator();
            class_WindowTypes = class_SQLiteOperator.GetWindowTypes();
            this.barEditItem2.Visibility = BarItemVisibility.Always;
            if ((class_WindowTypes != null) && (class_WindowTypes.Count > 0))
            {
                this.repositoryItemProgressBar2.Maximum = class_WindowTypes.Count;
                this.repositoryItemProgressBar2.Minimum = 0;
                Class_WindowType OpenPageTag = new Class_WindowType();
                int Counter = 0;
                foreach (Class_WindowType class_WindowType in class_WindowTypes)
                {
                    if (class_WindowType.ActiveSign)
                    {
                        OpenPageTag.XmlFileName = class_WindowType.XmlFileName;
                        OpenPageTag.WindowType = class_WindowType.WindowType;
                        OpenPageTag.ActiveSign = class_WindowType.ActiveSign;
                    }
                    switch (class_WindowType.WindowType)
                    {
                        case "select":
                            OpenSelectWin(class_WindowType.XmlFileName);
                            break;
                        case "insert":
                            OpenInsertWin(class_WindowType.XmlFileName);
                            break;
                        case "update":
                            OpenUpdateWin(class_WindowType.XmlFileName);
                            break;
                        case "delete":
                            OpenDeleteWin(class_WindowType.XmlFileName);
                            break;
                        case "welcome":
                            openFirstPage();
                            break;
                        default:
                            OpenSelectWin(class_WindowType.XmlFileName);
                            break;
                    }
                    this.barEditItem2.EditValue = (++Counter).ToString();
                    Thread.Sleep(0);
                    Application.DoEvents();
                }
                int num = -1;
                foreach (Form Children in this.MdiChildren)
                {
                    num++;
                    Class_WindowType ChildrenTag = new Class_WindowType()
                    {
                        XmlFileName = (Children.Tag as Class_WindowType).XmlFileName,
                        WindowType = (Children.Tag as Class_WindowType).WindowType,
                    };
                    if (ChildrenTag.XmlFileName.Equals(OpenPageTag.XmlFileName))
                    {
                        Children.WindowState = FormWindowState.Maximized;
                        Children.Select();
                        Children.BringToFront();
                        if (this.xtraTabbedMdiManager1.MdiParent != null)
                        {
                            this.xtraTabbedMdiManager1.Pages[Children].TabControl.ViewInfo.SelectedTabPageIndex = num;
                        }
                        break;
                    }
                }
            }
            this.barEditItem2.Visibility = BarItemVisibility.Never;
        }
        private void iOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_WindowSelect form_WindowSelect = new Form_WindowSelect();
            form_WindowSelect.OperateType = "打开";
            if (form_WindowSelect.ShowDialog() == DialogResult.OK)
            {
                string PageKey = form_WindowSelect.PageKey;
                if ((PageKey != null) && (PageKey.Length > 0))
                {
                    switch (form_WindowSelect.PageType)
                    {
                        case "select":
                            OpenSelectWin(PageKey);
                            break;
                        case "insert":
                            OpenInsertWin(PageKey);
                            break;
                        case "update":
                            OpenUpdateWin(PageKey);
                            break;
                        case "delete":
                            OpenDeleteWin(PageKey);
                            break;
                        default:
                            OpenSelectWin(PageKey);
                            break;
                    }
                }
            }
            form_WindowSelect.Dispose();
        }
        private string OpenSub()
        {
            string AllPathFileName = null;
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "打开窗体文件";
            fdlg.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            fdlg.Filter = "XML files（*.xml）|*.xml|All files(*.*)|*.* ";
            //fdlg.FilterIndex = 2;
            /*
             *如果值为false，那么下一次选择文件的初始目录是上一次你选择的那个目录，
             *不固定；如果值为true，每次打开这个对话框初始目录不随你的选择而改变，是固定的  
             */
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                AllPathFileName = System.IO.Path.GetFileNameWithoutExtension(fdlg.FileName);

            }
            return AllPathFileName;
        }
        private void RefreshForm(bool IsRefresh)
        {
            if (IsRefresh)
                Refresh();
        }
        private void OpenSubForm(Form OpenPage)
        {
            RefreshForm(true);
            bool finder = false;
            int num = -1;
            foreach (Form Children in this.MdiChildren)
            {
                num++;
                Class_WindowType ChildrenTag = new Class_WindowType()
                {
                    XmlFileName = (Children.Tag as Class_WindowType).XmlFileName,
                    WindowType = (Children.Tag as Class_WindowType).WindowType,
                };
                Class_WindowType OpenPageTag = new Class_WindowType()
                {
                    XmlFileName = (OpenPage.Tag as Class_WindowType).XmlFileName,
                    WindowType = (OpenPage.Tag as Class_WindowType).WindowType,
                };
                if (OpenPageTag.XmlFileName != null && ChildrenTag.XmlFileName.Equals(OpenPageTag.XmlFileName))
                {
                    finder = true;
                    Children.WindowState = FormWindowState.Maximized;
                    Children.Select();
                    Children.BringToFront();
                    if (this.xtraTabbedMdiManager1.MdiParent != null)
                    {
                        this.xtraTabbedMdiManager1.Pages[Children].TabControl.ViewInfo.SelectedTabPageIndex = num;
                    }
                    break;
                }
            }
            if (!finder)
            {
                WaitDialogForm waitDialogForm = new WaitDialogForm("正在玩命加载中......", "温馨提示");
                OpenPage.MdiParent = this;
                OpenPage.WindowState = FormWindowState.Maximized;
                OpenPage.Show();
                if (this.xtraTabbedMdiManager1.MdiParent != null)
                {
                    this.xtraTabbedMdiManager1.Pages[OpenPage].TabControl.ViewInfo.SelectedTabPageIndex = base.MdiChildren.Length - 1;
                }
                waitDialogForm.Close();
                waitDialogForm.Dispose();

            }

            RefreshForm(false);
        }
        private void enableCloseMenu(bool isEnable)
        {
            barButtonItem14.Enabled = isEnable;
            barButtonItem15.Enabled = isEnable;
            barButtonItem17.Enabled = isEnable;
        }
        private void frmMain_MdiChildActivate(object sender, System.EventArgs e)
        {
        }

        private void iClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ActiveMDIForm != null)
                ActiveMDIForm.Close();
        }

        private void iExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void iCascade_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void iTileHorizontal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void iTileVertical_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void iAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "http://www.baidu.com/";
            process.StartInfo.Verb = "Open";
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            process.Start();
        }

        private void setIniSkin(string skinName)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
            barManager1.GetController().PaintStyleName = "Skin";
        }
        private void ips_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barManager1.GetController().PaintStyleName = e.Item.Description;
            MessageBox.Show(e.Item.Description);
            barManager1.GetController().ResetStyleDefaults();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetDefaultStyle();
        }


        private void InitTabbedMDI()
        {
            xtraTabbedMdiManager1.MdiParent = IsTabbedMdi ? this : null;
            iCascade.Visibility = iTileHorizontal.Visibility = iTileVertical.Visibility = IsTabbedMdi ? BarItemVisibility.Never : BarItemVisibility.Always;
            this.biTabbedMDI.Caption = IsTabbedMdi ? "标签方式" : "传统方式";
        }

        private void biTabbedMDI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitTabbedMDI();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = _Text + " V " + this.Version + "   作者：梁昊";
            OpenHistoryWin();
            Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
            if ((this.MdiChildren.Length == 0) && (class_PublicMethod.GetOpenWelcome()))
            {
                openFirstPage();
            }
        }

        #region 公共方法
        public string getVersion()
        {
            return this.Version;
        }
        public string getText()
        {
            return _Text;
        }
        #endregion

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.MdiChildren.Length == 0)
                return;
            if (MessageBox.Show(string.Format("您确定要关闭当前【{0}】个子窗体吗？", this.MdiChildren.Length.ToString()), "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                while (this.MdiChildren.Length > 0)
                {
                    this.MdiChildren[0].Close();
                }
            }
        }
        public void displayErrorMessage(string textInfo)
        {
            displayAlertMessage("错误", textInfo, null, 1);
        }
        public void displayOkMessage(string textInfo)
        {
            displayAlertMessage("温馨", textInfo, null, 3);
        }
        /// <summary>
        /// 提示框方法
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="textInfo"></param>
        /// <param name="hotTrackedText"></param>
        /// <param name="imageIndex"></param>
        private void displayAlertMessage(string caption, string textInfo, string hotTrackedText, int imageIndex)
        {
            caption = string.Format("<size=10><i>{0}</i>提示", caption);
            textInfo = string.Format("<size=12>{0}", textInfo);
            alertControl1.Show(
                this.FindForm(),
                caption,
                textInfo,
                hotTrackedText,
                this.imageCollection1.Images[imageIndex], null, true);
        }
        private void openFirstPage()
        {
            Class_WindowType class_WindowType = new Class_WindowType();
            class_WindowType.WindowType = "welcome";
            class_WindowType.XmlFileName = "代码生成器欢迎您啊！";
            Form_welCome form_WelCome = new Form_welCome(mySkinName);
            form_WelCome.Tag = class_WindowType;
            form_WelCome.mainPage = this;
            OpenSubForm(form_WelCome);

            displayOkMessage(string.Format("{0}登录成功", Class_MyInfo.UseNameValue));
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            openFirstPage();
        }
        private void xtraTabbedMdiManager1_MouseUp(object sender, MouseEventArgs e)
        {
            //点左键无效, 必须是点右键弹出菜单
            if (e.Button != MouseButtons.Right)
                return;
            BaseTabHitInfo hint = xtraTabbedMdiManager1.CalcHitInfo(e.Location);
            //点击有效,且点击在TabPage标题上
            if (hint.IsValid && (hint.Page != null))
            {
                //有效子窗体
                if (xtraTabbedMdiManager1.SelectedPage.MdiChild != null)
                {
                    Point p = xtraTabbedMdiManager1.SelectedPage.MdiChild.PointToScreen(e.Location);
                    enableCloseMenu(xtraTabbedMdiManager1.Pages.Count == 1 ? false : true);
                    this.popupMenu2.ShowPopup(p);
                }
            }
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form Children in this.MdiChildren)
            {
                if (ActiveMDIForm != Children)
                {
                    Children.Close();
                }
            }
        }
        /// <summary>
        /// 关闭左边的页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<Form> list = new List<Form>();
            bool isRigth = false;
            foreach (XtraMdiTabPage xtra in xtraTabbedMdiManager1.Pages)
            {
                if (ActiveMDIForm == xtra.MdiChild)
                {
                    isRigth = true;
                    continue;
                }
                if (isRigth)
                {
                    list.Add(xtra.MdiChild);
                }
            }
            foreach (Form form in list)
            {
                xtraTabbedMdiManager1.Pages[form].MdiChild.Close();
            }
            list.Clear();
        }
        /// <summary>
        /// 关闭右边的页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<Form> list = new List<Form>();
            bool isRigth = true;
            foreach (XtraMdiTabPage xtra in xtraTabbedMdiManager1.Pages)
            {
                if (ActiveMDIForm == xtra.MdiChild)
                {
                    isRigth = false;
                    break;
                }
                if (isRigth)
                {
                    list.Add(xtra.MdiChild);
                }
            }
            foreach (Form form in list)
            {
                xtraTabbedMdiManager1.Pages[form].MdiChild.Close();
            }
            list.Clear();
        }

        private void alertControl1_BeforeFormShow(object sender, Alerter.AlertFormEventArgs e)
        {
            e.AlertForm.Size = new Size(250, alertControl1.AutoHeight ? 100 : 110);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您确定要退出此系统吗？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                WaitDialogForm waitDialogForm = new WaitDialogForm("正在保存当前状态......", "温馨提示");
                List<Class_WindowType> class_WindowTypes = new List<Class_WindowType>();
                foreach (XtraMdiTabPage xtra in xtraTabbedMdiManager1.Pages)
                {
                    if (ActiveMDIForm.Tag != null)
                    {
                        Class_WindowType class_WindowType = new Class_WindowType();
                        class_WindowType = xtra.MdiChild.Tag as Class_WindowType;
                        if (class_WindowType != null)
                        {
                            if (class_WindowType.XmlFileName == (ActiveMDIForm.Tag as Class_WindowType).XmlFileName)
                                class_WindowType.ActiveSign = true;
                            class_WindowTypes.Add(class_WindowType);
                        }
                    }
                }
                Class_SQLiteOperator class_SQLiteOperator = new Class_SQLiteOperator();
                class_SQLiteOperator.SaveCurrentOpenWin(class_WindowTypes);
                waitDialogForm.Close();
                waitDialogForm.Dispose();
                e.Cancel = false;
            }
            else
                e.Cancel = true;
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
            Font font = new Font("Tahoma", class_PublicMethod.GetGridFontSize());
            FontDialog fontDialog = new FontDialog
            {
                Font = font
            };
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                class_PublicMethod.SetGridFontSize(fontDialog.Font.Size);
            }
            fontDialog.Dispose();
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
            Form_WindowSelect form_WindowSelect = new Form_WindowSelect(true);
            form_WindowSelect.OperateType = "效仿添加";
            if (form_WindowSelect.ShowDialog() == DialogResult.OK)
            {
                string PageKey = form_WindowSelect.PageKey;
                if ((PageKey != null) && (PageKey.Length > 0))
                {
                    PageKey = class_PublicMethod.CopyToNewXml(PageKey, form_WindowSelect.NewPageType, form_WindowSelect.PageType);
                    if (PageKey != null)
                    {
                        switch (form_WindowSelect.NewPageType)
                        {
                            case "select":
                                OpenSelectWin(PageKey);
                                break;
                            case "insert":
                                OpenInsertWin(PageKey);
                                break;
                            case "update":
                                OpenUpdateWin(PageKey);
                                break;
                            case "delete":
                                OpenDeleteWin(PageKey);
                                break;
                            default:
                                OpenSelectWin(PageKey);
                                break;
                        }
                    }
                }
            }
            form_WindowSelect.Dispose();
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!Class_MyInfo.UseTypeValue.Equals("R005"))
            {
                Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
                Form_WindowSelect form_WindowSelect = new Form_WindowSelect();
                form_WindowSelect.OperateType = "删除";
                if (form_WindowSelect.ShowDialog() == DialogResult.OK)
                {
                    if (class_PublicMethod.DeleteXml(form_WindowSelect.PageKey, form_WindowSelect.PageType))
                    {
                        if (IsTabbedMdi)
                        {
                            XtraMdiTabPage xtraMdiTabPage = null;
                            foreach (XtraMdiTabPage xtra in xtraTabbedMdiManager1.Pages)
                            {
                                if ((xtra.MdiChild.Tag as Class_WindowType).XmlFileName == form_WindowSelect.PageKey)
                                    xtraMdiTabPage = xtra;
                            }
                            if (xtraMdiTabPage != null)
                                xtraMdiTabPage.MdiChild.Close();
                        }
                        else
                        {
                            Form form = null;
                            foreach (Form item in this.MdiChildren)
                            {
                                if ((item.Tag as Class_WindowType).XmlFileName == form_WindowSelect.PageKey)
                                    form = item;
                            }
                            if (form != null)
                                form.Close();
                        }
                        displayAlertMessage("温馨", "指定窗体已删除成功！", null, 3);
                    }
                    else
                        displayAlertMessage("温馨", "指定窗体删除失败！", null, 3);
                }
                form_WindowSelect.Dispose();
            }
            else
                MessageBox.Show("前端开发者没有删除权限!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenSelectWin(null);
        }

        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenInsertWin(null);
        }

        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenUpdateWin(null);
        }

        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenDeleteWin(null);
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
            Class_DataBaseConDefault class_DataBaseConDefault = new Class_DataBaseConDefault();
            class_DataBaseConDefault = class_PublicMethod.FromXmlToDefaultValueObject<Class_DataBaseConDefault>("DataBaseDefaultValues");
            Form_DataBaseDefaultSet form_DataBaseDefaultSet = new Form_DataBaseDefaultSet(class_DataBaseConDefault);
            if (form_DataBaseDefaultSet.ShowDialog() == DialogResult.OK)
            {
                if (class_PublicMethod.DataBaseDefaultValueToXml("DataBaseDefaultValues", form_DataBaseDefaultSet.class_DataBaseConDefault))
                    MessageBox.Show("已将数据库连接默认值，保存到本地!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            form_DataBaseDefaultSet.Dispose();
        }
        /// <summary>
        /// 综合设置
        /// </summary>
        private void _AllSetUp()
        {
            Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
            Class_AllParamSetUp class_AllParamSetUp = new Class_AllParamSetUp();
            class_AllParamSetUp = class_PublicMethod.FromXmlToAllParamSetUpObject<Class_AllParamSetUp>("Class_AllParamSetUp");
            Form_AllSetUp form_AllSetUp = new Form_AllSetUp(class_AllParamSetUp);
            if (form_AllSetUp.ShowDialog() == DialogResult.OK)
            {
                if (class_PublicMethod.DataBaseAllParamSetUpValueToXml("Class_AllParamSetUp", form_AllSetUp.class_AllParamSetUp))
                    MessageBox.Show("已将参数默认值，保存到本地!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            form_AllSetUp.Dispose();
        }
        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            _AllSetUp();
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form_UpdatePass form_UpdatePass = new Form_UpdatePass();
            if (form_UpdatePass.ShowDialog() == DialogResult.OK)
            {
                Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
                if (class_PublicMethod.UpdatePassWord(form_UpdatePass.PassWord) > 0)
                    MessageBox.Show("密码修改成功！", "温馨提示"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Information
                        , MessageBoxDefaultButton.Button1);
                else
                    MessageBox.Show("密码修改失败！", "温馨提示"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error
                    , MessageBoxDefaultButton.Button1);
            }
            form_UpdatePass.Dispose();
        }
    }
}
