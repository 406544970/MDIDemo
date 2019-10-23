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
            const string _Version = "1.13";
            Form_Log form_Log = new Form_Log(_Version);
            if (form_Log.ShowDialog() == DialogResult.OK)
            {
                Form_DownLoad form_DownLoad = new Form_DownLoad();
                if (form_DownLoad.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new frmMain(_Version));
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
