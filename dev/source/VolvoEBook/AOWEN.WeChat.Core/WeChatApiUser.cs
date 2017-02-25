using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using AOWEN.WeChat.Core.Model;

namespace AOWEN.WeChat.Core
{
    public class WeChatApiUser : WeChatApiBase
    {

        public WeChatApiUser(string openId)
        {
            this.OpenId = openId;
        }

        public WeChatApiUser()
        {
        }

        #region 基础方法
        /// <summary>
        /// 接口返回的json字符串序列化为API接口返回对象
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public WeChatUserInfo GetJsonToUser(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = (Dictionary<string, object>)jss.DeserializeObject(json);
            WeChatUserInfo wui = new WeChatUserInfo();
            foreach (System.Reflection.PropertyInfo pi in typeof(WeChatUserInfo).GetProperties())
            {
                if (dic.ContainsKey(pi.Name.ToLower()))
                {
                    wui.GetType().GetProperty(pi.Name).SetValue(wui, dic[pi.Name.ToLower()], null);
                }
            }
            return wui;
        }
        #endregion

        #region 成员管理

        /// <summary>
        /// 创建成员
        /// </summary>
        /// <param name="message">json字符串</param>
        /// ZSH
        /// 20151117
        public WeChatApiResult UserCreate(string strJson)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token={0}", AccessToken);
            string message = WechatPostRequest(url, strJson);
            WeChatApiResult apiResult = GetJsonToModel(message);
            return apiResult;
        }

        /// <summary>
        /// 更新成员
        /// </summary>
        /// <param name="message">json字符串</param>
        /// ZSH
        /// 20151117
        public WeChatApiResult UserUpdate(string strJson)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/update?access_token={0}", AccessToken);
            string message = WechatPostRequest(url, strJson);
            WeChatApiResult apiResult = GetJsonToModel(message);
            return apiResult;
        }

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="userId">成员UserID。对应管理端的帐号</param>
        /// ZSH
        /// 20151117
        public WeChatApiResult UserDelete(string userId)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token={0}&userid={1}", AccessToken, userId);
            string message = "";
            WechatGetRequest(url, out message);
            WeChatApiResult apiResult = GetJsonToModel(message);
            return apiResult;
        }

        /// <summary>
        /// 批量删除成员
        /// </summary>
        /// <param name="message">json字符串</param>
        /// ZSH
        /// 20151117
        public WeChatApiResult UserBatchdelete(string strJson)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/batchdelete?access_token={0}", AccessToken);
            string message = WechatPostRequest(url, strJson);
            WeChatApiResult apiResult = GetJsonToModel(message);
            return apiResult;
        }

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="userId">成员UserID。对应管理端的帐号</param>
        /// ZSH
        /// 20151117
        public WeChatUserInfo UserGet(string userId)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}", AccessToken, userId);
            string message = "";
            WechatGetRequest(url, out message);
            WeChatUserInfo UserInfo = GetJsonToUser(message);
            return UserInfo;
        }

        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="department_id">获取的部门id</param>
        /// <param name="fetch_child">1/0：是否递归获取子部门下面的成员</param>
        /// <param name="status">0获取全部成员，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加，未填写则默认为4</param>
        /// ZSH
        /// 20151117
        public WeChatUserInfo UserSimpleList(string department_id, int fetch_child = 1, int status = 0)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child={2}&status={3}", AccessToken, department_id, fetch_child, status);
            string message = "";
            WechatGetRequest(url, out message);
            WeChatUserInfo UserInfo = GetJsonToUser(message);
            return UserInfo;
        }

        /// <summary>
        /// 获取部门成员(详情)
        /// </summary>
        /// <param name="message">json字符串</param>
        /// ZSH
        /// 20151117
        public WeChatUserInfo UserList(string department_id, int fetch_child = 1, int status = 0)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/list?access_token={0}&department_id={1}&fetch_child={2}&status={3}", AccessToken, department_id, fetch_child, status);
            string message = "";
            WechatGetRequest(url, out message);
            WeChatUserInfo UserInfo = GetJsonToUser(message);
            return UserInfo;
        }

        /// <summary>
        /// 邀请成员关注
        /// </summary>
        /// <param name="strJson">json字符串</param>
        /// ZSH
        /// 20151117
        public WeChatApiResult InviteSend(string strJson)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/invite/send?access_token={0}", AccessToken);
            string message = WechatPostRequest(url, strJson);
            WeChatApiResult apiResult = GetJsonToModel(message);
            return apiResult;
        }

        #endregion

        #region 粉丝及分组

        /// <summary>
        /// 根据粉丝OpenId获得粉丝的详细信息
        /// </summary>
        /// <param name="openId">OpenId</param>
        /// <param name="lang">语言类型：返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语 </param>
        /// <returns>WeChatFans</returns>
        /// Author:fredjiang
        /// Created:2015-12-16
        public WeChatFans UserInfo(string openId, string lang = "zh_CN")
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}", AccessToken, openId, lang);
            string message = "";
            WechatGetRequest(url, out message);
            WeChatFans result = JsonToModel<WeChatFans>(message);
            Remark = url;
            return result;
        }

        /// <summary>
        /// 获取公众账号分组列表信息
        /// </summary>
        /// <returns>反馈状态对象，包括分组列表</returns>
        /// Author:fredjiang
        /// Created:2016-01-06
        public WeChatGroupResult GroupsGet()
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}", AccessToken);
            string message = "";
            WechatGetRequest(url, out message);

            WeChatGroupResult result = JsonDeserialize<WeChatGroupResult>(message);
            Remark = url;
            return result;
        }

        /// <summary>
        /// 创建微信分组
        /// </summary>
        /// <param name="json">分组JSON字符串</param>
        /// <returns>反馈状态对象</returns>
        /// Author:fredjiang
        /// Created:2016-01-06
        public WeChatGroupResult1 GroupsCreate(string json)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}", AccessToken);
            string message = "";
            message = WechatPostRequest(url, json);
            WeChatGroupResult1 result = JsonDeserialize<WeChatGroupResult1>(message);
            Remark = url;
            return result;
        }

        /// <summary>
        /// 查询粉丝所在分组
        /// </summary>
        /// <param name="json">分组JSON字符串</param>
        /// <returns>反馈状态对象</returns>
        /// Author:fredjiang
        /// Created:2016-01-06
        public WeChatApiResult GroupsGetId(string json)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/getid?access_token={0}", AccessToken);
            string message = "";
            message = WechatPostRequest(url, json);
            WeChatApiResult result = JsonDeserialize<WeChatApiResult>(message);
            Remark = url;
            return result;
        }

        /// <summary>
        /// 修改分组信息
        /// </summary>
        /// <param name="json">分组信息JSON字符串</param>
        /// <returns>反馈状态对象</returns>
        /// Author:fredjiang
        /// Created:2016-01-06
        public WeChatApiResult GroupsUpdate(string json)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}", AccessToken);
            string message = "";
            message = WechatPostRequest(url, json);
            WeChatApiResult result = JsonDeserialize<WeChatApiResult>(message);
            Remark = url;
            return result;
        }

        /// <summary>
        /// 移动粉丝分组
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <returns>反馈状态对象</returns>
        /// Author:fredjiang
        /// Created:2016-01-06
        public WeChatApiResult GroupsMembersUpdate(string json)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}", AccessToken);
            string message = "";
            message = WechatPostRequest(url, json);
            WeChatApiResult result = JsonDeserialize<WeChatApiResult>(message);
            Remark = url;
            return result;
        }

        /// <summary>
        /// 批量移动粉丝分组
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <returns>反馈状态对象</returns>
        /// Author:fredjiang
        /// Created:2016-01-06
        public WeChatApiResult GroupsMembersBatchUpdate(string json)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/members/batchupdate?access_token={0}", AccessToken);
            string message = "";
            message = WechatPostRequest(url, json);
            WeChatApiResult result = JsonDeserialize<WeChatApiResult>(message);
            Remark = url;
            return result;
        }

        /// <summary>
        ///删除粉丝分组
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <returns>反馈状态对象</returns>
        /// Author:fredjiang
        /// Created:2016-01-06
        public WeChatApiResult GroupsDelete(string json)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/delete?access_token={0}", AccessToken);
            string message = "";
            message = WechatPostRequest(url, json);
            WeChatApiResult result = JsonDeserialize<WeChatApiResult>(message);
            Remark = url;
            return result;
        }


        #endregion

        #region 部门管理

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="message">json字符串</param>
        /// ZSH 20151216
        public WeChatApiResult DepartmentCreate(string strJson)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token={0}", AccessToken);
            string message = WechatPostRequest(url, strJson);
            WeChatApiResult apiResult = GetJsonToModel(message);
            return apiResult;
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="message">json字符串</param>
        /// ZSH 20151216
        public WeChatApiResult DepartmentUpdate(string strJson)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token={0}", AccessToken);
            string message = WechatPostRequest(url, strJson);
            WeChatApiResult apiResult = GetJsonToModel(message);
            return apiResult;
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="Id">部门id。获取指定部门及其下的子部门</param>
        /// ZSH 20151216
        public WeChatApiResult DepartmentDelete(string id)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/delete?access_token={0}&id={1}", AccessToken, id);
            string message = "";
            WechatGetRequest(url, out message);
            WeChatApiResult apiResult = GetJsonToModel(message);
            return apiResult;
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="Id">部门id。获取指定部门及其下的子部门</param>
        /// ZSH 20151216
        public WeChatUserInfo DepartmentList(string id)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={0}&id={1}", AccessToken, id);
            string message = "";
            WechatGetRequest(url, out message);
            WeChatUserInfo UserInfo = GetJsonToUser(message);

            return UserInfo;
        }

        #endregion

        /// <summary>
        /// 获取关注者列表
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string UserGet_MP(string openId)
        {
            string ret;
            string url;
            if (string.IsNullOrEmpty(openId))
            {
                url = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}", AccessToken);
            }
            else
            {
                url = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}", AccessToken, openId);
            }
            WechatGetRequest(url, out ret);
            return ret;
        }

    }
}
