using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.dbml;
using ebs.commons;
using System.Linq.Dynamic;

namespace ebs.MoKuai_Kehu
{
    public partial class KehuListAudit : System.Web.UI.Page
    {
        public ComCls.LoginUser currentUser
        {
            get { return ((Kehu)(Page.Master)).LoginUserInfo; }
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
                BindRepeat(1);
            } 
        }

        private void BindRepeat(int page)
        {
            int LoginID = ((Kehu)Page.Master).LoginUserInfo.ID;
            ComCls.LoginUser loginUser = ((Kehu)Page.Master).LoginUserInfo;
            try
            {
                if (LoginID != 0)
                {
                    using (ebsDBData db = new ebsDBData())
                    {
                        List<Customers> sources = db.Customers.Where(q => q.AuditStatus == "未审批" || q.AuditStatus == "通过" || q.AuditStatus == "退回").OrderByDescending(q => q.ID).ToList();
                        if (reset == false && sources != null)
                        {
                            //sources = MakeContditon(sources);
                            string OrderByStr = hdOrderBy.Value + " " + (hdAsc.Value == "1" ? "asc" : "desc");
                            sources = sources.OrderBy(OrderByStr).ToList();
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

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            LinkButton edit = e.Item.FindControl("lbEdit") as LinkButton;
            string ID = edit.CommandArgument;
            LinkButton lbView = e.Item.FindControl("lbView") as LinkButton;
            lbView.PostBackUrl = ("KehuEdit.aspx?ID=" + ID);
            edit.PostBackUrl = ("KehuEdit.aspx?ID=" + ID);
            Label lbStatus = e.Item.FindControl("lbZhuangtai") as Label;

            using (ebsDBData db = new ebsDBData())
            {
                var cus = db.Customers.First(q => q.ID == int.Parse(ID));

                //判断客户的状态
                if (cus.Zhuangtai == "未下发")
                {
                    lbStatus.Text = "未下发";
                }
                if (cus.CustomerType == "商务" && cus.Zhuangtai == "已下发")
                {
                    if (cus.CustomerFollowBySales_Bussiness.Count > 0)
                    {
                        lbStatus.Text = cus.CustomerFollowBySales_Bussiness.Last().isChengDan == "是" ? "已成单" : "未成单";
                    }
                    else lbStatus.Text = "已下发";
                }
                if (cus.CustomerType == "婚宴" && cus.Zhuangtai == "已下发")
                {
                    if (cus.CustomerFollowBySales.Count > 0)
                    {
                        var cfLast = cus.CustomerFollowBySales.Last();
                        string res = "";
                        string isChengdan = cfLast.isChengDan;
                        string isDaodian = cfLast.IsDaoDian;
                        if (isDaodian == "是")
                        {
                            res += "到店";
                            res += string.Format("({0})", cus.DaoDianCount);
                            if (isChengdan == "是") res += "已成单";
                            else res += "未成单";
                        }
                        else res += "未到店";
                        lbStatus.Text = res;
                    }
                    else lbStatus.Text = "已下发";
                }

                //判断客户是否有效
                if (cus.AuditStatus == "通过" && cus.AuditType == "无效")
                {
                    System.Web.UI.HtmlControls.HtmlTableRow thisTR = e.Item.FindControl("thisTR") as System.Web.UI.HtmlControls.HtmlTableRow;
                    thisTR.Style.Add("color", "gray");
                    lbStatus.Text = "无效";
                }

                //判断客户的操作与编辑权
                edit.Visible = false;
                if (currentUser.roles == "客服主管")
                {
                    edit.Visible = true;
                    lbView.Visible = false;
                }
                if (currentUser.roles == "客服" && currentUser.userName == cus.Kefu)
                {
                    edit.Visible = true;
                    lbView.Visible = false;
                }

            }




        }

        protected void UpdatePanel1_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideLoad", "hideLoad()", true);
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

                while (i >= 0)
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
            BindRepeat(absolutePage * 5 + offsetPage);
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
            BindRepeat(absolutePage * 5 + offsetPage);
            p1.Text = (absolutePage * 5 + 1).ToString();
            p2.Text = (absolutePage * 5 + 2).ToString();
            p3.Text = (absolutePage * 5 + 3).ToString();
            p4.Text = (absolutePage * 5 + 4).ToString();
            p5.Text = (absolutePage * 5 + 5).ToString();
            offsetPage = 1;
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "setActive", "setActive('1')", true);
        }

        protected void lbConfirm_Click(object sender, EventArgs e)
        {

            BindRepeat(1);
        }

        protected string handleStatus(string str)
        {
            if (str == "已下发")
            {

            }

            return str;
        }
        protected string ToDisplayName(string str)
        {
            return str.ToDisplayName();
        }

    }
}