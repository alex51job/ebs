<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pasReset.aspx.cs" Inherits="ebs.Register.pasReset" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
        <meta charset="utf-8" />
           <title><%=projectName%></title>
    <meta content="IE=Edge,chrome=1" http-equiv="X-UA-Compatible" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <!-- Css -->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,700' rel='stylesheet'
        type='text/css' />
    <link href="../Styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../font/font-awesome/css/font-awesome.css" />
    <link rel="stylesheet" type="text/css" href="../styles/theme.css" />
    <link rel="stylesheet" type="text/css" href="../styles/premium.css" />
    
    <!-- Bootstrap -->
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Scripts/highcharts.js" type="text/javascript"></script>
    <script src="../Scripts/modules/exporting.js" type="text/javascript"></script>
    <script src="../Scripts/modules/drilldown.js" type="text/javascript"></script>
    
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
</head>
<body class="theme-2">
    <form id="form1" runat="server">
    <div class="navbar navbar-default" role="navigation">
        <div class="navbar-header">
          <span class="navbar-brand">
       <%--<img class="navbar-left" alt="Brand" src="images/logo.gif"  style=" height:30px;">--%>
             &nbsp;&nbsp;<%=projectName %></span></div>

        <div class="navbar-collapse collapse" style="height: 1px;">

        </div>
      </div>
      <div class="dialog">
    <div class="panel panel-default">
        <p class="panel-heading no-collapse">Reset Your Passward</p>
        <div class="panel-body">
            <div>
                <div class="form-group form-horizontal">
                    <label class="control-label">Login Name :</label>
                   <asp:Label ID="lblUserName" runat="server" Text="Label" ForeColor="Red" Font-Bold="false" ></asp:Label>
                </div>
                <div class="form-group">
                <label  class="control-label">Password</label>
                    <asp:TextBox ID="txtPsd" runat="server" TextMode="Password"  CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPsd" ErrorMessage="*"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                  
                </div>
                <div class="form-group">
                <label  class="control-label">Confirm password</label>
                 <asp:TextBox ID="txtConfirm" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtConfirm" ErrorMessage="*"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
             <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" class="btn btn-primary pull-right" />
           
               
                <div class="clearfix"></div>
           </div>
        </div>
    </div>
   <p class="pull-right">Designed by <a href="mailTo:Alex.xu2@delphi.com" target="_blank" style="font-size: .75em; margin-top: .25em;">Alex's Idea </a></p>
   
</div>
  
    </form>
</body>
</html>
