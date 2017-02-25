using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOWEN.WeChat.Core.Model
{
    public class WeChatGroupResult : WeChatApiResult
    {
        List<WeChatGroup> _weChatGroupList = new List<WeChatGroup>();

        /// <summary>
        /// 分组集合
        /// </summary>
        public List<WeChatGroup> Groups
        {
            get { return _weChatGroupList; }
            set { _weChatGroupList = value; }
        }

    }

    public class WeChatGroupResult1 : WeChatApiResult
    {
        public WeChatGroup group = new WeChatGroup();
    }

    public class WeChatGroup
    {
        int _id = 0;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        string _name = "";

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        int _count = 0;

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }
    }
}
