using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BIZ
{
    public class DeCryptedQueryString
    {
        public DeCryptedQueryString() { }
        public string GetDecryptedQueryString(EncryptedQueryString objEncryptedQueryString)
        {
            StringBuilder Info = new StringBuilder();
            foreach (String key in objEncryptedQueryString.Keys)
            {
                Info.AppendFormat("{0} = {1}<br>", key, objEncryptedQueryString[key]);
            }
            return Info.ToString();
        }
        public string GetDecryptedQueryString(EncryptedQueryString objEncryptedQueryString, string pkey)
        {
            StringBuilder Info = new StringBuilder();
            pkey = pkey.ToLower();
            foreach (String key in objEncryptedQueryString.Keys)
            {
                if (key == pkey)
                {
                    Info.Append(objEncryptedQueryString[key]);
                    break;
                }
            }
            return Info.ToString();
        }
        public static string Decrypted_REQUEST_FromURL(string vData, string request)
        {
            string vREQUEST = string.Empty;
            DeCryptedQueryString objDecryptedQueryString = new DeCryptedQueryString();
            if (!string.IsNullOrEmpty(vData))
            {
                EncryptedQueryString objQueryString = new EncryptedQueryString(vData);
                vREQUEST = objDecryptedQueryString.GetDecryptedQueryString(objQueryString, request);
            }
            else
            {
                vREQUEST = "0";
            }

            return vREQUEST;
        }
    }
}
