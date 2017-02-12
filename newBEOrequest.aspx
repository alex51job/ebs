<%@ Page Title="关于我们" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="newBEOrequest.aspx.cs" Inherits="ebs.newBEOrequest" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <script type="text/javascript">
        $(function () {
            //alert("");
            makeDatepicker();
            checkNewRequestForm();
        });
    </script>
    <script type="text/javascript">
        function checkNewRequestForm() {
            $('#interviewForm').bootstrapValidator({
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    '<%= dpCustomer.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '客户不能为空'
                            }
                        }
                    },
                    '<%= dpContact.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '联系人不能为空'
                            }
                        }
                    },
                    'inlineRadioOptions': {
                        validators: {
                            notEmpty: {
                                message: '状态不能为空'
                            }
                        }
                    },
                    '<%= txtName.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '活动名不能为空'

                            }
//                            ,
//                            emailAddress: {
//                                message: '邮件格式错误'
//                            }
                        }
                    }
                }
            });
            $('#<%=lbAdd.ClientID%>').click(function () {
                showLoad();
                var res = true;

                var validatorObj = $('#interviewForm').data('bootstrapValidator');
                validatorObj.validate();
                res = validatorObj.isValid() && res;
                if (res == false) {
                    document.getElementById('loading').style.display = "none";
                }
                return false;
            });

        };
        function reset() {

            $("#interviewForm")[0].reset();
          
        }

    </script>
    <script type="text/javascript">
        function makeDatepicker() {
            $('#<%= txtInputDate.ClientID %>').datetimepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                todayBtn: true,
                startView: "year",
                minView: "month"

            });
            
            $("#<%=txtDueDate.ClientID%>").datetimepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                todayBtn: true,
                startView: "year",
                showMeridian: true,
                minView: "month"

            });
        }
    </script>
</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="titleContent">
        Create New BEO
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div id="">
    <div class="col-sm-10 col-md-10 col-lg-10 col-lg-offset-1">
        <!--基本-->
        <div class="panel panel-default" >
            <a href="#searchBox" class="panel-heading" data-toggle="collapse">基本信息</a>
            <div id="searchBox" class="panel-collapse panel-body collapse in">
                <div class="main-content">
                    <div class="row">
                        <div class=" col-lg-12 form-horizontal ">
                            <br>
                            <div class="form-group form-group-sm col-lg-6 ">
                                <label class="col-sm-3 control-label">
                                    BEO#</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="tbBEONb" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    录入日期</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtInputDate" runat="server" class="form-control"></asp:TextBox>
                                   
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--客户-->
        <div class="panel panel-default">
            <a href="#CustomerInfo" class="panel-heading" data-toggle="collapse">客户信息</a>
            <div id="CustomerInfo" class="panel-collapse panel-body collapse in">
                <div class="main-content">
                    <div class="row">
                        <div class=" col-lg-12 form-horizontal ">
                            <br>
                            <div class="form-group  form-group-sm col-lg-6 ">
                                <label class="col-sm-3 control-label">
                                    客户列表</label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="dpCustomer" runat="server" ForeColor="Blue" AutoPostBack="True"
                                        class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <a href="#myModalCust" data-toggle="modal" class="btn btn-danger btn-sm">&nbsp;&nbsp;Add&nbsp;&nbsp;</a></div>
                            <br style="clear: both" />
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    客户品牌</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtBrand" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    客户名称</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtCustomerName" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    类型</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtType" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    级别</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtLevel" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    行业</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtBusiness" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--联系人-->
        <div class="panel panel-default">
            <a href="#ContactInfo" class="panel-heading" data-toggle="collapse">联系人信息</a>
            <div id="ContactInfo" class="panel-collapse panel-body collapse in">
                <div class="main-content">
                    <div class="row">
                        <div class=" col-lg-12  form-horizontal ">
                            <br>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    联系人</label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="dpContact" runat="server" ForeColor="Blue" AutoPostBack="True"
                                        class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <a href="#myModalContact" data-toggle="modal" class="btn btn-danger btn-sm">&nbsp;&nbsp;Add&nbsp;&nbsp;</a></div>
                            <div class=" clearfix">
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    职位</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtPosition" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    座机</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtTelephone" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    手机</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtMobile" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    邮件</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtMail" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    状态</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtStatus" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    其他信息</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtOthers" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    联系地址</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtAddress" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--活动-->
        <div class="panel panel-default">
            <a href="#EventInfo" class="panel-heading" data-toggle="collapse">活动信息</a>
            <div id="EventInfo" class="panel-collapse panel-body collapse in">
                <div class="main-content">
                    <div class="row">
                        <div class=" col-lg-12  form-horizontal ">
                            <br>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    活动名称</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtName" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class=" clearfix"></div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    活动时间</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtDueDate" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    活动形式</label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="dphuodongxingshi" runat="server" class="form-control">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>Social Event(S)</asp:ListItem>
                                        <asp:ListItem>Cooperate(C)</asp:ListItem>
                                        <asp:ListItem>Wedding(W)</asp:ListItem>
                                        <asp:ListItem>Owner/Government(O/G)</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    场地</label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="dpVenue" runat="server" class="form-control">
                                        <asp:ListItem>VenueA</asp:ListItem>
                                        <asp:ListItem>VenueB</asp:ListItem>
                                        <asp:ListItem>VenueC</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    人数</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtCount" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    预算</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtFee" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    其他信息</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtOtherInfo" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- 状态-->
        <div class="panel panel-default">
            <a href="#EventInfo" class="panel-heading" data-toggle="collapse">活动信息</a>
            <div id="Div1" class="panel-collapse panel-body collapse in">
                <div class="main-content">
                    <div class="row">
                        <div class=" col-lg-12  form-horizontal ">
                            <br>
                            <div class="form-group  form-group-sm col-lg-6">
                                <label class="col-sm-3 control-label">
                                    状态</label>
                                <div class="col-sm-9">
                                    <label class="radio-inline">
                                        <input type="radio" name="inlineRadioOptions" id="inlineRadio1" value="P">
                                        P
                                    </label>
                                    <label class="radio-inline">
                                        <input type="radio" name="inlineRadioOptions" id="inlineRadio2" value="T">
                                        T
                                    </label>
                                    <label class="radio-inline">
                                        <input type="radio" name="inlineRadioOptions" id="inlineRadio3" value="D">
                                        D
                                    </label>
                                    <label class="radio-inline">
                                        <input type="radio" name="inlineRadioOptions" id="Radio1" value="C">
                                        C
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=" col-lg-2 col-lg-offset-5">
        <div class="btn-toolbar list-toolbar center-block ">
            <asp:LinkButton ID="lbAdd" runat="server" class="btn btn-primary btn-sm" 
                OnClick="lbAdd_Click"><i class="fa fa-save"></i> Submit</asp:LinkButton>
            <a class="btn btn-danger btn-sm"><i class="fa fa-trash-o"></i>Cancel</a>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ID="ContentDia" runat="server" ContentPlaceHolderID="dialogContent">
    <!-- 对话框 -->
    <div class="modal  fade" id="myModalCust" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×</button>
                    <h3 class=" modal-title">
                        新建客户</h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="form-group form-group-sm col-lg-12 col-md-12 col-sm-12 ">
                            <label class="col-sm-3 control-label">
                                客户品牌</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-sm col-lg-12 col-md-12 col-sm-12 ">
                            <label class="col-sm-3 control-label">
                                客户名称</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="TextBox2" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-sm col-lg-12 col-md-12 col-sm-12 ">
                            <label class="col-sm-3 control-label">
                                类型</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="dpType" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="1">直客</asp:ListItem>
                                    <asp:ListItem Value="2">公关公司</asp:ListItem>
                                    <asp:ListItem Value="3">旅行社\代理公司</asp:ListItem>
                                    <asp:ListItem Value="4">其他</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group form-group-sm col-lg-12 col-md-12 col-sm-12 ">
                            <label class="col-sm-3 control-label">
                                级别</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="dpLevel" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group form-group-sm col-lg-12 col-md-12 col-sm-12 ">
                            <label class="col-sm-3 control-label">
                                行业</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="dpBusiness" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="1">直客</asp:ListItem>
                                    <asp:ListItem Value="1">化工</asp:ListItem>
                                    <asp:ListItem Value="2">医药</asp:ListItem>
                                    <asp:ListItem Value="3">培训</asp:ListItem>
                                    <asp:ListItem Value="4">快消</asp:ListItem>
                                    <asp:ListItem Value="5">奢侈品</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class=" clearfix">
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">
                        提交</button>
                    
                </div>
            </div>
        </div>
    </div>
    <div class="modal  fade" id="myModalContact" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×</button>
                    <h3 id="H1">
                        新建联系人</h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="form-group form-group-sm col-lg-12 col-md-12 col-sm-12 ">
                            <label class="col-sm-3 control-label">
                                联系人</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtContactor" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-sm col-lg-12 col-md-12 col-sm-12 ">
                            <label class="col-sm-3 control-label">
                                职位</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="TextBox3" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-sm col-lg-12 col-md-12 col-sm-12 ">
                            <label class="col-sm-3 control-label">
                                座机</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="TextBox4" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-sm col-lg-12 col-md-12 col-sm-12 ">
                            <label class="col-sm-3 control-label">
                                手机</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="TextBox5" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-sm col-lg-12 col-md-12 col-sm-12 ">
                            <label class="col-sm-3 control-label">
                                邮件</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="TextBox6" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-sm col-lg-12 col-md-12 col-sm-12 ">
                            <label class="col-sm-3 control-label">
                                状态</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="dpStatus" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="1">active-有具体sales lead</asp:ListItem>
                                    <asp:ListItem Value="2">inactive-没有具体sales lead</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group form-group-sm col-lg-12 col-md-12 col-sm-12 ">
                            <label class="col-sm-3 control-label">
                                列名类型</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="dpLieming" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="1">非列名</asp:ListItem>
                                    <asp:ListItem Value="2">成交列名:销售自己成交过的客户</asp:ListItem>
                                    <asp:ListItem Value="3">分配列名:不是销售自己成交过的客户,经分配成为列名客户</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group form-group-sm col-lg-12 col-md-12 col-sm-12 ">
                            <label class="col-sm-3 control-label">
                                其他信息</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="TextBox7" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-sm col-lg-12 col-md-12 col-sm-12 ">
                            <label class="col-sm-3 control-label">
                                地址邮编</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="TextBox8" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">
                        提交</button>
                    
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="H2">Modal title</h4>
      </div>
      <div class="modal-body">
        ...
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>
</asp:Content>
