using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.Tools;
using ebs.commons
namespace MyProject.Register
{
    public partial class applyNew : ComCls
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtUserName.Text.ToString().Trim();
                string password = Security.EnCode(this.txtPsd.Text.ToString().Trim());
                string phone = txtPhone.Text.ToString().Trim();
                string mail = txtMail.Text.ToString().Trim();

                using (DataBaseDataContext context = new DataBaseDataContext())
                {
                    if (context.tb_UserInfo.Any(p => p.MailAddress == mail))
                    {
                        Response.Write("<script>window.alert('the mail address has been registed before.');</script>");
                        return;
                    }
                    tb_UserInfo user = new tb_UserInfo();
                    user.UserName = name;
                    user.Password = password;
                    user.Phone = phone;
                   // user.Role = int.Parse(dpRole.SelectedValue.Trim());
                    user.Role = dpRole.SelectedItem.Text.ToString().Trim();
                    user.MailAddress = mail;
                    user.CreatedTime = DateTime.Now;

                    context.tb_UserInfo.InsertOnSubmit(user);
                    context.SubmitChanges();
                
                    //ShowDialog("successful.", true);
                    Response.Write("<script>window.returnValue='successful';window.close();</script>");
                }
            }
            catch (Exception ex) { //ShowDialog("errors", true); 
                throw ex;
            }
        }

       
    }
}