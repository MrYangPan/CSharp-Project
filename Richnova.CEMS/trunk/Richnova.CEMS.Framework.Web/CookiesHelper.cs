using System;
using System.Collections.Specialized;
using System.Web;
using Richnova.CEMS.Framework.Utility.Encrypt;

namespace Richnova.CEMS.Framework.Web
{
    /// <summary>
    /// <para>　</para>
    /// 　常用工具类——COOKIES操作类
    /// <para>　-------------------------------------------------------------------</para>
    /// <para>　WriteCookie：创建COOKIE对象并赋Value值或值集合 [+4重载]</para>
    /// <para>　GetCookie：读取Cookie某个对象的Value值，返回Value值，如果对象本就不存在，则返回null</para>
    /// <para>　DelCookie：删除COOKIE对象</para>
    /// </summary>
    public class CookiesHelper
    {

        #region 创建COOKIE对象并赋Value值

        public static void Add(HttpCookie cookie)
        {
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        
        /// <summary>
        /// 创建COOKIE对象并赋Value值
        /// </summary>
        /// <param name="cookiesName">COOKIE对象名</param>
        /// <param name="expires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>  
        /// <param name="cookiesValue">COOKIE对象Value值</param>
        public static void Add(string cookiesName, int expires, string cookiesValue)
        {
            var objCookie = new HttpCookie(cookiesName.Trim())
            {
                Value = EncryptHelper.DESEncrypt(cookiesValue.Trim())
            };
            if (expires > 0)
            {
                objCookie.Expires = expires == 1 ? DateTime.MaxValue : DateTime.Now.AddMinutes(expires);
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /// <summary>
        /// 创建COOKIE对象并赋Value值
        /// </summary>
        /// <param name="cookiesName">COOKIE对象名</param>
        /// <param name="expires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>  
        /// <param name="cookiesValue">COOKIE对象Value值</param>
        /// <param name="cookiesDomain">作用域</param>
        public static void Add(string cookiesName, int expires, string cookiesValue, string cookiesDomain)
        {
            var objCookie = new HttpCookie(cookiesName.Trim())
            {
                Value = EncryptHelper.DESEncrypt(cookiesValue.Trim()),
                Domain = cookiesDomain.Trim()
            };
            if (expires > 0)
            {
                objCookie.Expires = expires == 1 ? DateTime.MaxValue : DateTime.Now.AddMinutes(expires);
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /// <summary>   
        /// 创建COOKIE对象并赋多个KEY键值   
        /// 设键/值如下：   
        /// NameValueCollection myCol = new NameValueCollection();   
        /// myCol.Add("red", "rojo");   
        /// myCol.Add("green", "verde");   
        /// myCol.Add("blue", "azul");   
        /// myCol.Add("red", "rouge");   结果“red:rojo,rouge；green:verde；blue:azul”   
        /// </summary>   
        /// <param name="cookiesName">COOKIE对象名</param>   
        /// <param name="expires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>   
        /// <param name="cookiesKeyValueCollection">键/值对集合</param> 
        public static void Add(string cookiesName, int expires, NameValueCollection cookiesKeyValueCollection)
        {
            var objCookie = new HttpCookie(cookiesName.Trim());
            foreach (String key in cookiesKeyValueCollection.AllKeys)
            {
                objCookie[key] = EncryptHelper.DESEncrypt(cookiesKeyValueCollection[key].Trim());
            }
            if (expires > 0)
            {
                objCookie.Expires = expires == 1 ? DateTime.MaxValue : DateTime.Now.AddSeconds(expires);
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /// <summary>   
        /// 创建COOKIE对象并赋多个KEY键值   
        /// 设键/值如下：   
        /// NameValueCollection myCol = new NameValueCollection();   
        /// myCol.Add("red", "rojo");   
        /// myCol.Add("green", "verde");   
        /// myCol.Add("blue", "azul");   
        /// myCol.Add("red", "rouge");   结果“red:rojo,rouge；green:verde；blue:azul”   
        /// </summary>   
        /// <param name="cookiesName">COOKIE对象名</param>   
        /// <param name="expires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)，</param>   
        /// <param name="cookiesKeyValueCollection">键/值对集合</param> 
        /// <param name="cookiesDomain">作用域</param>
        public static void Add(string cookiesName, int expires, NameValueCollection cookiesKeyValueCollection, string cookiesDomain)
        {
            var objCookie = new HttpCookie(cookiesName.Trim());
            foreach (var key in cookiesKeyValueCollection.AllKeys)
            {
                objCookie[key] = EncryptHelper.DESEncrypt(cookiesKeyValueCollection[key].Trim());
            }
            objCookie.Domain = cookiesDomain.Trim();
            if (expires > 0)
            {
                objCookie.Expires = expires == 1 ? DateTime.MaxValue : DateTime.Now.AddSeconds(expires);
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        #endregion

        #region 读取Cookie某个对象的Value值，返回Value值，如果对象本就不存在，则返回null
        /// <summary>
        /// 读取Cookie某个对象的Value值，返回Value值，如果对象本就不存在，则返回null
        /// </summary>
        /// <param name="cookiesName">Cookie对象名称</param>
        /// <returns>返回对象的Value值，返回Value值，如果对象本就不存在，则返回null</returns>
        public static string Get(string cookiesName)
        {
            return HttpContext.Current.Request.Cookies[cookiesName] == null
                ? null
                : EncryptHelper.DESDecrypt(HttpContext.Current.Request.Cookies[cookiesName].Value);
        }

        /// <summary>
        /// 读取Cookie某个对象的Value值，返回Value值，如果对象本就不存在，则返回null
        /// </summary>
        /// <param name="cookiesName">Cookie对象名称</param>
        /// <param name="keyName">键值</param>
        /// <returns>返回对象的Value值，返回Value值，如果对象本就不存在，则返回null</returns>
        public static string Get(string cookiesName, string keyName)
        {
            if (HttpContext.Current.Request.Cookies[cookiesName] == null) return null;

            var strObjValue = EncryptHelper.DESDecrypt(HttpContext.Current.Request.Cookies[cookiesName].Value);
            var strKeyName2 = keyName + "=";
            return strObjValue.IndexOf(strKeyName2, StringComparison.Ordinal) == -1
                ? null
                : EncryptHelper.DESDecrypt(HttpContext.Current.Request.Cookies[cookiesName][keyName]);
        }
        #endregion

        #region 删除COOKIE对象
        /// <summary>
        /// 删除COOKIE对象
        /// </summary>
        /// <param name="cookiesName">Cookie对象名称</param>
        public static void Remove(string cookiesName)
        {
            var objCookie = new HttpCookie(cookiesName.Trim()) {Expires = DateTime.Now.AddYears(-5)};
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
        #endregion
    }
}
