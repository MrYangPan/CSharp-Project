using System;
using System.Configuration;

namespace Richnova.CEMS.Framework.Cache
{

    /// <summary>
    /// 缓存工厂
    /// </summary>
    public class CacheFactory
    {
        /// <summary>
        /// 缓存类别
        /// </summary>
        public enum CacheType
        {
            /// <summary>
            /// 默认缓存
            /// </summary>
            DefaultCache = 0,

            /// <summary>
            /// 分布式Memcached缓存
            /// </summary>
            MemcachedCache = 1
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static ICacheStorage CreateCacheFactory()
        {
            var cache = ConfigurationManager.AppSettings["CacheType"];
            if (CacheType.DefaultCache.ToString() == cache)
            {
                return new DefaultCache();
            }
            if (CacheType.MemcachedCache.ToString() == cache)
            {
                return new MemcachedCache();
            }
            throw new NotImplementedException();
        }
    }
}
