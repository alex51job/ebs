using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.dbml;
using ebs.commons;

namespace ebs.MoKuai_admin
{
    public partial class RiliManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
                //BindRepeat();
            }
        }

        private void BindRepeat(DateTime dt)
        {
            List<tbRili> lstRili = new List<tbRili>();
            

            using (ebsDBData db = new ebsDBData())
            {
                List<tbRili> AlreadyHas = db.tbRili.Where(q=>q.StartDate>= dt && q.StartDate <= dt.AddMonths(1).AddDays(-1)).ToList();
                lstRili.AddRange(AlreadyHas);
                List<tbShangwu> lstShangwu = db.tbShangwu.Where(q => q.Huodongshijian >= dt && q.Huodongshijian <= dt.AddMonths(1).AddDays(-1)).ToList();
                List<tbMainWedding> lstWedding = db.tbMainWedding.Where(q => q.StartDate >= dt && q.StartDate <= dt.AddMonths(1).AddDays(-1)).ToList();
                foreach (tbShangwu item in lstShangwu)
                {
                    if (lstRili.Exists(q=>q.Source== "SW" && q.MainID == item.ID))
                    {
                        continue;
                    }
                    else
                    {
                        tbRili tbNew = new tbRili();
                        tbNew.MainID = item.ID;
                        tbNew.Miaoshu = item.Huodongxingshi;
                        tbNew.Renshu = item.Renshu;
                        tbNew.SalesName = ComCls.HandleSales(item.Owner.ToString());
                        tbNew.Show = false;
                        tbNew.Site = item.Changdi;
                        tbNew.Source = "SW";
                        tbNew.StartDate = item.Huodongshijian;
                        tbNew.Title = item.Huodongmingcheng;
                        tbNew.Can = true;
                        tbNew.Status = true;
                        tbNew.EndDate = item.Huodongshijian.AddHours(6);
                        lstRili.Add(tbNew);
                    }
                }

                foreach (tbMainWedding item in lstWedding)
                {
                    if (lstRili.Exists(q => q.Source == "HL" && q.MainID == item.ID))
                    {
                        continue;
                    }
                    else
                    {
                        tbRili tbNew = new tbRili();
                        tbNew.MainID = item.ID;
                        tbNew.Miaoshu = item.zongAmount;
                        tbNew.Renshu = item.tbKehu.Zhuoshu;
                        tbNew.SalesName = ComCls.HandleSales(item.Owner.ToString());
                        tbNew.Show = false;
                        tbNew.Site = item.yishiChangdi+","+item.yanhuiting;
                        tbNew.Source = "HL";
                        tbNew.StartDate = item.StartDate;
                        tbNew.Title = ComCls.HandleXinren(item.Xinlang, item.Xinniang);
                        tbNew.Can = true;
                        tbNew.Status = true;
                        tbNew.EndDate = Convert.ToDateTime( item.StartDate.ToString("yyyy-MM-dd")+" "+item.yanhuiEndDt.ToString());
                        lstRili.Add(tbNew);
                    }
                }
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
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
           
                CheckBox ckShow = e.Item.FindControl("ckShow") as CheckBox;
                tbRili item = e.Item.DataItem as tbRili;
                if (item.Show == true)
                {
                    ckShow.Checked = true;
                }
                HiddenField hvID = e.Item.FindControl("hvID") as HiddenField;
                hvID.Value = item.ID.ToString();
                if (item.ID == 0)
                {
                    hvID.Value = item.Source + "|" + item.MainID;
                }
            
            
           
        }

        protected void btSubmit_Click(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(txtDt.Text);
            lbMonth.Text = dt.ToString("MMM yyyy");
            BindRepeat(dt);
        }


        protected void btSave_Click(object sender, EventArgs e)
        {
            using (ebsDBData db = new ebsDBData())
            {
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    bool ckShow = ((CheckBox)item.FindControl("ckShow")).Checked;
                    string hdValue = ((HiddenField)item.FindControl("hvID")).Value;
                    int ID = 0;
                    int HID = 0;
                    string Source = "";
                    if (hdValue.Contains("|"))
                    {
                        Source = hdValue.Split('|')[0];
                        HID =Convert.ToInt32( hdValue.Split('|')[1]);
                    }
                    else
                    {
                        ID = Convert.ToInt32(hdValue);
                    }
                    
                    if (ID != 0)
                    {
                        tbRili tbItem =   db.tbRili.Where(q => q.ID == ID).First();
                      tbItem.Show = ckShow;
                        if (tbItem.Source == "SW")
                        {
                            tbShangwu tbSW = db.tbShangwu.Where(q => q.ID == tbItem.MainID).First();
                            tbItem.StartDate = tbSW.Huodongshijian;
                            tbItem.EndDate = tbSW.Huodongshijian.AddHours(6);
                        }
                        if (tbItem.Source == "HL")
                        {
                            tbMainWedding tbHL = db.tbMainWedding.Where(q => q.ID == tbItem.MainID).First();
                            tbItem.StartDate = tbHL.StartDate;
                            tbItem.EndDate = Convert.ToDateTime(tbHL.StartDate.ToString("yyyy-MM-dd") + " " + tbHL.yanhuiEndDt.ToString());
                        }
                    }
                    else
                    {
                        tbRili tbNew = new tbRili();
                        if (Source == "SW")
                        {
                            tbShangwu tbItem = db.tbShangwu.Where(q => q.ID == HID).First();
                            tbNew.MainID = tbItem.ID;
                            tbNew.Miaoshu = tbItem.Huodongxingshi;
                            tbNew.Renshu = tbItem.Renshu;
                            tbNew.SalesName = ComCls.HandleSales(tbItem.Owner.ToString());
                            tbNew.Show = ckShow;
                            tbNew.Site = tbItem.Changdi;
                            tbNew.Source = "SW";
                            tbNew.StartDate = tbItem.Huodongshijian;
                            tbNew.Title = tbItem.Huodongmingcheng;
                            tbNew.Can = true;
                            tbNew.Status = true;
                            tbNew.EndDate = tbItem.Huodongshijian.AddHours(6);
                        }
                        if (Source == "HL")
                        {
                            tbMainWedding tbItem = db.tbMainWedding.Where(q => q.ID == HID).First();
                            tbNew.MainID = tbItem.ID;
                            tbNew.Miaoshu = tbItem.zongAmount;
                            tbNew.Renshu = tbItem.tbKehu.Zhuoshu;
                            tbNew.SalesName = ComCls.HandleSales(tbItem.Owner.ToString());
                            tbNew.Show = ckShow;
                            tbNew.Site = tbItem.yishiChangdi + "," + tbItem.yanhuiting;
                            tbNew.Source = "HL";
                            tbNew.StartDate = tbItem.StartDate;
                            tbNew.Title = ComCls.HandleXinren(tbItem.Xinlang, tbItem.Xinniang);
                            tbNew.Can = true;
                            tbNew.Status = true;
                            tbNew.EndDate = Convert.ToDateTime(tbItem.StartDate.ToString("yyyy-MM-dd") + " " + tbItem.yanhuiEndDt.ToString());
                        }
                        db.tbRili.InsertOnSubmit(tbNew);
                    }

                    db.SubmitChanges();
                   
                }
            }



            DateTime dt = txtDt.Text.Trim() == "" ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) : Convert.ToDateTime(txtDt.Text);
            lbMonth.Text = dt.ToString("MMM yyyy");
            BindRepeat(dt);
        }

    }
}