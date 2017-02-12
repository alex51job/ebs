using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.commons;
using ebs.dbml;
using System.Linq.Dynamic;

namespace ebs.MoKuai_Shangwu
{
    public partial class ShangwuKehuList : System.Web.UI.Page
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
                BindRepeat(1);
                displaybutton();
            }
        }
        public void InitPage()
        {
            ddlQudao.ddlForQudao("All");
            ddlMendian.ddlForMenDian();
            ddlKefu.ddlForKefus("", "All");
            ddlSales.ddlForSales("", "All");
        }

        private void BindRepeat(int page)
        {
            int LoginID = ((Shangwu)Page.Master).LoginUserInfo.ID;
            ComCls.LoginUser loginUser = ((Shangwu)Page.Master).LoginUserInfo;
            try
            {
                if (LoginID != 0)
                {
                    using (ebsDBData db = new ebsDBData())
                    {
                        //List<> sources = db.Customers.OrderByDescending(q=>q.ID).ToList();
                        List<Customers> sources = DependOnRole(db.Customers.OrderByDescending(q => q.ID).ToList());
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

        private void displaybutton()
        {
            PlaceHolder1.Visible = false;
            switch (currentUser.roles)
            {
                case "客服":
                    PlaceHolder1.Visible = true;
                    break;
                case "客服主管":
                    PlaceHolder1.Visible = true;
                    break;
                case "总经理":
                    break;
                case "婚宴销售":

                    break;
                case "婚宴销售主管":

                    break;
                case "商务销售":

                    break;
                case "商务销售主管":

                    break;
                case "监督者":

                    break;
                default:
                    break;
            }
        }

        private List<Customers> DependOnRole(List<Customers> sources)
        {
            switch (currentUser.roles)
            {
                case "客服":
                case "客服主管":
                case "总经理":
                    break;
                case "婚宴销售":
                    sources = sources.Where(q => q.Sales == currentUser.userName && q.Zhuangtai != "未下发" && q.CustomerType == "婚宴").ToList();
                    break;
                case "婚宴销售主管":
                    sources = sources.Where(q => q.CustomerType == "婚宴" && q.Zhuangtai != "未下发").ToList();
                    break;
                case "商务销售":
                    sources = sources.Where(q => q.Sales == currentUser.userName && q.Zhuangtai != "未下发" && q.CustomerType == "商务").ToList();
                    break;
                case "商务销售主管":
                    sources = sources.Where(q => q.CustomerType == "商务" && q.Zhuangtai != "未下发").ToList();
                    break;
                default:
                    break;
            }
            return sources;
        }

        private List<Customers> MakeContditon(List<Customers> sources)
        {
            List<Customers> listCustomers = new List<Customers>();
            Boolean returnFlag = false;
            var resultList = from q in sources
                             where q.Source.Contains(ddlQudao.SelectedValue)
                             where q.SourceNb.ToUpper().Contains(tbQudaoBianhao.Text.Trim().ToUpper())
                             where q.ZixunDiDian.Contains(ddlMendian.SelectedValue)
                             where q.CustomerName.ToUpper().Contains(tbCustomerName.Text.Trim().ToUpper())
                             where q.Telephone.ToUpper().Contains(tbMobile.Text.Trim().ToUpper())
                             where q.Sales.Contains(ddlSales.SelectedValue)
                             where q.Kefu.Contains(ddlKefu.SelectedValue)
                             where q.NeedHuiFang.Contains(ddlHuifang.SelectedValue)
                             select q;
            DateTime StartDate, EndDate;
            if (DateTime.TryParse(tbRiqiStart.Text, out StartDate))
            {
                resultList = resultList.Where(q => q.CreationDate >= StartDate);
            }
            if (DateTime.TryParse(tbRiqiEnd.Text, out EndDate))
            {
                resultList = resultList.Where(q => q.CreationDate <= EndDate);
            }

            if (ddlZhuangtai.SelectedValue != "")
            {
                switch (ddlZhuangtai.SelectedValue)
                {
                    case "无效/审批中":
                        resultList = resultList.Where(q => q.AuditStatus != "" || q.AuditType == "无效");
                        break;
                    case "未下发":
                        resultList = resultList.Where(q => q.AuditType != "无效" && q.Zhuangtai == "未下发");
                        break;
                    case "已下发":
                        resultList = resultList.Where(q => q.AuditType != "无效" && q.Zhuangtai == "已下发");
                        break;
                    case "到店":
                        resultList = resultList.Where(q => q.CustomerFollowBySales.Count > 0 && q.CustomerFollowBySales.Last().IsDaoDian == "是");
                        break;
                    case "未到店":
                        resultList = resultList.Where(q => q.CustomerFollowBySales.Count > 0 && q.CustomerFollowBySales.Last().IsDaoDian == "否");
                        break;
                    case "未成单":
                        foreach (var item in resultList)
                        {
                            if (item.CustomerFollowBySales.Count > 0 && item.CustomerFollowBySales.Last().isChengDan == "否")
                            {
                                listCustomers.Add(item);
                            }
                            if (item.CustomerFollowBySales_Bussiness.Count > 0 && item.CustomerFollowBySales_Bussiness.Last().isChengDan == "否")
                            {
                                listCustomers.Add(item);
                            }
                        }
                        returnFlag = true;
                        break;
                    case "已成单":
                        foreach (var item in resultList)
                        {
                            if (item.CustomerFollowBySales.Count > 0 && item.CustomerFollowBySales.Last().isChengDan == "是")
                            {
                                listCustomers.Add(item);
                            }
                            if (item.CustomerFollowBySales_Bussiness.Count > 0 && item.CustomerFollowBySales_Bussiness.Last().isChengDan == "是")
                            {
                                listCustomers.Add(item);
                            }
                        }
                        returnFlag = true;
                        break;
                    default:
                        break;
                }
            }

            string OrderByStr = hdOrderBy.Value + " " + (hdAsc.Value == "1" ? "asc" : "desc");
            if (returnFlag)
            {
              
                listCustomers = listCustomers.OrderBy(OrderByStr).ToList();
                return listCustomers;
            }

            //排序
            resultList = resultList.OrderBy(OrderByStr);
            return resultList.ToList();
        }



        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            LinkButton edit = e.Item.FindControl("lbEdit") as LinkButton;

            string ID = edit.CommandArgument;
            LinkButton lbView = e.Item.FindControl("lbView") as LinkButton;
            //lbView.PostBackUrl = ();
            lbView.OnClientClick = "window.open('KehuEdit.aspx?ID=" + ID + "','_blank')";

            edit.OnClientClick = "window.open('KehuEdit.aspx?ID=" + ID + "','_blank')";
            //edit.PostBackUrl = ("KehuEdit.aspx?ID=" + ID);
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
                if (cus.AuditStatus == "未审批" && cus.AuditType == "无效")
                {
                    System.Web.UI.HtmlControls.HtmlTableRow thisTR = e.Item.FindControl("thisTR") as System.Web.UI.HtmlControls.HtmlTableRow;
                    thisTR.Style.Add("color", "gray");
                    lbStatus.Text = "无效审批中";
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