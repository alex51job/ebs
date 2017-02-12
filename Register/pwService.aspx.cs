using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.Tools;
using ebs.dbml;

namespace MyProject.Register
{
    public partial class pwService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            using (ebsDBData context = new ebsDBData())
            {
                var res = context.tbUsers.Where(p => p.UserMail == txtMail.Text.Trim());
                if (res.Count() == 0)
                {
                    Response.Write("<script>window.alert('there no exists the mail address.');</script>");
                    return;
                }
                MailSender.Instance.SendPsdServiceMail(res.First().UserMail, res.First().ID.ToString().Trim());

                Response.Write("<script>window.alert('mail send successfully.');</script>");
            }
        }
    }
}