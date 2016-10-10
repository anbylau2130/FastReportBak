using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace USP.Common
{
    public static class ConverType
    {

        public static string TreeDataToJson<T>(this T data)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(data.GetType());
                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, data);

                    sb.Append(string.Format("{0}", Encoding.UTF8.GetString(ms.ToArray())));

                    return sb.ToString();
                }
            }
            catch
            {
                return sb.ToString();
            }
        }
    }
}