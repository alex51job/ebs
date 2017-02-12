<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Shangwu/Shangwu.Master"
    AutoEventWireup="true" CodeBehind="upload.aspx.cs" Inherits="ebs.MoKuai_Shangwu.upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-10 col-md-10 col-lg-10 col-lg-offset-1">
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
                             <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    </label>
                                <div class="col-sm-8">
                                  
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
                              <div class="form-group col-lg-12 " style=" text-align:center">
                              <div class="col-lg-8 col-lg-offset-2">
                            <table class="table table-condensed table-bordered text-center"  width="100%">
                                       	<tr>
                                       		<th style=" text-align:center">文件名</th><th style=" text-align:center">上传日期</th>
                                       	</tr>
                                        
                                    <asp:Repeater ID="RepeaterFile" runat="server">
                                    <ItemTemplate>
                                    <tr>
                                        <td>
                                       <a href='<%# Eval("FilePath") %>' target="_blank"><%# Eval("FileName") %></a> 
                                        </td>
                                        <td>
                                        <%# Eval("CreatedDt", "{0:yyyy-mm-dd}") %>
                                        </td>
                                        </tr>
                                    </ItemTemplate>
                                    </asp:Repeater>
                                    </table>
                                    </div>
                                    </div>
                            <div class="clearfix"></div>
                              
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
