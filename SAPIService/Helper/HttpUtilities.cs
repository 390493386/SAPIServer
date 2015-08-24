using System.IO;
using System.Net;
using System.Text;

namespace SiweiSoft.SAPIService.Helper
{
    public class HttpUtilities
    {
        public static string HttpGet(string url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "json; charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
            string retString = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();

            return retString;
        }
    }
}
