using System;
using System.Linq;
using ebs.Tools;
using ebs.commons;
using ebs.dbml;

namespace ebs.Register
{
    public partial class pasReset : System.Web.UI.Page
    {
        private string UserID { get { return Security.DeCode(Request.QueryString["UserID"].ToString().Trim()); } }
        public string projectName = "";
        public string head = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            projectName = ComCls.title;
            if (!IsPostBack)
            {
                using (ebsDBData context = new ebsDBData())
                {
                    var user = context.tbUsers.Single(p => p.ID == int.Parse(UserID));
                    lblUserName.Text = user.UserName;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string psw1 = txtPsd.Text.Trim();
                string psw2 = txtConfirm.Text.Trim();

                if (psw1 != psw2)
                {
                    Response.Write("<script>window.alert('not the same password.');</script>");
                    return;
                }

                using (ebsDBData context = new ebsDBData())
                {
                    var user = context.tbUsers.Single(p => p.ID == int.Parse(UserID));
                    user.password = Security.EnCode(psw1);
                    context.SubmitChanges();
                    //ShowDialog("successful.", true);
                  
                }
            }
            catch { throw new Exception("error"); }
            Response.Redirect("~/login.aspx");
        }
    }
}