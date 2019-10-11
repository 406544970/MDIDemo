using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicSetUp
{
    public class Class_SetButtonEdit
    {
        public void SetButtonEdit(ButtonEdit buttonEdit)
        {
            _SetButtonEdit(buttonEdit, true);
        }
        public void SetButtonEdit(ButtonEdit buttonEdit, bool DisableTextEditor)
        {
            _SetButtonEdit(buttonEdit, DisableTextEditor);
        }
        private void _SetButtonEdit(ButtonEdit buttonEdit,bool DisableTextEditor)
        {
            buttonEdit.Properties.TextEditStyle = DisableTextEditor ? DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor : DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        }
    }
}
