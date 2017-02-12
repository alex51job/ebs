using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.commons;
using ebs.dbml;

namespace ebs.MoKuai_Rili
{
    public partial class EventList : LoginChecker
    {
        public ComCls.LoginUser currentUser
        {
            get { return (ComCls.LoginUser)Session["LoginUserInfo"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        private void BindRepeat(DateTime dt)
        {
            using(ebsDBData db = new ebsDBData())
	{

        var lstRili = db.tbRili.Where(q => q.StartDate > dt && q.StartDate < dt.AddMonths(1));
        Repeater1.DataSource = lstRili;
        Repeater1.DataBind();
	}
            



        }

        private void InitPage()
        {
            DateTime dt = DateTime.Now;
            lbMonth.Text = dt.ToString("MMM yyyy");
            dt = new DateTime(dt.Year, dt.Month, 1);
            BindRepeat(dt);
            for (int i = 1; i < 25; i++)
            {

                ddStartHour.Items.Add(new ListItem(i.ToString().PadLeft(2,'0')));
                ddEndHour.Items.Add(new ListItem(i.ToString().PadLeft(2, '0')));
            }
           
        }

        protected void btSubmit_Click(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(txtDt.Text);
            lbMonth.Text = dt.ToString("MMM yyyy");
            dt = new DateTime(dt.Year, dt.Month, 1);
            BindRepeat(dt);

          
        }

        protected void btEvent_Click(object sender, EventArgs e)
        {
            using (ebsDBData db = new ebsDBData())
            {
                if (hdID.Value == "0")
                {

                    tbRili MR = new tbRili();
                    MR.Title = tbTitle.Text;
                    MR.Renshu = tbRenshu.Text;
                    MR.StartDate = Convert.ToDateTime(tbRiqi.Text).AddHours(Convert.ToInt32(ddStartHour.SelectedValue));
                    MR.EndDate = Convert.ToDateTime(tbRiqi.Text).AddHours(Convert.ToInt32(ddEndHour.SelectedValue));
                    MR.Source = tbShanghu.Text;
                    MR.Jine = tbJine.Text;
                    MR.SalesName = tbSales.Text;
                    MR.yanhuiting = tbYanhuiting.Text;
                    MR.yishiChangdi = tbYishichangdi.Text;
                    MR.IsConfirmed = ddIsconfirmed.SelectedValue;
                    MR.Can = ddHascan.SelectedValue;
                    MR.Miaoshu = tbBeizhu.Text;

                    db.tbRili.InsertOnSubmit(MR);
                    db.SubmitChanges();
                }
                else
                {
                    int ID = Convert.ToInt32(hdID.Value);
                    tbRili MR = db.tbRili.First(q => q.ID == ID);
                    MR.Title = tbTitle.Text;
                    MR.Renshu = tbRenshu.Text;
                    MR.StartDate = Convert.ToDateTime(tbRiqi.Text).AddHours(Convert.ToInt32(ddStartHour.SelectedValue));
                    MR.EndDate = Convert.ToDateTime(tbRiqi.Text).AddHours(Convert.ToInt32(ddEndHour.SelectedValue));
                    MR.Source = tbShanghu.Text;
                    MR.Jine = tbJine.Text;
                    MR.SalesName = tbSales.Text;
                    MR.yanhuiting = tbYanhuiting.Text;
                    MR.yishiChangdi = tbYishichangdi.Text;
                    MR.IsConfirmed = ddIsconfirmed.SelectedValue;
                    MR.Can = ddHascan.SelectedValue;
                    MR.Miaoshu = tbBeizhu.Text;
                    db.SubmitChanges();
                }
            }
            
            DateTime dt = DateTime.Now;
            if (txtDt.Text !="")
            {
                DateTime.TryParse(txtDt.Text, out dt);
            }
            dt = new DateTime(dt.Year, dt.Month, 1);
            lbMonth.Text = dt.ToString("MMM yyyy");
            BindRepeat(dt);
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                using (ebsDBData db = new ebsDBData())
                {
                    var res = db.tbRili.First(q => q.ID == ID);
                    db.tbRili.DeleteOnSubmit(res);
                    db.SubmitChanges();
                }
            }
            DateTime dt = DateTime.Now;
            if (txtDt.Text != "")
            {
                DateTime.TryParse(txtDt.Text, out dt);
            }
            dt = new DateTime(dt.Year, dt.Month, 1);
            lbMonth.Text = dt.ToString("MMM yyyy");
            BindRepeat(dt);
        }

      
        //protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{

        //    CheckBox ckShow = e.Item.FindControl("ckShow") as CheckBox;
        //    tbRili item = e.Item.DataItem as tbRili;
        //    if (item.Show == true)
        //    {
        //        ckShow.Checked = true;
        //    }
        //    HiddenField hvID = e.Item.FindControl("hvID") as HiddenField;
        //    hvID.Value = item.ID.ToString();
        //    if (item.ID == 0)
        //    {
        //        hvID.Value = item.Source + "|" + item.MainID;
        //    }



        //}
    }
}