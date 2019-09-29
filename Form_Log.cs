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
        }

        private void LogOk()
        {
            Class_RestClient class_RestClient = new Class_RestClient("localhost:2510");
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
                string ResultValue = class_RestClient.Post("myBatisUseController/useLogCS", class_ParaArrays);
                ResultVO<Class_Use> resultVO = new ResultVO<Class_Use>();
                resultVO = JsonTools.JsonToObject(ResultValue, resultVO) as ResultVO<Class_Use>;
                if (resultVO.code == 0)
                {
                    Class_Use class_Use = new Class_Use();
                    class_Use = resultVO.data;
                    Class_MyInfo.UseIdValue = class_Use.id;
                    Class_MyInfo.UseNameValue = class_Use.nickName;
                    Class_MyInfo.TokenNameValue = class_Use.token;
                    Class_MyInfo.DateTimeString = class_Use.tokenEffective;
                    Class_MyInfo.UseTypeValue = class_Use.useType;
                    this.DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show(string.Format("登录失败:\r\n原因：{0}",resultVO.msg)
                        , "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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
                LogOk();
            }
        }
    }
}
