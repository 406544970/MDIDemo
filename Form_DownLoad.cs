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
            Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
            if (class_PublicMethod.GetVersionUpdateInfo(this.progressBarControl1))
                this.DialogResult = DialogResult.OK;
        }

        private void Form_DownLoad_Load(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }
    }
}
