using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.dbml;

namespace ebs.MoKuai_admin
{
    public partial class YonghuList : System.Web.UI.Page
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
                Repeater1.DataSource = db.tbUsers.ToList();
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
                    tbUsers user = db.tbUsers.First(q => q.ID == ID);
                    //将该人员负责的case移动至系统账户下
                    //var res = db.tbKehu.Where(q => q.Owner == ID);
                    //foreach (var item in res)
                    //{
                    //    item.Owner = 2;
                    //}

                    //var res2 = db.tbShangwu.Where(q => q.Owner == ID);
                    //foreach (var item in res2)
                    //{
                    //    item.Owner = 5;
                    //}
                    //var res3 = db.tbMainWedding.Where(q => q.Owner == ID);
                    //foreach (var item in res3)
                    //{
                    //    item.Owner = 6;
                    //}

                    db.tbUsers.DeleteOnSubmit(user);
                    db.SubmitChanges();
                    Response.Redirect(Request.Url.ToString()); 
                }
            }
           
        }

        
    }
}