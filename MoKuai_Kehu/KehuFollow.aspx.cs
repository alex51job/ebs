using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.dbml;
using ebs.commons;
using System.Text;

namespace ebs.MoKuai_Kehu
{
    public partial class KehuFollow : System.Web.UI.Page
    {
        public int ID
        {
            get { return ViewState["ID"] == null? 0:Convert.ToInt32(ViewState["ID"]); }
            set { ViewState["ID"] = value; }
        }
        public int SW = 0;
        public int HL = 0;
        public ComCls.LoginUser LoginUserInfo
        {
            get { return ViewState["LoginUserInfo"] as ComCls.LoginUser; }
            set { ViewState["LoginUserInfo"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ID = Convert.ToInt32(Request.QueryString["ID"]);
                SW = Convert.ToInt32(Request.QueryString["SW"]);
                HL = Convert.ToInt32(Request.QueryString["HL"]);

                LoginUserInfo = (ComCls.LoginUser)(Session["LoginUserInfo"]);
             
                BindActions();
                BindActionsSales();
            }
        }

        public void BindActions()
        {
            using (ebsDBData database = new ebsDBData())
            {
                var res = database.tbKehuFollow.Where(p => p.KehuID == ID && p.CreatedRole == "客服");
                if (res.Count() > 0)
                {
                    gwActions.DataSource = res;
                    gwActions.DataBind();
                    return;
                }
                List<tbKehuFollow> lst = new List<tbKehuFollow>();
                lst.Add(new tbKehuFollow { });
                gwActions.DataSource = lst;
                gwActions.DataBind();
                gwActions.Rows[0].Visible = false;
            }
        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            GridViewRow row = gwActions.FooterRow;
            tbKehuFollow data = GetFootValueFromGrid(row);
            //追加
            using (ebsDBData database = new ebsDBData())
            {
                database.tbKehuFollow.InsertOnSubmit(data);
                database.SubmitChanges();
            }
            BindActions();
        }

        private tbKehuFollow GetFootValueFromGrid(GridViewRow row)
        {
            tbKehuFollow data = new tbKehuFollow();
            data.KehuID = ID;
            data.ActionContent = ((TextBox)row.FindControl("txtActions")).Text.Trim();
            data.CreatedBy = LoginUserInfo.userName;
            data.CreatedDt = DateTime.Now;
            data.CreatedRole = "客服";
            return data;
        }

        private tbKehuFollow GetKeyFromGrid(int rowIndex)
        {
            tbKehuFollow data = new tbKehuFollow();
            DataKey keys = gwActions.DataKeys[rowIndex];
            if (keys == null)
            {
                return data;
            }
            data.ID = (int)keys["ID"];
            return data;
        }
        protected void gwActions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            tbKehuFollow data = GetKeyFromGrid(e.RowIndex);

            using (ebsDBData context = new ebsDBData())
            {
                var res = context.tbKehuFollow.First(p => p.ID == data.ID);

                context.tbKehuFollow.DeleteOnSubmit(res);

                context.SubmitChanges();
            }

            BindActions();
        }


        public void BindActionsSales()
        {
            using (ebsDBData database = new ebsDBData())
            {
                var res = database.tbKehuFollow.Where(p => p.KehuID == ID && p.CreatedRole == "销售");
                if (res.Count() > 0)
                {
                    gwActionsSales.DataSource = res;
                    gwActionsSales.DataBind();
                    return;
                }
                List<tbKehuFollow> lst = new List<tbKehuFollow>();
                lst.Add(new tbKehuFollow { });
                gwActionsSales.DataSource = lst;
                gwActionsSales.DataBind();
                gwActionsSales.Rows[0].Visible = false;
            }
        }

        protected void lnkAddNewSales_Click(object sender, EventArgs e)
        {
            GridViewRow row = gwActionsSales.FooterRow;
            tbKehuFollow data = GetFootValueFromGridSales(row);
            //追加
            using (ebsDBData database = new ebsDBData())
            {
                database.tbKehuFollow.InsertOnSubmit(data);
                database.SubmitChanges();
            }
            BindActionsSales();
        }

        private tbKehuFollow GetFootValueFromGridSales(GridViewRow row)
        {
            tbKehuFollow data = new tbKehuFollow();
            data.KehuID = ID;
            data.ActionContent = ((TextBox)row.FindControl("txtActionsSales")).Text.Trim();
            data.CreatedBy = LoginUserInfo.userName;
            data.CreatedDt = DateTime.Now;
            data.CreatedRole = "销售";//Session["LoginRole"].ToString();
            return data;
        }

        private tbKehuFollow GetKeyFromGridSales(int rowIndex)
        {
            tbKehuFollow data = new tbKehuFollow();
            DataKey keys = gwActionsSales.DataKeys[rowIndex];
            if (keys == null)
            {
                return data;
            }
            data.ID = (int)keys["ID"];
            return data;
        }
        protected void gwActionsSales_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            tbKehuFollow data = GetKeyFromGridSales(e.RowIndex);

            using (ebsDBData context = new ebsDBData())
            {
                var res = context.tbKehuFollow.First(p => p.ID == data.ID);

                context.tbKehuFollow.DeleteOnSubmit(res);

                context.SubmitChanges();
            }

            BindActionsSales();
        }

        public string MakeTabs(int _ID, int _SW, int _HL)
        {
            StringBuilder sb = new StringBuilder();
            string href = @"<li role=""presentation"" {0}><a href=""{1}"">{2}</a></li>";
            if (_SW != 0)
            {
                sb.AppendFormat(href, "", ResolveUrl("~/MoKuai_Shangwu/ShangwuEdit.aspx?ID=" + _SW), "商务订单信息");

            }
            if (_HL != 0)
            {
                sb.AppendFormat(href, "", ResolveUrl("~/MoKuai_Hunli/HunliEdit.aspx?ID=" + _HL), "婚宴订单信息");
            }
            sb.AppendFormat(href, "", ResolveUrl("~/MoKuai_Kehu/KehuiEdit.aspx?SW="+_SW+"&HL="+_HL+"&ID=" + _ID), "客户信息");
             sb.AppendFormat(href, "class='active'","#" , "跟进信息");

            return sb.ToString();
        }
    }
}