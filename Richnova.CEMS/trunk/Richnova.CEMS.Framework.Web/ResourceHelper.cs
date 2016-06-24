using System;
using System.Web;

namespace Richnova.CEMS.Framework.Web
{
    public static class ResourceHelper
    {
        public static string GetMessage(string messageCode)
		{
            string fieldName="";
            if (messageCode == null || messageCode.Trim().Equals("")){return "";}
            try
            {
                var globalResourceObject = HttpContext.GetGlobalResourceObject("Message", messageCode);
                if (globalResourceObject != null)
                    fieldName = globalResourceObject.ToString();
            }
            catch{fieldName = messageCode;}
            return fieldName;
		}

        public static string GetMessage(string messageCode,params object[] parms)
        {
            string msg = GetMessage(messageCode);
            if (parms != null) msg = String.Format(msg, parms);
            return msg;
        }

        public static string GetResource(string resource,string field)
        {
            string fieldName = "";
            try
            {
                var globalResourceObject = HttpContext.GetGlobalResourceObject(resource, field);
                if (globalResourceObject != null)
                    fieldName = globalResourceObject.ToString();
            }
            catch {fieldName=field; }
            return fieldName;
        }
    }
}
