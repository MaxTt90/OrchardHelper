using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOWEN.WeChat.Core.Model;

namespace AOWEN.WeChat.Core
{
    public class WeChatApiMenu : WeChatApiBase
    {

        private string _type = "";
        /// <summary>
        /// 菜单类型
        /// </summary>
        /// <param name="type">QY:企业号应用菜单，MP：公众号菜单</param>
        public WeChatApiMenu(string type)
        {
            _type = type;
        }

        #region past
        ///// <summary>
        ///// 创建企业号应用菜单
        ///// </summary>
        ///// <param name="strJson">json字符串</param>
        ///// ZSH
        ///// 20151116
        //public WeChatApiResult MenuCreateForQY(string strJson)
        //{
        //    string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/menu/create?access_token={0}&agentid={1}", AccessToken, AgentId);
        //    string message = WechatPostRequest(url, strJson);
        //    WeChatApiResult apiResult = GetJsonToModel(message);
        //    return apiResult;
        //}
        ///// <summary>
        ///// 创建服务号订阅号菜单
        ///// </summary>
        ///// <param name="strJson"></param>
        ///// <returns></returns>
        //public WeChatApiResult MenuCreateForMP(string strJson)
        //{
        //    string url = string.Format(" https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", AccessToken);
        //    string message = WechatPostRequest(url, strJson);
        //    WeChatApiResult apiResult = GetJsonToModel(message);
        //    return apiResult;
        //} 
        #endregion
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="strJson">菜单json</param>
        /// <returns></returns>
        public WeChatApiResult CreateMenu(string strJson)
        {
            string url = "";
            if (_type =="QY")
            {
                 url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/menu/create?access_token={0}&agentid={1}", AccessToken, AgentId);

            }
            else if (_type == "MP")
            {
                url = string.Format(" https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", AccessToken);
            }
            string message = WechatPostRequest(url, strJson);
            WeChatApiResult apiResult = GetJsonToModel(message);
            return apiResult;
        }
    }
}
