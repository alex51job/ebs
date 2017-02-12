<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_admin/admin.Master" AutoEventWireup="true"
    CodeBehind="YonghuList.aspx.cs" Inherits="ebs.MoKuai_admin.YonghuList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<script type="text/javascript">
    $(function () {
        if (window.location.search.indexOf("already")==1) {
            alert("用户名已经被注册");
        }
        $("#btNewUser").click(
        function () {

            $('#exampleModal').modal({
                remote: "YonghuEdit.aspx?ts=" + (new Date().getTime())
            });
            $('#exampleModal').modal('show');

        })
    });

    function Edit(obj) {
        var s = $(obj).attr("args");
        $('#exampleModal').modal({
            remote: "YonghuEdit.aspx?id="+s+"&ts=" + (new Date().getTime())
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
    用户管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="col-sm-12 col-md-12 col-lg-12 col-lg-offset-0">
    <div class="btn-toolbar list-toolbar">
        <a class="btn btn-primary " id="btNewUser">
            <i class="fa fa-plus"></i>&nbsp;增加新用户</a>
        <div class="btn-group">
        </div>
    </div>
    </div> 
<div class="col-sm-12 col-md-12 col-lg-12 col-lg-offset-0">
            <div class="panel panel-default">
                <a href="#listUser" class="panel-heading" data-toggle="collapse">用户列表</a>
                <div id="listUser" class=" panel-collapse collapse in">
                   <div class=" col-lg-12 table-responsive">
                    <table class="table">
                        <thead>
                            <tr>
                                <th>
                                    用户名
                                </th>
                                <th>
                                    显示名
                                </th>
                                <th>
                                    邮箱地址
                                </th>
                                <th>
                                    联系电话
                                </th>
                                <th>
                                    角色
                                </th>
                                 <th>
                                   门店
                                </th>
                                <th>
                                   创建时间
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
                                   <%#Eval("UserName") %>
                                </td>
                                 <td>
                                    <%#Eval("DisplayName") %>
                                </td>
                                <td>
                                    <%#Eval("UserMail") %>
                                </td>
                                <td>
                                     <%#Eval("Mobile") %>
                                </td>
                                <td>
                                    <%#Eval("Role") %>
                                </td>
                                <td>
                                     <%#Eval("Region") %>
                                </td>
                                <td >
                                    <%#Eval("CreationDate","{0:yyyy-MM-dd}") %>
                                </td>
                                
                                <td>
                             
                                    <a href="#" id="EditUser" args='<%#Eval("ID") %>' onclick="Edit(this)" ><i class="fa fa-pencil"></i></a>
                              
                                </td>
                                 <td>
                                   <asp:PlaceHolder ID="PlaceHolder1"  runat="server" Visible='<%#Eval("Sys").ToString() == "False"%>'>
                                   <a  href="#" role="button"  args='<%#Eval("ID") %>' onclick="Del(this)"><i class="fa fa-trash-o"></i></a>
                                    </asp:PlaceHolder>
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
        
        <p class="error-text"><i class="fa fa-warning modal-icon"></i>该用户下的客户与活动数据将转入系统销售账户，确认删除？</p>
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
