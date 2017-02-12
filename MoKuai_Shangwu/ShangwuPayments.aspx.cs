using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.commons;
using ebs.dbml;


namespace ebs.MoKuai_Shangwu
{
    public partial class ShangwuPayments : System.Web.UI.Page
    {
        public int ID
        {
            get { return ViewState["ID"] != null ? Convert.ToInt32(ViewState["ID"].ToString()) : 0; }
            set { ViewState["ID"] = value; }
        }
        public ComCls.LoginUser currentUser
        {
            get { return ((Shangwu)(Page.Master)).LoginUserInfo; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["ID"] = Request.QueryString["ID"];
                hdID.Value = ID.ToString();
                initPage();
                BussinessTabs.setBussinessOrderTabs(Request.Url.ToString(), currentUser.roles, ID);
            }
        }

        private void initPage()
        {
            if (ID ==0)
            {
                return;
            }
            using (ebsDBData db = new ebsDBData())
            {
                Bussiness BO = db.Bussiness.First(q => q.ID == ID);
                ConvertStringsInDB.JineByID_Bussiness Jine = new ConvertStringsInDB.JineByID_Bussiness(ID);
                hdZongjine.Value = Jine.Zongjine.ToString();
                List<BussinessPayment> payParts = db.BussinessPayment.Where(q => q.OrderID == ID && (q.PayType == "现金" || q.PayType == "转账") ).ToList();
                //data: {PayNo:'{0}',PayType:'{1}',PayDate:'{2}',PayAmount:'{3}',PayStatus:'{4}'}
                //data:{[1,2,3,4]}
                if (Jine.ZhuangtaiAudit == "结束")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HideBtadd", "HideBtadd();", true);
                }

                string data = "{payments:[";
                if (payParts.Count > 0)
                {
                    foreach (var item in payParts)
                    {
                        string payContent = string.Format("{{PayNo:'{0}',PayType:'{1}',PayDate:'{2}',PayAmount:'{3}',PayStatus:'{4}',PayId:'{5}'}}", item.PayNo, item.PayType, item.PayDate.ToString("yyyy-MM-dd"), item.PayAmount, item.Zhuangtai,item.ID);
                        data += payContent + ",";
                    }
                    data = data.Substring(0, data.Length - 1) + "]}";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadPayments", "LoadPayments(" + data + ");", true);
                }

               


                List<BussinessPayment> RefundParts = db.BussinessPayment.Where(q => q.OrderID == ID && (q.PayType == "赔款" || q.PayType == "退款")).ToList();
                string RefundData = "{Refunds:[";
                if (RefundParts.Count > 0)
                {
                    foreach (var item in RefundParts)
                    {
                        string RefundContent = string.Format("{{PayNo:'{0}',PayType:'{1}',PayDate:'{2}',PayAmount:'{3}',PayStatus:'{4}',PayId:'{5}'}}", item.PayNo, item.PayType, item.PayDate.ToString("yyyy-MM-dd"), item.PayAmount, item.Zhuangtai, item.ID);
                        RefundData += RefundContent + ",";
                    }
                    RefundData = RefundData.Substring(0, RefundData.Length - 1) + "]}";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadRefund", "LoadRefund(" + RefundData + ");", true);
                }

              
            }
        }
    }
}