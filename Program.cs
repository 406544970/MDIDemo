using MDIDemo;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DevExpress.XtraBars.Demos.MDIDemo
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form_Log form_Log = new Form_Log();
            if (form_Log.ShowDialog() == DialogResult.OK)
            {
                Form_DownLoad form_DownLoad = new Form_DownLoad();
                if (form_DownLoad.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new frmMain());
                }
                form_DownLoad.Dispose();
            }
            else
            {
                Application.Exit();
            }
            form_Log.Dispose();

        }

    }
}
