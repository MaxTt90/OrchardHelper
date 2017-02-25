using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AOWEN.Model;
using AOWEN.BLL;
using WeDo.Log;

namespace AOWEN.WebCampaign.Controllers
{
    public class CampaignController : Controller
    {
        //
        // GET: /Campaign/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SZC60()
        {
            //记录访问日志
            int sourceId=0;
            int.TryParse((Request.QueryString["s"] ?? "0"),out sourceId);
            WeDo.Log.LogTrackMan.AddLog("2015深圳XC90活动页面访问记录", sourceId, 0, 0, "", "2015深圳XC90活动", 0, 0, 0, "", "", "");
            return View();
        }

        [HttpPost]
        public ActionResult SZC60(CampaignApply model)
        {
            string logName = "2015深圳XC90活动";
            try
            {
                model.IP = Request.UserHostAddress;
                model.ApplyUrl = Request.Url.AbsoluteUri;
                model.Source = Request.QueryString["s"] ?? "";
                CampaignApplyMan caMan = new CampaignApplyMan();
                if (caMan.ExistsByMobile(model.CampaignName, model.Mobile))
                {
                    ViewBag.code = "2";
                    ViewBag.msg = "手机号已注册";
                }
                else
                {
                    caMan.Add(model);
                    ViewBag.code = "1";
                    ViewBag.msg = "申请成功";
                }
            }
            catch (Exception ex)
            {
                LogExceptionMan.AddLog(logName, WeDo.Log.Model.EnumListLog.LogLevel.ERROR, ex);
                ViewBag.code = "2";
                ViewBag.msg = "操作失败，请稍后再试！";
            }
            return View();
        }
    }
}