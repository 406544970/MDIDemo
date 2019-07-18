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
        private void _SetButtonEdit(ButtonEdit buttonEdit,bool ReadOnly)
        {
            buttonEdit.Properties.ReadOnly = ReadOnly;
        }
    }
}
