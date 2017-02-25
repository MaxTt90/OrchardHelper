using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOWEN.WeChat.Core.Model
{
    /// <summary>
    /// 微信账号信息类
    /// </summary>
    /// Author:fredjiang
    /// Created:2015-11-12
    [Serializable]
    public class WeChatAccount
    {
        private string _appId = "";
        private string _encodingAESKey = "";
        private string _openId = "";
        private string _appSecret = "";
        private string _domainName = "";
        private string _token = "";
        private int _agentId = 0;
        private int _accounttype = 1;      

       
        public string AppId
        {
            get { return _appId; }
            set { _appId = value; }
        }

        public string EncodingAESKey
        {
            get { return _encodingAESKey; }
            set { _encodingAESKey = value; }
        }

        public string OpenId
        {
            get { return _openId; }
            set { _openId = value; }
        }

        public string AppSecret
        {
            get { return _appSecret; }
            set { _appSecret = value; }
        }

        public string DomainName
        {
            get { return _domainName; }
            set { _domainName = value; }
        }


        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }

        public int AgentId
        {
            get { return _agentId; }
            set { _agentId = value; }
        }

        /// <summary>
        /// 账号类型，订阅号=1,服务号=2,企业号=3
        /// </summary>
        public int AccounType
        {
            get { return _accounttype; }
            set { _accounttype = value; }
        }

        
    }
}
