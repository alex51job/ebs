<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="ebs.MoKuai_Rili.Import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script type="text/javascript">
          function checkNewRequestForm() {
              $('#FormImport').bootstrapValidator({
                  feedbackIcons: {
                      valid: 'glyphicon glyphicon-ok',
                      invalid: 'glyphicon glyphicon-remove',
                      validating: 'glyphicon glyphicon-refresh'
                  },
                  fields: {
                      '<%= txtYearMonth.UniqueID %>': {
                          validators: {
                              notEmpty: {
                                  message: '场地名不能为空'
                              }
                          }
                      },
                      '<%=FileUpload1.UniqueID %>': {
                          validators: {
                              file: {
                                  extension: 'xlsx',
                                 
                                  message: '错误的文件格式'
                              },
                              notEmpty: {
                                  message: '文件上传不能为空'
                              }
                          }
                      }

                  }
              });

          };

          function reset() {
              $("#interviewForm")[0].reset();
          }
          function makeDate() {
              $(".TimeSelect").datetimepicker({
                  format: 'yyyy-mm',
                  autoclose: true,
                  //todayBtn: true,
                  startView: 3,
                  minView: "year",
                  showMeridian: true
              }).change(function (e) {
                  //alert("1");
                
                      $('#FormImport').data('bootstrapValidator').revalidateField('<%= txtYearMonth.UniqueID %>');
                  


              });
          }
      </script>
    <script type="text/javascript">
        $(function () {
            checkNewRequestForm();
            makeDate();
        });
    </script>
    <script type="text/javascript">
        function CheckResult(obj) {
            var len = obj.length;
            window.open("Rili.htm");
    }
    </script>
</head>
<body>
    <form id="FormImport" runat="server">
    
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
     <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="exampleModalLabel">
            日历导入</h4>
    </div>
    <div class="modal-body">
        <div class="row">
            <div class="form-group  col-lg-12 col-sm-12 col-md-12">
                <label class="col-sm-3 control-label">
                    选择年月</label>
                <div class="col-sm-6">
                    <asp:TextBox ID="txtYearMonth" class="form-control TimeSelect" runat="server"></asp:TextBox>
                 
                </div>
            </div>
            <div class="form-group col-lg-12  col-sm-12 col-md-12">
                <label class="col-sm-3 control-label">
                    浏览</label>
                <div class="col-sm-6">
                    <asp:FileUpload ID="FileUpload1" type="file" class="form-control"  runat="server" />
                   
                </div>
            </div>
             <div class="form-group col-lg-12  col-sm-12 col-md-12">
                <label class="col-sm-8 control-label">
                    清空当月已导入与手动输入的数据后覆盖</label>
                <div class="col-sm-4">
                    <asp:CheckBox ID="ckRemove" Checked runat="server" />
                    
                   
                </div>
            </div>
        </div>
    </div>
    <div class="modal-footer">
      
        <asp:Button ID="btnSave" runat="server" Text="导入" OnClick="btnSave_Click" class="btn btn-primary" />
    </div>
    </form>
</body>
</html>
