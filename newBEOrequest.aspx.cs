using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.dbml;

namespace ebs
{
    public partial class newBEOrequest : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            InsertBEO();
        }

        private void InsertBEO()
        {
            try
            {
                using (ebsDBData db = new ebsDBData())
                {
                    tbMainBEO Main = new tbMainBEO
                    {
                        BEONo = tbBEONb.Text,
                        ruluriqi = Convert.ToDateTime(txtInputDate.Text),
                        CustomerID = Convert.ToInt32(dpCustomer.SelectedValue),
                        ContactID = Convert.ToInt32(dpContact.SelectedValue),
                        EventName = txtName.Text,
                        EventShijian = txtDueDate.Text,
                        EventXingshi = dphuodongxingshi.SelectedValue,
                        EventChangdi = dpVenue.SelectedValue,
                        EventRenshu = txtCount.Text,
                        EventYusuan = txtFee.Text,
                        EventQita = txtOtherInfo.Text,
                        BEOStatus = Request.Form["inlineRadioOptions"].ToString(),
                        CreatedBy = "Tester",
                        CreationDt = DateTime.Now,
                        LastModifiedBy = "Tester",
                        LastModifiedDt = DateTime.Now
                    };
                    db.tbMainBEO.InsertOnSubmit(Main);
                    db.SubmitChanges();
                     
                }
                
            }
            catch (System.Exception ex)
            {
            	
            }
        }

       
    }
}