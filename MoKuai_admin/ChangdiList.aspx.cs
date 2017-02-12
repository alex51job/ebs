using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.dbml;

namespace ebs.MoKuai_admin
{
    public partial class ChangdiList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {

                Bindrepeat();
            }
        }

        public void Bindrepeat()
        {
            using (dbml.ebsDBData db = new dbml.ebsDBData())
            {
                Repeater1.DataSource = db.tbSysVenue.ToList();
                Repeater1.DataBind();

            }

        }

        protected void btDel_Click(object sender, EventArgs e)
        {
            int ID = 0;
            if (hdDelID.Value != "0")
            {
                ID = int.Parse(hdDelID.Value);
                using (ebsDBData db = new ebsDBData())
                {
                    tbSysVenue table = db.tbSysVenue.First(q => q.ID == ID);
                    db.tbSysVenue.DeleteOnSubmit(table);
                    db.SubmitChanges();
                    Response.Redirect(Request.Url.ToString());
                }
            }

        }
    }
}