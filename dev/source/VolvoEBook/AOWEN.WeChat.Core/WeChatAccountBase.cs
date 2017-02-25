using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Threading.Tasks;

using AOWEN.WeChat.Core.Model;
using WeDo.Log;

namespace AOWEN.WeChat.Core
{
    public class WeChatAccountBase
    {
        static object LockWeChatAccountList = new object(); //微信公众账号集合缓存锁


        private int _agentId = -1;
        /// <summary>
        /// 企业号的应用编号
        /// </summary>
        public int AgentId
        {
            get
            {
                try
                {
                    if (_agentId == -1)
                    {
                        try
                        {
                            if (HttpContext.Current != null && (HttpContext.Current.Request["AgentId"] ?? "") != "")
                            {
                                _agentId = Convert.ToInt32(HttpContext.Current.Request.Params["AgentId"]);
                            }
                            else
                            {
                                _agentId = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
                return _agentId;
            }

            set { _agentId = value; }
        }

        private string _openId = "";

        /// <summary>
        /// 指定前公众账号的OpenId
        /// </summary>
        public string OpenId
        {
            get
            {
                if (string.IsNullOrEmpty(_openId) && HttpContext.Current != null && HttpContext.Current.Request.Url.Host == "localhost")
                {
                    //如果是测试环境则从配置文件中获得默认的公众账号OpenId
                    _openId = ConfigurationManager.AppSettings["DefaultWeChatAccount"];
                }
                return _openId;
            }
            set { _openId = value; }
        }

        private string _accountCacheKey = "";

        public string AccountCacheKey
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(_accountCacheKey))
                    {
                        if (!string.IsNullOrEmpty(OpenId))
                        {
                            _accountCacheKey = OpenId + "_" + AgentId.ToString();
                        }
                        else
                        {
                            _accountCacheKey = HttpContext.Current.Request.Url.Host + "_" + AgentId.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
                return _accountCacheKey;
            }
        }

        private WeChatAccount _weChatAccount = null;
        //获得当前平台的微信账号对象
        public WeChatAccount WeChatAccount
        {
            get
            {
                if (_weChatAccount == null)
                {
                    _weChatAccount = GetWeChatAccount();
                }
                return _weChatAccount;
            }
        }

        /// <summary>
        /// 设置系统微信公众账号集合
        /// </summary>
        public List<WeChatAccount> WeChatAccountList
        {
            set
            {
                lock (LockWeChatAccountList)
                {
                    string key = "aowen_wechat_account_list";
                    if (value != null && value.Count > 0)
                    {
                        HttpRuntime.Cache[key] = value;
                    }
                }
            }

        }

        /// <summary>
        /// 获得当前应用账号信息
        /// </summary>
        /// <returns>应用平台账号信息</returns>
        private WeChatAccount GetWeChatAccount()
        {
            WeChatAccount wa = null;
            try
            {
                if (HttpRuntime.Cache[AccountCacheKey] == null)
                {
                    //lock (LockWeChatAccountList)
                    //{
                    string key = "aowen_wechat_account_list";

                    if (HttpRuntime.Cache[key] != null)
                    {
                        List<WeChatAccount> list = HttpRuntime.Cache[key] as List<WeChatAccount>;
                        if (!string.IsNullOrEmpty(OpenId))
                        {
                            //根据OpenId和AgentId获取公众账号对象
                            wa = list.Find(o => o.AgentId == AgentId && o.OpenId == OpenId);
                        }
                        else
                        {
                            //根据域名和AgentId获取公众账号对象
                            wa = list.Find(o => o.AgentId == AgentId && o.DomainName == HttpContext.Current.Request.Url.Host);
                        }

                        if (wa != null)
                        {
                            HttpRuntime.Cache.Insert(AccountCacheKey, wa, null, DateTime.MaxValue, new TimeSpan(0, 30, 0));
                        }
                        //}
                    }
                }
                else
                {
                    wa = (WeChatAccount)HttpRuntime.Cache[AccountCacheKey];
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return wa;
        }


    }
}
