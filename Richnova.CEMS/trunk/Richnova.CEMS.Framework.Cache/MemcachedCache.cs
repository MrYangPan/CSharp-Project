using System;
using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace Richnova.CEMS.Framework.Cache
{
    /// <summary>
    /// 分布式Memcached缓存
    /// </summary>
    class MemcachedCache : ICacheStorage
    {
        /// <summary>
        /// 分布式缓存客户端
        /// </summary>
        internal MemcachedClient Cache;

        public MemcachedCache()
        {
            Cache = new MemcachedClient();

            //if(Cache.Get("Keys") == null)
            //    Cache.Store(StoreMode.Add, "Keys", new List<string>());

            //if (Cache.Get("KeyCount") == null)
            //    Cache.Store(StoreMode.Add, "KeyCount", 0);
        }
        
        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Insert(string key, object value)
        {
            Cache.Store(StoreMode.Set, key, value);
        }
        
        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiration">绝对过期时间</param>
        public void Insert(string key, object value, DateTime expiration)
        {
            Cache.Store(StoreMode.Set, key, value, expiration);
        }
        
        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiration">过期时间</param>
        public void Insert(string key, object value, TimeSpan expiration)
        {
            Cache.Store(StoreMode.Set, key, value, expiration);
        }

        /// <summary>
        /// 根据key获取value
        /// </summary>
        public object Get(string key)
        {
            return Cache.Get(key);
        }

        /// <summary>
        /// 根据key获取value
        /// </summary>
        public T Get<T>(string key)
        {
            return Cache.Get<T>(key);
        }

        /// <summary>
        /// 删除key的缓存的值
        /// </summary>
        /// <param name="key">key</param>
        public void Remove(string key)
        {
            if (Exist(key))
                Cache.Remove(key);
        }

        /// <summary>
        /// 检验key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exist(string key)
        {
            return Cache.Get(key) != null;
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Flush()
        {
            Cache.FlushAll();
        }
    }
}
