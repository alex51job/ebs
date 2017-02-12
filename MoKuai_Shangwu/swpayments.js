/// <reference path="../Scripts/jquery.min.js">
//////////////////////////////////////////////////////////////////////////
///Init
//////////////////////////////////////////////////////////////////////////
$(function () {
    BindEvents();
    makeRiSelect();
    BindPayTable();
    InitIfhasID(0);
    ClickToDelete();
    ClickToSave();
    ClickToSubmit();

});
function BindEvents() {
    $("#btAdd").on("click", function () {
        AddRowForPaytable();
    });
    $("#btAdd_Tk").on("click", function () {
        AddRowForRefundTable();
    })
}

function BindPayTable() {
    $("#fuKuanTable").on("input propertychange", function () {
        CalcuateByPayment()
    });
    $("#tuiKuanTable").on("input propertychange", function () {
        CalcuateByRefunds();
    });
}
function InitIfhasID(ID) {
    if (ID!=0) {

    }
    $("#lbYingshou").text($("#MainContent_hdZongjine").val());
}

function HideBtadd() {
    $("#btAdd").hide();
}
//////////////////////////////////////////////////////////////////////////
///Pay table
//////////////////////////////////////////////////////////////////////////
function AddRowForPaytable() {
    var node = "<tr style='vertical-align: middle' id='payContent'>\
                            <td style='vertical-align: middle;height:40px;'>\
                                 <input type='text' class='OnlyBottom' id='PayOrder' name='PayOrder' />\
                            </td>\
                            <td style='vertical-align: middle'>\
                                <input type='text' class='RiSelect OnlyBottom' id='PayDate' name='PayDate' />\
                            </td>\
                             <td style='vertical-align: middle'>\
                                 <select id='PayMethod' name='PayMethod' class='PayMethod' style='width: 60px;' >\
                                <option></option>\
                                <option>转账</option>\
                                <option>现金</option>\
                                </select>\
                            </td>\
                            <td style='vertical-align: middle;text-align:left;padding-left:10px'>\
                                金额&nbsp;&nbsp;<input type='text' id= 'PayAmount' name='PayAmount' class='OnlyBottom PayAmount'/>\
                                &nbsp;&nbsp;&nbsp;&nbsp; 大写&nbsp;&nbsp;<label id='PayAmountDaxie' name='PayAmountDaxie' class='PayAmountDaxie'></label>&nbsp;&nbsp;\
                                   <label id='PayAmountPer' name='PayAmountPer' class='PayAmountPer Label30px'></label>\
                            </td>\
                            <td style='vertical-align: middle'>\
                              <label id='zhuangtai'>编辑中</label>\
                            </td>\
                            <td style='vertical-align: middle;'>\
                                 <input type='hidden' id='payID' name='payID' value='0' />\
                            <div id='btnSave' class='btn btn-default btn-sm btnSave' title='保存'><i class='fa fa-save' style='display:block'></i></div>\
                                <div id='btnSubmit' class='btn btn-default btn-sm btnSubmit' title='提交'><i class='fa fa-upload' style='display:block'></i></div>\
                                <div  id='btnDelete' class='btn btn-default btn-sm btnDelete' title='删除'><i class='fa fa-ban' style='display:block'></i></div>\
                            </td>\
                        </tr>";
    $(node).insertBefore($("#Summary"));

}

function LoadPayments(data) {
    var res = data;
    $.each(res.payments, function (i, item) {
        var node = "<tr style='vertical-align: middle' id='payContent'>\
                            <td style='vertical-align: middle;height:40px;'>\
                                 <input type='text' class='OnlyBottom' id='PayOrder' name='PayOrder'  value='" + item.PayNo + "'/>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                <input type='text' class='RiSelect OnlyBottom' id='PayDate' name='PayDate'  value='" + item.PayDate + "'/>\
                            </td>\
                             <td style='vertical-align: middle'>\
                                 <select id='PayMethod' name='PayMethod' class='PayMethod' style='width: 60px;' >\
                                <option>" + item.PayType + "</option>\
                                <option>转账</option>\
                                <option>现金</option>\
                                </select>\
                            </td>\
                            <td style='vertical-align: middle;text-align:left;padding-left:10px'>\
                                金额&nbsp;&nbsp;<input type='text' id= 'PayAmount' name='PayAmount' class='OnlyBottom PayAmount' value='" + item.PayAmount + "'/>\
                                &nbsp;&nbsp;&nbsp;&nbsp; 大写&nbsp;&nbsp;<label id='PayAmountDaxie' name='PayAmountDaxie' class='PayAmountDaxie'></label>&nbsp;&nbsp;\
                                   <label id='PayAmountPer' name='PayAmountPer' class='PayAmountPer Label30px'></label>\
                            </td>\
                            <td style='vertical-align: middle'>\
                              <label id='zhuangtai'>" + item.PayStatus + "</label>\
                            </td>\
                            <td style='vertical-align: middle;'>\
                                 <input type='hidden' id='payID' name='payID' value='" + item.PayId + "' />\
                            <div id='btnSave' class='btn btn-default btn-sm btnSave " + (item.PayStatus == "可编辑" ? "" : "hide") + "' title='保存'><i class='fa fa-save' style='display:block'></i></div>\
                                <div id='btnSubmit' class='btn btn-default btn-sm btnSubmit  " + (item.PayStatus == "可编辑" ? "" : "hide") + "' title='提交'><i class='fa fa-upload' style='display:block'></i></div>\
                                <div  id='btnDelete' class='btn btn-default btn-sm btnDelete  " + (item.PayStatus == '审批完成' ? "hide" : "") + "' title='删除'><i class='fa fa-ban' style='display:block'></i></div>\
                            </td>\
                        </tr>";
        $(node).insertBefore($("#Summary"));
        CalcuateByPayment();
    });
}

function ClickToSave() {

    $(document).on("click", ".btnSave", function () {
        var trThis = $(this).closest("tr");
        if ($(trThis).find("#PayOrder").val() == "") {
            alert("请填写付款单号");
            return false;
        }
        if ($(trThis).find("#PayDate").val() == "") {
            alert("请选择日期");
            return false;
        }
        if ($(trThis).find("#PayMethod").val() == "") {
            alert("请选择支付方式");
            return false;
        }
        if ($(trThis).find("#PayAmount").val() == "") {
            alert("请填写金额");
            return false;
        }
        showLoad();
        $.ajax({
            url: "Shangwu.ashx?method=save&orderID=" + $("#MainContent_hdID").val() + "&dt=" + new Date().getTime(),
            type: "post",
            async: false,
            data: { "PayOrder": $(trThis).find("#PayOrder").val(), "PayDate": $(trThis).find("#PayDate").val(), "PayMethod": $(trThis).find("#PayMethod").val(), "PayAmount": $(trThis).find("#PayAmount").val(), "id": $(trThis).find("#payID").val() },
            success: function (data) {
                var res = eval("(" + data + ")");
                if (res.result == "error") {
                    alert(res.message);
                }
                else {
                    $(trThis).find("#payID").val(res.id);
                    //alert(res.result);
                    $("#loadingInfo").html(res.result);
                }
            },
            error: function (e) {
                var s = eval("(" + e + ")");
                alert(s);
            }
        })
        hideLoad();
    });

    $(document).on("click", ".btnRefundSave", function () {
        var trThis = $(this).closest("tr");
        if ($(trThis).find("#PayDate").val() == "") {
            alert("请填写退款日期");
            return false;
        }
        if ($(trThis).find("#PayDate").val() == "") {
            alert("请选择日期");
            return false;
        }
        if ($(trThis).find("#PayAmount").val() == "") {
            alert("请填写金额");
            return false;
        }
        showLoad();
        $.ajax({
            url: "Shangwu.ashx?method=saveRefund&orderID=" + $("#MainContent_hdID").val() + "&dt=" + new Date().getTime(),
            type: "post",
            async: false,
            data: { "PayDate": $(trThis).find("#PayDate").val(), "PayMethod": $(trThis).find("#PayMethod").val(), "PayAmount": $(trThis).find("#PayAmount").val(), "id": $(trThis).find("#payID").val() },
            success: function (data) {
                var res = eval("(" + data + ")");
                if (res.result == "error") {
                    alert(res.message);
                }
                else {
                    $(trThis).find("#payID").val(res.id);
                    //alert(res.result);
                    $("#loadingInfo").html(res.result);
                }
            },
            error: function (e) {
                var s = eval("(" + e + ")");
                alert(s);
            }
        })
        hideLoad();
    });
}

function ClickToSubmit() {
    $(document).on("click", ".btnSubmit", function () {
        var trThis = $(this).closest("tr");
        if ($(trThis).find("#PayOrder").val() == "") {
            alert("请填写付款单号");
            return false;
        }
        if ($(trThis).find("#PayDate").val() == "") {
            alert("请选择日期");
            return false;
        }
        if ($(trThis).find("#PayMethod").val() == "") {
            alert("请选择支付方式");
            return false;
        }
        if ($(trThis).find("#PayAmount").val() == "") {
            alert("请填写金额");
            return false;
        }
        if (confirm("确认提交财务审批吗？")) {
            showLoad();
            $.ajax({
                url: "Shangwu.ashx?method=submitPayment&orderID=" + $("#MainContent_hdID").val() + "&dt=" + new Date().getTime(),
                type: "post",
                async: false,
                data: { "PayOrder": $(trThis).find("#PayOrder").val(), "PayDate": $(trThis).find("#PayDate").val(), "PayMethod": $(trThis).find("#PayMethod").val(), "PayAmount": $(trThis).find("#PayAmount").val(), "id": $(trThis).find("#payID").val() },
                success: function (data) {

                    var res = eval("(" + data + ")");
                    if (res.result == "error") {
                        alert(res.message);
                    }
                    else {
                        $(trThis).find("#payID").val(res.id);
                        $(trThis).find("#btnSave").hide();
                        $(trThis).find("#btnSubmit").hide();
                        $(trThis).find("#zhuangtai").html("审批中");
                        //alert(res.result);

                        $("#loadingInfo").text(res.result);
                    }
                },
                error: function (e) {
                    alert(e);
                }
            })
            hideLoad();
        }

    });
    $(document).on("click", ".btnRefundSubmit", function () {
        var trThis = $(this).closest("tr");
        if ($(trThis).find("#PayDate").val() == "") {
            alert("请填写退款日期");
            return false;
        }
        if ($(trThis).find("#PayDate").val() == "") {
            alert("请选择日期");
            return false;
        }
        if ($(trThis).find("#PayAmount").val() == "") {
            alert("请填写金额");
            return false;
        }
        if (confirm("确认提交财务审批吗？")) {
            showLoad();
            $.ajax({
                url: "Shangwu.ashx?method=submitRefund&orderID=" + $("#MainContent_hdID").val() + "&dt=" + new Date().getTime(),
                type: "post",
                async: false,
                data: { "PayDate": $(trThis).find("#PayDate").val(), "PayMethod": $(trThis).find("#PayMethod").val(), "PayAmount": $(trThis).find("#PayAmount").val(), "id": $(trThis).find("#payID").val() },
                success: function (data) {

                    var res = eval("(" + data + ")");
                    if (res.result == "error") {
                        alert(res.message);
                    }
                    else {
                        $(trThis).find("#payID").val(res.id);
                        $(trThis).find("#btnSave").hide();
                        $(trThis).find("#btnSubmit").hide();
                        $(trThis).find("#zhuangtai").html("审批中");
                        //alert(res.result);

                        $("#loadingInfo").text(res.result);
                    }
                },
                error: function (e) {
                    alert(e);
                }
            })
            hideLoad();
        }

    });

}

function ClickToDelete() {
    $(document).on("click", ".btnDelete", function () {

        var trThis = $(this).closest("tr");
        var id = $(trThis).find("#payID").val();
        if (confirm("确认删除吗？")) {
            showLoad();
            if (id == 0) {
                //$("#loadingInfo").html(res.result);
                $(this).closest("tr").remove();
            }
            else {
                $.ajax({
                    url: "Shangwu.ashx?method=deletePayment&dt=" + new Date().getTime(),
                    async: false,
                    data: { "payID": id },
                    success: function (data) {
                        var res = eval("(" + data + ")");
                        if (res.result == "error") {
                            alert(res.message);
                        }
                        else {
                            $(trThis).remove();
                            $("#loadingInfo").html(res.result);
                            // alert(res.result);
                        }
                    }
                });
            }
            CalcuateByPayment();
            hideLoad("slow");
        }

    });

    $(document).on("click", ".btnRefundDelete", function () {

        var trThis = $(this).closest("tr");
        var id = $(trThis).find("#payID").val();
        if (confirm("确认删除吗？")) {
            showLoad();
            if (id == 0) {
                //$("#loadingInfo").html(res.result);
                $(this).closest("tr").remove();
            }
            else {
                $.ajax({
                    url: "Shangwu.ashx?method=deleteRefund&dt=" + new Date().getTime(),
                    async: false,
                    data: { "payID": id },
                    success: function (data) {
                        var res = eval("(" + data + ")");
                        if (res.result == "error") {
                            alert(res.message);
                        }
                        else {
                            $(trThis).remove();
                            $("#loadingInfo").html(res.result);
                            // alert(res.result);
                        }
                    }
                });
            }
            CalcuateByRefunds();
            hideLoad("slow");
        }

    });
}

//////////////////////////////////////////////////////////////////////////
///Refund Table
//////////////////////////////////////////////////////////////////////////
function AddRowForRefundTable() {
    var node = "<tr style='vertical-align: middle' id='payContent'>\
                              <td style='vertical-align: middle'>\
                                 <select id='PayMethod' name='PayMethod' class='PayMethod' style='width: 60px;' >\
                                <option></option>\
                                <option>退款</option>\
                                <option>赔款</option>\
                                </select>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                <input type='text' class='RiSelect OnlyBottom' id='PayDate' name='PayDate' />\
                            </td>\
                            <td style='vertical-align: middle;text-align:left;padding-left:10px'>\
                                金额&nbsp;&nbsp;<input type='text' id= 'PayAmount' name='PayAmount' class='OnlyBottom PayAmountR'/>\
                                &nbsp;&nbsp;&nbsp;&nbsp; 大写&nbsp;&nbsp;<label id='PayAmountDaxie' name='PayAmountDaxie' class='PayAmountDaxieR'></label>&nbsp;&nbsp;\
                                   <label id='PayAmountPer' name='PayAmountPer' class='PayAmountPer Label30px'></label>\
                            </td>\
                            <td style='vertical-align: middle'>\
                              <label id='zhuangtai'>编辑中</label>\
                            </td>\
                            <td style='vertical-align: middle;'>\
                                 <input type='hidden' id='payID' name='payID' value='0' />\
                            <div id='btnSave' class='btn btn-default btn-sm btnRefundSave' title='保存'><i class='fa fa-save' style='display:block'></i></div>\
                                <div id='btnSubmit' class='btn btn-default btn-sm btnRefundSubmit' title='提交'><i class='fa fa-upload' style='display:block'></i></div>\
                                <div  id='btnDelete' class='btn btn-default btn-sm btnRefundDelete' title='删除'><i class='fa fa-ban' style='display:block'></i></div>\
                            </td>\
                        </tr>";
    $(node).insertBefore($("#SummaryTuikuan"));
}

function LoadRefund(data) {
    var res = data;
    $.each(res.Refunds, function (i, item) {
        var node = "<tr style='vertical-align: middle' id='payContent'>\
                           <td style='vertical-align: middle'>\
                                 <select id='PayMethod' name='PayMethod' class='PayMethod' style='width: 60px;' >\
                                <option>" + item.PayType + "</option>\
                                  <option>退款</option>\
                                <option>赔款</option>\
                                </select>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                <input type='text' class='RiSelect OnlyBottom' id='PayDate' name='PayDate'  value='" + item.PayDate + "'/>\
                            </td>\
                            <td style='vertical-align: middle;text-align:left;padding-left:10px'>\
                                金额&nbsp;&nbsp;<input type='text' id= 'PayAmount' name='PayAmount' class='OnlyBottom PayAmountR' value='" + item.PayAmount + "'/>\
                                &nbsp;&nbsp;&nbsp;&nbsp; 大写&nbsp;&nbsp;<label id='PayAmountDaxie' name='PayAmountDaxie' class='PayAmountDaxieR'></label>&nbsp;&nbsp;\
                                   <label id='PayAmountPer' name='PayAmountPer' class='PayAmountPer Label30px'></label>\
                            </td>\
                            <td style='vertical-align: middle'>\
                              <label id='zhuangtai'>" + item.PayStatus + "</label>\
                            </td>\
                            <td style='vertical-align: middle;'>\
                                 <input type='hidden' id='payID' name='payID' value='" + item.PayId + "' />\
                            <div id='btnSave' class='btn btn-default btn-sm btnRefundSave " + (item.PayStatus == "可编辑" ? "" : "hide") + "' title='保存'><i class='fa fa-save' style='display:block'></i></div>\
                                <div id='btnSubmit' class='btn btn-default btn-sm btnRefundSubmit  " + (item.PayStatus == "可编辑" ? "" : "hide") + "' title='提交'><i class='fa fa-upload' style='display:block'></i></div>\
                                <div  id='btnDelete' class='btn btn-default btn-sm btnRefundDelete  " + (item.PayStatus == '审批完成' ? "hide" : "") + "' title='删除'><i class='fa fa-ban' style='display:block'></i></div>\
                            </td>\
                        </tr>";
        $(node).insertBefore($("#SummaryTuikuan"));
        CalcuateByRefunds();
    });
}


//////////////////////////////////////////////////////////////////////////
///Calcuate
//////////////////////////////////////////////////////////////////////////
function CalcuateByPayment() {
    var zongjine = $("#MainContent_hdZongjine").val() * 1;
    var payments = $(".PayAmount");
    var shishouJine = 0;
    for (var i = 0; i < payments.length; i++) {
        var payment = $(".PayAmount").eq(i);
        var PayAmountDaxie = $(".PayAmountDaxie").eq(i);
        var PayAmountPer = $(".PayAmountPer").eq(i);

        PayAmountDaxie.text(digit_uppercase(payment.val()));
        PayAmountPer.text((payment.val() * 1 / zongjine * 100).toFixed(0) + "%");

        shishouJine += payment.val() * 1;
    }
    $("#lbShishou").text(shishouJine);
    $("#lbShishouPer").text((shishouJine / zongjine * 100).toFixed(0) + "%");


}

function CalcuateByRefunds() {
    //var zongTuikuan = $("#MainContent_lbZongTKHY").val() * 1;
    var zong = 0;
    var refunds = $(".PayAmountR");
    for (var i = 0; i < refunds.length; i++) {
        var refund = $(".PayAmountR").eq(i);
        $(".PayAmountDaxieR").eq(i).text(digit_uppercase(refund.val()));
        zong += refund.val() * 1;
    }
    $("#lbZongTKHY").text(zong);
}

//////////////////////////////////////////////////////////////////////////
///Date and Check
//////////////////////////////////////////////////////////////////////////
function makeRiSelect() {
    $(document).on("focus", ".RiSelect", function () {
        $(this).datetimepicker({
            format: 'yyyy-mm-dd',
            autoclose: true,
            todayBtn: true,
            startView: "month",
            minView: "month",
            showMeridian: true

        });
    });
}
function showLoad() {
    $('#loadingInfo').html("处理中...")
    $('#loading').fadeIn();
}

function hideLoad() {
    $('#loading').fadeOut("slow");
}
function digit_uppercase(n) {
    var fraction = ['角', '分'];
    var digit = [
        '零', '壹', '贰', '叁', '肆',
        '伍', '陆', '柒', '捌', '玖'
    ];
    var unit = [
        ['元', '万', '亿'],
        ['', '拾', '佰', '仟']
    ];
    var head = n < 0 ? '欠' : '';
    n = Math.abs(n);
    var s = '';
    for (var i = 0; i < fraction.length; i++) {
        s += (digit[Math.floor(n * 10 * Math.pow(10, i)) % 10] + fraction[i]).replace(/零./, '');
    }
    s = s || '整';
    n = Math.floor(n);
    for (var i = 0; i < unit[0].length && n > 0; i++) {
        var p = '';
        for (var j = 0; j < unit[1].length && n > 0; j++) {
            p = digit[n % 10] + unit[1][j] + p;
            n = Math.floor(n / 10);
        }
        s = p.replace(/(零.)*零$/, '')
             .replace(/^$/, '零')
          + unit[0][i] + s;
    }
    return head + s.replace(/(零.)*零元/, '元')
                   .replace(/(零.)+/g, '零')
                   .replace(/^整$/, '零元整');
}

function ThrowError(str) {

    ChangeTriggerForKehu();
    InitEventTable($("#MainContent_hdTableEventJsons").val());
    alert("发生错误，错误代码：" + str);
}