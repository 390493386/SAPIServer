using SiweiSoft.SAPIService.Core;
using SiweiSoft.SAPIService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SiweiSoft.SAPIServer.Controllers
{
    public class WeChatController : Controller
    {
        /// <summary>
        /// 微信服务接口，微信号：weipingtai_dev
        /// </summary>
        /// <returns></returns>
        [Action("WPTDEV", needAuthorize: false)]
        public WXActionResult WPTDev()
        {
            string token = "weipingtaidev";

            WXActionResult ar = new WXActionResult();

            if (Parameters.ContainsKey("echostr") && ValidateSignature(token))
            {
                ar.Result.Add("single", Parameters["echostr"]);
            }
            else
            {
                string text = @"<xml>
                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                <CreateTime>{2}</CreateTime>
                                <MsgType><![CDATA[text]]></MsgType>
                                <Content><![CDATA[{3}]]></Content>
                                </xml>";
                string content = Parameters.ContainsKey("Content") ? Parameters["Content"].ToString() : null;
                string resp = HttpUtilities.HttpGet("http://rmbz.net/Api/AiTalk.aspx", "key=rmbznet&word=" + content);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, string> result = serializer.Deserialize<Dictionary<string, string>>(resp);

                string responseUser = "空";
                if (result.ContainsKey("content"))
                    responseUser = result["content"];

                ar.Result.Add("single", String.Format(text, Parameters["FromUserName"].ToString(), 
                    Parameters["ToUserName"].ToString(),DateTime.Now.Ticks, responseUser));
            }

            return ar;
        }

        private bool ValidateSignature(string token)
        {
            bool validate = false;

            string[] arrTemp = new string[3];
            arrTemp[0] = token;
            string signature = Parameters.ContainsKey("signature") ? Parameters["signature"].ToString() : null;
            arrTemp[1] = Parameters.ContainsKey("timestamp") ? Parameters["timestamp"].ToString() : null;
            arrTemp[2] = Parameters.ContainsKey("nonce") ? Parameters["nonce"].ToString() : null;
            //字典排序
            Array.Sort(arrTemp);
            string tmpStr = string.Join("", arrTemp);

            byte[] sha1In = UTF8Encoding.Default.GetBytes(tmpStr);
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] sha1Out = sha1.ComputeHash(sha1In);
            sha1.Clear();
            tmpStr = BitConverter.ToString(sha1Out);

            tmpStr = tmpStr.Replace("-", "").ToLower();
            if (tmpStr == signature)
                validate = true;

            return validate;
        }
    }

    /// <summary>
    /// 微信返回结果
    /// </summary>
    public class WXActionResult : ActionResult
    {
        public override string GetResultString()
        {
            return Result.ContainsKey("single") ? Result["single"].ToString() : "";
        }
    }
}
