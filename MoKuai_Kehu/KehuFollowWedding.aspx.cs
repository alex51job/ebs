using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.commons;
using ebs.dbml;

namespace ebs.MoKuai_Kehu
{
    public partial class KehuFollowWedding : System.Web.UI.Page
    {
        public int ID
        {
            get { return ViewState["ID"] != null ?   Convert.ToInt32(ViewState["ID"].ToString()):(Request.QueryString["ID"]!=null? Convert.ToInt32(Request.QueryString["ID"]):0); }
            set { ViewState["ID"] = value; }
        }
        public ComCls.LoginUser currentUser
        {
            get { return ((Kehu)(Page.Master)).LoginUserInfo; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CustomerTabs.setCustomerTabs(Request.Url.ToString(), ID);
                bindData();
                initPage();
               
            }
        }

        private void bindData()
        {
            List<CustomerFollowBySales> lstCfSales = new List<CustomerFollowBySales>();
            List<CustomerFollowByKefu> lstCfKefu = new List<CustomerFollowByKefu>();
            using (ebsDBData db = new ebsDBData())
            {
                lstCfKefu = db.CustomerFollowByKefu.Where(q => q.MainID == ID).ToList();
                lstCfSales = db.CustomerFollowBySales.Where(q => q.MainID == ID).ToList();
                if (lstCfSales.Count>0)
                {
                    var lastCfSales = lstCfSales.Last();
                    ddlDaodian.SelectedValue = lastCfSales.IsDaoDian;
                    lbDaodian.Text = lastCfSales.IsDaoDian;
                    tbDaodianshijian.Text = lastCfSales.DaoDianDate;
                    lbDaodianshijian.Text = lastCfSales.DaoDianDate;
                    ddlChengdan.SelectedValue = lastCfSales.isChengDan;
                    lbChendan.Text = lastCfSales.isChengDan;
                    ddlFandian.SelectedValue = lastCfSales.SourceDiscount;
                    lbFandian.Text = lastCfSales.SourceDiscount;
                    tbFeedback_Sales.Text = lastCfSales.FeedBack;

                    lbFollowDate_Sales.Text = lastCfSales.FollowDate.ToString("yyyy-MM-dd hh:mm:ss");
                }

                if (lstCfKefu.Count > 0)
                {
                    var lastCfKefu = lstCfKefu.Last();
                    tbHuifangRiqi.Text = lastCfKefu.HuiFangRiqi;
                    lbHuifangRiqi.Text = lastCfKefu.HuiFangRiqi;
                    tbHuifangkefu.Text = lastCfKefu.HuiFangKefu;
                    lbHuifangKefu.Text = lastCfKefu.HuiFangKefu;
                    tbFeedback_Kefu.Text = lastCfKefu.HuiFangXinxi;

                    lbFollowDate_Kefu.Text = lastCfKefu.FollowDate.ToString("yyyy-MM-dd hh:mm:ss"); 
                }

                var LogSales = from q in lstCfSales
                               select new
                               {
                                   FollowDate = q.FollowDate,
                                   UserName = q.UserName,
                                   UserRole = q.UserRole,
                                   FollowType = Translate(q.IsDaoDian, q.isChengDan),
                                   FeedbackDate = q.DaoDianDate,
                                   FeedbackInfo = q.FeedBack
                               };
                var LogKefu = from q in lstCfKefu
                              select new
                              {
                                  FollowDate = q.FollowDate,
                                  UserName = q.UserName,
                                  UserRole = q.UserRole,
                                  FollowType = "回访",
                                  FeedbackDate = q.HuiFangRiqi,
                                  FeedbackInfo = q.HuiFangXinxi
                              };
                Repeater1.DataSource = LogSales.Union(LogKefu).OrderByDescending(q=>q.FollowDate);
                Repeater1.DataBind();

            }

        }

        private string Translate(string Daodian, string Chengdan)
        {
            string result = "";
            if (Daodian == "是")
            {
                result += "到店";
            }
            else
            {
                result += "未到店";
            }
            if (result == "到店")
            {
                if (Chengdan == "是")
                {
                    result += "成单";
                }
                else
                {
                    result += "未成单";
                }
            }
            return result;

        }

       

        private void initPage()
        {
            tbHuifangRiqi.Visible = false;
            tbHuifangkefu.Visible = false;
            ddlDaodian.Visible = false;
            tbDaodianshijian.Visible = false;
            ddlChengdan.Visible = false;
            ddlFandian.Visible = false;
            btSubmit_Kefu.Visible = false;
            btSubmit_Sales.Visible = false;
            

            if (currentUser.roles.Contains("销售"))
            {
                lbDaodian.Visible = false;
                lbChendan.Visible = false;
                lbDaodianshijian.Visible = false;
                lbFandian.Visible = false;

                ddlDaodian.Visible = true;
                tbDaodianshijian.Visible = true;
                ddlChengdan.Visible = true;
                ddlFandian.Visible = true;
                btSubmit_Sales.Visible = true;
               
            }

            if (currentUser.roles.Contains("客服"))
            {
                lbHuifangRiqi.Visible = false;
                lbHuifangKefu.Visible = false;

                tbHuifangRiqi.Visible = true;
                tbHuifangkefu.Visible = true;
                btSubmit_Kefu.Visible = true;
            }

        }

        protected void btSubmit_Sales_Click(object sender, EventArgs e)
        {
            using (ebsDBData db = new ebsDBData())
            {
                CustomerFollowBySales cfSales = new CustomerFollowBySales();
                cfSales.IsDaoDian = ddlDaodian.SelectedValue ;
                
                cfSales.DaoDianDate = tbDaodianshijian.Text.Trim();
                cfSales.isChengDan = ddlChengdan.SelectedValue ;
               
                cfSales.SourceDiscount = ddlFandian.SelectedValue;
                cfSales.FeedBack = tbFeedback_Sales.Text;
                cfSales.FollowDate = DateTime.Now;
                cfSales.MainID = ID;
                cfSales.UserName = currentUser.DisplayName;
                cfSales.UserRole = currentUser.roles;
                db.Customers.First(q => q.ID == ID).NeedHuiFang = "待回访";
                if (ddlChengdan.SelectedValue == "是")
                {
                    db.Customers.First(q => q.ID == ID).DaoDianCount += 1;
                }
                db.CustomerFollowBySales.InsertOnSubmit(cfSales);
                db.SubmitChanges();
            }
            Response.Redirect(Request.Url.ToString());

        }

        protected void btSubmit_Kefu_Click(object sender, EventArgs e)
        {
            using (ebsDBData db = new ebsDBData())
            {
                CustomerFollowByKefu cfKefu = new CustomerFollowByKefu();
                cfKefu.HuiFangRiqi = tbHuifangRiqi.Text.Trim();
                cfKefu.HuiFangKefu = tbHuifangkefu.Text.Trim();
                cfKefu.HuiFangXinxi = tbFeedback_Kefu.Text;
                cfKefu.FollowDate = DateTime.Now;
                cfKefu.MainID = ID;
                cfKefu.UserName = tbHuifangkefu.Text.Trim();
                cfKefu.UserRole = "客服";
                db.Customers.First(q => q.ID == ID).NeedHuiFang = "已回访";
                db.CustomerFollowByKefu.InsertOnSubmit(cfKefu);
                db.SubmitChanges();

            }
            Response.Redirect(Request.Url.ToString());
        }
    }
}