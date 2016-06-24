using System.Web;

namespace Richnova.CEMS.Framework.Web.Request
{
    /// <summary>
    /// WEB请求上下文信息工具类
    /// </summary>
    public partial class RequestHelper
    {
        #region 获取当前站点的Application实例
        /// <summary>
        /// 获取当前站点的Application实例
        /// </summary>
        public static HttpApplicationState Application
        {
            get
            {
                return HttpContext.Current.Application;
            }
        }
        #endregion

        #region 取得页面返回的信息
        public static string GetUrlData(string url)
        {
            url = RootFullPath + url.Trim('/');
            var urlData = "";
            try
            {
                var request = System.Net.WebRequest.Create(url);
                var response = request.GetResponse();
                var resStream = response.GetResponseStream();
                if (resStream != null)
                {
                    var sr = new System.IO.StreamReader(resStream, System.Text.Encoding.Default);
                    urlData = sr.ReadToEnd();
                    resStream.Close();
                    sr.Close();
                }
            }
            catch {;}
            return urlData.Trim();
        }
        #endregion
    }
}

