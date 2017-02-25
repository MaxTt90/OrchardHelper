using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOWEN.WeChat.Core.Model
{
    public class WeChatQRcode:WeChatApiResult
    {
        string _ticket = "";
        string _url = "";
        int _expire_seconds = 0;

        /// <summary>
        /// 获取的二维码ticket，凭借此ticket可以在有效时间内换取二维码
        /// </summary>
        public string Ticket
        {
            get { return _ticket; }
            set { _ticket = value; }
        }

        /// <summary>
        /// 二维码图片解析后的地址，开发者可根据该地址自行生成需要的二维码图片
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// 该二维码有效时间，以秒为单位。 最大不超过2592000（即30天）。
        /// </summary>
        public int Expire_seconds
        {
            get { return _expire_seconds; }
            set { _expire_seconds = value; }
        }
    }
}
