using MDIDemo.Model;
using MDIDemo.PublicClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static MDIDemo.PublicClass.PageVersionListInParam;

namespace MDIDemo
{
    public partial class Form_DownLoad : Form
    {
        public Form_DownLoad()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            string FileName = @"SE20191005112039188E9FB614F1B36.xml";
            string AllPathFileName = string.Format(@"D:\MDIDemo\bin\Debug\select\{0}", FileName);
            string FolderName = "select";
            if (File.Exists(AllPathFileName))
            {
                Class_Remote class_Remote = new Class_Remote();
                bool Upload = class_Remote.upLoadFileBinary<bool>(AllPathFileName, FolderName, FileName).data;
                if (Upload)
                {
                    Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
                    if (class_PublicMethod.GetVersionUpdateInfo(this.progressBarControl1))
                        this.DialogResult = DialogResult.OK;
                }
                else
                {
                    Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
                    if (class_PublicMethod.GetVersionUpdateInfo(this.progressBarControl1))
                        this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void Form_DownLoad_Load(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }
    }
}
