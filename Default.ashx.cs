using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using ebs.dbml;
using ebs.commons;
using System.Web.SessionState;

namespace ebs
{
    /// <summary>
    /// Default 的摘要说明
    /// </summary>
    public class Default : IHttpHandler,IRequiresSessionState 
    {
        public ComCls.LoginUser currentUser
        {
            get { return (ComCls.LoginUser)HttpContext.Current.Session["LoginUserInfo"]; }
        }
        public void ProcessRequest(HttpContext context)
        {
            string method = context.Request["method"];
            switch (method)
            {
                case "getDaiban":
                    getDaiban(context);
                    break;
                default:
                    break;
            }
        }

        private void getDaiban(HttpContext context)
        {
           
            //{list:[{content:'',num:''},{},{}]}
            string role = currentUser.roles;
            StringBuilder sb = new StringBuilder("{list:[");
            string jsonObj = @"{{content:'{0}',num:'{1}'}}";
            string content = "";
            int n = 0;
            using (ebsDBData db = new ebsDBData())
            {
                if (role == "客服")
                {
                    //"{content:'待回访客户',num:'3'}"
                    content = "待下发客户";
                    n = db.Customers.Count(q => q.Kefu == currentUser.userName && q.Zhuangtai == "未下发" && q.AuditStatus !="通过");
                    sb.AppendFormat(jsonObj, content, n);
                    sb.Append(",");

                    content = "待回访客户";
                    n = db.Customers.Count(q => q.NeedHuiFang == "待回访");
                    sb.AppendFormat(jsonObj, content, n);
                }

                if (role == "客服主管")
                {
                    content = "待下发客户";
                    n = db.Customers.Count(q =>  q.Zhuangtai == "未下发" && q.AuditStatus != "通过");
                    sb.AppendFormat(jsonObj, content, n);
                    sb.Append(",");

                    content = "待回访客户";
                    n = db.Customers.Count(q => q.NeedHuiFang == "待回访");
                    sb.AppendFormat(jsonObj, content, n);
                    sb.Append(",");
                    
                    content = "待审核客户";
                    n = db.Customers.Count(q=>q.AuditStatus == "未审批");
                    sb.AppendFormat(jsonObj, content, n);


                }

                if (role == "婚宴销售")
                {
                    content = "待跟进客户";
                    n = db.Customers.Count(q => q.Sales == currentUser.userName && q.CustomerFollowBySales.Count == 0 && q.AuditStatus !="无效" && q.Zhuangtai == "已下发");
                    sb.AppendFormat(jsonObj, content, n);
                    sb.Append(",");

                    content = "待处理订单";
                    n = db.WeddingOrders.Count(q => q.Sales == currentUser.userName && q.Zhuangtai == "编辑" );
                    sb.AppendFormat(jsonObj, content, n);
                }

                if (role == "婚宴销售主管")
                {
                    content = "待跟进客户";
                    n = db.Customers.Count(q => q.Sales == currentUser.userName && q.CustomerFollowBySales.Count == 0 && q.AuditStatus != "无效" && q.Zhuangtai == "已下发");
                    sb.AppendFormat(jsonObj, content, n);
                    sb.Append(",");

                    content = "待处理订单";
                    n = db.WeddingOrders.Count(q => q.Zhuangtai == "编辑");
                    sb.AppendFormat(jsonObj, content, n);
                    sb.Append(",");

                    content = "待审核订单";
                    var res = db.WeddingOrders.Where(q => q.Zhuangtai == "待审批");
                    n = 0;
                    foreach (var item in res)
                    {
                        var jine = new ConvertStringsInDB.JineByID(item.ID);
                        if (role.Contains(jine.RoleToAudit))
                        {
                            n++;
                        }
                    }
                    sb.AppendFormat(jsonObj, content, n);
                }

                if (role == "商务销售")
                {
                    content = "待跟进客户";
                    n = db.Customers.Count(q => q.Sales == currentUser.userName && q.CustomerFollowBySales.Count == 0 && q.AuditStatus != "无效" && q.Zhuangtai == "已下发");
                    sb.AppendFormat(jsonObj, content, n);
                    sb.Append(",");

                    content = "待处理订单";
                    n = db.Bussiness.Count(q => q.Sales == currentUser.userName && q.Zhuangtai == "编辑");
                    sb.AppendFormat(jsonObj, content, n);
                }

                if (role == "商务销售主管")
                {
                    content = "待跟进客户";
                    n = db.Customers.Count(q => q.Sales == currentUser.userName && q.CustomerFollowBySales.Count == 0 && q.AuditStatus != "无效" && q.Zhuangtai == "已下发");
                    sb.AppendFormat(jsonObj, content, n);
                    sb.Append(",");

                    content = "待处理订单";
                    n = db.Bussiness.Count(q => q.Zhuangtai == "编辑");
                    sb.AppendFormat(jsonObj, content, n);
                    sb.Append(",");

                    content = "待审核订单";
                    var res = db.Bussiness.Where(q => q.Zhuangtai == "待审批");
                    n = 0;
                    foreach (var item in res)
                    {
                        var jine = new ConvertStringsInDB.JineByID_Bussiness(item.ID);
                        if (role.Contains(jine.RoleToAudit))
                        {
                            n++;
                        }
                    }
                    sb.AppendFormat(jsonObj, content, n);
                }
                if (role == "文员")
                {
                    content = "待审核订单";
                    var res = db.WeddingOrders.Where(q => q.Zhuangtai == "待审批");
                    n = 0;
                    foreach (var item in res)
                    {
                        var jine = new ConvertStringsInDB.JineByID(item.ID);
                        if (role.Contains(jine.RoleToAudit))
                        {
                            n++;
                        }
                    }
                    var resB = db.Bussiness.Where(q => q.Zhuangtai == "待审批");
                    foreach (var item in resB)
                    {
                        var jine = new ConvertStringsInDB.JineByID_Bussiness(item.ID);
                        if (role.Contains(jine.RoleToAudit))
                        {
                            n++;
                        }
                    }
                    sb.AppendFormat(jsonObj, content, n);
                    sb.Append(",");

                    content = "待处理收款";
                    n = db.WeddingPayment.Count(q =>q.PayType.Length == 1 && q.Zhuangtai == "可编辑");
                    n += db.BussinessPayment.Count(q => q.PayType == "现金" || q.PayType == "转账" && q.Zhuangtai == "可编辑");
                    sb.AppendFormat(jsonObj, content, n);
                    sb.Append(",");

                    content = "待处理退款";
                    n = db.WeddingPayment.Count(q => q.PayType.Length == 2 && q.Zhuangtai == "可编辑");
                    n += db.BussinessPayment.Count(q => q.PayType == "赔款" || q.PayType == "退款" && q.Zhuangtai == "可编辑");
                    sb.AppendFormat(jsonObj, content, n);
                }
                if (role == "总经理")
                {

                    content = "待审核订单";
                    var res = db.WeddingOrders.Where(q => q.Zhuangtai == "待审批");
                    n = 0;
                    foreach (var item in res)
                    {
                        var jine = new ConvertStringsInDB.JineByID(item.ID);
                        if (role.Contains(jine.RoleToAudit))
                        {
                            n++;
                        }
                    }
                    var resB = db.Bussiness.Where(q => q.Zhuangtai == "待审批");
                    foreach (var item in resB)
                    {
                        var jine = new ConvertStringsInDB.JineByID_Bussiness(item.ID);
                        if (role.Contains(jine.RoleToAudit))
                        {
                            n++;
                        }
                    }
                    sb.AppendFormat(jsonObj, content, n);
                }

                if (role == "财务")
                {
                    content = "待审核订单";
                    var res = db.WeddingOrders.Where(q => q.Zhuangtai == "待审批");
                    n = 0;
                    foreach (var item in res)
                    {
                        var jine = new ConvertStringsInDB.JineByID(item.ID);
                        if (jine.RoleToAudit == role)
                        {
                            n++;
                        }
                    }
                    var resB = db.Bussiness.Where(q => q.Zhuangtai == "待审批");
                    foreach (var item in resB)
                    {
                        var jine = new ConvertStringsInDB.JineByID_Bussiness(item.ID);
                        if (role.Contains(jine.RoleToAudit))
                        {
                            n++;
                        }
                    }
                    sb.AppendFormat(jsonObj, content, n);
                    sb.Append(",");

                    content = "待审核收款";
                    n = db.WeddingPayment.Count(q => q.PayType.Length == 1 && q.Zhuangtai == "审批中");
                    n += db.BussinessPayment.Count(q => q.PayType == "现金" || q.PayType == "转账" && q.Zhuangtai == "审批中");
                    sb.AppendFormat(jsonObj, content, n);
                    sb.Append(",");

                    content = "待审核退款";
                    n = db.WeddingPayment.Count(q => q.PayType.Length == 2 && q.Zhuangtai == "审批中");
                    n += db.BussinessPayment.Count(q => q.PayType == "赔款" || q.PayType == "退款" && q.Zhuangtai == "审批中");
                    sb.AppendFormat(jsonObj, content, n);
                }
            }
            sb.Append("]}");
            context.Response.Write(sb.ToString());
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