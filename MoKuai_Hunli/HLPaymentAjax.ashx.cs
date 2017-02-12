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
    /// HLPaymentAjax 的摘要说明
    /// </summary>
    public class HLPaymentAjax : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string method = context.Request["method"];
                switch (method)
                {
                    case "save":
                        savePayment(context);
                        break;
                    case "delete":
                        deletePayment(context);
                        break;
                    case "submit":
                        submitPayment(context);
                        break;
                    case "pass":
                        passPayment(context);
                        break;
                    case "redrew":
                        redrewPayment(context);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                string s = "{result:'error',message:'{"+ex.Message+"}'}";
                context.Response.Write(s);
            }
           
        }

        private void redrewPayment(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request["payID"]);
            using (ebsDBData db = new ebsDBData())
            {
                var wp = db.WeddingPayment.First(q => q.ID == id);
                wp.Zhuangtai = "可编辑";
                db.SubmitChanges();
            }
            context.Response.Write("{result:'已退回'}");
        }

        private void passPayment(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request["payID"]);
            string finishable = "0";
            using (ebsDBData db = new ebsDBData())
            {
                var wp = db.WeddingPayment.First(q => q.ID == id);
                wp.Zhuangtai = "审批完成";
                db.SubmitChanges();
                //判断是否可完成订单
                int orderid = wp.OrderID;
                var r = new ConvertStringsInDB.JineByID(orderid);
                double total = Convert.ToDouble(r.Zongjine) + Convert.ToDouble(r.Zongjinex);
                double total_yingfu = Convert.ToDouble(r.YifujineAuditFinish);
                string auditStatus = r.ZhuangtaiAudit;
                if (total <= total_yingfu && auditStatus == "审批完成") finishable = "1";
            }
            context.Response.Write("{result:'审批完成',finish:"+finishable+"}");
        }

        private void deletePayment(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request["payID"]);
            using (ebsDBData db = new ebsDBData())
            {
                var wp = db.WeddingPayment.FirstOrDefault(q => q.ID == id);
                db.WeddingPayment.DeleteOnSubmit(wp);
                db.SubmitChanges();
            }
            context.Response.Write("{result:'删除成功'}");
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
                    WeddingPayment wp = new WeddingPayment();
                    wp.OrderID = OrderID;
                    wp.PayType = context.Request["payType"];
                    wp.PayDate = Convert.ToDateTime(context.Request["payDate"]);
                    wp.ShishouHQ = Convert.ToDouble(context.Request["shishouHQ"]);
                    wp.ShishouHY = Convert.ToDouble(context.Request["shishouHY"]);
                    wp.PayOrderNumber = context.Request["PayOrderNumber"];
                    wp.Zhuangtai = "可编辑";
                    db.WeddingPayment.InsertOnSubmit(wp);
                    db.SubmitChanges();
                    id = wp.ID;
                    resultAct = "保存";
                }
                else
                {
                    WeddingPayment wp = db.WeddingPayment.First(q => q.ID == id);
                    wp.PayType = context.Request["payType"];
                    wp.PayDate = Convert.ToDateTime(context.Request["payDate"]);
                    wp.ShishouHQ = Convert.ToDouble(context.Request["shishouHQ"]);
                    wp.ShishouHY = Convert.ToDouble(context.Request["shishouHY"]);
                    wp.PayOrderNumber = context.Request["PayOrderNumber"];
                    wp.Zhuangtai = "可编辑";
                    resultAct = "更新";
                    db.SubmitChanges();
                }
                string s = "{result:'"+resultAct+"成功',id:'"+id+"'}";
                context.Response.Write(s);
            }

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
                    WeddingPayment wp = new WeddingPayment();
                    wp.OrderID = OrderID;
                    wp.PayType = context.Request["payType"];
                    wp.PayDate = Convert.ToDateTime(context.Request["payDate"]);
                    wp.ShishouHQ = Convert.ToDouble(context.Request["shishouHQ"]);
                    wp.ShishouHY = Convert.ToDouble(context.Request["shishouHY"]);
                    wp.PayOrderNumber = context.Request["PayOrderNumber"];
                    wp.Zhuangtai = "审批中";
                    db.WeddingPayment.InsertOnSubmit(wp);
                    db.SubmitChanges();
                    id = wp.ID;
                    resultAct = "保存并提交";
                }
                else
                {
                    WeddingPayment wp = db.WeddingPayment.First(q => q.ID == id);
                    wp.PayType = context.Request["payType"];
                    wp.PayDate = Convert.ToDateTime(context.Request["payDate"]);
                    wp.ShishouHQ = Convert.ToDouble(context.Request["shishouHQ"]);
                    wp.ShishouHY = Convert.ToDouble(context.Request["shishouHY"]);
                    wp.PayOrderNumber = context.Request["PayOrderNumber"];
                    wp.Zhuangtai = "审批中";
                    resultAct = "更新并提交";
                    db.SubmitChanges();
                }
                string s = "{result:'" + resultAct + "成功',id:'" + id + "'}";
                context.Response.Write(s);
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