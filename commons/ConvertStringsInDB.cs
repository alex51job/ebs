using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ebs.dbml;

namespace ebs.commons
{
    public static class ConvertStringsInDB
    {
        public static string ToDisplayName(this string UserName)
        {
            string DisplayName = "";
            using (dbml.ebsDBData db = new dbml.ebsDBData())
            {
                if (db.tbUsers.Any(q => q.UserName == UserName))
                {
                    DisplayName = db.tbUsers.First(q => q.UserName == UserName).DisplayName;
                }

            }
            return DisplayName;
        }


        public class JineByID
        {
            public string HunliRiqi { get; set; }
            public string RoleToAudit { get; set; }
            public string ZhuangtaiAudit { get; set; }
            public string HunyanZongjine { get; set; }
            public string HunqinZongjine { get; set; }
            public string Zongjine { get; set; }

            public string HunyanZongjinex { get; set; }
            public string HunqinZongjinex { get; set; }
            public string Zongjinex { get; set; }

            public string Yifujine { get; set; }
            public string YifujineAuditFinish { get; set; }
            public JineByID(int WeddingOrderID)
            {
                try
                {
                    using (ebsDBData db = new ebsDBData())
                    {
                        if (db.WeddingOrders.Any(q => q.ID == WeddingOrderID))
                        {

                            WeddingOrders wo = db.WeddingOrders.FirstOrDefault(q => q.ID == WeddingOrderID);
                            if (wo.HunliDate !=null)
                            {
                                HunliRiqi = wo.HunliDate.Value.ToString("yyyy-MM-dd");
                            }
                            else HunliRiqi = db.Customers.First(q => q.ID == wo.MainID).EventDate;
                            HunyanZongjine = wo.HunyanZongJine;
                            Double hunqin = Convert.ToDouble(wo.Hunqin) + Convert.ToDouble(wo.Zhuohua == "" ? "0" : wo.Zhuohua) + Convert.ToDouble(wo.Qita == "" ? "0" : wo.Qita);
                            HunqinZongjine = hunqin.ToString("0.0");
                            Zongjine = (Convert.ToDouble(HunyanZongjine) + hunqin).ToString("0.0");
                            ZhuangtaiAudit = wo.Zhuangtai;
                            RoleToAudit = "-";
                            if (ZhuangtaiAudit == "待审批")
                            {
                                RoleToAudit = wo.AuditRecords.Where(q => q.AuditDate == null).OrderBy(q => q.AuditPriority).First().AuditLevel;
                                ZhuangtaiAudit = "待审批(" + RoleToAudit + ")";
                            }
                            HunyanZongjinex = "0";
                            HunqinZongjinex = "0";
                            if (wo.WeddingExtraPayContent.Count > 0)
                            {
                                var wep = wo.WeddingExtraPayContent.First();
                                double hq = wep.Caijin * wep.CaijinZhuoshu + wep.Jiushui * wep.JiushuiZhuoshu - wep.WeikuanDikou;
                                double hy = wep.EXzhuohua + wep.EXqita;
                                HunyanZongjinex = hq.ToString("0.0");
                                HunqinZongjinex = hy.ToString("0.0");
                                Zongjinex = (hq + hy).ToString("0.0");
                            }
                            Yifujine = "0";
                            YifujineAuditFinish = "0";
                            if (wo.WeddingPayment.Count > 0)
                            {
                                string[] stas = { "1", "2", "3" };
                                var r = wo.WeddingPayment.Where(q => stas.Contains(q.PayType)).Sum(q => q.ShishouHQ + q.ShishouHY);
                                Yifujine = r.ToString("0.0");
                                var t = wo.WeddingPayment.Where(q => stas.Contains(q.PayType) && q.Zhuangtai=="审批完成").Sum(q => q.ShishouHQ + q.ShishouHY);
                                YifujineAuditFinish = t.ToString("0.0");
                            }

                        }
                    }
                }
                catch (System.Exception ex)
                {
                    
                }

            }
        }

        public class JineByID_Bussiness
        {
            public double hetongZongjine { get; set; }
            public double Zongjine { get; set; }
            public double Yifujine { get; set; }
            public double yifujineFinish { get; set; }
            public double FanyongPer { get; set; }
            public List<string> EventFormat { get; set; }
            public List<string> EventVenue { get; set; }

            public string RoleToAudit { get; set; }
            public string ZhuangtaiAudit { get; set; }

            public JineByID_Bussiness(int BussinessOrderID)
            {
                hetongZongjine = 0;
                Zongjine = 0;
                Yifujine = 0;
                yifujineFinish = 0;
                FanyongPer = 0;
                EventFormat = new List<string>();
                EventVenue =  new List<string>();

                using (ebsDBData db = new ebsDBData())
                {
                    if (db.Bussiness.Count(q=>q.ID == BussinessOrderID) == 0)
                    {
                        return;
                    }
                    Bussiness BO = db.Bussiness.First(q => q.ID == BussinessOrderID);
                    List<BussinessEventFormat> BEvents = BO.BussinessEventFormat.ToList();

                    ZhuangtaiAudit = BO.Zhuangtai;
                    RoleToAudit = "-";
                    if (ZhuangtaiAudit == "待审批")
                    {
                        RoleToAudit = BO.BussinessAuditRecords.Where(q => q.AuditDate == null).OrderBy(q => q.AuditPriority).First().AuditLevel;
                        ZhuangtaiAudit = "待审批(" + RoleToAudit + ")";
                    }

                    double TotalPayment = BO.Dabaodanjia * BO.Dabaodanjia_ren + (BO.Otherfee1_Amount??0) + (BO.Otherfee2_Amount??0) + (BO.Otherfee3_Amount??0) - BO.Zhekou - BO.Fanyong;
                    double TotalEvents = 0;
                    foreach (var item in BEvents)
                    {
                        if (item.EventFormat == "会议")
                        {
                            TotalEvents += (item.Dajianfei??0) + (item.Changdifei??0);
                            if (!EventFormat.Contains("会议"))
                            {
                                EventFormat.Add("会议");
                            }
                           
                        }
                        if (item.EventFormat == "用餐")
                        {
                            TotalEvents += (item.Canbiao??0) * (item.ShuiliangA??0) + (item.Jiushui??0) * (item.ShuiliangB??0);
                            if (!EventFormat.Contains("用餐"))
                            {
                                EventFormat.Add("用餐");
                            }
                        }
                        if (item.Venue!=null && item.Venue !="")
                        {
                            string[] venues = item.Venue.Split(',');
                            foreach (var ve in venues)
                            {
                                if (!EventVenue.Contains(ve))
                                {
                                    EventVenue.Add(ve);
                                }
                            }
                        }
                       
                    }
                    hetongZongjine = TotalEvents + TotalPayment + BO.Fanyong;
                    Zongjine = TotalEvents + TotalPayment;
                    FanyongPer = Math.Round(BO.Fanyong / Zongjine, 2);
                    if (BO.BussinessPayment.Count > 0)
                    {
                        Yifujine = BO.BussinessPayment.Where(q => q.PayType == "转账" || q.PayType == "现金").Sum(q => q.PayAmount);
                        yifujineFinish = BO.BussinessPayment.Where(q =>q.Zhuangtai=="审批完成" && (q.PayType == "转账" || q.PayType == "现金")).Sum(q => q.PayAmount);
                    }

                    
                }
            }


        }

        public class paymentByIDandType
        {
            //public string Hunyanzongjine { get; set; }
            //public string Hunqinzongjine { get; set; }
            public string HunqinNeedPay { get; set; }
            public string HunyanNeedPay { get; set; }
            public string HunqinBai { get; set; }
            public string HunyanBai { get; set; }
            
            public string YiFuHQbyPay { get; set; }
            public string YiFuHYbyPay { get; set; }
            public string YiFuHunqin { get; set; }
            public string YiFuHunyan { get; set; }

            
            
            public paymentByIDandType(int ID, string PayType)
            {
                using (ebsDBData db = new ebsDBData())
                {
                    if (db.WeddingOrders.Any(q => q.ID == ID))
                    {
                        WeddingOrders wo = db.WeddingOrders.First(q => q.ID == ID);

                        Double Hunyan = Convert.ToDouble(wo.HunyanZongJine);
                        Double Intval_Hunqin = wo.Hunqin==""?0: Convert.ToDouble(wo.Hunqin);
                        Double Zhuohua = wo.Zhuohua == "" ? 0 : Convert.ToDouble(wo.Zhuohua);
                        Double Qita = wo.Qita == "" ? 0 : Convert.ToDouble(wo.Qita);
                        Double Hunqin = Intval_Hunqin + Zhuohua + Qita;
                      
                        //Hunqinzongjine = Hunqin.ToString("0");
                        //Hunyanzongjine = Hunyan.ToString("0");
                        switch (PayType)
                        {
                            case "1":
                                HunqinNeedPay = (Hunqin * wo.DingjinPercentHunqin / 100).ToString("0.0");
                                HunqinBai = wo.DingjinPercentHunqin + "%";
                                HunyanNeedPay = (Hunyan * wo.DingjinPercentHunyan / 100).ToString("0.0");
                                HunyanBai = wo.DingjinPercentHunyan + "%";
                              
                                break;
                            case "2":
                                HunqinNeedPay = (Hunqin * wo.ZhongkuanPercentHunqin / 100).ToString("0.0");
                                HunqinBai = wo.ZhongkuanPercentHunqin + "%";
                                HunyanNeedPay = (Hunyan * wo.ZhongkuanPercentHunyan / 100).ToString("0.0");
                                HunyanBai = wo.ZhongkuanPercentHunyan + "%";
                                break;
                            case "3":
                                HunqinNeedPay = (Hunqin * wo.WeikuanPercentHunqin / 100).ToString("0.0");
                                HunqinBai = wo.WeikuanPercentHunqin + "%";
                                HunyanNeedPay = (Hunyan * wo.WeikuanPercentHunyan / 100).ToString("0.0");
                                HunyanBai = wo.WeikuanPercentHunyan + "%";
                                break;
                            case "e":
                                JineByID wep = new JineByID(ID);
                                HunqinNeedPay = wep.HunqinZongjinex;
                                HunqinBai = "100%";
                                HunyanNeedPay = wep.HunyanZongjinex;
                                HunyanBai = "100%";
                                break;
                            default:
                                break;
                        }
                        YiFuHQbyPay = wo.WeddingPayment.Where(q=>q.PayType == PayType).Sum(q => q.ShishouHQ).ToString("0.0");
                        YiFuHYbyPay = wo.WeddingPayment.Where(q => q.PayType == PayType).Sum(q => q.ShishouHY).ToString("0.0");
                        YiFuHunqin = wo.WeddingPayment.Sum(q => q.ShishouHQ).ToString("0.0");
                        YiFuHunyan = wo.WeddingPayment.Sum(q => q.ShishouHY).ToString("0.0");
                    }
                }
            }
        }

        public class paymentLeftByIDandType_HL
        {
            public int OrderID { get; set; }
            public string ContractID { get; set; }

            public string Hunqi { get; set; }
            
            public DateTime YinshouRiqi { get; set; }
            public string PayType { get; set; }
            public string YingshouJine { get; set; }
            public paymentLeftByIDandType_HL(int orderID, string paymentType)
            {
                OrderID = orderID;
                paymentByIDandType pbt = new paymentByIDandType(orderID, paymentType);
                PayType = paymentType;
                using (ebsDBData db = new ebsDBData())
                {
                    var wo = db.WeddingOrders.FirstOrDefault(q => q.ID == orderID);
                    Hunqi = db.Customers.FirstOrDefault(q => q.ID == wo.MainID).EventDate;
                    ContractID = wo.HetongID;
                    switch (paymentType)
                    {
                        case "1":
                            PayType = "定金";
                            YinshouRiqi = wo.DingjinDate;
                            break;
                        case "2":
                            PayType = "中款";
                            YinshouRiqi = wo.ZhongkuanDate;

                            break;
                        case "3":
                            PayType = "尾款";
                            YinshouRiqi = wo.WeikuanDate;
                            break;
                        default:
                            break;
                    }
                    YingshouJine = (Convert.ToDouble(pbt.HunqinNeedPay) + Convert.ToDouble(pbt.HunyanNeedPay) - Convert.ToDouble(pbt.YiFuHQbyPay) - Convert.ToDouble(pbt.YiFuHYbyPay)).ToString();
                }


            }
        }
    }
}