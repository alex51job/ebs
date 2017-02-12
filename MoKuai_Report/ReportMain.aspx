<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Report/ReportMuban.Master"
    AutoEventWireup="true" CodeBehind="ReportMain.aspx.cs" Inherits="ebs.MoKuai_Report.ReportMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="ReportScripts.js?d=<%=DateTime.Now.ToFileTime() %>" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".TimeSelect").datetimepicker({
                format: 'yyyy-mm',
                autoclose: true,
                todayBtn: false,
                startView: "year",
                minView: "year",
                showMeridian: true
            });

            $(".YearSelect").datetimepicker({
                format: 'yyyy',
                autoclose: true,
                todayBtn: false,
                startView: "decade",
                minView: "decade",
                showMeridian: true
            })


        })

       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">
    报表
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-12 col-md-12 col-lg-12 col-lg-offset-0">
        <div class="panel panel-default">
            <a href="#searchBox" class="panel-heading" data-toggle="collapse"></a>
            <div id="searchBox" class="panel-collapse panel-body collapse in">
                <div class="main-content">
                    <div class="row">
                        <div class=" col-lg-12 form-horizontal ">
                            <ul class="list-group">
                                <li class="list-group-item">财务婚宴订单明细报表:&nbsp;&nbsp;<div id="btnByTimeRange_Rpt1" class="btn btn-link btn-sm"
                                    data-toggle="modal" data-target="#myModal">
                                    按时间范围</div>
                                </li>
                            </ul>
                        </div>
                         <div class=" col-lg-12 form-horizontal ">
                            <ul class="list-group">
                                <li class="list-group-item">一滴水月完成情况汇总表:&nbsp;&nbsp;<asp:LinkButton ID="lbYDSWeddingMonthly"
                                    class="btn btn-link btn-sm"  data-toggle="modal" data-target="#myModal2">按时间范围</asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                         <div class=" col-lg-12 form-horizontal ">
                            <ul class="list-group">
                                <li class="list-group-item">一滴水婚宴预定合计:&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"
                                    class="btn btn-link btn-sm"  data-toggle="modal" data-target="#myModal3">按年份</asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;
                    </button>
                    <h4 class="modal-title" id="myModalLabel">
                        按时间范围导出财务婚宴订单明细报表
                    </h4>
                </div>
                <div class="modal-body">
                    <div class=" col-lg-12 ">
                        <div class="form-group  col-lg-12 ">

                            <div class="col-sm-12">
                                 合同日期开始 <asp:TextBox ID="tbHetongStart" runat="server" class="form-control TimeSelect"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group  col-lg-12">

                            <div class="col-sm-12">
                                合同日期结束 <asp:TextBox ID="tbHetongEnd" runat="server" class="form-control TimeSelect"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        关闭
                    </button>
                    <asp:Button ID="ReportExport1"  class="btn btn-primary" runat="server" 
                        Text="提交" onclick="ReportExport1_Click" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal -->
    </div>
    <div class="modal fade" id="myModal2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;
                    </button>
                    <h4 class="modal-title" id="H1">
                        按月范围导出一滴水月完成情况汇总表
                    </h4>
                </div>
                <div class="modal-body">
                    <div class=" col-lg-12 ">
                        <div class="form-group  col-lg-12 ">

                            <div class="col-sm-12">
                                 开始年月 <asp:TextBox ID="tbStartMonth" runat="server" class="form-control TimeSelect"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group  col-lg-12">

                            <div class="col-sm-12">
                                结束年月 <asp:TextBox ID="tbEndMonth" runat="server" class="form-control TimeSelect"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        关闭
                    </button>
                    <asp:Button ID="Button1"  class="btn btn-primary" runat="server" 
                        Text="提交" onclick="lbYDSWeddingMonthly_Click" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal -->
    </div>
    <div class="modal fade" id="myModal3" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;
                    </button>
                    <h4 class="modal-title" id="H2">
                        按年份导出一滴水婚宴预定合计
                    </h4>
                </div>
                <div class="modal-body">
                    <div class=" col-lg-12 ">
                        <div class="form-group  col-lg-12 ">

                            <div class="col-sm-12">
                                 年份 <asp:TextBox ID="tbYear" runat="server" class="form-control YearSelect"></asp:TextBox>
                            </div>
                        </div>
                       
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        关闭
                    </button>
                    <asp:Button ID="btReport3"  class="btn btn-primary" runat="server"  OnClick="lbYearReport_Click"
                        Text="提交" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal -->
    </div>
</asp:Content>
