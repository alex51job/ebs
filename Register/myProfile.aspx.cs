using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.Tools;

namespace MyProject.Register
{
    public partial class myProfile : ComCls
    {
        protected int UserID { get { return int.Parse(Security.DeCode(Request.QueryString["ID"].ToString())); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (DataBaseDataContext db = new DataBaseDataContext())
                {
                    if (db.tb_UserInfo.Count(q=>q.ID == UserID)> 0)
                    {
                        tb_UserInfo item = db.tb_UserInfo.First(q=>q.ID == UserID);
                        tbUserName.Text = item.UserName;
                        tbEmail.Text = item.MailAddress;
                        tbPhone.Text = item.Phone;
                        lbRole.Text = item.Role;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            using (DataBaseDataContext db = new DataBaseDataContext())
            {
                if (db.tb_UserInfo.Count(q => q.MailAddress == tbEmail.Text && q.ID != UserID) > 0)
                {
                    Response.Write("<script>window.alert('该邮件地址已经被注册');</script>");
                    return;
                }
                 tb_UserInfo item = db.tb_UserInfo.First(q=>q.ID == UserID);
               
                 item.MailAddress = tbEmail.Text;
                 item.Phone = tbPhone.Text;
                 item.UserName = tbUserName.Text;
                 db.SubmitChanges();
            }
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
                using (DataBaseDataContext context = new DataBaseDataContext())
                {
                    var user = context.tb_UserInfo.Single(p => p.ID == UserID);
                    user.Password = Security.EnCode(pw1.Text);
                    context.SubmitChanges();
                    //ShowDialog("successful.", true);

                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
    }
}