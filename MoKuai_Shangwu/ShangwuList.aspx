<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Shangwu/Shangwu.Master" AutoEventWireup="true" CodeBehind="ShangwuList.aspx.cs" Inherits="ebs.MoKuai_Shangwu.ShangwuList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
      table th,td
        {
             white-space:nowrap !important;
        }
        th
        {
            cursor:pointer;
            }
    </style>
    <script type="text/javascript">
    function showLoad() {
        document.getElementById('loading').style.display = "block";
    }
    function hideLoad() {
        document.getElementById('loading').style.display = "none";
    }
    function resetform() {
        showLoad();
        $("#MainForm")[0].reset();
        //document.getElementById('loading').style.display = "none";
    }
    var asc = 1;
    $(function () {
        $(".TimeSelect").datetimepicker({
            format: 'yyyy-mm-dd',
            autoclose: true,
            todayBtn: true,
            startView: "month",
            minView: "month",
            showMeridian: true
        });
        //Sort();
    })
    function SetsPages() {

        document.getElementById('loading').style.display = "block";
    }
    function setActive(index) {
        //alert(index);
        $(".pagination li:eq(" + index + ")").addClass("active").siblings().removeClass("active");
    }
    function SortByDB(obj) {
        thisValue = $(obj).attr("value")
        $("#MainContent_hdOrderBy").val(thisValue);
        $("#MainContent_hdAsc").val() == "1" ? $("#MainContent_hdAsc").val("0") : $("#MainContent_hdAsc").val("1");
        document.getElementById("MainContent_lbConfirm").click();
    }
    function Sort() {

        var table = document.getElementById("mainTable");
        $("#mainTable th").click(function () {
            var rows = table.rows.length;
            //var c = table.rows[0].cells[1].innerHTML;
            var col = $(this).closest("th").index();
            for (i = 1; i < rows - 1; i++) {
                k = i;
                for (j = i + 1; j < rows; j++) {
                    if (asc == 1) {
                        if (table.rows[k].cells[col].innerHTML > table.rows[j].cells[col].innerHTML)
                            k = j;
                    } else {
                        if (table.rows[k].cells[col].innerHTML < table.rows[j].cells[col].innerHTML)
                            k = j;
                    }
                }
                if (k > i) {
                    //                tmp = table.rows[i].cells[1].innerHTML;
                    //                table.rows[i].cells[1].innerHTML = table.rows[k].cells[1].innerHTML;
                    //                table.rows[k].cells[1].innerHTML = tmp;
                    tmp = table.rows[i].innerHTML;
                    table.rows[i].innerHTML = table.rows[k].innerHTML;
                    table.rows[k].innerHTML = tmp;
                }
            }
            asc = (asc == 1 ? 0 : 1);

        });

    }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">商务订单
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-12 col-md-12 col-lg-12 col-lg-offset-0">
                    <div class="panel panel-default">
                        <a href="#searchBox" class="panel-heading" data-toggle="collapse">Search</a>
                        <div id="searchBox" class="panel-collapse panel-body collapse in">
                            <div class="main-content">
                                <div class="row">
                                    <div class=" col-lg-12 form-horizontal small form-group-sm ">
                                        <br>
                                        <div class="form-group  col-lg-3 col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                合同编号</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="tbHetongbianhao" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group  col-lg-3 col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                合同日期开始</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="tbHetongStart" runat="server" class="form-control TimeSelect"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group  col-lg-3 col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                合同日期结束</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="tbHetongEnd" runat="server" class="form-control TimeSelect"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group  col-lg-3 col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                活动地点</label>
                                            <div class="col-sm-8">
                                                  <asp:DropDownList ID="ddlWeddingDidian" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                            <div class="form-group  col-lg-3 col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                活动日期始</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="tbEventStart" runat="server" class="form-control TimeSelect"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group  col-lg-3 col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                活动日期末</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="tbEventEnd" runat="server" class="form-control TimeSelect"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group  col-lg-3 col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                联系人1</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="tbLianxirenA" runat="server" class="form-control "></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group  col-lg-3 col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                联系人2</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="tbLianxirenB" runat="server" class="form-control "></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group  col-lg-3 col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                公司名称</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="tbCompany" runat="server" class="form-control "></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group  col-lg-3 col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                活动名称</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="tbEventName" runat="server" class="form-control "></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group  col-lg-3 col-xs-3">
                                            <label class="col-sm-4 control-label">
                                               活动类型</label>
                                            <div class="col-sm-8">
                                                  <asp:DropDownList ID="ddlEventType" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                        <div class="form-group  col-lg-3 col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                场地</label>
                                            <div class="col-sm-8">
                                                  <asp:DropDownList ID="ddlEventVenue" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                     
                                        <div class="form-group  col-lg-3 col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                状态</label>
                                            <div class="col-sm-8">
                                                  <asp:DropDownList ID="ddlBusinessStatus" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group  col-lg-3 col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                负责销售</label>
                                            <div class="col-sm-8">
                                                  <asp:DropDownList ID="ddlSales" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group  col-lg-3 col-xs-3 hide">
                                            <label class="col-sm-4 control-label">
                                                付款状态</label>
                                            <div class="col-sm-8">
                                                  <asp:DropDownList ID="ddlPayStatus" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div  class="clearfix"></div>
                                        <div style="text-align:center">
                                            <asp:LinkButton ID="lbConfirm" runat="server" class="btn btn-primary btn-sm " 
                                                OnClientClick="showLoad()" onclick="lbConfirm_Click"><i class="fa fa-search"></i> 搜索</asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="lbReset" runat="server" class="btn btn-primary btn-sm " 
                                                OnClientClick="return resetform()" onclick="lbReset_Click"><i class="fa fa-recycle"></i> 重置</asp:LinkButton>&nbsp;
                                            <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                                                 <a class="btn btn-primary btn-sm" href="ShangwuEdit.aspx" id="btNew">
            <i class="fa fa-plus"></i>&nbsp;增加商务订单</a>
            </asp:PlaceHolder>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12 col-lg-12 col-lg-offset-0">
                    <div class="panel panel-default">
                        <a href="#result" class="panel-heading" data-toggle="collapse">Result</a>
                        <div id="result" class="panel-collapse panel-body collapse in">
                            <div class="main-content">
                                <div class=" col-lg-12 table-responsive">
<asp:UpdatePanel ID="UpdatePanel1" runat="server" onload="UpdatePanel1_Load">
                <ContentTemplate>
                <div class="row" id="Div1" style="display: none; line-height: 25px; z-index: 999;
        position: fixed; top: 50%; left: 50%;">
        <div class="panel col-lg-12">
            <img src="images/loading.gif" />
            Loading ...</div>
    </div><div class="table-responsive">
                    <table class="table table-condensed small" id="mainTable">
                        <thead>
                            <asp:HiddenField ID="hdOrderBy" Value="HetongID" runat="server" />
                            <asp:HiddenField ID="hdAsc" Value="1" runat="server" />
                            <tr>
                               <th style="width: 80px;">
                                    操作
                                </th>
                                 <th>
                                     <div onclick="SortByDB(this)" value="HetongID">合同编号</div>
                                </th>
                                <th>
                                     <div onclick="SortByDB(this)" value="HetongDate">合同日期</div>
                                </th>
                                <th>
                                     <div onclick="SortByDB(this)" value="EventVenue">活动地点</div>
                                </th>
                                <th>
                                     <div onclick="SortByDB(this)" value="EventDate">活动时间</div>
                                </th>
                                 <th>
                                     <div onclick="SortByDB(this)" value="Lianxiren1">联系人1</div>
                                </th>
                                <th>
                                    <div onclick="SortByDB(this)" value="Lianxiren2">联系人2</div>
                                </th>
                                <th>
                                    <div onclick="SortByDB(this)" value="Company">公司名称</div>
                                </th>
                                 <th>
                                   <div onclick="SortByDB(this)" value="EventName">活动名称</div>
                                </th>
                                  <th>
                                   <div onclick="SortByDB(this)" value="EventType">活动类型</div>
                                </th>
                                  <th>
                                   <div onclick="SortByDB(this)" value="Huodongxingshi">活动形式</div>
                                </th>
                                <th>
                                    <div onclick="SortByDB(this)" value="Changdi">场地</div>
                                </th>
                                <th>
                                    <div onclick="SortByDB(this)" value="Zongjine">合同总金额</div>
                                </th>
                                <th>
                                   <div onclick="SortByDB(this)" value="Fanyong">返佣金额</div>
                                </th>
                                <th>
                                    <div onclick="SortByDB(this)" value="Yifujine">已付金额</div>
                                </th>
                                <th>
                                   <div onclick="SortByDB(this)" value="Zhuangtai">状态</div>
                                </th>
                                  <th>
                                    <div onclick="SortByDB(this)" value="Sales">负责销售</div>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                        
                                <asp:Repeater ID="Repeater1" runat="server" 
                                    onitemdatabound="Repeater1_ItemDataBound" 
                                    >
                                <ItemTemplate>
                            <tr>
                             <td>
                                   <asp:LinkButton ID="lbView" runat="server"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbAudit" runat="server"><i class="fa fa-legal"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%#Eval("ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                </td>
                                <td>
                                   <%#Eval("HetongID") %>
                                </td>
                                <td>
                                     <%#Eval("HetongDate","{0:yyyy-MM-dd}") %>
                                </td>
                                <td>
                                   <%#Eval("EventVenue")%>
                                </td>
                                <td>
                                  <%#Eval("EventDate","{0:yyyy-MM-dd HH:mm}") %>
                                </td>
                                <td>
                                    <%#Eval("Lianxiren1")%>
                                </td>
                                <td>
                                    <%#Eval("Lianxiren2")%>
                                </td>
                                 <td>
                                  <%#Eval("Company") %>
                                </td>
                                  <td>
                                  <%#Eval("EventName") %>
                                </td>
                                <td>
                                 <%#Eval("EventType") %>
                                 </td>
                                 <td>
                                  <asp:Literal ID="lbEventFormat" runat="server"></asp:Literal>
                                 </td>
                                  <td>
                                  <asp:Literal ID="lbVenue" runat="server"></asp:Literal>
                                </td>
                                <td>
                                 <asp:Literal ID="lbZongjine" runat="server"></asp:Literal>
                                </td>
                                  <td>
                                 <%#Eval("Fanyong") %> <asp:Literal ID="lbFanyongPer" runat="server"></asp:Literal>
                                </td>
                                 <td>
                                     <asp:Literal ID="lbYifujine" runat="server"></asp:Literal>
                                </td>
                                  <td>
                                    <asp:Literal ID="lbZhuangtai" runat="server"></asp:Literal>
                                </td>
                                  <td>
                               <%#ToDisplayName(Eval("Sales").ToString())%>
                                </td>
                            </tr>
                            </ItemTemplate>
                                </asp:Repeater>
                        </tbody>
                    </table></div>
                    <ul class="pagination pagination-sm " style="margin: 0; padding: 0;">
                        <li runat="server" id="liPla">
                            <asp:LinkButton ID="pla" runat="server" OnClick="pla_Click" OnClientClick="SetsPages()">
                                                                    <span aria-hidden="true">&laquo;</span>
                            </asp:LinkButton>
                        </li>
                        <li runat="server" id="li1" class="">
                            <asp:LinkButton ID="p1" runat="server" OnClick="p1_Click" OnClientClick="SetsPages()">1</asp:LinkButton></li>
                        <li runat="server" id="li2" class="">
                            <asp:LinkButton ID="p2" runat="server" OnClick="p2_Click" OnClientClick="SetsPages()">2</asp:LinkButton></li>
                        <li runat="server" id="li3" class="">
                            <asp:LinkButton ID="p3" runat="server" OnClick="p3_Click" OnClientClick="SetsPages()">3</asp:LinkButton></li>
                        <li runat="server" id="li4" class="">
                            <asp:LinkButton ID="p4" runat="server" OnClick="p4_Click" OnClientClick="SetsPages()">4</asp:LinkButton></li>
                        <li runat="server" id="li5" class="">
                            <asp:LinkButton ID="p5" runat="server" OnClick="p5_Click" OnClientClick="SetsPages()">5</asp:LinkButton></li>
                        <li runat="server" id="liPra">
                            <asp:LinkButton ID="pra" runat="server" OnClick="pra_Click" OnClientClick="SetsPages()">
                                                                    <span aria-hidden="true">&raquo;</span>
                                                                  </asp:LinkButton>
                        </li>
                    </ul>
                    <span class="label label-primary pull-right well-sm" style="font-size: 12px;"><%=PageSize.ToString() %> 条/页 |
                         当前页:<%=absolutePage*5+offsetPage %>/<%=(Convert.ToInt32(RecourdCount / PageSize) + (RecourdCount % PageSize == 0 ? (RecourdCount == 0 ? 1 : 0) : 1))%></span>
                </ContentTemplate>
                <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="pla" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="p1" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="p2" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="p3" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="p4" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="p5" EventName="Click" />
                     <asp:AsyncPostBackTrigger ControlID="pra" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lbConfirm" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lbReset" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                     <small>*销售部：销售仅可编辑自己创建的订单</small><br />
                     <small>*其他部：总经理、文员、财务均可查看所有订单</small><br />
                     <small>*订单状态：<i class="fa fa-eye"></i> 不可编辑&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-edit"></i> 可编辑&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-legal"></i> 需审批</small>&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-money"></i> 填写支付信息（仅文员）</small><br />
                </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
