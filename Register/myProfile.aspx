<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myProfile.aspx.cs" Inherits="MyProject.Register.myProfile" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>
        <%=projectName%></title>
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
    <link href="../bootstrapValidator/dist/css/bootstrapValidator.min.css" rel="stylesheet"
        type="text/css" />
    <!-- Bootstrap -->
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Scripts/highcharts.js" type="text/javascript"></script>
    <script src="../Scripts/modules/exporting.js" type="text/javascript"></script>
    <script src="../Scripts/modules/drilldown.js" type="text/javascript"></script>
    <script src="../bootstrapValidator/dist/js/bootstrapValidator.min.js" type="text/javascript"></script>
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
        excluded: [':disabled'],
        $(function () {
            valForm1();
            //valForm2();
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

            $("#btSave").click(function () {
                var validatorObj = $('#home').data('bootstrapValidator');
                validatorObj.validate();
                return validatorObj.isValid();
            });
        }
    </script>
    <script type="text/javascript">
        function valForm2() {
            $("#profile").bootstrapValidator({
                excluded: [':disabled'],
                framework: 'bootstrap',
                icon: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    '<%=pw1.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '密码不能为空'
                            }
                        }
                    }
                }

            });
            $('#<%=btPassword.ClientID %>').click(function () {
                var validatorObj = $('#profile').data('bootstrapValidator');
                validatorObj.validate();
                return validatorObj.isValid();


            });
    }
    </script>
</head>
<body class="theme-2">
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row" id="loading" style="display: none; line-height: 25px; z-index: 999;
        position: fixed; top: 50%; left: 50%;">
        <div class="panel col-lg-12">
            <img src="../images/loading.gif" />
            Loading ...</div>
    </div>
    <!-- HeadLine -->
    <div class="navbar navbar-default" role="navigation">
        <%=head %>
    </div>
    <!-- EndHeadLine -->
    <!-- Left NavBar-->
    <div class="sidebar-nav">
        <%=menu %>
    </div>
    <!-- Content -->
    <div class="content" style="">
        <div class="header">
            <h1 class="page-title">
                Create new account</h1>
        </div>
        <div class="main-content ">
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
                                <asp:Label ID="lbRole" runat="server" Text="Label" class="form-control"></asp:Label>
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
                             <asp:Button ID="btPassword" CssClass="btn btn-primary" runat="server"  Text="Change Password" onclick="btPassword_Click"></asp:Button>
                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <footer>
               <%=foot %>
            </footer>
        </div>
    </div>
    </form>
</body>
</html>
