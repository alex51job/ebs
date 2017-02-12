<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.master"  CodeBehind="webAdmin.aspx.cs" Inherits="ebs.webAdmin" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeaderContent">
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
</asp:content>
<asp:content id="TitleContent" runat="server" contentplaceholderid="titleContent">
    <h1 class="page-title">客户数据管理与设置</h1>
</asp:content>
<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">
 <table style="width: 100%; margin-bottom: 170px;">
        <tr>
            <td style="width: 2%;">
            </td>
            <td style="width: 50%;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <font style="font-size: large;">请选择一个模块进行设置</font>
                        </td>
                    </tr>
                    <tr style="height: 10px;">
                    </tr>
                    <tr align="center">
                        <td>
                            <ul class="shortcut-buttons-set">
                                <li><a class="shortcut-button" href="#"><span>
                                    <img src="../images/Users.png" alt="icon" style="width:55px; height:55px;"  />
                                    <br />
                                    <font style="font-size: small; font-weight: bold;">用户 </font></span></a></li>
                              
                                <li><a class="shortcut-button" href="#"><span>
                                    <img src="../images/customer.png" alt="icon" style="width:55px; height:55px;" /><br />
                                    <font style="font-size: small; font-weight: bold;">客户 </font></span></a>
                                </li>
                                  <li><a class="shortcut-button" href="#"><span>
                                    <img src="../images/venue.png" alt="icon"  style="width:55px; height:55px;"/><br />
                                    <font style="font-size: small; font-weight: bold;">场地 </font></span></a>
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
 </asp:content>