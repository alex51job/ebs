using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.dbml;
using ebs.Tools;
using ebs.commons;
using System.Text;


namespace ebs.MoKuai_Kehu
{
   
    public partial class KehuiEdit : System.Web.UI.Page
    {
        public int ID {
             get { return ViewState["ID"] == null? 0: Convert.ToInt32(ViewState["ID"].ToString()); }
             set { ViewState["ID"] = value; }
         }
        public ComCls.LoginUser currentUser
         {
             get { return ((Kehu)(Page.Master)).LoginUserInfo; }
         }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ID = Convert.ToInt32(Request.QueryString["ID"]);
                initPage();
                setButtons();
                
            }
        }
       
        public void initPage()
        {
            CustomerTabs.setCustomerTabs(Request.Url.ToString(), ID);
            PHSWKehu.Visible = false;
            string Type = Request.QueryString["type"];
            if (Type == "w")
            {
                ddQudao.ddlForQudao("婚宴");
                ddCustomerType.ddlForCustomerType("婚宴");
                ddCustomerType.setDisabled();
                tbCompany.Text = "NA";
                
            }
            if (Type == "b")
            {
                ddQudao.ddlForQudao("商务");
                ddCustomerType.ddlForCustomerType("商务");
                ddCustomerType.setDisabled();
                PHSWKehu.Visible = true;
            }
            
            ddZixundidian.ddlForMenDian();
            if (Type == "z")
            {
                ddQudao.ddlForQudao("商务");
                ddCustomerType.ddlForCustomerType("商务");
                ddCustomerType.setDisabled();
                lbKefu.Text = "商务销售";
                ddSales.ddlForSales(currentUser.userName, "All", "商务");
                ddZixundidian.ddlForMenDian(currentUser.region);
                ddSales.setReadonly();
                PHSWKehu.Visible = true;
                btSubmit.Visible = false;
                //btSubmit.Text = "保存";
            }
            using (ebsDBData db = new ebsDBData())
            {
                
                if (ID != 0)
                {
                    Customers item = db.Customers.First(q => q.ID == ID);
                    lbID.Text = item.ID.ToString().PadLeft(4,'0');
                    ddQudao.ddlForQudao(item.CustomerType, item.Source);
                    tbQudaobianhao.Text = item.SourceNb;
                    txtInputDate.Text = item.CreationDate.ToString("yyyy-MM-dd");
                    ddZixundidian.ddlForMenDian(item.ZixunDiDian);
                    tbCustomerName.Text = item.CustomerName;
                    tbCompany.Text = item.Company;
                    tbZuoji.Text = item.Telephone;
                    tbCustomerQQ.Text = item.QQNb;
                    ddCustomerType.ddlForCustomerType(item.CustomerType);
                    tbZhuoshu.Text = item.Zhuoshu;
                    tbHunqi.Text = item.EventDate;
                    tbYuyue.Text = item.Yuyue;
                    lbZhuangtai.Text = item.Zhuangtai;
                    ddSales.ddlForSales(item.Sales,item.ZixunDiDian);
                    lbXiafa.Text = item.Xiafashijian;
                    lbKefu.Text = item.Kefu.ToDisplayName();
                    tbQita.Text = item.Beizhu;
                    tbKefuFollow.Text = item.KefuFollow;
                    Rptlog.DataSource = item.CustomerRecords.OrderByDescending(q=>q.ModifyDate);
                    Rptlog.DataBind();
                    //if (item.CustomerType == "商务")
                    //{
                    //    tbZhuoshu.setDisabled();
                    //    tbYuyue.setDisabled();
                    //}
                    if (item.Kefu == "商务销售" || item.CustomerType == "商务")
                    {
                        PHSWKehu.Visible = true;
                    }
                    if (db.BussinessAdditionalForSW.Count(q=>q.BussinessID == ID) > 0)
                    {
                        BussinessAdditionalForSW BSW = db.BussinessAdditionalForSW.First(q => q.BussinessID == ID);
                        //tbSWHangye.Text = BSW.Hangye;
                        tbSWHangye.Text = BSW.Hangye;
                        tbSWLianxiren1.Text = BSW.Lianxiren1;
                        tbSWMail1.Text = BSW.Youjian1;
                        tbSWZhiwei1.Text = BSW.Zhiwei1;
                        tbSWZuoji1.Text = BSW.Zuoji1;
                        tbSWShouji1.Text = BSW.Shouji1;
                        tbSWLianxiren2.Text = BSW.Lianxiren2;
                        tbSWMail2.Text = BSW.Youjian2;
                        tbSWZhiwei2.Text = BSW.Zhiwei2;
                        tbSWZuoji2.Text = BSW.Zuoji2;
                        tbSWShouji2.Text = BSW.Shouji2;
                        tbSWDizhi.Text = BSW.Dizhi;
                        
                    }

                    
                }
            }
        }

        private void setButtons()
        {
            DivAudit.Visible = false;
            btApplyWuxiao.Visible = false;
            btSubmitApply.Visible = false;
            btRedrewApply.Visible = false;
            btPass.Visible = false;
            btKickback.Visible = false;
            btChongxinXiafa.Visible = false;
            btResetApply.Visible = false;

            if (ID !=0)
            {
                Customers cus;
                using (ebsDBData db = new ebsDBData())
                {
                    cus = db.Customers.First(q => q.ID == ID);
                    if (cus.Kefu != currentUser.userName && currentUser.roles !="客服主管")
                    {
                        btSubmit.Visible = false;
                        btXiafa.Visible = false;
                        btApplyWuxiao.Visible = false;
                    }

                    if (cus.Zhuangtai == "已下发")
                    {
                        btXiafa.Visible = false;
                        ddSales.setDisabled();//.Attributes.Add("disabled","disabled");
                        ddCustomerType.setDisabled();
                        ddZixundidian.setDisabled();
                    }
                    tbAuditReason.Text = cus.AuditContent;
                    lbAuditStatus.Text = cus.AuditType + " "+ cus.AuditStatus;

                    if (currentUser.roles == "客服" && cus.Kefu == currentUser.userName)
                    {
                        if (cus.AuditStatus == "" && cus.AuditType == "") btApplyWuxiao.Visible = true;

                        if (cus.AuditStatus == "未审批" || cus.AuditStatus == "退回")
                        {
                            btRedrewApply.Visible = true;
                            DivAudit.Visible = true;
                        }
                        if (cus.AuditStatus == "通过")
                        {
                            DivAudit.Visible = true;
                        }
                    }
                    if (currentUser.roles == "客服主管")
                    {
                      
                        if (cus.Zhuangtai == "已下发")
                        {
                            btChongxinXiafa.Visible = false;
                            ddSales.setEnabled();
                            ddCustomerType.setEnabled();
                            ddZixundidian.setEnabled();
                        }

                        if (cus.AuditStatus == "未审批")
                        {
                            DivAudit.Visible = true;
                            btPass.Visible = true;
                            btKickback.Visible = true;
                            btSubmit.Visible = false;
                            btXiafa.Visible = false;
                            btChongxinXiafa.Visible = false;
                        }

                        if (cus.AuditStatus == "通过" || cus.AuditStatus == "退回")
                        {
                            DivAudit.Visible = true;
                            btResetApply.Visible = true;
                            btSubmit.Visible = false;
                            btXiafa.Visible = false;
                            btChongxinXiafa.Visible = false;
                          
                        }
                    }

                  
                }
            }

            if (currentUser.roles == "商务销售" || currentUser.roles == "商务销售主管")
            {
                //btSubmit.Visible = true;
                btXiafa.Visible = true;
                btXiafa.Text = "保存";
            }

        }

        protected void btApplyWuxiao_Click(object sender, EventArgs e)
        {
            DivAudit.Visible = true;
            btSubmitApply.Visible = true;
            btRedrewApply.Visible = true;
            btApplyWuxiao.Visible = false;
            tbAuditReason.Focus();
        }

        protected void btSubmitApply_Click(object sender, EventArgs e)
        {
            using (ebsDBData db = new ebsDBData())
            {
                var cus = db.Customers.First(q => q.ID == ID);
                cus.AuditType = "无效";
                cus.AuditStatus = "未审批";
                cus.AuditContent = tbAuditReason.Text;
                db.SubmitChanges();
            }
         
            Response.Redirect(Request.Url.ToString());
        }

        protected void btRedrewApply_Click(object sender, EventArgs e)
        {
            using (ebsDBData db = new ebsDBData())
            {
                var cus = db.Customers.First(q => q.ID == ID);
                if (cus.AuditStatus == "未审批" || cus.AuditStatus == "退回")
                {
                    cus.AuditStatus = "";
                    cus.AuditType = "";
                    cus.AuditContent = "";
                    db.SubmitChanges();
                }
            }
            Response.Redirect(Request.Url.ToString());
        }

        protected void btPass_Click(object sender, EventArgs e)
        {
            using (ebsDBData db = new ebsDBData())
            {
                var cus = db.Customers.First(q => q.ID == ID);
                if (cus.AuditStatus == "未审批")
                {
                    cus.AuditStatus = "通过";
                    db.SubmitChanges();
                }
            }
            Response.Redirect(Request.Url.ToString());
        }

        protected void btKickback_Click(object sender, EventArgs e)
        {
            using (ebsDBData db = new ebsDBData())
            {
                var cus = db.Customers.First(q => q.ID == ID);
                if (cus.AuditStatus == "未审批")
                {
                    cus.AuditStatus = "退回";
                    db.SubmitChanges();
                }
            }
            Response.Redirect(Request.Url.ToString());
        }

        protected void btResetApply_Click(object sender, EventArgs e)
        {
            using (ebsDBData db = new ebsDBData())
            {
                var cus = db.Customers.First(q => q.ID == ID);
                if (cus.AuditType == "无效")
                {
                    cus.AuditStatus = "";
                    cus.AuditContent = "";
                    cus.AuditStatus = "";
                    db.SubmitChanges();
                }
            }
            Response.Redirect("KehuList.aspx");
        }

        protected void btSubmit_Click(object sender, EventArgs e)
        {
            using (ebsDBData db = new ebsDBData())
            {
                if (ID == 0)
                {
                    Customers item = new Customers();
                    item.Source = ddQudao.SelectedValue;
                    item.SourceNb = tbQudaobianhao.Text.Trim();
                    item.CreationDate =Convert.ToDateTime(txtInputDate.Text);
                    item.ZixunDiDian = ddZixundidian.SelectedValue;
                    item.CustomerName = tbCustomerName.Text.Trim();
                    item.Company = tbCompany.Text.Trim();
                    item.Telephone = tbZuoji.Text.Trim();
                    item.QQNb = tbCustomerQQ.Text.Trim();
                    item.CustomerType = ddCustomerType.SelectedValue;
                  
                    item.Zhuoshu = tbZhuoshu.Text.Trim();
                    item.EventDate = tbHunqi.Text.Trim();
                    item.Yuyue =tbYuyue.Text;
                    
                    if (item.Zhuangtai == null || item.Zhuangtai == "")
                    {
                        item.Zhuangtai = "未下发";
                    }

                    item.Sales = ddSales.SelectedValue;
                    item.Xiafashijian = "";
                    item.Kefu = currentUser.userName;
                    item.Beizhu = tbQita.Text;
                    item.KefuFollow = tbKefuFollow.Text;
                  
                    //若未下发，则不会进去可审批的状态
                    item.DaoDianCount = 0;
                    item.NeedHuiFang = "";
                    item.AuditType = "";
                    item.AuditStatus = "";
                    item.AuditContent = "";
                   

                    db.Customers.InsertOnSubmit(item);
                    db.SubmitChanges();
                    int currentID = item.ID;
                    CustomerRecords CR = new CustomerRecords();
                    CR.MainID = currentID;
                    CR.ModifyDate = DateTime.Now;
                    CR.ModifyRecord = "初始录入";
                    CR.ModifyUser = currentUser.DisplayName;
                    db.CustomerRecords.InsertOnSubmit(CR);
                    db.SubmitChanges();
                    if (item.CustomerType == "商务")
                    {
                        if (db.BussinessAdditionalForSW.Any(q => q.BussinessID == ID))
                        {
                            BussinessAdditionalForSW BSW = db.BussinessAdditionalForSW.First(q => q.BussinessID == ID);
                            BSW.Hangye = tbSWHangye.Text;
                            BSW.Lianxiren1 = tbSWLianxiren1.Text;
                            BSW.Zuoji1 = tbSWZuoji1.Text;
                            BSW.Shouji1 = tbSWShouji1.Text;
                            BSW.Youjian1 = tbSWMail1.Text;
                            BSW.Zhiwei1 = tbSWZhiwei1.Text;

                            BSW.Lianxiren2 = tbSWLianxiren2.Text;
                            BSW.Zuoji2 = tbSWZuoji2.Text;
                            BSW.Shouji2 = tbSWShouji2.Text;
                            BSW.Youjian2 = tbSWMail2.Text;
                            BSW.Zhiwei2 = tbSWZhiwei2.Text;

                            BSW.Dizhi = tbSWDizhi.Text;

                        }
                        else
                        {
                            BussinessAdditionalForSW BSW = new BussinessAdditionalForSW();
                            BSW.Hangye = tbSWHangye.Text;
                            BSW.Lianxiren1 = tbSWLianxiren1.Text;
                            BSW.Zuoji1 = tbSWZuoji1.Text;
                            BSW.Shouji1 = tbSWShouji1.Text;
                            BSW.Youjian1 = tbSWMail1.Text;
                            BSW.Zhiwei1 = tbSWZhiwei1.Text;

                            BSW.Lianxiren2 = tbSWLianxiren2.Text;
                            BSW.Zuoji2 = tbSWZuoji2.Text;
                            BSW.Shouji2 = tbSWShouji2.Text;
                            BSW.Youjian2 = tbSWMail2.Text;
                            BSW.Zhiwei2 = tbSWZhiwei2.Text;

                            BSW.Dizhi = tbSWDizhi.Text;
                            BSW.BussinessID = item.ID;
                            db.BussinessAdditionalForSW.InsertOnSubmit(BSW);
                        }
                    }
                    db.SubmitChanges();
                }
                else
                {
                    Customers item = db.Customers.First(q => q.ID == ID);
                    List<CustomerRecords> CusRecords = new List<CustomerRecords>();
                    if (item.Source != ddQudao.SelectedValue)
                    {
                        MakeARecord(CusRecords, item.Source, ddQudao.SelectedValue, "来源");
                        item.Source = ddQudao.SelectedValue;
                    }
                    if (item.SourceNb != tbQudaobianhao.Text.Trim())
                    {
                        MakeARecord(CusRecords, item.SourceNb, tbQudaobianhao.Text.Trim(), "渠道编号");
                        item.SourceNb = tbQudaobianhao.Text.Trim();
                    }

                    if (item.CreationDate.ToString("yyyy-MM-dd") != txtInputDate.Text)
                    {
                        MakeARecord(CusRecords, item.CreationDate.ToString("yyyy-MM-dd"), txtInputDate.Text, "录入日期");
                        item.CreationDate = Convert.ToDateTime(txtInputDate.Text);
                    }
                    if (item.ZixunDiDian != ddZixundidian.SelectedValue)
                    {
                        MakeARecord(CusRecords, item.ZixunDiDian, ddZixundidian.SelectedValue, "咨询地点");
                        item.ZixunDiDian = ddZixundidian.SelectedValue;

                    }
                    if (item.CustomerName != tbCustomerName.Text.Trim())
                    {
                        MakeARecord(CusRecords, item.CustomerName, tbCustomerName.Text.Trim(), "客户姓名");
                        item.CustomerName = tbCustomerName.Text.Trim();

                    }
                    if (item.Company != tbCompany.Text.Trim())
                    {
                        MakeARecord(CusRecords, item.Company, tbCompany.Text.Trim(), "公司名");
                        item.Company = tbCompany.Text.Trim();

                    }
                   
                    if ( item.Telephone != tbZuoji.Text.Trim())
                    {
                        MakeARecord(CusRecords,item.Telephone,tbZuoji.Text.Trim(),"客户电话");
                         item.Telephone = tbZuoji.Text.Trim();
                    }
                    if ( item.QQNb != tbCustomerQQ.Text.Trim())
                    {
                        MakeARecord(CusRecords, item.QQNb, tbCustomerQQ.Text.Trim(), "客户QQ");
                        item.QQNb = tbCustomerQQ.Text.Trim();
                    }
                    if (item.CustomerType != ddCustomerType.SelectedValue)
                    {
                        MakeARecord(CusRecords, item.CustomerType, ddCustomerType.SelectedValue,"客户类型");
                        item.CustomerType = ddCustomerType.SelectedValue;
                    }
                   
                    if (item.Zhuoshu != tbZhuoshu.Text.Trim())
                    {
                        MakeARecord(CusRecords, item.Zhuoshu, tbZhuoshu.Text.Trim(),"桌数");
                        item.Zhuoshu = tbZhuoshu.Text.Trim();
                    }
                    
                    if (item.EventDate != tbHunqi.Text.Trim())
                    {
                        MakeARecord(CusRecords, item.EventDate, tbHunqi.Text.Trim(), "婚期/活动时间");
                        item.EventDate = tbHunqi.Text.Trim();
                    }
                    
                    if (item.Yuyue!= tbYuyue.Text)
                    {
                        MakeARecord(CusRecords, item.Yuyue, tbYuyue.Text, "到店预约时间");
                        item.Yuyue = tbYuyue.Text;

                    }
                    if (item.Sales != ddSales.SelectedValue)
                    {
                        MakeARecord(CusRecords, item.Sales.ToDisplayName(), ddSales.SelectedValue.ToDisplayName(),"负责销售");
                        item.Sales = ddSales.SelectedValue;
                    }

                    item.Beizhu = tbQita.Text;
                    item.KefuFollow = tbKefuFollow.Text;
                    if (CusRecords.Count >0)
                    {
                        db.CustomerRecords.InsertAllOnSubmit(CusRecords);
                    }
                    db.SubmitChanges();

                    if (item.Kefu == "商务销售" || item.CustomerType == "商务")
                    {
                        if (db.BussinessAdditionalForSW.Any(q => q.BussinessID == ID))
                        {
                            BussinessAdditionalForSW BSW = db.BussinessAdditionalForSW.First(q => q.BussinessID == ID);
                            BSW.Hangye = tbSWHangye.Text;
                            BSW.Lianxiren1 = tbSWLianxiren1.Text;
                            BSW.Zuoji1 = tbSWZuoji1.Text;
                            BSW.Shouji1 = tbSWShouji1.Text;
                            BSW.Youjian1 = tbSWMail1.Text;
                            BSW.Zhiwei1 = tbSWZhiwei1.Text;

                            BSW.Lianxiren2 = tbSWLianxiren2.Text;
                            BSW.Zuoji2 = tbSWZuoji2.Text;
                            BSW.Shouji2 = tbSWShouji2.Text;
                            BSW.Youjian2 = tbSWMail2.Text;
                            BSW.Zhiwei2 = tbSWZhiwei2.Text;

                            BSW.Dizhi = tbSWDizhi.Text;

                        }
                        else
                        {
                            BussinessAdditionalForSW BSW = new BussinessAdditionalForSW();
                            BSW.Hangye = tbSWHangye.Text;
                            BSW.Lianxiren1 = tbSWLianxiren1.Text;
                            BSW.Zuoji1 = tbSWZuoji1.Text;
                            BSW.Shouji1 = tbSWShouji1.Text;
                            BSW.Youjian1 = tbSWMail1.Text;
                            BSW.Zhiwei1 = tbSWZhiwei1.Text;

                            BSW.Lianxiren2 = tbSWLianxiren2.Text;
                            BSW.Zuoji2 = tbSWZuoji2.Text;
                            BSW.Shouji2 = tbSWShouji2.Text;
                            BSW.Youjian2 = tbSWMail2.Text;
                            BSW.Zhiwei2 = tbSWZhiwei2.Text;

                            BSW.Dizhi = tbSWDizhi.Text;
                            BSW.BussinessID = ID;
                            db.BussinessAdditionalForSW.InsertOnSubmit(BSW);
                        }

                    }
                    db.SubmitChanges();
                }
            }

            Response.Redirect("KehuList.aspx");
            //Response.Write("<script>location.href='KehuList.aspx'</script>");

        }

        private void MakeARecord(List<CustomerRecords> CusRecords, string OrginalStr, string ModifyedStr, string Category)
        {
            CustomerRecords CR = new CustomerRecords();
            CR.MainID = ID;
            CR.ModifyDate = DateTime.Now;
            CR.ModifyUser = currentUser.DisplayName;
            CR.ModifyRecord = string.Format("\"{0}\"由\"{1}\"修改为\"{2}\"", Category, OrginalStr, ModifyedStr);
            CusRecords.Add(CR);
        }

        protected void ddZixundidian_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            ddSales.ddlForSales("", ddZixundidian.SelectedValue, ddCustomerType.SelectedValue);
        }

        protected void ddCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddSales.ddlForSales("", ddZixundidian.SelectedValue, ddCustomerType.SelectedValue);
        }

        protected void btXiafa_Click(object sender, EventArgs e)
        {
            using (ebsDBData db = new ebsDBData())
            {
                if (ID == 0)
                {
                    Customers item = new Customers();
                    item.Source = ddQudao.SelectedValue;
                    item.SourceNb = tbQudaobianhao.Text.Trim();
                    item.CreationDate = Convert.ToDateTime(txtInputDate.Text);
                    item.ZixunDiDian = ddZixundidian.SelectedValue;
                    item.CustomerName = tbCustomerName.Text.Trim();
                    item.Company = tbCompany.Text.Trim();
                    item.Telephone = tbZuoji.Text.Trim();
                    item.QQNb = tbCustomerQQ.Text.Trim();
                    item.CustomerType = ddCustomerType.SelectedValue;
                    item.Zhuoshu = tbZhuoshu.Text.Trim();
                    item.EventDate = tbHunqi.Text.Trim();
                    item.Yuyue = tbYuyue.Text;

                    if (item.Zhuangtai == null || item.Zhuangtai == "")
                    {
                        item.Zhuangtai = "已下发";
                    }

                    item.Sales = ddSales.SelectedValue;
                    item.Xiafashijian = DateTime.Now.ToString("yyyy-MM-dd");
                  
                    item.Beizhu = tbQita.Text;
                    item.KefuFollow = tbKefuFollow.Text;


                    if (lbKefu.Text == "商务销售")
                    {
                        item.Kefu = "商务销售";
                    }
                    else
                    {
                        item.Kefu = currentUser.userName;
                    }

                    //下发，进去可审批的状态
                    item.DaoDianCount = 0;
                    item.NeedHuiFang = "";
                    item.AuditType = "";
                    item.AuditStatus = "";
                    item.AuditContent = "";


                    db.Customers.InsertOnSubmit(item);
                    db.SubmitChanges();

                    if (lbKefu.Text == "商务销售")
                    {
                        item.Kefu = "商务销售";
                        BussinessAdditionalForSW BSW = new BussinessAdditionalForSW();
                        BSW.Hangye = tbSWHangye.Text;
                        BSW.Lianxiren1 = tbSWLianxiren1.Text;
                        BSW.Zuoji1 = tbSWZuoji1.Text;
                        BSW.Shouji1 = tbSWShouji1.Text;
                        BSW.Youjian1 = tbSWMail1.Text;
                        BSW.Zhiwei1 = tbSWZhiwei1.Text;

                        BSW.Lianxiren2 = tbSWLianxiren2.Text;
                        BSW.Zuoji2 = tbSWZuoji2.Text;
                        BSW.Shouji2 = tbSWShouji2.Text;
                        BSW.Youjian2 = tbSWMail2.Text;
                        BSW.Zhiwei2 = tbSWZhiwei2.Text;

                        BSW.Dizhi = tbSWDizhi.Text;
                        BSW.BussinessID = item.ID;
                        db.BussinessAdditionalForSW.InsertOnSubmit(BSW);


                    }
                    else
                    {
                        item.Kefu = currentUser.userName;
                        if (item.CustomerType == "商务")
                        {
                            BussinessAdditionalForSW BSW = new BussinessAdditionalForSW();
                            BSW.Hangye = tbSWHangye.Text;
                            BSW.Lianxiren1 = tbSWLianxiren1.Text;
                            BSW.Zuoji1 = tbSWZuoji1.Text;
                            BSW.Shouji1 = tbSWShouji1.Text;
                            BSW.Youjian1 = tbSWMail1.Text;
                            BSW.Zhiwei1 = tbSWZhiwei1.Text;

                            BSW.Lianxiren2 = tbSWLianxiren2.Text;
                            BSW.Zuoji2 = tbSWZuoji2.Text;
                            BSW.Shouji2 = tbSWShouji2.Text;
                            BSW.Youjian2 = tbSWMail2.Text;
                            BSW.Zhiwei2 = tbSWZhiwei2.Text;

                            BSW.Dizhi = tbSWDizhi.Text;
                            BSW.BussinessID = ID;
                            db.BussinessAdditionalForSW.InsertOnSubmit(BSW);

                        }
                    }


                    int currentID = item.ID;
                    CustomerRecords CR = new CustomerRecords();
                    CR.MainID = currentID;
                    CR.ModifyDate = DateTime.Now;
                    CR.ModifyRecord = "初始录入";
                    CR.ModifyUser = currentUser.DisplayName;
                    db.CustomerRecords.InsertOnSubmit(CR);
                    db.SubmitChanges();

               
                }
                else
                {
                    Customers item = db.Customers.First(q => q.ID == ID);
                    List<CustomerRecords> CusRecords = new List<CustomerRecords>();
                    if (item.Source != ddQudao.SelectedValue)
                    {
                        MakeARecord(CusRecords, item.Source, ddQudao.SelectedValue, "来源");
                        item.Source = ddQudao.SelectedValue;
                    }
                    if (item.SourceNb != tbQudaobianhao.Text.Trim())
                    {
                        MakeARecord(CusRecords, item.SourceNb, tbQudaobianhao.Text.Trim(), "渠道编号");
                        item.SourceNb = tbQudaobianhao.Text.Trim();
                    }

                    if (item.CreationDate.ToString("yyyy-MM-dd") != txtInputDate.Text)
                    {
                        MakeARecord(CusRecords, item.CreationDate.ToString("yyyy-MM-dd"), txtInputDate.Text, "录入日期");
                        item.CreationDate = Convert.ToDateTime(txtInputDate.Text);
                    }
                    if (item.ZixunDiDian != ddZixundidian.SelectedValue)
                    {
                        MakeARecord(CusRecords, item.ZixunDiDian, ddZixundidian.SelectedValue, "咨询地点");
                        item.ZixunDiDian = ddZixundidian.SelectedValue;

                    }
                    if (item.CustomerName != tbCustomerName.Text.Trim())
                    {
                        MakeARecord(CusRecords, item.CustomerName, tbCustomerName.Text.Trim(), "客户姓名");
                        item.CustomerName = tbCustomerName.Text.Trim();

                    }
                    if (item.Company != tbCompany.Text.Trim())
                    {
                        MakeARecord(CusRecords, item.Company, tbCompany.Text.Trim(), "公司名");
                        item.Company = tbCompany.Text.Trim();

                    }
                    if (item.Telephone != tbZuoji.Text.Trim())
                    {
                        MakeARecord(CusRecords, item.Telephone, tbZuoji.Text.Trim(), "客户电话");
                        item.Telephone = tbZuoji.Text.Trim();
                    }
                    if (item.QQNb != tbCustomerQQ.Text.Trim())
                    {
                        MakeARecord(CusRecords, item.QQNb, tbCustomerQQ.Text.Trim(), "客户QQ");
                        item.QQNb = tbCustomerQQ.Text.Trim();
                    }
                    if (item.CustomerType != ddCustomerType.SelectedValue)
                    {
                        MakeARecord(CusRecords, item.CustomerType, ddCustomerType.SelectedValue, "客户类型");
                        item.CustomerType = ddCustomerType.SelectedValue;
                    }

                    if (item.Zhuoshu != tbZhuoshu.Text.Trim())
                    {
                        MakeARecord(CusRecords, item.Zhuoshu, tbZhuoshu.Text.Trim(), "桌数");
                        item.Zhuoshu = tbZhuoshu.Text.Trim();
                    }

                    if (item.EventDate != tbHunqi.Text.Trim())
                    {
                        MakeARecord(CusRecords, item.EventDate, tbHunqi.Text.Trim(), "婚期/活动时间");
                        item.EventDate = tbHunqi.Text.Trim();
                    }

                    if (item.Yuyue!= tbYuyue.Text)
                    {
                        MakeARecord(CusRecords, item.Yuyue, tbYuyue.Text, "到店预约时间");
                        item.Yuyue = tbYuyue.Text;

                    }
                    //if (item.Sales != ddSales.SelectedValue)
                    //{
                    //    MakeARecord(CusRecords, item.Sales.ToDisplayName(), ddSales.SelectedValue.ToDisplayName(), "负责销售");
                    //    item.Sales = ddSales.SelectedValue;
                    //}
                    item.Zhuangtai = "已下发";
                    item.Xiafashijian = DateTime.Now.ToString("yyyy-MM-dd");
                    item.Beizhu = tbQita.Text;
                    item.KefuFollow = tbKefuFollow.Text;
                    if (CusRecords.Count > 0)
                    {
                        db.CustomerRecords.InsertAllOnSubmit(CusRecords);
                    }
                    if (item.Kefu == "商务销售" || item.CustomerType == "商务")
                    {
                        if (db.BussinessAdditionalForSW.Any(q => q.BussinessID == ID))
                        {
                            BussinessAdditionalForSW BSW = db.BussinessAdditionalForSW.First(q => q.BussinessID == ID);
                            BSW.Hangye = tbSWHangye.Text;
                            BSW.Lianxiren1 = tbSWLianxiren1.Text;
                            BSW.Zuoji1 = tbSWZuoji1.Text;
                            BSW.Shouji1 = tbSWShouji1.Text;
                            BSW.Youjian1 = tbSWMail1.Text;
                            BSW.Zhiwei1 = tbSWZhiwei1.Text;

                            BSW.Lianxiren2 = tbSWLianxiren2.Text;
                            BSW.Zuoji2 = tbSWZuoji2.Text;
                            BSW.Shouji2 = tbSWShouji2.Text;
                            BSW.Youjian2 = tbSWMail2.Text;
                            BSW.Zhiwei2 = tbSWZhiwei2.Text;

                            BSW.Dizhi = tbSWDizhi.Text;

                        }
                        else
                        {
                            BussinessAdditionalForSW BSW = new BussinessAdditionalForSW();
                            BSW.Hangye = tbSWHangye.Text;
                            BSW.Lianxiren1 = tbSWLianxiren1.Text;
                            BSW.Zuoji1 = tbSWZuoji1.Text;
                            BSW.Shouji1 = tbSWShouji1.Text;
                            BSW.Youjian1 = tbSWMail1.Text;
                            BSW.Zhiwei1 = tbSWZhiwei1.Text;

                            BSW.Lianxiren2 = tbSWLianxiren2.Text;
                            BSW.Zuoji2 = tbSWZuoji2.Text;
                            BSW.Shouji2 = tbSWShouji2.Text;
                            BSW.Youjian2 = tbSWMail2.Text;
                            BSW.Zhiwei2 = tbSWZhiwei2.Text;

                            BSW.Dizhi = tbSWDizhi.Text;
                            BSW.BussinessID = ID;
                            db.BussinessAdditionalForSW.InsertOnSubmit(BSW);
                        }
                       
                    }
                    db.SubmitChanges();
                }
            }

            Response.Redirect("KehuList.aspx");
        }

       

    }
}