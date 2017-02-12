using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ebs.commons;
using ebs.dbml;

namespace ebs.MoKuai_Rili
{
    /// <summary>
    /// crudEventList 的摘要说明
    /// </summary>
    public class crudEventList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string act = context.Request.QueryString["act"];
            switch (act)
            {
                case "read":
                    Read(context);
                    break;
                default:
                    break;
            }
        }

        //{"success":"1","result":{"Title":"","Renshu":,"SalesName":,"YishiChangdi":,"Date":,"BeginHour":,"EndHour":,"Source":,"IsConfirmed":,"Jine":"","Yanhuiting":"","Can":"","Mianshu":}}
        private void Read(HttpContext context)
        {
            var jsonEvent = "{{\"success\":\"1\",\"result\":{{\"Title\":\"{0}\",\"Renshu\":\"{1}\",\"SalesName\":\"{2}\",\"YishiChangdi\":\"{3}\",\"Date\":\"{4}\",\"BeginHour\":\"{5}\",\"EndHour\":\"{6}\",\"Source\":\"{7}\",\"IsConfirmed\":\"{8}\",\"Jine\":\"{9}\",\"Yanhuiting\":\"{10}\",\"Can\":\"{11}\",\"Miaoshu\":\"{12}\"}}}}";
            int ID = Convert.ToInt32(context.Request.QueryString["ID"]);
            using (ebsDBData db = new ebsDBData())
            {
                if (db.tbRili.Any(q=>q.ID == ID))
                {
                    var oneEvent = db.tbRili.First(q => q.ID == ID);
                    jsonEvent = string.Format(jsonEvent, oneEvent.Title, oneEvent.Renshu, oneEvent.SalesName, oneEvent.yishiChangdi, oneEvent.StartDate.ToString("yyyy-MM-dd"), oneEvent.StartDate.ToString("hh"), oneEvent.EndDate.HasValue?oneEvent.EndDate.Value.ToString("hh"):"", oneEvent.Source, oneEvent.IsConfirmed, oneEvent.Jine, oneEvent.yanhuiting,oneEvent.Can, oneEvent.Miaoshu);
                    context.Response.Write(jsonEvent);
                }
                else
                {
                    context.Response.Write("{'success':'0'}");
                }
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}