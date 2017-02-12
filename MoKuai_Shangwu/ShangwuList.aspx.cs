using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.dbml;
using ebs.commons;
using ebs.Tools;
using System.Linq.Dynamic;

namespace ebs.MoKuai_Shangwu
{
    public partial class ShangwuList : System.Web.UI.Page
    {

        public ComCls.LoginUser currentUser
        {
            get { return ((Shangwu)(Page.Master)).LoginUserInfo; }
        }
        public bool reset = false;
        public int absolutePage
        {
            get { return ViewState["absolutePage"] == null ? 0 : int.Parse(ViewState["absolutePage"].ToString()); }
            set { ViewState["absolutePage"] = value; }
        }
        public int offsetPage
        {
            get { return ViewState["offsetPage"] == null ? 1 : int.Parse(ViewState["offsetPage"].ToString()); }
            set { ViewState["offsetPage"] = value; }
        }
        public int RecourdCount
        {
            get { return ViewState["RecourdCount"] == null ? 0 : int.Parse(ViewState["RecourdCount"].ToString()); }
            set { ViewState["RecourdCount"] = value; }
        }
        public int PageSize = 20;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
              
                InitPage();
                displaybutton();
                BindRepeat(1);
            }
        }
        private void InitPage()
        {
            ddlWeddingDidian.ddlForMenDian();//活动地点
            ddlEventType.ddlForEventType();//活动类型
            ddlEventVenue.ddlForYanhuiting();//活动场地
            ddlBusinessStatus.ddlForHunliOrderStatus();
            ddlSales.ddlForSales("", "All", "商务");
            ddlPayStatus.ddlForPayStatus();
           
        }
        private void displaybutton()
        {
            PlaceHolder1.Visible = false;
            switch (currentUser.roles)
            {
                case "客服":

                    break;
                case "客服主管":

                    break;
                case "总经理":
                    break;
                case "婚宴销售":
                  
                    break;
                case "婚宴销售主管":
                    
                    break;
                case "商务销售":
                    PlaceHolder1.Visible = true;
                    break;
                case "商务销售主管":
                    PlaceHolder1.Visible = true;
                    break;
                case "监督者":

                    break;
                default:
                    break;
            }
        }
        private void BindRepeat(int page)
        {
            try
            {
                if (currentUser != null)
                {

                    using (ebsDBData db = new ebsDBData())
                    {

                        //var a = db.Bussiness.ToList();
                        List<Bussiness> sources = DependOnRole(db.Bussiness.OrderByDescending(q => q.ID).Where(q => q.Zhuangtai != "已覆盖").ToList());
                        if (reset == false)
                        {
                            sources = MakeContditon(sources);
                        }
                        if (sources != null)
                        {
                            RecourdCount = sources.Count;
                            SetPages();
                            Repeater1.DataSource = sources.Skip(PageSize * (page - 1)).Take(PageSize);
                            Repeater1.DataBind();

                        }
                        else
                        {
                            absolutePage = 0;
                            offsetPage = 1;
                            RecourdCount = 0;
                            SetPages();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }



        }


        private List<Bussiness> DependOnRole(List<Bussiness> sources)
        {

            switch (currentUser.roles)
            {
                case "商务销售":
                    sources = sources.Where(q => q.Sales == currentUser.userName).ToList();
                    break;
                case "商务销售主管":
                    sources = sources.Where(q=>q.Sales == currentUser.userName || q.Zhuangtai != "编辑").ToList();
                    break;
                default:
                    sources = sources.Where(q => q.Zhuangtai != "编辑").ToList();
                    break;
            }
            string mengdian = currentUser.region;
            if (mengdian != "All" && mengdian != "")
            {
                sources = sources.Where(q => q.EventVenue == mengdian).ToList();
            }
            return sources;
        }

        private List<Bussiness> MakeContditon(List<Bussiness> list)
        {
            var res = from q in list
                      where q.HetongID.ToUpper().Contains(tbHetongbianhao.Text.ToUpper())
                      where q.EventVenue.Contains(ddlWeddingDidian.SelectedValue)
                      where q.Lianxiren1.ToUpper().Contains(tbLianxirenA.Text.ToUpper())
                      where q.Lianxiren2.ToUpper().Contains(tbLianxirenB.Text.ToUpper())
                      where q.Company.ToUpper().Contains(tbCompany.Text.ToUpper())
                      where q.EventName.ToUpper().Contains(tbEventName.Text.ToUpper())
                      where q.EventType.Contains(ddlEventType.SelectedValue)
                      where q.BussinessEventFormat.Any(m=>m.Venue.Contains(ddlEventVenue.SelectedValue))
                      where q.Sales.Contains(ddlSales.SelectedValue)
                      where (new ConvertStringsInDB.JineByID_Bussiness(q.ID)).ZhuangtaiAudit.Contains(ddlBusinessStatus.SelectedValue)
                      select q;

            DateTime StartDate, EndDate;
            if (DateTime.TryParse(tbHetongStart.Text, out StartDate))
            {
                res = res.Where(q => q.HetongDate >= StartDate);
            }
            if (DateTime.TryParse(tbHetongEnd.Text, out EndDate))
            {
                res = res.Where(q => q.HetongDate <= EndDate);
            }
            DateTime EventStart, EventEnd;
            if (DateTime.TryParse(tbEventStart.Text, out EventStart))
            {
                res = res.Where(q=>q.EventDate >= EventStart);
            }
            if (DateTime.TryParse(tbEventEnd.Text,out EventEnd))
            {
                res = res.Where(q=>q.EventDate<=EventEnd);
            }

            //我大排序。。。
            string asc = hdAsc.Value == "1" ? "asc" : "desc";
            if (asc == "asc")
            {
                switch (hdOrderBy.Value)
                {
                    case "Huodongxingshi":
                        res = res.OrderBy(q => (new ConvertStringsInDB.JineByID_Bussiness(q.ID)).EventFormat);
                        break;
                    case "Changdi":
                        res = res.OrderBy(q => (new ConvertStringsInDB.JineByID_Bussiness(q.ID)).EventVenue);
                        break;
                    case "Zongjine":
                        res = res.OrderBy(q => (new ConvertStringsInDB.JineByID_Bussiness(q.ID)).Zongjine);
                        break;
                    case "Fanyong":
                        res = res.OrderBy(q => (new ConvertStringsInDB.JineByID_Bussiness(q.ID)).FanyongPer);
                        break;
                    case "Yifujine":
                        res = res.OrderBy(q => (new ConvertStringsInDB.JineByID_Bussiness(q.ID)).Yifujine);
                        break;
                    case "Zhuangtai":
                        res = res.OrderBy(q => (new ConvertStringsInDB.JineByID_Bussiness(q.ID)).ZhuangtaiAudit);
                        break;
                    default:
                        res = res.OrderBy(hdOrderBy.Value + " " + asc);
                        break;
                }
            }
            else
            {
                switch (hdOrderBy.Value)
                {
                    case "Huodongxingshi":
                        res = res.OrderByDescending(q => (new ConvertStringsInDB.JineByID_Bussiness(q.ID)).EventFormat);
                        break;
                    case "Changdi":
                        res = res.OrderByDescending(q => (new ConvertStringsInDB.JineByID_Bussiness(q.ID)).EventVenue);
                        break;
                    case "Zongjine":
                        res = res.OrderByDescending(q => (new ConvertStringsInDB.JineByID_Bussiness(q.ID)).Zongjine);
                        break;
                    case "Fanyong":
                        res = res.OrderByDescending(q => (new ConvertStringsInDB.JineByID_Bussiness(q.ID)).FanyongPer);
                        break;
                    case "Yifujine":
                        res = res.OrderByDescending(q => (new ConvertStringsInDB.JineByID_Bussiness(q.ID)).Yifujine);
                        break;
                    case "Zhuangtai":
                        res = res.OrderByDescending(q => (new ConvertStringsInDB.JineByID_Bussiness(q.ID)).ZhuangtaiAudit);
                        break;
                    default:
                        res = res.OrderBy(hdOrderBy.Value + " " + asc);
                        break;
                }
            }
            return res.ToList();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            LinkButton edit = e.Item.FindControl("lbEdit") as LinkButton;
            LinkButton view = e.Item.FindControl("lbView") as LinkButton;
            LinkButton audit = e.Item.FindControl("lbAudit") as LinkButton;

            edit.Visible = false;
            view.Visible = false;
            audit.Visible = false;

            string ID = edit.CommandArgument;
            var res = new ConvertStringsInDB.JineByID_Bussiness(Convert.ToInt32(ID));

          

            Literal lbEventFormat = e.Item.FindControl("lbEventFormat") as Literal;
            lbEventFormat.Text = string.Join(",",res.EventFormat);

            Literal lbVenue = e.Item.FindControl("lbVenue") as Literal;
            lbVenue.Text = string.Join(",",res.EventVenue);

            Literal lbZongjine = e.Item.FindControl("lbZongjine") as Literal;
            lbZongjine.Text = res.hetongZongjine.ToString();

            Literal lbFanyongPer = e.Item.FindControl("lbFanyongPer") as Literal;
            lbFanyongPer.Text = "("+res.FanyongPer*100+"%)";

            Literal lbYifujine = e.Item.FindControl("lbYifujine") as Literal;
            lbYifujine.Text = res.Yifujine.ToString();

            Literal lbZhuangtai = e.Item.FindControl("lbZhuangtai") as Literal;
            lbZhuangtai.Text = res.ZhuangtaiAudit;
            if (currentUser.roles == "商务销售")
            {
                if (res.ZhuangtaiAudit == "编辑") 
                {
                    edit.Visible = true;
                    edit.OnClientClick = "window.open('ShangwuEdit.aspx?ID=" + ID + "','_blank')";
                }
                else
                {
                    view.Visible = true;
                    view.OnClientClick = "window.open('ShangwuEdit.aspx?ID=" + ID + "','_blank')";
                }
            }
            else if (currentUser.roles.Contains("商务销售主管"))
            {

                if (res.ZhuangtaiAudit == "编辑")
                {
                    using (ebsDBData db = new ebsDBData())
                    {
                        var WO = db.Bussiness.FirstOrDefault(q => q.ID == Convert.ToInt32(ID));
                        if (WO != null && WO.Sales == currentUser.userName)
                        {
                            edit.Visible = true;
                            edit.OnClientClick = "window.open('ShangwuEdit.aspx?ID=" + ID + "','_blank')";
                        }
                    }
                }
                else
                {
                    if (currentUser.roles.Contains(res.RoleToAudit))
                    {
                        audit.Visible = true;
                        //audit.PostBackUrl = "HunliAudit.aspx?ID=" + ID;
                        audit.OnClientClick = "window.open('ShangwuAudit.aspx?ID=" + ID + "','_blank')";
                    }
                    else
                    {
                        view.Visible = true;
                        //view.PostBackUrl = "HunliEdit.aspx?ID=" + ID;
                        view.OnClientClick = "window.open('ShangwuEdit.aspx?ID=" + ID + "','_blank')";
                    }

                }
            }
            else if (currentUser.roles == "文员")
            {
              
                edit.Visible = true;
                //edit.PostBackUrl = "HunliEditAddedPay.aspx?ID=" + ID;
                edit.OnClientClick = "window.open('ShangwuPayments.aspx?ID=" + ID + "','_blank')";
                edit.Text = "<i class='fa fa-money'></i>";
                if (currentUser.roles.Contains(res.RoleToAudit))
                {
                    audit.Visible = true;
                    //audit.PostBackUrl = "HunliAudit.aspx?ID=" + ID;
                    audit.OnClientClick = "window.open('ShangwuAudit.aspx?ID=" + ID + "','_blank')";
                }
            }
            else if (currentUser.roles == "财务")
            {
                edit.Visible = true;
                //edit.PostBackUrl = "HunliEditAddedPay.aspx?ID=" + ID;
                edit.OnClientClick = "window.open('SWAuditFinance.aspx?ID=" + ID + "','_blank')";
                edit.Text = "<i class='fa fa-money'></i>";
                if (currentUser.roles.Contains(res.RoleToAudit))
                {
                    audit.Visible = true;
                    //audit.PostBackUrl = "HunliAudit.aspx?ID=" + ID;
                    audit.OnClientClick = "window.open('ShangwuAudit.aspx?ID=" + ID + "','_blank')";
                }
            }
            else
            {
                if (currentUser.roles.Contains(res.RoleToAudit))
                {
                    audit.Visible = true;
                    //audit.PostBackUrl = "HunliAudit.aspx?ID=" + ID;
                    audit.OnClientClick = "window.open('ShangwuAudit.aspx?ID=" + ID + "','_blank')";
                }
                else
                {
                    view.Visible = true;
                    //view.PostBackUrl = "HunliAudit.aspx?ID=" + ID;
                    view.OnClientClick = "window.open('ShangwuEdit.aspx?ID=" + ID + "','_blank')";
                }

            }

        }

        protected void UpdatePanel1_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideLoad", "hideLoad()", true);
            //ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "Sort", "Sort();", true);
        }


        private void SetPages()
        {

            int more1page = RecourdCount % PageSize;
            int pageCount = Convert.ToInt32(RecourdCount / PageSize) + (more1page != 0 ? 1 : 0);


            ((System.Web.UI.HtmlControls.HtmlGenericControl)liPla).Attributes["Class"] = "";
            ((System.Web.UI.HtmlControls.HtmlGenericControl)liPra).Attributes["Class"] = "";
            ((System.Web.UI.HtmlControls.HtmlGenericControl)li5).Attributes["Class"] = "";
            ((System.Web.UI.HtmlControls.HtmlGenericControl)li4).Attributes["Class"] = "";
            ((System.Web.UI.HtmlControls.HtmlGenericControl)li3).Attributes["Class"] = "";
            ((System.Web.UI.HtmlControls.HtmlGenericControl)li2).Attributes["Class"] = "";

            if (absolutePage == 0)
            {
                ((System.Web.UI.HtmlControls.HtmlGenericControl)liPla).Attributes["Class"] = "disabled";
            }
            if (5 * (absolutePage + 1) >= pageCount)
            {
                ((System.Web.UI.HtmlControls.HtmlGenericControl)liPra).Attributes["Class"] = "disabled";
                int i = 5 * (absolutePage + 1) - pageCount;

                while (i > 0)
                {
                    switch (i)
                    {
                        case 1:
                            ((System.Web.UI.HtmlControls.HtmlGenericControl)li5).Attributes["Class"] = "disabled";
                            break;
                        case 2:
                            ((System.Web.UI.HtmlControls.HtmlGenericControl)li4).Attributes["Class"] = "disabled";
                            break;
                        case 3:
                            ((System.Web.UI.HtmlControls.HtmlGenericControl)li3).Attributes["Class"] = "disabled";
                            break;
                        case 4:
                            ((System.Web.UI.HtmlControls.HtmlGenericControl)li2).Attributes["Class"] = "disabled";
                            break;
                        default:
                            break;
                    }
                    i--;
                }
            }

        }

        protected void pla_Click(object sender, EventArgs e)
        {
            absolutePage = absolutePage - 1;
            SetPages();
            BindRepeat(absolutePage * 5 + 1);
            p1.Text = (absolutePage * 5 + 1).ToString();
            p2.Text = (absolutePage * 5 + 2).ToString();
            p3.Text = (absolutePage * 5 + 3).ToString();
            p4.Text = (absolutePage * 5 + 4).ToString();
            p5.Text = (absolutePage * 5 + 5).ToString();
            offsetPage = 1;
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "setActive", "setActive('1')", true);
        }

        protected void p1_Click(object sender, EventArgs e)
        {
            offsetPage = 1;
            BindRepeat(absolutePage * 5 + offsetPage);
            //((System.Web.UI.HtmlControls.HtmlGenericControl)li1).Attributes["Class"] = "active";
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "setActive", "setActive('1')", true);

        }

        protected void p2_Click(object sender, EventArgs e)
        {
            offsetPage = 2;
            BindRepeat(absolutePage * 5 + offsetPage);
            //((System.Web.UI.HtmlControls.HtmlGenericControl)li2).Attributes["Class"] = "active";
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "setActive", "setActive('2')", true);
        }

        protected void p3_Click(object sender, EventArgs e)
        {
            offsetPage = 3;
            BindRepeat(absolutePage * 5 + offsetPage);
            //((System.Web.UI.HtmlControls.HtmlGenericControl)li3).Attributes["Class"] = "active";
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "setActive", "setActive('3')", true);
        }

        protected void p4_Click(object sender, EventArgs e)
        {
            offsetPage = 4;
            BindRepeat(absolutePage * 5 + offsetPage);
            //((System.Web.UI.HtmlControls.HtmlGenericControl)li4).Attributes["Class"] = "active";
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "setActive", "setActive('4')", true);
        }

        protected void p5_Click(object sender, EventArgs e)
        {
            offsetPage = 5;
            BindRepeat(absolutePage * 5 + offsetPage);
            // ((System.Web.UI.HtmlControls.HtmlGenericControl)li5).Attributes["Class"] = "active";
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "setActive", "setActive('5')", true);
        }

        protected void pra_Click(object sender, EventArgs e)
        {
            absolutePage = absolutePage + 1;
            SetPages();
            BindRepeat(absolutePage * 5 + 1);
            p1.Text = (absolutePage * 5 + 1).ToString();
            p2.Text = (absolutePage * 5 + 2).ToString();
            p3.Text = (absolutePage * 5 + 3).ToString();
            p4.Text = (absolutePage * 5 + 4).ToString();
            p5.Text = (absolutePage * 5 + 5).ToString();
            offsetPage = 1;
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "setActive", "setActive('1')", true);
        }

        protected string ToDisplayName(string str)
        {
            return str.ToDisplayName();
        }

        protected void lbConfirm_Click(object sender, EventArgs e)
        {

            BindRepeat(1);
        }

        protected void lbReset_Click(object sender, EventArgs e)
        {
            reset = true;
            BindRepeat(1);
            reset = false;
        }
    }
}