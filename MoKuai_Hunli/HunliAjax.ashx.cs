using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ebs.dbml;
using System.Text;
using ebs.commons;
namespace ebs.MoKuai_Hunli
{
    /// <summary>
    /// HunliAjax 的摘要说明
    /// </summary>
    public class HunliAjax : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request["method"])
            {
                case "getCaijinDanjia":
                    getCaijinDanjia(context);
                    break;
                case "getOptionsForHYservices":
                    getOptionsForHYservices(context);
                    break;
                case "getOptionsForHQservices":
                    getOptionsForHQservices(context);
                    break;
                case "getVenueDate":
                    getVenueDate(context);
                    break;
                case "getPaymentByType":
                    getPaymentByType(context);
                    break;
                case "getRevision":
                    getRevision(context);
                    break;
                case "getAuditNeed":
                    getAuditNeed(context);
                    break;
                case "getPriceByDateLevel":
                    getPriceByDateLevel(context);
                    break;
                case "checkHetongBianhao":
                    checkHetongBianhao(context);
                    break;
                default:
                    break;
            }
        }

        private void checkHetongBianhao(HttpContext context)
        {
            string res = "{{\"valid\":\"{0}\"}}";
            using (ebsDBData db = new ebsDBData())
            {
                string hetongbianhao = context.Request.QueryString["ctl00$MainContent$tbHetongBianhao"];
                int ID = 0;
                int.TryParse(context.Request.QueryString["id"], out ID);
                if (db.WeddingOrders.Any(q => q.HetongID == hetongbianhao && q.ID != ID && q.Zhuangtai != "已覆盖"))
                {

                    res = string.Format(res, "false");
                }
                else res = string.Format(res, "true");
                context.Response.Write(res);
            }
        }

        private void getPriceByDateLevel(HttpContext context)
        {
            
            try
            {
                DateTime txtDate = Convert.ToDateTime(context.Request["Date"]);
                string venues = context.Request["Venue"].ToString().Trim();
                List<string> venue = new List<string>();
                if (venues.Contains(","))
                {
                    var v = venues.Split(',');
                    foreach (var item in v)
                    {
                        if (!venue.Contains(item))
                        {
                            venue.Add(item);
                        }
                       
                    }
                }
                else if (venues.Contains(" "))
                {
                    var v = venues.Split(' ');
                    foreach (var item in v)
                    {
                        if (!venue.Contains(item))
                        {
                            venue.Add(item);
                        }
                    }
                }
                else { venue.Add(venues); }
                //string[] venue = context.Request["Venue"].ToString().Split(',');
                string price = "0";
                string DateLevel = "D";
                using (ebsDBData db = new ebsDBData())
                {
                    //if (db.tbSysStandardWeddingDateLevel.Any(q => q.Date.Date == txtDate.Date))
                    //{
                    //    DateLevel = db.tbSysStandardWeddingDateLevel.First(q => q.Date.Date == txtDate.Date).DateLevel;
                    //}
                    //else DateLevel = "D";
                    //var p = db.tbSysStandardWeddingPrice.Where(q => venue.Contains(q.VenueName) && q.DateLevel == DateLevel).OrderByDescending(q => q.StandardPrice).FirstOrDefault();
                    //string Levels = p.DateLevel;
                    //string VenueMax = p.VenueName;
                    //string Cate = "淡季";
                    //price = p.StandardPrice.ToString();
                    //switch (Levels)
                    //{
                    //    case "A":
                    //        Cate = "黄道吉日+节假日+旺季";
                    //        break;
                    //    case "B":
                    //        Cate = "节假日+旺季";
                    //        break;
                    //    case "C":
                    //        Cate = "节假日";
                    //        break;
                    //    default:
                    //        break;
                    //}
                   


                }

                string result = string.Format("{{venue:'{0}',level:'{1}',cate:'{2}',price:'{3}'}}", "", "", "", "9999999999");
                //{venue:'Ating',Level:'A',Date:'黄道吉日+节假日+旺季'}
                context.Response.Write(result);
            }
            catch (System.Exception ex)
            {
                context.Response.Write("{error:'1'}");
            }
           
        }

        private void getAuditNeed(HttpContext context)
        {
            try
            {
                double CurrentPrice = Convert.ToDouble(context.Request["currentPrice"]);
                double standardPrice = Convert.ToDouble(context.Request["standardPrice"]);
                StringBuilder sb = new StringBuilder("{");
                using (ebsDBData db = new ebsDBData())
                {

                    sb.AppendFormat("StandardPrice:'{0}',", standardPrice.ToString("N"));
                    sb.Append("AuditNeed:[");
                    var auditNeeds = db.tbSysAuditConfig.OrderBy(q => q.priority).ToList();
                    int i = 1;
                    foreach (var item in auditNeeds)
                    {
                        if (item.ConditionMin * standardPrice <= CurrentPrice && item.ConditionMax * standardPrice > CurrentPrice)
                        {
                            if (item.NeedRole == "文员")
                            {
                                continue;
                            }
                            sb.Append("{");
                            sb.AppendFormat("Role:'{0}',Min:'{1}',Max:'{2}'", item.NeedRole, (item.ConditionMin * standardPrice).ToString("N"), (item.ConditionMax * standardPrice).ToString("N"));
                            sb.Append("}");
                            if (item.NeedRole == "不可提交")
                            {
                                break;
                            }
                            if (i != auditNeeds.Count)
                            {
                                sb.Append(",");
                            }
                        }
                    }
                    sb.Append("]}");
                    //{StandardPrice:'',AuditNeed:[{Role:'f',Min:'1',Max:'2'},{Role:'f',Min:'1',Max:'2'}]}
                }
                context.Response.Write(sb.ToString());
            }
            catch (System.Exception ex)
            {
                context.Response.Write("error");
            }
           
        }

        private void getRevision(HttpContext context)
        {
            StringBuilder sb = new StringBuilder("{ Revs: [");
            int ID = Convert.ToInt32(context.Request["ID"]);
            if (ID !=0)
            {
                using (ebsDBData db = new ebsDBData())
                {
                    if (!db.tbWeddingOrdersRevision.Any(q=>q.OrderID == ID))
                    {
                        context.Response.Write("");
                        return;
                    }
                    int ContractID = db.tbWeddingOrdersRevision.First(q => q.OrderID == ID).ContractID;
                    if (ContractID != 0)
                    {
                        //{Revs:[{id:'1',orderName:'1991-11-11'},{id:'1',orderName:'1992-12-01'}]}
                        var res = db.tbWeddingOrdersRevision.Where(q => q.ContractID == ContractID);
                        int i = 1;
                        foreach (var item in res)
                        {
                            sb.Append("{");
                            sb.AppendFormat("id:'{0}',orderName:'{1}'",item.OrderID.ToString(), item.OrderName);
                            sb.Append("}");
                            if (i != res.Count())
                            {
                                sb.Append(",");
                            }
                            i++;
                         
                        }
                        sb.Append("]}");
                    }
                    context.Response.Write(sb.ToString());
                }
            }
        }

        private void getPaymentByType(HttpContext context)
        {
            string payType = context.Request["type"];
            int id = Convert.ToInt32(context.Request["id"].ToString());
            if (id!=0)
            {
                ConvertStringsInDB.paymentByIDandType obj = new ConvertStringsInDB.paymentByIDandType(id, payType);
                if (obj != null)
                {
                    StringBuilder sb = new StringBuilder("{");
                    sb.AppendFormat("payPercentHY:'{0}',payAmountHY:'{1}',payPercentHQ:'{2}',payAmountHQ:'{3}'", obj.HunyanBai, obj.HunyanNeedPay, obj.HunqinBai, obj.HunqinNeedPay);
                    sb.Append("}");
                    context.Response.Write(sb.ToString());
                } 
            }
            context.Response.Write("");
        }

        

        private void getVenueDate(HttpContext context)
        {
            string res = "";
            int cusID = Convert.ToInt32(context.Request["selectItem"].ToString());
            using (ebsDBData db = new ebsDBData())
            {
                if (db.Customers.Any(q=>q.ID == cusID))
                {
                    var s = db.Customers.First(q => q.ID == cusID);
                    res = string.Format("riqi:'{0}',source:'{1}'", s.EventDate, s.Source);
                }
            }
            context.Response.Write("{"+res+"}");
        }

        private void getOptionsForHYservices(HttpContext context)
        {
            List<string> result = new List<string>();
            string items = "";
            using (ebsDBData db = new ebsDBData())
            {
                result = db.tbSysServicesHY.Select(q=>q.ServiceName).ToList();
            }
            foreach (var item in result)
            {
                items += string.Format("'{0}',",item);
            }
            if (items != "")
            {
                items = items.Substring(0, items.Length - 1);
            }
            context.Response.Write("{Options:["+items+"]}");
          
        }

        private void getOptionsForHQservices(HttpContext context)
        {
            List<string> result = new List<string>();
            string items = "";
            using (ebsDBData db = new ebsDBData())
            {
                result = db.tbSysServicesHQ.Select(q => q.ServiceName).ToList();
            }
            foreach (var item in result)
            {
                items += string.Format("'{0}',", item);
            }
            if (items != "")
            {
                items = items.Substring(0, items.Length - 1);
            }
            context.Response.Write("{Options:[" + items + "]}");

        }

        private void getCaijinDanjia(HttpContext context)
        {
            string result ="";
            var selectMenu = context.Request["selectItem"];
            if (selectMenu !="")
            {
                using (ebsDBData db = new ebsDBData())
                {
                    result = db.tbSysMenu.First(q => q.MenuName == selectMenu).MenuPrice.ToString();
                }
            }
            context.Response.Write(result);

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