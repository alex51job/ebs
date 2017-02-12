using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.IO;
using ebs.dbml;
using ebs.commons;

namespace ebs.MoKuai_Report
{
    public partial class ReportMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ReportExport1_Click(object sender, EventArgs e)
        {
            DateTime dateSart, dateEnd;
            if (!DateTime.TryParse(tbHetongStart.Text, out dateSart) || !DateTime.TryParse(tbHetongEnd.Text, out dateEnd))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ThrowError", "ThrowError('时间格式不正确')", true);
                return;
            }
            if (dateSart > dateEnd)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ThrowError", "ThrowError('时间格式不正确')", true);
                return;
            }

            string savePath = Server.MapPath(@"~/Generate/" + "财务婚宴订单明细报表[" + dateSart.ToString("yy.MM") + "-" + dateEnd.ToString("yy.MM") + "]" + DateTime.Now.ToString("yyyyMMddhhmmfff") + ".xlsx"); ;
            FileInfo saveFile = new FileInfo(savePath);
            //string filename = (new FileInfo(savePath)).Name;
            FileInfo templateReport = new FileInfo(Server.MapPath(@"Template-财务婚宴订单明细报表.xlsx"));


            using (ExcelPackage excel = new ExcelPackage(new FileInfo(savePath), templateReport))
            {
                ExcelWorksheet sheet = excel.Workbook.Worksheets[1];
                using (ebsDBData db = new ebsDBData())
                {
                    if (!db.WeddingOrders.Any(q => q.HetongDate >= dateSart && q.HetongDate <= dateEnd))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ThrowError", "ThrowError('日期范围内无订单数据')", true);
                        return;
                    }

                    var res = db.WeddingOrders.Where(q => q.HetongDate >= dateSart && q.HetongDate <= dateEnd);
                    int k = 3;


                    foreach (var item in res)
                    {
                        Customers cus = db.Customers.First(q => q.ID == item.MainID);
                        ConvertStringsInDB.JineByID jinePay = new ConvertStringsInDB.JineByID(item.ID);
                        sheet.Cells[k, (int)sf.ZT].Value = item.Zhuangtai;
                        sheet.Cells[k, (int)sf.hetongID].Value = item.HetongID;
                        sheet.Cells[k, (int)sf.YanhuiRiqi].Value = jinePay.HunliRiqi;
                        sheet.Cells[k, (int)sf.Yanhuiting].Value = item.Yanhuiting;
                        sheet.Cells[k, (int)sf.XinlangXinning].Value = item.XinlangName + "/" + item.XinNiangName;
                        sheet.Cells[k, (int)sf.Zhuoshu].Value = item.CaijinZhuoshu;
                        sheet.Cells[k, (int)sf.CaipingDanjia].Value = item.CaijinDanjia;
                        sheet.Cells[k, (int)sf.JiushuiDanjia].Value = item.JiushuiDanjia;
                        sheet.Cells[k, (int)sf.Zhekoujine].Value = Convert.ToDouble(item.CaijinZhekou == "" ? "0" : item.CaijinZhekou) + Convert.ToDouble(item.JiushuiZhekou == "" ? "0" : item.JiushuiZhekou);
                        sheet.Cells[k, (int)sf.Hunyanhetongkuan].Value = jinePay.HunyanZongjine;
                        sheet.Cells[k, (int)sf.HetongPingjunDanjia].Value = (Convert.ToDouble(jinePay.HunyanZongjine) / Convert.ToDouble(item.CaijinZhuoshu));

                        double exZhuoshu = 0;
                        if (db.WeddingExtraPayContent.Any(q => q.OrderID == item.ID))
                        {
                            var ex = db.WeddingExtraPayContent.First(q => q.OrderID == item.ID);
                            sheet.Cells[k, (int)sf.AddedZhuoshu].Value = ex.CaijinZhuoshu;
                            sheet.Cells[k, (int)sf.AddedCaiping].Value = ex.Caijin;
                            sheet.Cells[k, (int)sf.AddedJiushui].Value = ex.Jiushui;
                            exZhuoshu = ex.CaijinZhuoshu;

                        }
                        sheet.Cells[k, (int)sf.AddedZongKuan].Value = jinePay.HunyanZongjinex;
                        sheet.Cells[k, (int)sf.HunyanJiesuan].Value = Convert.ToDouble(jinePay.HunyanZongjine) + Convert.ToDouble(jinePay.HunyanZongjinex);
                        sheet.Cells[k, (int)sf.JiesuanPingjun].Value = (Convert.ToDouble(jinePay.HunyanZongjine) + Convert.ToDouble(jinePay.HunqinZongjinex)) / (Convert.ToInt32(item.CaijinZhuoshu) + exZhuoshu);

                        ConvertStringsInDB.paymentByIDandType firstPay = new ConvertStringsInDB.paymentByIDandType(item.ID, "1");
                        sheet.Cells[k, (int)sf.HYFirstFuYushou].Value = firstPay.HunyanNeedPay;
                        sheet.Cells[k, (int)sf.HYFirstFuBili].Value = firstPay.HunyanBai;
                        sheet.Cells[k, (int)sf.HYFirstFuRiqi].Value = item.DingjinDate.ToString("yyyy-MM-dd");
                        sheet.Cells[k, (int)sf.HYFirstFuJine].Value = firstPay.YiFuHYbyPay;
                        sheet.Cells[k, (int)sf.HYFirstFuShoukuanlv].Value = Convert.ToDouble(firstPay.YiFuHYbyPay) / Convert.ToDouble(jinePay.HunyanZongjine);
                        sheet.Cells[k, (int)sf.HQFirstFuYushou].Value = firstPay.HunqinNeedPay;
                        sheet.Cells[k, (int)sf.HQFirstFuBili].Value = firstPay.HunqinBai;
                        sheet.Cells[k, (int)sf.HQFirstFuRiqi].Value = item.DingjinDate.ToString("yyyy-MM-dd");
                        sheet.Cells[k, (int)sf.HQFirstFuJine].Value = firstPay.YiFuHQbyPay;
                        sheet.Cells[k, (int)sf.HQFirstFuShoukuanlv].Value = Convert.ToDouble(firstPay.YiFuHQbyPay) / Convert.ToDouble(jinePay.HunqinZongjine);

                        ConvertStringsInDB.paymentByIDandType SecondPay = new ConvertStringsInDB.paymentByIDandType(item.ID, "2");
                        sheet.Cells[k, (int)sf.HYSecondFuYushou].Value = SecondPay.HunyanNeedPay;
                        sheet.Cells[k, (int)sf.HYSecondFuBili].Value = SecondPay.HunyanBai;
                        sheet.Cells[k, (int)sf.HYSecondFuRiqi].Value = item.ZhongkuanDate.ToString("yyyy-MM-dd");
                        sheet.Cells[k, (int)sf.HYSecondFuJine].Value = SecondPay.YiFuHYbyPay;
                        sheet.Cells[k, (int)sf.HYSecondFuShoukuanlv].Value = Convert.ToDouble(SecondPay.YiFuHYbyPay) / Convert.ToDouble(jinePay.HunyanZongjine);
                        sheet.Cells[k, (int)sf.HQSecondFuYushou].Value = SecondPay.HunqinNeedPay;
                        sheet.Cells[k, (int)sf.HQSecondFuBili].Value = SecondPay.HunqinBai;
                        sheet.Cells[k, (int)sf.HQSecondFuRiqi].Value = item.ZhongkuanDate.ToString("yyyy-MM-dd");
                        sheet.Cells[k, (int)sf.HQSecondFuJine].Value = SecondPay.YiFuHQbyPay;
                        sheet.Cells[k, (int)sf.HQSecondFuShoukuanlv].Value = Convert.ToDouble(SecondPay.YiFuHQbyPay) / Convert.ToDouble(jinePay.HunqinZongjine);

                        ConvertStringsInDB.paymentByIDandType ThirdPay = new ConvertStringsInDB.paymentByIDandType(item.ID, "3");
                        sheet.Cells[k, (int)sf.HYThirdFuYushou].Value = ThirdPay.HunyanNeedPay;
                        sheet.Cells[k, (int)sf.HYThirdFuBili].Value = ThirdPay.HunyanBai;
                        sheet.Cells[k, (int)sf.HYThirdFuRiqi].Value = item.WeikuanDate.ToString("yyyy-MM-dd");
                        sheet.Cells[k, (int)sf.HYThirdFuJine].Value = ThirdPay.YiFuHYbyPay;
                        sheet.Cells[k, (int)sf.HYThirdFuShoukuanlv].Value = Convert.ToDouble(ThirdPay.YiFuHYbyPay) / Convert.ToDouble(jinePay.HunyanZongjine);
                        sheet.Cells[k, (int)sf.HQThirdFuYushou].Value = ThirdPay.HunqinNeedPay;
                        sheet.Cells[k, (int)sf.HQThirdFuBili].Value = ThirdPay.HunqinBai;
                        sheet.Cells[k, (int)sf.HQThirdFuRiqi].Value = item.WeikuanDate.ToString("yyyy-MM-dd");
                        sheet.Cells[k, (int)sf.HQThirdFuJine].Value = ThirdPay.YiFuHQbyPay;
                        sheet.Cells[k, (int)sf.HQThirdFuShoukuanlv].Value = Convert.ToDouble(ThirdPay.YiFuHQbyPay) / Convert.ToDouble(jinePay.HunqinZongjine);

                        ConvertStringsInDB.paymentByIDandType exPay = new ConvertStringsInDB.paymentByIDandType(item.ID, "e");
                        sheet.Cells[k, (int)sf.HYAddedFuYushou].Value = exPay.HunyanNeedPay;
                        sheet.Cells[k, (int)sf.HYAddedFuJine].Value = exPay.YiFuHYbyPay;
                        sheet.Cells[k, (int)sf.HQAddedFuYushou].Value = exPay.HunqinNeedPay;
                        sheet.Cells[k, (int)sf.HQAddedFuJine].Value = exPay.YiFuHQbyPay;


                        sheet.Cells[k, (int)sf.HunqinhetongKuan].Value = jinePay.HunqinZongjine;
                        sheet.Cells[k, (int)sf.HunqinAddedKuan].Value = jinePay.HunqinZongjinex;
                        double zongHunqin = Convert.ToDouble(jinePay.HunqinZongjine) + Convert.ToDouble(jinePay.HunqinZongjinex);
                        sheet.Cells[k, (int)sf.HunqinJiesuan].Value = zongHunqin.ToString();

                        sheet.Cells[k, (int)sf.Zongjine].Value = Convert.ToDouble(jinePay.Zongjine) + Convert.ToDouble(jinePay.Zongjinex);
                        sheet.Cells[k, (int)sf.Laiyuan].Value = cus.Source;
                        if (db.tbSysStandardWeddingDateLevel.Any(q => q.Date == Convert.ToDateTime(cus.EventDate)))
                        {
                            sheet.Cells[k, (int)sf.Dangqi].Value = db.tbSysStandardWeddingDateLevel.First(q => q.Date == Convert.ToDateTime(cus.EventDate)).DateLevel;
                        }

                        sheet.Cells[k, (int)sf.Sales].Value = ConvertStringsInDB.ToDisplayName(item.Sales);

                        sheet.Cells[k, (int)sf.HYFuwuxiang].Value = item.HunYanServices;
                        sheet.Cells[k, (int)sf.HQFuwuxiang].Value = item.HunQinServices;
                        sheet.Cells[k, (int)sf.Beizhu].Value = item.BuchongXinxi;

                        k++;

                    }
                }

                excel.Save();
            }

            //FileInfo file = new FileInfo(fileURL);
            //Response.ContentType = "application/x-zip-compressed";
            Response.ContentType = "application/octet-stream";
            //Response.Charset = "UTF-8";
            //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.UrlEncode(saveFile.Name) + "\"");
            Response.TransmitFile(savePath);
            Response.End();

        }

        public enum sf
        {
            ZT = 1,
            hetongID = 2,
            Yanhuiting = 3,
            YanhuiRiqi = 4,
            XinlangXinning = 5,
            Zhuoshu = 7,
            CaipingDanjia = 8,
            JiushuiDanjia = 9,
            Zhekoujine = 10,
            ZhekouZhuming = 11,
            Hunyanhetongkuan = 12,
            HetongPingjunDanjia = 13,
            AddedZhuoshu = 15,
            AddedCaiping = 16,
            AddedJiushui = 17,
            AddedZongKuan = 18,
            HunyanJiesuan = 19,
            JiesuanPingjun = 20,

            HYFirstFuYushou = 22,
            HYFirstFuBili = 23,
            HYFirstFuRiqi = 24,
            HYFirstFuJine = 25,
            HYFirstShouju = 26,
            HYFirstFuShoukuanlv = 27,

            HYSecondFuYushou = 29,
            HYSecondFuBili = 30,
            HYSecondFuRiqi = 31,
            HYSecondFuJine = 32,
            HYSecondShouju = 33,
            HYSecondFuShoukuanlv = 34,

            HYThirdFuYushou = 36,
            HYThirdFuBili = 37,
            HYThirdFuRiqi = 38,
            HYThirdFuJine = 39,
            HYThirdShouju = 40,
            HYThirdFuShoukuanlv = 41,

            HYAddedFuYushou = 43,
            HYAddedFuXiangmu = 44,
            HYAddedFuRiqi = 45,
            HYAddedFuJine = 46,
            HYAddedShouju = 47,
            HYAddedFuBeizhu = 48,

            HunqinhetongID = 50,
            HunqinhetongKuan = 51,
            HunqinAddedKuan = 52,
            HunqinJiesuan = 53,
            Hunqinjiesuan20 = 54,

            HQFirstFuYushou = 55,
            HQFirstFuBili = 56,
            HQFirstFuRiqi = 57,
            HQFirstFuJine = 58,
            HQFirstFuShouju = 59,
            HQFirstFuShoukuanlv = 60,

            HQSecondFuYushou = 61,
            HQSecondFuBili = 62,
            HQSecondFuRiqi = 63,
            HQSecondFuJine = 64,
            HQSecondFuShouju = 65,
            HQSecondFuShoukuanlv = 66,

            HQThirdFuYushou = 67,
            HQThirdFuBili = 68,
            HQThirdFuRiqi = 69,
            HQThirdFuJine = 70,
            HQThirdFuShouju = 71,
            HQThirdFuShoukuanlv = 72,

            HQAddedFuYushou = 73,
            HQAddedFuXiangmu = 74,
            HQAddedFuRiqi = 75,
            HQAddedFuJine = 76,
            HQAddedFuShouju = 77,
            HQAddedFuBeizhu = 78,

            Zongjine = 80,
            Laiyuan = 81,
            Dangqi = 82,
            Sales = 83,
            HYFuwuxiang = 84,
            HQFuwuxiang = 85,
            Beizhu = 86
        }

        protected void lbYDSWeddingMonthly_Click(object sender, EventArgs e)
        {

            DateTime dateSart, dateEnd;
            if (!DateTime.TryParse(tbStartMonth.Text, out dateSart) || !DateTime.TryParse(tbEndMonth.Text, out dateEnd))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ThrowError", "ThrowError('时间格式不正确')", true);
                return;
            }
            if (dateSart > dateEnd)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ThrowError", "ThrowError('时间格式不正确')", true);
                return;
            }


            string savePath = Server.MapPath(@"~/Generate/" + "一滴水当月完成情况汇总表 " + DateTime.Now.ToString("yyyyMMddhhmmfff") + ".xlsx"); ;
            FileInfo saveFile = new FileInfo(savePath);
            //string filename = (new FileInfo(savePath)).Name;
            FileInfo templateReport = new FileInfo(Server.MapPath(@"Template-一滴水当月完成情况汇总表.xlsx"));


            using (ExcelPackage excel = new ExcelPackage(new FileInfo(savePath), templateReport))
            {


                ExcelWorksheet sheet = excel.Workbook.Worksheets[1];
                using (ebsDBData db = new ebsDBData())
                {
                    int i = 5;

                    int MonthNb = (dateEnd.Year - dateSart.Year) * 12 + (dateEnd.Month) - dateSart.Month;

                    for (int m = 1; m <= MonthNb; m++)
                    {
                        List<WeddingOrders> wos = db.WeddingOrders.Where(q => q.HetongDate.Month == dateSart.AddMonths(m).Month && q.HetongDate.Year == dateSart.AddMonths(m).Year).ToList();
                        int Dingdanshu = wos.Count;
                        Double Dingdanzhuoshu = 0;
                        Double HetongWanchengjine = 0;
                        Double JiesuanJine = 0;
                        Double ShijiPingjun = 0;

                        Double HetongHQ = 0;
                        Double HetongHQ21 = 0;
                        Double JiesuanHQ = 0;
                        Double PinjunHQ = 0;
                        foreach (var item in wos)
                        {

                            HetongWanchengjine += NewConvert(item.CaijinZhuoshu) * item.CaijinDanjia + NewConvert(item.JiushuiDanjia) * NewConvert(item.JiushuiZhekou) - NewConvert(item.CaijinZhekou) - NewConvert(item.JiushuiZhekou);
                            Dingdanzhuoshu += NewConvert(item.CaijinZhuoshu);
                            HetongHQ += NewConvert(item.Hunqin) + NewConvert(item.Zhuohua) + NewConvert(item.Qita);
                            // HetongWanchengjine += item.
                            if (db.WeddingExtraPayContent.Count(q => q.OrderID == item.ID) > 0)
                            {
                                WeddingExtraPayContent ex = db.WeddingExtraPayContent.First(q => q.OrderID == item.ID);
                                Dingdanzhuoshu += ex.CaijinZhuoshu;
                                HetongWanchengjine += ex.CaijinZhuoshu * ex.Caijin + ex.Jiushui * ex.JiushuiZhuoshu - ex.WeikuanDikou;
                                HetongHQ += ex.EXzhuohua + ex.EXqita;
                            }

                            if (db.WeddingPayment.Count(q => q.OrderID == item.ID) > 0)
                            {
                                List<WeddingPayment> wp = db.WeddingPayment.Where(q => q.OrderID == item.ID).ToList();
                                var shouru = wp.Where(q => q.Zhuangtai == "审批完成" && (q.PayType == "1" || q.PayType == "2" || q.PayType == "3" || q.PayType == "e")).Sum(q => q.ShishouHY);
                                var tuikuan = wp.Where(q => (q.PayType == "pk" || q.PayType == "tk") && q.Zhuangtai == "审批完成").Sum(q => q.ShishouHY);

                                var shouruHQ = wp.Where(q => q.Zhuangtai == "审批完成" && (q.PayType == "1" || q.PayType == "2" || q.PayType == "3" || q.PayType == "e")).Sum(q => q.ShishouHQ);
                                var tuikuanHQ = wp.Where(q => (q.PayType == "pk" || q.PayType == "tk") && q.Zhuangtai == "审批完成").Sum(q => q.ShishouHQ);

                                JiesuanJine += shouru - tuikuan;
                                JiesuanHQ += shouruHQ - tuikuanHQ;
                            }

                            ShijiPingjun = JiesuanJine / Dingdanzhuoshu;
                            HetongHQ21 = HetongHQ * 0.21;
                            PinjunHQ = JiesuanHQ / Dingdanzhuoshu;

                        }
                        if (dateSart.AddMonths(m) > DateTime.Now)
                        {
                            break;
                        }
                        DateTime cur = dateSart.AddMonths(m);
                        sheet.Cells[i, 1].Value = cur.ToString("yyyy/MM");
                        sheet.Cells[i, 2].Value = Dingdanshu;
                        sheet.Cells[i, 3].Value = Dingdanzhuoshu;
                        sheet.Cells[i, 4].Value = HetongWanchengjine;
                        sheet.Cells[i, 5].Value = JiesuanJine;
                        sheet.Cells[i, 6].Value = ShijiPingjun;
                        sheet.Cells[i, 7].Value = HetongHQ;
                        sheet.Cells[i, 8].Value = HetongHQ21;
                        sheet.Cells[i, 9].Value = JiesuanHQ;
                        sheet.Cells[i, 10].Value = PinjunHQ;
                        i++;
                    }



                }


                excel.Save();
            }
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.UrlEncode(saveFile.Name) + "\"");
            Response.TransmitFile(savePath);
            Response.End();
        }

        public double NewConvert(string p)
        {
            if (p == null)
            {
                return 0;
            }
            if (p == "")
            {
                return 0;
            }
            return Convert.ToDouble(p);
        }

        protected void lbYearReport_Click(object sender, EventArgs e)
        {

            int yearSelect;
            if (!int.TryParse(tbYear.Text, out yearSelect))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ThrowError", "ThrowError('年份格式不正确')", true);
                return;
            }
            string savePath = Server.MapPath(@"~/Generate/" + "一滴水婚宴预收情况表" + DateTime.Now.ToString("yyyyMMddhhmmfff") + ".xlsx"); ;
            FileInfo saveFile = new FileInfo(savePath);
            //string filename = (new FileInfo(savePath)).Name;
            FileInfo templateReport = new FileInfo(Server.MapPath(@"Template-一滴水婚宴预收情况表.xlsx"));
            using (ExcelPackage excel = new ExcelPackage(new FileInfo(savePath), templateReport))
            {
                ExcelWorksheet sheet = excel.Workbook.Worksheets[1];
                int i = 4;
                using (ebsDBData db = new ebsDBData())
                {
                    for (int y = 0; y < 12; y++)
                    {
                        if (y > DateTime.Now.Month -1)
                        {
                            break;
                        }


                        List<WeddingOrders> wosAuditIng = db.WeddingOrders.Where(q => q.HetongDate.Year ==yearSelect && q.HetongDate.Month == (y+1) && q.Zhuangtai == "待审批").ToList();
                        List<WeddingOrders> wosAuditComplete = db.WeddingOrders.Where(q => q.HetongDate.Year == yearSelect && q.HetongDate.Month == (y + 1) && q.Zhuangtai == "审批完成").ToList();

                        int DingdanshuIng = wosAuditIng.Count;
                        int DingdanshuComplete = wosAuditComplete.Count;

                        Double zhuoshuIng = 0;
                        Double zhuoshuComplete = 0;

                        Double HetongWanchengjineIng = 0;
                        Double HetongWanchengjineComplete = 0;

                        Double HetongHQIng = 0;
                        Double HetongHQComplete = 0;

                        Double PerZhuoshuIng = 0;
                        Double PerZhuoshuComplete = 0;

                        Double HYAudited = 0;
                        Double HYUnaudited = 0;

                        Double HQAudited = 0;
                        Double HQUnaudited = 0;


                        foreach (var item in wosAuditIng)
                        {
                            HetongWanchengjineIng += NewConvert(item.CaijinZhuoshu) * item.CaijinDanjia + NewConvert(item.JiushuiDanjia) * NewConvert(item.JiushuiZhekou) - NewConvert(item.CaijinZhekou) - NewConvert(item.JiushuiZhekou);
                            HetongHQIng += NewConvert(item.Hunqin) + NewConvert(item.Zhuohua) + NewConvert(item.Qita);
                            zhuoshuIng += NewConvert(item.CaijinZhuoshu);
                            if (db.WeddingExtraPayContent.Count(q => q.OrderID == item.ID) > 0)
                            {
                                WeddingExtraPayContent ex = db.WeddingExtraPayContent.First(q => q.OrderID == item.ID);
                                HetongWanchengjineIng += ex.CaijinZhuoshu * ex.Caijin + ex.Jiushui * ex.JiushuiZhuoshu - ex.WeikuanDikou;
                                HetongHQIng += ex.EXzhuohua + ex.EXqita;
                                zhuoshuIng += ex.CaijinZhuoshu;
                            }
                            PerZhuoshuIng = HetongWanchengjineIng / zhuoshuIng;

                            if (db.WeddingPayment.Count(q => q.OrderID == item.ID) > 0)
                            {
                                List<WeddingPayment> wp = db.WeddingPayment.Where(q => q.OrderID == item.ID).ToList();
                                var shouru = wp.Where(q => q.Zhuangtai == "审批完成" && (q.PayType == "1" || q.PayType == "2" || q.PayType == "3" || q.PayType == "e")).Sum(q => q.ShishouHY);
                                var tuikuan = wp.Where(q => (q.PayType == "pk" || q.PayType == "tk") && q.Zhuangtai == "审批完成").Sum(q => q.ShishouHY);

                                var shouruUn = wp.Where(q => q.Zhuangtai == "审批中" && (q.PayType == "1" || q.PayType == "2" || q.PayType == "3" || q.PayType == "e")).Sum(q => q.ShishouHY);
                                var tuikuanUn = wp.Where(q => (q.PayType == "pk" || q.PayType == "tk") && q.Zhuangtai == "审批中").Sum(q => q.ShishouHY);

                                var shouruHQ = wp.Where(q => q.Zhuangtai == "审批完成" && (q.PayType == "1" || q.PayType == "2" || q.PayType == "3" || q.PayType == "e")).Sum(q => q.ShishouHQ);
                                var tuikuanHQ = wp.Where(q => (q.PayType == "pk" || q.PayType == "tk") && q.Zhuangtai == "审批完成").Sum(q => q.ShishouHQ);

                                var shouruHQUn = wp.Where(q => q.Zhuangtai == "审批中" && (q.PayType == "1" || q.PayType == "2" || q.PayType == "3" || q.PayType == "e")).Sum(q => q.ShishouHQ);
                                var tuikuanHQUn = wp.Where(q => (q.PayType == "pk" || q.PayType == "tk") && q.Zhuangtai == "审批中").Sum(q => q.ShishouHQ);

                                HYAudited += shouru - tuikuan;
                                HYUnaudited += shouruUn - tuikuanUn;

                                HQAudited += shouruHQ - tuikuanHQ;
                                HQUnaudited += shouruHQUn - tuikuanHQUn;
                            }
                        }

                        foreach (var item in wosAuditComplete)
                        {
                            HetongWanchengjineComplete += NewConvert(item.CaijinZhuoshu) * item.CaijinDanjia + NewConvert(item.JiushuiDanjia) * NewConvert(item.JiushuiZhekou) - NewConvert(item.CaijinZhekou) - NewConvert(item.JiushuiZhekou);
                            HetongHQComplete += NewConvert(item.Hunqin) + NewConvert(item.Zhuohua) + NewConvert(item.Qita);
                            zhuoshuComplete += NewConvert(item.CaijinZhuoshu);
                            if (db.WeddingExtraPayContent.Count(q => q.OrderID == item.ID) > 0)
                            {
                                WeddingExtraPayContent ex = db.WeddingExtraPayContent.First(q => q.OrderID == item.ID);
                                HetongWanchengjineComplete += ex.CaijinZhuoshu * ex.Caijin + ex.Jiushui * ex.JiushuiZhuoshu - ex.WeikuanDikou;
                                HetongHQComplete += ex.EXzhuohua + ex.EXqita;
                                zhuoshuComplete += ex.CaijinZhuoshu;
                            }
                            PerZhuoshuComplete = HetongWanchengjineComplete / zhuoshuComplete;

                            if (db.WeddingPayment.Count(q => q.OrderID == item.ID) > 0)
                            {
                                List<WeddingPayment> wp = db.WeddingPayment.Where(q => q.OrderID == item.ID).ToList();
                                var shouru = wp.Where(q => q.Zhuangtai == "审批完成" && (q.PayType == "1" || q.PayType == "2" || q.PayType == "3" || q.PayType == "e")).Sum(q => q.ShishouHY);
                                var tuikuan = wp.Where(q => (q.PayType == "pk" || q.PayType == "tk") && q.Zhuangtai == "审批完成").Sum(q => q.ShishouHY);

                                var shouruUn = wp.Where(q => q.Zhuangtai == "审批中" && (q.PayType == "1" || q.PayType == "2" || q.PayType == "3" || q.PayType == "e")).Sum(q => q.ShishouHY);
                                var tuikuanUn = wp.Where(q => (q.PayType == "pk" || q.PayType == "tk") && q.Zhuangtai == "审批中").Sum(q => q.ShishouHY);

                                var shouruHQ = wp.Where(q => q.Zhuangtai == "审批完成" && (q.PayType == "1" || q.PayType == "2" || q.PayType == "3" || q.PayType == "e")).Sum(q => q.ShishouHQ);
                                var tuikuanHQ = wp.Where(q => (q.PayType == "pk" || q.PayType == "tk") && q.Zhuangtai == "审批完成").Sum(q => q.ShishouHQ);

                                var shouruHQUn = wp.Where(q => q.Zhuangtai == "审批中" && (q.PayType == "1" || q.PayType == "2" || q.PayType == "3" || q.PayType == "e")).Sum(q => q.ShishouHQ);
                                var tuikuanHQUn = wp.Where(q => (q.PayType == "pk" || q.PayType == "tk") && q.Zhuangtai == "审批中").Sum(q => q.ShishouHQ);

                                HYAudited += shouru - tuikuan;
                                HYUnaudited += shouruUn - tuikuanUn;

                                HQAudited += shouruHQ - tuikuanHQ;
                                HQUnaudited += shouruHQUn - tuikuanHQUn;
                            }
                        }
                       
                        sheet.Cells["B1"].Value = DateTime.Now.ToString("yyyy/MM/dd");
                        sheet.Cells["A2"].Value = tbYear.Text;
                        sheet.Cells[i, 3].Value = HetongWanchengjineComplete;
                        sheet.Cells[i, 4].Value = HetongHQComplete;
                        sheet.Cells[i, 5].Value = DingdanshuComplete;
                        sheet.Cells[i, 6].Value = zhuoshuComplete;

                        sheet.Cells[i + 1, 3].Value = HetongWanchengjineIng;
                        sheet.Cells[i + 1, 4].Value = HetongHQIng;
                        sheet.Cells[i + 1, 5].Value = DingdanshuIng;
                        sheet.Cells[i + 1, 6].Value = zhuoshuIng;

                        sheet.Cells[i + 2, 3].Value = HetongWanchengjineIng + HetongWanchengjineComplete;
                        sheet.Cells[i + 2, 4].Value = HetongHQIng + HetongHQComplete;
                        sheet.Cells[i + 2, 5].Value = DingdanshuIng + DingdanshuComplete;
                        sheet.Cells[i + 2, 6].Value = zhuoshuIng + zhuoshuComplete;

                        sheet.Cells[i, 7].Value = HYAudited;
                        sheet.Cells[i, 8].Value = HYUnaudited;
                        sheet.Cells[i, 9].Value = HQAudited;
                        sheet.Cells[i, 10].Value = HQUnaudited;
                        sheet.Cells[i, 11].Value = HetongWanchengjineIng + HetongWanchengjineComplete + HetongHQIng + HetongHQComplete;
                        i = i + 3;
                    }
                }

                excel.Save();
            }

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.UrlEncode(saveFile.Name) + "\"");
            Response.TransmitFile(savePath);
            Response.End();
        }
    }
}