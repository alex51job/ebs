<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Kehu/Kehu.Master" AutoEventWireup="true" CodeBehind="KehuListAudit.aspx.cs" Inherits="ebs.MoKuai_Kehu.KehuListAudit" %>
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

    function SortByDB(obj) {
        thisValue = $(obj).attr("value")
        $("#MainContent_hdOrderBy").val(thisValue);
        $("#MainContent_hdAsc").val() == "1" ? $("#MainContent_hdAsc").val("0") : $("#MainContent_hdAsc").val("1");
        document.getElementById("MainContent_lbConfirm").click();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">客户审批申请
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="col-sm-12 col-md-12 col-lg-12 col-lg-offset-0">
                    <div class="panel panel-default">
                        <a href="#result" class="panel-heading" data-toggle="collapse">审批记录</a>
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
                    <table class="table table-condensed small">
                      <asp:HiddenField ID="hdOrderBy" Value="ID" runat="server" />
                            <asp:HiddenField ID="hdAsc" Value="1" runat="server" />
                             <asp:LinkButton ID="lbConfirm" runat="server" class="btn btn-primary btn-sm "  style="display:none;"
                                                OnClientClick="showLoad()" onclick="lbConfirm_Click"><i class="fa fa-search"></i> 搜索</asp:LinkButton>
                        <thead>
                            <tr>
                                <th style="width: 1.5em;">操作
                                </th>
                                <th>审批状态</th>
                                 <th>
                                       <div onclick="SortByDB(this)" value="ID">编号</div>
                                </th>
                                <th>
                                       <div onclick="SortByDB(this)" value="Source">渠道</div>
                                </th>
                                <th>
                                       <div onclick="SortByDB(this)" value="SourceNb">渠道编号</div>
                                </th>
                                <th>
                                       <div onclick="SortByDB(this)" value="CreationDate">日期</div>
                                </th>
                                 <th>   <div onclick="SortByDB(this)" value="ZixunDiDian">
                                    咨询地点</div>
                                </th>
                                <th>
                                      <div onclick="SortByDB(this)" value="CustomerName">客户姓名</div>
                                </th>
                                <th>
                                      <div onclick="SortByDB(this)" value="Telephone">客户电话</div>
                                </th>
                                 <th>
                                      <div onclick="SortByDB(this)" value="CustomerType">客户类型</div>
                                </th>
                                <th>
                                   <div onclick="SortByDB(this)" value="Zhuoshu">桌数</div>
                                </th>
                                 <th>
                                      <div onclick="SortByDB(this)" value="EventDate">婚期/活动日期</div>
                                </th>
                               <th>
                                   <div onclick="SortByDB(this)" value="Kefu">接单客服</div>
                               </th>
                               <th>
                                  <div onclick="SortByDB(this)" value="Sales">负责销售</div>
                               </th>
                               <th>
                                  <div onclick="SortByDB(this)" value="Zhuangtai">状态</div>
                               </th>
                               <th>
                                  <div onclick="SortByDB(this)" value="NeedHuiFang">回访</div>
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
                                   <asp:LinkButton ID="lbView" runat="server" CommandArgument='<%#Eval("ID")%>'><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%#Eval("ID")%>'><i class="fa fa-pencil"></i></asp:LinkButton>
                                </td>
                                <td><%#Eval("AuditStatus") %></td>
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
                 
                </Triggers>
            </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
