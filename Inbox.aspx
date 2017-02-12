<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="inbox.aspx.cs" Inherits="ebs.inbox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<script type="text/javascript">
    function SetsPages() {

        document.getElementById('loading').style.display = "block";
    }
    function setActive(index) {
        //alert(index);
        $(".pagination li:eq(" + index + ")").addClass("active").siblings().removeClass("active");
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">
  <h1 class="page-title">
                我的任务</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server" onload="UpdatePanel1_Load">
                <ContentTemplate>
                <div class="row" id="loading" style="display: none; line-height: 25px; z-index: 999;
        position: fixed; top: 50%; left: 50%;">
        <div class="panel col-lg-12">
            <img src="images/loading.gif" />
            Loading ...</div>
    </div>
                    <table class="table">
                        <thead>
                            <tr>
                                <th>
                                    BEO#
                                </th>
                                <th>
                                    客户
                                </th>
                                <th>
                                    联系人
                                </th>
                                <th>
                                    活动名
                                </th>
                                <th>
                                   活动时间
                                </th>
                                <th>
                                   活动场地
                                </th>
                                <th>
                                    状态
                                </th>
                                <th>
                                    创建者
                                </th>
                                <th style="width: 3.5em;">
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                        
                                <asp:Repeater ID="Repeater1" runat="server" 
                                    onitemdatabound="Repeater1_ItemDataBound">
                                <ItemTemplate>
                            <tr>
                                <td>
                                   <%#Eval("BEONumber")%>
                                </td>
                                <td>
                                    <%#Eval("Customer") %>
                                </td>
                                <td>
                                     <%#Eval("Contact")%>
                                </td>
                                <td>
                                    <%#Eval("EventName")%>
                                </td>
                                <td>
                                    <%#Eval("EventDt", "{0:yyyy-MM-dd}")%>
                                </td>
                                 <td>
                                     <%#Eval("Changdi")%>
                                </td>
                                <td>
                                     <%#Eval("Status") %>
                                </td>
                                <td>
                                     <%#Eval("Owner") %>
                                </td>
                                <td>
                                   <asp:LinkButton ID="lbEdit" runat="server" OnClientClick="showLoad()" CommandArgument='<%#Bind("ID") %>'><i class="fa fa-pencil"></i></asp:LinkButton>
                                 <%--  <a href="#myModal" role="button"
                                        data-toggle="modal"><i class="fa fa-trash-o"></i></a>--%>
                                </td>
                            </tr>
                            </ItemTemplate>
                                </asp:Repeater>
                        </tbody>
                    </table>
                    <ul class="pagination pagination-sm " style="margin: 0; padding: 0;">
                        <li runat="server" id="liPla">
                            <asp:LinkButton ID="pla" runat="server" OnClick="pla_Click">
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
                            <asp:LinkButton ID="pra" runat="server" OnClick="pra_Click">
                                                                    <span aria-hidden="true">&raquo;</span>
                                                                  </asp:LinkButton>
                        </li>
                    </ul>
                    <span class="label label-primary pull-right well-sm" style="font-size: 12px;">10 Records/Page|
                        Page Count:<%=absolutePage*5+offsetPage %>/<%=(Convert.ToInt32(RecourdCount / 10) + (RecourdCount % 10 == 0 ? (RecourdCount==0?1:0) : 1)) %></span>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="p1" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="p2" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="p3" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="p4" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="p5" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
