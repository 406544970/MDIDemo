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

namespace DevExpress.XtraBars.Demos.MDIDemo
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
            InitTabbedMDI();
            SetCompoment();
        }
        private string mySkinName;
        private const string _Text = "myBatis 代码生成器";
        private const string _Version = "1.0";
        bool IsTabbedMdi { get { return biTabbedMDI.Down; } }
        public static string Version => _Version;

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

            displayState(string.Format("{0}，您好！", Class_UseInfo.UserName));
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
        public void OpenSelectWin(string xmlFileName)
        {
            Class_WindowType class_WindowType = new Class_WindowType();
            class_WindowType.WindowType = "select";
            Form_Select form_Select;
            if (xmlFileName == null)
            {
                form_Select = new Form_Select(mySkinName);
                form_Select.Text = "新SELECT";
                form_Select.Tag = class_WindowType;
            }
            else
            {
                class_WindowType.XmlFileName = xmlFileName;
                form_Select = new Form_Select(mySkinName, xmlFileName);
                form_Select.Text = string.Format("SELECT：{0}", xmlFileName);
                form_Select.Tag = class_WindowType;
            }
            OpenSubForm(form_Select);
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
                            break;
                        case "update":
                            break;
                        case "delete":
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
                            break;
                        case "update":
                            break;
                        case "delete":
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
                if (ChildrenTag.XmlFileName.Equals(OpenPageTag.XmlFileName))
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
            if (MessageBox.Show("您确定要退出此系统吗？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                Close();
            }
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
            this.Text = _Text + " V " + _Version + "   作者：梁昊";
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
            return _Version;
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

            displayOkMessage(string.Format("{0}登录成功", Class_UseInfo.UserName));
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
            e.AlertForm.Size = new Size(250,
            alertControl1.AutoHeight ? 100 : 110);

        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenSelectWin(null);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            WaitDialogForm waitDialogForm = new WaitDialogForm("正在保存当前状态......", "温馨提示");
            List<Class_WindowType> class_WindowTypes = new List<Class_WindowType>();
            foreach (XtraMdiTabPage xtra in xtraTabbedMdiManager1.Pages)
            {
                Class_WindowType class_WindowType = new Class_WindowType();
                class_WindowType = xtra.MdiChild.Tag as Class_WindowType;
                if (class_WindowType.XmlFileName == (ActiveMDIForm.Tag as Class_WindowType).XmlFileName)
                    class_WindowType.ActiveSign = true;
                class_WindowTypes.Add(class_WindowType);
            }
            Class_SQLiteOperator class_SQLiteOperator = new Class_SQLiteOperator();
            class_SQLiteOperator.SaveCurrentOpenWin(class_WindowTypes);
            waitDialogForm.Close();
            waitDialogForm.Dispose();
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
            Form_WindowSelect form_WindowSelect = new Form_WindowSelect();
            form_WindowSelect.OperateType = "效仿添加";
            if (form_WindowSelect.ShowDialog() == DialogResult.OK)
            {
                string PageKey = form_WindowSelect.PageKey;
                if ((PageKey != null) && (PageKey.Length > 0))
                {
                    switch (form_WindowSelect.PageType)
                    {
                        case "select":
                            PageKey = class_PublicMethod.CopyToNewXml(PageKey, form_WindowSelect.PageType);
                            if (PageKey != null)
                            {
                                OpenSelectWin(PageKey);
                            }
                            break;
                        case "insert":
                            break;
                        case "update":
                            break;
                        case "delete":
                            break;
                        default:
                            PageKey = class_PublicMethod.CopyToNewXml(PageKey, form_WindowSelect.PageType);
                            if (class_PublicMethod.CopyToNewXml(PageKey, form_WindowSelect.PageType) != null)
                            {
                                OpenSelectWin(PageKey);
                            }
                            break;
                    }
                }
            }
            form_WindowSelect.Dispose();
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
            Form_WindowSelect form_WindowSelect = new Form_WindowSelect();
            form_WindowSelect.OperateType = "删除";
            if (form_WindowSelect.ShowDialog() == DialogResult.OK)
            {
                if (class_PublicMethod.DeleteXml(form_WindowSelect.PageKey, form_WindowSelect.PageType))
                {
                    XtraMdiTabPage xtraMdiTabPage = null;
                    foreach (XtraMdiTabPage xtra in xtraTabbedMdiManager1.Pages)
                    {
                        Class_WindowType class_WindowType = new Class_WindowType();
                        class_WindowType = xtra.MdiChild.Tag as Class_WindowType;
                        if (class_WindowType.XmlFileName == (ActiveMDIForm.Tag as Class_WindowType).XmlFileName)
                            xtraMdiTabPage = xtra;
                    }
                    if (xtraMdiTabPage != null)
                        xtraMdiTabPage.MdiChild.Close();
                    displayAlertMessage("温馨", "指定窗体已删除成功！", null, 3);
                }
                else
                    displayAlertMessage("温馨", "指定窗体删除失败！", null, 3);
            }
            form_WindowSelect.Dispose();
        }
    }
}
