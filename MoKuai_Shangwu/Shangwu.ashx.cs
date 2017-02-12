using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ebs.dbml;
using ebs.commons;

namespace ebs.MoKuai_Shangwu
{
    /// <summary>
    /// Shangwu1 的摘要说明
    /// </summary>
    public class Shangwu1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                switch (context.Request["method"])
                {
                    case "checkHetongBianhao":
                        checkHetongBianhao(context);
                        break;
                    case "getCustomerInfo":
                        getCustomerInfo(context);
                        break;
                    case "save":
                        savePayment(context);
                        break;
                    case "submitPayment":
                        submitPayment(context);
                        break;
                    case "deletePayment":
                        deletePayment(context);
                        break;
                    case "saveRefund":
                        saveRefund(context);
                        break;
                    case "submitRefund":
                        submitRefund(context);
                        break;
                    case "deleteRefund":
                        deleteRefund(context);
                        break;
                    case "pass":
                        pass(context);
                        break;
                    case "redrew":
                        redrew(context);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                
                string s = "{result:'error',message:\""+ex.Message.Replace("\r\n", "\\r\\n").ToString()+"\"}";
                context.Response.Write(s);
            }
           
        }

        private void redrew(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request["payID"]);
            using (ebsDBData db = new ebsDBData())
            {
                var wp = db.BussinessPayment.First(q => q.ID == id);
                wp.Zhuangtai = "可编辑";
                db.SubmitChanges();
            }
            context.Response.Write("{result:'已退回'}");
        }

        private void pass(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request["payID"]);
            string finishable = "0";
            using (ebsDBData db = new ebsDBData())
            {
                var wp = db.BussinessPayment.First(q => q.ID == id);
                wp.Zhuangtai = "审批完成";
                db.SubmitChanges();
                //是否可完成订单
                int orderid = wp.OrderID;
                var r = new ConvertStringsInDB.JineByID_Bussiness(orderid);
                double total = Convert.ToDouble(r.Zongjine);
                double total_yingfu = Convert.ToDouble(r.yifujineFinish);
                string auditStatus = r.ZhuangtaiAudit;
                if (total <= total_yingfu && auditStatus == "审批完成") finishable = "1";
            }
            context.Response.Write("{result:'审批完成',finish:"+finishable+"}");
        }

        private void saveRefund(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request["id"]);
            int OrderID = Convert.ToInt32(context.Request["orderID"]);
            string resultAct = "";
            using (ebsDBData db = new ebsDBData())
            {
                if (id == 0)
                {
                    BussinessPayment wp = new BussinessPayment();
                    wp.OrderID = OrderID;
                    wp.PayType = context.Request["PayMethod"];
                    wp.PayDate = Convert.ToDateTime(context.Request["PayDate"]);
                    wp.PayNo ="NA";
                    wp.PayAmount = Convert.ToDouble(context.Request["PayAmount"]);
                    wp.Zhuangtai = "可编辑";
                    db.BussinessPayment.InsertOnSubmit(wp);
                    db.SubmitChanges();
                    id = wp.ID;
                    resultAct = "保存";
                }
                else
                {
                    BussinessPayment wp = db.BussinessPayment.First(q => q.ID == id);
                    wp.PayType = context.Request["PayMethod"];
                    wp.PayDate = Convert.ToDateTime(context.Request["PayDate"]);
                    wp.PayNo = "NA";
                    wp.PayAmount = Convert.ToDouble(context.Request["PayAmount"]);
                    wp.Zhuangtai = "可编辑";
                    resultAct = "更新";
                    db.SubmitChanges();
                }
                string s = "{result:'" + resultAct + "成功',id:'" + id + "'}";
                context.Response.Write(s);
            }
        }

        private void submitRefund(HttpContext context)
        {

            int id = Convert.ToInt32(context.Request["id"]);
            int OrderID = Convert.ToInt32(context.Request["orderID"]);
            string resultAct = "";
            using (ebsDBData db = new ebsDBData())
            {
                if (id == 0)
                {
                    BussinessPayment wp = new BussinessPayment();
                    wp.OrderID = OrderID;
                    wp.PayType = context.Request["PayMethod"];
                    wp.PayDate = Convert.ToDateTime(context.Request["PayDate"]);
                    wp.PayNo = "NA";
                    wp.PayAmount = Convert.ToDouble(context.Request["PayAmount"]);
                    wp.Zhuangtai = "审批中";
                    db.BussinessPayment.InsertOnSubmit(wp);
                    db.SubmitChanges();
                    id = wp.ID;
                    resultAct = "保存并提交";
                }
                else
                {
                    BussinessPayment wp = db.BussinessPayment.First(q => q.ID == id);
                    wp.PayType = context.Request["PayMethod"];
                    wp.PayDate = Convert.ToDateTime(context.Request["PayDate"]);
                    wp.PayNo = "NA";
                    wp.PayAmount = Convert.ToDouble(context.Request["PayAmount"]);
                    wp.Zhuangtai = "审批中";
                    resultAct = "更新并提交";
                    db.SubmitChanges();
                }
                string s = "{result:'" + resultAct + "成功',id:'" + id + "'}";
                context.Response.Write(s);
            }
        }

        private void deleteRefund(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request["payID"]);
            using (ebsDBData db = new ebsDBData())
            {
                var wp = db.BussinessPayment.FirstOrDefault(q => q.ID == id);
                db.BussinessPayment.DeleteOnSubmit(wp);
                db.SubmitChanges();
            }
            context.Response.Write("{result:'删除成功'}");
        }

        private void deletePayment(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request["payID"]);
            using (ebsDBData db = new ebsDBData())
            {
                var wp = db.BussinessPayment.FirstOrDefault(q => q.ID == id);
                db.BussinessPayment.DeleteOnSubmit(wp);
                db.SubmitChanges();
            }
            context.Response.Write("{result:'删除成功'}");
        }

        private void submitPayment(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request["id"]);
            int OrderID = Convert.ToInt32(context.Request["orderID"]);
            string resultAct = "";
            using (ebsDBData db = new ebsDBData())
            {
                if (id == 0)
                {
                    BussinessPayment wp = new BussinessPayment();
                    wp.OrderID = OrderID;
                    wp.PayType = context.Request["PayMethod"];
                    wp.PayDate = Convert.ToDateTime(context.Request["PayDate"]);
                    wp.PayNo = context.Request["PayOrder"];
                    wp.PayAmount = Convert.ToDouble(context.Request["PayAmount"]);
                    wp.Zhuangtai = "审批中";
                    db.BussinessPayment.InsertOnSubmit(wp);
                    db.SubmitChanges();
                    id = wp.ID;
                    resultAct = "保存并提交";
                }
                else
                {
                    BussinessPayment wp = db.BussinessPayment.First(q => q.ID == id);
                    wp.PayType = context.Request["PayMethod"];
                    wp.PayDate = Convert.ToDateTime(context.Request["PayDate"]);
                    wp.PayNo = context.Request["PayOrder"];
                    wp.PayAmount = Convert.ToDouble(context.Request["PayAmount"]);
                    wp.Zhuangtai = "审批中";
                    resultAct = "更新并提交";
                    db.SubmitChanges();
                }
                string s = "{result:'" + resultAct + "成功',id:'" + id + "'}";
                context.Response.Write(s);
            }
        }

        private void savePayment(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request["id"]);
            int OrderID = Convert.ToInt32(context.Request["orderID"]);
            string resultAct = "";
            using (ebsDBData db = new ebsDBData())
            {
                if (id == 0)
                {
                    BussinessPayment wp = new BussinessPayment();
                    wp.OrderID = OrderID;
                    wp.PayType = context.Request["PayMethod"];
                    wp.PayDate = Convert.ToDateTime(context.Request["PayDate"]);
                    wp.PayNo = context.Request["PayOrder"];
                    wp.PayAmount = Convert.ToDouble(context.Request["PayAmount"]);
                    wp.Zhuangtai = "可编辑";
                    db.BussinessPayment.InsertOnSubmit(wp);
                    db.SubmitChanges();
                    id = wp.ID;
                    resultAct = "保存";
                }
                else
                {
                    BussinessPayment wp = db.BussinessPayment.First(q => q.ID == id);
                    wp.PayType = context.Request["PayMethod"];
                    wp.PayDate = Convert.ToDateTime(context.Request["PayDate"]);
                    wp.PayNo = context.Request["PayOrder"];
                    wp.PayAmount = Convert.ToDouble(context.Request["PayAmount"]);
                    wp.Zhuangtai = "可编辑";
                    resultAct = "更新";
                    db.SubmitChanges();
                }
                string s = "{result:'" + resultAct + "成功',id:'" + id + "'}";
                context.Response.Write(s);
            }
        }

        private void getCustomerInfo(HttpContext context)
        {
            string res = "{{'Company':'{0}','Qudao':'{1}'}}";
            int CusID = Convert.ToInt32(context.Request["CusID"]);
            using (ebsDBData db = new ebsDBData())
            {
                Customers cus = db.Customers.First(q => q.ID == CusID);
                res = string.Format(res, cus.Company, cus.Source+" "+cus.Beizhu);
                context.Response.Write(res);
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
                if (db.Bussiness.Any(q => q.HetongID == hetongbianhao && q.ID != ID))
                {

                    res = string.Format(res, "false");
                }
                else res = string.Format(res, "true");
                context.Response.Write(res);
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