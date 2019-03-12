using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDIDemo.vou
{
    public partial class Form_test : DevExpress.XtraEditors.XtraForm
    {
        public Form_test()
        {
            InitializeComponent();
            setIniSkin("Metropolis Dark");
        }
        public Form_test(string skinName)
        {
            InitializeComponent();
            setIniSkin(skinName);
        }
        public string publicSkinName;
        private void setIniSkin(string skinName)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
        }

    }
}
