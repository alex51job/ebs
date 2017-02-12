using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using ebs.dbml;

namespace ebs.MoKuai_Rili
{

    public class events : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            List<tbRili> RL = new List<tbRili>();
            DateTime Dtfrom = new DateTime(1970, 1, 1).ToLocalTime().AddMilliseconds(long.Parse(context.Request["from"]));
            DateTime Dtto = new DateTime(1970, 1, 1).ToLocalTime().AddDays(1).AddMilliseconds(long.Parse(context.Request["to"]));
            using (ebsDBData db = new ebsDBData())
            {
                RL = (from q in db.tbRili
                      where ((q.StartDate >= Dtfrom && q.StartDate <= Dtto)
                      ||
                      (q.EndDate.HasValue ? (q.EndDate.Value >= Dtfrom && q.EndDate.Value <= Dtto) : false))
                      orderby q.StartDate, q.Source
                      select q
                     ).ToList();

            }
            string events = "";
            foreach (tbRili item in RL)
            {
                events += addItem(item) + ',';
            }
            if (events.Length > 0)
            {
                events = events.Substring(0, events.Length - 1);
            }
            else
            {
                context.Response.Write("");

            }


            string JsonTxt = "{\"success\":1,\"result\":[";
            JsonTxt = JsonTxt + events + "]}";

            context.Response.ContentType = "text/plain";
            context.Response.Write(JsonTxt);


        }

        private string addItem(tbRili item)
        {
            DateTime dt = new DateTime(1970, 1, 1).ToLocalTime();
            string start = (item.StartDate - dt).TotalMilliseconds.ToString();
            string end = "";
            if (item.EndDate != null)
            {
                end = (item.EndDate.Value - dt).TotalMilliseconds.ToString();
            }
            else
            {
                end = (item.StartDate.AddHours(12) - dt).TotalMilliseconds.ToString();
            }

            string className = item.IsConfirmed == "是" ? "event-success" : "event-warning";
            string url = "#";
            string venue = ""+ item.yishiChangdi + "|" + item.yanhuiting;
            string title = item.Title + " " + item.Renshu + " " + item.SalesName + " "+item.Source+" " + venue + " " + item.Miaoshu;// +" <i class='fa fa-glass'></i>";
            if (item.Can == "是")
            {
                title = " <i class='fa fa-glass'></i> " + title;
            }
            if (item.EndDate != null)
            {
                title = item.StartDate.ToString("HH:mm") + " - " + item.EndDate.Value.ToString("HH:mm") + " " + title;

            }
            else
            {
                title = item.StartDate.ToString("HH:mm") + " - " + item.StartDate.AddHours(12).ToString("HH:mm") + " " + title;
            }

            string id = item.ID.ToString();
            StringBuilder ItemStr = new StringBuilder();
            string ItemFormat = "\"id\":\"{0}\",\"title\":\"{1}\",\"url\":\"{2}\",\"class\":\"{3}\",\"start\":\"{4}\",\"end\":\"{5}\"";
            ItemStr.AppendFormat(ItemFormat, id, title, url, className, start, end);
            return "{" + ItemStr + "}";

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