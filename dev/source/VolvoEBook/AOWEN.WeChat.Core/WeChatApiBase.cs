using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using AOWEN.WeChat.Core.Model;
using Tencent;
using System.Security.Cryptography;
using WeDo.Log;
using WeDo.Log.Model;

namespace AOWEN.WeChat.Core
{
    /// <summary>
    /// 微信基础接口处理
    /// </summary>
    /// Author:fredjiang
    /// Created:2015-11-12
    public class WeChatApiBase : WeChatAccountBase
    {
        public string Remark = "";
        public static object tokenLock = new object();

        //微信公众平台的AccessToken，只读，自动获取

        public WeChatApiBase()
        {
        }

        public WeChatApiBase(string openId)
        {
            this.OpenId = openId;
        }
        public string AccessToken
        {
            get
            {   //解决多线程下生成token或token异常失效
                string key = "access_token_" + WeChatAccount.OpenId;
                lock (tokenLock)
                {
                    try
                    {
                        if (HttpRuntime.Cache[key] == null)
                        {
                            WeChatApiResult apiResult = new WeChatApiResult();
                            if (WeChatAccount.AccounType == 3)
                            {
                                apiResult = CgiBinToken();
                            }
                            else
                            {
                                apiResult = CgiBinTokenForMP();
                            }

                            if (apiResult.Errcode == -100)
                            {
                                //腾讯接口令牌缓存110分钟
                                HttpRuntime.Cache.Insert(key, apiResult.Access_token, null, DateTime.Now.AddMinutes(110), TimeSpan.Zero);
                            }
                        }

                        if (HttpRuntime.Cache[key] != null)
                        {
                            return HttpRuntime.Cache[key].ToString();
                        }
                        else
                        {
                            return "";
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        return "";
                    }
                }
            }
        }


        public string JSApiTicket
        {
            get
            {
                //加锁为了防止重复生成token和token异常失效
                string wechat_JsApiTicket_LockStr = WeChatAccount.OpenId + "_Wechat_JsApiTicket_LockStr";

                lock (wechat_JsApiTicket_LockStr)
                {
                    if (HttpRuntime.Cache[wechat_JsApiTicket_LockStr] != null)
                    {
                        return HttpRuntime.Cache[wechat_JsApiTicket_LockStr].ToString();
                    }
                    else
                    {
                        string msg = "";
                        if (WeChatAccount.AccounType == 3)
                        {
                            msg = GetJsApi_TicketQY();
                        }
                        else
                        {
                            msg = GetJsApi_TicketMP();
                        }

                        if (!string.IsNullOrEmpty(msg))
                        {
                            HttpRuntime.Cache.Insert(wechat_JsApiTicket_LockStr, msg, null, DateTime.Now.AddMinutes(110), TimeSpan.Zero);
                        }
                        return msg;
                    }
                }
            }
        }

        #region Request方法
        /// <summary>
        /// 调用微信GET接口获取信息的通用方法。
        /// 该方法检查WECHAT API返回结果是否为错误信息，如果是，则在ExceptionLog中插入一条记录。(被注释,已无效)
        /// </summary>
        /// <param name="url">微信GET URL</param>
        /// <returns>结果字符串</returns>
        /// Author：gs 
        /// Date：20140710
        public bool WechatGetRequest(string url, out string result)
        {
            result = string.Empty;
            try
            {
                WebRequest wRequest = WebRequest.Create(url);
                WebResponse wResponse = wRequest.GetResponse();
                Stream stream = wResponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                result = reader.ReadToEnd();
                wResponse.Close();
                return true;
            }
            catch
            {

            }
            return false;

        }


        /// <summary>
        /// 向指定接口地发起POST请求
        /// </summary>
        /// <param name="url">接口url</param>
        /// <param name="reqStr">请求的字符串</param>
        /// <returns>接口返回的原始字符串</returns>
        /// Author：gs 
        /// Date：20140710
        public string WechatPostRequest(string url, string reqStr, string contentType = "application/x-www-form-urlencoded")
        {
            string result = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = contentType;
                Stream reqstream = req.GetRequestStream();
                byte[] b = Encoding.UTF8.GetBytes(reqStr);
                reqstream.Write(b, 0, b.Length);
                StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream(), System.Text.Encoding.UTF8);
                result = responseReader.ReadToEnd();
                responseReader.Close();
                reqstream.Close();
            }
            catch
            {

            }

            return result;
        }

        #endregion

        #region 基础方法

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>时间的数字格式</returns>
        /// Author FredJiang
        /// Create Date 2014年9月10日
        public int ConvertTimestamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// int转换为Datetime
        /// </summary>
        /// <param name="time">int类型</param>
        /// <returns>时间</returns>
        /// Author FredJiang
        /// Create Date 2014年9月10日
        public DateTime ConvertTimestamp(int time)
        {
            DateTime dt1970 = DateTime.Parse("01/01/1970");
            double addsec = Convert.ToDouble(time);
            DateTime result = dt1970.AddMilliseconds(addsec * 1000).ToLocalTime();
            return result;
        }

        /// <summary>
        /// 生成接口使用的随机数
        /// 10位
        /// </summary>
        /// <returns>10整数</returns>
        /// Author FredJiang
        /// Create Date 2014年10月24日
        public int CreateNonce()
        {
            Random random = new Random();
            int result = random.Next(1000000000, 2147483647);
            return result;
        }

        /// <summary>
        /// 获得当前网站的域名(http://www.aa.com)
        /// </summary>
        /// <returns></returns>
        /// gs
        /// 20140717
        public static string GetCurrentDomain()
        {
            string reStr = "";
            reStr = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;
            return reStr;
        }


        /// <summary>
        /// 把虚拟路径转化为http格式路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// gs
        /// 20140717
        public string GetHttpUrl(string url)
        {
            string reStr = "";
            if (url.Contains("://"))
            {
                return url;
            }
            try
            {
                reStr = GetCurrentDomain() + VirtualPathUtility.ToAbsolute(url);
            }
            catch
            {
                reStr = "";
            }
            return reStr;
        }

        /// <summary>
        /// 接口返回的json字符串序列化为API接口返回对象
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public WeChatApiResult GetJsonToModel(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = (Dictionary<string, object>)jss.DeserializeObject(json);
            WeChatApiResult war = new WeChatApiResult();
            foreach (System.Reflection.PropertyInfo pi in typeof(WeChatApiResult).GetProperties())
            {
                foreach (string k in dic.Keys)
                {
                    if (k.ToLower() == pi.Name.ToLower())
                    {
                        war.GetType().GetProperty(pi.Name).SetValue(war, dic[k], null);
                        break;
                    }
                }
            }
            return war;
        }

        /// <summary>
        /// JSON字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">目标对象</typeparam>
        /// <param name="json">JSON</param>
        /// <returns>封装值的对象</returns>
        public T JsonToModel<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = (Dictionary<string, object>)jss.DeserializeObject(json);
            foreach (System.Reflection.PropertyInfo pi in typeof(T).GetProperties())
            {
                foreach (string k in dic.Keys)
                {
                    if (k.ToLower() == pi.Name.ToLower())
                    {
                        obj.GetType().GetProperty(pi.Name).SetValue(obj, dic[k], null);
                        break;
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// JSON字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">目标对象</typeparam>
        /// <param name="json">JSON</param>
        /// <returns>封装值的对象</returns>
        public T JsonDeserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            obj = jss.Deserialize<T>(json);
            return obj;
        }

        /// <summary>
        /// 对象序列化为JSON字符串
        /// </summary>
        /// <typeparam name="T">目标对象</typeparam>
        /// <param name="json">JSON</param>
        /// <returns>封装值的对象</returns>
        public string ModelToJson<T>(T t)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(t);
            return json;
        }


        /// <summary>
        /// 返回当前请求中指定参数值
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <returns>参数值</returns>
        /// Author FredJiang
        /// Create Date 2014年10月25日
        private string GetQueryString(string parameterName)
        {
            string reStr = HttpContext.Current.Request.QueryString[parameterName] ?? "";
            return reStr;
        }

        #endregion

        #region 企业号接口基础方法

        /// <summary>
        /// 对加密码消息进行解密
        /// </summary>
        /// <param name="msg">已加密消息</param>
        /// <returns>原始字符</returns>
        /// Author FredJiang
        /// Create Date 2014年10月24日
        public string DecryptMeg(string msg, string msg_signature, string timestamp, string nonce)
        {
            string reStr = "";
            WXBizMsgCrypt wxb = new WXBizMsgCrypt(WeChatAccount.Token, WeChatAccount.EncodingAESKey, WeChatAccount.OpenId);
            int result = wxb.DecryptMsg(msg_signature, timestamp, nonce, msg, ref reStr);
            if (result != 0)
            {
                Remark = result.ToString();//把失败原因做为备注返回
            }
            return reStr;
        }

        /// <summary>
        /// 对原始消息进行加密操作
        /// </summary>
        /// <param name="msg">原始消息内容</param>
        /// <returns>加密码字符</returns>
        /// Author FredJiang
        /// Create Date 2014年10月24日
        public string EncryptMsg(string msg, string timeStamp, string nonce)
        {
            string reStr = "";
            WXBizMsgCrypt wxb = new WXBizMsgCrypt(WeChatAccount.Token, WeChatAccount.EncodingAESKey, WeChatAccount.OpenId);
            int result = wxb.EncryptMsg(msg, timeStamp, nonce, ref reStr);
            if (result != 0)
            {
                Remark = result.ToString();
            }

            return reStr;
        }

        /// <summary>
        /// 生成接口签名字符串
        /// </summary>
        /// <param name="timeStamp">赶时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <returns>签名字符串</returns>
        /// Author FredJiang
        /// Create Date 2014年11月2日
        public string GenarateSinature(string timeStamp, string nonce, string sMsgEncrypt)
        {
            string reStr = "";
            int result = WXBizMsgCrypt.GenarateSinature(WeChatAccount.Token, timeStamp, nonce, sMsgEncrypt, ref reStr);

            return reStr;
        }

        /// <summary>
        /// 微信服务器请求验证方法,并返回验证字符串,验证失败返回空
        /// </summary>
        /// <param name="echoStr">加密的随机字符串</param>
        /// <param name="msg_signature">签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <returns>验证字符串</returns>
        /// Author:fredjiang
        /// Created:2015-11-14
        public string ValidRequest(string echoStr, string msg_signature, string timestamp, string nonce)
        {


            string reStr = "";
            if (!string.IsNullOrEmpty(echoStr) && !string.IsNullOrEmpty(msg_signature) && !string.IsNullOrEmpty(timestamp) && !string.IsNullOrEmpty(nonce))
            {


                WXBizMsgCrypt wxb = new WXBizMsgCrypt(WeChatAccount.Token, WeChatAccount.EncodingAESKey, WeChatAccount.OpenId);
                string replyEchoStr = "";
                int n = wxb.VerifyURL(msg_signature, timestamp, nonce, echoStr, ref replyEchoStr);
                if (n == 0)
                {
                    reStr = replyEchoStr;
                }
                else
                {
                    Remark = n.ToString();
                }
                Remark += string.Format("openId:{0},agentId:{1},Token:{2},AccountToken:{3}", OpenId, AgentId, WeChatAccount.Token, AccessToken);
            }


            return reStr;
        }

        #endregion

        #region 服务号、订阅号基本方法
        /// <summary>
        /// 服务号订阅号微信服务器请求验证方法,并返回验证字符串,验证失败返回空
        /// </summary>
        /// <param name="echoStr">加密的随机字符串</param>
        /// <param name="msg_signature">签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <returns>验证字符串</returns>
        /// Author:fredjiang
        /// Created:2015-11-14
        public string MPValidRequest(string echoStr, string signature, string timestamp, string nonce)
        {
            string reStr = "";
            if (!string.IsNullOrEmpty(echoStr) && !string.IsNullOrEmpty(signature) && !string.IsNullOrEmpty(timestamp) && !string.IsNullOrEmpty(nonce))
            {
                Remark = WeChatAccount.OpenId + "," + WeChatAccount.Token;
                string[] ArrTmp = { WeChatAccount.Token, timestamp, nonce };
                Array.Sort(ArrTmp);     //字典排序
                string tmpStr = string.Join("", ArrTmp);
                tmpStr = SHA1_Encrypt(tmpStr);
                tmpStr = tmpStr.ToLower();
                if (tmpStr == signature)
                {
                    reStr = echoStr;
                }
                else
                {
                    reStr = "error";
                }
            }
            return reStr;
        }

        /// <summary>
        /// 对字符串进行SHA1加密
        /// </summary>
        /// <param name="Source_String"></param>
        /// <returns></returns>
        public string SHA1_Encrypt(string Source_String)
        {
            byte[] StrRes = Encoding.Default.GetBytes(Source_String);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }
        #endregion

        #region 微信接口调用方法
        /// <summary>
        /// 获取企业号access token。
        /// 私有。
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// Author：gs
        /// Date：20140709
        public WeChatApiResult CgiBinToken()
        {

            string RequestUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", WeChatAccount.OpenId, WeChatAccount.AppSecret);
            string message = "";
            WechatGetRequest(RequestUrl, out message);
            WeChatApiResult apiResult = GetJsonToModel(message);
            return apiResult;
        }
        /// <summary>
        /// 获得订阅号，服务号accesstoken
        /// </summary>
        /// <returns></returns>
        public WeChatApiResult CgiBinTokenForMP()
        {

            string RequestUrl = string.Format(" https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", WeChatAccount.AppId, WeChatAccount.AppSecret);
            string message = "";
            WechatGetRequest(RequestUrl, out message);
            WeChatApiResult apiResult = GetJsonToModel(message);
            return apiResult;
        }
        /// <summary>
        /// 根据code获取成员信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// ZSH
        /// 20151116
        public WeChatApiResult Oauth2GetuserInfo(string code)
        {
            string url = "";
            if (WeChatAccount.AccounType == 3)
            {
                url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}", AccessToken, code);
            }
            else
            {
                url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", WeChatAccount.AppId, WeChatAccount.AppSecret, code);
            }
            string message = "";
            WechatGetRequest(url, out message);
            Remark = message;
            WeChatApiResult apiResult = GetJsonToModel(message);
            return apiResult;
        }

        /// <summary>
        /// 根据code获取成员信息
        /// </summary>
        /// <param name="access_token">授权链接令牌</param>
        /// <param name="openId">粉丝OpenId</param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        /// <returns></returns>
        /// Author:fredjiang
        /// Created:2015-12-18
        public WeChatFans SNSUserInfo(string access_token, string openId, string lang = "zh_CN")
        {
            string url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang={2}", access_token, openId, lang);
            string message = "";
            WechatGetRequest(url, out message);
            WeChatFans apiResult = JsonToModel<WeChatFans>(message);
            return apiResult;
        }

        /// <summary>
        /// 把虚拟路径转化为可以授权的url
        /// </summary>
        /// <param name="url">待转化的url</param>
        /// <param name="scope">授权类型，1为"snsapi_base"仅获得openid，2为"snsapi_userinfo"还可获得用户信息</param>
        /// <param name="state">重定向后会带上state参数，可以填写a-zA-Z0-9的参数值</param>
        /// <returns></returns>
        /// gs
        /// 20140717
        /// updated:2015-11-26 fredjiang
        public string GetOauthUrl(string url, int scope = 1, string state = "state")
        {
            string reStr = "";
            if (url.Contains("http://") == false && url.Contains("https://") == false&&url!="")
            {
                url = "http://" + WeChatAccount.DomainName + VirtualPathUtility.ToAbsolute(url);
                url = System.Web.HttpUtility.UrlEncode(url);
                string scopeStr = "snsapi_userinfo";
                if (scope == 1)
                {
                    scopeStr = "snsapi_base";
                }
                string targetUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect";
                reStr = string.Format(targetUrl, WeChatAccount.AppId, url, scopeStr, state);
            }
            else
            {
                reStr = url;
            }
            return reStr;
        }
        /// <summary>
        /// 获得服务号JSAPI Tiket
        /// </summary>
        /// <returns></returns>
        public string GetJsApi_TicketMP()
        {
            string message = "";

            string url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", AccessToken);
            string ret;

            WechatGetRequest(url, out ret);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = (Dictionary<string, object>)jss.DeserializeObject(ret);
            if (dic.ContainsKey("ticket"))
            {
                message = dic["ticket"].ToString();
            }

            return message;
        }

        /// <summary>
        /// 获得企业号JSAPI Tiket
        /// </summary>
        /// <returns></returns>
        public string GetJsApi_TicketQY()
        {
            string message = "";

            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={0}", AccessToken);
            string ret;

            WechatGetRequest(url, out ret);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = (Dictionary<string, object>)jss.DeserializeObject(ret);
            if (dic.ContainsKey("ticket"))
            {
                message = dic["ticket"].ToString();
            }
            LogRunMan.AddLog("企业获得jsapiticket", EnumListLog.LogLevel.INFO, DateTime.Now, "ticket:" + message);
            return message;
        }
        #endregion
    }
}
