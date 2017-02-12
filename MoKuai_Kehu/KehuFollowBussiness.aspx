<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Kehu/Kehu.Master" AutoEventWireup="true" CodeBehind="KehuFollowBussiness.aspx.cs" Inherits="ebs.MoKuai_Kehu.KehuFollowBussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<script type="text/javascript">
    function confirmToDelete() {
        if (confirm("确定删除?")) {
            return true;
        }
        return false;
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">商务客户跟进
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 <asp:Literal ID="CustomerTabs" runat="server"></asp:Literal>
    <br />
    <div class="col-sm-10 col-md-10 col-lg-10 col-lg-offset-1">
        <div class="panel panel-default">
            <a href="#SalesFollow" class="panel-heading" data-toggle="collapse">销售部跟踪信息</a>
            <div id="SalesFollow" class="panel-collapse panel-body collapse in">
                <div class="main-content">
                    <div class="row">
                        <div class=" col-lg-12 form-horizontal ">
                            <br>
                            <div class="form-group form-group-sm col-lg-3  col-xs-3">
                                <label class="col-sm-4 control-label">
                                    是否成单：</label>
                                      <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlChengdan" CssClass="form-control" runat="server">
                                        <asp:ListItem>是</asp:ListItem>
                                        <asp:ListItem>否</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group col-lg-12 ">
                                <label class="col-sm-4">
                                    销售反馈信息 ：</label>
                                <div class="col-sm-12">
                                    <asp:TextBox ID="tbFeedback_Sales" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            
   <div style="text-align: center" class="col-sm-12">
            <asp:Button ID="btSubmit_Sales" runat="server" class="btn btn-default" 
                Text="新增" onclick="btSubmit_Sales_Click"  />
         
        </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <a href="#FollowLog" class="panel-heading" data-toggle="collapse">跟踪历史记录</a>
            <div id="FollowLog" class="panel-collapse  collapse in">
                    <table class="table table-condensed">
                        <tr>
                            <th>
                                跟进时间
                            </th>
                            <th>
                                跟进人
                            </th>
                            <th>
                                跟进信息
                            </th>
                            <th>
                                是否成单
                            </th>
                            <th>
                                操作
                            </th>
                        </tr>
                        <asp:Repeater ID="Repeater1" runat="server" 
                            onitemcommand="Repeater1_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("FollowDate","{0:yyyy-MM-dd HH:mm:ss}") %>
                                    </td>
                                    <td><%#Eval("UserName") %>
                                    </td>
                                    <td><%#Eval("Feedback") %>
                                    </td>
                                    <td><%#Eval("IsChengDan") %>
                                    </td>
                                    <td>
                                        <asp:Button ID="btDel" runat="server" Text="删除"  CommandName="delete"   OnClientClick="return confirmToDelete()" CommandArgument='<%#Eval("ID") %>' /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
