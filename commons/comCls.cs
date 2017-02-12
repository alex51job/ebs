using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using ebs.dbml;

namespace ebs.commons
{
    public  class ComCls
    {
        public static string title = "Events Booking System";
        public static string CreateHead(LoginUser Info)
        {

           
            StringBuilder builder = new StringBuilder();
            string htmlBefore = @"<div class=""navbar-header""><button type=""button"" class=""navbar-toggle collapsed"" data-toggle=""collapse"" data-target="".navbar-collapse""><span class=""sr-only"">Toggle navigation</span> <span class=""icon-bar""></span><span class=""icon-bar""></span><span class=""icon-bar""></span></button><span class=""navbar-brand"">&nbsp;&nbsp;Event Booking System</span></div>";
            string htmlEnd = @"<div class=""navbar-collapse collapse"" style=""height: 1px;""><ul id=""main-menu"" class=""nav navbar-nav navbar-right""><li class=""dropdown hidden-xs""><a href=""#"" class=""dropdown-toggle"" data-toggle=""dropdown""><span class=""glyphicon glyphicon-user padding-right-small"" style=""position: relative; top: 3px;""></span>{0} <i class=""fa fa-caret-down""></i></a><ul class=""dropdown-menu""><li><a href=""#"">角色 : {1}</a></li><li class=""divider""></li><li class=""dropdown-header"">{2}</li><li class=""divider""></li><li><a tabindex=""-1"" href=http://" +  HttpContext.Current.Request.Url.Host +"/login.aspx" + ">Logout</a></li> </ul></li></ul></div>";
            builder.Append(htmlBefore);
            builder.AppendFormat(htmlEnd, Info.DisplayName, Info.roles,Info.mailAddress);
            return builder.ToString();
        }
        //public static string CreateStats(LoginUser Info)
        //{
        //    //return "";
        //    StringBuilder SB = new StringBuilder();
        //    string item =  @"<p class=""stat"" ><span class=""label label-info"">{0}</span> {1}</p>";
        //    if (Info.roles == "客服")
        //    {
        //        using (ebsDBData db = new ebsDBData())
        //        {
        //            int DaihuifangNumber = db.Customers.Count(q => q.NeedHuiFang == "待回访");
        //            SB.AppendFormat(item, DaihuifangNumber, "待回访");
        //        }
        //    }

        //    if (Info.roles == "客服主管")
        //    {
        //        using (ebsDBData db = new ebsDBData())
        //        {
        //            int DaihuifangNumber = db.Customers.Count(q => q.NeedHuiFang == "待回访");
        //            SB.AppendFormat(item, DaihuifangNumber, "待回访");
        //            int DaishenpiNumber = db.Customers.Count(q => q.AuditStatus == "待审批");
        //            SB.AppendFormat(item, DaishenpiNumber, "待审批");
        //        }
        //    }
        //    return SB.ToString();
        //}
        public static string CreateFoot()
        {
            string res = @"<hr><p class=""pull-right"">Designed by <a href=""mailTo:Alex.xu2@delphi.com"" target=""_blank"">Alex's Idea </a></p><p>© 2015 <a href=""#"" target=""_blank"">AI</a> All Right Reserved.</p>";
            return res;
        }
        public delegate string URLHandle(string Url);
        public static string CreateMenu(LoginUser Info, URLHandle ResolveUrl)
        {
            StringBuilder builder = new StringBuilder();
            List<MainMenu> lstMM = new List<MainMenu>();
            List<SubMenu> lstSM = new List<SubMenu>();
            MainMenu mm1;

            mm1 = new MainMenu { id = 1, img = "fa-briefcase", name = "总览", noChild = true, link = ResolveUrl("~/Default.aspx") };
            lstMM.Add(mm1);
            if (Info.roles == "客服")
            {
                mm1 = new MainMenu { id = 2, img = "fa-group", name = "客户数据管理", noChild = true, link = ResolveUrl("~/MoKuai_Kehu/KehuList.aspx") };
                lstMM.Add(mm1);
            }

            
            if (Info.roles == "客服主管")
            {
                mm1 = new MainMenu { id = 2, img = "fa-group", name = "客户数据管理", noChild = true, link = ResolveUrl("~/MoKuai_Kehu/KehuList.aspx") };
                lstMM.Add(mm1);
                mm1 = new MainMenu { id = 3, img = "fa-group", name = "客户审批申请", noChild = true, link = ResolveUrl("~/MoKuai_Kehu/KehuListAudit.aspx") };
                lstMM.Add(mm1);
            }

            if (Info.roles == "婚宴销售")
            {
                mm1 = new MainMenu { id = 2, img = "fa-group", name = "客户数据管理", noChild = true, link = ResolveUrl("~/MoKuai_Kehu/KehuList.aspx") };
                lstMM.Add(mm1);
                mm1 = new MainMenu { id = 4, img = "fa-glass", name = "婚宴订单管理", noChild = true, link = ResolveUrl("~/MoKuai_Hunli/HunliList.aspx") };
                lstMM.Add(mm1);
            }

            if (Info.roles == "婚宴销售主管")
            {
                mm1 = new MainMenu { id = 2, img = "fa-group", name = "客户数据管理", noChild = true, link = ResolveUrl("~/MoKuai_Kehu/KehuList.aspx") };
                lstMM.Add(mm1);
                mm1 = new MainMenu { id = 4, img = "fa-glass", name = "婚宴订单管理", noChild = true, link = ResolveUrl("~/MoKuai_Hunli/HunliList.aspx") };
                lstMM.Add(mm1);
            }

            if (Info.roles == "商务销售")
            {
                mm1 = new MainMenu { id = 2, img = "fa-group", name = "客户数据管理", noChild = true, link = ResolveUrl("~/MoKuai_Kehu/KehuList.aspx") };
                lstMM.Add(mm1);
                mm1 = new MainMenu { id = 4, img = "fa-glass", name = "商务订单管理", noChild = true, link = ResolveUrl("~/MoKuai_Shangwu/ShangwuList.aspx") };
                lstMM.Add(mm1);
            }

            if (Info.roles == "商务销售主管")
            {
                mm1 = new MainMenu { id = 2, img = "fa-group", name = "客户数据管理", noChild = true, link = ResolveUrl("~/MoKuai_Kehu/KehuList.aspx") };
                lstMM.Add(mm1);
                mm1 = new MainMenu { id = 4, img = "fa-glass", name = "商务订单管理", noChild = true, link = ResolveUrl("~/MoKuai_Shangwu/ShangwuList.aspx") };
                lstMM.Add(mm1);
            }

            if (Info.roles == "总经理")
            {
                mm1 = new MainMenu { id = 2, img = "fa-group", name = "客户数据管理", noChild = true, link = ResolveUrl("~/MoKuai_Kehu/KehuList.aspx") };
                lstMM.Add(mm1);
                mm1 = new MainMenu { id = 4, img = "fa-glass", name = "婚宴订单管理", noChild = true, link = ResolveUrl("~/MoKuai_Hunli/HunliList.aspx") };
                lstMM.Add(mm1);
                mm1 = new MainMenu { id = 4, img = "fa-glass", name = "商务订单管理", noChild = true, link = ResolveUrl("~/MoKuai_Shangwu/ShangwuList.aspx") };
                lstMM.Add(mm1);
            }

            if (Info.roles == "文员")
            {
                //mm1 = new MainMenu { id = 2, img = "fa-group", name = "客户数据管理", noChild = true, link = ResolveUrl("~/MoKuai_Kehu/KehuList.aspx") };
                //lstMM.Add(mm1);
                mm1 = new MainMenu { id = 4, img = "fa-glass", name = "婚宴订单管理", noChild = true, link = ResolveUrl("~/MoKuai_Hunli/HunliList.aspx") };
                lstMM.Add(mm1);
                mm1 = new MainMenu { id = 9, img = "fa-glass", name = "商务订单管理", noChild = true, link = ResolveUrl("~/MoKuai_Shangwu/ShangwuList.aspx") };
                lstMM.Add(mm1);
            }
            if (Info.roles == "监督者")
            {
                mm1 = new MainMenu { id = 2, img = "fa-group", name = "客户数据管理", noChild = true, link = ResolveUrl("~/MoKuai_Kehu/KehuList.aspx") };
                lstMM.Add(mm1);
                mm1 = new MainMenu { id = 4, img = "fa-glass", name = "婚宴订单管理", noChild = true, link = ResolveUrl("~/MoKuai_Hunli/HunliList.aspx") };
                lstMM.Add(mm1);
                mm1 = new MainMenu { id = 9, img = "fa-glass", name = "商务订单管理", noChild = true, link = ResolveUrl("~/MoKuai_Shangwu/ShangwuList.aspx") };
                lstMM.Add(mm1);
                mm1 = new MainMenu { id = 8, img = "fa-list", name = "报表", noChild = true, link = ResolveUrl("~/MoKuai_Report/ReportMain.aspx") };
                lstMM.Add(mm1);
            }

            if (Info.roles == "财务")
            {
                //mm1 = new MainMenu { id = 2, img = "fa-group", name = "客户数据管理", noChild = true, link = ResolveUrl("~/MoKuai_Kehu/KehuList.aspx") };
                //lstMM.Add(mm1);
                mm1 = new MainMenu { id = 4, img = "fa-glass", name = "婚宴订单管理", noChild = true, link = ResolveUrl("~/MoKuai_Hunli/HunliList.aspx") };
                lstMM.Add(mm1);
                mm1 = new MainMenu { id = 9, img = "fa-glass", name = "商务订单管理", noChild = true, link = ResolveUrl("~/MoKuai_Shangwu/ShangwuList.aspx") };
                lstMM.Add(mm1);
                mm1 = new MainMenu { id = 8, img = "fa-list", name = "报表", noChild = true, link = ResolveUrl("~/MoKuai_Report/ReportMain.aspx") };
                lstMM.Add(mm1);
            }

            mm1 = new MainMenu { id = 7, img = "fa-user", name = "我的档案", noChild = true, link = ResolveUrl("~/MoKuai_admin/MyProfile.aspx") };
            lstMM.Add(mm1);

            if (Info.roles == "管理员")
            {
                mm1 = new MainMenu { id = 6, img = "fa-cog", name = "系统设置", noChild = true, link = ResolveUrl("~/MoKuai_admin/AdminPanel.aspx") };
                lstMM.Add(mm1);
            }

            //mm1 = new MainMenu { id = 3, img = "fa-bold", name = "商务订单管理", noChild = true, link = ResolveUrl("~/MoKuai_Shangwu/ShangwuList.aspx") };
            //lstMM.Add(mm1);
            mm1 = new MainMenu { id = 5, img = "fa-calendar", name = "活动日历", noChild = true, link = ResolveUrl("~/MoKuai_Rili/Rili.aspx") };
            lstMM.Add(mm1);
          

            string beforeHtml = @"<ul>";
            string endHtml = @"</ul>";
            string node = "";
            builder.Append(beforeHtml);
                foreach (MainMenu item in lstMM)
                {
                    node = @" <li><a href=""{0}"" class=""nav-header""><i class=""fa fa-fw {1}""></i> {2}</a></li>";
                    builder.AppendFormat(node, item.link, item.img, item.name);

                   

                }
            builder.Append(endHtml);
            return builder.ToString();

        }
        public static string enCode(string ToCode)
        {
            string URL = ToCode.Split('?')[0];
            string pram = ToCode.Split('?')[1];
            return URL + "?" + HttpUtility.UrlEncode(HttpUtility.UrlEncode(pram, System.Text.Encoding.UTF8));
        }
        public static Dictionary<string, string> deCode(string FromCode)
        {
            string URL = FromCode.Split('?')[0];
            string pram = FromCode.Split('?')[1];
            string query = HttpUtility.UrlDecode(HttpUtility.UrlDecode(pram, System.Text.Encoding.UTF8));
            string[] qry = query.Split('&');
            Dictionary<string, string> dicQuery = new Dictionary<string, string>();
            for (int i = 0; i < qry.Length; i++)
            {
                dicQuery.Add(qry[i].Split('=')[0], qry[i].Split('=')[1]);
            }
            if (!dicQuery.ContainsKey("act"))
            {
                dicQuery.Add("act", "");
            }
            return dicQuery;
        }
        public static void Show(System.Web.UI.Page page, string msg)
        {
            
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "alert('" + msg.ToString() + "')",true);
        }
        public static void ShowAndReload(System.Web.UI.Page page, string msg,string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "alert('" + msg.ToString() + "');window.location='"+url+"'",true);
        }
        //public static string HandleKehu(string str)
        //{
        //    using (dbml.ebsDBData db = new dbml.ebsDBData())
        //    {
        //        return db.tbKehu.First(q => q.ID == int.Parse(str)).Mingchen;
        //    }
        //}

        //public static string HandleXinren(string xinlang, string xinning)
        //{
        //    string xinren = "";
        //    if (xinlang.Contains("|"))
        //    {
        //        xinren += xinlang.Split('|')[0];
        //    }
        //    else
        //    {
        //        xinren += xinlang;
        //    }

        //    xinren += " / ";
        //    if (xinning.Contains("|"))
        //    {
        //        xinren += xinning.Split('|')[0];
        //    }
        //    else
        //    {
        //        xinren += xinning;
        //    }
        //    return xinren;

        //}
        public class MainMenu
        {
            public int id;
            public string name;
            public string img;
            public bool noChild;
            public string link;
        }
        public class SubMenu
        {
            public int MainId;
            public string name;
            public string link;
            public int number;
        }
        [Serializable]
        public class LoginUser
        {
            public int ID { get; set; }
            public string DisplayName { get; set; }
            public string roles { get; set; }
            public string userName { get; set; }
            public string mailAddress { get; set; }
            public string region { get; set; }
        }
    }
}