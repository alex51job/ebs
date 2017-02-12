using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.dbml;
using ebs.commons;

namespace ebs
{
    public partial class inbox : System.Web.UI.Page
    {
        public int absolutePage
        {
            get { return ViewState["absolutePage"] == null ? 0 : int.Parse(ViewState["absolutePage"].ToString()); }
            set { ViewState["absolutePage"] = value; }
        }
        public int offsetPage
        {
            get { return ViewState["offsetPage"] == null ? 0 : int.Parse(ViewState["offsetPage"].ToString()); }
            set { ViewState["offsetPage"] = value; }
        }
        public int RecourdCount
        {
            get { return ViewState["RecourdCount"] == null ? 0 : int.Parse(ViewState["RecourdCount"].ToString()); }
            set { ViewState["RecourdCount"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["loginID"] = "pzx2k2";//Sup
                BindRepeat(1);
            }
        }
        protected void UpdatePanel1_Load(object sender, EventArgs e)
        {

            p1.Text = (absolutePage * 5 + 1).ToString();
            p2.Text = (absolutePage * 5 + 2).ToString();
            p3.Text = (absolutePage * 5 + 3).ToString();
            p4.Text = (absolutePage * 5 + 4).ToString();
            p5.Text = (absolutePage * 5 + 5).ToString();
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hideLoad", "hideLoad()", true);


        }

        private void BindRepeat(int page)
        {
            try
            {
                if (Session["loginID"] != null)
                {
                    string owner = Session["loginID"].ToString();
                    using (ebsDBData db = new ebsDBData())
                    {
                        var res = db.tbMainBEO.OrderByDescending(q => q.ruluriqi).ToList();
                        List<inboxItem> lstInboxItem = new List<inboxItem>();
                        foreach (var item in res)
                        {
                            inboxItem inboxI = new inboxItem { ID = item.ID, type="BEO", BEONumber = item.BEONo, Customer = item.tbListContact.tbListCustomer.CustomerName, Contact = item.tbListContact.ContactName, Owner = item.CreatedBy, Status = item.BEOStatus, Changdi = item.EventChangdi, EventName = item.EventName, EventDt = Convert.ToDateTime(item.EventShijian) };
                            lstInboxItem.Add(inboxI);

                        }

                        if (lstInboxItem.Count != 0)
                        {
                            absolutePage = 0;
                            offsetPage = 1;
                            RecourdCount = lstInboxItem.Count;
                            SetPages();
                            Repeater1.DataSource = lstInboxItem.Skip(10 * (page - 1)).Take(10);
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

    
        private void SetPages()
        {
            int PageSize = 10;
            int more1page = RecourdCount % PageSize;
            int pageCount = Convert.ToInt32(RecourdCount / PageSize) + (more1page != 0 ? 1 : 0);



            if (absolutePage == 0)
            {
                ((System.Web.UI.HtmlControls.HtmlGenericControl)liPla).Attributes["Class"] = "disabled";
            }
            if (5 * (absolutePage + 1) >= pageCount)
            {
                ((System.Web.UI.HtmlControls.HtmlGenericControl)liPra).Attributes["Class"] = "disabled";
                int i = 5 * (absolutePage + 1) - pageCount;
                ((System.Web.UI.HtmlControls.HtmlGenericControl)li5).Attributes["Class"] = "";
                ((System.Web.UI.HtmlControls.HtmlGenericControl)li4).Attributes["Class"] = "";
                ((System.Web.UI.HtmlControls.HtmlGenericControl)li3).Attributes["Class"] = "";
                ((System.Web.UI.HtmlControls.HtmlGenericControl)li2).Attributes["Class"] = "";
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
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            LinkButton edit = e.Item.FindControl("lbEdit") as LinkButton;
            string status = ((ebs.inbox.inboxItem)(e.Item.DataItem)).Status;
            string URLPage = "newBEOrequest.aspx";
            string act = "view";
            string ID = edit.CommandArgument.ToString();
            string byWho = "test";
            edit.PostBackUrl = ComCls.enCode(URLPage + "?act=" + act + "&ID=" + ID + "&byWho=" + byWho);

        }
        public class inboxItem
        {
            public int ID { get; set; }
            public string type { get; set; }
            public string BEONumber { get; set; }
            public string Customer { get; set; }
            public string Contact { get; set; }
            public string EventName { get; set; }
            public DateTime EventDt { get; set; }
            public string Status { get; set; }
            public string Changdi { get; set; }
            public string Owner { get; set; }
        }
    }
}