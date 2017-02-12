using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.dbml;


namespace ebs.MoKuai_Shangwu
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID = 1;
                using (ebsDBData db = new ebsDBData())
                {
                    if (db.BussinessFiles.Any(q => q.BussinessID == ID))
                    {
                        RepeaterFile.Visible = true;
                        RepeaterFile.DataSource = db.BussinessFiles.Where(q => q.BussinessID == ID);
                        RepeaterFile.DataBind();
                    }
                    else RepeaterFile.Visible = false;

                }
            }
            
        }

        protected void btUpload_Click(object sender, EventArgs e)
        {
            int BussineID = 1;
            if (FileUpload1.HasFile)
            {
                string path = "/upload/"+DateTime.Now.ToString("yyyyMMdd")+"/";
                string savePath = Server.MapPath("~"+path);
                HttpPostedFile file =  FileUpload1.PostedFile;
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }

                FileUpload1.PostedFile.SaveAs(savePath + FileUpload1.FileName);
                using (ebsDBData db = new ebsDBData())
                {
                    BussinessFiles bf = new BussinessFiles
                    {
                        BussinessID = BussineID,
                        CreatedDt = DateTime.Now,
                        FileName = FileUpload1.FileName,
                        FilePath = ".."+path + FileUpload1.FileName
                    };
                    db.BussinessFiles.InsertOnSubmit(bf);
                    db.SubmitChanges();
                    RepeaterFile.DataSource = db.BussinessFiles.Where(q => q.BussinessID == BussineID).OrderByDescending(q=>q.CreatedDt);
                    RepeaterFile.DataBind();
                }


             
                //RepeaterFile.DataSource
            }
        }
      
    }
}