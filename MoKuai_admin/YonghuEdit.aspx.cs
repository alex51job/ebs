using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.dbml;
using ebs.Tools;

namespace ebs.MoKuai_admin
{
    public partial class YonghuEdit : System.Web.UI.Page
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
                       tbUsers user = db.tbUsers.First(q => q.ID == ID);
                       txtUserName.Text = user.UserName;
                       txtMail.Text = user.UserMail;
                       tbDisplayName.Text = user.DisplayName;
                       txtPhone.Text = user.Mobile;
                       txtPsd.Text = Security.DeCode(user.password);
                       dpRole.SelectedValue = user.Role.ToString().Trim();
                       ddRegion.SelectedValue = user.Region;
                   }
               }
           }
       
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
                string name = txtUserName.Text.ToString().Trim();
                string phone = txtPhone.Text.ToString().Trim();
                string mail = txtMail.Text.ToString().Trim();
                bool alreadyExist = false;
            try
            {
               
                
                using (dbml.ebsDBData context = new dbml.ebsDBData())
                {
                    if (ID == 0)
                    {
                        if (context.tbUsers.Any(p => p.UserName == name))
                        {
                            alreadyExist = true;
                        }
                        else
                        {
                            tbUsers user = new tbUsers();
                            user.UserName = name;
                            user.DisplayName = tbDisplayName.Text;
                            user.password = Security.EnCode(txtPsd.Text.ToString().Trim());
                            user.DisplayName = tbDisplayName.Text.Trim();
                            user.Mobile = phone;
                            user.Role = dpRole.SelectedItem.Text;
                            user.UserMail = mail;
                            user.Region = ddRegion.SelectedValue;
                            user.CreationDate = DateTime.Now;
                            context.tbUsers.InsertOnSubmit(user);
                            context.SubmitChanges();
                        }
                    }
                    else
                    {
                        tbUsers user = context.tbUsers.First(q => q.ID == ID);
                        if (context.tbUsers.Any(p => p.UserName == name && p.ID != ID))
                        {
                            alreadyExist = true;
                        }
                        else
                        {
                            user.UserName = name;
                            user.DisplayName = tbDisplayName.Text;
                            user.password = Security.EnCode(txtPsd.Text.ToString().Trim());
                            user.Mobile = phone;
                            user.DisplayName = tbDisplayName.Text.Trim();
                            user.Role = dpRole.SelectedItem.Text;
                            user.UserMail = mail;
                            user.Region = ddRegion.SelectedValue;
                            context.SubmitChanges();
                        }


                    }
                  
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //ShowDialog("errors", true);
            }
            finally
            { 
                if (alreadyExist)
                {
                    Response.Redirect("YonghuList.aspx?already=1");
                }
                else
                {
                    Response.Redirect("YonghuList.aspx");
                }
               

            }
        }
    }
}