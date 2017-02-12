<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Hunli/Hunli.Master" AutoEventWireup="true"
    CodeBehind="HunliPayments.aspx.cs" Inherits="ebs.MoKuai_Hunli.HunliPayments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="hlPaymentsScripts.js?d=<%=DateTime.Now.ToFileTime() %>" type="text/javascript"></script>
    <style type="text/css">
        .OnlyBottom
        {
            border-left: none;
            border-top: none;
            border-right: none;
            border-bottom: 1px solid gray;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">
    付款信息
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Literal ID="WeddingTabs" runat="server"></asp:Literal>
    <br />
    <asp:HiddenField ID="hdID" runat="server" />
    <asp:HiddenField ID="hdZongjineHQ" runat="server" />
    <asp:HiddenField ID="hdZongjineHY" runat="server" />
     <asp:HiddenField ID="hdZongjineHQex" runat="server" />
    <asp:HiddenField ID="hdZongjineHYex" runat="server" />
    <div class="col-sm-12 col-md-12 col-lg-12">
        <div class="panel panel-default">
            <a href="#fukuanInfo" class="panel-heading" data-toggle="collapse">付款内容</a>
            <div id="fukuanInfo" class="panel-collapse panel-body collapse in">
                <div class="table-responsive">
                    <table class="table table-bordered small table-condensed" id="fuKuanTable" style="text-align: center;">
                        <thead>
                            <tr>
                                <th style="text-align: center;">
                                    付款项目
                                </th>
                                <th style="text-align: center;">
                                    支付时间
                                </th>
                                 <th style="text-align: center;">
                                    付款单号
                                </th>
                                <th style="text-align: center;">
                                    支付金额
                                </th>
                                <th style="text-align: center;">
                                    累积金额
                                </th>
                                <th style="text-align: center;">
                                    状态
                                </th>
                                <th style="text-align: center;width:150px">
                                    操作
                                </th>
                            </tr>
                        </thead>
                        
                        <tr id="SummaryKuanxiang" style="vertical-align: middle; background-color: #f9f9f9">
                            <td style="vertical-align: middle">
                                小计
                            </td>
                            <td style="vertical-align: middle">
                            </td>
                             <td style="vertical-align: middle">
                            </td>
                            <td style="vertical-align: middle">
                            </td>
                            <td style="vertical-align: middle">
                               
                                婚宴应收&nbsp;&nbsp;<label id="lbYingshouZongHY" runat="Server" class="Label80px" style=" font-weight:bold"></label>&nbsp;&nbsp;<label id="Label10" style="width:40px"></label>&nbsp;&nbsp;婚宴实收&nbsp;&nbsp;<label
                                        id="lbShishouZongHY" class="Label80px"></label>&nbsp;&nbsp;<label id="lbShishouZongHYB"
                                            class="Label30px"></label>
                                <br />
                                <br />
                                婚庆应收&nbsp;&nbsp;<label id="lbYingshouZongHQ" runat="Server" class="Label80px" style=" font-weight:bold"></label>&nbsp;&nbsp;<label  id="Label11" style="width:40px"></label>&nbsp;&nbsp;婚庆实收&nbsp;&nbsp;<label
                                        id="lbShishouZongHQ" class="Label80px"></label>&nbsp;&nbsp;<label id="lbShishouZongHQB" class="Label30px"></label>
                            </td>
                            <td style="vertical-align: middle">
                            </td>
                            <td style="vertical-align: middle">
                                <div id="btAdd" class="btn btn-link">
                                    增加</div>
                            </td>
                        </tr>
                        <tr id="SummaryAddtional" style="vertical-align: middle; background-color: #f9f9f9">
                            <td style="vertical-align: middle">
                                小计(额外)
                            </td>
                            <td style="vertical-align: middle">
                            </td>
                             <td style="vertical-align: middle">
                            </td>
                            <td style="vertical-align: middle">
                            </td>
                            <td style="vertical-align: middle">
                                婚宴应收&nbsp;&nbsp;<label id="lbYingshouZongHYex"   runat="Server" class="Label80px"  style=" font-weight:bold"></label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label
                                    id="Label2" class="Label30px"></label>&nbsp;&nbsp;婚宴实收&nbsp;&nbsp;<label
                                        id="lbShishouZongHYex" class="Label80px"></label>&nbsp;&nbsp;<label id="lbShishouZongHYBex"
                                            class="Label30px"></label>
                                <br />
                                <br />
                                婚庆应收&nbsp;&nbsp;<label id="lbYingshouZongHQex" runat="Server" class="Label80px"  style=" font-weight:bold"></label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label
                                    id="Label7" class="Label30px"></label>&nbsp;&nbsp;婚庆实收&nbsp;&nbsp;<label
                                        id="lbShishouZongHQex" class="Label80px"></label>&nbsp;&nbsp;<label id="lbShishouZongHQBex" class="Label30px"></label>
                            </td>
                            <td style="vertical-align: middle">
                            </td>
                            <td style="vertical-align: middle">
                                <div id="btAdd_Ex" class="btn btn-link">
                                    增加</div>
                            </td>
                        </tr>
                        <tr id="Summary" style="vertical-align: middle; background-color: #f9f9f9">
                            <td style="vertical-align: middle">
                                合计
                            </td>
                            <td style="vertical-align: middle">
                            </td>
                             <td style="vertical-align: middle">
                            </td>
                            <td style="vertical-align: middle">
                            </td>
                            <td style="vertical-align: middle">
                                婚宴应收&nbsp;&nbsp;<label id="lbhj_yingshouHY"   runat="Server" class="Label80px"  style=" font-weight:bold"></label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label
                                    id="Label3" class="Label30px"></label>&nbsp;&nbsp;婚宴实收&nbsp;&nbsp;<label
                                        id="lbhj_shishouHY" class="Label80px"></label>&nbsp;&nbsp;<label id="Label5"
                                            class="Label30px"></label>
                                <br />
                                <br />
                                婚庆应收&nbsp;&nbsp;<label id="lbhj_yingshouHQ" runat="Server" class="Label80px"  style=" font-weight:bold"></label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label
                                    id="Label8" class="Label30px"></label>&nbsp;&nbsp;婚庆实收&nbsp;&nbsp;<label
                                        id="lbhj_shishouHQ" class="Label80px"></label>&nbsp;&nbsp;<label id="Label12" class="Label30px"></label>
                            </td>
                            <td style="vertical-align: middle">
                            </td>
                            <td style="vertical-align: middle">
                                
                            </td>
                        </tr>
                    </table>
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
                       
                                婚宴金额&nbsp;&nbsp;<label id="lbZongTKHY" class="lbZongTKHY" style=" font-weight:bold"></label>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;<label id='lbdxtbHQhd'  class='Label80px'>&nbsp;</label>
                                <br />
                                <br />
                                婚庆金额&nbsp;&nbsp;<label id="lbZongTKHQ" class="lbZongTKHQ" style=" font-weight:bold"></label>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;<label id='Label1hd'  class='Label80px'>&nbsp;</label>
                            </td>
                            <td style="vertical-align: middle">
                            </td>
                            <td style="vertical-align: middle">
                                <div id="btAdd_Tk" class="btn btn-link">
                                    增加</div>
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
