<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="weddingContract.aspx.cs"
    Inherits="ebs.weddingContract" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>
        <%=projectName%></title>
    <meta content="IE=Edge,chrome=1" http-equiv="X-UA-Compatible" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <!-- Css -->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,700' rel='stylesheet'
        type='text/css' />
    <link href="../Styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="font/font-awesome/css/font-awesome.css" />
    <link rel="stylesheet" type="text/css" href="styles/theme.css" />
    <link rel="stylesheet" type="text/css" href="styles/premium.css" />
    <link href="datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet"
        type="text/css" />
    <!-- Bootstrap -->
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="Scripts/highcharts.js" type="text/javascript"></script>
    <script src="Scripts/modules/exporting.js" type="text/javascript"></script>
    <script src="Scripts/modules/drilldown.js" type="text/javascript"></script>
    <script src="datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <style type="text/css">
        .navbar-default .navbar-brand, .navbar-default .navbar-brand:hover
        {
            color: #fff;
        }
        
        .modal-lg
        {
            width: 950px;
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

        $(function () {
            MakeDatePicker();
            //MakeSelect2();
        });
        function MakeSelect2() {
            // selector for person
            $("#dpCustomer").select2({
                allowClear: true,
                placeholder: "Select one customer"
            });
        };

      
    
    </script>
    <script type="text/javascript">
        function MakeDatePicker() {
            $("#tb1").datetimepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                todayBtn: true,
                startView: "year",
          //      minView: "month",
                showMeridian: true,
                linkField: "tb2",
                linkFormat: "hh:ii P"
            });
//            $("#tb2").datetimepicker({
//                format: 'hh:ii P',
//                autoclose: true,
//                startView: "day",
//                maxView:"day",
//                minView: "hour",
//                viewSelect:"hour",
//                showMeridian:true
//            })
        };
    </script>
    <script type="text/javascript">
        var row_count = 1;    //因为页面已经有一行了，为了和谐，所以直接从2开始。详细见pic
        function addRowChgDesc() {
            var $chgDesc = $("#table_ChgDesc")
            var firstTr = $chgDesc.find("tbody>tr:last");
            var row = $("<tr></tr>");
            var td_1 = $("<td></td>");
            var td_2 = $("<td></td>");
            var td_3 = $("<td></td>");
            var td_4 = $("<td></td>");
            var td_5 = $("<td></td>");
            var td_6 = $("<td class='text-center'></td>");
            td_1.append($("<span id='chgDeNb" + row_count + "'>" + row_count + "</span>"));
            td_2.append($("<input id='chgDeA" + row_count + "' class='form-control' type='text' />")); // "<input id='Text"+row_count+"' class='form-control' type='text' />"
            td_3.append($("<input id='chgDeB" + row_count + "' class='form-control' type='text' />")); // "<select id='"+Select1+"' class='form-control'> <option></option><option>Y</option><option>N</option></select>"
            //td_4.append($("<select id='chgDeC" + row_count + "' class='form-control'> <option></option><option>Y</option><option>N</option></select>"));
            //td_5.append($("<select id='chgDeD" + row_count + "' class='form-control'> <option></option><option>Y</option><option>N</option></select>"));
            td_6.append($("<div class=' btn btn-default btn-sm' onclick='del(" + row_count + ")'><i class='fa fa-trash-o'  id='" + row_count + "' ></i>&nbsp;Delete</div>")); // <div class=' btn btn-default btn-sm'></div>
            row.append(td_1);
            row.append(td_2);
            row.append(td_3);
            //row.append(td_4);
            //row.append(td_5);
            row.append(td_6);
            $chgDesc.append(row);
            row_count++;
        }
        function del(e) {
            //alert(e);
            //获取选中的复选框，然后循环遍历删除//
            var ckbs = $("#" + e + "");
            ckbs.each(function () {
                $(this).parent().parent().parent().remove();
            });
        }
        function pullChgDesc() {
            var txt = "";
            for (var i = 1; i < row_count; i++) {
                if ($("#chgDeNb" + i).text() != "") {
                    txt += $("#chgDeA" + i).val() + "|" + $("#chgDeB" + i).val() + "|" + $("#chgDeC" + i).val() + "|" + $("#chgDeD" + i).val() + "^";
                }
            }
            $("#hdChgDes").val(txt);
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
    <!-- Content -->
    <div class="content" style="">
        <div class="header">
            <h1 class="page-title">
                创建新的婚宴合同</h1>
        </div>
        <div class="main-content ">
            <div class="row">
                <div class="col-sm-10 col-md-10 col-lg-10 col-lg-offset-1">
                    <!--基本-->
                    <div class="panel panel-default">
                        <a href="#basInfo" class="panel-heading" data-toggle="collapse">新人基本信息</a>
                        <div id="basInfo" class="panel-collapse panel-body collapse in">
                            <div class="main-content">
                                <div class="row">
                                    <div class=" col-lg-12 form-horizontal ">
                                        <br>
                                        <div class="form-group form-group-sm col-lg-4 ">
                                            <label class="col-sm-2 control-label">
                                                新郎</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="tbXinlang" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group  form-group-sm col-lg-4">
                                            <label class="col-sm-2 control-label">
                                                手机</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="tbXLshouji" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group  form-group-sm col-lg-4">
                                            <label class="col-sm-3 control-label">
                                                邮寄地址</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="tbXLdizhi" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="form-group form-group-sm col-lg-4 ">
                                            <label class="col-sm-2 control-label">
                                                新娘</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="tbXinning" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group  form-group-sm col-lg-4">
                                            <label class="col-sm-2 control-label">
                                                手机</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="tbXNshouji" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group  form-group-sm col-lg-4">
                                            <label class="col-sm-3 control-label">
                                                邮寄地址</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="tbXNdizhi" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal small fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                                    aria-hidden="true">
                                    <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                    ×</button>
                                                <h3 id="myModalLabel">
                                                    Delete Confirmation</h3>
                                            </div>
                                            <div class="modal-body">
                                                <p class="error-text">
                                                    <i class="fa fa-warning modal-icon"></i>Are you sure you want to delete the user?</p>
                                            </div>
                                            <div class="modal-footer">
                                                <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">
                                                    Search</button>
                                                <button class="btn btn-danger" data-dismiss="modal">
                                                    Delete</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--合同信息-->
                    <div class="panel panel-default">
                        <a href="#CustomerInfo" class="panel-heading" data-toggle="collapse">服务内容及定价</a>
                        <div id="CustomerInfo" class="panel-collapse collapse in">
                            <style type="text/css">
                                table tr td
                                {
                                    vertical-align: middle !important;
                                }
                                table tr th
                                {
                                    vertical-align: middle !important;
                                    text-align: center !important;
                                }
                            </style>
                            <table class="table  table-bordered text-center" id="tbContractPart1">
                                <tbody>
                                    <tr>
                                        <td colspan="2">
                                            婚礼举办日期
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="TextBox6" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                        <td colspan="2">
                                            仪式开始时间
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="TextBox7" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                     <td rowspan="3" style="vertical-align: middle; width:120px;">
                                            仪式
                                        </td>
                                        <td colspan="1">
                                            仪式场地
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="TextBox8" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                          <td rowspan="3" style="vertical-align: middle ;width:120px;">
                                            宴会
                                        </td>
                                          <td colspan="1">
                                            宴会厅
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="TextBox10" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                      
                                    </tr>
                                    <tr>
                                       
                                        <td>
                                            开始时间
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="TextBox9" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                      
                                        
                                        <td>
                                            开始时间
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="TextBox16" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                       
                                    </tr>
                                    <tr>
                                       <td>
                                            结束时间
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="TextBox11" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                        <td>
                                            结束时间
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="TextBox17" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td colspan="2">
                                            所选套餐
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="DropDownList1" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            菜金
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox12" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                        <td>
                                            酒水
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox14" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                        <td>
                                            婚庆
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox13" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                        <td>
                                            婚庆服务费
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox15" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <!--支付-->
                    <div class="panel panel-default">
                        <a href="#PayInfo" class="panel-heading" data-toggle="collapse">联系人信息服务费用及支付方式</a>
                        <div id="PayInfo" class="panel-collapse collapse in">
                            <table class="table  table-bordered text-center">
                                <tr>
                                    <th class=" col-lg-2">
                                        项目
                                    </th>
                                    <th class=" col-lg-3">
                                        支付时间
                                    </th>
                                    <th class=" col-lg-7">
                                        支付金额
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        定金
                                    </td>
                                    <td>
                                        签订合同当日
                                    </td>
                                    <td class="">
                                        <div class="form-inline">
                                            <div class="form-group">
                                                <label class="control-label">
                                                    <b>支付总额的&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b></label>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        %</div>
                                                    <input type="text" class="form-control" style="width: 40px" id="exampleInputAmount">
                                                    <div class="input-group-addon">
                                                        ￥</div>
                                                    <input type="text" class="form-control" style="" id="Text1">
                                                    <div class="input-group-addon">
                                                        大写</div>
                                                    <input type="text" class="form-control" id="Text2">
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        中款
                                    </td>
                                    <td>
                                        婚礼前120天
                                    </td>
                                    <td class="">
                                        <div class="form-inline">
                                            <div class="form-group">
                                                <label class="control-label">
                                                    <b>支付总额的&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b></label>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        %</div>
                                                    <input type="text" class="form-control" style="width: 40px" id="Text3">
                                                    <div class="input-group-addon">
                                                        ￥</div>
                                                    <input type="text" class="form-control" style="" id="Text4">
                                                    <div class="input-group-addon">
                                                        大写</div>
                                                    <input type="text" class="form-control" id="Text5">
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        尾款
                                    </td>
                                    <td>
                                        婚礼前1周
                                    </td>
                                    <td class="">
                                        <div class="form-inline">
                                            <div class="form-group">
                                                <label class="control-label">
                                                    <b>支付总额的&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b></label>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        %</div>
                                                    <input type="text" class="form-control" style="width: 40px" id="Text6">
                                                    <div class="input-group-addon">
                                                        ￥</div>
                                                    <input type="text" class="form-control" style="" id="Text7">
                                                    <div class="input-group-addon">
                                                        大写</div>
                                                    <input type="text" class="form-control" id="Text8">
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <!--其他服务约定-->
                    <div class="panel panel-default">
                        <a href="#EventInfo" class="panel-heading" data-toggle="collapse">其他服务约定</a>
                        <div id="EventInfo" class="panel-collapse  collapse in">
                            <table class="table table-striped table-condensed table-bordered" id="table_ChgDesc"
                                runat="server" style="border-top: 1px dotted #ddd;">
                                <thead>
                                    <tr>
                                        <th style="width: 40px">
                                            Nr.
                                        </th>
                                        <th class="col-sm-2">
                                            类别
                                        </th>
                                        <th>
                                            详细内容
                                        </th>
                                        <%--     <th style="width: 150px">
                                        费用
                                    </th>
                                    <th style="width: 150px">
                                        Displace DCS<br />
                                        with other. Y/N
                                    </th>--%>
                                        <th class=" col-lg-1" style="text-align: left">
                                            <div class=" btn btn-default btn-sm" onclick="addRowChgDesc()">
                                                <i class="fa fa-plus-circle"></i>&nbsp;&nbsp;Add&nbsp;&nbsp;&nbsp;
                                            </div>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                            <script type="text/javascript">
                                $(function () {
                                    if ($("#hdChgDes").val() != "") {
                                        var $chgDesc = $("#table_ChgDesc")
                                        var firstTr = $chgDesc.find("tbody>tr:last");
                                        var PushTxt = $("#hdChgDes").val();
                                        var Need_count = PushTxt.split("^").length;
                                        for (var i = 0; i < Need_count; i++) {
                                            var rowContent = PushTxt.split("^")[i];
                                            var row = $("<tr></tr>");
                                            var td_1 = $("<td></td>");
                                            var td_2 = $("<td></td>");
                                            var td_3 = $("<td></td>");
                                            var td_4 = $("<td></td>");
                                            var td_5 = $("<td></td>");
                                            var td_6 = $("<td class='text-center'></td>");
                                            td_1.append($("<span id='chgDeNb" + row_count + "'>" + row_count + "</span>"));
                                            td_2.append($("<input id='chgDeA" + row_count + "' class='form-control' value='" + rowContent.split("|")[0] + "' type='text' />")); // "<input id='Text"+row_count+"' class='form-control' type='text' />"
                                            td_3.append($("<input id='chgDeB" + row_count + "' class='form-control' value='" + rowContent.split("|")[1] + "' type='text' />")); // "<select id='"+Select1+"' class='form-control'> <option></option><option>Y</option><option>N</option></select>"
                                            // td_4.append($("<select id='chgDeC" + row_count + "' class='form-control'> <option></option><option>Y</option><option>N</option></select>"));
                                            //td_5.append($("<select id='chgDeD" + row_count + "' class='form-control'> <option></option><option>Y</option><option>N</option></select>"));
                                            td_6.append($("<div class=' btn btn-default btn-sm' onclick='del(" + row_count + ")'><i class='fa fa-trash-o'  id='" + row_count + "' ></i>&nbsp;Delete</div>")); // <div class=' btn btn-default btn-sm'></div>
                                            row.append(td_1);
                                            row.append(td_2);
                                            row.append(td_3);
                                            //row.append(td_4);
                                            //row.append(td_5);
                                            row.append(td_6);
                                            $chgDesc.append(row);
                                            //$("#chgDeC" + row_count).val(rowContent.split("|")[2]);
                                            //$("#chgDeD" + row_count).val(rowContent.split("|")[3]);
                                            row_count++;
                                        }

                                    }
                                })
                            </script>
                        </div>
                    </div>
                    <!-- 总金额-->
                    <div class="panel panel-default">
                        <a href="#EventInfo" class="panel-heading" data-toggle="collapse">协议总金额</a>
                        <div id="Div1" class="panel-collapse panel-body collapse in">
                            <div class="main-content">
                                <div class="row">
                                    <div class=" col-lg-12   form-horizontal ">
                                        <div class="form-group  form-group-sm col-lg-12 col-sm-12 col-md-12">
                                            <label class="col-sm-2 control-label">
                                                <b>本协议总金额</b>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                            <div class="col-sm-10">
                                                <div class="form-inline">
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                            <div class="input-group-addon">
                                                                (元):</div>
                                                            <input type="text" class="form-control" id="Text10">
                                                            <div class="input-group-addon">
                                                                (大写):</div>
                                                            <input type="text" class="form-control"  id="Text11">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class=" col-lg-2 col-lg-offset-5">
                    <div class="btn-toolbar list-toolbar center-block ">
                        <asp:LinkButton ID="lbRegion" runat="server" class="btn btn-primary btn-sm" OnClientClick="showLoad()"><i class="fa fa-save"></i>  Submit</asp:LinkButton>
                        <a class="btn btn-danger btn-sm"><i class="fa fa-trash-o"></i>Cancel</a>
                        <asp:HiddenField ID="hdChgDes" runat="server" />
                    </div>
                </div>
            </div>
            <footer>
               <%=foot %>
            </footer>
        </div>
    </div>
    </form>
</body>
</html>
