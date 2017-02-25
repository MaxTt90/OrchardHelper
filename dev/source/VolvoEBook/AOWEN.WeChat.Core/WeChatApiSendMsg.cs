using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOWEN.WeChat.Core.Model;

namespace AOWEN.WeChat.Core
{
    public class WeChatApiSendMsg : WeChatApiBase
    {
        /// <summary>
        /// 微信发送客服消息接口
        /// </summary>
        /// <param name="json">请求的json字符串</param>
        /// <returns>接口反馈对象</returns>
        /// Author:fredjiang
        /// Created:2015-12-20
        public WeChatApiResult customSend(string json)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", AccessToken);
            string message = WechatPostRequest(url, json);
            WeChatApiResult result = JsonToModel<WeChatApiResult>(message);
            return result;
        }


        /// <summary>
        /// 预览接口【订阅号与服务号认证后均可用】
        /// </summary>
        /// <param name="json">请求的json字符串</param>
        /// <returns>接口反馈对象</returns>
        /// zhangsonghe 20160118
        public WeChatApiResult MessageMassPreview(string json)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token={0}", AccessToken);
            string message = WechatPostRequest(url, json);
            WeChatApiResult result = JsonToModel<WeChatApiResult>(message);
            return result;
        }

        /// <summary>
        /// 群发
        /// </summary>
        /// <param name="json">请求的json字符串</param>
        /// <returns>接口反馈对象</returns>
        /// zhangsonghe 20160118
        public string SendAll(string json)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}", AccessToken);
            string message = WechatPostRequest(url, json);
            return message;
        }

        /// <summary>
        /// 群发
        /// </summary>
        /// <param name="json">请求的json字符串</param>
        /// <returns>接口反馈对象</returns>
        /// zhangsonghe 20160118
        public string MassSend(string json)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}", AccessToken);
            string message = WechatPostRequest(url, json);
            return message;
        }
    }
}
