using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOWEN.WeChat.Core.Model
{
    public class WeChatFans : WeChatApiBaseResult
    {
        private string _openid = "";
        private int _subscribe = 0;
        private string _nickname = "";
        private int _sex = 0;
        private string _language = "";
        private string _city = "";
        private string _province = "";
        private string _country = "";
        private string _headimgurl = "";
        private int _subscribe_time = 0;
        private string _remark = "";
        private string _unionid = "";
        private int _groupid = 0;

        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId
        {
            get { return _openid; }
            set { _openid = value; }
        }

        /// <summary>
        /// 关注状态
        /// </summary>
        public int Subscribe
        {
            get { return _subscribe; }
            set { _subscribe = value; }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            get { return _nickname; }
            set { _nickname = value; }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        /// <summary>
        /// 语言
        /// </summary>
        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }
        /// <summary>
        /// 城市
        /// </summary>
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province
        {
            get { return _province; }
            set { _province = value; }
        }

        /// <summary>
        /// 国家
        /// </summary>
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImgUrl
        {
            get { return _headimgurl; }
            set { _headimgurl = value; }
        }
        /// <summary>
        /// 关注时间
        /// </summary>
        public int Subscribe_Time
        {
            get { return _subscribe_time; }
            set { _subscribe_time = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        /// <summary>
        /// 粉丝唯一标识
        /// </summary>
        public string Unionid
        {
            get { return _unionid; }
            set { _unionid = value; }
        }


        /// <summary>
        /// 分组编号
        /// </summary>
        public int GroupId
        {
            get { return _groupid; }
            set { _groupid = value; }
        }

    }
}
