using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOWEN.WeChat.Core.Model
{
    /// <summary>
    /// 微信API接口返回基础信息对象
    /// </summary>
    public class WeChatApiBaseResult
    {
        int _errcode = -1;
        string _errmsg = "";

        /// <summary>
        /// 结果代码，-1表示没有返回值，0表示成功
        /// </summary>
        public int Errcode
        {
            get { return _errcode; }
            set { _errcode = value; }
        }

        /// <summary>
        /// 结果描述
        /// </summary>
        public string Errmsg
        {
            get { return _errmsg; }
            set { _errmsg = value; }
        }
    }
}
