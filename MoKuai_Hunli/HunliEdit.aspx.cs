using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ebs.commons;
using ebs.dbml;

namespace ebs.MoKuai_Hunli
{
    public partial class HunliEdit :System.Web.UI.Page
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
            DivXiaofei.Visible = false;
            btApply.Visible = false;
            divModify.Visible = false;
            if (ID == 0)
            {
                tbZhuangtai.Text = "编辑";
                ddKehu.ddlForKehuCombination(currentUser.userName);
                if (currentUser.region != "All")
                {
                    ddHunliDidian.ddlForMenDian(currentUser.region);
                    ddHunliDidian.setReadonly();
                }
                else ddHunliDidian.ddlForMenDian(); 
                ddSales.ddlForSales(currentUser.userName, "All", "");
                ddSales.setReadonly();
             
                ddYishiChangdi.ddlForYishiChangdi();
                ddYanhuiting.ddlForYanhuiting();
                ddHunliTaocan.ddlForMenu();
                ddHunqinTaocan.ddlForHunqinMenu();
            }
            if (ID != 0)
            {
                using (ebsDBData db =new ebsDBData())
                {
                    if (db.WeddingOrders.Any(q=>q.ID == ID))
                    {
                        WeddingOrders WO = db.WeddingOrders.First(q => q.ID == ID);
                        Customers WOsCus = db.Customers.First(q=>q.ID == WO.MainID);

                        hdID.Value = ID.ToString();

                        ddKehu.ddlForKehuCombination("",WO.MainID.ToString());
                        ddHunliDidian.ddlForMenDian(WO.HunliDidian);
                        ddSales.ddlForSales(WO.Sales, "All", "");
                        ddSales.setReadonly();
                        ddYishiChangdi.ddlForYishiChangdi(WO.YishiChangdi);
                        //YISHICHANGDI select2 JS trigger 
                        ddYanhuiting.ddlForYanhuiting();
                     
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Select2Sel", "Select2Sel('" + WO.Yanhuiting + "');", true);
                        ddHunliTaocan.ddlForMenu(WO.HunyanTaocan);
                        ddHunqinTaocan.ddlForHunqinMenu(WO.HunqinTaocan);
                        ddWuWanCan.SelectedValue = WO.WuWanyan;
                        //tbPaymentid.Text = WO.paymentid;
                       
                        tbHetongBianhao.Text = WO.HetongID;
                        tbHetongRiqi.Text = WO.HetongDate.ToString("yyyy-MM-dd");
                        tbXinLangName.Text = WO.XinlangName;
                        tbXinLangShouji.Text = WO.XinLangMB;
                        tbXinNiangName.Text = WO.XinNiangName;
                        tbXinNiangShouji.Text = WO.XinNiangMB;
                        if (WO.HunliDate == null)
                        {
                            tbHunliRiqi.Text = WOsCus.EventDate;
                        }
                        else tbHunliRiqi.Text = WO.HunliDate.Value.ToString("yyyy-MM-dd");
                        tbQudao.Text = WOsCus.Source;
                        tbZhuangtai.Text = WO.Zhuangtai;
                       
                        
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

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "initPageIfhasID", "initPageIfhasID();", true);


                       
                        if (WO.Zhuangtai == "审批完成" && WO.Sales == currentUser.userName)
                        {
                            divModify.Visible = true;
                            btApply.Visible = true;
                        }
                        if (WO.Zhuangtai == "编辑" && WO.Sales == currentUser.userName)
                        {
                            btSave.Visible = true;
                            btSubmit.Visible = true;
                        }
                        else
                        {
                            btSubmit.Visible = false;
                            btSave.Visible = false;
                        }


                        if (WO.WeddingExtraPayContent.Count > 0 && WO.WeddingPayment.Count >0)
                        {
                            DivXiaofei.Visible = true;
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
                            if (WO.WeddingPayment.Count(q => q.PayType == "e") > 0)
                            {
                                var wepE = WO.WeddingPayment.Where(q => q.PayType == "e");
                                lbExYifujineHQ.Text = wepE.Sum(q => q.ShishouHQ).ToString();
                                lbExYifujineHY.Text = wepE.Sum(q => q.ShishouHY).ToString();
                            }
                            var paid = WO.WeddingPayment.Where(q => q.PayType.Length == 1);
                            lbZXF_HY_Yifu.Text = paid.Sum(q => q.ShishouHY).ToString();
                            lbZXF_HQ_Yifu.Text = paid.Sum(q => q.ShishouHQ).ToString();
                            lbZXF_heji_Yifu.Text = (paid.Sum(q => q.ShishouHQ) + paid.Sum(q => q.ShishouHY)).ToString();
                        }

                        //yifu
                        var r = new ConvertStringsInDB.paymentByIDandType(WO.ID, "1");
                        lbYifuHY1.Text = r.YiFuHYbyPay;
                        lbYifuHQ1.Text = r.YiFuHQbyPay;

                        r = new ConvertStringsInDB.paymentByIDandType(WO.ID, "2");
                        lbYifuHY2.Text = r.YiFuHYbyPay;
                        lbYifuHQ2.Text = r.YiFuHQbyPay;

                        r = new ConvertStringsInDB.paymentByIDandType(WO.ID, "3");
                        lbYifuHY3.Text = r.YiFuHYbyPay;
                        lbYifuHQ3.Text = r.YiFuHQbyPay;



                    }
                   
                }
              
            }

        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (ID == 0)
                {
                    NewWeddingOrder("编辑");
                }
                if (ID != 0)
                {
                    UpdateWeddingOrder("编辑");
                }
                Response.Redirect("HunliList.aspx");
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ThrowError", "ThrowError('" + ex.Message + "');", true);
            }
        }

        protected void btSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ID == 0)
                {
                    NewWeddingOrder("提交");
                }
                if (ID != 0)
                {
                    UpdateWeddingOrder("提交");
                }
                Response.Redirect("HunliList.aspx");
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ThrowError", "ThrowError('" + ex.Message + "');", true);
            }
        }

        private void UpdateWeddingOrder(string StatusStep)
        {
            using (ebsDBData db = new ebsDBData())
            {



                if (db.WeddingOrders.Any(q => q.ID == ID))
                {
                    WeddingOrders WO = db.WeddingOrders.First(q => q.ID == ID);
                    WO.MainID = Convert.ToInt32(ddKehu.SelectedValue);
                    WO.Zhuangtai = "待审批";
                    if (StatusStep == "编辑")
                    {
                        WO.Zhuangtai = "编辑";
                    }
                    WO.HunliDidian = ddHunliDidian.SelectedValue;
                    WO.Sales = ddSales.SelectedValue;
                    WO.HetongID = tbHetongBianhao.Text;
                    WO.HetongDate = Convert.ToDateTime(tbHetongRiqi.Text);
                    WO.HunliDate = Convert.ToDateTime(tbHunliRiqi.Text);
                    WO.XinlangName = tbXinLangName.Text;
                    WO.XinLangMB = tbXinLangShouji.Text;
                    WO.XinNiangName = tbXinNiangName.Text;
                    WO.XinNiangMB = tbXinNiangShouji.Text;
                    WO.YishiChangdi = ddYishiChangdi.SelectedValue;
                    WO.Yanhuiting = Request.Form["ctl00$MainContent$ddYanhuiting"].ToString();
                    WO.WuWanyan = ddWuWanCan.SelectedValue;
                    WO.CaijinDanjia = Convert.ToDouble(Request.Form["ctl00$MainContent$lbCaijinDanjia"].ToString());
                    //WO.paymentid = tbPaymentid.Text;

                    //Pay Contents
                    WO.HunyanTaocan = ddHunliTaocan.SelectedValue;
                    WO.CaijinDanjia = Convert.ToDouble(lbCaijinDanjia.Text);
                    WO.CaijinZhuoshu = tbCaijinZhuoshu.Text;
                    WO.CaijinZhekou = tbCaijinZhekou.Text;
                    WO.JiushuiDanjia = tbJiushuiDanjia.Text;
                    WO.JiushuiZhuoshu = tbJiushuiZhuoshu.Text;
                    WO.JiushuiZhekou = tbJiushuiZhekou.Text;
                    WO.HunqinTaocan = ddHunqinTaocan.SelectedValue;
                    WO.Hunqin = tbHunqin.Text;
                    WO.Zhuohua = tbZhuohua.Text;
                    WO.Qita = tbQita.Text;
                    WO.HunyanZongJine = hdHunyanZongjine.Value;

                    //Payment
                    WO.DingjinDate = Convert.ToDateTime(tbFirstPayDate.Text);
                    WO.DingjinPercentHunyan = Convert.ToDouble(tbFirstPayBaiHY.Text);
                    WO.DingjinPercentHunqin = Convert.ToDouble(tbFirstPayBaiHQ.Text);
                    WO.ZhongkuanDate = Convert.ToDateTime(tbSecondPayDate.Text);
                    WO.ZhongkuanPercentHunyan = Convert.ToDouble(tbSecondPayBaiHY.Text);
                    WO.ZhongkuanPercentHunqin = Convert.ToDouble(tbSecondPayBaiHQ.Text);
                    WO.WeikuanDate = Convert.ToDateTime(tbThirdPayDate.Text);
                    WO.WeikuanPercentHunyan = Convert.ToDouble(tbThirdPayBaiHY.Text);
                    WO.WeikuanPercentHunqin = Convert.ToDouble(tbThirdPayBaiHQ.Text);

                    WO.DingjinAmountHunyan = Convert.ToDouble(tbFirstPayJineHY.Text);
                    WO.DingjinAmoutHunqin = Convert.ToDouble(tbFirstPayJineHQ.Text);
                    WO.ZhongkuanAmountHunqin = Convert.ToDouble(tbSecondPayJineHQ.Text);
                    WO.ZhongkuanAmountHunyan = Convert.ToDouble(tbSecondPayJineHY.Text);
                    WO.WeikuanAmountHunqin = Convert.ToDouble(tbThirdPayJineHQ.Text);
                    WO.WeikuanAmountHunyan = Convert.ToDouble(tbThirdPayJineHY.Text);

                    //Services
                    WO.HunYanServices = tokenfieldHY.Value;
                    WO.HunQinServices = tokenfieldHQ.Value;
                    WO.BuchongXinxi = tbBuchongXinxi.Text;

                    Customers Cus = db.Customers.First(q => q.ID == Convert.ToInt32(ddKehu.SelectedValue));
                    // Cus.EventDate = tbHunliRiqi.Text;

                    //Audit
                    if (StatusStep == "提交")
                    {
                        double Zongjine = Convert.ToDouble(hdHunyanZongjine.Value);

                        tbSysAuditConfig AuditBySupervisor = db.tbSysAuditConfig.First(q => q.NeedRole == "婚宴销售主管");
                        if (Zongjine >= AuditBySupervisor.ConditionMin * StandardPrice && Zongjine < AuditBySupervisor.ConditionMax * StandardPrice)
                        {
                            AuditRecords ar = new AuditRecords();
                            ar.AuditLevel = "销售主管";
                            ar.AuditResult = "待审批";
                            ar.AuditSuggest = "";
                            ar.AuditType = "初次提交";
                            ar.AuditUser = "";
                            ar.AuditPriority = AuditBySupervisor.priority;
                            WO.AuditRecords.Add(ar);
                        }

                        tbSysAuditConfig AuditByGM = db.tbSysAuditConfig.First(q => q.NeedRole == "总经理");
                        if (Zongjine >= AuditByGM.ConditionMin * StandardPrice && Zongjine < AuditByGM.ConditionMax * StandardPrice)
                        {
                            AuditRecords ar = new AuditRecords();
                            ar.AuditLevel = "总经理";
                            ar.AuditResult = "待审批";
                            ar.AuditSuggest = "";
                            ar.AuditType = "初次提交";
                            ar.AuditUser = "";
                            ar.AuditPriority = AuditByGM.priority;
                            WO.AuditRecords.Add(ar);
                        }

                        tbSysAuditConfig AuditByFinance = db.tbSysAuditConfig.First(q => q.NeedRole == "财务");
                        {
                            if (Zongjine >= AuditByFinance.ConditionMin * StandardPrice)
                            {
                                AuditRecords ar = new AuditRecords();
                                ar.AuditLevel = "财务";
                                ar.AuditResult = "待审批";
                                ar.AuditSuggest = "";
                                ar.AuditType = "初次提交";
                                ar.AuditUser = "";
                                ar.AuditPriority = AuditByFinance.priority;
                                WO.AuditRecords.Add(ar);
                            }

                        tbSysAuditConfig AuditBySupervisor = db.tbSysAuditConfig.First(q => q.NeedRole == "婚宴销售主管");
                        //if (Zongjine >= AuditBySupervisor.ConditionMin * StandardPrize && Zongjine < AuditBySupervisor.ConditionMax * StandardPrize)
                        //{
                        AuditRecords ar = new AuditRecords();
                        ar.AuditLevel = "主管";
                        ar.AuditResult = "待审批";
                        ar.AuditSuggest = "";
                        ar.AuditType = "初次提交";
                        ar.AuditUser = "";
                        ar.AuditPriority = AuditBySupervisor.priority;
                        WO.AuditRecords.Add(ar);
                        //}

                        //tbSysAuditConfig AuditByGM = db.tbSysAuditConfig.First(q => q.NeedRole == "总经理");
                        //if (Zongjine >= AuditByGM.ConditionMin * StandardPrize && Zongjine < AuditByGM.ConditionMax * StandardPrize)
                        //{
                        //    AuditRecords ar = new AuditRecords();
                        //    ar.AuditLevel = "总经理";
                        //    ar.AuditResult = "待审批";
                        //    ar.AuditSuggest = "";
                        //    ar.AuditType = "初次提交";
                        //    ar.AuditUser = "";
                        //     ar.AuditPriority = AuditByGM.priority;
                        //    WO.AuditRecords.Add(ar);
                        //}

                        //tbSysAuditConfig AuditByFinance = db.tbSysAuditConfig.First(q => q.NeedRole == "财务");
                        //{
                        //    if (Zongjine >= AuditByFinance.ConditionMin * StandardPrize && Zongjine < AuditByFinance.ConditionMax * StandardPrize)
                        //    {
                        //        AuditRecords ar = new AuditRecords();
                        //        ar.AuditLevel = "财务";
                        //        ar.AuditResult = "待审批";
                        //        ar.AuditSuggest = "";
                        //        ar.AuditType = "初次提交";
                        //        ar.AuditUser = "";
                        //        ar.AuditPriority = AuditByFinance.priority;
                        //        WO.AuditRecords.Add(ar);
                        //    }

                        //}
                        tbSysAuditConfig AuditByWenyuan = db.tbSysAuditConfig.First(q => q.NeedRole == "文员");
                        //{
                        //if (Zongjine >= AuditByWenyuan.ConditionMin * StandardPrize && Zongjine <= AuditByWenyuan.ConditionMax * StandardPrize)
                        //{
                        AuditRecords ar_wy = new AuditRecords();
                        ar_wy.AuditLevel = "文员";
                        ar_wy.AuditResult = "待审批";
                        ar_wy.AuditSuggest = "";
                        ar_wy.AuditType = "初次提交";
                        ar_wy.AuditUser = "";
                        ar_wy.AuditPriority = AuditByWenyuan.priority;
                        WO.AuditRecords.Add(ar_wy);
                        //}

                        // }

                        if (WO.AuditRecords != null)
                        {
                            db.AuditRecords.DeleteAllOnSubmit(db.AuditRecords.Where(q => q.OrderID == WO.ID));
                            //db.AuditRecords.InsertAllOnSubmit(WO.AuditRecords);
                        }

                        //do a record
                        var aRecord = new WeddingOrdersLogs();
                        aRecord.Date = DateTime.Now;
                        aRecord.UserName = currentUser.userName;
                        aRecord.UserRole = currentUser.roles;
                        aRecord.ActionName = "提交审批";
                        aRecord.ActionType = "初次提交";
                        aRecord.Comments = "";
                        WO.WeddingOrdersLogs.Add(aRecord);

                    }
                    db.SubmitChanges();

                }

            }

        }

        private void NewWeddingOrder(string StatusStep)
        {

            using (ebsDBData db = new ebsDBData())
            {
                WeddingOrders WO = new WeddingOrders();
                WO.MainID = Convert.ToInt32(ddKehu.SelectedValue);
                WO.Zhuangtai = "待审批";
                if (StatusStep == "编辑")
                {
                    WO.Zhuangtai = "编辑";
                }
                WO.HunliDidian = ddHunliDidian.SelectedValue;
                WO.Sales = ddSales.SelectedValue;
                WO.HetongID = tbHetongBianhao.Text;
                WO.HetongDate = Convert.ToDateTime(tbHetongRiqi.Text);
                WO.HunliDate = Convert.ToDateTime(tbHunliRiqi.Text);
                WO.XinlangName = tbXinLangName.Text;
                WO.XinLangMB = tbXinLangShouji.Text;
                WO.XinNiangName = tbXinNiangName.Text;
                WO.XinNiangMB = tbXinNiangShouji.Text;
                WO.YishiChangdi = ddYishiChangdi.SelectedValue;
                WO.Yanhuiting = Request.Form["ctl00$MainContent$ddYanhuiting"].ToString();
                WO.WuWanyan = ddWuWanCan.SelectedValue;
                // WO.paymentid = tbPaymentid.Text;

                //Pay Contents
                WO.HunyanTaocan = ddHunliTaocan.SelectedValue;
                WO.CaijinDanjia = Convert.ToDouble(Request.Form["ctl00$MainContent$lbCaijinDanjia"].ToString());
                WO.CaijinZhuoshu = tbCaijinZhuoshu.Text;
                WO.CaijinZhekou = tbCaijinZhekou.Text;
                WO.JiushuiDanjia = tbJiushuiDanjia.Text;
                WO.JiushuiZhuoshu = tbJiushuiZhuoshu.Text;
                WO.JiushuiZhekou = tbJiushuiZhekou.Text;
                WO.HunqinTaocan = ddHunqinTaocan.SelectedValue;
                WO.Hunqin = tbHunqin.Text.Trim() == "" ? "0" : tbHunqin.Text;
                WO.Zhuohua = tbZhuohua.Text.Trim() == "" ? "0" : tbZhuohua.Text;
                WO.Qita = tbQita.Text.Trim() == "" ? "0" : tbQita.Text;
                WO.HunyanZongJine = hdHunyanZongjine.Value;

                //Payment
                WO.DingjinDate = Convert.ToDateTime(tbFirstPayDate.Text);
                WO.DingjinPercentHunyan = Convert.ToDouble(tbFirstPayBaiHY.Text);
                WO.DingjinPercentHunqin = Convert.ToDouble(tbFirstPayBaiHQ.Text);
                WO.ZhongkuanDate = Convert.ToDateTime(tbSecondPayDate.Text);
                WO.ZhongkuanPercentHunyan = Convert.ToDouble(tbSecondPayBaiHY.Text);
                WO.ZhongkuanPercentHunqin = Convert.ToDouble(tbSecondPayBaiHQ.Text);
                WO.WeikuanDate = Convert.ToDateTime(tbThirdPayDate.Text);
                WO.WeikuanPercentHunyan = Convert.ToDouble(tbThirdPayBaiHY.Text);
                WO.WeikuanPercentHunqin = Convert.ToDouble(tbThirdPayBaiHQ.Text);

                WO.DingjinAmountHunyan = Convert.ToDouble(tbFirstPayJineHY.Text);
                WO.DingjinAmoutHunqin = Convert.ToDouble(tbFirstPayJineHQ.Text);
                WO.ZhongkuanAmountHunqin = Convert.ToDouble(tbSecondPayJineHQ.Text);
                WO.ZhongkuanAmountHunyan = Convert.ToDouble(tbSecondPayJineHY.Text);
                WO.WeikuanAmountHunqin = Convert.ToDouble(tbThirdPayJineHQ.Text);
                WO.WeikuanAmountHunyan = Convert.ToDouble(tbThirdPayJineHY.Text);

                //Services
                WO.HunYanServices = tokenfieldHY.Value;
                WO.HunQinServices = tokenfieldHQ.Value;
                WO.BuchongXinxi = tbBuchongXinxi.Text;

                db.WeddingOrders.InsertOnSubmit(WO);

                Customers Cus = db.Customers.First(q => q.ID == Convert.ToInt32(ddKehu.SelectedValue));
                //Cus.EventDate = tbHunliRiqi.Text;

                //Audit
                if (StatusStep == "提交")
                {
                    double Zongjine = Convert.ToDouble(hdHunyanZongjine.Value);
                    //double StandardPrice = Convert.ToDouble(ComCls.getAppSetting("lowCost"));

                    tbSysAuditConfig AuditBySupervisor = db.tbSysAuditConfig.First(q => q.NeedRole == "婚宴销售主管");
                    //if (Zongjine >= AuditBySupervisor.ConditionMin * StandardPrice && Zongjine < AuditBySupervisor.ConditionMax * StandardPrice)
                    //{
                    AuditRecords ar = new AuditRecords();
                    ar.AuditLevel = "销售主管";
                    ar.AuditResult = "待审批";
                    ar.AuditSuggest = "";
                    ar.AuditType = "初次提交";
                    ar.AuditUser = "";
                    ar.AuditPriority = AuditBySupervisor.priority;
                    WO.AuditRecords.Add(ar);
                    //}

                    //tbSysAuditConfig AuditByGM = db.tbSysAuditConfig.First(q => q.NeedRole == "总经理");
                    //if (Zongjine >= AuditByGM.ConditionMin * StandardPrice && Zongjine < AuditByGM.ConditionMax * StandardPrice)
                    //{
                    //    AuditRecords ar = new AuditRecords();
                    //    ar.AuditLevel = "总经理";
                    //    ar.AuditResult = "待审批";
                    //    ar.AuditSuggest = "";
                    //    ar.AuditType = "初次提交";
                    //    ar.AuditUser = "";
                    //    ar.AuditPriority = AuditByGM.priority;
                    //    WO.AuditRecords.Add(ar);
                    //}

                    //tbSysAuditConfig AuditByFinance = db.tbSysAuditConfig.First(q => q.NeedRole == "财务");
                    //{
                    //    if (Zongjine >= AuditByFinance.ConditionMin * StandardPrice)
                    //    {
                    //        AuditRecords ar = new AuditRecords();
                    //        ar.AuditLevel = "财务";
                    //        ar.AuditResult = "待审批";
                    //        ar.AuditSuggest = "";
                    //        ar.AuditType = "初次提交";
                    //        ar.AuditUser = "";
                    //        ar.AuditPriority = AuditByFinance.priority;
                    //        WO.AuditRecords.Add(ar);
                    //    }

                    //}
                    //{
                    //if (Zongjine >= AuditByWenyuan.ConditionMin * StandardPrice && Zongjine <= AuditByWenyuan.ConditionMax * StandardPrice)
                    AuditRecords ar_wy = new AuditRecords();
                    ar_wy.AuditLevel = "文员";
                    ar_wy.AuditResult = "待审批";
                    ar_wy.AuditSuggest = "";
                    ar_wy.AuditType = "初次提交";
                    ar_wy.AuditUser = "";
                    ar_wy.AuditPriority = AuditByWenyuan.priority;
                    WO.AuditRecords.Add(ar_wy);
                    // }

                    // }

                    if (WO.AuditRecords != null)
                    {
                        db.AuditRecords.DeleteAllOnSubmit(db.AuditRecords.Where(q => q.OrderID == WO.ID));
                        //db.AuditRecords.InsertAllOnSubmit(WO.AuditRecords);
                    }

                    var aRecord = new WeddingOrdersLogs();
                    aRecord.Date = DateTime.Now;
                    aRecord.UserName = currentUser.userName;
                    aRecord.UserRole = currentUser.roles;
                    aRecord.ActionName = "提交审批";
                    aRecord.ActionType = "初次提交";
                    aRecord.Comments = "";
                    WO.WeddingOrdersLogs.Add(aRecord);

                }
                db.SubmitChanges();

            }

        }

        private void UpdateContractWeddingOrder()
        {

            using (ebsDBData db = new ebsDBData())
            {
                try
                {
                    if (db.Connection.State != ConnectionState.Open)
                    {
                        db.Connection.Open();
                    }
                    db.Transaction = db.Connection.BeginTransaction();
                    WeddingOrders WO = new WeddingOrders();
                    WO.MainID = Convert.ToInt32(ddKehu.SelectedValue);
                    WO.Zhuangtai = "待审批";
                    WO.HunliDidian = ddHunliDidian.SelectedValue;
                    WO.Sales = ddSales.SelectedValue;
                    WO.HetongID = tbHetongBianhao.Text;
                    WO.HetongDate = Convert.ToDateTime(tbHetongRiqi.Text);
                    WO.HunliDate = Convert.ToDateTime(tbHunliRiqi.Text);
                    WO.XinlangName = tbXinLangName.Text;
                    WO.XinLangMB = tbXinLangShouji.Text;
                    WO.XinNiangName = tbXinNiangName.Text;
                    WO.XinNiangMB = tbXinNiangShouji.Text;
                    WO.YishiChangdi = ddYishiChangdi.SelectedValue;
                    WO.Yanhuiting = Request.Form["ctl00$MainContent$ddYanhuiting"].ToString();
                    WO.WuWanyan = ddWuWanCan.SelectedValue;
                    //WO.paymentid = tbPaymentid.Text;

                    //Pay Contents
                    WO.HunyanTaocan = ddHunliTaocan.SelectedValue;
                    WO.CaijinDanjia = Convert.ToDouble(Request.Form["ctl00$MainContent$lbCaijinDanjia"].ToString());
                    WO.CaijinZhuoshu = tbCaijinZhuoshu.Text;
                    WO.CaijinZhekou = tbCaijinZhekou.Text;
                    WO.JiushuiDanjia = tbJiushuiDanjia.Text;
                    WO.JiushuiZhuoshu = tbJiushuiZhuoshu.Text;
                    WO.JiushuiZhekou = tbJiushuiZhekou.Text;
                    WO.HunqinTaocan = ddHunqinTaocan.SelectedValue;
                    WO.Hunqin = tbHunqin.Text.Trim() == "" ? "0" : tbHunqin.Text;
                    WO.Zhuohua = tbZhuohua.Text.Trim() == "" ? "0" : tbZhuohua.Text;
                    WO.Qita = tbQita.Text.Trim() == "" ? "0" : tbQita.Text;
                    WO.HunyanZongJine = hdHunyanZongjine.Value;

                    //Payment
                    WO.DingjinDate = Convert.ToDateTime(tbFirstPayDate.Text);
                    WO.DingjinPercentHunyan = Convert.ToDouble(tbFirstPayBaiHY.Text);
                    WO.DingjinPercentHunqin = Convert.ToDouble(tbFirstPayBaiHQ.Text);
                    WO.ZhongkuanDate = Convert.ToDateTime(tbSecondPayDate.Text);
                    WO.ZhongkuanPercentHunyan = Convert.ToDouble(tbSecondPayBaiHY.Text);
                    WO.ZhongkuanPercentHunqin = Convert.ToDouble(tbSecondPayBaiHQ.Text);
                    WO.WeikuanDate = Convert.ToDateTime(tbThirdPayDate.Text);
                    WO.WeikuanPercentHunyan = Convert.ToDouble(tbThirdPayBaiHY.Text);
                    WO.WeikuanPercentHunqin = Convert.ToDouble(tbThirdPayBaiHQ.Text);

                    WO.DingjinAmountHunyan = Convert.ToDouble(tbFirstPayJineHY.Text);
                    WO.DingjinAmoutHunqin = Convert.ToDouble(tbFirstPayJineHQ.Text);
                    WO.ZhongkuanAmountHunqin = Convert.ToDouble(tbSecondPayJineHQ.Text);
                    WO.ZhongkuanAmountHunyan = Convert.ToDouble(tbSecondPayJineHY.Text);
                    WO.WeikuanAmountHunqin = Convert.ToDouble(tbThirdPayJineHQ.Text);
                    WO.WeikuanAmountHunyan = Convert.ToDouble(tbThirdPayJineHY.Text);

                    //Services
                    WO.HunYanServices = tokenfieldHY.Value;
                    WO.HunQinServices = tokenfieldHQ.Value;
                    WO.BuchongXinxi = tbBuchongXinxi.Text;

                    db.WeddingOrders.InsertOnSubmit(WO);

                    Customers Cus = db.Customers.First(q => q.ID == Convert.ToInt32(ddKehu.SelectedValue));
                    Cus.EventDate = tbHunliRiqi.Text;

                   


                    //Audit
                    db.SubmitChanges();

                    int NewID = WO.ID;
                    tbWeddingOrdersRevision wor = new tbWeddingOrdersRevision();

                    var oldWO = db.WeddingOrders.First(q=>q.ID == ID);

                    //AddedPayment
                    if (oldWO.WeddingExtraPayContent.Count > 0)
                    {
                        WeddingExtraPayContent exPayment = new WeddingExtraPayContent();
                         WeddingExtraPayContent oldExPayment = oldWO.WeddingExtraPayContent.First();
                        exPayment.Caijin = oldExPayment.Caijin;
                        exPayment.CaijinZhuoshu = oldExPayment.CaijinZhuoshu;
                        exPayment.Jiushui = oldExPayment.Jiushui;
                        exPayment.JiushuiZhuoshu = oldExPayment.JiushuiZhuoshu;
                        exPayment.WeikuanDikou = oldExPayment.WeikuanDikou;
                        exPayment.EXzhuohua = oldExPayment.EXzhuohua;
                        exPayment.EXqita = oldExPayment.EXqita;
                        WO.WeddingExtraPayContent.Add(exPayment);
                    }
                   

                    oldWO.Zhuangtai = "已覆盖";
                    int ContractID = 0;
                    if (db.tbWeddingOrdersRevision.Any(q=>q.OrderID == oldWO.ID))
                    {
                        ContractID = db.tbWeddingOrdersRevision.First(q => q.OrderID == oldWO.ID).ContractID;
                    }
                    else
                    {
                        ContractID = new Random().Next(0,100000);
                        tbWeddingOrdersRevision oldWor = new tbWeddingOrdersRevision();
                        oldWor.ContractID = ContractID;
                        oldWor.OrderID = oldWO.ID;
                        oldWor.OrderName = oldWO.HetongDate.ToString("yyyy-MM-dd");
                        db.tbWeddingOrdersRevision.InsertOnSubmit(oldWor);
                    }
                    wor.ContractID = ContractID;
                    wor.OrderName = DateTime.Now.ToString("yyyy-MM-dd");
                    wor.OrderID = WO.ID;
                    db.tbWeddingOrdersRevision.InsertOnSubmit(wor);
                    db.SubmitChanges();

                    var AuditNeed = db.tbSysAuditModifyConfig.OrderBy(q=>q.Priority).ToList();
                    foreach (var item in AuditNeed)
                    {
                        AuditRecords ar = new AuditRecords();
                        ar.AuditLevel = item.Role;
                        ar.AuditPriority = item.Priority;
                        ar.OrderID = WO.ID;
                        ar.AuditResult = "待审批";
                        ar.AuditUser = "";
                        ar.AuditSuggest = "";
                        ar.AuditType = ddlRevisionType.SelectedValue;
                        WO.AuditRecords.Add(ar);
                    }

                    var aRecord = new WeddingOrdersLogs();
                    aRecord.Date = DateTime.Now;
                    aRecord.UserName = currentUser.userName;
                    aRecord.UserRole = currentUser.roles;
                    aRecord.ActionName = "提交审批";
                    aRecord.ActionType = ddlRevisionType.SelectedValue;
                    aRecord.Comments = tbRevisionReason.Text;
                    WO.WeddingOrdersLogs.Add(aRecord);

                    //WeddingPayment
                    foreach (var item in oldWO.WeddingPayment.ToList())
                    {
                        WeddingPayment wp = new WeddingPayment();
                        wp.PayType = item.PayType;
                        wp.PayDate = item.PayDate;
                        wp.ShishouHQ = item.ShishouHQ;
                        wp.ShishouHY = item.ShishouHY;
                        wp.Zhuangtai = "可编辑";
                        WO.WeddingPayment.Add(wp);
                    }
                   

                    db.SubmitChanges();
                    db.Transaction.Commit();
                    Response.Redirect("HunliEdit.aspx?ID=" + WO.ID);
                }
                catch (System.Exception ex)
                {
                    db.Transaction.Rollback();
                    throw ex;
                }

            }
           
          

        }

        protected void btApply_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (ID != 0)
                {
                    //NewWeddingOrder("提交");
                    UpdateContractWeddingOrder();
                }
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ThrowError", "ThrowError('" + ex.Message + "');", true);
            }
        }
    }
}