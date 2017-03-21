using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ad_Tools.Log4net
{
    public class LogHelper
    {
        public static void WriteLog(Type t, string username,string operation,bool status)                     //username  记录的用户名   operation  操作  
        {
            string msg;
            if (status)    //成功
            {
                 msg = username + "*" + operation+"*Succeed";
            }
            else
            {
                 msg = username + "*" + operation +"*fail";
            }
           
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void WriteLog(Type t, Exception ex)
        public static void WriteLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
            
        }
        #endregion
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void WriteLog(Type t, string msg)
        public static void WriteLog(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }
        #endregion
    }
}