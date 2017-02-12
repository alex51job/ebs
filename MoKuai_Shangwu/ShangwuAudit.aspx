<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Shangwu/Shangwu.Master" AutoEventWireup="true" CodeBehind="ShangwuAudit.aspx.cs" Inherits="ebs.MoKuai_Shangwu.ShangwuAudit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<script type="text/javascript">
    $(function () {
        $("#MainContent_btPass").on("click", function () {
            if ($("#MainContent_ddDangqi").val() == "") {
                alert("选择活动档期");
                return false;
            }
        })
    })
  
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">审批信息
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Literal ID="BussinessTabs" runat="server"></asp:Literal>
    <br />
    <div class="col-sm-10 col-md-10 col-lg-10 col-lg-offset-1">
    <div class="panel  panel-info ">
            <a href="#RecordInfo" class="panel-heading" data-toggle="collapse">审批记录</a>
            <div id="RecordInfo" class="panel-collapse  collapse in">
            <div class=" table-responsive">
            <table class="table table-condensed">
                        <tr>
                            <th>
                                审核时间
                            </th>
                            <th>
                                审核人
                            </th>
                            <th>
                                审核级别
                            </th>
                            <th>
                                审核类型
                            </th>
                            <th>
                                审核结果
                            </th>
                            <th>
                                审核意见
                            </th>
                        </tr>
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("FollowDate","{0:yyyy-MM-dd HH:mm:ss}") %>
                                    </td>
                                    <td><%#Eval("UserName") %>
                                    </td>
                                    <td><%#Eval("UserRole") %>
                                    </td>
                                    <td><%#Eval("AuditType") %>
                                    </td>
                                    <td><%#Eval("AuditResult") %>
                                    </td>
                                    <td><%#Eval("Comments") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <hr />
                    <div class="form-group col-lg-12 " style=" text-align:center">
                              <div class="col-lg-8 col-lg-offset-2">
                            <table class="table table-condensed table-bordered text-center"  width="100%">
                                       	<tr>
                                       		<th style=" text-align:center">文件名</th><th style=" text-align:center">上传日期</th><th style=" text-align:center" runat="Server" id="titleAction">操作</th>
                                       	</tr>
                                        
                                    <asp:Repeater ID="RepeaterFile" runat="server" 
                                            onitemcommand="RepeaterFile_ItemCommand" 
                                            onitemdatabound="RepeaterFile_ItemDataBound">
                                    <ItemTemplate>
                                    <tr>
                                        <td>
                                       <a href='<%# Eval("FilePath") %>' target="_blank"><%# Eval("FileName") %></a> 
                                            <asp:HiddenField ID="hdfile" Value='<%# Eval("FileName") %>' runat="server" />
                                        </td>
                                        <td>
                                        <%# Eval("CreatedDt", "{0:yyyy-MM-dd}") %>
                                        </td>
                                        <td runat="Server" id="tdAction">
                                            <asp:LinkButton ID="lbdel" runat="server"  CommandName="delete" Visible="false" CommandArgument='<%# Eval("ID") %>'>删除</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    </asp:Repeater>
                                    </table>
                            <table class="table table-condensed table-bordered text-center" width="100%">
                                        <tr>
                                            <th style="text-align: center">
                                                主题
                                            </th>
                                            <th style="text-align: center">
                                                厅
                                            </th>
                                             <th style="text-align: center">
                                                仪式场地
                                            </th>
                                             <th style="text-align: center">
                                                时间段
                                            </th>
                                             <th style="text-align: center">
                                                使用人
                                            </th>
                                         
                                        </tr>
                                        <asp:Repeater ID="RepeaterRili" runat="server" >
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                     <%# Eval("Title") %>
                                                    </td>
                                                    <td>
                                                       <%# Eval("yanhuiting") %>
                                                    </td>
                                                   <td>
                                                        <%# Eval("yishiChangdi") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("StartDate","{0:yyyy-MM-dd HH:mm}") %> - <%# Eval("EndDate", "{0:yyyy-MM-dd HH:mm}")%>
                                                    </td>
                                                    <td>
                                                         <%# Eval("Source")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    </div>
                                    </div>
            </div>
            </div>
            </div>
    <div class="panel  panel-info " runat="server" id="DivAudit" >
            <a href="#AuditInfo" class="panel-heading" data-toggle="collapse">我的审批</a>
            <div id="AuditInfo" class="panel-collapse panel-body collapse in">
            
                <div class="main-content">
                    <div class="row">
                        <div class=" col-lg-12 form-horizontal ">
                            <br>
                           <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    选择档期</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddDangqi" class="form-control " runat="server">
                                    </asp:DropDownList>
                                  
                                </div>
                            </div>
                          <div class="form-group  col-lg-6 ">
                             <label class="col-sm-4 control-label">
                             上传文件
                                    </label>
                                <div class="col-sm-8">
                                      <asp:FileUpload ID="FileUpload1" CssClass="btn btn-default" runat="server" style="width:200px;display:inline" />&nbsp;&nbsp;<asp:Button ID="btUpload"  CssClass="btn btn-danger" runat="server" style=""
                                        Text="保存" onclick="btUpload_Click" />
                                </div>
                            </div>
                            </asp:PlaceHolder>
                            <div class="clearfix"></div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    审核人</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbShenheRen" runat="server" class="form-control " ReadOnly></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    审核级别</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbShenheLevel" runat="server" class="form-control " ReadOnly></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    审核类型</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbShenheType" runat="server" class="form-control " ReadOnly></asp:TextBox>
                                </div>
                            </div>
                            
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    审核意见</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbShenheYijian" TextMode="MultiLine" Rows="3" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            
                            <div class="clearfix"></div>
                              <div style="text-align: center">
                               <asp:Button ID="btPass" runat="server" class="btn btn-default" Text="通过" 
                                      onclick="btPass_Click" />&nbsp;&nbsp;
                                 <asp:Button ID="btReject" runat="server" class="btn btn-default" Text="退回" 
                                      onclick="btReject_Click" />
                                 </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
