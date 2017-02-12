using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using ebs.dbml;
using System.IO;

namespace ebs.MoKuai_Rili
{
    public partial class Import : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo(FileUpload1.PostedFile.FileName);
            string wrongID = "";
            DateTime dtSelect = Convert.ToDateTime(txtYearMonth.Text);
            List<tbRili> lstRili = new List<tbRili>();
            using (ebsDBData db = new ebsDBData())
            {

                using (ExcelPackage ep = new ExcelPackage(fi))
                {
                    ExcelWorksheet ws = ep.Workbook.Worksheets[1];
                    int colStart = 1;// ws.Dimension.Start.Column;  //工作区开始列//
                    int colEnd = 8;//ws.Dimension.End.Column;       //工作区结束列//
                    int rowStart = 14;// ws.Dimension.Start.Row;       //工作区开始行号//
                    int rowEnd = ws.Dimension.End.Row;       //工作区结束行号//

                    for (int r = rowStart; r < rowEnd + 1; r++)
                    {
                        string status = ws.Cells[r, 1].Value.ToString().Trim();
                        string title = ws.Cells[r, 2].Value.ToString().Trim();
                        string iCan =  ws.Cells[r, 3].Value.ToString().Trim();
                        if (status == "" || title == "" || iCan == "")
                        {
                            wrongID+="Row { "+r.ToString()+" } -->标题、状态、餐饮格式错误\\n";
                            continue;
                        }
                        double DouDtS;
                        double DouDtE;
                        if (!double.TryParse(ws.Cells[r, 7].Value.ToString().Trim(),out DouDtS))
                        {
                            wrongID += ("Row { " + r.ToString() + " } -->开始时间格式错误\\n");
                            continue;
                        }
                        if (!double.TryParse(ws.Cells[r, 8].Value.ToString().Trim(), out DouDtE))
                        {
                            wrongID += ("Row { " + r.ToString() + " } -->结束时间格式错误\\n");
                            continue;
                        }
                      
                        tbRili iRili = new tbRili();
                        iRili.Status =  status== "确定"? true:false;
                        iRili.Title = title;
                        iRili.Can = iCan== "Y" ? true : false;
                        iRili.Renshu = ws.Cells[r, 4].Value.ToString().Trim();
                        iRili.SalesName = ws.Cells[r, 5].Value.ToString().Trim();
                        iRili.Site = ws.Cells[r, 6].Value.ToString().Trim();
                        iRili.StartDate = (new DateTime(1900,1,1)).AddDays(DouDtS);
                        iRili.EndDate = (new DateTime(1900, 1, 1)).AddDays(DouDtE);
                        iRili.Source = "Import";

                        if (iRili.StartDate.Month != dtSelect.Month || iRili.StartDate.Year != dtSelect.Year )
                        {
                            wrongID += ("Row { " + r.ToString() + " } -->开始时间不在所选月份内\\n");
                            continue;
                        }

                        lstRili.Add(iRili);

                    }
                    if (lstRili.Count >0 && wrongID == "")
                    {
                        if (ckRemove.Checked)
                        {
                            var res = db.tbRili.Where(q => q.StartDate.Month == dtSelect.Month && q.StartDate.Year == dtSelect.Year && q.Source.Contains("Import"));
                            db.tbRili.DeleteAllOnSubmit(res);
                        }
                        db.tbRili.InsertAllOnSubmit(lstRili);
                        wrongID = "导入成功";
                        db.SubmitChanges();
                    }
                    else
                    {
                        wrongID = "导入失败，请检查Excel中的如下行:\\n" + wrongID;
                      
                    }
                

                    Response.Write("<script language='javascript'>alert('" + wrongID.ToString() + "');window.location='Rili.aspx'</script>");


                }
            }
           
         
        }
    }
}