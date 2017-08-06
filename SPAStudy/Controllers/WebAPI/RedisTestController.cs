using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using SPAStudy.Common;

namespace SPAStudy.Controllers
{
    [RoutePrefix("api/RedisTest")]
    public class RedisTestController : ApiController
    {
        /// <summary>
        /// 根据Key获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public string StringGet(string key)
        {
            return RedisHelper.StringGet(key);
        }

        /// <summary>
        /// 单条存值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        public bool StringSet(string key, string value)
        {
            return RedisHelper.StringSet(key, value);
        }
    }
}
