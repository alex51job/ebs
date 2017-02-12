using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.dbml;

namespace ebs.MoKuai_admin
{
    public partial class ChangdiEdit : System.Web.UI.Page
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
                        tbSysVenue table = db.tbSysVenue.First(q => q.ID == ID);
                        txtName.Text = table.VenueName;
                        ddRegion.SelectedValue = table.Region;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.ToString().Trim();
           
            try
            {

              
               

                using (dbml.ebsDBData context = new dbml.ebsDBData())
                {
                    if (ID == 0)
                    {
                        if (context.tbSysVenue.Any(p => p.VenueName == name))
                        {
                            Response.Write("<script>window.alert('该场地已经建立.');location.href='ChangdiList.aspx';</script>");
                            return;
                        }
                        tbSysVenue table = new tbSysVenue();
                        table.VenueName = name;
                        table.Region = ddRegion.SelectedValue;
                        table.CreationDate = DateTime.Now;

                        context.tbSysVenue.InsertOnSubmit(table);
                        context.SubmitChanges();
                        Response.Redirect("ChangdiList.aspx");
                    }
                    else
                    {
                        tbSysVenue table = context.tbSysVenue.First(q => q.ID == ID);

                        if (context.tbSysVenue.Any(p => p.VenueName == name && p.ID != ID))
                        {
                            Response.Write("<script>window.alert('场地名已经存在.');location.href='ChangdiList.aspx';</script>");
                            return;
                        }

                        //var res = context.tbShangwu.Where(q => q.Changdi == table.Changdi);
                        //foreach (var item in res)
                        //{
                        //    item.Changdi = name;
                        //}
                        //var res2 = context.tbMainWedding.Where(q => q.yishiChangdi == table.Changdi);
                        //foreach (var item in res2)
                        //{
                        //    item.yishiChangdi = name;
                        //}

                        //var res3 = context.tbMainWedding.Where(q => q.yanhuiting.Contains(table.Changdi.Trim()));
                        //foreach (var item in res3)
                        //{
                        //   item.yanhuiting =  item.yanhuiting.Replace(table.Changdi.Trim(), name);
                        //}

                        table.VenueName = name;
                        table.Region = ddRegion.SelectedValue;

                        context.SubmitChanges();
                        Response.Redirect("ChangdiList.aspx");

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