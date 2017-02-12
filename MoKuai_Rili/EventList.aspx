<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventList.aspx.cs" Inherits="ebs.MoKuai_Rili.EventList" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>活动日历</title>
    <meta content="IE=Edge,chrome=1" http-equiv="X-UA-Compatible" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link href="../Styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../font/font-awesome/css/font-awesome.css" />
    <link rel="stylesheet" type="text/css" href="../styles/theme.css" />
    <link rel="stylesheet" type="text/css" href="../styles/premium.css" />
    <link rel="stylesheet" href="css/calendar.css">
    <link href="../datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../bootstrapValidator/dist/css/bootstrapValidator.min.css" rel="stylesheet"
        type="text/css" />
    <!-- Bootstrap -->
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../bootstrapValidator/dist/js/bootstrapValidator.min.js" type="text/javascript"></script>
    <style type="text/css">
        .navbar-default .navbar-brand, .navbar-default .navbar-brand:hover
        {
            color: #fff;
        }
        
        .modal-lg
        {
            width: 950px;
        }
        .has-feedback .form-control-feedback
        {
            right: -30px !important;
        }
        .has-feedback .form-control
        {
            padding-right: 10px !important;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            var uls = $('.sidebar-nav > ul > *').clone();
            uls.addClass('visible-xs');
            $('#main-menu').append(uls.clone());
            $("[rel=tooltip]").tooltip();

        });
    </script>
    <script type="text/javascript">
        function makeDate() {
            $(".TimeSelect").datetimepicker({
                format: 'yyyy-mm',
                autoclose: true,
                todayBtn: false,
                startView: "year",
                minView: "year",
                showMeridian: true
            }).on("changeDate", function (ev) {
                $(".hourSelect").datetimepicker('setStartDate', ev.date.valueOf());
            });

            $(".hourSelect").datetimepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                todayBtn: false,
                startView: "year",
                minView: "month",
                showMeridian: true
            });
        }
        $(function () {
            makeDate();
        })
        function checkTxtDt() {
            if ($("#txtDt").val() == "") {
                alert("请选择月份");
                return false;

            }
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $("#btAdd").click(function () {

                $('#exampleModal').modal();
                $("#tbTitle").val("");
                $("#tbRenshu").val("");
                $("#tbRiqi").val("");
                $("#ddStartHour").val("01");
                $("#ddEndHour").val("01");
                $("#tbShanghu").val("");
                $("#tbJine").val("");
                $("#tbSales").val("");
                $("#tbYanhuiting").val("");
                $("#ddIsconfirmed").val("");
                $("#ddHascan").val("");
                $("#tbYishichangdi").val("");
                $("#tbBeizhu").val("");
                $("#hdID").val(0);
                $('#exampleModal').modal('show');

            });

            $("#btUpdate").click(function () {
                $.ajax({
                    url: "crudEventList.ashx?",
                    async: false,
                    data: { "act": "read", "ID": 1, "dt": new Date().getTime() },
                    success: function (data) {
                        var res = eval("(" + data + ")");
                        if (res.success == "0") {
                            alert("数据拉取错误");
                        }
                        else {
                            $("#tbTitle").val(res.result.Title);
                            $("#tbRenshu").val(res.result.Renshu);
                            $("#tbRiqi").val(res.result.Date);
                            $("#ddStartHour").val(res.result.BeginHour);
                            $("#ddEndHour").val(res.result.EndHour);
                            $("#tbShanghu").val(res.result.Source);
                            $("#tbJine").val(res.result.Jine);
                            $("#tbSales").val(res.result.SalesName);
                            $("#tbYanhuiting").val(res.result.yanhuiting);
                            $("#ddIsconfirmed").val(res.result.IsConfirmed);
                            $("#ddHascan").val(res.result.Can);
                            $("#tbYishichangdi").val(res.result.YishiChangdi);
                            $("#tbBeizhu").val(res.result.Miaoshu);
                        }
                        $('#exampleModal').modal('show');

                    }

                });
            })
        });

        function ReadModal(ID) {

            $.ajax({
                url: "crudEventList.ashx?",
                async: false,
                data: { "act": "read", "ID": ID, "dt": new Date().getTime() },
                success: function (data) {
                    var res = eval("(" + data + ")");
                    if (res.success == "0") {
                        alert("数据拉取错误");
                    }
                    else {
                        $("#tbTitle").val(res.result.Title);
                        $("#tbRenshu").val(res.result.Renshu);
                        $("#tbRiqi").val(res.result.Date);
                        $("#ddStartHour").val(res.result.BeginHour);
                        $("#ddEndHour").val(res.result.EndHour);
                        $("#tbShanghu").val(res.result.Source);
                        $("#tbJine").val(res.result.Jine);
                        $("#tbSales").val(res.result.SalesName);
                        $("#tbYanhuiting").val(res.result.Yanhuiting);
                        $("#ddIsconfirmed").val(res.result.IsConfirmed);
                        $("#ddHascan").val(res.result.Can);
                        $("#tbYishichangdi").val(res.result.YishiChangdi);
                        $("#tbBeizhu").val(res.result.Miaoshu);
                        $("#hdID").val(ID);
                    }
                    $('#exampleModal').modal('show');

                }

            });
        }

        function Delete(obj) {
            var title = $(obj).parents("tr").find("td:first").text().trim();
            return confirm("确定删除 "+title+" 吗 ？");
        }

        function CheckModal() {
            if ($("#tbTitle").val() == "") {
                alert("请输入主题");
                return false;
            }
            if ($("#tbRiqi").val() == "") {
                alert("请输入日期");
                return false;
            }
            if ($("#ddStartHour").val() > $("#ddEndHour").val()) {
                alert("时间格式错误");
                return false;
            }
        }
   
    </script>
</head>
<body class="theme-blue">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row" id="loading" style="display: none; line-height: 25px; z-index: 999;
        position: fixed; top: 50%; left: 50%;">
        <div class="panel col-lg-12">
            <img src="../images/loading.gif" />
            Loading ...</div>
    </div>
    <!-- HeadLine -->
    <div class="navbar navbar-default" role="navigation">
        <%=head %>
    </div>
    <!-- EndHeadLine -->
    <!-- Left NavBar-->
    <div class="sidebar-nav">
        <%=menu %>
    </div>
    <div class="content">
        <div class="header">
            <div class="stats">
            </div>
            <div class="page-title">
                日历设置&nbsp;
                <div style="float: right" class=" form-inline form-group-sm">
                    <asp:TextBox ID="txtDt" runat="server" class=" form-control  TimeSelect"></asp:TextBox>&nbsp;&nbsp;
                    <asp:LinkButton ID="btSubmit" OnClick="btSubmit_Click" OnClientClick="return checkTxtDt()"
                        class=" btn  btn-primary btn-sm " runat="server">搜索</asp:LinkButton>&nbsp;
                    <div id="btAdd" class=" btn  btn-primary btn-sm ">
                        新增</div>
                    &nbsp;  
                    <asp:HiddenField ID="hdID" runat="server" />
                    &nbsp; <a id="btReturn" class=" btn btn-danger btn-sm " href="Rili.aspx">返回日历</a>&nbsp;
                </div>
            </div>
        </div>
        <div class="main-content">
            <div class="col-sm-12 col-md-12 col-lg-12 col-lg-offset-0">
                <div class="panel panel-default">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <a href="#listUser" class="panel-heading" data-toggle="collapse">活动列表:
                                <asp:Label ID="lbMonth" runat="server" Text="" /></a>
                            <div id="listUser" class="panel-collapse collapse in">
                                <div class=" col-lg-12 table-responsive">
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th>
                                                    活动主题
                                                </th>
                                                <th>
                                                    人数
                                                </th>
                                                <th>
                                                    日期
                                                </th>
                                                <th>
                                                    时间段
                                                </th>
                                                <th>
                                                    使用人/商户
                                                </th>
                                                <th>
                                                    活动金额
                                                </th>
                                                <th>
                                                    销售
                                                </th>
                                                <th>
                                                    宴会厅
                                                </th>
                                                <th>
                                                    仪式场地
                                                </th>
                                                <th>
                                                    是否确定
                                                </th>
                                                <th>
                                                    餐饮
                                                </th>
                                                <th>
                                                    备注
                                                </th>
                                                <th style="width: 3.5em;">
                                                    修改
                                                </th>
                                                <th style="width: 3.5em;">
                                                    删除
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="Repeater1" runat="server"  onitemcommand="Repeater1_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%#Eval("Title")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("Renshu")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("StartDate","{0:yyyy-MM-dd}")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("StartDate","{0:hh:mm}")%>~<%#Eval("EndDate","{0:hh:mm}")%></td>
                                                        <td>
                                                            <%#Eval("Source")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("jine")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("SalesName")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("yanhuiting")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("yishiChangdi")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("Isconfirmed")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("Can")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("Miaoshu")%>
                                                        </td>
                                                        <td>
                                                            <div class="btn btn-link" style="display: inline"  onclick='ReadModal(<%#Eval("ID") %>)'>
                                                                <i class="fa fa-edit"></i>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="btDelete" class="btn btn-link" style="display: inline"  CommandArgument='<%#Eval("ID") %>' OnClientClick="return Delete(this)" CommandName="Delete" runat="server"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btSubmit" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
            aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class=" col-lg-12 ">
                            <div class="form-group  col-lg-6 ">
                                <div class="col-sm-12">
                                    <font color="red">*</font> 活动主题
                                    <asp:TextBox ID="tbTitle" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6">
                                <div class="col-sm-12">
                                    人数
                                    <asp:TextBox ID="tbRenshu" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <div class="col-sm-12">
                                     <font color="red">*</font> 日期
                                    <asp:TextBox ID="tbRiqi" runat="server" class="form-control hourSelect"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6">
                                <div class="col-sm-12">
                                    时间段
                                    <div class="form-horizontal">
                                        <asp:DropDownList ID="ddStartHour" runat="server" class="form-control" Style="display: inline;
                                            width: 80px">
                                        </asp:DropDownList>
                                        &nbsp;-&nbsp;
                                        <asp:DropDownList ID="ddEndHour" runat="server" class="form-control" Style="display: inline;
                                            width: 80px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6">
                                <div class="col-sm-12">
                                    使用人/商户
                                    <asp:TextBox ID="tbShanghu" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6">
                                <div class="col-sm-12">
                                    活动金额
                                    <asp:TextBox ID="tbJine" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6">
                                <div class="col-sm-12">
                                    销售人员
                                    <asp:TextBox ID="tbSales" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6">
                                <div class="col-sm-12">
                                    宴会厅
                                    <asp:TextBox ID="tbYanhuiting" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6">
                                <div class="col-sm-12">
                                    仪式场地
                                    <asp:TextBox ID="tbYishichangdi" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6">
                                <div class="col-sm-12">
                                    是否已确定
                                    <asp:DropDownList ID="ddIsconfirmed" runat="server" class="form-control">
                                        <asp:ListItem>是</asp:ListItem>
                                        <asp:ListItem>否</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6">
                                <div class="col-sm-12">
                                    是否有餐饮
                                    <asp:DropDownList ID="ddHascan" runat="server" class="form-control">
                                        <asp:ListItem>是</asp:ListItem>
                                        <asp:ListItem>否</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group  col-lg-12">
                                <div class="col-sm-12">
                                    备注
                                    <asp:TextBox ID="tbBeizhu" runat="server" class="form-control "></asp:TextBox>
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
                        <asp:Button ID="btEvent" class="btn btn-primary" runat="server" Text="提交" OnClick="btEvent_Click" OnClientClick="return CheckModal()" />
                    </div>
                </div>
            </div>
        </div>
        <footer>
              <%=foot %>
            </footer>
    </div>
    </form>
</body>
</html>
