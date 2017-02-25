using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using AOWEN.WeChat.Core.Model;
using System.Net;
using System.IO;

namespace AOWEN.WeChat.Core
{
    public class WeChatApiMaterial : WeChatApiBase
    {

        /// <summary>
        /// 上传临时素材文件
        /// </summary>
        /// <param name="fname">文件</param>
        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video），普通文件(file)</param>
        /// <returns></returns>
        public WeChatApiResult MediaUpload(string fname, string type)
        {
            string url = "";
            if (WeChatAccount.AccounType == 3)
            {
                url = "https://qyapi.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}";
            }
            else
            {
                url = "https://api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}";
            }
            string requestUrl = String.Format(url, AccessToken, type);
            System.Net.WebClient webclient = new System.Net.WebClient();
            byte[] response = webclient.UploadFile(new Uri(requestUrl), fname);
            string reslut = Encoding.UTF8.GetString(response);
            WeChatApiResult apiResult = GetJsonToModel(reslut);
            return apiResult;
        } /// <summary>
        /// 上传临时素材文件
        /// </summary>
        /// <param name="fname">文件</param>
        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video），普通文件(file)</param>
        /// <returns></returns>
        public WeChatApiResult MediaUploadMP(string fname, string type)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}";
            string requestUrl = String.Format(url, AccessToken, type);
            System.Net.WebClient webclient = new System.Net.WebClient();
            byte[] response = webclient.UploadFile(new Uri(requestUrl), fname);
            string reslut = Encoding.UTF8.GetString(response);
            WeChatApiResult apiResult = GetJsonToModel(reslut);
            return apiResult;
        }

        /// <summary>
        /// 上传永久图文素材(图文)(企业，服务)
        /// </summary>
        /// <param name="json">参与json字符串</param>
        /// <returns>接口调用返回的json</returns>
        /// Author:fredjiang
        /// Created:2015-05-16
        public WeChatApiResult AddMpnews(string json)
        {
            string urlTemplate = "";
            if (WeChatAccount.AccounType == 3)
            {
                urlTemplate = "https://qyapi.weixin.qq.com/cgi-bin/material/add_mpnews?access_token={0}";
            }
            else
            {
                urlTemplate = "https://api.weixin.qq.com/cgi-bin/material/add_news?access_token={0}";
            }
            string requestUrl = String.Format(urlTemplate, AccessToken);

            byte[] form_data = Encoding.UTF8.GetBytes(json);

            WebRequest req = WebRequest.Create(requestUrl);
            req.Method = "POST";
            req.ContentType = "application/json;charset=UTF-8";
            req.ContentLength = form_data.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(form_data, 0, form_data.Length);

            stream.Close();
            stream.Dispose();

            WebResponse res = req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream());
            string reslut = sr.ReadToEnd();
            WeChatApiResult apiResult = GetJsonToModel(reslut);
            return apiResult;
        }

        // <summary>
        /// 上传其他类型永久素材，非图文(企业，服务)
        /// </summary>
        /// <param name="fname">文件</param>
        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video），普通文件(file)</param>
        /// <returns></returns>
        /// Author：fredjiang
        /// Created:2015-05-14
        /// update:zhangsonghe 20160122
        public WeChatApiResult AddMaterial(string filename, string type, string agentid = "0", string title = "", string introduction = "")
        {
            //文件
            string contenttype = "image/jpeg";
            string accesstoken = AccessToken;
            FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fileStream);
            byte[] buffer = br.ReadBytes(Convert.ToInt32(fileStream.Length));
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            //请求
            string requestUrl = "";
            if (WeChatAccount.AccounType == 3)
            {
                string url = "https://qyapi.weixin.qq.com/cgi-bin/material/add_material?agentid={0}&type={1}&access_token={2}";
                requestUrl = String.Format(url, agentid, type, accesstoken);
            }
            else
            {
                string url = "https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={0}&type={1}";
                requestUrl = String.Format(url, accesstoken, type);
            }
            WebRequest req = WebRequest.Create(requestUrl);

            req.Method = "POST";

            //req.ContentType = "multipart/form-data; boundary=" + boundary;
            req.ContentType = "multipart/form-data";

            //组织表单数据
            StringBuilder sb = new StringBuilder();
            sb.Append("--" + boundary + "\r\n");
            sb.Append("Content-Disposition: form-data; name=\"media\"; filename=\"" + filename + "\"; filelength=\"" + fileStream.Length + "\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: " + contenttype);
            sb.Append("\r\n\r\n");
            string head = sb.ToString();
            //---------------视频----
            string data1 = "";
            if (type == "video")
            {
                sb = new StringBuilder();
                sb.Append("--" + boundary + "\r\n");
                sb.Append("Content-Disposition: form-data; name=\"description\"\r\n\r\n{\"title\":\"这是标题\", \"introduction\":\"这是描述\"}");
                data1 = sb.ToString();
            }
            byte[] form_data1 = Encoding.UTF8.GetBytes(data1);

            //----------------------------
            byte[] form_data = Encoding.UTF8.GetBytes(head);

            //结尾
            byte[] foot_data = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            //post总长度
            long length = form_data.Length + form_data1.Length + fileStream.Length + foot_data.Length;

            req.ContentLength = length;

            Stream requestStream = req.GetRequestStream();
            //这里要注意一下发送顺序，先发送form_data > buffer > foot_data
            //发送表单参数
            requestStream.Write(form_data, 0, form_data.Length);
            //发送文件内容
            requestStream.Write(buffer, 0, buffer.Length);

            if (type == "video")//在上传视频素材时需要POST另一个表单，id为description，包含素材的描述信息，内容格式为JSON
                requestStream.Write(form_data1, 0, form_data1.Length);

            //结尾
            requestStream.Write(foot_data, 0, foot_data.Length);


            requestStream.Close();
            fileStream.Close();
            fileStream.Dispose();
            br.Close();
            br.Dispose();
            //响应
            WebResponse pos = req.GetResponse();
            StreamReader sr = new StreamReader(pos.GetResponseStream(), Encoding.UTF8);
            string reslut = sr.ReadToEnd().Trim();
            sr.Close();
            sr.Dispose();
            if (pos != null)
            {
                pos.Close();
                pos = null;
            }
            if (req != null)
            {
                req = null;
            }

            WeChatApiResult apiResult = GetJsonToModel(reslut);
            return apiResult;
        }

        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="media_id">素材资源标识ID</param>
        /// <param name="agentid">企业应用的id，整型。可在应用的设置页面查看</param>
        public WeChatApiResult MaterialDel(string media_id)
        {
            string url = "https://qyapi.weixin.qq.com/cgi-bin/material/del?access_token={0}&agentid={1}&media_id={2}";
            string requestUrl = String.Format(url, AccessToken, AgentId, media_id);
            string message = "";
            WechatGetRequest(requestUrl, out message);
            WeChatApiResult apiResult = GetJsonToModel(message);
            return apiResult;
        }

        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="media_id">素材资源标识ID</param>
        /// <param name="agentid">企业应用的id，整型。可在应用的设置页面查看</param>
        public WeChatApiBaseResult MaterialDelMP(string media_id)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/material/del_material?access_token={0}";
            string requestUrl = String.Format(url, AccessToken);
            string reqStr = "{\"media_id\":\"" + media_id + "\"}";
            string message = WechatPostRequest(url, reqStr);
            WeChatApiBaseResult apiResult = JsonToModel<WeChatApiBaseResult>(message);
            return apiResult;
        }

        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="message">json字符串</param>
        public WeChatApiResult UpdateMpnews(string strJson)
        {
            string url = "";
            if (WeChatAccount.AccounType == 3)
            {
                url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/material/update_mpnews?access_token={0}", AccessToken);
            }
            else
            {
                url = string.Format("https://api.weixin.qq.com/cgi-bin/material/update_news?access_token={0}", AccessToken);
            }
            string message = WechatPostRequest(url, strJson);
            WeChatApiResult apiResult = GetJsonToModel(message);
            return apiResult;
        }

        /// <summary>
        /// 下载多媒体文件；临时素材(企业，服务号)
        /// </summary>
        ///<param name="filePath">保存下载文件的目录</param>
        ///<param name="mediaId">媒体编号</param>
        ///<param name="msgType">素材类型</param>
        ///<param name="outFilePath">下载后的物理文件路径</param>
        /// <returns>接口反馈对象</returns>
        /// Author：gs
        /// Date：20140710
        public WeChatApiResult MediaGet(string mediaId, string msgType, string filePath, out string outFilePath)
        {
            string msg = "";
            outFilePath = "";
            try
            {
                string urlTemplate = "";
                if (WeChatAccount.AccounType == 3)
                {
                    urlTemplate = "https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}";
                }
                else
                {
                    urlTemplate = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}";
                }

                string requestUrl = String.Format(urlTemplate, AccessToken, mediaId);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                if (msgType == "image")
                {
                    outFilePath = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
                }
                else if (msgType == "voice")
                {
                    outFilePath = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".amr";
                }
                else if (msgType == "video" || msgType == "shortvideo")
                {
                    outFilePath = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".mp4";
                }

                System.Net.WebClient webClient = new System.Net.WebClient();
                webClient.DownloadFile(new Uri(requestUrl), filePath + outFilePath);
                msg = "{\"errcode\":0,\"errmsg\":\"ok\"}";
            }
            catch (Exception ex)
            {
                msg = "{\"errcode\":2,\"errmsg\":\"" + ex.Message + "\"}";
            }

            return JsonToModel<WeChatApiResult>(msg);
        }

        public string BatchGetMaterial(string json)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}", AccessToken);
            string message = WechatPostRequest(url, json);
            return message;
        }
    }
}
