<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Shangwu/Shangwu.Master" AutoEventWireup="true" CodeBehind="SWAuditFinance.aspx.cs" Inherits="ebs.MoKuai_Shangwu.SWAuditFinance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<script src="SWAuditFinance.js?d=<%=DateTime.Now.ToFileTime() %>" type="text/javascript"></script>
<style type="text/css">
        .OnlyBottom
        {
            border-left: none;
            border-top: none;
            border-right: none;
            border-bottom: none;
            text-align: center;
            width: 80px;
        }
        .Label80px
        {
            width: 60px;
        }
        .Label30px
        {
            width: 30px;
        }
           .PayAmount,.PayAmountR
        {
            min-width:120px;
        }
        .PayAmountDaxie
        {
            min-width: 140px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">
付款审核
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Literal ID="BussinessTabs" runat="server"></asp:Literal>
    <br />
    <asp:HiddenField ID="hdZongjine" runat="server" />
    <asp:HiddenField ID="hdID" runat="server" />
    <div class="col-sm-12 col-md-12 col-lg-12">
        <div class="panel panel-default">
            <a href="#fukuanInfo" class="panel-heading" data-toggle="collapse">付款内容</a>
            <div id="fukuanInfo" class="panel-collapse panel-body collapse in">
                <div class="table-responsive">
                    <table class="table table-bordered small table-condensed" id="fuKuanTable" style="text-align: center;">
                        <thead>
                            <tr>
                                <th style="text-align: center;">
                                    付款单号
                                </th>
                                <th style="text-align: center;">
                                    支付时间
                                </th>
                                <th style="text-align: center;">
                                    支付方式
                                </th>
                                <th style="text-align: center;">
                                    支付金额
                                </th>
                                 <th style="text-align: center;">
                                    状态
                                </th>
                                <th style="text-align: center;width:150px">
                                    操作
                                </th>
                            </tr>
                        </thead>
                        <tr id="Summary" style="vertical-align: middle; background-color: #f9f9f9">
                            <td style="vertical-align: middle">
                                合计
                            </td>
                            <td style="vertical-align: middle">
                            </td>
                            <td style="vertical-align: middle">
                            </td>
                            <td style="vertical-align: middle;text-align:left;padding-left:10px">
                                金额&nbsp;&nbsp;<label id="lbShishou"  class=" PayAmount"  style=" font-weight:bold;min-width:120px; padding:auto;"></label>
                                  &nbsp;&nbsp;&nbsp;&nbsp; 应收&nbsp;&nbsp;<label
                                        id="lbYingshou"  style="min-width:140px;font-weight:bold"></label>&nbsp;&nbsp;
                                <label class="Label30px" id="lbShishouPer"></label>
                            </td>
                            <td style="vertical-align: middle">
                            </td>
                          
                            <td style="vertical-align: middle">
                              <asp:Button ID="btFinish" runat="server" CssClass="btn btn-default" 
                                    style="display:none" Text="结束订单" onclick="btFinish_Click" />
                            </td>
                        </tr>
                    </table>
                     <small>注：当收款总额>= 应收款总额，可进行订单结束操作。</small>
                   <table class="table table-bordered small table-condensed" id="tuiKuanTable" style="text-align: center;">
                        <thead>
                            <tr>
                                <th style="text-align: center;">
                                    退款项目
                                </th>
                                <th style="text-align: center;">
                                    退款时间
                                </th>
                                <th style="text-align: center;">
                                    支付金额
                                </th>
                               
                                <th style="text-align: center;">
                                    状态
                                </th>
                                <th style="text-align: center;width:150px">
                                    操作
                                </th>
                            </tr>
                        </thead>
                        <tr id="SummaryTuikuan" style="vertical-align: middle; background-color: #f9f9f9">
                            <td style="vertical-align: middle">
                                合计
                            </td>
                            <td style="vertical-align: middle">
                            </td>
                           
                            <td style="vertical-align: middle">
                       
                                金额&nbsp;&nbsp;<label id="lbZongTKHY" class="lbZongTKHY" style=" font-weight:bold"></label>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;<label id='lbdxtbHQhd'  class='Label80px'>&nbsp;</label>
                               </td>
                            <td style="vertical-align: middle">
                            </td>
                            <td style="vertical-align: middle">
                                 
                            </td>
                        </tr>
                        </table>
                </div>
                
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
