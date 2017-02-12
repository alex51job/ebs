<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WeddingRequest.aspx.cs" Inherits="ebs.WeddingRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<style type="text/css">
.select2-container
{
    width:100% !important;
    }
</style>
   <script type="text/javascript">

       $(function () {
           MakeDatePicker();
           MakeSelect2();
           checkNewRequestForm();
       });
       function MakeSelect2() {
           // selector for person

           $("#<%=tbYianhuiting.ClientID%>").select2({
               allowClear: true,
               placeholder: ""
           });
       };

    </script>
    <script type="text/javascript">
        function MakeDatePicker() {
            $('#<%=tbHunyanriqi.ClientID%>').datetimepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                todayBtn: true,
                startView: "year",
                //      minView: "month",
                showMeridian: true,
                linkField: "<%=tbkaishi.ClientID %>",
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
            var $chgDesc = $("#MainContent_table_ChgDesc")
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
    <script type="text/javascript">
        function checkNewRequestForm() {
            $('#interviewForm').bootstrapValidator({
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    '<%= tbXinlang.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '新郎不能为空'
                            }
                        }
                    },
                    '<%= tbXinning.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '新娘不能为空'
                            }
                        }
                    }
                   
                            //                            ,
                            //                            emailAddress: {
                            //                                message: '邮件格式错误'
                            //                            }
                        
                    }
                
            });
            $('#<%=lbSubmit.ClientID%>').click(function () {
                showLoad();
                var res = true;

                var validatorObj = $('#interviewForm').data('bootstrapValidator');
                validatorObj.validate();
                res = validatorObj.isValid() && res;
                if (res == false) {
                    document.getElementById('loading').style.display = "none";
                }
                return false;
            });

        };
        function reset() {

            $("#interviewForm")[0].reset();

        }

    </script>
    <script type="text/javascript">
        function digit_uppercase(n) {
            var fraction = ['角', '分'];
            var digit = [
        '零', '壹', '贰', '叁', '肆',
        '伍', '陆', '柒', '捌', '玖'
    ];
            var unit = [
        ['元', '万', '亿'],
        ['', '拾', '佰', '仟']
    ];
            var head = n < 0 ? '欠' : '';
            n = Math.abs(n);
            var s = '';
            for (var i = 0; i < fraction.length; i++) {
                s += (digit[Math.floor(n * 10 * Math.pow(10, i)) % 10] + fraction[i]).replace(/零./, '');
            }
            s = s || '整';
            n = Math.floor(n);
            for (var i = 0; i < unit[0].length && n > 0; i++) {
                var p = '';
                for (var j = 0; j < unit[1].length && n > 0; j++) {
                    p = digit[n % 10] + unit[1][j] + p;
                    n = Math.floor(n / 10);
                }
                s = p.replace(/(零.)*零$/, '')
             .replace(/^$/, '零')
          + unit[0][i] + s;
            }
            return head + s.replace(/(零.)*零元/, '元')
                   .replace(/(零.)+/g, '零')
                   .replace(/^整$/, '零元整');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">
婚宴合同
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
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
                                            <asp:TextBox ID="tbHunyanriqi" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                        <td colspan="2">
                                            仪式开始时间
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="tbkaishi" runat="server" class="form-control"></asp:TextBox>
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
                                            <asp:DropDownList ID="tbYianhuiting" runat="server"  class="form-control" multiple="multiple">
                                            <asp:ListItem>场地A</asp:ListItem>
                                             <asp:ListItem>场地C</asp:ListItem>
                                              <asp:ListItem>场地B</asp:ListItem>
                                            </asp:DropDownList>
                                          
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
//                                $(function () {
//                                    if ($("#hdChgDes").val() != "") {
//                                        var $chgDesc = $("#table_ChgDesc")
//                                        var firstTr = $chgDesc.find("tbody>tr:last");
//                                        var PushTxt = $("#hdChgDes").val();
//                                        var Need_count = PushTxt.split("^").length;
//                                        for (var i = 0; i < Need_count; i++) {
//                                            var rowContent = PushTxt.split("^")[i];
//                                            var row = $("<tr></tr>");
//                                            var td_1 = $("<td></td>");
//                                            var td_2 = $("<td></td>");
//                                            var td_3 = $("<td></td>");
//                                            var td_4 = $("<td></td>");
//                                            var td_5 = $("<td></td>");
//                                            var td_6 = $("<td class='text-center'></td>");
//                                            td_1.append($("<span id='chgDeNb" + row_count + "'>" + row_count + "</span>"));
//                                            td_2.append($("<input id='chgDeA" + row_count + "' class='form-control' value='" + rowContent.split("|")[0] + "' type='text' />")); // "<input id='Text"+row_count+"' class='form-control' type='text' />"
//                                            td_3.append($("<input id='chgDeB" + row_count + "' class='form-control' value='" + rowContent.split("|")[1] + "' type='text' />")); // "<select id='"+Select1+"' class='form-control'> <option></option><option>Y</option><option>N</option></select>"
//                                            // td_4.append($("<select id='chgDeC" + row_count + "' class='form-control'> <option></option><option>Y</option><option>N</option></select>"));
//                                            //td_5.append($("<select id='chgDeD" + row_count + "' class='form-control'> <option></option><option>Y</option><option>N</option></select>"));
//                                            td_6.append($("<div class=' btn btn-default btn-sm' onclick='del(" + row_count + ")'><i class='fa fa-trash-o'  id='" + row_count + "' ></i>&nbsp;Delete</div>")); // <div class=' btn btn-default btn-sm'></div>
//                                            row.append(td_1);
//                                            row.append(td_2);
//                                            row.append(td_3);
//                                            //row.append(td_4);
//                                            //row.append(td_5);
//                                            row.append(td_6);
//                                            $chgDesc.append(row);
//                                            //$("#chgDeC" + row_count).val(rowContent.split("|")[2]);
//                                            //$("#chgDeD" + row_count).val(rowContent.split("|")[3]);
//                                            row_count++;
//                                        }

//                                    }
//                                })
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
                                                    <div class="form-group  col-lg-12">
                                                        <div class="input-group  col-lg-12">
                                                            <div class="input-group-addon">
                                                                (元):</div>
                                                            <input type="text" class="form-control" id="tbTotal" onkeyup="makeBig('tbTotal')">
                                                            <div class="input-group-addon">
                                                                (大写):</div>
                                                            <input type="text" class="form-control"  id="tbTotalUpcase">
                                                        </div>
                                                            <script type="text/javascript">
                                                                function makeBig(param) {
                                                                    var vals = $("#" + param).val();
                                                                    $("#" + param + "Upcase").val(digit_uppercase(vals));

                                                                }
                                                            </script>
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
                        <asp:LinkButton ID="lbSubmit" runat="server" class="btn btn-primary btn-sm" OnClientClick="showLoad()"><i class="fa fa-save"></i>  Submit</asp:LinkButton>
                        <a class="btn btn-danger btn-sm"><i class="fa fa-trash-o"></i>Cancel</a>
                        <asp:HiddenField ID="hdChgDes" runat="server" />
                    </div>
                </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
