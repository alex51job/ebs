<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_admin/admin.Master" AutoEventWireup="true"
    CodeBehind="RiliManager.aspx.cs" Inherits="ebs.MoKuai_admin.RiliManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function makeDate() {
            $(".TimeSelect").datetimepicker({
                format: 'yyyy-mm',
                autoclose: true,
                todayBtn: true,
                startView: "year",
                minView: "year",
                showMeridian: true
            })
        }
        $(function () {
            makeDate();
        })
    </script>
      <script type="text/javascript">
          $(function () {
              $("#btImport").click(
        function () {

            $('#exampleModal').modal({
                remote: "Import.aspx?ts=" + (new Date().getTime())
            });
            $('#exampleModal').modal('show');

        })
          });

   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">
  
            <div class="page-title">
                日历设置&nbsp;
             
                <span class="btn btn-sm btn-danger" id="btImport" style="position:relative">Import</span>
                &nbsp;<a class="btn btn-info btn-sm" href="../MoKuai_Rili/日历导入工具.xlsx">日历导入工具下载</a>
              
                <div style=" float:right" class="form-group-sm" >
                   
                        <asp:TextBox ID="txtDt" runat="server" class="  TimeSelect"></asp:TextBox>&nbsp;&nbsp;
                       <asp:Button ID="btSubmit" class=" btn  btn-primary btn-sm " runat="server" 
                            Text="搜索" onclick="btSubmit_Click" />&nbsp;
                    
               
                </div>
               
            </div>
            
        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  
    <div class="col-sm-12 col-md-12 col-lg-12 col-lg-offset-0">
        <div class="panel panel-default">
            <a href="#listUser" class="panel-heading" data-toggle="collapse">活动列表:   <asp:label id="lbMonth" runat="server" Text="August 2015" /></a> 
            <div id="listUser" class="panel-collapse collapse in">
                <div class=" col-lg-12 table-responsive">
                    <table class="table">
                        <thead>
                            <tr>
                                <th>
                                   在日历上否？
                                </th>
                                <th>
                                    活动名
                                </th>
                                <th>
                                    场地
                                </th>
                                <th>
                                    人数
                                </th>
                                <th>
                                    销售
                                </th>
                                <th>
                                    活动时间
                                </th>
                                <th style="width: 3.5em;">
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
                                            <asp:CheckBox ID="ckShow"  runat="server" /><asp:HiddenField ID="hvID" runat="server" />
                                        </td>
                                        <td>
                                            <%#Eval("Title") %>
                                        </td>
                                        <td>
                                            <%#Eval("Site") %>
                                        </td>
                                        <td>
                                            <%#Eval("Renshu") %>
                                        </td>
                                        <td>
                                            <%#Eval("SalesName") %>
                                        </td>
                                        <td>
                                            <%#Eval("StartDate","{0:yyyy-MM-dd}") %>
                                        </td>
                                        <td>
                                            <%#Eval("Source") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                
            </div>
        </div>
          <asp:Button ID="btSave" class=" btn  btn-primary  " runat="server" Text="保存"  
                    CommandName="Save" onclick="btSave_Click"/>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                
                <div class="modal-body">
                <div class="row" id="Div1" style=" line-height: 25px; z-index: 999;
       ">
        <div class="panel col-lg-12">
            <img src="../images/loading.gif" />
            Loading ...</div>
    </div>
                </div>
                
            </div>
        </div>
    </div>
</asp:Content>
