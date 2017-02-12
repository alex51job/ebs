<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Kehu/Kehu.Master" AutoEventWireup="true"
    CodeBehind="KehuEdit.aspx.cs" Inherits="ebs.MoKuai_Kehu.KehuiEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">

        $(function () {
            checkNewRequestForm();
            makeDate();
           // MakeSelect2();

        });

    </script>
    <script type="text/javascript">
        function makeDate() {
            $(".TimeSelect").datetimepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                todayBtn: true,
                startView: "month",
                minView: "month",

                showMeridian: true
            }).change(function (e) {
                //alert("1");
                if ($(e)[0].target.id == "MainContent_tbHunqi") {
                    $('#MainForm').data('bootstrapValidator').revalidateField('<%= tbHunqi.UniqueID %>');
                }


            });
        }
    </script>
    <script type="text/javascript">
        function MakeSelect2() {
            // selector for person
            $(".muSelect").select2({
                placeholder: "",
                allowClear: true

            });
        };

        function Select2Sel(str) {

            $(".muSelect").val(str.split(",")).trigger("change");
        };
    </script>
    <script type="text/javascript">
        function checkNewRequestForm() {
            $('#MainForm').bootstrapValidator({
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    '<%= ddQudao.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '渠道不能为空'
                            }
                        }
                    },
                  
                    '<%= txtInputDate.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '录入日期不能为空'
                            }
                        }
                    },
                    '<%= ddZixundidian.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '咨询地点不能为空'
                            }
                        }
                    },
                    '<%= tbCustomerName.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '客户名称不能为空'
                            }
                        }
                    },
                    '<%= tbCompany.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '公司名不能为空'
                            }
                        }
                    },
                    '<%= tbZuoji.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '客户电话不能为空'
                            }
                        }
                    },
                    '<%= ddCustomerType.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '客户类型不能为空'
                            }
                        }
                    },
                    '<%= tbHunqi.UniqueID %>': {
                        validators: {
                            notEmpty: {
                                message: '婚期/活动时间不能为空'
                            }
                        }
                    }
                }
            });

        };
        function reset() {

            $("#FormMain")[0].reset();
        }

        function CheckSales() {
            if ($('#<%= ddSales.ClientID %>').val() == null || $('#<%= ddSales.ClientID %>').val() == "") {
                alert("下发必须选择负责销售");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">
    客户信息
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Literal ID="CustomerTabs" runat="server"></asp:Literal>
    <br />
    <div class="col-sm-10 col-md-10 col-lg-10 col-lg-offset-1">
        <!--基本-->
        <div class="panel panel-default">
            <a href="#AInfo" class="panel-heading" data-toggle="collapse">基本信息</a>
            <div id="AInfo" class="panel-collapse panel-body collapse in">
            <small><span style="color:red">*</span>为必填项，选择负责销售前请先选择咨询地点，下发时必须填写负责销售</small>
                <div class="main-content">
                    <div class="row">
                        <div class=" col-lg-12 form-horizontal ">
                            <br>
                            <div class="form-group form-group-sm col-lg-6 col-xs-6">
                                <label class="col-sm-4 control-label">
                                    编号</label>
                                <div class="col-sm-8">
                                    <asp:Label ID="lbID" runat="server" class="form-control" ReadOnly Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                </label>
                                <div class="btn btn-primary" data-toggle="modal" data-target="#myModal">
                                    修改日志</div>
                            </div>
                             <div class="clearfix"></div>
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    客户类型<span style="color:red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddCustomerType" class="form-control" runat="server" OnSelectedIndexChanged="ddCustomerType_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    咨询地点<span style="color:red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddZixundidian" class="form-control" runat="server" OnSelectedIndexChanged="ddZixundidian_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                         <div class="clearfix"></div> 
                          <hr style="border-left:none;border-right:none;border-top:none;border-bottom:1px dashed #cccccc;line-height:5px;margin-top:0px"/>
                           
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    渠道<span style="color:red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddQudao" class="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    渠道编号</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbQudaobianhao" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    录入日期<span style="color:red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtInputDate" runat="server" class="form-control TimeSelect"></asp:TextBox>
                                </div>
                            </div>
                            
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    客户姓名<span style="color:red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbCustomerName" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    公司名<span style="color:red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbCompany" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    客户电话<span style="color:red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbZuoji" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    客户QQ</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbCustomerQQ" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            
                            <div class="form-group form-group-sm col-lg-6 col-xs-6">
                                <label class="col-sm-4 control-label">
                                    桌数</label>
                                <div class="col-sm-8">
                                        <asp:TextBox ID="tbZhuoshu" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-sm col-lg-6 col-xs-6">
                                <label class="col-sm-4 control-label">
                                    婚期/活动日期<span style="color:red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbHunqi" runat="server" class="form-control TimeSelect"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-sm col-lg-6 col-xs-6">
                                <label class="col-sm-4 control-label">
                                    预约到店时间</label>
                                <div class="col-sm-8">
                                            <asp:TextBox ID="tbYuyue" runat="server" class="form-control TimeSelect"></asp:TextBox>
                                </div>
                            </div>
                            
                            <div class="form-group form-group-sm col-lg-6 col-xs-6">
                                <label class="col-sm-4 control-label">
                                    状态</label>
                                <div class="col-sm-8">
                                    <p>
                                        <asp:Label ID="lbZhuangtai" runat="server"  class="form-control" ReadOnly Text=""></asp:Label>
                                    </p>
                                </div>
                            </div>
                            
                            <div class="form-group form-group-sm col-lg-6 col-xs-6">
                                <label class="col-sm-4 control-label">
                                    下发时间</label>
                                <div class="col-sm-8">
                                    <p>
                                        <asp:Label ID="lbXiafa" runat="server"  class="form-control" ReadOnly Text=""></asp:Label>
                                    </p>
                                </div>
                            </div>
                             <div class="clearfix"></div> 
                              <hr style="border-left:none;border-right:none;border-top:none;border-bottom:1px dashed #cccccc;line-height:5px;margin-top:0px"/>
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    接单客服</label>
                                <div class="col-sm-8">
                                    <p>
                                        <asp:Label ID="lbKefu" runat="server" class="form-control-static" Text=""></asp:Label>
                                    </p>
                                </div>
                            </div>
                              <div class="clearfix"></div> 
                            <div class="form-group form-group-sm col-lg-6 col-xs-6">
                                <label class="col-sm-4 control-label">
                                    负责销售<span style="color:red">*</span></label>
                                <div class="col-sm-8">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddSales" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddZixundidian" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddCustomerType" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                              <div class="clearfix"></div>
                            <asp:PlaceHolder ID="PHSWKehu" runat="server">
                              <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    行业</label>
                                     <div class="col-sm-8">
                                    <asp:TextBox ID="tbSWHangye" runat="server" class="form-control"></asp:TextBox>
                                
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    联系人</label>
                             
                                     <div class="col-sm-8">
                                    <asp:TextBox ID="tbSWLianxiren1" runat="server" class="form-control"></asp:TextBox>
                           
                                </div>
                            </div>
                             <div class="clearfix"></div>
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    职位</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbSWZhiwei1" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    座机</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbSWZuoji1" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    手机</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbSWShouji1" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    邮件</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbSWMail1" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                              <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    联系人2</label>
                              
                                     <div class="col-sm-8">
                                    <asp:TextBox ID="tbSWLianxiren2" runat="server" class="form-control"></asp:TextBox>
                                
                                </div>
                            </div>
                             <div class="clearfix"></div>
                              <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    职位</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbSWZhiwei2" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    座机</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbSWZuoji2" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    手机</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbSWShouji2" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    邮件</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbSWMail2" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>


                            <div class="form-group form-group-sm col-lg-6  col-xs-6">
                                <label class="col-sm-4 control-label">
                                    联系地址</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbSWDizhi" runat="server" class="form-control" style="position:absolute; width:150%"></asp:TextBox>
                                </div>
                            </div>
                            </asp:PlaceHolder>
                             <div class="clearfix"></div>
                            <div class="form-group col-lg-12 ">
                                <label class="col-sm-4 ">
                                    渠道备注</label>
                                <div class="col-sm-12">
                                    <asp:TextBox ID="tbQita" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-12 ">
                                <label class="col-sm-4">
                                    客服跟进信息</label>
                                <div class="col-sm-12">
                                    <asp:TextBox ID="tbKefuFollow" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="DivAudit" runat="server">
                                        <div class="form-group  col-lg-12 ">
                                            <label class="col-sm-4">
                                                无效/有效说明</label>
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="tbAuditReason" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group form-group-sm col-lg-3 col-xs-3">
                                            <label class="col-sm-4 control-label">
                                                无效/有效状态：<asp:Label ID="lbAuditStatus" runat="server" CssClass="" Text=""></asp:Label></label>
                                        </div>
                                    </asp:PlaceHolder>
                                    <div class="clearfix">
                                    </div>
                                    <div style="text-align: center">
                                    <asp:Button ID="btSubmit" runat="server" class="btn btn-default" Text="保存" OnClick="btSubmit_Click" Style="margin-right: 15px"/>
                                <asp:Button ID="btXiafa" runat="server" class="btn btn-default" Text="下发" OnClick="btXiafa_Click"  Style="margin-right: 15px"/>
                                <asp:LinkButton ID="btApplyWuxiao" runat="server" class="btn btn-danger" OnClick="btApplyWuxiao_Click">无效申请</asp:LinkButton>
                                        <asp:LinkButton ID="btSubmitApply" runat="server" OnClick="btSubmitApply_Click" class="btn btn-default"
                                            Style="margin-right: 20px">提交申请</asp:LinkButton>
                                        <asp:LinkButton ID="btRedrewApply" runat="server" OnClick="btRedrewApply_Click" class="btn btn-default">撤回申请</asp:LinkButton>
                                          <asp:LinkButton ID="btChongxinXiafa" runat="server" class="btn btn-danger" OnClick="btXiafa_Click" OnClientClick="return CheckSales()" Visible="false" Style="margin-right: 15px">重新下发</asp:LinkButton>
                                        <asp:LinkButton ID="btPass" runat="server" OnClick="btPass_Click" class="btn btn-default"
                                            Style="margin-right: 15px">通过</asp:LinkButton>
                                        <asp:LinkButton ID="btKickback" runat="server" OnClick="btKickback_Click" class="btn btn-default"  Style="margin-right: 15px">退回</asp:LinkButton>
                                         <asp:LinkButton ID="btResetApply" runat="server" class="btn btn-danger"  OnClick="btResetApply_Click">重置有效</asp:LinkButton>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btApplyWuxiao" EventName="Click" />
                                    <asp:PostBackTrigger ControlID="btSubmit"  />
                                      <asp:PostBackTrigger ControlID="btXiafa"  />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h5 class="modal-title" id="myModalLabel">
                        修改记录</h5>
                </div>
                <div class="modal-body">
                    <table class="table table-condensed small">
                        <thead>
                            <tr>
                                <th style="width: 1.5em;">
                                    修改时间
                                </th>
                                <th>
                                    修改人
                                </th>
                                <th>
                                    修改记录
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="Rptlog" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#Eval("ModifyDate","{0:yyyy-MM-dd}") %>
                                        </td>
                                        <td>
                                            <%#Eval("ModifyUser") %>
                                        </td>
                                        <td>
                                            <%#Eval("ModifyRecord") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
