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
    public partial class Form_DownLoad : Form
    {
        public Form_DownLoad()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            //class_DbVou.c1Label = this.c1Label1;
            //class_DbVou.inputProgressBar = this.inputProgressBar1;
            //if (class_DbVou.IsDown())//以后加入比较版本
            //{
            //    if (class_DbVou.SqlServerToSqlite())
            //    {
            //        c1Label1.Value = "同步完成!";
            //    }
            //    else
            //    {
            //        c1Label1.Value = "同步失败!";
            //    }
            //}
            //else
            //{
            //    this.inputProgressBar1.Value = 100;
            //    this.c1Label1.Value = "同步完成!";
            //    Application.DoEvents();
            //    Thread.Sleep(500);
            //}
            this.DialogResult = DialogResult.OK;

        }

        private void Form_DownLoad_Load(object sender, EventArgs e)
        {
            //class_DbVou = new Class_DbVou();
            //this.c1Label2.Value = class_DbVou.GetClientWinDesgin();
            this.timer1.Enabled = true;
        }
    }
}
