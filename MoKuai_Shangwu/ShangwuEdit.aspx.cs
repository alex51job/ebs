using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using ebs.dbml;
using ebs.commons;
using ebs.Tools;

namespace ebs.MoKuai_Shangwu
{
    public partial class ShangwuEdit : System.Web.UI.Page
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
        public int KehuID
        {

            get { return ViewState["KehuID"] != null ? Convert.ToInt32(ViewState["KehuID"].ToString()) : 0; }
            set { ViewState["KehuID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ViewState["ID"] = Request.QueryString["ID"];
                initPage();
                setButtons();
                BussinessTabs.setBussinessOrderTabs(Request.Url.ToString(), currentUser.roles, ID);
            }
           
        }

        public void initPage()
        {
            setInfoList();
            
            if (ID == 0)
            {
                tbZhuangtai.Text = "编辑";
                if (currentUser.region !="All")
                {
                    ddEventVenue.ddlForMenDian(currentUser.region);
                    ddEventVenue.setReadonly();
                }
                else ddEventVenue.ddlForMenDian();
                ddSales.ddlForSales(currentUser.userName, "All", "");
                ddSales.setReadonly();
                ddEventType.ddlForEventType();
                hdID.Value = "0";
                ddKehu.ddlForKehuCombination(currentUser.userName);
            }
            else
            {
                using (ebsDBData db = new ebsDBData())
                {
                    if (db.Bussiness.Count(q => q.ID == ID) < 1)
                    {
                        return;
                    }
                    Bussiness BO = db.Bussiness.First(q => q.ID == ID);

                    ddKehu.ddlForKehuCombination("", BO.MainID.ToString());
                    hdID.Value = BO.ID.ToString();
                    tbZhuangtai.Text = BO.Zhuangtai;
                    tbHetongBianhao.Text = BO.HetongID;
                    tbHetongRiqi.Text = BO.HetongDate.ToString("yyyy-MM-dd");
                    tbCompany.Text = BO.Company;
                    tbQudao.Text = BO.Qudaobeizhu;
                    if (BO.Qudaobeizhu.Trim() == "")
                    {
                        tbQudao.Text = db.Customers.First(q => q.ID == BO.MainID).Source;
                    }
                    tbLianxiren1.Text = BO.Lianxiren1;
                    tbLianxiren1_Shouji.Text = BO.Lianxiren1_Shouji;
                    tbLianxiren1_Zuoji.Text = BO.Lianxiren1_Zuoji;
                    tbLianxiren2.Text = BO.Lianxiren2;
                    tbLianxiren2_Shouji.Text = BO.Lianxiren2_Shouji;
                    tbLianxiren2_Zuoji.Text = BO.Lianxiren2_Zuoji;
                    ddEventVenue.ddlForMenDian(BO.EventVenue);
                    ddSales.ddlForSales(BO.Sales, "All");
                    ddSales.setReadonly();
                    tbEventName.Text = BO.EventName;
                    tbEventRiqi.Text = BO.EventDate.ToString("yyyy-MM-dd HH:mm");
                    ddEventType.ddlForEventType(BO.EventType);
                    tbDabaoFee.Text = BO.Dabaodanjia.ToString();
                    tbDabaoRen.Text = BO.Dabaodanjia_ren.ToString();
                    if (BO.Otherfee1 != null && BO.Otherfee1 != "")
                    {
                        OtherFeeName1.Text = BO.Otherfee1;
                        OtherFeeValue1.Text = BO.Otherfee1_Amount.ToString();
                    }
                    if (BO.Otherfee2 != null && BO.Otherfee2 != "")
                    {
                        OtherFeeName2.Text = BO.Otherfee2;
                        OtherFeeValue2.Text = BO.Otherfee2_Amount.ToString();
                    }
                    if (BO.Otherfee3 != null && BO.Otherfee3 != "")
                    {
                        OtherFeeName3.Text = BO.Otherfee3;
                        OtherFeeValue3.Text = BO.Otherfee3_Amount.ToString();
                    }
                    Double yingfu = BO.BussinessPayment.Where(q => (q.PayType == "转账" || q.PayType == "现金") && q.Zhuangtai == "审批完成").Sum(q => q.PayAmount) - BO.BussinessPayment.Where(q => (q.PayType == "赔款" || q.PayType == "退款") && q.Zhuangtai == "审批完成").Sum(q => q.PayAmount);
                    tbYifuJine.Text = yingfu.ToString();
                    tbZhekou.Text = BO.Zhekou.ToString();
                    tbFanyongAmount.Text = BO.Fanyong.ToString();

                    var jser = new JavaScriptSerializer();
                    List<eventPayBussiness> eps = new List<eventPayBussiness>();
                    foreach (var item in BO.BussinessEventFormat)
                    {
                        eventPayBussiness ep = new eventPayBussiness();
                        ep.Venue = item.Venue;
                        ep.EventType = item.EventFormat;
                        ep.Yongcan = item.Yongcan;
                        ep.Dajianfei = (float)(item.Dajianfei ?? 0);
                        ep.Changdifei = (float)(item.Changdifei ?? 0);
                        ep.canbiao = (float)(item.Canbiao ?? 0);
                        ep.shuliangA = (float)(item.ShuiliangA ?? 0);
                        ep.danweiA = item.DanweiA;
                        ep.jiushui = (float)(item.Jiushui ?? 0);
                        ep.shuliangB = (float)(item.ShuiliangB ?? 0);
                        ep.danweiB = item.DanweiB;
                        eps.Add(ep);

                    }
                    string JsonRe = jser.Serialize(eps);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "InitEventTable", "InitEventTable('" + JsonRe + "');", true);


                }
            }
            
        }
        public void setButtons()
        {
            using (ebsDBData db = new ebsDBData())
            {
                btSave.Visible = false;
                btSubmit.Visible = false;
                if (ID != 0)
                {
                    Bussiness BO = db.Bussiness.First(q => q.ID == ID);
                    if (currentUser.userName == BO.Sales && BO.Zhuangtai == "编辑" )
                    {
                        btSave.Visible = true;
                        btSubmit.Visible = true;
                    }
                }
                else
                {
                    btSave.Visible = true;
                    btSubmit.Visible = true;
                }
            }
        }
        public void setInfoList()
        {
            using (ebsDBData db = new ebsDBData())
            {
                string venueInfo = "{{'id':'{0}','text':'{1}'}}";
                string YongcanInfo = "{{'id':'{0}','text':'{1}'}}"; ;
                string venueAll = "";
                string YongcanAll = "";
                foreach (var item in db.tbSysVenue)
                {
                    venueAll+= string.Format(venueInfo,item.VenueName,item.VenueName)+",";
                }
                foreach (var item in db.tbSysYongcan)
                {
                    YongcanAll += string.Format(YongcanInfo, item.YongcanType, item.YongcanType) + ",";
                }
                venueAll ="["+ venueAll.Substring(0,venueAll.Length -1)+"]";
                YongcanAll = "[" + YongcanAll.Substring(0, YongcanAll.Length - 1) + "]";
                venueList.InnerText = venueAll;
                yanhuiList.InnerText = YongcanAll;
            }

        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateBussinessCase("Save");
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
                UpdateBussinessCase("Submit");
            }
            catch (System.Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ThrowError", "ThrowError('" + ex.Message + "');", true);
            }
           
        }

        public void UpdateBussinessCase(string state)
        {
            var jser = new JavaScriptSerializer();
            string jsons = hdTableEventJsons.Value;
            List<eventPayBussiness> EventsFormat = jser.Deserialize<List<eventPayBussiness>>(jsons);
            int CreatedID = 0;
            #region  Create or Update

            if (ID == 0)
            {
                using (ebsDBData db = new ebsDBData())
                {
                    Bussiness NewOrder = new Bussiness();
                    NewOrder.MainID = Convert.ToInt32(ddKehu.SelectedValue);
                    NewOrder.Zhuangtai = "编辑";
                    NewOrder.HetongID = tbHetongBianhao.Text;
                    NewOrder.HetongDate = Convert.ToDateTime(tbHetongRiqi.Text);
                    NewOrder.Company = Request.Form["ctl00$MainContent$tbCompany"]; 
                    NewOrder.Qudaobeizhu = Request.Form["ctl00$MainContent$tbQudao"];
                    NewOrder.Lianxiren1 = tbLianxiren1.Text;
                    NewOrder.Lianxiren1_Shouji = tbLianxiren1_Shouji.Text;
                    NewOrder.Lianxiren1_Zuoji = tbLianxiren1_Zuoji.Text;
                    NewOrder.Lianxiren2 = tbLianxiren2.Text;
                    NewOrder.Lianxiren2_Shouji = tbLianxiren2_Shouji.Text;
                    NewOrder.Lianxiren2_Zuoji = tbLianxiren2_Zuoji.Text;
                    NewOrder.EventVenue = ddEventVenue.SelectedValue;
                    NewOrder.Sales = ddSales.SelectedValue;
                    NewOrder.EventName = tbEventName.Text;
                    NewOrder.EventDate = Convert.ToDateTime(tbEventRiqi.Text);
                    NewOrder.EventType = ddEventType.SelectedValue;
                    NewOrder.Dabaodanjia = Convert.ToDouble(tbDabaoFee.Text);
                    NewOrder.Dabaodanjia_ren = Convert.ToInt32(tbDabaoRen.Text);
                    if (OtherFeeName1.Text.Trim() !="")
                    {
                        NewOrder.Otherfee1 = OtherFeeName1.Text;
                        NewOrder.Otherfee1_Amount = Convert.ToDouble(OtherFeeValue1.Text);
                    }
                    if (OtherFeeName2.Text.Trim() != "")
                    {
                        NewOrder.Otherfee2 = OtherFeeName2.Text;
                        NewOrder.Otherfee2_Amount = Convert.ToDouble(OtherFeeValue2.Text);
                    }
                    if (OtherFeeName3.Text.Trim() != "")
                    {
                        NewOrder.Otherfee3 = OtherFeeName3.Text;
                        NewOrder.Otherfee3_Amount = Convert.ToDouble(OtherFeeValue3.Text);
                    }
                  
                    NewOrder.Zhekou = Convert.ToDouble(tbZhekou.Text);
                    NewOrder.Fanyong = Convert.ToDouble(tbFanyongAmount.Text);
                    NewOrder.IsChengdan = false;
                    foreach (var item in EventsFormat)
                    {
                        BussinessEventFormat BE = new BussinessEventFormat();
                        BE.Venue = item.Venue;
                        if (item.EventType == "会议")
                        {
                            BE.EventFormat = "会议";
                            BE.Dajianfei = item.Dajianfei;
                            BE.Changdifei = item.Changdifei;
                        }
                        if (item.EventType == "用餐")
                        {
                            BE.EventFormat = "用餐";
                            BE.Yongcan = item.Yongcan;
                            BE.Canbiao = item.canbiao;
                            BE.ShuiliangA = item.shuliangA;
                            BE.DanweiA = item.danweiA;
                            BE.Jiushui = item.jiushui;
                            BE.ShuiliangB = item.shuliangB;
                            BE.DanweiB = item.danweiB;
                        }
                        NewOrder.BussinessEventFormat.Add(BE);
                    }
                    db.Bussiness.InsertOnSubmit(NewOrder);
                    db.SubmitChanges();
                    CreatedID = NewOrder.ID;

                }
            }
            else
            {
                using (ebsDBData db = new ebsDBData())
                {


                    if (db.Bussiness.Count(q => q.ID == ID) < 1)
                    {
                        return;
                    }
                    Bussiness NewOrder = db.Bussiness.First(q => q.ID == ID);
                    //NewOrder.MainID = Convert.ToInt32(ddKehu.SelectedValue);
                    if (state == "Save")
                    {
                        NewOrder.Zhuangtai = "编辑";
                    }
                    else NewOrder.Zhuangtai = "待审批";
                    NewOrder.HetongID = tbHetongBianhao.Text;
                    NewOrder.HetongDate = Convert.ToDateTime(tbHetongRiqi.Text);
                    NewOrder.Company = Request.Form["ctl00$MainContent$tbCompany"];
                    NewOrder.Qudaobeizhu = Request.Form["ctl00$MainContent$tbQudao"];
                    NewOrder.Lianxiren1 = tbLianxiren1.Text;
                    NewOrder.Lianxiren1_Shouji = tbLianxiren1_Shouji.Text;
                    NewOrder.Lianxiren1_Zuoji = tbLianxiren1_Zuoji.Text;
                    NewOrder.Lianxiren2 = tbLianxiren2.Text;
                    NewOrder.Lianxiren2_Shouji = tbLianxiren2_Shouji.Text;
                    NewOrder.Lianxiren2_Zuoji = tbLianxiren2_Zuoji.Text;
                    NewOrder.EventVenue = ddEventVenue.SelectedValue;
                    NewOrder.Sales = ddSales.SelectedValue;
                    NewOrder.EventName = tbEventName.Text;
                    NewOrder.EventDate = Convert.ToDateTime(tbEventRiqi.Text);
                    NewOrder.EventType = ddEventType.SelectedValue;
                    NewOrder.Dabaodanjia = Convert.ToDouble(tbDabaoFee.Text);
                    NewOrder.Dabaodanjia_ren = Convert.ToInt32(tbDabaoRen.Text);
                    if (OtherFeeName1.Text.Trim() != "")
                    {
                        NewOrder.Otherfee1 = OtherFeeName1.Text;
                        NewOrder.Otherfee1_Amount = Convert.ToDouble(OtherFeeValue1.Text);
                    }
                    if (OtherFeeName2.Text.Trim() != "")
                    {
                        NewOrder.Otherfee2 = OtherFeeName2.Text;
                        NewOrder.Otherfee2_Amount = Convert.ToDouble(OtherFeeValue2.Text);
                    }
                    if (OtherFeeName3.Text.Trim() != "")
                    {
                        NewOrder.Otherfee3 = OtherFeeName3.Text;
                        NewOrder.Otherfee3_Amount = Convert.ToDouble(OtherFeeValue3.Text);
                    }
                    NewOrder.Zhekou = Convert.ToDouble(tbZhekou.Text);
                    NewOrder.Fanyong = Convert.ToDouble(tbFanyongAmount.Text);
                    NewOrder.IsChengdan = false;
                    db.BussinessEventFormat.DeleteAllOnSubmit(NewOrder.BussinessEventFormat);
                    foreach (var item in EventsFormat)
                    {
                        BussinessEventFormat BE = new BussinessEventFormat();
                        BE.Venue = item.Venue;
                        if (item.EventType == "会议")
                        {
                            BE.EventFormat = "会议";
                            BE.Dajianfei = item.Dajianfei;
                            BE.Changdifei = item.Changdifei;
                        }
                        if (item.EventType == "用餐")
                        {
                            BE.EventFormat = "用餐";
                            BE.Yongcan = item.Yongcan;
                            BE.Canbiao = item.canbiao;
                            BE.ShuiliangA = item.shuliangA;
                            BE.DanweiA = item.danweiA;
                            BE.Jiushui = item.jiushui;
                            BE.ShuiliangB = item.shuliangB;
                            BE.DanweiB = item.danweiB;
                        }
                        NewOrder.BussinessEventFormat.Add(BE);
                    }
                    db.SubmitChanges();
                    CreatedID = NewOrder.ID;

                }

            }

            #endregion
            if (state == "Submit")
            {
                #region make AuditRecord

                using (ebsDBData db = new ebsDBData())
                {
                    Bussiness BO = db.Bussiness.First(q => q.ID == CreatedID);
                    var AuditNeed = db.tbSysAuditConfig_Bussiness.OrderBy(q => q.Priority).ToList();
                    foreach (var item in AuditNeed)
                    {
                        BussinessAuditRecords ar = new BussinessAuditRecords();
                        ar.AuditLevel = item.Role;
                        ar.AuditPriority = item.Priority;
                        ar.OrderID = BO.ID;
                        ar.AuditResult = "待审批";
                        ar.AuditUser = "";
                        ar.AuditSuggest = "";
                        ar.AuditType = "初次提交";
                        BO.BussinessAuditRecords.Add(ar);
                    }

                    var aRecord = new BussinessOrderLogs();
                    aRecord.Date = DateTime.Now;
                    aRecord.UserName = currentUser.userName;
                    aRecord.UserRole = currentUser.roles;
                    aRecord.ActionName = "提交审批";
                    aRecord.ActionType = "初次提交";
                    aRecord.Comments = "";
                    BO.BussinessOrderLogs.Add(aRecord);
                    db.SubmitChanges();

                }
                #endregion
            }
            

            Response.Redirect("ShangwuList.aspx");
        }

        public class eventPayBussiness
        {
            public string EventType { get; set; }
            public string Venue { get; set; }
            public float Dajianfei { get; set; }
            public float Changdifei { get; set; }

            public string Yongcan { get; set; }
            public float canbiao { get; set; }
            public float shuliangA { get; set; }
            public string danweiA { get; set; }
            public float jiushui { get; set; }
            public float shuliangB { get; set; }
            public string danweiB { get; set; }
        }



    }
}