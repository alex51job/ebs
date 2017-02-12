using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using ebs.commons;
using ebs.dbml;


namespace ebs
{
    public partial class LoginChecker:System.Web.UI.Page
    {
        public ComCls.LoginUser LoginUserInfo { get; set; }
        public string projectName { get; set; }
        public string head { get; set; }
        public string stats { get; set; }
        public string foot { get; set; }
        public string menu { get; set; }
        //public bool Validate { get; set; }
       

        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                initJavascript();
            }
          
          
            //ForTest
            //using (ebsDBData db = new ebsDBData())
            //{
            //    tbUsers us = db.tbUsers.First(q => q.ID == 16);
            //    Session["LoginUserInfo"] = new ComCls.LoginUser { ID = us.ID, mailAddress = us.UserMail, userName = us.UserName, DisplayName = us.DisplayName, roles = us.Role };
            //}
            if (HttpContext.Current.Session["LoginUserInfo"] != null)
            {
                LoginUserInfo = (ComCls.LoginUser)(HttpContext.Current.Session["LoginUserInfo"]);
                head = ComCls.CreateHead(LoginUserInfo);
                //stats = ComCls.CreateStats(LoginUserInfo);
                foot = ComCls.CreateFoot();
                menu = ComCls.CreateMenu(LoginUserInfo, this.ResolveUrl);
            }
            else
            {
           
                Response.Write("<script language='javascript'>alert('登陆已过期，请重新登陆！');window.location=\""+ResolveUrl("~/login.aspx")+"\"</script>");
    
            }
            base.OnInit(e);
          
        }
        #region "页面加载中效果"

        /// <summary>

        /// 页面加载中效果

        /// </summary>

        public static void initJavascript()
        {

            string ua = HttpContext.Current.Request.UserAgent.ToLower();

            if (HttpContext.Current.Session["blog"] == null && !ua.Contains("bot") && !ua.Contains("spider") && !ua.Contains("slurp") && !ua.Contains("google") && !ua.Contains("baidu"))
            {

                string f = string.Empty;

                f += " <script language=JavaScript type=text/javascript>";

                f += "var t_id = setInterval(animate,20);";

                f += "var pos=0;var dir=2;var len=0;";

                f += "function animate(){";

                f += "var elem = document.getElementById('progress');";

                f += "if(elem != null) {";

                f += "if (pos==0) len += dir;";

                f += "if (len>32 || pos>79) pos += dir;";

                f += "if (pos>79) len -= dir;";

                f += " if (pos>79 && len==0) pos=0;";

                f += "elem.style.left = pos;";

                f += "elem.style.width = len;";

                f += "}}";

                f += "</script>";

                f += "<style>";

                f += "#loader_container {text-align:center; position:fixed;_position:absolute; top:40%; width:100%; left: 0;}";

                f += "#loader {font-family:Tahoma, Helvetica, sans; font-size:11.5px; color:#000000; background-color:#FFFFFF; padding:10px 0 16px 0; margin:0 auto; display:block; width:130px; border:1px solid #5a667b; text-align:left; z-index:2;}";

                f += "#progress {height:5px; font-size:1px; width:1px; position:relative; top:1px; left:0px; background-color:#8894a8;}";

                f += "#loader_bg {background-color:#e4e7eb; position:relative; top:8px; left:8px; height:7px; width:113px; font-size:1px;}";

                f += "</style>";

                f += "<div id=loader_container>";

                f += "<div id=loader>";

                f += "<div align=center>页面正在加载中 ...</div>";

                f += "<div id=loader_bg><div id=progress> </div></div>";

                f += "</div></div>";

                HttpContext.Current.Session["blog"] = "billpeng";

                HttpContext.Current.Response.Write(f + "<script>location.replace('" + HttpContext.Current.Request.Url.ToString() + "');</script>");

                HttpContext.Current.Response.Flush();

                HttpContext.Current.Response.End();

            }

            else
            {

                HttpContext.Current.Session["blog"] = null;

            }

        }

        #endregion
    }
}