using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.commons;
using ebs.dbml;

namespace ebs.MoKuai_Hunli
{
    public partial class HunliAudit : System.Web.UI.Page
    {
        public int ID
        {
            get { return ViewState["ID"] != null ? Convert.ToInt32(ViewState["ID"].ToString()) : 1; }
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
            if (ID != 0)
            {
                using (ebsDBData db = new ebsDBData())
                {
                    if (db.WeddingOrders.Any(q => q.ID == ID))
                    {
                        WeddingOrders WO = db.WeddingOrders.First(q => q.ID == ID);
                        Customers WOsCus = db.Customers.First(q => q.ID == WO.MainID);
                        ddKehu.Text = WOsCus.CustomerName + "/" + WOsCus.Telephone + "/" + WOsCus.Sales.ToDisplayName() + "/" + WOsCus.ZixunDiDian;
                        ddHunliDidian.Text = WO.HunliDidian;
                        ddSales.Text = WO.Sales.ToDisplayName();
                        ddYishiChangdi.Text = WO.YishiChangdi;
                        ddYanhuiting.Text = WO.Yanhuiting.Replace(",", " ");
                        ddHunliTaocan.Text = WO.HunyanTaocan;
                        ddHunqinTaocan.Text = WO.HunqinTaocan;
                        ddWuWanCan.Text = WO.WuWanyan;

                        hdID.Value = ID.ToString();

                        tbZhuangtai.Text = WO.Zhuangtai;
                        tbHetongBianhao.Text = WO.HetongID;
                        tbHetongRiqi.Text = WO.HetongDate.ToString("yyyy-MM-dd");
                        tbXinLangName.Text = WO.XinlangName;
                        tbXinLangShouji.Text = WO.XinLangMB;
                        tbXinNiangName.Text = WO.XinNiangName;
                        tbXinNiangShouji.Text = WO.XinNiangMB;
                        //tbHunliRiqi.Text = WOsCus.EventDate;
                        if (WO.HunliDate == null)
                        {
                            tbHunliRiqi.Text = WOsCus.EventDate;
                        }
                        else tbHunliRiqi.Text = WO.HunliDate.Value.ToString("yyyy-MM-dd");
                        tbQudao.Text = WOsCus.Source;
                        //tbPaymentid.Text = WO.paymentid;


                        //Pay Contents
                        lbCaijinDanjia.Text = WO.CaijinDanjia.ToString();
                        tbCaijinZhuoshu.Text = WO.CaijinZhuoshu;
                        tbCaijinZhekou.Text = WO.CaijinZhekou.ToString();

                        tbJiushuiDanjia.Text = WO.JiushuiDanjia;
                        tbJiushuiZhuoshu.Text = WO.JiushuiZhuoshu;
                        tbJiushuiZhekou.Text = WO.JiushuiZhekou;

                        tbHunqin.Text = WO.Hunqin;
                        tbZhuohua.Text = WO.Zhuohua;
                        tbQita.Text = WO.Qita;

                        tbFirstPayDate.Text = WO.DingjinDate.ToString("yyyy-MM-dd");
                        tbFirstPayBaiHQ.Text = WO.DingjinPercentHunqin.ToString();
                        tbFirstPayBaiHY.Text = WO.DingjinPercentHunyan.ToString();

                        tbSecondPayDate.Text = WO.ZhongkuanDate.ToString("yyyy-MM-dd");
                        tbSecondPayBaiHQ.Text = WO.ZhongkuanPercentHunqin.ToString();
                        tbSecondPayBaiHY.Text = WO.ZhongkuanPercentHunyan.ToString();

                        tbThirdPayDate.Text = WO.WeikuanDate.ToString("yyyy-MM-dd");
                        tbThirdPayBaiHQ.Text = WO.WeikuanPercentHunqin.ToString();
                        tbThirdPayBaiHY.Text = WO.WeikuanPercentHunyan.ToString();

                        tbFirstPayJineHQ.Text = WO.DingjinAmoutHunqin.ToString();
                        tbFirstPayJineHY.Text = WO.DingjinAmountHunyan.ToString();
                        tbSecondPayJineHQ.Text = WO.ZhongkuanAmountHunqin.ToString();
                        tbSecondPayJineHY.Text = WO.ZhongkuanAmountHunyan.ToString();
                        tbThirdPayJineHQ.Text = WO.WeikuanAmountHunqin.ToString();
                        tbThirdPayJineHY.Text = WO.WeikuanAmountHunyan.ToString();

                        tokenfieldHQ.Value = WO.HunQinServices;
                        tokenfieldHY.Value = WO.HunYanServices;
                        tbBuchongXinxi.Text = WO.BuchongXinxi;

                        //如果订单结束 不可保存
                        if (WO.Zhuangtai == "结束")
                        {
                            btUpdate.Visible = false;
                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "initPageIfhasID", "initPageIfhasID();", true);


                        //exPay
                        if (WO.WeddingExtraPayContent.Count>0)
                        {
                            var wep = WO.WeddingExtraPayContent.First();
                            tbExJiushui.Text = wep.Jiushui.ToString("0");
                            tbExJS_ZS.Text = wep.JiushuiZhuoshu.ToString();
                            tbExCaijin.Text = wep.Caijin.ToString();
                            tbExCJ_ZS.Text = wep.CaijinZhuoshu.ToString();
                            tbWeikuanDikou.Text = wep.WeikuanDikou.ToString();
                            tbExZhuohua.Text = wep.EXzhuohua.ToString();
                            tbExQita.Text = wep.EXqita.ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "setZongxiaofei_Jinge", "setZongxiaofei_Jinge();", true);
                            //exPay Payed
                            if (WO.WeddingPayment.Count(q=>q.PayType == "e") >0)
                            {
                                var wepE =  WO.WeddingPayment.Where(q=>q.PayType=="e");
                                lbExYifujineHQ.Text = wepE.Sum(q => q.ShishouHQ).ToString();
                                lbExYifujineHY.Text = wepE.Sum(q => q.ShishouHY).ToString();

                                
                            }

                        }
                        var paid = WO.WeddingPayment.Where(q => q.PayType.Length == 1);
                        lbZXF_HY_Yifu.Text = paid.Sum(q => q.ShishouHY).ToString();
                        lbZXF_HQ_Yifu.Text = paid.Sum(q => q.ShishouHQ).ToString();
                        lbZXF_heji_Yifu.Text = (paid.Sum(q => q.ShishouHQ) + paid.Sum(q => q.ShishouHY)).ToString();
                        
                    }

                }

            }

        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (ebsDBData db = new ebsDBData())
                {
                    if (db.WeddingExtraPayContent.Count(q => q.OrderID == ID) > 0)
                    {
                        var wep = db.WeddingExtraPayContent.First(q => q.OrderID == ID);
                        wep.OrderID = ID;
                        wep.Jiushui = Convert.ToDouble(tbExJiushui.Text);
                        wep.JiushuiZhuoshu = Convert.ToDouble(tbExJS_ZS.Text);
                        wep.Caijin = Convert.ToDouble((tbExCaijin.Text));
                        wep.CaijinZhuoshu = Convert.ToDouble(tbExCJ_ZS.Text);
                        wep.WeikuanDikou = Convert.ToDouble(tbWeikuanDikou.Text);
                        wep.EXzhuohua = Convert.ToDouble(tbExZhuohua.Text);
                        wep.EXqita = Convert.ToDouble(tbExQita.Text);
                    }
                    else
                    {
                        WeddingExtraPayContent wep = new WeddingExtraPayContent();
                        wep.OrderID = ID;
                        wep.Jiushui = Convert.ToDouble(tbExJiushui.Text);
                        wep.JiushuiZhuoshu = Convert.ToDouble(tbExJS_ZS.Text);
                        wep.Caijin = Convert.ToDouble((tbExCaijin.Text));
                        wep.CaijinZhuoshu = Convert.ToDouble(tbExCJ_ZS.Text);
                        wep.WeikuanDikou = Convert.ToDouble(tbWeikuanDikou.Text);
                        wep.EXzhuohua = Convert.ToDouble(tbExZhuohua.Text);
                        wep.EXqita = Convert.ToDouble(tbExQita.Text);
                        db.WeddingExtraPayContent.InsertOnSubmit(wep);
                    }
                    db.SubmitChanges();

                }
                Response.Redirect(Request.Url.ToString());
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ThrowError", "ThrowError('" + ex.Message + "');", true);
            }
          
        } 

    }
}