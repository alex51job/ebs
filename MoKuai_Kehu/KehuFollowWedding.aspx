<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Kehu/Kehu.Master" AutoEventWireup="true"
    CodeBehind="KehuFollowWedding.aspx.cs" Inherits="ebs.MoKuai_Kehu.KehuFollowWedding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
    function makeDate() {
        $(".TimeSelect").datetimepicker({
            format: 'yyyy-mm-dd',
            autoclose: true,
            todayBtn: true,
            startView: "month",
            minView: "month",

            showMeridian: true
        }).change(function (e) {
            //alert("1");
           // if ($(e)[0].target.id == "MainContent_txtInputDate") {
             //   $('#MainForm').data('bootstrapValidator').revalidateField('<%//= txtInputDate.UniqueID %>');
            //}


        });
    }
    </script>
     <script type="text/javascript">

         $(function () {          
             makeDate();
         });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">
    婚宴客户跟进
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   <asp:Literal ID="CustomerTabs" runat="server"></asp:Literal>
    <br />
    <div class="col-sm-10 col-md-10 col-lg-10 col-lg-offset-1">
        <div class="panel panel-default">
            <a href="#SalesFollow" class="panel-heading" data-toggle="collapse">销售部跟踪信息</a>
            <div id="SalesFollow" class="panel-collapse panel-body collapse in">
                <div class="main-content">
                    <div class="row">
                        <div class=" col-lg-12 form-horizontal ">
                            <br>
                            <div class="form-group form-group-sm col-lg-3 col-xs-3">
                                <label class="col-sm-4 control-label">
                                    是否到店:
                                    <asp:Label ID="lbDaodian" runat="server" CssClass="" Text=""></asp:Label>
                                </label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlDaodian" CssClass="form-control" runat="server">
                                        <asp:ListItem>是</asp:ListItem>
                                        <asp:ListItem>否</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group form-group-sm col-lg-3 col-xs-3">
                                <label class="col-sm-4 control-label">
                                    到店时间:
                                    <asp:Label ID="lbDaodianshijian" runat="server" CssClass="" Text=""></asp:Label></label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="tbDaodianshijian" CssClass="form-control TimeSelect" runat="server"></asp:TextBox>
                                    </div>
                            </div>
                            <div class="form-group form-group-sm col-lg-3  col-xs-3">
                                <label class="col-sm-4 control-label">
                                    是否成单：<asp:Label ID="lbChendan" runat="server" CssClass="" Text=""></asp:Label></label>
                                      <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlChengdan" CssClass="form-control" runat="server">
                                        <asp:ListItem>是</asp:ListItem>
                                        <asp:ListItem>否</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                           
                            <div class="form-group form-group-sm col-lg-3  col-xs-3">
                                <label class="col-sm-4 control-label">
                                    渠道返点：<asp:Label ID="lbFandian" runat="server" CssClass="" Text=""></asp:Label></label>
                                     <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlFandian" CssClass="form-control" runat="server">
                                        <asp:ListItem>是</asp:ListItem>
                                        <asp:ListItem>否</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group col-lg-12 ">
                                <label class="col-sm-4">
                                    销售反馈信息 ：</label>
                                <div class="col-sm-12">
                                    <asp:TextBox ID="tbFeedback_Sales" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group form-group-sm col-lg-3 col-xs-3">
                                <label class="col-sm-4 control-label">
                                    跟进时间：<asp:Label ID="lbFollowDate_Sales" runat="server" CssClass="" Text=""></asp:Label></label>
                                    
                            </div>
   <div style="text-align: center" class="col-sm-12">
            <asp:Button ID="btSubmit_Sales" runat="server" class="btn btn-default" 
                Text="更新" onclick="btSubmit_Sales_Click"  />
         
        </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <a href="#KefuFollow" class="panel-heading" data-toggle="collapse">客服部跟踪信息</a>
            <div id="KefuFollow" class="panel-collapse panel-body collapse in">
                <div class="main-content">
                    <div class="row">
                        <div class=" col-lg-12 form-horizontal ">
                            <br>
                            <div class="form-group form-group-sm col-lg-3 col-xs-3">
                                <label class="col-sm-4 control-label">
                                    回访日期:
                                    <asp:Label ID="lbHuifangRiqi" runat="server" CssClass="" Text=""></asp:Label>
                                </label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbHuifangRiqi" runat="server" CssClass=" form-control TimeSelect"></asp:TextBox></div>
                            </div>
                            <div class="form-group form-group-sm col-lg-3 col-xs-3">
                                <label class="col-sm-4 control-label">
                                    回访客服:
                                    <asp:Label ID="lbHuifangKefu" runat="server" CssClass="" Text=""></asp:Label></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbHuifangkefu" runat="server" CssClass=" form-control"></asp:TextBox></div>
                            </div>
                        </div>
                        <div class="form-group col-lg-12 ">
                            <label class="col-sm-4">
                                回访信息 ：</label>
                            <div class="col-sm-12">
                                <asp:TextBox ID="tbFeedback_Kefu" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="form-group form-group-sm col-lg-3 col-xs-3">
                            <label class="col-sm-4 control-label">
                                跟进时间：<asp:Label ID="lbFollowDate_Kefu" runat="server" CssClass="" Text=""></asp:Label></label>
                            
                        
                    </div>
                    <div style="text-align: center" class="col-sm-12">
            <asp:Button ID="btSubmit_Kefu" runat="server" class="btn btn-default" Text="更新" 
                            onclick="btSubmit_Kefu_Click"  />
         
        </div>
                </div>
            </div>
        </div>
        </div>
        <div class="panel panel-default">
            <a href="#FollowLog" class="panel-heading" data-toggle="collapse">跟踪历史记录</a>
            <div id="FollowLog" class="panel-collapse  collapse in">
                    <table class="table table-condensed">
                        <tr>
                            <th>
                                跟进时间
                            </th>
                            <th>
                                跟进人
                            </th>
                            <th>
                                跟进角色
                            </th>
                            <th>
                                跟进类型
                            </th>
                            <th>
                                到店/回访时间
                            </th>
                            <th>
                                跟进信息
                            </th>
                        </tr>
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("FollowDate","{0:yyyy-MM-dd HH:mm:ss}") %>
                                    </td>
                                    <td><%#Eval("UserName") %>
                                    </td>
                                    <td><%#Eval("UserRole") %>
                                    </td>
                                    <td><%#Eval("FollowType") %>
                                    </td>
                                    <td><%#Eval("FeedbackDate") %>
                                    </td>
                                    <td><%#Eval("FeedbackInfo") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
