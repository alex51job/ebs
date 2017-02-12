using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.commons;
using ebs.dbml;

namespace ebs.MoKuai_Hunli
{
    public partial class HunliAudit1 : System.Web.UI.Page
    {
        public int ID
        {
            get { return ViewState["ID"] != null ? Convert.ToInt32(ViewState["ID"].ToString()) : 0; }
            set { ViewState["ID"] = value; }
        }
        public ComCls.LoginUser currentUser
        {
            get { return ((Hunli)(Page.Master)).LoginUserInfo; }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["ID"] = Request.QueryString["ID"];
                initPage();
                WeddingTabs.setWeddingOrderTabs(Request.Url.ToString(), currentUser.roles, ID);
            }
        }

        private void initPage()
        {
            DivAudit.Visible = false;
            using (ebsDBData db = new ebsDBData())
            {
                if (db.WeddingOrders.Any(q => q.ID == ID))
                {
                    List<int> IDs = new List<int>();
                    IDs.Add(ID);
                    if (db.tbWeddingOrdersRevision.Any(q=>q.OrderID == ID))
                    {
                        var contractID = db.tbWeddingOrdersRevision.First(q => q.OrderID == ID).ContractID;
                        var iids = db.tbWeddingOrdersRevision.Where(q => q.ContractID == contractID).Select(q=>q.OrderID).ToList();
                        IDs.AddRange(iids);
                    
                    }
                    var res = from q in db.WeddingOrdersLogs.Where(q => IDs.Contains(q.OrderID))
                              join u in db.tbUsers on q.UserName equals u.UserName
                              select new
                              {
                                  FollowDate = q.Date,
                                  UserName = u.DisplayName,
                                  UserRole = q.UserRole,
                                  AuditType = q.ActionType,
                                  AuditResult = q.ActionName,
                                  Comments = q.Comments
                              };

                    Repeater1.DataSource = res.OrderByDescending(q=>q.FollowDate);
                    Repeater1.DataBind();

                    var WO = db.WeddingOrders.First(q => q.ID == ID);
                   
                    var  audit = WO.AuditRecords.OrderBy(q => q.AuditPriority).FirstOrDefault(q => q.AuditDate == null);
                    if (audit != null)
                    {
                        var role = audit.AuditLevel;
                        if (currentUser.roles.Contains(role))
                        {
                            DivAudit.Visible = true;
                            tbShenheRen.Text = currentUser.DisplayName;
                            tbShenheLevel.Text = currentUser.roles;
                            tbShenheType.Text = audit.AuditType;
                        }
                    }
                   
                    //上传文件
                    if (db.WeddingFiles.Any(q => q.WeddingID == ID))
                    {
                        //RepeaterFile.Visible = true;
                        RepeaterFile.DataSource = db.WeddingFiles.Where(q => q.WeddingID == ID);
                        RepeaterFile.DataBind();
                    }
                    if (currentUser.roles != "文员")
                    {
                        titleAction.Visible = false;
                    }

                    // 日历绑定框
                    if (db.tbRili.Any(q => q.MainID == ID && q.Type == "婚宴")) {
                        RepeaterRili.DataSource = db.tbRili.Where(q => q.MainID == ID && q.Type == "婚宴");
                        RepeaterRili.DataBind();
                    }

                    PlaceHolder1.Visible = false;
                    //如果是文员则display placehode1
                    if (currentUser.roles == "文员")
                    {
                        PlaceHolder1.Visible = true;
                        if (db.tbRili.Any(q => q.MainID == WO.ID && q.Type == "婚宴"))
                        {
                            ddDangqi.DataSource = db.tbRili.Where(q => q.MainID == WO.ID && q.Type == "婚宴");
                            ddDangqi.DataTextField = "Title";
                            ddDangqi.DataValueField = "ID";
                            ddDangqi.DataBind();
                        }
                        else
                        {
                            DateTime dtEvent;
                            if (WO.HunliDate != null)
                            {
                                dtEvent = WO.HunliDate.Value;
                            }
                            else dtEvent = Convert.ToDateTime(db.Customers.FirstOrDefault(q => q.ID == WO.MainID).EventDate);

                            var RiliDangqi = db.tbRili.Where(q => q.StartDate.Date == dtEvent.Date && (q.MainID == null));
                            foreach (var item in RiliDangqi)
                            {
                                //ddDangqi.Items.Insert(0, new ListItem(item.Title + "|" + item.yanhuiting + "|" + item.yishiChangdi + "|" + item.StartDate.ToString("yyyy-MM-dd") + "|" + item.SalesName,item.ID.ToString()));
                                string ti = item.Title + "|" + item.yanhuiting + "|" + item.yishiChangdi + "|" + item.StartDate.ToString("yyyy-MM-dd") + (item.EndDate == null ? ("~" + item.EndDate.Value.ToString("yyyy-MM-dd")) : "") + "|" + item.Source + item.ID.ToString();
                                //ListItem o = new ListItem("", "");
                                ddDangqi.Items.Insert(0, new ListItem(ti,item.ID.ToString()));
                            }
                            ddDangqi.Items.Insert(0, new ListItem());
                        }
                   
                    }

                }
            }
        }
        protected void btUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string path = "/upload/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                string savePath = Server.MapPath("~" + path);
                HttpPostedFile file = FileUpload1.PostedFile;
                string fullname = FileUpload1.FileName;
                string filename = fullname.Substring(0,fullname.LastIndexOf("."));
                string suffix = fullname.Substring(fullname.LastIndexOf(".")).ToLower();
                string timestamp = DateTime.Now.ToFileTime().ToString();
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }

                FileUpload1.PostedFile.SaveAs(savePath + filename + timestamp + suffix);
                using (ebsDBData db = new ebsDBData())
                {
                    WeddingFiles bf = new WeddingFiles
                    {
                        WeddingID = ID,
                        CreatedDt = DateTime.Now,
                        FileName = FileUpload1.FileName,
                        FilePath = ".." + path + filename+ timestamp + suffix
                    };
                    //使用viewstate保存文件名
                    ViewState["files"] += fullname + ",";
                    db.WeddingFiles.InsertOnSubmit(bf);
                    db.SubmitChanges();
                    RepeaterFile.DataSource = db.WeddingFiles.Where(q => q.WeddingID == ID).OrderByDescending(q => q.CreatedDt);
                    RepeaterFile.DataBind();

                   
                }
            }
        }
        protected void btPass_Click(object sender, EventArgs e)
        {
            using (ebsDBData db = new ebsDBData())
            {
                var logs = new WeddingOrdersLogs();
                logs.UserName = currentUser.userName;
                logs.Date = DateTime.Now;
                logs.UserRole = currentUser.roles;
                logs.ActionName = "审批完成";
                logs.ActionType = tbShenheType.Text;
                logs.Comments = tbShenheYijian.Text;

                var WO = db.WeddingOrders.First(q => q.ID == ID);
                WO.WeddingOrdersLogs.Add(logs);

                var audit = WO.AuditRecords.OrderBy(q => q.AuditPriority).First(q => q.AuditDate == null);
                audit.AuditDate = DateTime.Now;
                audit.AuditUser = currentUser.userName;
                audit.AuditSuggest = tbShenheYijian.Text;
                audit.AuditResult = "审批完成";

                if (WO.AuditRecords.All(q => q.AuditResult == "审批完成"))
                {
                    WO.Zhuangtai = "审批完成";
                }
                if (currentUser.roles == "文员")
                {
                    db.tbRili.First(q => q.ID == Convert.ToInt32(ddDangqi.SelectedValue)).MainID = WO.ID;
                    db.tbRili.First(q => q.ID == Convert.ToInt32(ddDangqi.SelectedValue)).Type = "婚宴";
                    //logs.Comments = logs.Comments + " 绑定档期：" + db.tbRili.First(q => q.ID == Convert.ToInt32(ddDangqi.SelectedValue)).Title;
                }
             
                db.SubmitChanges();

            }
            Response.Redirect(Request.Url.ToString());
        }

        protected void btReject_Click(object sender, EventArgs e)
        {
            using (ebsDBData db = new ebsDBData())
            {
                var logs = new WeddingOrdersLogs();
                logs.UserName = currentUser.userName;
                logs.Date = DateTime.Now;
                logs.UserRole = currentUser.roles;
                logs.ActionName = "退回";
                logs.ActionType = tbShenheType.Text;
                logs.Comments = tbShenheYijian.Text;

                var WO = db.WeddingOrders.First(q => q.ID == ID);
                WO.WeddingOrdersLogs.Add(logs);
                db.AuditRecords.DeleteAllOnSubmit(WO.AuditRecords);
                WO.Zhuangtai = "编辑";

                db.SubmitChanges();
            }
            Response.Redirect(Request.Url.ToString());
        }

        protected void RepeaterFile_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                if (currentUser.roles != "文员")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoWenyuan", "alert('你无此权限！');", true);
                    return;
                }
                int fileID = Convert.ToInt32(e.CommandArgument);
                using (ebsDBData db = new ebsDBData())
                {
                    if (db.WeddingFiles.Any(q => q.ID == fileID))
                    {
                        WeddingFiles wf = db.WeddingFiles.First(q => q.ID == fileID);

                        string filePath = Server.MapPath("~" + wf.FilePath.Substring(2));
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                        db.WeddingFiles.DeleteOnSubmit(wf);
                        db.SubmitChanges();
                    }
                    RepeaterFile.DataSource = db.WeddingFiles.Where(q => q.WeddingID == ID);
                    RepeaterFile.DataBind();
                }
               
            }
        }

        protected void RepeaterFile_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (currentUser.roles != "文员")
            {
                e.Item.FindControl("tdAction").Visible = false;
                e.Item.FindControl("lbdel").Visible = false;

            }
            else
            {
                string fn = ((HiddenField)e.Item.FindControl("hfFileName")).Value;
                object vfn = ViewState["files"];
                if (vfn !=null && vfn.ToString().Contains(fn))
                {
                    e.Item.FindControl("lbdel").Visible = true;
                }
            }
        }
    }
}