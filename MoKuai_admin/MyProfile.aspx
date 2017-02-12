<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_admin/admin.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="ebs.MoKuai_admin.MyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<script type="text/javascript">
    excluded: [':disabled'],
        $(function () {
            valForm1();
           
        });
    
    </script>
    <script type="text/javascript">
        function valForm1() {
            $("#home").bootstrapValidator({
                excluded: [':disabled'],
                framework: 'bootstrap',
                icon: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                }, fields: {
                    '<%=tbUserName.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '用户名不能为空'
                            }
                        }
                    },
                    '<%=tbEmail.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '邮件地址不能为空'
                            },
                            emailAddress: {
                                message: '邮件地址不合法'
                            }

                        }
                    }

                }
            });

        }
    </script>
    <script type="text/javascript">
        function valForm2() {
            var pw1 = $.trim($("#<%=pw1.ClientID %>").val());
            var pw2 = $.trim($("#<%=pw2.ClientID %>").val());
            if (pw1 == "" || pw2 == "") {
                alert("密码不能为空");
                return false;
            }
            if (pw1 != pw2) {
                alert("两次密码不相同");
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">我的档案
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div class="col-sm-12 col-md-12 col-lg-12 col-lg-offset-0">
 <ul class="nav nav-tabs">
                <li class="active"><a href="#home" data-toggle="tab">Profile</a></li>
                <li><a href="#profile" data-toggle="tab">Password</a></li>
            </ul>
 <div class="row">
                <div class="col-md-4">
                    <br>
                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane active in" id="home">
                            <div class="form-group">
                                <label>
                                    用户名</label>
                                <asp:TextBox ID="tbUserName" class="form-control" runat="server"></asp:TextBox>
                            </div>
                             <div class="form-group">
                                <label>
                                    显示名</label>
                                <asp:TextBox ID="tbDisplayName" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    电子邮件</label>
                                <asp:TextBox ID="tbEmail" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    联系电话</label>
                                <asp:TextBox ID="tbPhone" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    角色</label>
                                <asp:Label ID="lbRole" runat="server" Text="Label" class="form-control" disabled></asp:Label>
                            </div>
                             <div class="form-group">
                                <label>
                                    门店</label>
                                 <asp:DropDownList ID="ddMendian"  class="form-control" runat="server">
                                 </asp:DropDownList>
                            </div>
                            <div>
                            <asp:Button ID="btSave" CssClass="btn btn-primary" runat="server" Text="Save" 
                                    onclick="btSave_Click"></asp:Button>
                               
                            </div>
                        </div>
                        <div class="tab-pane fade" id="profile">
                            <div class="form-group">
                                <label>
                                    新密码</label>
                                    <asp:TextBox ID="pw1"  TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                
                            </div>
                            <div class="form-group">
                                <label>
                                   确认新密码</label>
                                     <asp:TextBox ID="pw2" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                
                            </div>
                            <div>
                             <asp:Button ID="btPassword" CssClass="btn btn-primary" runat="server"  Text="Change Password" OnClientClick="return  valForm2()" onclick="btPassword_Click"></asp:Button>
                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
