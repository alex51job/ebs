<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaidanEdit.aspx.cs" Inherits="ebs.MoKuai_admin.CaidanEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                    '<%= txtName.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '菜单名不能为空'
                            }
                        }
                    },
                    '<%= txtPrice.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '菜单价格不能为空'
                            },
                            numeric:
                             {
                                 message: '请填写数字'
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
            菜单</h4>
    </div>
    <div class="modal-body">
        <div class="row">
            <div class="form-group form-group-sm col-lg-12 col-sm-12 col-md-12">
                <label class="col-sm-3 control-label">
                    菜单名</label>
                <div class="col-sm-6">
                    <asp:TextBox ID="txtName" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group col-lg-12  col-sm-12 col-md-12">
                <label class="col-sm-3 control-label">
                    价格</label>
                <div class="col-sm-6">
                    <asp:TextBox ID="txtPrice" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group col-lg-12  col-sm-12 col-md-12">
                <label class="col-sm-3 control-label">
                    描述</label>
                <div class="col-sm-6">
                    <asp:TextBox ID="txtBeizhu" runat="server" TextMode="MultiLine" Rows="5" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="modal-footer">
        <button id="btReset" class="btn btn-danger" onclick="reset()">
            Reset</button>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" class="btn btn-primary" />
    </div>
    </form>
</body>
</html>
