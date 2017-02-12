<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_admin/admin.Master" AutoEventWireup="true"
    CodeBehind="AdminPanel.aspx.cs" Inherits="ebs.MoKuai_admin.AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        .content-box, .content-box-header, ul.content-box-tabs li a.current, .shortcut-button, .notification
        {
            -moz-border-radius: 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px;
        }
        .shortcut-button
        {
            border: 1px solid #ccc;
            background: #f7f7f7 url('/Resource/images/shortcut-button-bg.gif') top left no-repeat;
            display: block;
            width: 120px;
            margin: 0 0 20px 0;
        }
        .shortcut-button span
        {
            -moz-border-radius: 7px;
            -webkit-border-radius: 7px;
            border-radius: 7px;
        }
        .shortcut-button span
        {
            border: 1px solid #fff;
            display: block;
            padding: 15px 10px 15px 10px;
            text-align: center;
            color: #555;
            font-size: 13px;
            line-height: 1.3em;
        }
        ul.shortcut-buttons-set
        {
            float: left;
            list-style: none;
        }
        ul.shortcut-buttons-set li
        {
            float: left;
            margin: 0 15px 0 0;
            padding: 0 !important;
            background: 0;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">
    管理与设置
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<table style="width: 100%; margin-bottom: 170px;">
        <tr>
            <td style="width: 2%;">
            </td>
            <td style="width: 50%;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <font style="font-size: large;">请选择一个模块进行设置</font> <small>&nbsp;&nbsp;&nbsp;请注意 ： 场地和用户的改变，会同步改变已绑定该场地与用户的订单。</small>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <ul class="shortcut-buttons-set">
                                <li><a class="shortcut-button" href='<%= ResolveUrl("~/MoKuai_admin/YonghuList.aspx")%>'><span>
                                 <img src="../images/Users.png" alt="icon" style="width:55px; height:55px;"  /> 
                                    <br />
                                    <font style="font-size: small; font-weight: bold;">用户管理 </font></span></a></li>
                              
                                <li><a class="shortcut-button" href='<%= ResolveUrl("~/MoKuai_admin/ChangdiList.aspx")%>'><span>
                                <img src="../images/site2.png" alt="icon"  style="width:55px; height:55px;"/>
                                  <br />
                                    <font style="font-size: small; font-weight: bold;">场地设置 </font></span></a>
                                </li>
                                  <li><a class="shortcut-button" href='<%= ResolveUrl("~/MoKuai_admin/CaidanList.aspx")%>'><span>
                                    <img src="../images/food1.png" alt="icon" style="width:55px; height:55px;" />
                                    <br />
                                    <font style="font-size: small; font-weight: bold;">菜单设置 </font></span></a>
                                </li>
                             <%--    <li><a class="shortcut-button" href='<%= ResolveUrl("~/MoKuai_admin/RiliManager.aspx")%>'><span>
                                <img src="../images/rili.png" alt="icon"  style="width:55px; height:55px;"/>
                                  <br />
                                    <font style="font-size: small; font-weight: bold;">日历设置 </font></span></a>
                                </li>--%>
                                
                                  <li><a class="shortcut-button" href='<%= ResolveUrl("~/MoKuai_admin/MyProfile.aspx")%>'><span>
                                    <img src="../images/customer.png" alt="icon" style="width:55px; height:55px;" />
                                    <br />
                                    <font style="font-size: small; font-weight: bold;">我的档案 </font></span></a>
                                </li>
                            </ul>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 25%;">
            </td>
        </tr>
    </table>
   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
