using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.commons;
using ebs.dbml;
using System.Text;

namespace ebs
{
    public partial class _default : LoginChecker
    {
        public ComCls.LoginUser currentUser
        {
            get { return (ComCls.LoginUser)Session["LoginUserInfo"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        protected void InitPage()
        {
            divUsername.InnerText = currentUser.userName;
            divDisplayname.InnerText = currentUser.DisplayName;
            divMail.InnerText = currentUser.mailAddress;
            divRole.InnerText = currentUser.roles;
            divRegin.InnerText = currentUser.region;

            if (currentUser.roles == "客服" || currentUser.roles == "客服主管")
            {
                divFukuan.Visible = false;
            }
            else
            {
                using (ebsDBData db =new ebsDBData())
                {
                    var res = db.WeddingPayment.Where(q => q.PayDate.AddDays(7) > DateTime.Now && q.Zhuangtai != "审核完成");

                }
            }

            using (ebsDBData db = new ebsDBData())
            {
                DateTime currentMonthS, currentMonthE, lastMonthS, lastMonthE;
                currentMonthS = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                if (DateTime.Now.Month != 12)
                {
                    currentMonthE = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
                }
                else
                {
                    currentMonthE = new DateTime(DateTime.Now.Year + 1, 1, 1);
                }
                if (DateTime.Now.Month == 1)
                {
                    lastMonthS = new DateTime(DateTime.Now.Year-1, 12, 1);
                }
                else
                {
                    lastMonthS = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
                }
               
               
                lastMonthE = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) ;


                lbCurrent_HL_Customer.Text = db.Customers.Count(q => q.CreationDate > currentMonthS && q.CreationDate < currentMonthE && q.CustomerType == "婚宴").ToString();
                lbCurrent_SW_Customer.Text = db.Customers.Count(q => q.CreationDate > currentMonthS && q.CreationDate < currentMonthE && q.CustomerType == "商务").ToString();
                lbCurrent_HL_Order.Text = db.WeddingOrders.Count(q => q.HetongDate > currentMonthS && q.HetongDate < currentMonthE).ToString();
                lbCurrent_SW_Order.Text = db.Bussiness.Count(q => q.HetongDate > currentMonthS && q.HetongDate < currentMonthE).ToString();

                lbLast_HL_Customer.Text = db.Customers.Count(q => q.CreationDate > lastMonthS && q.CreationDate < lastMonthE && q.CustomerType == "婚宴").ToString();
                lbLast_SW_Customer.Text = db.Customers.Count(q => q.CreationDate > lastMonthS && q.CreationDate < lastMonthE && q.CustomerType == "商务").ToString();
                lbLast_HL_Order.Text = db.WeddingOrders.Count(q => q.HetongDate > lastMonthS && q.HetongDate < lastMonthE).ToString();
                lbLast_SW_Order.Text = db.Bussiness.Count(q => q.HetongDate > lastMonthS && q.HetongDate < lastMonthE).ToString();

               List<WeddingOrders> res = new List<WeddingOrders>();
                string HideHY = "hide";
                string HideSW = "hide";
                int SpanHY = 0;
                int SpanSW = 0;

              if (currentUser.roles == "婚宴销售")
                {
                    res = db.WeddingOrders.Where(q => q.Sales == currentUser.userName).Where(q=>q.Zhuangtai !="已覆盖").Take(10).ToList();
                  HideHY = "active";
                }
                if (currentUser.roles == "婚宴销售主管" )
                {
                    //res = db.WeddingOrders.Where(q=>q.Zhuangtai !="已覆盖").Take(10).ToList();
                    res = db.WeddingOrders.Where(q => q.Zhuangtai != "已覆盖").ToList();
                    HideHY = "active";
                    
                }
              

                if (currentUser.roles == "文员" || currentUser.roles == "财务" || currentUser.roles=="监督者")
                {
                    res = db.WeddingOrders.Where(q=>q.Zhuangtai !="已覆盖").Take(10).ToList();
                    HideHY = "active";
                    HideSW = "";
                    
                }
                if (currentUser.roles == "商务销售" || currentUser.roles == "商务销售主管")
                {
                    HideSW = "active";
                }

                if (currentUser.roles == "文员")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLinkA", " HideLinkA();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLinkB", " HideLinkB();", true);
                }

                string[] paytypes = {"1","2","3"};
                List<ConvertStringsInDB.paymentLeftByIDandType_HL> lstFukuan = new List<ConvertStringsInDB.paymentLeftByIDandType_HL>();

                foreach (var item in res)
                {
                    foreach (string paytype in paytypes)
                    {
                        var pbtHL = new ConvertStringsInDB.paymentLeftByIDandType_HL(item.ID, paytype);
                        double YingshouJine = 0;

                        if (double.TryParse(pbtHL.YingshouJine, out YingshouJine) && YingshouJine > 0 && (pbtHL.YinshouRiqi.AddDays(-7) < DateTime.Now ))
                        {
                            lstFukuan.Add(pbtHL);
                        }
                    }
                }
                Repeater1.DataSource = lstFukuan.OrderByDescending(q => q.YinshouRiqi);
                Repeater1.DataBind();
                SpanHY = lstFukuan.Count();

                var resB = from q in db.BussinessPayment.Where(q=>q.Zhuangtai!="审批完成").OrderBy(q => q.PayDate)//.Take(10)
                           join p in db.Bussiness on q.OrderID equals p.ID
                           select new
                           {
                               OrderID = p.ID,
                               HetongID = p.HetongID,
                               EventDate = p.EventDate,
                               PayAmount = q.PayAmount,
                               PayDate = q.PayDate,
                               PayType = q.PayType
                           };
                Repeater2.DataSource = resB;
                Repeater2.DataBind();
                SpanSW= resB.Count();
                string tabs = "<li role=\"presentation\" class=\"{0}\"><a href=\"#hy\" aria-controls=\"hy\" role=\"tab\" data-toggle=\"tab\">婚宴&nbsp;<span class=\" badge small\">{2}</span></a></li><li role=\"presentation\" class=\"{1}\"><a href=\"#sw\" aria-controls=\"sw\" role=\"tab\" data-toggle=\"tab\">商务&nbsp;<span class=\"badge small\">{3}</span></a></li>";
                ltTabsPay.Text = string.Format(tabs, HideHY, HideSW, SpanHY.ToString(), SpanSW.ToString());
                if (HideHY == "active")
                {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "", " HideSWorHY('HY');", true);
                }
                if (HideSW == "active")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", " HideSWorHY('SW');", true);
                }

            }

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "", " getDaiban();", true);
        }

     

        protected void handleHunliKuanxiang(DateTime hunliRiqi, DateTime hetongRiqi)
        {
            ;
        }

     

        class HunLiItem
        {
            public string XinRen { get; set; }
            public string HuodongBianhao { get; set; }
            public DateTime HunliShijian { get; set; }
            public string Kuangxiang { get; set; }
            public DateTime YingfuRiqi { get; set; }
            public string Jinge { get; set; }
            public string Changdi { get; set; }
       
            
        }
    }
}