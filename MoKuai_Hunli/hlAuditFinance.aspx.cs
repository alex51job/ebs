using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.commons;
using ebs.dbml;
using System.Text;


namespace ebs.MoKuai_Hunli
{
    public partial class hlAuditFinance : System.Web.UI.Page
    {
        public int ID
        {
            get { return ViewState["ID"] != null ? Convert.ToInt32(ViewState["ID"].ToString()) : 0; }
            set { ViewState["ID"] = value; }
        }
        public ComCls.LoginUser currentUser
        {
            get { return ((Hunli)(Page.Master)).LoginUserInfo; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["ID"] = Request.QueryString["ID"];
                initPage();
                WeddingTabs.setWeddingOrderTabs(Request.Url.ToString(), currentUser.roles, ID);
            }
        }

        private void initPage()
        {
            ID = Convert.ToInt32(Request.QueryString["ID"]);
            hdID.Value = ID.ToString();
            var r = new ConvertStringsInDB.JineByID(ID);
            hdZongjineHQ.Value = r.HunqinZongjine;
            hdZongjineHY.Value = r.HunyanZongjine;
            lbYingshouZongHY.InnerText = Convert.ToDouble(r.HunyanZongjine).ToString("0.0");
            lbYingshouZongHQ.InnerText = Convert.ToDouble(r.HunqinZongjine).ToString("0.0");

            hdZongjineHQex.Value = r.HunqinZongjinex;
            hdZongjineHYex.Value = r.HunyanZongjinex;
            lbYingshouZongHYex.InnerText = Convert.ToDouble(r.HunyanZongjinex).ToString("0.0");
            lbYingshouZongHQex.InnerText = Convert.ToDouble(r.HunqinZongjinex).ToString("0.0");

            lbhj_yingshouHQ.InnerText = (Convert.ToDouble(r.HunqinZongjine) + Convert.ToDouble(r.HunqinZongjinex)).ToString();
            lbhj_yingshouHY.InnerText = (Convert.ToDouble(r.HunyanZongjine) + Convert.ToDouble(r.HunyanZongjinex)).ToString();

            //结束订单按钮是否可见？
            if ((Convert.ToDouble(r.Zongjine) + Convert.ToDouble(r.Zongjinex)) <= Convert.ToDouble(r.YifujineAuditFinish) && r.ZhuangtaiAudit == "审批完成")
            {
                btFinish.Style["display"] = "block";
            }

            StringBuilder PayParts = new StringBuilder("{payments:[");
            using (ebsDBData db = new ebsDBData())
            {
                //正常付款
                List<WeddingPayment> lstPayment = db.WeddingPayment.Where(q => q.OrderID == ID && q.PayType.Length == 1 && (q.Zhuangtai=="审批中" || q.Zhuangtai=="审批完成")).OrderBy(q => q.PayType).ToList();
                if (lstPayment.Count > 0)
                {
                    int p = 1;
                    foreach (var item in lstPayment)
                    {
                        ConvertStringsInDB.paymentByIDandType jine = new ConvertStringsInDB.paymentByIDandType(item.OrderID, item.PayType);
                        PayParts.Append("{");
                        PayParts.AppendFormat("payType:'{0}',payDate:'{1}',shishouHY:'{2}',shishouHQ:'{3}',NeedPayHY:'{4}',BaiHY:'{5}',NeedPayHQ:'{6}',BaiHQ:'{7}',id:'{8}',zhuangtai:'{9}',PayOrderNumber:'{10}'",
                                              item.PayType, item.PayDate.ToString("yyyy-MM-dd"), item.ShishouHY, item.ShishouHQ, jine.HunyanNeedPay, jine.HunyanBai, jine.HunqinNeedPay, jine.HunqinBai, item.ID, item.Zhuangtai,item.PayOrderNumber);
                        if (lstPayment.Count == p) PayParts.Append("}");
                        else PayParts.Append("},");

                        p++;
                    }
                    PayParts.Append("]}");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "initPageIfhasID", "initPageIfhasID(" + PayParts + ");", true);

                }

                //退款
                lstPayment = db.WeddingPayment.Where(q => q.OrderID == ID && q.PayType.Length == 2 && (q.Zhuangtai == "审批中" || q.Zhuangtai == "审批完成")).OrderBy(q => q.PayType).ToList();
                PayParts = new StringBuilder("{payments:[");
                if (lstPayment.Count > 0)
                {
                    int tk = 1;
                    foreach (var item in lstPayment)
                    {
                        ConvertStringsInDB.paymentByIDandType jine = new ConvertStringsInDB.paymentByIDandType(item.OrderID, item.PayType);
                        PayParts.Append("{");
                        PayParts.AppendFormat("payType:'{0}',payDate:'{1}',shishouHY:'{2}',shishouHQ:'{3}',id:'{4}',zhuangtai:'{5}'",
                                              item.PayType, item.PayDate.ToString("yyyy-MM-dd"), item.ShishouHY, item.ShishouHQ, item.ID, item.Zhuangtai);
                        if (lstPayment.Count == tk) PayParts.Append("}");
                        else PayParts.Append("},");

                        tk++;
                    }
                    PayParts.Append("]}");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "initPageIfhasID_TK", "initPageIfhasID_TK(" + PayParts + ");", true);
                }

            }


        }

        protected void btFinish_Click(object sender, EventArgs e)
        {
          
            using (ebsDBData db = new ebsDBData())
            {
                WeddingOrders WO = db.WeddingOrders.First(q => q.ID == ID);
                var r = new ConvertStringsInDB.JineByID(ID);
                if ((Convert.ToDouble(r.Zongjine) + Convert.ToDouble(r.Zongjinex)) <= Convert.ToDouble(r.Yifujine) && r.ZhuangtaiAudit == "审批完成")
                {
                    WO.Zhuangtai = "结束";
                    db.SubmitChanges();
                    ComCls.ShowAndReload(this, "订单结束", Request.Url.ToString());
                    // Response.Write("<script language=javascript>window.location.reload();</script>"); 
                }
                else
                {
                    ComCls.ShowAndReload(this, "该订单不可结束", Request.Url.ToString());
                }
            }
        }

       
    }
}