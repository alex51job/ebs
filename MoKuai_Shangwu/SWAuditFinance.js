/// <reference path="../Scripts/jquery.min.js">
$(function () {
    $("#lbYingshou").text($("#MainContent_hdZongjine").val());
    BtPassClick();
})

//////////////////////////////////////////////////////////////////////////
///BindEvent
//////////////////////////////////////////////////////////////////////////
function BtPassClick() {
    $(document).on("click", ".btnRedrew", function () {
        if (confirm("确认退回吗？")) {
            showLoad();
            var trThis = $(this).closest("tr");
            $.ajax({
                url: "Shangwu.ashx?method=redrew&orderID=" + $("#MainContent_hdID").val() + "&dt=" + new Date().getTime(),
                async: false,
                data: { "payID": $(trThis).find("#payID").val() },
                success: function (data) {
                    var res = eval("(" + data + ")");
                    if (res.result == "error") {
                        alert(res.message);
                    }
                    else {
                        $(trThis).remove();
                        //alert(res.result);
                        $("#loadingInfo").html(res.result);
                    }
                },
                error: function (e) {
                    alert(e);
                }
            })
            hideLoad();
        }
    });

    $(document).on("click", ".btnPass", function () {
        if (confirm("确认审批通过吗？")) {
            showLoad();
            var trThis = $(this).closest("tr");
            $.ajax({
                url: "Shangwu.ashx?method=pass&orderID=" + $("#MainContent_hdID").val() + "&dt=" + new Date().getTime(),
                async: false,
                data: { "payID": $(trThis).find("#payID").val() },
                success: function (data) {

                    var res = eval("(" + data + ")");
                    if (res.result == "error") {
                        alert(res.message);
                    }
                    else {
                        $(trThis).find("#btnPass").hide();
                        $(trThis).find("#btnRedrew").hide();
                        $(trThis).find("#zhuangtai").html("审批完成");
                        //alert(res.result);
                        //判断是否能完成订单
                        if (res.finish && res.finish == "1" && $("#MainContent_btFinish").length > 0) {
                            $("#MainContent_btFinish").show();
                        } else {
                            $("#MainContent_btFinish").hide();
                        }

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


//////////////////////////////////////////////////////////////////////////
///LoadPayTable
//////////////////////////////////////////////////////////////////////////
function LoadRefund(data) {
    var res = data;
    $.each(res.Refunds, function (i, item) {
        var node = "<tr style='vertical-align: middle' id='payContent'>\
                           <td style='vertical-align: middle'>\
                            <input type='text' class='OnlyBottom' id='PayMethod' name='PayMethod'  value='" + item.PayType + "'/>\
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
                               <div id='btnPass' class='btn btn-default btn-sm btnPass " + (item.PayStatus == '审批中' ? "" : "hide") + "' title='通过'>通过</div>\
                                <div id='btnRedrew' class='btn btn-default btn-sm btnRedrew " + (item.PayStatus == '审批中' ? "" : "hide") + "' title='退回'>退回</div>\
                            </td>\
                        </tr>";
        $(node).insertBefore($("#SummaryTuikuan"));
        CalcuateByRefunds();
    });
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
                              <input type='text' class='OnlyBottom' id='PayMethod' name='PayMethod'  value='" + item.PayType + "'/>\
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
                                  <div id='btnPass' class='btn btn-default btn-sm btnPass " + (item.PayStatus == '审批中' ? "" : "hide") + "' title='通过'>通过</div>\
                                <div id='btnRedrew' class='btn btn-default btn-sm btnRedrew " + (item.PayStatus == '审批中' ? "" : "hide") + "' title='退回'>退回</div>\
                                </td>\
                        </tr>";
        $(node).insertBefore($("#Summary"));
        CalcuateByPayment();
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
///General
//////////////////////////////////////////////////////////////////////////
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