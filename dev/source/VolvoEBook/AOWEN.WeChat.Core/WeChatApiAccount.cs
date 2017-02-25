using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AOWEN.WeChat.Core.Model;

namespace AOWEN.WeChat.Core
{
    public class WeChatApiAccount : WeChatApiBase
    {
        /// <summary>
        /// 设置企业号应用
        /// </summary>
        /// <param name="json">应用信息</param>
        /// <returns></returns>
        /// author:duanxianghai 2015-12-8 13:42:52
        public WeChatApiResult SetAgent(string json)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/agent/set?access_token=" + AccessToken;
            byte[] formData = Encoding.UTF8.GetBytes(json);

            WebRequest req = WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json;charset=UTF-8";
            req.ContentLength = formData.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(formData, 0, formData.Length);

            stream.Close();
            stream.Dispose();

            WebResponse res = req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream());
            string reslut = sr.ReadToEnd();
            WeChatApiResult apiResult = GetJsonToModel(reslut);
            return apiResult;
        }

        /// <summary>
        /// 生成自定义二维码
        /// </summary>
        /// <param name="sceneId">场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）</param>
        /// <param name="expire">该二维码有效时间，以秒为单位。 最大不超过2592000（即30天），此字段如果不填，则默认有效期为30秒。</param>
        /// <param name="sceneStr">场景值ID（字符串形式的ID），字符串类型，长度限制为1到64，仅永久二维码支持此字段 </param>
        /// <param name="type">二维码类型，QR_SCENE=1为临时,QR_LIMIT_SCENE=2为永久,QR_LIMIT_STR_SCENE=3为永久的字符串参数值 </param>
        /// <returns>接口反馈信息对象</returns>
        /// Author:fredjiang
        /// Created:2015-12-19
        public WeChatQRcode QRCodeCreate(int sceneId, int expire = 2592000, int type = 1, string sceneStr = "")
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}", AccessToken);
            string reqStr = "";
            if (type == 1)
            {
                reqStr = string.Format("{{\"expire_seconds\": {0}, \"action_name\": \"QR_SCENE\", \"action_info\": {{\"scene\": {{\"scene_id\": {1}}}}}}}", expire, sceneId);
            }
            else if (type == 2)
            {
                reqStr = string.Format("{{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\": {{\"scene\": {{\"scene_id\": {0}}}}}}}", sceneId);

            }
            else if (type == 3)
            {
                reqStr = string.Format("{{\"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\": {{\"scene\": {{\"scene_str\": \"{0}\"}}}}}}", sceneStr);
            }

            string reJson = WechatPostRequest(url, reqStr);
            WeChatQRcode result = JsonToModel<WeChatQRcode>(reJson);
            return result;
        }



    }
}
