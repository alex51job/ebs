<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangdiEdit.aspx.cs" Inherits="ebs.MoKuai_admin.ChangdiEdit" %>


  
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
                                message: '场地名不能为空'
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

    <form id="interviewForm" runat="server">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="exampleModalLabel">
            场地</h4>
    </div>
    <div class="modal-body">
        <div class="row">
            <div class="form-group form-group-sm col-lg-12 col-sm-12 col-md-12">
                <label class="col-sm-3 control-label">
                    场地名</label>
                <div class="col-sm-6">
                    <asp:TextBox ID="txtName" runat="server"  class="form-control"></asp:TextBox>
                </div>
            </div>
             <div class="form-group form-group-sm col-lg-12  col-sm-12 col-md-12">
                <label class="col-sm-3 control-label">
                    门店</label>
                <div class="col-sm-6">
                    <asp:DropDownList ID="ddRegion" CssClass="form-control" runat="server">
                     <asp:ListItem>一滴水</asp:ListItem>
                        <asp:ListItem>婚礼中心</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="modal-footer">
        <button id="btReset" class="btn btn-danger" onclick= "reset()">Reset</button>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" class="btn btn-primary" />
    </div>
    </form>

