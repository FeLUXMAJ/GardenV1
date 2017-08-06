using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Data;

using Newtonsoft.Json;


using SPAStudy.Common;
using SPAStudy.Domain.Context;
using SPAStudy.Domain.Entities;

namespace SPAStudy.Controllers
{
    [RoutePrefix("api/MySQLTest")]
    public class MySQLTestController : ApiController
    {
        /// <summary>
        /// 通过EF对MySql进行操作
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public bool InitData()
        //{
        //    try
        //    {
        //        //Database.SetInitializer(
        //        //    new DropCreateDatabaseIfModelChanges<BloggingContext>());
        //        var context = new BloggingContext();
        //        //插入一行值
        //        context.Blogs.Add(new Blog { BlogId = 1, Name = "hhhh" });
        //        context.SaveChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        LogHelper.WriteLog(e.ToString());
        //        return false;
        //    }
        //    return true;
        //}

        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="nBlogId"></param>
        /// <param name="sName"></param>
        /// <returns></returns>
        [HttpGet]
        public bool WriteData(int nBlogId, string sName)
        {
            try
            {
                var context = new BloggingContext();
                context.Blogs.Add(new Blog { BlogId = nBlogId, Name = sName });
                context.SaveChanges();
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e.ToString());
                return false;
            }
            return true;
        }

        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="nBlogID"></param>
        /// <returns></returns>
        [HttpGet]
        public string ReadData(int nBlogID)
        {
            string reString = string.Empty;
            try
            {
                var context = new BloggingContext();
                reString = JsonConvert.SerializeObject(
                    context.Blogs.FirstOrDefault().Name);
                //.Select(e => e.BlogId == nBlogID).FirstOrDefault())
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e.ToString());
                return string.Empty;
            }

            return reString;
        }

        /// <summary>
        /// 通过MySqlHelper类对Mysql进行操作
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public string test()
        //{
        //    MySqlHelper mh = new MySqlHelper(ConfigurationManager.ConnectionStrings["MyContext"].ToString());
        //    DataSet ds = mh.ExecuteDataSet("select * from user");

        //    return ds.Tables[0].Rows[0]["Name"].ToString();
        //}
    }
}
