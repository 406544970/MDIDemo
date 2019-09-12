using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    public class Class_CreateInsertCode : IClass_InterFaceCreateCode, IClass_CreateFrontPage
    {
        public Class_CreateInsertCode()
        {
            InitClass(null);
        }
        public Class_CreateInsertCode(string xmlFileName)
        {
            InitClass(xmlFileName);
        }
        private void InitClass(string xmlFileName)
        {
            if (xmlFileName != null)
            {
                Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
                class_InsertAllModel = new Class_InsertAllModel();
                class_InsertAllModel = class_PublicMethod.FromXmlToSelectObject<Class_InsertAllModel>(xmlFileName);
            }
            class_SQLiteOperator = new Class_SQLiteOperator();
        }
        private Class_SQLiteOperator class_SQLiteOperator;
        private Class_InsertAllModel class_InsertAllModel;
        public void AddAllOutFieldName()
        {
            throw new NotImplementedException();
        }

        public string GetControl(int Index)
        {
            return "没做";
        }

        public string GetDAO(int Index)
        {
            return "没做";
        }

        public string GetDTO(int Index)
        {
            return "没做";
        }

        public string GetFrontPage()
        {
            return "没做";
        }

        public string GetInPutParam(int Index)
        {
            return "没做";
        }

        public string GetMap(int Index)
        {
            return null;
        }

        public string GetModel(int Index)
        {
            return "没做";
        }

        public string GetServiceImpl(int Index)
        {
            return "没做";
        }

        public string GetServiceInterFace(int Index)
        {
            return "没做";
        }

        public string GetSql(int Index)
        {
            return "没做";
        }

        public string GetTestSql(int Index)
        {
            return "没做";
        }

        public string GetUsedMethod()
        {
            return "没做";
        }

        public bool IsCheckOk(ref List<string> outMessage)
        {
            return true;
        }

        public List<string> GetComponentType()
        {
            return class_SQLiteOperator.GetComponentList();
        }
    }
}
