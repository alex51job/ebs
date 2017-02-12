using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.Tools;
using ebs.dbml;
using ebs.commons;

namespace ebs
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        private void InitPage()
        {
            if (Request.Cookies["userName"] != null)
            {
                string userName = Request.Cookies["userName"].Value;
                string psw = Request.Cookies["psw"].Value;
                username.Text = userName;
                //txtpsd.Attributes["value"] = psw;
                txtpsd.Attributes.Add("value", psw);
                ipRem.Checked = true;
            }
        }
        protected void lbLogin_Click(object sender, EventArgs e)
        {
            string name = username.Text.ToString().Trim();
            string password = Security.EnCode(txtpsd.Text.ToString().Trim());

            using (ebsDBData context = new ebsDBData())
            {
                if (context.tbUsers.Any(p => p.UserName == name && p.password == password))
                {
                    var user = context.tbUsers.First(p => p.UserName == name && p.password == password);

                    if (ipRem.Checked)
                    {
                        Response.Cookies["userName"].Value = user.UserName;
                        Response.Cookies["userName"].Expires = DateTime.MaxValue;
                        Response.Cookies["psw"].Value = txtpsd.Text;
                        Response.Cookies["psw"].Expires = DateTime.MaxValue;
                    }
                    else
                    {
                        Response.Cookies["userName"].Value = null;
                        Response.Cookies["psw"].Value = null;
                    }
                    Session["LoginUserInfo"] = new ComCls.LoginUser { ID = user.ID, roles=user.Role, userName = user.UserName,DisplayName=user.DisplayName, mailAddress = user.UserMail, region = user.Region };
                    
                    Response.Redirect("Default.aspx");
                }
                Response.Write("<script>window.alert('wrong username & password.');</script>");
            }
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