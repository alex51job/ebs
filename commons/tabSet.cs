using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;
using ebs.dbml;

namespace ebs.commons
{
    public static class tabSet
    {
       public static void setCustomerTabs(this Literal tabs,string currentURL,int CustomerID=0)
       {
           string tab = @"<li role=""presentation"" class=""{0}""><a href=""{1}"">{2}</a></li>";
           StringBuilder sbtabs = new StringBuilder();
           sbtabs.Append(@"<div class=""col-sm-12 col-md-12 col-lg-12 ""><ul class=""nav nav-tabs"">");
           //对于客户编辑页面
           if (currentURL.Contains("KehuEdit"))
           {
               sbtabs.AppendFormat(tab, "active", "#", "客户信息");
               if (CustomerID !=0)
               {
                   using (ebsDBData db = new ebsDBData())
                   {
                       string type = db.Customers.First(q => q.ID == CustomerID).CustomerType;
                       if (type == "商务") sbtabs.AppendFormat(tab, "", "KehuFollowBussiness.aspx?ID="+CustomerID, "跟进信息");
                       else sbtabs.AppendFormat(tab, "", "KehuFollowWedding.aspx?ID=" + CustomerID, "跟进信息");
                   }
               }
           }
           //对于客户跟进页面
           if (currentURL.Contains("KehuFollow"))
           {
               sbtabs.AppendFormat(tab, "", "KehuEdit.aspx?ID="+CustomerID, "客户信息");
               sbtabs.AppendFormat(tab, "active", "#", "跟进信息");
           }
           sbtabs.Append(@"</ul></div><div class=""clearfix""></div>");
           tabs.Text = sbtabs.ToString();
       }
        public static void setBussinessOrderTabs(this Literal tabs, string currentURL, string currentRole, int BussinessOrderID = 0)
       {
           StringBuilder sbtabs = new StringBuilder();
           sbtabs.Append(@"<div class=""col-sm-12 col-md-12 col-lg-12 ""><ul class=""nav nav-tabs"">");
            List<Tabs> listtabs = new List<Tabs>();

            Tabs tbBOEdit = new Tabs("ShangwuEdit.aspx", "订单信息", BussinessOrderID);
            Tabs tbBOPayment = new Tabs("ShangwuPayments.aspx", "支付信息", BussinessOrderID);
            Tabs tbBOAudit = new Tabs("ShangwuAudit.aspx", "审批信息", BussinessOrderID);
            Tabs tbBOAuditFinance = new Tabs("SWAuditFinance.aspx", "支付审批（财务）", BussinessOrderID);

            if (BussinessOrderID !=0)
            {
                if (currentRole == "商务销售" || currentRole == "商务销售主管" || currentRole == "总经理" || currentRole == "监督者")
                {
                    listtabs.Add(tbBOEdit);
                    listtabs.Add(tbBOAudit);
                    listtabs.Add(tbBOPayment);
                }
                if (currentRole == "文员")
                {
                    listtabs.Add(tbBOEdit);
                    listtabs.Add(tbBOAudit);
                    listtabs.Add(tbBOPayment);
                }
                if (currentRole == "财务")
                {
                    listtabs.Add(tbBOEdit);
                    listtabs.Add(tbBOAudit);
                    listtabs.Add(tbBOAuditFinance);
                }
            }
            else
            {
                listtabs.Add(tbBOEdit);
            }

            foreach (var item in listtabs)
            {
                if (currentURL.Contains(item.link))
                {
                    item.active = "active";
                }
                sbtabs.Append(item.ToString());
            }
            sbtabs.Append(@"</ul></div><div class=""clearfix""></div>");
            tabs.Text = sbtabs.ToString();
       }
        public static void setWeddingOrderTabs(this Literal tabs, string currentURL, string currentRole,int WeddingID=0)
       {
            StringBuilder sbtabs = new StringBuilder();
            sbtabs.Append(@"<div class=""col-sm-12 col-md-12 col-lg-12 ""><ul class=""nav nav-tabs"">");
            List<Tabs> listtabs = new List<Tabs>();

            Tabs tbHunliEdit = new Tabs("HunliEdit.aspx","订单信息",WeddingID);
            Tabs tbHunliPayment = new Tabs("HunliPayments.aspx", "支付信息", WeddingID);
            Tabs tbHunliEditAddedPay = new Tabs("HunliEditAddedPay.aspx", "订单信息（文员）", WeddingID);
            Tabs tbHunliAuditFinance = new Tabs("hlAuditFinance.aspx", "支付审批（财务）", WeddingID);
            Tabs tbHunliAudit = new Tabs("HunliAudit.aspx", "审批信息",WeddingID);


            if (WeddingID != 0)
            {

                if (currentRole == "婚宴销售" || currentRole == "婚宴销售主管" || currentRole == "总经理" || currentRole == "监督者")
                {
                    listtabs.Add(tbHunliEdit);
                    listtabs.Add(tbHunliAudit);
                    listtabs.Add(tbHunliPayment);
                }
                if (currentRole == "文员")
                {
                    listtabs.Add(tbHunliEditAddedPay);
                    listtabs.Add(tbHunliAudit);
                    listtabs.Add(tbHunliPayment);
                }
                if (currentRole == "财务")
                {
                    listtabs.Add(tbHunliEdit);
                    listtabs.Add(tbHunliAudit);
                    listtabs.Add(tbHunliAuditFinance);
                }
            }
            else
            {
                listtabs.Add(tbHunliEdit);
            }
           

            foreach (var item in listtabs)
            {
                if (currentURL.Contains(item.link))
                {
                    item.active = "active";
                }
                sbtabs.Append(item.ToString());
            }
            sbtabs.Append(@"</ul></div><div class=""clearfix""></div>");
            tabs.Text = sbtabs.ToString();

       }

        public class Tabs
        {
            public string active { get; set; }
            public string link { get; set; }
            public string linkID { get; set; }
            public string linkName { get; set; }
            public Tabs(string Link, string LinkName, int wedID = 0)
            {
                active = "";
                link = Link;
                linkID = wedID == 0 ? "" : wedID.ToString();
                linkName = LinkName;
            }

            public override string ToString()
            {
                return string.Format(@"<li role=""presentation"" class=""{0}""><a href=""{1}"">{2}</a></li>", active, link + "?ID=" + linkID, linkName);
            }
        }
      
    }
}