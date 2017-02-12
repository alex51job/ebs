<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ebs.Login" %>
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
        <meta charset="utf-8" />
    <title>Event Booking System</title>
    <meta content="IE=Edge,chrome=1" http-equiv="X-UA-Compatible" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <!-- Css -->
   
    <link href="Styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="font/font-awesome/css/font-awesome.css" />
    <link rel="stylesheet" type="text/css" href="styles/theme.css" />
    <link rel="stylesheet" type="text/css" href="styles/premium.css" />
    <link href="bootstrapValidator/dist/css/bootstrapValidator.min.css" rel="stylesheet"
        type="text/css" />
    <!-- Bootstrap -->
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="Scripts/highcharts.js" type="text/javascript"></script>
    <script src="Scripts/modules/exporting.js" type="text/javascript"></script>
    <script src="Scripts/modules/drilldown.js" type="text/javascript"></script>
    <script src="bootstrapValidator/dist/js/bootstrapValidator.min.js" type="text/javascript"></script>
    <style type="text/css">
            .navbar-default .navbar-brand, .navbar-default .navbar-brand:hover
        {
            color: #fff;
        }
        
        .modal-lg
        {
            width: 950px;
        }
    </style>
    <script type="text/javascript">

        $(function () {
            $("#valForm").bootstrapValidator({
                framework: 'bootstrap',
                icon: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                }, fields: {
                    '<%=txtMail.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: 'The Login ID is required'
                            },
                            emailAddress: {
                                message: 'The value is not a valid email address'
                            }

                        }

                    }

                }
            });

            document.onkeydown = function (e) {
                var ev = document.all ? window.event : e;
                if (ev.keyCode == 13) {

                    document.getElementById('lbLogin').click(); //处理事件

                }
            }
        });
    
    </script>
</head>
<body class="theme-blue">
    <form id="valForm" runat="server">
       <div class="navbar navbar-default" role="navigation">
        <div class="navbar-header">
          <span class="navbar-brand">
       <%--<img class="navbar-left" alt="Brand" src="images/logo.gif"  style=" height:30px;">--%>
             &nbsp;&nbsp;Event Booking System</span></div>

        <div class="navbar-collapse collapse" style="height: 1px;">

        </div>
      </div>
    


        <div class="dialog">
    <div class="panel panel-default">
        <p class="panel-heading no-collapse">Sign In</p>
        <div class="panel-body">
            <div>
                <div class="form-group">
                    <label>用户名</label>
                    <asp:TextBox  runat="server" class="form-control span12"  id="username" ></asp:TextBox>
                </div>
                <div class="form-group">
                <label>密码</label>
                    <asp:TextBox  TextMode="Password" runat="server" class="form-control span12" id="txtpsd" ></asp:TextBox>
                  
                </div>
                <asp:LinkButton ID="lbLogin" class="btn btn-primary pull-right" runat="server"  OnClick="lbLogin_Click"
                    >Sign In</asp:LinkButton>

           
                <label class="remember-me"><input type="checkbox" id="ipRem" runat="server" > Remember me</label>
                <div class="clearfix"></div>
           </div>
        </div>
    </div>
   <p class="pull-right">Designed by <a href="mailTo:Alex.xu2@delphi.com" target="_blank" style="font-size: .75em; margin-top: .25em;">Alex's Idea </a></p>
    <p><a href="#PwRes" data-toggle="modal" >Forgot your password?</a></p>
</div>
<div class="modal fade " id="PwRes">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title">Forget You Password ?</h4>
      </div>
      <div class="modal-body">
        <div class="form-group "> <label class=" control-label ">
        please input your mail address :</label>
        <div class="">
                <asp:TextBox ID="txtMail" runat="server" class="form-control"></asp:TextBox>
                
            </div>
           <div class="clearfix"></div>    
      </div>
      <div class="modal-footer">
      <asp:Button ID="btnSend"
                    runat="server" Text="Send"  class="btn btn-default" onclick="btnSend_Click" />
      </div>
    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->
    </form>
</body>
</html>
