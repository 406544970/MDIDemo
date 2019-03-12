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
        private const string _Text = "myBatis ճ�Ӳ����������";
        private const string _Version = "1.0";
        bool IsTabbedMdi { get { return biTabbedMDI.Down; } }
        public static string Version => _Version;

        private void SetCompoment()
        {
            Class_SetUpBar class_SetUpBar = new Class_SetUpBar();
            class_SetUpBar.setBar(this.bar2, "���幦��");
            class_SetUpBar.setBar(this.bar3, "��ʾ��ʽ");
            class_SetUpBar.setBar(this.bar4, "�Ŷ�");
            class_SetUpBar.setBar(this.bar6, "��������");
            mySkinName = "Metropolis Dark";
            setIniSkin(mySkinName);
            this.barEditItem2.Visibility = BarItemVisibility.Never;
            this.WindowState = FormWindowState.Maximized;
            this.alertControl1.AllowHtmlText = true;
            this.alertControl1.ControlBoxPosition = Alerter.AlertFormControlBoxPosition.Top;
            this.alertControl1.FormLocation = Alerter.AlertFormLocation.BottomRight;

            displayState(string.Format("{0}�����ã�", Class_UseInfo.UserName));
        }

        /// <summary>
        /// ��ʾ״̬��
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
            Form_Select form_Select;
            if (xmlFileName == null)
            {
                form_Select = new Form_Select(mySkinName);
                form_Select.Text = "��SELECT";
                form_Select.Tag = "������ţ���";
            }
            else
            {
                form_Select = new Form_Select(mySkinName, xmlFileName);
                form_Select.Text = string.Format("SELECT��{0}", xmlFileName);
                form_Select.Tag = xmlFileName;
            }
            OpenSubForm(form_Select);
        }
        private void iOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_WindowSelect form_WindowSelect = new Form_WindowSelect();
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
            fdlg.Title = "�򿪴����ļ�";
            fdlg.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            fdlg.Filter = "XML files��*.xml��|*.xml|All files(*.*)|*.* ";
            //fdlg.FilterIndex = 2;
            /*
             *���ֵΪfalse����ô��һ��ѡ���ļ��ĳ�ʼĿ¼����һ����ѡ����Ǹ�Ŀ¼��
             *���̶������ֵΪtrue��ÿ�δ�����Ի����ʼĿ¼�������ѡ����ı䣬�ǹ̶���  
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
                if (Children.Tag.Equals(OpenPage.Tag))
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
                WaitDialogForm waitDialogForm = new WaitDialogForm("��������������......", "��ܰ��ʾ");
                OpenPage.MdiParent = this;
                OpenPage.WindowState = FormWindowState.Maximized;
                OpenPage.Show();
                if (this.xtraTabbedMdiManager1.MdiParent != null)
                {
                    this.xtraTabbedMdiManager1.Pages[OpenPage].TabControl.ViewInfo.SelectedTabPageIndex = base.MdiChildren.Length - 1;
                }
                waitDialogForm.Close();
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
            //
        }

        private void iClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ActiveMDIForm != null)
                ActiveMDIForm.Close();
        }

        private void iExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("��ȷ��Ҫ�˳���ϵͳ��", "��ܰ��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
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
            process.StartInfo.FileName = "http://www.jk2002.cn/";
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
            this.biTabbedMDI.Caption = IsTabbedMdi ? "��ǩ��ʽ" : "��ͳ��ʽ";
        }

        private void biTabbedMDI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitTabbedMDI();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = _Text + " V " + _Version;
            openFirstPage();
        }

        #region ��������
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
            if (MessageBox.Show(string.Format("��ȷ��Ҫ�رյ�ǰ��{0}�����Ӵ�����", this.MdiChildren.Length.ToString()), "��ܰ��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                while (this.MdiChildren.Length > 0)
                {
                    this.MdiChildren[0].Close();
                }
            }
        }
        public void displayErrorMessage(string textInfo)
        {
            displayAlertMessage("����", textInfo, null, 1);
        }
        public void displayOkMessage(string textInfo)
        {
            displayAlertMessage("��ܰ", textInfo, null, 3);
        }
        /// <summary>
        /// ��ʾ�򷽷�
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="textInfo"></param>
        /// <param name="hotTrackedText"></param>
        /// <param name="imageIndex"></param>
        private void displayAlertMessage(string caption, string textInfo, string hotTrackedText, int imageIndex)
        {
            caption = string.Format("<size=10><i>{0}</i>��ʾ", caption);
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
            Form_welCome form_WelCome = new Form_welCome(mySkinName);
            form_WelCome.Tag = "������������ӭ������Ŷ����������";
            form_WelCome.mainPage = this;
            OpenSubForm(form_WelCome);

            displayOkMessage(string.Format("{0}��¼�ɹ�", Class_UseInfo.UserName));
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            openFirstPage();
        }
        private void xtraTabbedMdiManager1_MouseUp(object sender, MouseEventArgs e)
        {
            //�������Ч, �����ǵ��Ҽ������˵�
            if (e.Button != MouseButtons.Right)
                return;
            BaseTabHitInfo hint = xtraTabbedMdiManager1.CalcHitInfo(e.Location);
            //�����Ч,�ҵ����TabPage������
            if (hint.IsValid && (hint.Page != null))
            {
                //��Ч�Ӵ���
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
        /// �ر���ߵ�ҳ��
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
        /// �ر��ұߵ�ҳ��
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
    }
}