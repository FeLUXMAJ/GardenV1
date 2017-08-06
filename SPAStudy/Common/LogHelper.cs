using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using log4net;

namespace SPAStudy.Common
{
    public class LogHelper
    {
        //public static readonly ILog loginfo = LogManager.GetLogger("loginfo");

        //public static readonly ILog logerror = LogManager.GetLogger("logerror");

        public static void WriteLog(string info)
        {
            //logger.Info("消息");
            //logger.Warn("警告");
            //logger.Fatal("错误");

            ILog loginfo = LogManager.GetLogger("loginfo");
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }

        public static void WriteLog(string info, Exception se)
        {
            ILog logerror = LogManager.GetLogger("logerror");
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info, se);
            }
        }
    }
}