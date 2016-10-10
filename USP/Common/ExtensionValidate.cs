using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace USP.Common
{
    public static class ExtensionValidate
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 验证IP地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIP(this string str)
        {
            return !string.IsNullOrEmpty(str)
                && Regex.IsMatch(str, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证电话号 如:010-29292929
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsTelephone(this string str)
        {
            return !string.IsNullOrEmpty(str) && Regex.IsMatch(str, @"^\d{3,4}-?\d{6,8}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证手机号 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(this string str)
        {
            return !string.IsNullOrEmpty(str) && Regex.IsMatch(str, @"^1[35]\d{9}$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 验证邮箱地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(this string str)
        {
            return !string.IsNullOrEmpty(str) && Regex.IsMatch(str, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", RegexOptions.IgnoreCase); ;
        }

        /// <summary>
        /// 验证日期数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDate(this string str)
        {
            try
            {
                DateTime time = Convert.ToDateTime(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 验证是否是中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsChinese(this string str)
        {
            return !string.IsNullOrEmpty(str) && Regex.IsMatch(str, @"^[\u4e00-\u9fa5]+$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证URL
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsUrl(this string str)
        {
            return string.IsNullOrEmpty(str) && Regex.IsMatch(str, @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?$", RegexOptions.IgnoreCase);
        }


        /// <summary>
        /// 验证邮编
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsZipCode(this string str)
        {
            return !string.IsNullOrEmpty(str) && Regex.IsMatch(str, @"^\d{6}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证QQ
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsQQ(this string str)
        {
            return !string.IsNullOrEmpty(str) && Regex.IsMatch(str, "^[1-9]\\d{4,9}$");
        }

        /// <summary>
        /// 验证纯字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLetter(this string str)
        {
            return !string.IsNullOrEmpty(str) && Regex.IsMatch(str, "^[a-zA-Z]+$");
        }



        /// <summary>
        /// 验证字符串长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool IsLength(this string str, int begin, int end)
        {
            int length = Regex.Replace(str, @"[^\x00-\xff]", "OK").Length;
            if ((length <= begin) && (length >= end))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 是不是Int型的
        /// </summary>
        /// <returns></returns>
        public static bool IsInt(string str)
        {
            Regex regex = new Regex(@"^(-){0,1}\d+$");
            if (regex.Match(str).Success)
            {
                if ((long.Parse(str) > 0x7fffffffL) || (long.Parse(str) < -2147483648L))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
