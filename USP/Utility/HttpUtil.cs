using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace USP.Utility
{
    public class HttpUtil
    {
        public static string HttpRequestByGet(string Url, CookieContainer cookieContainer)
        {
            HttpWebRequest webRequest = null;
            WebResponse webResponse = null;
            StreamReader sr = null;
            string response = "";
            try
            {
                webRequest = (HttpWebRequest)WebRequest.Create(Url);
                if (cookieContainer != null)
                {
                    webRequest.CookieContainer = cookieContainer;
                }
                webResponse = webRequest.GetResponse();
                sr = new StreamReader(webResponse.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                response = sr.ReadToEnd();
                sr.Close();
                sr = null;
            }
            catch { }
            finally
            {
                if (webResponse != null)
                {
                    webResponse.Close(); ;
                }
                if (sr != null)
                {
                    sr.Close();
                    sr = null;
                }
            }
            return response;
        }

        public static string HttpRequestByPost(string Url, string data, CookieContainer cookieContainer)
        {
            HttpWebRequest webRequest = null;
            WebResponse webResponse = null;
            StreamReader sr = null;
            Stream webWriter = null;
            string response = "";
            StringBuilder stringData = new StringBuilder();
            byte[] postData;
            try
            {
                postData = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(stringData.ToString());
                webRequest = (HttpWebRequest)WebRequest.Create(Url);
                webRequest.CookieContainer = cookieContainer;
                webRequest.AllowAutoRedirect = false;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";//内容类型
                webWriter = webRequest.GetRequestStream();
                webWriter.Write(postData, 0, postData.Length);
                webWriter.Close();
                webWriter = null;
                webResponse = webRequest.GetResponse();
                sr = new StreamReader(webResponse.GetResponseStream(), Encoding.GetEncoding(data));
                response = sr.ReadToEnd();
                sr.Close();
                sr = null;
            }
            catch { }
            finally
            {
                if (webResponse != null)
                {
                    webResponse.Close(); ;
                }
                if (sr != null)
                {
                    sr.Close();
                    sr = null;
                }
            }
            return response;
        }

        public static string GetClientIP(HttpContextBase httpContext)
        {
            string result = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = httpContext.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = httpContext.Request.UserHostAddress;
            }
            return result;
        }

        public static string GetClientIP(HttpRequest request)
        {
            string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = request.UserHostAddress;
            }
            return result;
        }
    }
}