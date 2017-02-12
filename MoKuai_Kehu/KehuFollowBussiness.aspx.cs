using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.dbml;
using ebs.commons;

namespace ebs.MoKuai_Kehu
{
    public partial class KehuFollowBussiness : System.Web.UI.Page
    {
        public int ID
        {
            get { return ViewState["ID"] != null ? Convert.ToInt32(ViewState["ID"].ToString()) : (Request.QueryString["ID"] != null ? Convert.ToInt32(Request.QueryString["ID"]) : 0); }
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
             
            }
        }

        private void bindData()
        {
            using (ebsDBData db = new ebsDBData())
            {
                Repeater1.DataSource = db.CustomerFollowBySales_Bussiness.Where(q => q.MainID == ID).OrderByDescending(q=>q.FollowDate);
                Repeater1.DataBind();
            }
        }

        protected void btSubmit_Sales_Click(object sender, EventArgs e)
        {
            using (ebsDBData db = new ebsDBData())
            {
                CustomerFollowBySales_Bussiness cfSales = new CustomerFollowBySales_Bussiness();
                cfSales.FollowDate = DateTime.Now;
                cfSales.isChengDan = ddlChengdan.SelectedValue;
                cfSales.UserName = currentUser.DisplayName;
                cfSales.Feedback = tbFeedback_Sales.Text;
                cfSales.MainID = ID;
                db.CustomerFollowBySales_Bussiness.InsertOnSubmit(cfSales);
                db.SubmitChanges();
            }
            Response.Redirect(Request.Url.ToString());
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                using (ebsDBData db = new ebsDBData())
                {
                    var item = db.CustomerFollowBySales_Bussiness.First(q=>q.ID == ID);

                    db.CustomerFollowBySales_Bussiness.DeleteOnSubmit(item);
                    db.SubmitChanges();
                }
            }
            Response.Redirect(Request.Url.ToString());
        }
    }
}