<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ebs._default" %>

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
    <!-- Bootstrap -->
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="Default.js?dt=<%=DateTime.Now.ToFileTime() %>"></script>

    <style type="text/css">
        .navbar-default .navbar-brand, .navbar-default .navbar-brand:hover
        {
            color: #fff;
        }
        
        .modal-lg
        {
            width: 950px;
        }
          . control-label
        {
            white-space:nowrap  !important;
            
        }
        table th
        {
             white-space:nowrap !important;
        }
        .panel ul li .label
        {
            margin-left:50px
        }
        
        .panel
        {
            min-height:250px;
        }
        .divProfile 
        {
            display:block;
            width:100%;
            float:left;
            line-height:30px;
        }
        .divLeft
        {
            width:100px;
            text-align:right;
            display:block;
            float:left;
         }
          .divRight
        {
            width:200px;
            margin-left:25px;
            text-align:left;
            display:block;
              float:left;
        
         }
         .textProfile
         {
             width:250px;
             padding-left:20px;
             display:inline;
             line-height:20px;
             height:15px;
             
          }
        #tableXinxi td
        {
            text-align:center
            }
           
    </style>
    
</head>
<body class=" theme-blue" onload=" getDaiban()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row" id="loading" style="display: none; line-height: 25px; z-index: 999;
        position: fixed; top: 50%; left: 50%;">
        <div class="panel col-lg-12">
            <img src="images/loading.gif" />
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
    <div class="content">
        <div class="header">
            <div class="stats">
        
            </div>
            <h1 class="page-title">总览
                </h1>
        </div>
        <div class="main-content">
            
            <div class="row">
    <div class="col-sm-6 col-md-6">
        <div class="panel panel-default">
            <div class="panel-heading collapse in fa"><h2 style='display:inline'>待办事宜</h2><span class="btn btn-link btn-sm"  id='stopMessage'>已读</span></div>
            <div class="panel-body">
             <ul class="list-group" id="ulMyTask">
              
            </ul>
                <small style=" position:absolute; bottom:0px; margin-bottom:24px">&nbsp;&nbsp;*该面板每10分钟将自动刷新,点击“已读”可停止有新消息提示时的闪烁</small>
                </div>
        </div>
    </div>
    <div class="col-sm-6 col-md-6">
        <div class="panel panel-default">
             <div class="panel-heading collapse in"><h2 style='display:inline'>个人信息</h2><a id="a1"  style='display:inline' href="MoKuai_admin/MyProfile.aspx"><span class='btn btn-link btn-sm'>修改</span></a></div>
                <div class="panel-body">
                <div  class="divProfile"><div class="divLeft">用户名 :</div><div runat="Server"  class="divRight" id="divUsername"></div></div>
                <div class="divProfile"><div class="divLeft">显示名 :</div><div  runat="Server" class="divRight" id="divDisplayname"></div></div>
                <div class="divProfile"><div class="divLeft">电子邮件 :</div><div  runat="Server" class="divRight" id="divMail"></div></div>
                <div class="divProfile"><div class="divLeft">角色 :</div><div  runat="Server" class="divRight" id="divRole"></div></div>
                 <div class="divProfile"><div class="divLeft">门店 :</div><div  runat="Server" class="divRight" id="divRegin"></div></div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
    <div class="col-sm-6 col-md-6">
        <div class="panel panel-default"> 
            <div class="panel-heading no-collapse">
                <h2 style='display:inline'>信息统计</h2>
            </div>
     
                <table  class="table" id="tableXinxi">
                	<tr>
                		<td></td><td colspan=2>本月</td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<td></td><td colspan=2>上月</td>
                	</tr>
                    <tr>
                		<td></td><td>婚宴</td><td>商务</td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>婚宴</td><td>商务</td>
                	</tr>
                    <tr>
                		<td>新客户</td><td>
                        <asp:Label ID="lbCurrent_HL_Customer" runat="server" Text=""></asp:Label>
                        </td><td>
                          <asp:Label ID="lbCurrent_SW_Customer" runat="server" Text=""></asp:Label>
                        </td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>
                            <asp:Label ID="lbLast_HL_Customer" runat="server" Text=""></asp:Label></td><td>
                                <asp:Label ID="lbLast_SW_Customer" runat="server" Text=""></asp:Label></td>
                       
                	</tr>
                    <tr>
                      <td>新订单</td><td>
                       <asp:Label ID="lbCurrent_HL_Order" runat="server" Text=""></asp:Label>
                      </td><td>
                        <asp:Label ID="lbCurrent_SW_Order" runat="server" Text=""></asp:Label>
                      </td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>
                      <asp:Label ID="lbLast_HL_Order" runat="server" Text=""></asp:Label>
                      </td><td>
                        <asp:Label ID="lbLast_SW_Order" runat="server" Text=""></asp:Label>
                      </td>
                    </tr>
                </table>
     
        </div>
    </div>
    <div class="col-sm-6 col-md-6" runat="Server" id="divFukuan">
        <div class="panel panel-default">
             <div class="panel-heading no-collapse">
                <h2 style='display:inline'>收款提醒<%--<small class="text-danger">&nbsp;&nbsp;近10个订单的应付款记录</small>--%></h2>
            </div>
            <div  class="panel-body collapse in">
     <ul class="nav  nav-pills small" role="tablist">
         <asp:Literal ID="ltTabsPay" runat="server"></asp:Literal>
  </ul>
             <div class="tab-content">
              <div role="tabpanel" class='table-responsive tab-pane ' id="hy" style=" height:230px">
                <table class="table table-condensed list small">
              <tbody>
              <tr>
              <th>合同编号</th> <th>婚期</th><th>应收日期</th><th>款项</th><th>剩余金额</th>
              </tr>
                  <asp:Repeater ID="Repeater1" runat="server" >
                  <ItemTemplate>
                  <tr>
                      <td>
                          <a href='MoKuai_Hunli/HunliEdit.aspx?ID=<%#Eval("OrderID").ToString()%>'  id="LinkA" class="LinkA"><p class="title"><%#Eval("ContractID")%></p></a>
                          <a href='MoKuai_Hunli/HunliEditAddedPay.aspx?ID=<%#Eval("OrderID").ToString()%>' id="LinkB" class="LinkB"><p class="title"><%#Eval("ContractID")%></p></a>
                      </td>
                      <td>
                          <p><%#Eval("Hunqi")%></p>
                      </td>
                       <td>
                          <p><%#Eval("YinshouRiqi","{0:yyyy-MM-dd}")%></p>
                      </td>
                       <td>
                          <p><%#Eval("PayType")%></p>
                      </td>
                      <td>
                          <p class="text-danger " style=""><%#Eval("YingshouJine")%></p>
                      </td>
                  </tr> 
                  </ItemTemplate>
                  </asp:Repeater>
              </tbody>
            </table>
            </div>
              <div role="tabpanel" class='table-responsive tab-pane ' id="sw" style=" height:230px">
                <table class="table table-condensed list small">
              <tbody>
              <tr>
              <th>合同编号</th><th>活动日期</th><th>应收日期</th><th>款项类型</th><th>应收金额</th>
              </tr>
                <asp:Repeater ID="Repeater2" runat="server" >
                  <ItemTemplate>
                  <tr>
                      <td>
                         <a href='MoKuai_SHangwu/ShangwuEdit.aspx?ID=<%#Eval("OrderID").ToString()%>'><p class="title"><%#Eval("HetongID")%></p></a>
                      </td>
                      <td>
                          <p><%#Eval("EventDate","{0:yyyy-MM-dd}")%></p>
                      </td>
                      <td>
                          <p><%#Eval("PayDate","{0:yyyy-MM-dd}")%></p>
                      </td>
                       <td>
                          <p><%#Eval("PayType")%></p>
                      </td>
                      <td>
                          <p class="text-danger " style=""><%#Eval("PayAmount")%></p>
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
</div>
            
        </div>
</div>


        <footer>
              <%=foot %>
            </footer>
    </div>
    </form>
</body>
</html>
