using MDIDemo.Model;
using MDIDemo.PublicClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDIDemo
{
    public partial class Form_Log : DevExpress.XtraEditors.XtraForm
    {
        public Form_Log(string version)
        {
            this.version = version;
            InitializeComponent();
            Ini();
        }
        private string version;
        private int LogCount;
        private Class_SQLiteOperator class_SQLiteOperator;

        private void setIniSkin(string skinName)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
            //barManager1.GetController().PaintStyleName = "Skin";
        }

        private void Ini()
        {
            //this.textEdit1.Text = "";
            //this.textEdit2.Text = "";
            setIniSkin("Metropolis Dark");
            this.Text = string.Format("登录 V {0}", this.version);
            class_SQLiteOperator = new Class_SQLiteOperator();
            LogCount = 5;
        }

        private void LogOk()
        {
            Class_Remote class_Remote = new Class_Remote("dictionary", false);
            List<Class_ParaArray> class_ParaArrays = new List<Class_ParaArray>();
            Class_ParaArray class_ParaArray = new Class_ParaArray()
            {
                ParaName = "num",
                ParaValue = this.textEdit1.Text
            };
            class_ParaArrays.Add(class_ParaArray);
            Class_ParaArray class_ParaArrayPass = new Class_ParaArray()
            {
                ParaName = "passWord",
                ParaValue = this.textEdit2.Text
            };
            class_ParaArrays.Add(class_ParaArrayPass);
            try
            {
                ResultVO<Class_Use> resultVO = new ResultVO<Class_Use>();
                resultVO = class_Remote.UseLogCS<Class_Use>(class_ParaArrays);
                if (resultVO.code == 0)
                {
                    Class_Use class_Use = new Class_Use();
                    class_Use = resultVO.data;
                    Class_MyInfo.UseIdValue = class_Use.id;
                    Class_MyInfo.UseNameValue = class_Use.nickName;
                    Class_MyInfo.TokenNameValue = class_Use.token;
                    Class_MyInfo.TokenEffectiveDateTime = Convert.ToDateTime(class_Use.tokenEffective);
                    Class_MyInfo.UseTypeValue = class_Use.useType;
                    Class_MyInfo.CompanyName = class_Use.companyName;

                    #region 记住登录信息
                    class_SQLiteOperator.UpdateLogInfo(this.textEdit1.Text
                        , this.textEdit2.Text.Length == 0 ? null : this.textEdit2.Text
                        , this.checkEdit1.Checked
                        , this.checkEdit2.Checked);
                    #endregion
                    this.DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show(string.Format("登录失败\r\n原因：{0}", resultVO.msg)
                        , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("登录失败\r\n原因：{0}", e.Message)
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.textEdit1.Text.Length == 0)
            {
                MessageBox.Show("请输入登录账号、手机号或邮件！"
                , "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (this.textEdit2.Text.Length == 0)
            {
                MessageBox.Show("请输入登录密码！"
                , "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.timer1.Enabled = false;
            this.timer2.Enabled = false;
            LogOk();
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.textEdit2.Focus();
            }
        }

        private void textEdit2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (this.textEdit1.Text.Length == 0)
                {
                    MessageBox.Show("请输入登录账号、手机号或邮件！"
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (this.textEdit2.Text.Length == 0)
                {
                    MessageBox.Show("请输入登录密码！"
                    , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.timer1.Enabled = false;
                this.timer2.Enabled = false;
                LogOk();
            }
        }

        private void Form_Log_Load(object sender, EventArgs e)
        {
            Class_LogRemamber class_LogRemamber = new Class_LogRemamber();
            class_LogRemamber = class_SQLiteOperator.GetLogRemember();
            if (class_LogRemamber != null)
            {
                this.textEdit1.Text = class_LogRemamber.UseName;
                if (class_LogRemamber.PassWord != null)
                    this.textEdit2.Text = class_LogRemamber.PassWord;
                this.checkEdit1.Checked = class_LogRemamber.RememberSign;
                this.checkEdit2.Checked = class_LogRemamber.AutoLog;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.simpleButton1.Text = string.Format("安全登录({0})", LogCount);
            if (LogCount == 1)
            {
                this.timer1.Enabled = false;
                this.simpleButton1.Text = "安全登录";
                LogOk();
            }
            else
                LogCount--;
        }

        private void Form_Log_Shown(object sender, EventArgs e)
        {
            this.timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.timer2.Enabled = false;
            bool AutoSign = this.textEdit1.Text.Length > 0 ? true : false;
            AutoSign = AutoSign && (this.textEdit2.Text.Length > 0 ? true : false);
            AutoSign = AutoSign && this.checkEdit2.Checked;
            this.timer1.Enabled = AutoSign;
        }
    }
}
