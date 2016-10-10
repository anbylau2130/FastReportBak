using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace USP.Utility
{
    public class Util
    {
        private static string today = GetNowDateString();
        private static UInt64 sequence = 0;
        private static Mutex mutex = new Mutex();


        public static string GetPassword(string name, string password)
        {
            StringBuilder temp = new StringBuilder("");
            temp.Append("{");
            temp.Append(name);
            temp.Append("}");
            temp.Append("{");
            temp.Append(password);
            temp.Append("}");
            return MD5(temp.ToString()).ToUpper();
        }
        public static string MD5_16(string src)
        {
            StringBuilder result = new StringBuilder("");
            result.Clear();
            MD5CryptoServiceProvider md = new MD5CryptoServiceProvider();
            byte[] value, hash;
            value = System.Text.Encoding.UTF8.GetBytes(src);
            hash = md.ComputeHash(value);
            md.Clear();
            for (int i = 4; i < 12; i++) //for (int i = 8; i < 16; i++)  modify by tangbaogui (old md32_16 error)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString().ToLower();
        }

        public static string MD5(string src)
        {
            StringBuilder result = new StringBuilder("");
            result.Clear();
            MD5CryptoServiceProvider md = new MD5CryptoServiceProvider();
            byte[] value, hash;
            value = System.Text.Encoding.UTF8.GetBytes(src);
            hash = md.ComputeHash(value);
            md.Clear();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            string hh = result.ToString().ToLower();
            return result.ToString().ToLower();
        }

        public static string MD5(byte[] src)
        {
            StringBuilder result = new StringBuilder("");
            result.Clear();
            MD5CryptoServiceProvider md = new MD5CryptoServiceProvider();
            byte[] value, hash;
            value = src;
            hash = md.ComputeHash(value);
            md.Clear();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            string hh = result.ToString().ToLower();
            return result.ToString().ToLower();
        }

        public static string GetNowDatetimeStamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        public static string GetNowTimeStamp()
        {
            return DateTime.Now.ToString("HHmmssffff");
        }

        public static string GetNowDateString()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }

        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public static string GetSequence()
        {
            string result;
            mutex.WaitOne();
            if (GetNowDateString() != today)
            {
                today = GetNowDateString();
                sequence = 0;
            }
            if (sequence == UInt64.MaxValue)
            {
                sequence = 0;
            }
            sequence = ++sequence;
            result = GetNowDatetimeStamp() + "-" + sequence.ToString();
            mutex.ReleaseMutex();
            return result;
        }
        public static string GetNowDatetimeStampGuid()
        {
            return DateTime.Now.ToString("MMHHddffffyyyyssmm");
        }



        public static string GetSkinPath()
        {
            return "";
        }


        public static string GetOemSkinPath()
        {
            return "";
        }




        public static string GetOemLoginSkinPath()
        {
            return "";
        }



        public static string GetOEMLogoutPath()
        {
            return "";
        }        /// <summary>
        /// 设置以缓存依赖的方式缓存数据
        /// </summary>
        /// <param name="CacheKey">索引键值</param>
        /// <param name="objObject">缓存对象</param>
        /// <param name="cacheDepen">依赖对象</param>
        public static void SetCache(string CacheKey, object objObject, System.Web.Caching.CacheDependency dep)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(
                CacheKey,
                objObject,
                dep,
                System.Web.Caching.Cache.NoAbsoluteExpiration, //从不过期
                System.Web.Caching.Cache.NoSlidingExpiration, //禁用可调过期
                System.Web.Caching.CacheItemPriority.Default,
                null);
        }

        public static string CreateRandomNum(int randomNumLength, bool hyphen, int splitLen, bool onlyNumber)
        {
            string result = "";
                #region 生成随机号
            for (int i = 0; i < randomNumLength; i++)
            {
                Random myRandom1 = new Random((int)DateTime.Now.Ticks);
                System.Threading.Thread.Sleep(1);
                int kinds = 0;
                if (onlyNumber)
                {
                    kinds = 0;
                }
                else
                {
                    kinds = myRandom1.Next(0, 3);
                }
                switch (kinds)
                {
                    case 0://数字类
                        #region 数字
                        Random myRandom11 = new Random((int)DateTime.Now.Ticks);
                        System.Threading.Thread.Sleep(1);
                        int number11 = myRandom11.Next(0, 9);
                        result += number11.ToString();
                        continue;
                        #endregion
                    case 1://大写字母类
                        #region 大写字母类
                        Random myRandom22 = new Random((int)DateTime.Now.Ticks);
                        System.Threading.Thread.Sleep(1);
                        int number22 = myRandom22.Next(65, 90);
                        result += Convert.ToChar(number22).ToString();
                        continue;
                        #endregion
                    case 2://小写字母类
                        #region 小写字母类
                        Random myRandom33 = new Random((int)DateTime.Now.Ticks);
                        System.Threading.Thread.Sleep(1);
                        int number33 = myRandom33.Next(97, 122);
                        result += Convert.ToChar(number33).ToString();
                        continue;
                        #endregion
                    default: break;
                }
            }
            #endregion
            #region 加"-"
            if (hyphen)
            {
                string newstring = "";
                for (int i = 1; i <= result.Length; i++)
                {
                    newstring += result[i - 1];
                    if (i % splitLen == 0 && i != result.Length)
                        newstring += "-";
                }
                result = newstring;
            }
            #endregion
            return result.ToUpper();
        }

        public static string GetAreaName(string fullName)
        {
            Regex areaRegex = new Regex("Areas.(\\w*).Controllers", RegexOptions.IgnoreCase);
            Match match = areaRegex.Match(fullName);
            if (match.Success)
            {
                return match.Groups[1].ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
    }
}