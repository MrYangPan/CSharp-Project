using System.Collections.Generic;
using System.Net;
using System.Web;

namespace Richnova.CEMS.Framework.Web.Request
{
    /// <summary>
    /// WEB请求上下文信息工具类
    /// </summary>
    public partial class RequestHelper
    {
        #region 可以获取客户端上次请求的url的有关信息
        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string ReferrerUrl
        {
            get
            {
                return HttpContext.Current.Request.UrlReferrer == null ? string.Empty : HttpContext.Current.Request.UrlReferrer.OriginalString;
            }
        }
        #endregion

        #region 得到用户IP地址
        /// <summary>
        /// 得到用户IP地址
        /// </summary>
        /// <returns>返回用户IP地址,如果获取不到返回 0.0.0.0 </returns>
        /// 
        public static string ClientIP
        {
            get
            {
                var context = HttpContext.Current;
                string result = context.Request.UserHostAddress;
                if (string.IsNullOrEmpty(result))
                {
                    result = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];//获取包括使用了代理服务器的地址列表。
                }
                if (string.IsNullOrEmpty(result))
                {
                    result = context.Request.ServerVariables["REMOTE_ADDR"];//最后一个代理服务器地址。
                }
                if (string.IsNullOrEmpty(result))
                {
                    result = context.Request.UserHostAddress;
                }
                return result;
            }
        }
        #endregion

        #region 是否局域网IP
        public static bool IsLanIP(string IP)
        {
            var result = false;
            var ips = new List<string>() { "10.", "192.", "172.", "127.", "::1", "localhost" };
            ips.ForEach(x=>{
                if (IP.StartsWith(x)) result = true;
            });
            return result;
        }
        #endregion

        #region HostName
        public static string ClientHostName 
        {
            get 
            {
                var context = HttpContext.Current;
                string ip = context.Request.UserHostAddress;
                string name = context.Request.UserHostName;

                if (name == ip || name == "127.0.0.1" || name == "::1")
                {
                    name = context.Request.ServerVariables["REMOTE_HOST"];
                }
                if (name == ip || name == "127.0.0.1" || name == "::1")
                {
                    try { name = Dns.GetHostEntry(ip).HostName; }
                    catch {;}
                }

                return name;
            }
        }
        #endregion

        #region 判断是否来自搜索引擎链接
        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns>是否来自搜索引擎链接</returns>
        public static bool IsFromSearchEngines
        {
            get
            {
                string[] searchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };
                if (HttpContext.Current.Request.UrlReferrer != null)
                {
                    string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
                    for (int i = 0; i < searchEngine.Length; i++)
                    {
                        if (tmpReferrer.Contains(searchEngine[i]))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        #endregion

        #region 取得客户端的操作系统
        public static string GetPlatformName(HttpRequestBase request)
        {
            string userAgent = request.UserAgent;

            if (string.IsNullOrEmpty(userAgent))
                return "未知类型";
            else if (userAgent.IndexOf("Windows NT 6.2", System.StringComparison.Ordinal) != -1)
                return "Windows 8";
            else if (userAgent.IndexOf("Windows NT 6.1", System.StringComparison.Ordinal) != -1)
                return "Windows 7";
            else if (userAgent.IndexOf("Windows NT 6", System.StringComparison.Ordinal) != -1)
                return "Windows Vista";
            else if (userAgent.IndexOf("Windows NT 5.1", System.StringComparison.Ordinal) != -1)
                return "Windows XP";
            else if (userAgent.IndexOf("Windows NT 5.2", System.StringComparison.Ordinal) != -1)
                return "Windows Server 2003";
            else if (userAgent.IndexOf("Windows NT 5", System.StringComparison.Ordinal) != -1)
                return "Windows 2000";
            else if (userAgent.IndexOf("iPhone", System.StringComparison.Ordinal) != -1)
                return "iPhone";
            else if (userAgent.IndexOf("(iPad;", System.StringComparison.Ordinal) != -1)
                return "iPad";
            else if (userAgent.IndexOf("Android", System.StringComparison.Ordinal) != -1)
                return "Android";
            else if (userAgent.IndexOf("9x", System.StringComparison.Ordinal) != -1)
                return "Windows ME";
            else if (userAgent.IndexOf("98", System.StringComparison.Ordinal) != -1)
                return "Windows 98";
            else if (userAgent.IndexOf("95", System.StringComparison.Ordinal) != -1)
                return "Windows 95";
            else if (userAgent.IndexOf("NT 4", System.StringComparison.Ordinal) != -1)
                return "Windows NT 4";
            if (request.Browser != null && !string.IsNullOrEmpty(request.Browser.Platform))
                return request.Browser.Platform.Replace("WinCE", "Windows CE");
            else
                return "未知类型";
        }
        #endregion
    }
}

