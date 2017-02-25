using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOWEN.WeChat.Core.Model
{
    [Serializable]
    public class WeChatUserInfo
    {
        int _errcode = -1;
        string _errmsg = "";
        string _userid = "";
        string _name = "";
        string _position = "";
        string _mobile = "";
        string _gender = "";
        string _email = "";
        string _weixinid = "";
        string _avatar = "";
        int _status = 0;

        object _department = new object();
        object _extattr = new object();
        object _userlist = new object();

        #region 特殊处理

        public List<WeChatUserInfo> DeptUserList; 

        #endregion

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
        public string Userid
        {
            get { return _userid; }
            set { _userid = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string Weixinid
        {
            get { return _weixinid; }
            set { _weixinid = value; }

        }
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }

        }
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public object Department
        {
            get { return _department; }
            set { _department = value;}

        }
        public object Extattr
        {
            get { return _extattr; }
            set {  _extattr = value;}
        }
        public object Userlist
        {
            get { return _userlist; }
            set { _userlist = value; }
        }
    }
    public class attrs
    {
        public string name = "";
        public string value = "";
    }
}
