using SiweiSoft.SAPIService.Core;
using SiweiSoft.SAPIService.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
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
            string msgFrom = Parameters["FromUserName"].ToString();
            string msgTo = Parameters["ToUserName"].ToString();
            string msgType = Parameters["MsgType"].ToString();

            WXActionResult ar = new WXActionResult();
            if (msgType == "text")
            {
                string content = Parameters["Content"].ToString();
                string[] userConfig = GetUserConfig(msgFrom);
                if (Parameters.ContainsKey("echostr") && ValidateSignature(userConfig[0]))
                {
                    ar.Result.Add("single", Parameters["echostr"]);
                }
                else
                {
                    string resp = HttpUtilities.HttpGet("http://www.tuling123.com/openapi/api", "key=" +
                        userConfig[1] + "&userid=" + msgFrom + "&info=" + content);
                    string text = HandleResponse(resp);

                    ar.Result.Add("single", String.Format(text, Parameters["FromUserName"].ToString(),
                        Parameters["ToUserName"].ToString()));
                }
            }
            else if (msgType == "event")
            {
                string _event = Parameters["Event"].ToString();
                if (_event == "subscribe")
                {
                    string subscribeMsgModel = "<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{3}]]></Content></xml>";
                    string subscribeMsg = "欢迎关注“微平台开发”/:rose：\n无论您是想了解微信公众平台开发还是有开发公众平台的需求都可以关注我们，我们定期更新最新的IT动向，发布最新的移动互联网信息，让您在移动互联网时代快人一步，我们专业承接微信公众平台开发，欢迎前来洽谈。\n使用说明：\n/:heart回复任意内容可和智能客服聊天。/:rose\n/:heart查询天气：回复关键字“天气”，如“上海天气”，可查询当天上海天气情况。/:rose\n/:heart查询快递：回复关键字“快递查询”，可激活查询快递功能。/:rose\n/:heart查询菜谱：回复关键字“红烧肉”，可智能搜索多种红烧肉做法。/:rose\n/:heart成语接龙：回复关键字“成语接龙”，可激活成语接龙游戏。/:rose\n/:heart讲笑话，看新闻等等更过精彩内容等你去发掘。/:rose\n";
                    ar.Result.Add("single", String.Format(subscribeMsgModel, Parameters["FromUserName"].ToString(),
                        Parameters["ToUserName"].ToString(), DateTime.Now.Ticks, subscribeMsg));
                }
            }
            else
                ar.Result.Add("single", "不支持的消息类型！");

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

        //用户配置数组
        //0:token
        //1:Turning robert API key
        //2:Turning robert user id
        private string[] GetUserConfig(string wechatId)
        {
            return new string[] 
            {
                "weipingtaidev",
                "84be2362d067f9f1afb6c3d9ace2cd6f"
            };
        }

        private string HandleResponse(string response)
        {
            StringBuilder result = new StringBuilder("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>")
                .Append(DateTime.Now.Ticks.ToString()).Append("</CreateTime>");

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var responseObj = serializer.Deserialize<Dictionary<string, object>>(response);
            int code = (int)responseObj["code"];
            if (code == 100000)
            {
                result.Append("<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[")
                    .Append((string)responseObj["text"])
                    .Append("]]></Content></xml>");
            }
            else if (code == 200000)
            {
                result.Append("<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[<a ")
                    .Append((string)responseObj["url"])
                    .Append(">")
                    .Append((string)responseObj["text"])
                    .Append("</a>]]></Content></xml>");
            }
            else if (code == 302000)
            {
                ArrayList articles = (ArrayList)responseObj["list"];
                int count = articles.Count > 9 ? 10 : articles.Count + 1;
                result.Append("<MsgType><![CDATA[news]]></MsgType><ArticleCount>")
                    .Append(count.ToString())
                    .Append("</ArticleCount><Articles>")
                    .Append("<item><Title><![CDATA[")
                    .Append((string)responseObj["text"])
                    .Append("]]></Title><Description><![CDATA[]]></Description><PicUrl><![CDATA[")
                    .Append("http://unidust.cn/images/weixin-xinwen.png")
                    .Append("]]></PicUrl><Url><![CDATA[")
                    .Append("http://news.163.com/mobile/?from=index.titlebar")
                    .Append("]]></Url></item>");

                for (int i = 0; i < count; ++i)
                {
                    Dictionary<string, object> article = (Dictionary<string, object>)articles[i];
                    result.Append("<item><Title><![CDATA[")
                        .Append((string)article["article"])
                        .Append("]]></Title><Description><![CDATA[")
                        .Append((string)article["article"])
                        .Append("]]></Description><PicUrl><![CDATA[")
                        .Append((string)article["icon"])
                        .Append("]]></PicUrl><Url><![CDATA[")
                        .Append((string)article["detailurl"])
                        .Append("]]></Url></item>");
                }
                result.Append("</Articles></xml>");
            }
            else if (code == 305000)//列车信息
            {
                ArrayList articles = (ArrayList)responseObj["list"];
                int count = articles.Count > 9 ? 10 : articles.Count + 1;
                result.Append("<MsgType><![CDATA[news]]></MsgType><ArticleCount>")
                    .Append(count.ToString())
                    .Append("</ArticleCount><Articles>")
                    .Append("<item><Title><![CDATA[")
                    .Append((string)responseObj["text"])
                    .Append("]]></Title><Description><![CDATA[]]></Description><PicUrl><![CDATA[")
                    .Append("http://unidust.cn/images/weixin-lieche.png")
                    .Append("]]></PicUrl><Url><![CDATA[]]></Url></item>");

                for (int i = 0; i < count; ++i)
                {
                    Dictionary<string, object> article = (Dictionary<string, object>)articles[i];
                    string start = (string)article["start"];
                    string terminal = (string)article["terminal"];
                    string startTime = (string)article["starttime"];
                    string endTime = (string)article["endtime"];
                    result.Append("<item><Title><![CDATA[")
                        .Append(start + "-" + terminal + "\n" + startTime + "," + endTime)
                        .Append("]]></Title><Description><![CDATA[]]></Description><PicUrl><![CDATA[")
                        .Append((string)article["icon"])
                        .Append("]]></PicUrl><Url><![CDATA[")
                        .Append((string)article["detailurl"])
                        .Append("]]></Url></item>");
                }
                result.Append("</Articles></xml>");
            }
            else if (code == 308000)//菜谱
            {
                ArrayList articles = (ArrayList)responseObj["list"];
                int count = articles.Count > 9 ? 10 : articles.Count + 1;
                result.Append("<MsgType><![CDATA[news]]></MsgType><ArticleCount>")
                    .Append(count.ToString())
                    .Append("</ArticleCount><Articles>")
                    .Append("<item><Title><![CDATA[")
                    .Append((string)responseObj["text"])
                    .Append("]]></Title><Description><![CDATA[]]></Description><PicUrl><![CDATA[")
                    .Append("http://unidust.cn/images/weixin-caijia.png")
                    .Append("]]></PicUrl><Url><![CDATA[")
                    .Append("http://m.xiachufang.com/")
                    .Append("]]></Url></item>");

                for (int i = 0; i < count; ++i)
                {
                    Dictionary<string, object> article = (Dictionary<string, object>)articles[i];
                    result.Append("<item><Title><![CDATA[")
                        .Append((string)article["name"] + "\n" + (string)article["info"])
                        .Append("]]></Title><Description><![CDATA[]]></Description><PicUrl><![CDATA[")
                        .Append((string)article["icon"])
                        .Append("]]></PicUrl><Url><![CDATA[")
                        .Append((string)article["detailurl"])
                        .Append("]]></Url></item>");
                }
                result.Append("</Articles></xml>");
            }
            else if (code == 40001)
            {
                result.Append("<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[key的长度错误（32位）]]></Content></xml>");
            }
            else if (code == 40002)
            {
                result.Append("<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[请求内容为空]]></Content></xml>");
            }
            else if (code == 40003)
            {
                result.Append("<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[key错误或帐号未激活]]></Content></xml>");
            }
            else if (code == 40004)
            {
                result.Append("<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[当天请求次数已用完]]></Content></xml>");
            }
            else if (code == 40005)
            {
                result.Append("<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[暂不支持该功能]]></Content></xml>");
            }
            else if (code == 40006)
            {
                result.Append("<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[服务器升级中]]></Content></xml>");
            }
            else if (code == 40007)
            {
                result.Append("<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[服务器数据格式异常]]></Content></xml>");
            }
            else
            {
                result.Append("<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[未知响应码]]></Content></xml>");
            }
            return result.ToString(); ;
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
