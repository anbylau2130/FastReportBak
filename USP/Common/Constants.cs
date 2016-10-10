using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace USP.Common
{

    //<add key = "DBLinkServer" value="10.1.1.50"/>
    //<add key = "DBDataBase" value="20160418"/>
    //<add key = "DBUID" value="XXB"/>
    //<add key = "DBPWD" value="xinxibu"/>
    public class Constants
    {
        public const string CAPTCHA = "CAPTCHA";
        public const string USER_KEY = "USER_KEY";
        public const string USER_MENU = "USER_MENU";
        public const string USER_PRIVILEGE = "USER_PRIVILEGE";
        public const string ACTION_DURATION = "ACTION_DURATION";
        public static string DB_Server = System.Configuration.ConfigurationManager.AppSettings["DBLinkServer"].ToString();
        public static string DB_DataBase = System.Configuration.ConfigurationManager.AppSettings["DBDataBase"].ToString();
        public static string DB_UID = System.Configuration.ConfigurationManager.AppSettings["DBUID"].ToString();
        public static string DB_PWD = System.Configuration.ConfigurationManager.AppSettings["DBPWD"].ToString();

        public  static  List<string> Skins=new List< string>()
        {
          " ~/Static/Css/themes/bootstrap/easyui.css",
           " ~/Static/Css/themes/black/easyui.css",
            " ~/Static/Css/themes/default/easyui.css",
           " ~/Static/Css/themes/gray/easyui.css",
            " ~/Static/Css/themes/metro/easyui.css"
        }; 


    }

    public class Common
    {
        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetHostAddress()
        {
            string userHostAddress = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public static string SkinRender()
        {
            var currentUser = HttpContext.Current.Session[Constants.USER_KEY] as USP.Models.POCO.User;
            if (currentUser.SysOperator.Skin != 0)
            {
                return Constants.Skins[(int) currentUser.SysOperator.Skin];
            }
            return Constants.Skins[0];
        }
    }
}