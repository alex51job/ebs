<%@ Page Title="" Language="C#" MasterPageFile="~/MoKuai_Shangwu/Shangwu.Master"
    AutoEventWireup="true" CodeBehind="ShangwuEdit.aspx.cs" Inherits="ebs.MoKuai_Shangwu.ShangwuEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="Shangwu.js?d=<%=DateTime.Now.ToFileTime() %>" type="text/javascript"></script>
    <style type="text/css">
        table td, th
        {
            white-space: nowrap !important;
            vertical-align: middle !important;
            text-align: center !important;
        }
        .selType
        {
            width: 45%;
        }
        
        .inputInTable
        {
            border-left: none;
            border-top: none;
            border-right: none;
            border-bottom: 1px solid gray;
        }
        table .muSelect
        {
            width: 75% !important;
        }
        .Huiyi
        {
            display: none;
        }
        .Yongcan
        {
            display: none;
        }
        .canbiao
        {
            width: 20%;
        }
        .jiushui
        {
            width: 20%;
        }
        .shuliang
        {
            width: 20%;
        }
        hr
        {
            border-left: none;
            border-top: none;
            border-right: none;
            border-bottom: 1px dashed #CCCCCC;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titleContent" runat="server">
    商务订单
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Literal ID="BussinessTabs" runat="server"></asp:Literal>
    <br />
    <div class="col-sm-10 col-md-10 col-lg-10 col-lg-offset-1">
        <!-- 选择客户 -->
        <div class="panel panel-default">
            <a href="#KehuInfo" class="panel-heading" data-toggle="collapse">活动信息</a>
            <div id="KehuInfo" class="panel-collapse panel-body collapse in">
                <div class="main-content">
                    <div class="row">
                        <div class=" col-lg-12 form-horizontal ">
                            <br>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    客/电/销/地<font color="red">*</font></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddKehu" runat="server" class="form-control SingleSelect">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    状态</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="tbZhuangtai" runat="server" class="form-control disabled" Style="display: inline"
                                        ReadOnly></asp:TextBox>
                                </div>
                                &nbsp;
                                <label class="control-label btn btn-link" id="lbRevision">
                                    历史版本<span id="spanNumRevision"></span></label>
                                <div class="hide" id='divRevision'>
                                    <ul class='list-group' id='ulRevision'>
                                    </ul>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    合同编号<font color="red">*</font></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbHetongBianhao" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    合同日期<font color="red">*</font></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbHetongRiqi" runat="server" class="form-control RiSelectNeedVal"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    公司名<font color="red">*</font></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbCompany" runat="server" class="form-control" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    渠道</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbQudao" runat="server" class="form-control" ReadOnly></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <hr />
                            <div class="clearfix">
                            </div>
                            <div class="form-group  col-lg-4 ">
                                <label class="col-sm-4 control-label">
                                    联系人1<font color="red">*</font></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbLianxiren1" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-lg-4 ">
                                <label class="col-sm-4 control-label">
                                    座机</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbLianxiren1_Zuoji" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-lg-4 ">
                                <label class="col-sm-4 control-label">
                                    手机<font color="red">*</font></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbLianxiren1_Shouji" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-4 ">
                                <label class="col-sm-4 control-label">
                                    联系人2</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbLianxiren2" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-lg-4 ">
                                <label class="col-sm-4 control-label">
                                    座机</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbLianxiren2_Zuoji" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-lg-4 ">
                                <label class="col-sm-4 control-label">
                                    手机</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbLianxiren2_Shouji" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <hr />
                            <div class="clearfix">
                            </div>
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    活动地点<font color="red">*</font></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddEventVenue" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    负责销售</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddSales" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    活动名称<font color="red">*</font></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbEventName" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    活动时间<font color="red">*</font></label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbEventRiqi" runat="server" class="form-control TimeSelect"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    活动类型<font color="red">*</font></label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddEventType" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <hr />
                            <div class="clearfix">
                            </div>
                            <div style="display: none" id="venueList" runat="server">
                            </div>
                            <div style="display: none" id="yanhuiList" runat="server">
                                [{id:"zao",text:"zao"}, {id:"wan",text:"wan"}]</div>
                            <asp:HiddenField ID="hdTableEventJsons" runat="server" />
                            <asp:HiddenField ID="hdID" runat="server" />
                            <div class="table-responsive">
                                <table class="table table-bordered" style="vertical-align: middle; min-width: 980px"
                                    id="tableEvent">
                                    <thead>
                                        <tr>
                                            <th style="width: 20%">
                                                活动形式
                                            </th>
                                            <th style="width: 30%">
                                                场地
                                            </th>
                                            <th style="width: 20%">
                                                费用1
                                            </th>
                                            <th style="width: 20%">
                                                费用2
                                            </th>
                                            <th>
                                                <div class="btn btn-primary btn-sm" onclick="AddRow()">
                                                    增加</div>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td>
                                            <select id="selType_0" class="selType" onchange="changeFeeType(this)">
                                                <option></option>
                                                <option>会议</option>
                                                <option>用餐</option>
                                            </select>
                                            &nbsp;&nbsp;<select id="selYongcan_0" class="Yongcan selYongcan"></select>
                                        </td>
                                        <td>
                                            <select id="selVenue_0" class="muSelect form-control selVenue" multiple>
                                            </select>
                                        </td>
                                        <td>
                                            <div class="Huiyi">
                                                搭建费：<input type="text" id="tbDajianfei_0" value='0' class="inputInTable Dajianfei" /></div>
                                            <div class="Yongcan">
                                                餐标：<input type="text" id="tbCanbiao_0" value='0' class="inputInTable canbiao" />
                                                *
                                                <input type="text" id="tbCanbiaoShuoliang_0" value='0' class="inputInTable shuliang" />
                                                &nbsp;
                                                <select id="ddCanbiaoDanwei_0" class="ddDanwei">
                                                    <option>桌 </option>
                                                    <option>人 </option>
                                                </select>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="Huiyi">
                                                场地费：<input type="text" id="tbChangdifei_0" value='0' class="inputInTable Changdifei" /></div>
                                            <div class="Yongcan">
                                                酒水：<input type="text" id="tbJiushui_0" value='0' class="inputInTable jiushui" />
                                                *
                                                <input type="text" id="tbJiushuiShuiliang_0" value='0' class="inputInTable shuliang" />
                                                &nbsp;
                                                <select id="ddJiushuiDanwei_0" class="ddDanwei">
                                                    <option>桌 </option>
                                                    <option>人 </option>
                                                </select>
                                            </div>
                                        </td>
                                        <td>
                                            <div class='btn btn-danger btn-sm' onclick='DeleteRow(this)'>
                                                删除</div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <a href="#PayInfo" class="panel-heading" data-toggle="collapse">付款信息</a>
            <div id="PayInfo" class="panel-collapse panel-body collapse in">
                <div class="main-content">
                    <div class="row">
                        <div class=" col-lg-12 form-horizontal ">
                            <br>
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    打包单价</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbDabaoFee" runat="server" class="form-control" Style="width: 50%;
                                        display: inline">0</asp:TextBox>
                                    *
                                    <asp:TextBox ID="tbDabaoRen" runat="server" class="form-control" Style="width: 30%;
                                        display: inline">0</asp:TextBox>&nbsp;&nbsp;人
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    其他费用1</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="OtherFeeName1" placeholder="费用名称" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    金额</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="OtherFeeValue1" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    其他费用2</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="OtherFeeName2" placeholder="费用名称" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    金额</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="OtherFeeValue2" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    其他费用3</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="OtherFeeName3" placeholder="费用名称" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    金额</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="OtherFeeValue3" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-6 ">
                                <label class="col-sm-4 control-label">
                                    折扣金额</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="tbZhekou" runat="server" class="form-control ">0</asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="form-group  col-lg-7 ">
                            
                                <label class="col-sm-2 control-label">
                                    总金额</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="tbHetongZongjine" runat="server" class="form-control" ReadOnly Style="display: inline;"></asp:TextBox>
                                  
                                </div>
                                 
                               <label class="col-sm-2 control-label">
                                    已付金额</label>
                                      <div class="col-sm-4">
                              <asp:TextBox ID="tbYifuJine" runat="server" class="form-control " ReadOnly Style="display: inline;"></asp:TextBox>
                                        </div>
                           
                            </div>
                          
                            <div class="form-group  col-lg-5 ">
                                  <label class="col-sm-2 control-label">
                                    大写</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="tbDaxieZongjine" runat="server" class="form-control " ReadOnly Style="display: inline; "></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group  col-lg-7 ">
                                <label class="col-sm-2 control-label">
                                    返佣</label>
                                <div class="col-sm-4 ">
                                <div class="input-group">
                                    <asp:TextBox ID="tbFanyongPer" runat="server" class="form-control" Style="
                                        display: inline" value="0"></asp:TextBox>
                                   <span class="input-group-addon">%</span>
                                </div>
                                </div>
                                 <label class="col-sm-2 control-label">
                                    金额</label>
                                <div class="col-sm-4">
                                 <asp:TextBox ID="tbFanyongAmount" runat="server" class="form-control" Style="
                                        display: inline"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="text-align: center">
            <asp:Button ID="btSave" runat="server" class="btn btn-default" Text="保存订单" OnClientClick="return CheckAll()"
                OnClick="btSave_Click" />&nbsp;&nbsp;
            <asp:Button ID="btSubmit" runat="server" class="btn btn-default" Text="提交审批" OnClick="btSubmit_Click"
                OnClientClick="return CheckAll()" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="dialogContent" runat="server">
</asp:Content>
