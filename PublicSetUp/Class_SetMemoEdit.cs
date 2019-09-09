using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicSetUp
{
    public class Class_SetMemoEdit
    {
        public void SetMemoEdit(MemoEdit memoEdit, bool ReadOnly)
        {
            _SetMemoEdit(memoEdit, ReadOnly);
        }
        public void SetMemoEdit(MemoEdit memoEdit)
        {
            _SetMemoEdit(memoEdit, false);
        }
        private void _SetMemoEdit(MemoEdit memoEdit,bool ReadOnly)
        {
            memoEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            memoEdit.Properties.Appearance.Options.UseFont = true;
            memoEdit.Properties.ReadOnly = ReadOnly;
        }
    }
}
