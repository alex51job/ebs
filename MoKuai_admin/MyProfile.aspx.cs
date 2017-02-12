using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.commons;
using ebs.Tools;
using ebs.dbml;

namespace ebs.MoKuai_admin
{
    public partial class MyProfile : System.Web.UI.Page
    {
        protected int UserID
        {
            get { return Convert.ToInt32(ViewState["UserID"]); }
            set { ViewState["UserID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddMendian.ddlForMenDian();
                ddMendian.Items.Add(new ListItem("All"));
                UserID = ((admin)Page.Master).LoginUserInfo.ID;
                using (ebsDBData db = new ebsDBData())
                {
                    if (db.tbUsers.Count(q => q.ID == UserID) > 0)
                    {
                        tbUsers item = db.tbUsers.First(q => q.ID == UserID);
                        tbUserName.Text = item.UserName;
                        tbUserName.setReadonly();
                        tbDisplayName.Text = item.DisplayName;
                        tbEmail.Text = item.UserMail;
                        tbPhone.Text = item.Mobile;
                        lbRole.Text = item.Role;
                        ddMendian.ddlForMenDian(item.Region);
                    }
                    else
                    {
                        return;
                    }
                }
                ddMendian.setReadonly();
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            using (ebsDBData db = new ebsDBData())
            {
                if (db.tbUsers.Count(q => q.UserMail == tbEmail.Text && q.ID != UserID) > 0)
                {
                    Response.Write("<script>window.alert('该邮件地址已经被注册'); location=location</script>");
                    return;
                }
                tbUsers item = db.tbUsers.First(q => q.ID == UserID);

                item.UserMail = tbEmail.Text;
                item.Mobile = tbPhone.Text;
                item.DisplayName = tbDisplayName.Text;
                item.UserName = tbUserName.Text;
                item.Region = ddMendian.SelectedValue;
                if (UserID !=1 && UserID !=3 && UserID !=4)
                {
                    db.SubmitChanges();
                    Response.Write("<script>alert(\"修改成功\");location=location</script>");
                }
                else
                {
                    Response.Write("<script>alert(\"系统账户不可更改\");location=location</script>");
                }
             
 
            }
            //Response.Redirect(Request.Url.ToString());
        }

        protected void btPassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (pw1.Text != pw2.Text)
                {
                    Response.Write("<script>window.alert('not the same password.');</script>");
                    return;
                }
                if (UserID == 1 || UserID == 3 || UserID == 4)
                {
                    Response.Write("<script>alert(\"系统账户不可更改\");location=location</script>");
                }

                using (ebsDBData context = new ebsDBData())
                {
                    var user = context.tbUsers.Single(p => p.ID == UserID);
                    user.password = Security.EnCode(pw1.Text);
                    context.SubmitChanges();
                    Response.Write("<script>alert(\"修改成功\");location=location</script>");
                 

                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
    }
}