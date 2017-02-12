<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_admin/admin.Master" AutoEventWireup="true" CodeBehind="ChangdiList.aspx.cs" Inherits="ebs.MoKuai_admin.ChangdiList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<script type="text/javascript">
    $(function () {
        $("#btNew").click(
        function () {

            $('#exampleModal').modal({
                remote: "ChangdiEdit.aspx?ts=" + (new Date().getTime())
            });
            $('#exampleModal').modal('show');

        })
    });

    function Edit(obj) {
        var s = $(obj).attr("args");
        $('#exampleModal').modal({
            remote: "ChangdiEdit.aspx?id=" + s + "&ts=" + (new Date().getTime())
        });
        $('#exampleModal').modal('show');
    }

    function Del(obj) {
        var s = $(obj).attr("args");
        $('#<%=hdDelID.ClientID %>').val(s);
        $('#DeleteModal').modal('show');
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">
场地管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="col-sm-12 col-md-12 col-lg-12 col-lg-offset-0">
    <div class="btn-toolbar list-toolbar">
        <a class="btn btn-primary " id="btNew">
            <i class="fa fa-plus"></i>&nbsp;增加新场地</a>
        <div class="btn-group">
        </div>
    </div>
    </div> 
<div class="col-sm-12 col-md-12 col-lg-12 col-lg-offset-0">
            <div class="panel panel-default">
                <a href="#listUser" class="panel-heading" data-toggle="collapse">场地列表</a>
                <div id="listUser" class="panel-collapse collapse in">
                   <div class=" col-lg-12 table-responsive">
                    <table class="table">
                        <thead>
                            <tr>
                                <th>
                                    场地名
                                </th>
                                <th>
                                    门店
                                </th>
                                <th style="width: 3.5em;">
                                编辑
                                </th>
                                  <th style="width: 3.5em;">
                                删除
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                        
                                <asp:Repeater ID="Repeater1" runat="server" 
                                   >
                                <ItemTemplate>
                            <tr>
                                <td>
                                   <%#Eval("VenueName")%>
                                </td>
                                <td>
                                    <%#Eval("Region") %>
                                </td>
                                <td>
                             
                                    <a href="#" id="Edit" args='<%#Eval("ID") %>' onclick="Edit(this)" ><i class="fa fa-pencil"></i></a>
                              
                                </td>
                                 <td>
                                   <a  href="#" role="button"  args='<%#Eval("ID") %>' onclick="Del(this)"><i class="fa fa-trash-o"></i></a>
                                </td>
                            </tr>
                            </ItemTemplate>
                                </asp:Repeater>
                        </tbody>
                    </table>
                    </div>
                </div>
            </div>
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
<div class="modal small fade" id="DeleteModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
        <h3 id="myModalLabel">Delete Confirmation</h3>
      </div>
      <div class="modal-body">
        
        <p class="error-text"><i class="fa fa-warning modal-icon"></i>已有使用该场地的商务/婚礼订单，是否确定继续删除？</p>
      </div>
      <div class="modal-footer">
        <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
         <asp:Button ID="btDel" Text="Delete" class="btn btn-danger" runat="server"   onclick="btDel_Click" />
          <asp:HiddenField ID="hdDelID" Value="0" runat="server" />
      
      </div>
    </div>
  </div>
</div>
</asp:Content>
