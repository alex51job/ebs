using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.dbml;

namespace ebs.MoKuai_admin
{
    public partial class CaidanEdit : System.Web.UI.Page
    {
        public int ID
        {
            get { return Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]); }
            set { ViewState["ID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ID != 0)
                {
                    using (ebsDBData db = new ebsDBData())
                    {
                        tbSysMenu table = db.tbSysMenu.First(q => q.ID == ID);
                        txtName.Text = table.MenuName;
                        txtPrice.Text = table.MenuPrice.ToString();
                        txtBeizhu.Text = table.MenuDesc;
                    }
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                string name = txtName.Text.ToString().Trim();
                string price = txtPrice.Text.ToString().Trim();
                string Beizhu = txtBeizhu.Text.ToString().Trim();


                using (dbml.ebsDBData context = new dbml.ebsDBData())
                {
                    if (ID == 0)
                    {
                        if (context.tbSysMenu.Any(p => p.MenuName == name))
                        {
                            Response.Write("<script>window.alert('该菜单已经建立.');location.href='CaidanList.aspx';</script>");
                            return;
                        }
                        tbSysMenu table = new tbSysMenu();
                        table.MenuName = name;
                        table.MenuPrice = Convert.ToDouble(price);
                        table.MenuDesc = Beizhu;
                        table.CreationDate = DateTime.Now;

                        context.tbSysMenu.InsertOnSubmit(table);
                        context.SubmitChanges();
                        Response.Redirect("CaidanList.aspx");
                    }
                    else
                    {
                        tbSysMenu table = context.tbSysMenu.First(q => q.ID == ID);

                        if (context.tbSysMenu.Any(p => p.MenuName == name && p.ID != ID))
                        {
                            Response.Write("<script>window.alert('该菜单已经建立.');location.href='CaidanList.aspx';</script>");
                            return;
                        }

                        //var res = context.tbShangwu.Where(q => q.tao == table.Changdi);
                        //foreach (var item in res)
                        //{
                        //    item.Changdi = name;
                        //}
                        //var res2 = context.tbMainWedding.Where(q => q.taocan == table.MenuName);
                        //foreach (var item in res2)
                        //{
                        //    item.taocan = name;
                        //}

                        table.MenuName = name;
                        table.MenuPrice = Convert.ToDouble(price);
                        table.MenuDesc = Beizhu;

                        context.SubmitChanges();
                        Response.Redirect("CaidanList.aspx");

                    }

                }
            }
            catch
            {
                //ShowDialog("errors", true);
            }
        }
    }
}