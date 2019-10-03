using MDIDemo.Model;
using MDIDemo.PublicClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            List<PageVersionInParam> pageKey = new List<PageVersionInParam>();
            PageVersionInParam pageVersionInParam = new PageVersionInParam()
            {
                pageKey = "SE20190921083425696",
                pageVersion = 23
            };
            pageKey.Add(pageVersionInParam);
            PageVersionListInParam pageVersionListInParam = new PageVersionListInParam();
            pageVersionListInParam.pageKey = pageKey;
            Class_Remote class_Remote = new Class_Remote();
            ResultVO<List<PageModel>> resultVO = new ResultVO<List<PageModel>>();
            resultVO = class_Remote.SelectVersionList<List<PageModel>>(pageVersionListInParam);
            if (resultVO.code == 0)
            {
                if (resultVO.count > 0)
                {
                    this.progressBarControl1.Properties.Maximum = Convert.ToInt32(resultVO.count);
                    this.progressBarControl1.Properties.Maximum = 0;
                    this.progressBarControl1.Properties.Step = 1;
                    this.progressBarControl1.Text = "0";
                }
                List<PageModel> pageModels = new List<PageModel>();
                pageModels = resultVO.data;
                int index = 1;
                foreach (PageModel pageModel in pageModels)
                {
                    switch (pageModel.operateType)
                    {
                        case -1:
                            break;
                        case 0:
                            break;
                        case 1:
                            break;
                        default:
                            break;
                    }

                    this.progressBarControl1.Text = index.ToString();
                    index++;
                    Application.DoEvents();
                    Thread.Sleep(500);
                }

                this.DialogResult = DialogResult.OK;
            }


        }

        private void Form_DownLoad_Load(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }
    }
}
