using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ebs.commons;

namespace ebs
{
    public partial class weddingContract : System.Web.UI.Page
    {
        public string projectName = "";

        public string head = "";
        public string menu = "";
        public string foot = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            projectName = ComCls.title;
            //head = ComCls.CreateHead("", "");
            //menu = ComCls.CreateMenu("");
            foot = ComCls.CreateFoot();
            if (!IsPostBack)
            {

            }
        }
    }
}