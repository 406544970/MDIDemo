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
            #region 从SQLITE读取数据
            Class_SQLiteOperator class_SQLiteOperator = new Class_SQLiteOperator();
            List<string> vs = new List<string>();
            vs = class_SQLiteOperator.GetLocalPageList();
            if (vs != null)
            {
                foreach (string item in vs)
                {
                    string[] row = item.Split(';');
                    PageVersionInParam pageVersionInParam = new PageVersionInParam()
                    {
                        pageKey = row[0],
                        pageVersion = Convert.ToInt32(row[1])
                    };
                    pageKey.Add(pageVersionInParam);
            }
        }
            vs.Clear();
            
            #endregion
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
                    this.progressBarControl1.Position = 0;
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

                    this.progressBarControl1.Position = index++;
                    Thread.Sleep(0);
                    Application.DoEvents();
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
