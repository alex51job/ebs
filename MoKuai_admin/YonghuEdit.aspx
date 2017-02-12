<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YonghuEdit.aspx.cs" Inherits="ebs.MoKuai_admin.YonghuEdit" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head id="Head1" runat="server">
    <title></title>
  
    <script type="text/javascript">
        function checkNewRequestForm() {
            $('#interviewForm').bootstrapValidator({
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    '<%= txtUserName.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '用户名不能为空'
                            }
                        }
                    },
                    '<%= tbDisplayName.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '显示姓名不能为空'
                            }
                        }
                    },
                    '<%= txtPsd.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '密码不能为空'
                            }
                        }
                    },
                    '<%= txtMail.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '邮件地址不能为空'
                            },
                            emailAddress: {
                                message: '邮件格式错误'
                            }
                        }
                    }
                }
            });

        };

        function reset() {
            $("#interviewForm")[0].reset();
        }
    </script>
    <script type="text/javascript">
        $(function () {
            checkNewRequestForm();
        });
    </script>
</head>
<body>
    <form id="interviewForm" runat="server">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="exampleModalLabel">
            用户</h4>
    </div>
    <div class="modal-body">
        <div class="row">
            <div class="form-group form-group-sm col-lg-12 col-sm-12 col-md-12">
                <label class="col-sm-3 control-label">
                    用户名</label>
                <div class="col-sm-6">
                    <asp:TextBox ID="txtUserName" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group form-group-sm col-lg-12 col-sm-12 col-md-12">
                <label class="col-sm-3 control-label">
                    显示姓名</label>
                <div class="col-sm-6">
                    <asp:TextBox ID="tbDisplayName" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group form-group-sm col-lg-12  col-sm-12 col-md-12">
                <label class="col-sm-3 control-label">
                    密码</label>
                <div class="col-sm-6">
                    <asp:TextBox ID="txtPsd" runat="server" class="form-control" ></asp:TextBox>
                </div>
            </div>
            <div class="form-group form-group-sm col-lg-12  col-sm-12 col-md-12">
                <label class="col-sm-3 control-label">
                    角色</label>
                <div class="col-sm-6">
                    <asp:DropDownList ID="dpRole" CssClass="form-control" runat="server" 
                        DataSourceID="LinqDataSource1" DataTextField="Role" DataValueField="Role">
                    </asp:DropDownList>
                    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
                        ContextTypeName="ebs.dbml.ebsDBData" EntityTypeName="" Select="new (Role, ID)" 
                        TableName="tbSysRole">
                    </asp:LinqDataSource>
                </div>
            </div>
            <div class="form-group form-group-sm col-lg-12  col-sm-12 col-md-12">
                <label class="col-sm-3 control-label">
                    门店</label>
                <div class="col-sm-6">
                    <asp:DropDownList ID="ddRegion" CssClass="form-control" runat="server">
                     <asp:ListItem>一滴水</asp:ListItem>
                        <asp:ListItem>婚礼中心</asp:ListItem>
                         <asp:ListItem>All</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group form-group-sm col-lg-12  col-sm-12 col-md-12">
                <label class="col-sm-3 control-label">
                    电子邮件地址</label>
                <div class="col-sm-6">
                    <asp:TextBox ID="txtMail" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group form-group-sm col-lg-12  col-sm-12 col-md-12">
                <label class="col-sm-3 control-label">
                    联系方式</label>
                <div class="col-sm-6">
                    <asp:TextBox ID="txtPhone" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="modal-footer">
        <button id="btReset" class="btn btn-danger" onclick= "reset()">Reset</button>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" class="btn btn-primary" />
    </div>
    </form>
</body>
</html>
