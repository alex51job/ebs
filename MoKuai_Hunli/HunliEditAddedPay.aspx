<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Hunli/Hunli.Master" AutoEventWireup="true"
    CodeBehind="HunliEditAddedPay.aspx.cs" Inherits="ebs.MoKuai_Hunli.HunliAudit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../tokenfield/css/bootstrap-tokenfield.min.css" rel="stylesheet" type="text/css" />
    <script src="../tokenfield/bootstrap-tokenfield.min.js" type="text/javascript"></script>
    <script src="hlScripts.js?d=<%=DateTime.Now.ToFileTime() %>" type="text/javascript"></script>
    <style type="text/css">
        .likeLabel
        {
            border: none;
            border-bottom: 1px solid #cccccc;
            box-shadow: none;
            border-radius: 0;
            background-color: transparent !important;
            cursor: default !important;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("input[type=text]").attr("readonly", "readonly");
            $("#AuditInfo input[type=text]").removeAttr("readonly");
            $("#ExpayInfo input[type=text]").removeAttr("readonly");
            ListenTextboxsOnExPay(); 
            $('.tokenfield').tokenfield();
        })

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">
    婚礼订单审核
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Literal ID="WeddingTabs" runat="server"></asp:Literal>
    <br />
    <div class="col-sm-10 col-md-10 col-lg-10 col-lg-offset-1">
        <!-- 选择客户 -->
        <div class="panel panel-default">
            <a href="#KehuInfo" class="panel-heading" data-toggle="collapse">订单信息</a>
            <div id="KehuInfo" class="panel-collapse panel-body collapse in">
                <div class="main-content">
                    <div class="row">
                        <div class=" col-lg-12 form-horizontal ">
                            <br>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    客/电/销/地</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="ddKehu" runat="server" class="form-control likeLabel"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                                状态</label>
                                         <div class="col-sm-4">
                                             <asp:TextBox ID="tbZhuangtai"  runat="server" class="form-control likeLabel" style="display:inline" ></asp:TextBox>
                                            </div>&nbsp;
                                              <label class="control-label btn btn-link" id="lbRevision">
                                               历史版本<span id="spanNumRevision"></span></label>
                                               <div class="hide" id='divRevision'>
                                               <ul class='list-group' id='ulRevision'></ul>
                                         </div>
                            </div>
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    婚礼地点</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="ddHunliDidian" runat="server" class="form-control likeLabel"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    负责销售</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="ddSales" runat="server" class="form-control likeLabel"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    合同编号</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbHetongBianhao" runat="server" class="form-control likeLabel"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    合同日期</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbHetongRiqi" runat="server" class="form-control likeLabel"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    新郎姓名</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbXinLangName" runat="server" class="form-control likeLabel"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    新郎手机</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbXinLangShouji" runat="server" class="form-control likeLabel"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    新娘名称</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbXinNiangName" runat="server" class="form-control likeLabel"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    新娘手机</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbXinNiangShouji" runat="server" class="form-control likeLabel"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    婚礼日期</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbHunliRiqi" runat="server" class="form-control likeLabel"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    仪式场地</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="ddYishiChangdi" runat="server" class="form-control likeLabel"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    宴会厅</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="ddYanhuiting" runat="server" class="form-control likeLabel"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    午/晚宴</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="ddWuWanCan" runat="server" class="form-control likeLabel"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    婚礼套餐</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="ddHunliTaocan" runat="server" class="form-control likeLabel"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    渠道</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbQudao" runat="server" class="form-control likeLabel"></asp:TextBox>
                                </div>
                            </div>
                           
                            <div class="clearfix">
                            </div>
                             <div id="divDateLevel" class="label label-primary col-lg-10 col-lg-offset-1" style="display:none;text-align:center; font-size:13px; line-height:15px"></div>   
                            <div class=" col-lg-12 table-responsive">
                                <table class="table" style="min-width: 980px; margin-bottom: 0px">
                                    <tr >
                                        <td>
                                            <div class="form-group  form-inline  col-lg-12  ">
                                                <div class="col-sm-12">
                                                    <div style="width: 150px; float: left">
                                                        菜金单价：<asp:Label ID="lbCaijinDanjia" runat="server" Text="" Style="width: 100px"></asp:Label></div>
                                                    <div style="width: 150px; float: left">
                                                        菜金桌数：<asp:TextBox ID="tbCaijinZhuoshu" runat="server" Style="width: 30px; border-left: none;
                                                            border-top: none; border-right: none; border-bottom: 1px solid gray"></asp:TextBox></div>
                                                    <div style="width: 150px; float: left">
                                                        菜金总金额：<asp:Label ID="lbCaijinZongjine" runat="server" Text="" Style="width: 100px"></asp:Label></div>
                                                    <div style="width: 150px; float: left">
                                                        菜金折扣：<asp:TextBox ID="tbCaijinZhekou" runat="server" Style="width: 50px; border-left: none;
                                                            border-top: none; border-right: none; border-bottom: 1px solid gray"></asp:TextBox></div>
                                                    <div style="width: 150px; float: left">
                                                        菜金折扣率：<asp:Label ID="lbCaijinZhekoulv" runat="server" Text="" Style="width: 100px"></asp:Label></div>
                                                    <div style="width: 150px; float: left; padding-left: 14px">
                                                        折后金额：<asp:Label ID="lbCaijinZhehoujine" runat="server" Text="" Style="width: 100px"></asp:Label></div>
                                                </div>
                                            </div>
                                            <div class="form-group  form-inline  col-lg-12 ">
                                                <div class="col-sm-12">
                                                    <div style="width: 150px; float: left">
                                                        酒水单价：<asp:TextBox ID="tbJiushuiDanjia" runat="server" Style="width: 60px; border-left: none;
                                                            border-top: none; border-right: none; border-bottom: 1px solid gray"></asp:TextBox></div>
                                                    <div style="width: 150px; float: left">
                                                        酒水桌数：<asp:TextBox ID="tbJiushuiZhuoshu" runat="server" Style="width: 30px; border-left: none;
                                                            border-top: none; border-right: none; border-bottom: 1px solid gray"></asp:TextBox></div>
                                                    <div style="width: 150px; float: left">
                                                        酒水总金额：<asp:Label ID="lbJiushuiZongjine" runat="server" Text="" Style="width: 100px"></asp:Label></div>
                                                    <div style="width: 150px; float: left">
                                                        酒水折扣：<asp:TextBox ID="tbJiushuiZhekou" runat="server" Style="width: 50px; border-left: none;
                                                            border-top: none; border-right: none; border-bottom: 1px solid gray"></asp:TextBox></div>
                                                    <div style="width: 150px; float: left">
                                                        酒水折扣率：<asp:Label ID="lbJiushuiZHekoulv" runat="server" Text="" Style="width: 100px"></asp:Label></div>
                                                    <div style="width: 150px; float: left; padding-left: 14px">
                                                        折后金额：<asp:Label ID="lbJiushuiZhehoujine" runat="server" Text="" Style="width: 100px"></asp:Label></div>
                                                </div>
                                            </div>
                                            <div class="form-group  form-inline  col-lg-12 ">
                                                <div class="col-sm-12">
                                                    <div style="width: 300px; float: left; visibility: hidden">
                                                        &nbsp;</div>
                                                    <div style="width: 150px; float: left; padding-left: 28px">
                                                        总金额：<asp:Label ID="lbZongjine" runat="server" Text="" Style="width: 100px"></asp:Label></div>
                                                    <div style="width: 150px; float: left; padding-left: 14px">
                                                        总折扣：<asp:Label ID="lbZongZhekou" runat="server" Text="" Style="width: 100px"></asp:Label></div>
                                                    <div style="width: 150px; float: left; padding-left: 14px">
                                                        总折扣率：<asp:Label ID="lbZongZhekoulv" runat="server" Text="" Style="width: 100px"></asp:Label></div>
                                                    <div style="width: 150px; float: left">
                                                        折后总金额：<asp:Label ID="lbZhehouZongjine" runat="server" Text="" Style="width: 100px"></asp:Label></div>
                                                </div>
                                            </div>
                                            <div class="form-group  form-inline  col-lg-12 ">
                                                <div class="col-sm-12">
                                                    <div style="width: 300px; float: left;">
                                                        婚庆套餐：
                                                        <asp:TextBox ID="ddHunqinTaocan" runat="server" Style="width: 200px"></asp:TextBox>
                                                    </div>
                                                    <div style="width: 150px; float: left; padding-left: 28px">
                                                        婚庆：<asp:TextBox ID="tbHunqin" runat="server" Style="width: 50px; border-left: none;
                                                            border-top: none; border-right: none; border-bottom: 1px solid gray"></asp:TextBox></div>
                                                    <div style="width: 150px; float: left; padding-left: 28px">
                                                        桌花：<asp:TextBox ID="tbZhuohua" runat="server" Style="width: 50px; border-left: none;
                                                            border-top: none; border-right: none; border-bottom: 1px solid gray"></asp:TextBox></div>
                                                    <div style="width: 150px; float: left; padding-left: 42px">
                                                        其他：<asp:TextBox ID="tbQita" runat="server" Style="width: 50px; border-left: none;
                                                            border-top: none; border-right: none; border-bottom: 1px solid gray"></asp:TextBox></div>
                                                </div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                            <hr />
                                            <div class="form-group  form-inline  col-lg-12 ">
                                                <div class="col-sm-12">
                                                    <div style="">
                                                        婚宴总金额：<asp:Label ID="lbHunyanZongjine" runat="server" Text="" Style="width: 100px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;大写：<asp:Label
                                                            ID="lbDaxieHunyanZongjine" runat="server" Text="" Style="width: 100px"></asp:Label></div>
                                                    <br>
                                                     <asp:HiddenField ID="hdHunyanZongjine" runat="server" />
                                                     <asp:HiddenField ID="hdStandPrice" Value='0' runat="server" />
                                                    <div style="">
                                                        婚庆总金额：<asp:Label ID="lbHunqinZongjine" runat="server" Text="" Style="width: 100px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;大写：<asp:Label
                                                            ID="lbDaxieHunqinZongjine" runat="server" Text="" Style="width: 100px"></asp:Label></div>
                                                    <br>
                                                    <div style="">
                                                        协议总金额：<asp:Label ID="lbXieyiZongjine" runat="server" Text="" Style="width: 100px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;大写：<asp:Label
                                                            ID="lbDaxieXieyiZongjine" runat="server" Text="" Style="width: 100px"></asp:Label></div>
                                                    <br />
                                                    <div id="smallAlert"></div>
                                                    <hr />
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="table-responsive">
                                <table class="table table-bordered " style="text-align: center;">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center;">
                                                项目
                                            </th>
                                            <th style="text-align: center;">
                                                支付时间
                                            </th>
                                            <th style="text-align: center;">
                                                支付金额
                                            </th>
                                            <th style="text-align: center;">
                                                金额大写
                                            </th>
                                            <th style="text-align: center;">
                                                已付金额
                                            </th>
                                        </tr>
                                    </thead>
                                    <tr style="vertical-align: middle">
                                        <td style="vertical-align: middle">
                                            定金
                                        </td>
                                        <td style="vertical-align: middle">
                                            签订合同当日<asp:TextBox ID="tbFirstPayDate" runat="server" class="RiSelect" Style="width: 100px;
                                                border-left: none; border-top: none; border-right: none; border-bottom: 1px solid gray;
                                                text-align: center;"></asp:TextBox>
                                        </td>
                                        <td style="vertical-align: middle">
                                            婚宴%<asp:TextBox ID="tbFirstPayBaiHY" runat="server" Style="width: 100px; border-left: none;
                                                border-top: none; border-right: none; border-bottom: 1px solid gray; text-align: center;"></asp:TextBox>
                                            金额
                                            <asp:TextBox ID="tbFirstPayJineHY" runat="server" Style="width: 100px; border-left: none;
                                                border-top: none; border-right: none; border-bottom: 1px solid gray; text-align: center;"></asp:TextBox>
                                            <br />
                                            <br />
                                            婚庆%<asp:TextBox ID="tbFirstPayBaiHQ" runat="server" Style="width: 100px; border-left: none;
                                                border-top: none; border-right: none; border-bottom: 1px solid gray; text-align: center;"></asp:TextBox>
                                            金额
                                            <asp:TextBox ID="tbFirstPayJineHQ" runat="server" Style="width: 100px; border-left: none;
                                                border-top: none; border-right: none; border-bottom: 1px solid gray; text-align: center;"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbDaixieFirstpayHY" runat="server" Text=""></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="lbDaixieFirstpayHQ" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="vertical-align: middle">
                                            婚宴<asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                                            <br />
                                            <br />
                                            婚庆<asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: middle">
                                        <td style="vertical-align: middle">
                                            中款
                                        </td>
                                        <td style="vertical-align: middle">
                                            婚礼前120日<asp:TextBox ID="tbSecondPayDate" class="RiSelect" runat="server" Style="width: 100px;
                                                border-left: none; border-top: none; border-right: none; border-bottom: 1px solid gray;
                                                text-align: center;"></asp:TextBox>
                                        </td>
                                        <td style="vertical-align: middle">
                                            婚宴%<asp:TextBox ID="tbSecondPayBaiHY" runat="server" Style="width: 100px; border-left: none;
                                                border-top: none; border-right: none; border-bottom: 1px solid gray; text-align: center;"></asp:TextBox>
                                            金额
                                            <asp:TextBox ID="tbSecondPayJineHY" runat="server" Style="width: 100px; border-left: none;
                                                border-top: none; border-right: none; border-bottom: 1px solid gray; text-align: center;"></asp:TextBox>
                                            <br />
                                            <br />
                                            婚庆%<asp:TextBox ID="tbSecondPayBaiHQ" runat="server" Style="width: 100px; border-left: none;
                                                border-top: none; border-right: none; border-bottom: 1px solid gray; text-align: center;"></asp:TextBox>
                                            金额
                                            <asp:TextBox ID="tbSecondPayJineHQ" runat="server" Style="width: 100px; border-left: none;
                                                border-top: none; border-right: none; border-bottom: 1px solid gray; text-align: center;"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbDaixieSecondpayHY" runat="server" Text=""></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="lbDaixieSecondpayHQ" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="vertical-align: middle">
                                            婚宴<asp:Label ID="Label25" runat="server" Text="Label"></asp:Label>
                                            <br />
                                            <br />
                                            婚庆<asp:Label ID="Label26" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: middle">
                                        <td style="vertical-align: middle">
                                            尾款
                                        </td>
                                        <td style="vertical-align: middle">
                                            婚礼前1周<asp:TextBox ID="tbThirdPayDate" runat="server" class="RiSelect" Style="width: 100px;
                                                border-left: none; border-top: none; border-right: none; border-bottom: 1px solid gray;
                                                text-align: center;"></asp:TextBox>
                                        </td>
                                        <td style="vertical-align: middle">
                                            婚宴%<asp:TextBox ID="tbThirdPayBaiHY" runat="server" Style="width: 100px; border-left: none;
                                                border-top: none; border-right: none; border-bottom: 1px solid gray; text-align: center;"></asp:TextBox>
                                            金额
                                            <asp:TextBox ID="tbThirdPayJineHY" runat="server" Style="width: 100px; border-left: none;
                                                border-top: none; border-right: none; border-bottom: 1px solid gray; text-align: center;"></asp:TextBox>
                                            <br />
                                            <br />
                                            婚庆%<asp:TextBox ID="tbThirdPayBaiHQ" runat="server" Style="width: 100px; border-left: none;
                                                border-top: none; border-right: none; border-bottom: 1px solid gray; text-align: center;"></asp:TextBox>
                                            金额
                                            <asp:TextBox ID="tbThirdPayJineHQ" runat="server" Style="width: 100px; border-left: none;
                                                border-top: none; border-right: none; border-bottom: 1px solid gray; text-align: center;"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbDaixieThirdpayHY" runat="server" Text=""></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="lbDaixieThirdpayHQ" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="vertical-align: middle">
                                            婚宴<asp:Label ID="Label29" runat="server" Text="Label"></asp:Label>
                                            <br />
                                            <br />
                                            婚庆<asp:Label ID="Label30" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div class="clearfix">
                                </div>
                            </div>
                        </div>
                        <div class=" col-lg-12">
                            <br>
                            <div class="form-group">
                                婚宴服务:
                                <input type="text" class="form-control tokenfield" runat="Server" id="tokenfieldHY"
                                    value="" />
                            </div>
                            <div class="form-group">
                                婚庆服务:
                                <input type="text" class="form-control tokenfield" id="tokenfieldHQ" runat="Server"
                                    value="" />
                            </div>
                            <div id="divServicesHQ" style="display: none">
                            </div>
                            <div class="form-group">
                                补充信息
                                <asp:TextBox ID="tbBuchongXinxi" TextMode="MultiLine" Rows="3" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
        </div>
        <!--增加款项-->
        <div class="panel  panel-info">
            <a href="#ExpayInfo" class="panel-heading" data-toggle="collapse">消费信息</a>
            <div id="ExpayInfo" class="panel-collapse panel-body collapse in">
                 <div class=" col-lg-12 table-responsive" >
                        <table class="table" style=" min-width:980px; margin-bottom:0px; border:none"><tr><td style=" padding-top:20px">
                         <div class="form-group  form-inline  col-lg-12 ">
                                            <div class="col-sm-12">
                                            <div style=""><b>增加项</b></div><hr ? />
                                        </div>
                         <div class="form-group  form-inline  col-lg-12  ">
                                           <div class="col-sm-12">
                                              <div style="width:150px; float:left"><b>婚宴</b></div>
                                               <div style="width:150px; float:left">菜金：<asp:TextBox ID="tbExCaijin"  runat="server" style="width:60px;border-left:none;border-top:none;border-right:none;border-bottom:1px solid gray" >0</asp:TextBox></div>
                                               <div style="width:150px; float:left">桌数：<asp:TextBox ID="tbExCJ_ZS"  runat="server"   style="width:30px;border-left:none;border-top:none;border-right:none;border-bottom:1px solid gray" >0</asp:TextBox></div>
                                               <div style="width:150px; float:left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;金额：<asp:Label ID="lbExJingeCJ" runat="server" Text="" style="width:100px"></asp:Label></div>
                                        </div>
                                        </div>
                         <div class="form-group  form-inline  col-lg-12 ">
                                           <div class="col-sm-12">
                                               <div style="width:150px; float:left">&nbsp;</div>
                                               <div style="width:150px; float:left">酒水：<asp:TextBox ID="tbExJiushui" runat="server"  style="width:60px;border-left:none;border-top:none;border-right:none;border-bottom:1px solid gray">0</asp:TextBox></div>
                                               <div style="width:150px; float:left">桌数：<asp:TextBox ID="tbExJS_ZS" runat="server"  style="width:30px;border-left:none;border-top:none;border-right:none;border-bottom:1px solid gray">0</asp:TextBox></div>
                                               <div style="width:150px; float:left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;金额：<asp:Label ID="lbExJingeJS" runat="server" Text="" style="width:100px"></asp:Label></div>
                                        </div>
                                        </div>
                                <div class="form-group  form-inline  col-lg-12 ">
                                           <div class="col-sm-12">
                                            <div style="width:450px; float:left; visibility:hidden">&nbsp;</div>
                                            <div style="width:150px; float:left;">-尾款抵扣：<asp:TextBox ID="tbWeikuanDikou" runat="server"  style="width:60px;border-left:none;border-top:none;border-right:none;border-bottom:1px solid gray">0</asp:TextBox></div>
                                        </div>
                                        </div> 
                                 <div class="form-group  form-inline  col-lg-12 ">
                                           <div class="col-sm-12">
                                            <div style="width:450px; float:left; visibility:hidden">&nbsp;</div>
                                            <div style="width:150px; float:left;padding-bottom:10px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;小计：<asp:Label ID="lbXiaojiHY" runat="server" Text="" style="width:100px"></asp:Label></div>
                                            <div style="width:150px; float:left">已付金额：<asp:Label ID="lbExYifujineHY" runat="server" Text="" style="width:100px"></asp:Label></div>
                                        </div>
                                        </div> 
                                  <div class="form-group  form-inline  col-lg-12  ">
                                           <div class="col-sm-12">
                                              <div style="width:150px; float:left"><b>婚庆</b></div>
                                               <div style="width:150px; float:left">桌花：<asp:TextBox ID="tbExZhuohua"  runat="server"   style="width:60px;border-left:none;border-top:none;border-right:none;border-bottom:1px solid gray;" >0</asp:TextBox></div>
                                               <div style="width:150px; float:left">其他：<asp:TextBox ID="tbExQita"  runat="server"   style="width:60px;border-left:none;border-top:none;border-right:none;border-bottom:1px solid gray" >0</asp:TextBox></div>
                                              <div style="width:150px; float:left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;小计：<asp:Label ID="lbXiaojiHQ" runat="server" Text="" style="width:100px"></asp:Label></div>
                                            <div style="width:150px; float:left">已付金额：<asp:Label ID="lbExYifujineHQ" runat="server" Text="" style="width:100px"></asp:Label></div>
                                        </div>
                                        </div>  
                                        </div>             
                                        <div class="clearfix"></div>
                                        <hr />
                         <div class="form-group  form-inline  col-lg-12 ">
                                            <div class="col-sm-12">
                                            <div style=""><b>总消费</b></div>
                                         
                                                    <hr />
                                        </div>
                                          <div class="form-group  form-inline  col-lg-12  ">
                                           <div class="col-sm-12">
                                              <div style="width:150px; float:left"><b>婚宴</b></div>
                                               <div style="width:150px; float:left"><asp:Label ID="lbZXF_HY" runat="server" Text="" style="width:100px"></asp:Label></div>
                                               <div style="width:150px; float:left">已付金额：<asp:Label ID="lbZXF_HY_Yifu" runat="server" Text="" style="width:100px"></asp:Label></div>
                                        </div>
                                        </div>
                                            <div class="form-group  form-inline  col-lg-12  ">
                                           <div class="col-sm-12">
                                              <div style="width:150px; float:left"><b>婚庆</b></div>
                                               <div style="width:150px; float:left"><asp:Label ID="lbZXF_HQ" runat="server" Text="" style="width:100px"></asp:Label></div>
                                               <div style="width:150px; float:left">已付金额：<asp:Label ID="lbZXF_HQ_Yifu" runat="server" Text="" style="width:100px"></asp:Label></div>
                                        </div>
                                        </div>
                                         <div class="form-group  form-inline  col-lg-12  ">
                                           <div class="col-sm-12">
                                              <div style="width:150px; float:left"><b>合计</b></div>
                                               <div style="width:150px; float:left"><asp:Label ID="lbZXF_heji" runat="server" Text="" style="width:100px"></asp:Label></div>
                                               <div style="width:150px; float:left">已付金额：<asp:Label ID="lbZXF_heji_Yifu" runat="server" Text="" style="width:100px"></asp:Label></div>
                                        </div>
                                        </div>
                                        </div>  
                                            
                         </td></tr></table>    

                        </div>
            </div>
        </div>
        <!-- 审核 -->
        
    </div>
    <div class="clearfix">
    </div>
    <div style="text-align: center">
        <asp:Button ID="btUpdate" runat="server" class="btn btn-default" Text="保存" 
            onclick="btUpdate_Click" />&nbsp;&nbsp;
           <asp:HiddenField ID="hdID" runat="server" />
        <asp:HiddenField ID="hdChgDes" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
