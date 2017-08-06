using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garden.Frame.Cache
{
    public interface ICache
    {
        /// <summary>
        /// 根据Key获取值
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>System.String.</returns>
        string StringGet(string key);

        /// <summary>
        /// 批量获取值
        /// </summary>
        /// <param name="keyStrs"></param>
        /// <returns>System.String[]</returns>
        string[] StringGetMany(string[] keyStrs);

        /// <summary>
        /// 单条存值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool StringSet(string key, string value);

        /// <summary>
        /// 批量存值
        /// </summary>
        /// <param name="keysStr">key</param>
        /// <param name="valuesStr">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool StringSetMany(string[] keysStr, string[] valuesStr);

        /// <summary>
        /// 存值并设置过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="t">实体</param>
        /// <param name="ts">过期时间间隔</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool Set<T>(string key, T t, TimeSpan ts);

        /// <summary>
        /// 根据Key获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        T Get<T>(string key) where T : class;
    }
}
