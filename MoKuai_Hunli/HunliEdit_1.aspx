<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Hunli/Hunli.Master" AutoEventWireup="true" CodeBehind="HunliEdit_1.aspx.cs" Inherits="ebs.MoKuai_Hunli.HunliEdit_1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
  <style type="text/css">
  table td
        {
             white-space:nowrap !important;
        }
    </style>
<script type="text/javascript">

    $(function () {
        MakeDatePicker();
        MakeSelect2();
        checkNewRequestForm();
        //Select2Sel("");
    });
    function MakeSelect2() {
        // selector for person
        $(".muSelect").select2({
            placeholder:"",
            allowClear: true
           
        });
    };


    function Select2Sel(str) {
       
        $(".muSelect").val(str.split(",")).trigger("change");
    };

      
    
    </script>
    <script type="text/javascript">
        function checkNewRequestForm() {
            $('#MainForm').bootstrapValidator({
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    '<%= ddKehu.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '请先选择一个客户'
                            }
                        }
                    },
                    '<%= tbHetongbianhao.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '合同编号不能为空'
                            }
                        }
                    },
                    '<%= tbHetongriqi.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '合同日期不能为空'
                            }
                        }
                    },
                    '<%= tbXinlang.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '新郎不能为空'
                            }
                        }
                    },
                    '<%= tbXLshouji.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '新郎手机不能为空'
                            }
                        }
                    },
                    '<%= tbXLdizhi.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '新郎邮寄地址不能为空'
                            }
                        }
                    },
                    '<%= tbXinning.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '新娘不能为空'
                            }

                        }
                    },
                    '<%= tbXNshouji.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '新娘手机不能为空'
                            }
                        }
                    },
                    '<%= tbXNdizhi.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '新娘地址不能为空'
                            }

                        }
                    },
                    '<%= tbXNdizhi.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '新娘地址不能为空'
                            }

                        }
                    },
                    '<%= tbStartDate.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 婚礼举办日期不能为空'
                            }

                        }
                    },
                    '<%= ddYishichangdi.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 仪式场地不能为空'
                            }

                        }
                    },
                  
                    '<%= tbYishiKaishi.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 开始时间不能为空'
                            }

                        }
                    },
                    '<%= tbYanhuiKaishi.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 开始时间不能为空'
                            }

                        }
                    },
                    '<%= tbYishijieshu.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 结束时间不能为空'
                            }

                        }
                    },
                    '<%= tbYanhuiJieshu.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 结束时间不能为空'
                            },
                            regexp: {
                                regexp: "^(2[0-3]|[01]?[0-9]):([0-5]?[0-9])$",
                                message: '结束日期应符合 时时：分分 格式'
                            }

                        }
                    },
                    '<%= ddMenu.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 所选套餐不能为空'
                            }

                        }
                    },
                    '<%= tbCaijin.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 菜金不能为空'
                            }

                        }
                    },
                    '<%= tbJiushui.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 酒水不能为空'
                            }

                        }
                    },
                    '<%= tbHunqing.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 婚庆不能为空'
                            }

                        }
                    },
                    '<%= tbFuwufei.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 婚庆服务费不能为空'
                            }

                        }
                    },

                    '<%= tbPay1Percent.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 百分比不能为空'
                            },
                            numeric:
                            {
                                message: "百分比只能为数字"
                            }

                        }
                    },
                    '<%= tbPay1Amount.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 支付金额不能为空'
                            },
                            numeric:
                            {
                                message: "支付金额只能为数字"
                            }

                        }
                    },

                    '<%= tbPay2Percent.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 百分比不能为空'
                            },
                            numeric:
                            {
                                message: "百分比只能为数字"
                            }

                        }
                    },
                    '<%= tbPay2Amount.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 支付金额不能为空'
                            },
                            numeric:
                            {
                                message: "支付金额只能为数字"
                            }

                        }
                    },

                    '<%= tbPay3Percent.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 百分比不能为空'
                            },
                            numeric:
                            {
                                message: "百分比只能为数字"
                            }

                        }
                    },
                    '<%= tbPay3Amount.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 支付金额不能为空'
                            },
                            numeric:
                            {
                                message: "支付金额只能为数字"
                            }

                        }
                    },
                    '<%= tbZongAmount.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: ' 总金额不能为空'
                            },

                            numeric:
                            {
                                message: "总金额只能为数字"
                            }
                        }
                    }
                }
            });

        };
    </script>
    <script type="text/javascript">
        function MakeDatePicker() {
            $("#MainContent_tbStartDate").datetimepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                todayBtn: true,
                startView: "year",
                //      minView: "month",
                showMeridian: true,
                linkField: "MainContent_tbStartDate2",
                linkFormat: "hh:ii"
            }).change(function (e) {
                $('#MainForm').data('bootstrapValidator').revalidateField('<%= tbStartDate.UniqueID %>');



            }); ;

            $(".hourSelect").datetimepicker({
                format: 'hh:ii',
                autoclose: true,
                todayBtn: true,
                startView: "day",
                //      minView: "month",
                showMeridian: true

            }).change(function (e) {
                switch (this.id) {
                    case "MainContent_tbYishiKaishi":
                        $('#MainForm').data('bootstrapValidator').revalidateField('<%= tbYishiKaishi.UniqueID %>');
                        break;
                    case "MainContent_tbYanhuiKaishi":
                        $('#MainForm').data('bootstrapValidator').revalidateField('<%= tbYanhuiKaishi.UniqueID %>');
                        break;
                    case "MainContent_tbYishijieshu":
                        $('#MainForm').data('bootstrapValidator').revalidateField('<%= tbYishijieshu.UniqueID %>');
                        break;
                    case "MainContent_tbYanhuiJieshu":
                        $('#MainForm').data('bootstrapValidator').revalidateField('<%= tbYanhuiJieshu.UniqueID %>');
                        break;
                    default:
                        break;
                }
              


            }); 


            $(".RiSelect").datetimepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                todayBtn: true,
                startView: "month",
                minView: "day",
                showMeridian: true

            }).change(function (e) {
                $('#MainForm').data('bootstrapValidator').revalidateField('<%= tbHetongriqi.UniqueID %>');
            }); ;
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
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">婚礼
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="col-sm-12 col-md-12 col-lg-12 ">
        <ul class="nav nav-tabs">
         <li role="presentation" class="active"><a href="#">订单信息</a></li>
        <%if (KehuID != 0)
              {
                  %>
            <li role="presentation"><a href='<%= ResolveUrl("~/MoKuai_Kehu/KehuiEdit.aspx?HL="+ID+"&ID="+KehuID) %>'>客户信息</a></li>
            <li role="presentation"><a href="<%= ResolveUrl("~/MoKuai_Kehu/KehuFollow.aspx?HL="+ID+"&ID="+KehuID) %>">跟进信息</a></li>
                  <%
              } %>
        </ul>
    </div>
    <div class="clearfix"></div>
    <br />
    <div class="col-sm-10 col-md-10 col-lg-10 col-lg-offset-1">
        <!-- 选择客户 -->
         <div class="panel panel-default">
        <a href="#KehuInfo" class="panel-heading" data-toggle="collapse">客户信息</a>
        <div id="KehuInfo" class="panel-collapse panel-body collapse in">
            <div class="main-content">
                <div class="row">
                    <div class=" col-lg-12 form-horizontal ">
                        <br>
                        <div class="form-group form-group-sm col-lg-6 ">
                                            <label class="col-sm-4 control-label">
                                                客户&联系人</label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList ID="ddKehu" runat="server" class="form-control">
                                             </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                        <div class="form-group form-group-sm col-lg-6 ">
                                            <label class="col-sm-4 control-label">
                                                合同编号</label>
                                         <div class="col-sm-8">
                                             <asp:TextBox ID="tbHetongbianhao" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group form-group-sm col-lg-6 ">
                                            <label class="col-sm-4 control-label">
                                                合同日期</label>
                                         <div class="col-sm-8">
                                             <asp:TextBox ID="tbHetongriqi" runat="server" class="form-control RiSelect"></asp:TextBox>
                                            </div>
                                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
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
                            <div class="table-responsive">
                            <table class="table  table-bordered text-center" id="tbContractPart1">
                                <tbody>
                                    <tr>
                                        <td colspan="2">
                                            婚礼举办日期
                                        </td>
                                        <td colspan="2">
                                         <div class="form-group">
                                            <asp:TextBox ID="tbStartDate" runat="server" class="form-control"></asp:TextBox></div>
                                        </td>
                                        <td colspan="2">
                                            仪式开始时间
                                        </td>
                                        <td colspan="2">  
                                            <asp:TextBox ID="tbStartDate2" runat="server" class="form-control " ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                     <td rowspan="3" style="vertical-align: middle; width:120px;">
                                            仪式
                                        </td>
                                        <td colspan="1">
                                            仪式场地
                                        </td>
                                        <td colspan="2"> <div class="form-group">
                                            <asp:DropDownList ID="ddYishichangdi" runat="server"  class="form-control " >
                                            </asp:DropDownList> </div>
                                        </td>
                                          <td rowspan="3" style="vertical-align: middle ;width:120px;">
                                            宴会
                                        </td>
                                          <td colspan="1">
                                            宴会厅
                                        </td>
                                        <td colspan="2">
                                              <div class="form-group">
                                                <asp:DropDownList ID="ddYanhuiting" runat="server"  class="form-control muSelect" multiple="multiple">
                                            </asp:DropDownList> </div>
                                        </td>
                                      
                                    </tr>
                                    <tr>
                                       
                                        <td>
                                            开始时间
                                        </td>
                                        <td colspan="2">
                                          <div class="form-group">
                                            <asp:TextBox ID="tbYishiKaishi" runat="server" class="form-control hourSelect"></asp:TextBox></div>
                                        </td>
                                      
                                        
                                        <td>
                                            开始时间
                                        </td>
                                        <td colspan="2">  <div class="form-group">
                                            <asp:TextBox ID="tbYanhuiKaishi" runat="server" class="form-control hourSelect"></asp:TextBox></div>
                                        </td>
                                       
                                    </tr>
                                    <tr>
                                       <td>
                                            结束时间
                                        </td>
                                        <td colspan="2">  <div class="form-group">
                                            <asp:TextBox ID="tbYishijieshu" runat="server" class="form-control hourSelect"></asp:TextBox></div>
                                        </td>
                                        <td>
                                            结束时间
                                        </td>
                                        <td colspan="2">  <div class="form-group">
                                            <asp:TextBox ID="tbYanhuiJieshu" runat="server" class="form-control hourSelect"></asp:TextBox></div>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td colspan="2">
                                            所选套餐
                                        </td>
                                        <td colspan="2">  <div class="form-group">
                                            <asp:DropDownList ID="ddMenu" runat="server" class="form-control">
                                            </asp:DropDownList></div>
                                        </td>
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            菜金
                                        </td>
                                        <td>  <div class="form-group">
                                            <asp:TextBox ID="tbCaijin" runat="server" class="form-control"></asp:TextBox></div>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;酒水&nbsp;&nbsp;
                                        </td>
                                        <td>  <div class="form-group">
                                            <asp:TextBox ID="tbJiushui" runat="server" class="form-control"></asp:TextBox></div>
                                        </td>
                                        <td>
                                            婚庆
                                        </td>
                                        <td><div class="form-group">
                                            <asp:TextBox ID="tbHunqing" runat="server" class="form-control"></asp:TextBox></div>
                                        </td>
                                        <td>
                                            婚庆服务费
                                        </td>
                                        <td><div class="form-group">
                                            <asp:TextBox ID="tbFuwufei" runat="server" class="form-control"></asp:TextBox></div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            </div>
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
                                                            <asp:TextBox ID="tbZongAmount" runat="server" class="form-control"  style="min-width:200px"  onkeyup="makeBig(this,'tbZongdaxie')"></asp:TextBox>
                                                            
                                                            <div class="input-group-addon">
                                                                (大写):</div>
                                                                 <asp:TextBox ID="tbZongdaxie" runat="server" class="form-control disabled"   style="min-width:200px" readonly></asp:TextBox>
                                                           
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
                    <!--支付-->
                    <div class="panel panel-default">
                        <a href="#PayInfo" class="panel-heading" data-toggle="collapse">联系人信息服务费用及支付方式</a>
                        <div id="PayInfo" class="panel-collapse collapse in">
                         <div class="table-responsive">
                            <table class="table  table-bordered text-center">
                                <tr>
                                    <th class=" col-lg-2">
                                        项目
                                    </th>
                                    <th class=" col-lg-2">
                                        支付时间
                                    </th>
                                    <th class=" col-lg-8">
                                        支付金额(支付总额的)
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
                                            <div class="form-group  ">
                                              
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        %</div>
                                                    <asp:TextBox ID="tbPay1Percent" runat="server"  class="form-control" style="width: 45px" onkeyup="calAmount(this,'tbPay1Amount','tbPay1Daxie')"></asp:TextBox>
                                                    <div class="input-group-addon">
                                                        ￥</div>
                                                    <asp:TextBox ID="tbPay1Amount" runat="server"  class="form-control" style="min-width:150px"  onkeyup="makeBig(this,'tbPay1Daxie')"></asp:TextBox>
                                                    
                                                    <div class="input-group-addon">
                                                        大写</div>
                                                           <asp:TextBox ID="tbPay1Daxie" runat="server"  class="form-control disabled" style=" min-width:200px" readonly></asp:TextBox>
                                                    
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
                                              
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        %</div>
                                                    <asp:TextBox ID="tbPay2Percent" runat="server"  class="form-control" style="width: 45px"  onkeyup="calAmount(this,'tbPay2Amount','tbPay2Daxie')"></asp:TextBox>
                                                    <div class="input-group-addon">
                                                        ￥</div>
                                                    <asp:TextBox ID="tbPay2Amount" runat="server"  class="form-control"   style="min-width:150px" onkeyup="makeBig(this,'tbPay2Daxie')"></asp:TextBox>
                                                    
                                                    <div class="input-group-addon">
                                                        大写</div>
                                                           <asp:TextBox ID="tbPay2Daxie" runat="server"  class="form-control"   style="min-width:200px" readonly ></asp:TextBox>
                                                    
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
                                              
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        %</div>
                                                    <asp:TextBox ID="tbPay3Percent" runat="server"  class="form-control" style="width: 45px"  onkeyup="calAmount(this,'tbPay3Amount','tbPay3Daxie')"></asp:TextBox>
                                                    <div class="input-group-addon">
                                                        ￥</div>
                                                    <asp:TextBox ID="tbPay3Amount" runat="server"  class="form-control"  style="min-width:150px" onkeyup="makeBig(this,'tbPay3Daxie')"></asp:TextBox>
                                                    
                                                    <div class="input-group-addon">
                                                        大写</div>
                                                           <asp:TextBox ID="tbPay3Daxie" runat="server"  class="form-control disabled"   style="min-width:200px"   readonly></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            </div>
                        </div>
                    </div>
                    <!--其他服务约定-->
                    <div class="panel panel-default">
                        <a href="#EventInfo" class="panel-heading" data-toggle="collapse">其他服务约定</a>
                        <div id="EventInfo" class="panel-collapse  collapse in">
                          <div class="main-content">
                                <div class="row">
                           <div class=" col-lg-12  ">
                                        <br>
                                        <div class="form-group col-lg-12">
                                            <label class="col-sm-1  control-label ">
                                                其他信息</label>
                                            <div class="col-sm-11">
                                                <asp:TextBox ID="tbOthers" runat="server" TextMode="MultiLine"  Rows="5" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        </div>
                                        </div>
                                        </div>
                        </div>
                    </div>
                   
        
          <div  style=" text-align:center">
        <asp:Button ID="btSubmit" runat="server" class="btn btn-default" Text="Submit" onclick="btSubmit_Click" 
             /> <asp:HiddenField ID="hdChgDes" runat="server" />
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
  <script type="text/javascript">
      function makeBig(obj,param) {
          var vals = $(obj).val();
          $("#MainContent_"+param).val(digit_uppercase(vals));

      }
      function makeBigByPercent(param1, param2) {
         

      }
      function calAmount(obj, param1,param2) {
          var percent = $(obj).val();
          var bigAmount = $("#MainContent_tbZongAmount").val();
          if (bigAmount != null) {
              var vals = bigAmount * percent / 100;
            $("#MainContent_" + param1).val(vals);
            $("#MainContent_" + param2).val(digit_uppercase(vals));
            switch (param1) {
                case "tbPay1Amount":
                    $('#MainForm').data('bootstrapValidator').revalidateField('<%= tbPay1Amount.UniqueID %>');
                    break;
                case "tbPay2Amount":
                    $('#MainForm').data('bootstrapValidator').revalidateField('<%= tbPay2Amount.UniqueID %>');
                    break;
                case "tbPay3Amount":
                    $('#MainForm').data('bootstrapValidator').revalidateField('<%= tbPay3Amount.UniqueID %>');
                    break;
                default:
                    break;
            }
            
          }

      }
  </script>
</asp:Content>
