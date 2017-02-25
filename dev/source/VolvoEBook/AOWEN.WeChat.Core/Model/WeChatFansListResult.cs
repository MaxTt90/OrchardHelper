using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOWEN.WeChat.Core.Model
{
    public class WeChatFansListResult : WeChatApiBaseResult
    {
        private int _total = 0;
        private int _count = 0;
        private List<string> _data = new List<string>();
        private string _next_openid = "";

        public int Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public List<string> Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public string NextOpenId
        {
            get { return _next_openid; }
            set { _next_openid = value; }
        }


    }
}
