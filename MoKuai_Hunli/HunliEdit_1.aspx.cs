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
    public partial class HunliEdit_1 :System.Web.UI.Page
    {
        public int ID
        {
            get { return ViewState["ID"] != null ? Convert.ToInt32(ViewState["ID"].ToString()) : 0; }
            set { ViewState["ID"] = value; }
        }
        public int KehuID
        {

            get { return ViewState["KehuID"] != null ? Convert.ToInt32(ViewState["KehuID"].ToString()) : 0; }
            set { ViewState["KehuID"] = value; }
        }
     
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["ID"] = Request.QueryString["ID"];
            if (!IsPostBack)
            {
                try
                {
                    if (((Hunli)Page.Master).LoginUserInfo.roles.Contains("客服"))
                    {
                        btSubmit.Visible = false;
                    }
                    initPage();
                }
                catch (Exception)
                {

                    ;
                }
              
            }
        }
       
        public void initPage()
        {


            using (ebsDBData db = new ebsDBData())
            {
                var res = (from q in db.tbKehu
                           select new
                           {
                               txt = q.Mingchen + " | " + q.Lianxiren,
                               vas = q.ID
                           }
                         ).Distinct();
                ddKehu.DataSource = res;
                ddKehu.DataTextField = "txt";
                ddKehu.DataValueField = "vas";
                ddKehu.DataBind();
                ddKehu.Items.Insert(0, new ListItem());

                var res2 = (from q in db.tbAdminChangdi
                            select new
                            {
                                txt = q.Changdi
                            }).Distinct();
                ddYanhuiting.DataSource = res2;
                ddYanhuiting.DataTextField = "txt";
                ddYanhuiting.DataBind();
                ddYanhuiting.Items.Insert(0, new ListItem());

                ddYishichangdi.DataSource = res2;
                ddYishichangdi.DataTextField = "txt";
                ddYishichangdi.DataBind();
                ddYishichangdi.Items.Insert(0, new ListItem());

                var res3 = (from q in db.tbAdminMenu
                                select new
                                {
                                    txt = q.MenuName,
                                    vas = q.ID

                                }).Distinct();
                ddMenu.DataSource = res3;
                ddMenu.DataTextField = "txt";
                ddMenu.DataBind();
                ddMenu.Items.Insert(0,new ListItem());


                if (ID != 0)
                {
                    tbMainWedding item = db.tbMainWedding.First(q => q.ID == ID);

                    KehuID = item.KehuID;
                    ddKehu.Items.FindByValue(item.KehuID.ToString()).Selected = true;
                    tbXinlang.Text = item.Xinlang.Split('|')[0];
                    tbXLshouji.Text = item.Xinlang.Split('|')[1];
                    tbXLdizhi.Text = item.Xinlang.Split('|')[2];

                    tbXinning.Text = item.Xinniang.Split('|')[0];
                    tbXNshouji.Text = item.Xinniang.Split('|')[1];
                    tbXNdizhi.Text = item.Xinniang.Split('|')[2];

                    tbStartDate.Text = item.StartDate.ToString("yyyy-MM-dd");
                    tbStartDate2.Text = item.StartDate.ToString("hh:mm tt");

                    ddYishichangdi.Items.FindByText(item.yishiChangdi).Selected = true;
                   tbYishiKaishi.Text = item.yishiStartDt;
                   tbYishijieshu.Text = item.yishiEndDt;
                    string[] its= item.yanhuiting.Split(',');

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Select2Sel", "Select2Sel('" + item.yanhuiting + "')", true);
                   tbYanhuiKaishi.Text = item.yanhuiStartDt;
                    tbYanhuiJieshu.Text =item.yanhuiEndDt;
                    ddMenu.Items.FindByValue(item.taocan).Selected = true;
                    tbCaijin.Text =item.caijing;
                    tbJiushui.Text =item.jiushui;
                    tbHunqing.Text =item.hunqing;
                     tbFuwufei.Text = item.fuwufei;

                     tbPay1Percent.Text =item.pay1Percent;
                    tbPay1Amount.Text =item.pay1Amount  ;
                    tbPay1Daxie.Text =item.pay1Daxie ;


                    tbPay2Percent.Text = item.pay2Percent;
                    tbPay2Amount.Text = item.pay2Amount;
                    tbPay2Daxie.Text = item.pay2Daxie;


                    tbPay3Percent.Text = item.pay3Percent;
                    tbPay3Amount.Text = item.pay3Amount;
                    tbPay3Daxie.Text = item.pay3Daxie;

                    tbOthers.Text =item.otherContent;

                     tbZongAmount.Text =item.zongAmount;
                    tbZongdaxie.Text =item.zongDaxie ;

                  

                    tbHetongbianhao.Text = item.Huodongbianhao;
                    tbHetongriqi.Text = item.Hetongriqi.ToString("yyyy-MM-dd hh:mm tt");
      

                }
            }
        }

        protected void btSubmit_Click(object sender, EventArgs e)
        {
            int LoginID = ((Hunli)Page.Master).LoginUserInfo.ID;
            if (ID == 0)
            {
                
                using (ebsDBData db = new ebsDBData())
                {
                    tbMainWedding item = new tbMainWedding();
                    item.KehuID = int.Parse(ddKehu.SelectedValue);
                    item.Xinlang = tbXinlang.Text + "|" + tbXLshouji.Text + "|" + tbXLdizhi.Text;
                    item.Xinniang = tbXinning.Text + "|" + tbXNshouji.Text + "|" + tbXNshouji.Text;
                    item.StartDate = Convert.ToDateTime(tbStartDate.Text + " " + tbStartDate2.Text);
                    item.yishiChangdi = ddYishichangdi.SelectedValue;
                    item.yishiStartDt = tbYishiKaishi.Text;
                    item.yishiEndDt = tbYishijieshu.Text;
                    item.yanhuiting = Request.Form["ctl00$MainContent$ddYanhuiting"].ToString();
                    item.yanhuiStartDt = tbYanhuiKaishi.Text;
                    item.yanhuiEndDt = tbYanhuiJieshu.Text;
                    item.taocan = ddMenu.SelectedValue;
                    item.caijing = tbCaijin.Text;
                    item.jiushui = tbJiushui.Text;
                    item.hunqing = tbHunqing.Text;
                    item.fuwufei = tbFuwufei.Text;

                    item.pay1Percent = tbPay1Percent.Text;
                    item.pay1Amount = tbPay1Amount.Text;
                    item.pay1Daxie = Request.Form["ctl00$MainContent$tbPay1Daxie"].ToString();

                    item.pay2Percent = tbPay2Percent.Text;
                    item.pay2Amount = tbPay2Amount.Text;
                    item.pay2Daxie = Request.Form["ctl00$MainContent$tbPay2Daxie"].ToString();

                    item.pay3Percent = tbPay3Percent.Text;
                    item.pay3Amount = tbPay3Amount.Text;
                    item.pay3Daxie = Request.Form["ctl00$MainContent$tbPay3Daxie"].ToString(); 

                    item.otherContent = tbOthers.Text;

                    item.zongAmount = tbZongAmount.Text;
                    item.zongDaxie = Request.Form["ctl00$MainContent$tbZongdaxie"].ToString(); 

                    item.CreatationDt = DateTime.Now;
                    item.CreatedBy = LoginID;

                    item.LastModifiedBy = LoginID;
                    item.LastModifiedDt = DateTime.Now;

                    item.Owner = LoginID;
                    if (((Hunli)Page.Master).LoginUserInfo.roles == "管理员")
                    {
                        item.Owner = 4;
                    }


                    item.Huodongbianhao = tbHetongbianhao.Text;
                    item.Hetongriqi = Convert.ToDateTime(tbHetongriqi.Text);

                    db.tbMainWedding.InsertOnSubmit(item);
                    db.SubmitChanges();
                    Response.Redirect("HunliList.aspx");
                }
            }
            else
            {
                using (ebsDBData db = new ebsDBData())
                {
                    tbMainWedding item = db.tbMainWedding.First(q => q.ID == ID);
                    item.KehuID = int.Parse(ddKehu.SelectedValue);
                    item.Xinlang = tbXinlang.Text + "|" + tbXLshouji.Text + "|" + tbXLdizhi.Text;
                    item.Xinniang = tbXinning.Text + "|" + tbXNshouji.Text + "|" + tbXNshouji.Text;
                    item.StartDate = Convert.ToDateTime(tbStartDate.Text + " " + tbStartDate2.Text);
                    item.yishiChangdi = ddYishichangdi.SelectedValue;
                    item.yishiStartDt = tbYishiKaishi.Text;
                    item.yishiEndDt = tbYishijieshu.Text;
                    item.yanhuiting = Request.Form["ctl00$MainContent$ddYanhuiting"].ToString();
                    item.yanhuiStartDt = tbYanhuiKaishi.Text;
                    item.yanhuiEndDt = tbYanhuiJieshu.Text;
                    item.taocan = ddMenu.SelectedValue;
                    item.caijing = tbCaijin.Text;
                    item.jiushui = tbJiushui.Text;
                    item.hunqing = tbHunqing.Text;
                    item.fuwufei = tbFuwufei.Text;

                    item.pay1Percent = tbPay1Percent.Text;
                    item.pay1Amount = tbPay1Amount.Text;
                    item.pay1Daxie = Request.Form["ctl00$MainContent$tbPay1Daxie"].ToString(); 

                    item.pay2Percent = tbPay2Percent.Text;
                    item.pay2Amount = tbPay2Amount.Text;
                    item.pay2Daxie = Request.Form["ctl00$MainContent$tbPay2Daxie"].ToString(); 

                    item.pay3Percent = tbPay3Percent.Text;
                    item.pay3Amount = tbPay3Amount.Text;
                    item.pay3Daxie = Request.Form["ctl00$MainContent$tbPay3Daxie"].ToString(); 

                    item.otherContent = tbOthers.Text;

                    item.zongAmount = tbZongAmount.Text;
                    item.zongDaxie =  Request.Form["ctl00$MainContent$tbZongdaxie"].ToString(); 


                    item.LastModifiedBy = LoginID;
                    item.LastModifiedDt = DateTime.Now;

                    if (((Hunli)Page.Master).LoginUserInfo.roles != "管理员")
                    {
                        item.Owner = LoginID;
                    }
                   

                    item.Huodongbianhao = tbHetongbianhao.Text;
                    item.Hetongriqi = Convert.ToDateTime(tbHetongriqi.Text);
                    db.SubmitChanges();
                    Response.Redirect(Request.Url.ToString());

                }

            }
       
        }


    }
}