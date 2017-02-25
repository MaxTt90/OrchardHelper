using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOWEN.WeChat.Core.Model
{
    /// <summary>
    /// 保存微信接口返回值
    /// </summary>
    [Serializable]
    public class WeChatApiResult
    {
        int _errcode = -100;
        string _errmsg = "";
        string _access_token = "";
        int _expires_in = 0;
        string _userId = "";
        string _openId = "";
        string _type = "";
        string _media_id = "";
        string _scope = "";
        string _unionid = "";
        string _deviceId = "";
        int _groupId = 0;
        int _id = 0;
        string _name = "";
        string _url = "";
        
        public int Errcode
        {
            get { return _errcode; }
            set { _errcode = value; }
        }

        public string Errmsg
        {
            get { return _errmsg; }
            set { _errmsg = value; }
        }

        public string Access_token
        {
            get { return _access_token; }
            set { _access_token = value; }
        }

        public int Expires_in
        {
            get { return _expires_in; }
            set { _expires_in = value; }
        }

        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public string OpenId
        {
            get { return _openId; }
            set { _openId = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public string Media_id
        {
            get { return _media_id; }
            set { _media_id = value; }
        }

        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔
        /// </summary>
        public string Scope
        {
            get { return _scope; }
            set { _scope = value; }
        }

        /// <summary>
        /// 当且仅当该公众号已获取用户的userinfo授权，并且该公众号已经绑定到微信开放平台帐号时，才会出现该字段
        /// </summary>
        public string Unionid
        {
            get { return _unionid; }
            set { _unionid = value; }
        }

        /// <summary>
        /// 手机设备号(由微信在安装时随机生成，删除重装会改变，升级不受影响) 
        /// </summary>
        public string DeviceId
        {
            get { return _deviceId; }
            set { _deviceId = value; }
        }

        /// <summary>
        /// 粉丝分组编号
        /// </summary>
        public int GroupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }



    }
}
