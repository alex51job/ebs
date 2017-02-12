using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ebs.dbml;
using System.Web.UI.WebControls;


namespace ebs.commons
{
    public static class ddlSet
    {
        public static void ddlForMenDian(this DropDownList ddl, string selectedItem = "")
        {
            List<string> source = new List<string>();
            using (ebsDBData db = new ebsDBData())
            {
                foreach (var item in db.tbSysMendian)
                {
                    source.Add(item.Mendian.Trim());
                }
            }
            ddl.DataSource = source.removeDuplicateList(selectedItem);
            ddl.DataBind();
        }

        public static void ddlForQudao(this DropDownList ddl, string Cate,string selectedItem = "")
        {
            List<string> source = new List<string>();
            using (ebsDBData db = new ebsDBData())
            {
                List<tbSysQudao> ds;
                //var ds = db.tbSysQudao.Where(q=>q.Cate == Cate)
                if (Cate == "All")
                {
                    ds = db.tbSysQudao.ToList();
                }
                else if (selectedItem !="")
                {
                    string type = db.tbSysQudao.First(q => q.Qudao == selectedItem).Cate;
                    ds = db.tbSysQudao.Where(q => q.Cate == type).ToList();

                }
                else ds = db.tbSysQudao.Where(q=>q.Cate == Cate).ToList();


                foreach (var item in ds)
                {
                    if (!source.Contains(item.Qudao))
                    {
                        source.Add(item.Qudao);
                    }
                   
                }

            }
            ddl.DataSource = source.removeDuplicateList(selectedItem);
            ddl.DataBind();
        }

        public static void ddlForCustomerType(this DropDownList ddl, string selectedItem = "")
        {
            List<string> source = new List<string>();
            source.Add("婚宴");
            source.Add("商务");
            ddl.DataSource = source.removeDuplicateList(selectedItem);
            ddl.DataBind();
        }

        public static void ddlForWuWanCan(this DropDownList ddl, string selectedItem = "")
        {
            List<string> source = new List<string>();
            source.Add("午餐");
            source.Add("晚餐");
            ddl.DataSource = source.removeDuplicateList(selectedItem);
            ddl.DataBind();
        }

        public static void ddlForHunliOrderStatus(this DropDownList ddl, string selectedItem = "")
        {
            List<string> source = new List<string>();
            source.Add("编辑");
            source.Add("待审批(主管)");
            source.Add("待审批(总经理)");
            source.Add("待审批(文员)");
            source.Add("待审批(财务)");
            source.Add("审批完成");
            source.Add("结束");
            source.Add("已退订");
            ddl.DataSource = source.removeDuplicateList(selectedItem);
            ddl.DataBind();
        }

        public static void ddlForPayStatus(this DropDownList ddl, string selectedItem = "")
        {
            List<string> source = new List<string>();
            source.Add("待编辑");
            source.Add("审核完成");
            source.Add("已退订");
            ddl.DataSource = source.removeDuplicateList(selectedItem);
            ddl.DataBind();
        }

        public static void ddlForSales(this DropDownList ddl, string selectedItem = "", string region = "", string type = "")
        {
            using (ebsDBData db = new ebsDBData())
            {
                var datasource = db.tbUsers.Where(q => q.Role.Contains("销售"));
                if (region == "All") datasource = db.tbUsers.Where(q => q.Role.Contains("销售"));
                else datasource = db.tbUsers.Where(q => q.Region == region && q.Role.Contains("销售"));

                if (type != "")
                {
                    datasource = datasource.Where(q => q.Role.Contains(type));
                }

                ddl.DataSource = datasource;
                ddl.DataValueField = "UserName";
                ddl.DataTextField = "DisplayName";
                ddl.DataBind();
                if (selectedItem != "" && ddl.Items.FindByValue(selectedItem) != null)
                {
                    ddl.Items.FindByValue(selectedItem).Selected = true;
                }
                else ddl.Items.Insert(0, new ListItem());
            }

        }

        public static void ddlForKefus(this DropDownList ddl, string selectedItem = "", string region = "")
        {

            using (ebsDBData db = new ebsDBData())
            {
                if (region == "All") ddl.DataSource = db.tbUsers.Where(q => q.Role.Contains("客服"));
                else ddl.DataSource = db.tbUsers.Where(q => q.Region == region && q.Role.Contains("客服"));
                ddl.DataValueField = "UserName";
                ddl.DataTextField = "DisplayName";
                ddl.DataBind();
                if (selectedItem != "")
                {
                    ddl.Items.FindByValue(selectedItem).Selected = true;
                }
                else ddl.Items.Insert(0, new ListItem());
            }

        }

        public static void ddlForYishiChangdi(this DropDownList ddl, string selectedItem = "")
        {

            using (ebsDBData db = new ebsDBData())
            {
                ddl.DataSource = db.tbSysYishiChangdi;
                ddl.DataTextField = "YishiChangdi";
                ddl.DataBind();
                if (selectedItem != "")
                {
                    ddl.Items.FindByValue(selectedItem).Selected = true;
                }
                else ddl.Items.Insert(0, new ListItem());
                
            }
        }
        /// <summary>
        /// 设置宴会厅的多选模式
        /// </summary>
        /// <param name="ddl">下拉菜单</param>
        /// <param name="selectedItem">选中的item</param>
        public static void ddlForYanhuiting(this DropDownList ddl, string selectedItem = "")
        {
            using (ebsDBData db = new ebsDBData())
            {
                ddl.DataSource = db.tbSysVenue;
                ddl.DataTextField = "VenueName";
                ddl.DataBind();
                if (selectedItem != "")
                {
                    ddl.Items.FindByValue(selectedItem).Selected = true;
                }
                else ddl.Items.Insert(0, new ListItem());
            }
        }

        public static void ddlForMenu(this DropDownList ddl, string selectedItem = "")
        {
            using (ebsDBData db = new ebsDBData())
            {
                ddl.DataSource = db.tbSysMenu;
                ddl.DataTextField = "MenuName";
                ddl.DataBind();
                if (selectedItem != "")
                {
                    ddl.Items.FindByValue(selectedItem).Selected = true;
                }
                else ddl.Items.Insert(0, new ListItem());
            }
        }

        public static void ddlForHunqinMenu(this DropDownList ddl, string selectedItem = "")
        {
            using (ebsDBData db = new ebsDBData())
            {
                ddl.DataSource = db.tbSysHunqinMenu;
                ddl.DataTextField = "HunQinMenuName";
                ddl.DataBind();
                if (selectedItem != "")
                {
                    ddl.Items.FindByValue(selectedItem).Selected = true;
                }
                else ddl.Items.Insert(0, new ListItem());
            }
        } 


         public static void ddlForKehuCombination(this DropDownList ddl,string sales="", string selectedItem = "")
        {
            using (ebsDBData db = new ebsDBData())
            {
                var res = from q in db.Customers
                          join t in db.tbUsers on q.Sales equals t.UserName
                          where q.AuditType != "无效" && q.Zhuangtai =="已下发"
                         
                          select new
                          {
                              comb = q.CustomerName + " / " + q.Telephone + " / " + t.DisplayName + " / " + q.ZixunDiDian,
                              sales = q.Sales,
                              cusID = q.ID
                          };
                if (sales !="")
                {
                    res = res.Where(q => q.sales == sales);
                }
                ddl.DataSource = res;
                ddl.DataTextField = "comb";
                ddl.DataValueField = "cusID";
                ddl.DataBind();
                if (selectedItem != "")
                {
                    ddl.Items.FindByValue(selectedItem).Selected = true;
                }
                else ddl.Items.Insert(0, new ListItem());
            }
        }

         public static void ddlForEventType(this DropDownList ddl, string selectedItem = "")
         {
             using (ebsDBData db = new ebsDBData())
             {
                 ddl.DataSource = db.tbSysEventType.ToList();
                 ddl.DataTextField = "Type";
                 ddl.DataBind();
                 if (selectedItem != "" && ddl.Items.FindByValue(selectedItem) !=null) ddl.Items.FindByValue(selectedItem).Selected = true;
                 else ddl.Items.Insert(0, new ListItem());
             }
         }
       





        /// General
        public static List<string> removeDuplicateList(this List<string> source, string selectedItem)
        {
            if (selectedItem != "")
            {
                source.Remove(selectedItem);
                source.Insert(0, selectedItem);
            }
            else source.Insert(0, "");
            return source;
        }

        public static void setDisabled<T>(this T control) where T : System.Web.UI.WebControls.WebControl
        {
            control.Attributes.Add("disabled", "disabled");
        }

        public static void setReadonly<T>(this T control) where T : System.Web.UI.WebControls.WebControl
        {
            control.Attributes.Add("Readonly", "Readonly");
            if (control.GetType() == typeof(DropDownList))
            {
                DropDownList objDDL = control as DropDownList;
                ListItem firstItem = objDDL.SelectedItem;
                objDDL.Items.Clear();
                objDDL.Items.Add(firstItem);
                
            }
          
//            var cid = control.ClientID;
//            string jsfunc = string.Format(@"function addReadonlySpan() {{
//                                             var sp = '<span onmousemove=this.setCapture(); onmouseout=this.releaseCapture(); onfocus=this.blur();></span>';
//                                                    $('#{0}').wrap(sp);}};
//                                        addReadonlySpan();", cid);
//            System.Web.UI.ScriptManager.RegisterStartupScript(control.Page, control.Page.GetType(), "SetReadonly", jsfunc, true);
           
        }

        public static void setEnabled<T>(this T control) where T : System.Web.UI.WebControls.WebControl
        {
            control.Attributes.Remove("disabled");
        }

    }
}