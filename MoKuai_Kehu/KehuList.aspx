<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Kehu/Kehu.Master" AutoEventWireup="true" CodeBehind="KehuList.aspx.cs" Inherits="ebs.MoKuai_Kehu.KehuList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<style type="text/css">
table th
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
    function SetsPages() {

        document.getElementById('loading').style.display = "block";
    }
    function setActive(index) {
        //alert(index);
        $(".pagination li:eq(" + index + ")").addClass("active").siblings().removeClass("active");
    }

    function Sort() {

        var table = document.getElementById("mainTable");
        $("th").click(function () {
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

    function SortByDB(obj) {
        thisValue = $(obj).attr("value")
        $("#MainContent_hdOrderBy").val(thisValue);
        $("#MainContent_hdAsc").val() == "1" ? $("#MainContent_hdAsc").val("0") : $("#MainContent_hdAsc").val("1");
        document.getElementById("MainContent_lbConfirm").click();
    }

</script>
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
         var asc = 1;
         $(function () {
             makeDate();
             //Sort();
         });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">我的客户
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server"> 
<div class="col-sm-12 col-md-12 col-lg-12 col-lg-offset-0">
                    <div class="panel panel-default">
                        <a href="#searchBox" class="panel-heading" data-toggle="collapse">查找与新增</a>
                        <div id="searchBox" class="panel-collapse panel-body ">
                            <div class="main-content">
                                <div class="row">
                                    <div class=" col-lg-12 form-horizontal ">
                                        <br>
                                        <div class="form-group  form-group-sm col-lg-3  col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                渠道</label>
                                            <div class="col-sm-8">
                                                 <asp:DropDownList ID="ddlQudao" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group  form-group-sm col-lg-3  col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                渠道编号</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="tbQudaoBianhao" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                          <div class="form-group  form-group-sm col-lg-3  col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                日期开始</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="tbRiqiStart" runat="server" class="form-control TimeSelect"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group  form-group-sm col-lg-3  col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                日期结束</label>
                                             <div class="col-sm-8">
                                            <asp:TextBox ID="tbRiqiEnd" runat="server" class="form-control TimeSelect"></asp:TextBox>
                                            </div>
                                        </div>
                                          <div class="form-group  form-group-sm col-lg-3  col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                咨询地点</label>
                                            <div class="col-sm-8">
                                               <asp:DropDownList ID="ddlMendian" runat="server" onchange="getProjects()" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                          <div class="form-group  form-group-sm col-lg-3  col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                客户姓名</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="tbCustomerName" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                          <div class="form-group  form-group-sm col-lg-3  col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                客户电话</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="tbMobile" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                          <div class="form-group  form-group-sm col-lg-3  col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                接单客服</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlKefu" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                         
                                        <div class="form-group  form-group-sm col-lg-3  col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                负责销售</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSales" runat="server"  class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group  form-group-sm col-lg-3  col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                状态</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlZhuangtai" runat="server"  class="form-control">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>未下发</asp:ListItem>
                                                <asp:ListItem>已下发</asp:ListItem>
                                                <asp:ListItem>未到店</asp:ListItem>
                                                <asp:ListItem>到店</asp:ListItem>
                                                 <asp:ListItem>未成单</asp:ListItem>
                                                  <asp:ListItem>已成单</asp:ListItem>
                                                   <asp:ListItem>无效/审批中</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group  form-group-sm col-lg-3  col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                回访</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlHuifang" runat="server" class="form-control">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>待回访</asp:ListItem>
                                                <asp:ListItem>已回访</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="form-group  form-group-sm col-lg-12 col-sm-12 col-xs-12" style=" text-align:center">
                                            <asp:LinkButton ID="lbConfirm" runat="server" class="btn btn-primary btn-sm " 
                                                OnClientClick="showLoad()" onclick="lbConfirm_Click"><i class="fa fa-search"></i> 搜索</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="lbReset" runat="server" class="btn btn-primary btn-sm " 
                                                OnClientClick="return resetform()" onclick="lbReset_Click"><i class="fa fa-recycle"></i> 重置</asp:LinkButton>  &nbsp;&nbsp; &nbsp;
                                            <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <a class="btn btn-primary btn-sm" id="btNew" href="KehuEdit.aspx?type=w">
            <i class="fa fa-plus"></i>&nbsp;新增婚宴客户</a>&nbsp;&nbsp;
             <a class="btn btn-primary btn-sm" id="A1" href="KehuEdit.aspx?type=b">
            <i class="fa fa-plus"></i>&nbsp;新增商务客户</a>
             </asp:PlaceHolder>
              <asp:PlaceHolder ID="PlaceHolder2" runat="server">
             <a class="btn btn-primary btn-sm" id="A3" href="KehuEdit.aspx?type=z">
            <i class="fa fa-plus"></i>&nbsp;新增商务客户</a>
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
                        <a href="#result" class="panel-heading" data-toggle="collapse">客户列表</a>
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
    </div><div class=" col-lg-12 table-responsive">
                    <table class="table table-condensed small" id="mainTable">
                        <thead>
                            <asp:HiddenField ID="hdOrderBy" Value="ID" runat="server" />
                            <asp:HiddenField ID="hdAsc" Value="1" runat="server" />
                            <tr>
                                <th style="width: 1.5em;">操作
                                </th>
                                 <th>
                                    <div onclick="SortByDB(this)" value="ID">编号</div>
                                </th>
                                <th>
                                     <div onclick="SortByDB(this)" value="Source">渠道</div>
                                </th>
                                <th> <div onclick="SortByDB(this)" value="SourceNb">
                                    渠道编号</div>
                                </th>
                                <th> <div onclick="SortByDB(this)" value="CreationDate">
                                    日期</div>
                                </th>
                                 <th> <div onclick="SortByDB(this)" value="ZixunDiDian">
                                    咨询地点</div>
                                </th>
                                <th> <div onclick="SortByDB(this)" value="CustomerName">
                                   客户姓名</div>
                                </th>
                                <th> <div onclick="SortByDB(this)" value="Telephone">
                                   客户电话</div>
                                </th>
                                 <th> <div onclick="SortByDB(this)" value="CustomerType">
                                   客户类型</div>
                                </th>
                                <th> <div onclick="SortByDB(this)" value="Zhuoshu">
                                桌数</div>
                                </th>
                                 <th> <div onclick="SortByDB(this)" value="EventDate">
                                   婚期/活动日期</div>
                                </th>
                               <th> <div onclick="SortByDB(this)" value="Kefu">
                                接单客服</div>
                               </th>
                               <th><div onclick="SortByDB(this)" value="Sales">
                               负责销售</div>
                               </th>
                               <th> <div onclick="SortByDB(this)" value="Zhuangtai">
                               状态</div>
                               </th>
                               <th> <div onclick="SortByDB(this)" value="NeedHuiFang">
                               回访</div>
                               </th>
                            </tr>
                        </thead>
                        <tbody>
                        
                                <asp:Repeater ID="Repeater1" runat="server" 
                                    onitemdatabound="Repeater1_ItemDataBound" 
                                    >
                                <ItemTemplate>
                            <tr id="thisTR"  runat="server">
                             <td>
                                   <asp:LinkButton  ID="lbView"  runat="server" CommandArgument='<%#Eval("ID")%>'><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbEdit"  runat="server" CommandArgument='<%#Eval("ID")%>'><i class="fa fa-pencil"></i></asp:LinkButton>
                                </td>
                                <td>
                                   <%#Eval("ID") %>
                                </td>
                                <td>
                                    <%#Eval("Source") %>
                                </td>
                                <td>
                                     <%#Eval("SourceNb") %>
                                </td>
                                <td>
                                     <%#Eval("CreationDate", "{0:yyyy-MM-dd}")%>
                                </td>
                                <td>
                                   <%#Eval("ZixunDiDian")%>
                                </td>
                                  <td>
                                   <%#Eval("CustomerName")%>
                                </td>
                                  <td>
                                   <%#Eval("Telephone")%>
                                </td>
                                  <td>
                                   <%#Eval("CustomerType")%>
                                </td>
                                 <td>
                                   <%#Eval("Zhuoshu")%>
                                </td>
                                  <td>
                                   <%#Eval("EventDate","{0:yyyy-MM-dd}")%>
                                </td>
                                 <td>
                                  <%#ToDisplayName(Eval("Kefu").ToString()) %>
                                </td>
                                 <td>
                     
                                   <%#ToDisplayName(Eval("Sales").ToString()) %>
                                </td>
                                 <td>
                                  <asp:Label ID="lbZhuangtai" runat="server" Text="Label" ></asp:Label>
                                </td>
                                 <td>
                                   <%#Eval("NeedHuiFang")%>
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
                    <small>*客户部：客户数据均为可见，但仅能被接单客服或客服主管修改。 客户回访记录可以被任何客服修改。</small><br />
                    <small>*销售部：销售仅可查看下发给自己的客户数据，销售主管可查看所有客户数据。</small><br />
                    <small>*灰色行：经审批后确认为无效的客户，客服主管可重置。</small>
                </div>
        
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
