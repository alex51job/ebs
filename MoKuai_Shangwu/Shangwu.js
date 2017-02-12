/// <reference path="../Scripts/jquery.min.js">
$(function () {
    checkNewRequestForm();
    MakeSelect2("");
    MakeDatePicker();
    MakeSelectForVenueList("");
    $("#MainContent_ddKehu").on("change", function () {
        ChangeTriggerForKehu();
    });
    $("#tableEvent").on("input propertychange change", function () {
        CalculateTotalPrice("1");
    })
    $("#MainContent_tbDabaoFee").on("input propertychange", function () {
        CalculateTotalPrice("1");
    })
    $("#MainContent_tbDabaoRen").on("input propertychange", function () {
        CalculateTotalPrice("1");
    })
    $("#MainContent_OtherFeeValue1").on("input propertychange", function () {
        CalculateTotalPrice("1");
    })
    $("#MainContent_OtherFeeValue2").on("input propertychange", function () {
        CalculateTotalPrice("1");
    })
    $("#MainContent_OtherFeeValue3").on("input propertychange", function () {
        CalculateTotalPrice("1");
    })
    $("#MainContent_tbZhekou").on("input propertychange", function () {
        CalculateTotalPrice("1");
    })
    $("#MainContent_tbFanyongPer").on("input propertychange", function () {
        CalculateTotalPrice("1");
    })
    $("#MainContent_tbFanyongAmount").on("input propertychange", function () {
        CalculateTotalPrice("2");
    })
});

//////////////////////////////////////////////////////////////////////////
///Load Initial when has ID
//////////////////////////////////////////////////////////////////////////
function InitEventTable(JsonDatas) {
    MakeSelect2("");
    MakeSelectForVenueList("");
    var res = eval("(" + JsonDatas + ")");
    var resLength = res.length;
    for (var i = 0; i < resLength; i++) {
        if (i == 0) {
            var thisRow = $("#tableEvent").find("tr").eq(1);
            $(thisRow).find(".selVenue").val(res[i].Venue.split(",")).trigger("change");
            if (res[i].EventType == "会议") {
                $(thisRow).find(".selType").val(res[i].EventType);
                $(thisRow).find(".Dajianfei").val(res[i].Dajianfei);
                $(thisRow).find(".Changdifei").val(res[i].Changdifei);
                $(thisRow).find(".Huiyi").css("display", "inline");
            }
            else {
                $(thisRow).find(".selType").val(res[i].EventType);
                $(thisRow).find(".selYongcan").val(res[i].Yongcan);
                $(thisRow).find(".canbiao").val(res[i].canbiao);
                $(thisRow).find(".shuliang").eq(0).val(res[i].shuliangA);
                $(thisRow).find(".ddDanwei").eq(0).val(res[i].danweiA);
                $(thisRow).find(".jiushui").val(res[i].jiushui);
                $(thisRow).find(".shuliang").eq(1).val(res[i].shuliangB);
                $(thisRow).find(".ddDanwei").eq(1).val(res[i].danweiB);
                $(thisRow).find(".Yongcan").css("display", "inline");

            }

        }
        else {
            AddRow();
            var thisRow = $("#tableEvent").find("tr").eq(i + 1);
            $(thisRow).find(".selVenue").val(res[i].Venue.split(","));
            if (res[i].EventType == "会议") {
                $(thisRow).find(".selType").val(res[i].EventType);
                $(thisRow).find(".Dajianfei").val(res[i].Dajianfei);
                $(thisRow).find(".Changdifei").val(res[i].Changdifei);
                $(thisRow).find(".Huiyi").css("display", "inline");
            }
            else {
                $(thisRow).find(".selType").val(res[i].EventType);
                $(thisRow).find(".selYongcan").val(res[i].Yongcan);
                $(thisRow).find(".canbiao").val(res[i].canbiao);
                $(thisRow).find(".shuliang").eq(0).val(res[i].shuliangA);
                $(thisRow).find(".ddDanwei").eq(0).val(res[i].danweiA);
                $(thisRow).find(".jiushui").val(res[i].jiushui);
                $(thisRow).find(".shuliang").eq(1).val(res[i].shuliangB);
                $(thisRow).find(".ddDanwei").eq(1).val(res[i].danweiB);
                $(thisRow).find(".Yongcan").css("display", "inline");

            }
        }
    }
    $("#MainContent_hdTableEventJsons").val(JsonDatas);
    CalculateTotalPrice("2");
}

//////////////////////////////////////////////////////////////////////////
/// Events Table
//////////////////////////////////////////////////////////////////////////
 var number = 1;
function AddRow() {
        var tableEvent = $("#tableEvent");
        var rowNumber = tableEvent.find("tr").length;
        var td1 = "<select id='selType_"+number+"' class='selType' onchange='changeFeeType(this)' > \
                                        <option></option>\
                                        <option>会议</option>\
                                         <option>用餐</option>\
                                      </select>\
                                      &nbsp;&nbsp;<select id='selYongcan_" + number + "' class='Yongcan selYongcan'></select>";
        var td2 = "<select id='selVenue_" + number + "' class='muSelect form-control selVenue' multiple></select>";
        var td3 = "<div class='Huiyi'>搭建费：<input type='text' id='tbDajianfei_" + number + "' class='inputInTable Dajianfei' value='0'/></div>\
                     <div class='Yongcan'>餐标：<input type='text' id='tbCanbiao_" + number + "' class='inputInTable canbiao'  value='0'/> *\
                    <input type='text' id='tbCanbiaoShuoliang_" + number + "' class='inputInTable shuliang' value='0'/>\
                    &nbsp;<select id='ddCanbiaoDanwei_" + number + "' value='0' class='ddDanwei'><option> 桌</option><option>人</option></select></div>";
        var td4 = "<div class='Huiyi'>场地费：<input type='text' id='tbChangdifei_" + number + "' class='inputInTable Changdifei' value='0'/></div>\
                    <div class='Yongcan'>酒水：<input type='text' id='tbJiushui_" + number + "' class='inputInTable jiushui'  value='0'/> * \
                    <input type='text' id='tbJiushuiShuiliang_" + number + "' class='inputInTable shuliang' value='0'/>\
                     &nbsp;<select id='ddJiushuiDanwei_" + number + "' value='0' class='ddDanwei'><option>桌</option><option>人</option></select></div>";
        var lastRow = tableEvent.find("tr")[rowNumber - 1];
        var NewRow = $("<tr><td>" + td1 + "</td><td>" + td2 + "</td><td>" + td3 + "</td><td>" + td4 + "</td><td> <div class='btn btn-danger btn-sm' onclick='DeleteRow(this)'>删除</div></td></tr>");
        NewRow.appendTo(tableEvent);
        MakeSelect2($(NewRow));
        MakeSelectForVenueList($(NewRow));
        number++;

}
function DeleteRow(obj) {
    var row = $(obj).closest("tr");
    row.remove();
}

function changeFeeType(obj) {
    var val = $(obj).val();
    var row = $(obj).closest("tr");
    if (val == "会议") {
        row.find(".Huiyi").css("display", "inline");
        row.find(".Yongcan").css("display", "none");
    }
    else if (val == "用餐") {
        row.find(".Huiyi").css("display", "none");
        row.find(".Yongcan").css("display", "inline");
    }
    else {
        row.find(".Huiyi").css("display", "none");
        row.find(".Yongcan").css("display", "none");
    }

}



//////////////////////////////////////////////////////////////////////////
///Date && Select2
//////////////////////////////////////////////////////////////////////////
function MakeDatePicker() {
    $(".RiSelectNeedVal").datetimepicker({
        format: 'yyyy-mm-dd',
        autoclose: true,
        todayBtn: true,
        startView: "month",
        minView: "month",
        showMeridian: true

    }).change(function (e) {
        $('#MainForm').data('bootstrapValidator').revalidateField($(this)[0].name);
    });
    $(".TimeSelect").datetimepicker({
        format: 'yyyy-mm-dd hh:ii',
        autoclose: true,
        todayBtn: true,
        startView: "month",
        //minView: "day",
        showMeridian: true
    }).change(function (e) {
        $('#MainForm').data('bootstrapValidator').revalidateField($(this)[0].name);
    });
    $(".RiSelect").datetimepicker({
        format: 'yyyy-mm-dd',
        autoclose: true,
        todayBtn: true,
        startView: "month",
        minView: "month",
        showMeridian: true

    });
};

function MakeSelectForVenueList(obj) {
    var JsonObj = eval($("#MainContent_yanhuiList").text());
    if (obj != "") {
        var ddl = $(obj).find(".selYongcan");
        if ($(ddl).find("option").length == 0) {
            $.each(JsonObj, function (i, value) {
                $(ddl).append("<option>" + value.text + "</option>")
            });
        }
    }
    else {
        if ($(".selYongcan option").length == 0) {
            $.each(JsonObj, function (i, value) {
                //alert(value.text);
                $(".selYongcan").append("<option>" + value.text + "</option>")
            });
        }
    }
}

function MakeSelect2(obj) {
    var dataList = eval("(" + $("#MainContent_venueList").text() + ")");
    if (obj != '') {
        var ddl = $(obj).find(".selVenue");
        $(ddl).select2({
            placeholder: "",
            allowClear: true,
            data: dataList
        })
    }
    else {
        $(".selVenue").select2({
            placeholder: "",
            allowClear: true,
            data: dataList
        })
    }

    $(".SingleSelect").select2();
};

//////////////////////////////////////////////////////////////////////////
///Trigger
//////////////////////////////////////////////////////////////////////////
function ChangeTriggerForKehu() {
    var KehuID = $("#MainContent_ddKehu").val();
    if (KehuID != "") {
        $.ajax({
            async: false,
            url: "Shangwu.ashx",
            data: { "method": "getCustomerInfo", "CusID": KehuID, "dt": new Date().getTime() },
            success: function (data) {
                var res = eval("(" + data + ")");
                $("#MainContent_tbCompany").val(res.Company);
                $("#MainContent_tbQudao").val(res.Qudao);

            }

        });
    }
   
}

function CalculateTotalPrice(Ct) {
    var dabao = $("#MainContent_tbDabaoFee").val() * 1;
    var renshu = $("#MainContent_tbDabaoRen").val() * 1;
    var otherFee1 = $("#MainContent_OtherFeeValue1").val() * 1;
    var otherFee2 = $("#MainContent_OtherFeeValue2").val() * 1;
    var otherFee3 = $("#MainContent_OtherFeeValue3").val() * 1;
    var zheKou = $("#MainContent_tbZhekou").val() * 1;
    var Zongjine = dabao * renshu + otherFee1 + otherFee2 + otherFee3 - zheKou;
    var ZongJinePrice = Zongjine + CalculateEventTable()
    if (Ct == "1") {
        var FanPer = $("#MainContent_tbFanyongPer").val() / 100;
        var FanJine = ZongJinePrice * FanPer;
        $("#MainContent_tbFanyongAmount").val(FanJine.toFixed(0));
    }
    if (Ct == "2") {
        var FanJine = $("#MainContent_tbFanyongAmount").val();
        var FanPer = FanJine / ZongJinePrice * 100;
        $("#MainContent_tbFanyongPer").val(FanPer.toFixed(2));
    }

   
    $("#MainContent_tbHetongZongjine").val(ZongJinePrice);
    $("#MainContent_tbDaxieZongjine").val(digit_uppercase(ZongJinePrice));
   




}

function CalculateEventTable() {
    var rows = $("#tableEvent").find("tr");
    var Total = 0;
    $.each(rows, function (i, v) {
        var thisRow = rows[i];
        if (i > 0) {
            var type = $(thisRow).find(".selType").val();
            if (type == "会议") {
                var Dajianfei = $(thisRow).find(".Dajianfei").val() * 1;
                var Changdifei = $(thisRow).find(".Changdifei").val() * 1;
                Total += Dajianfei + Changdifei;
            }
            if (type == "用餐") {
                var Canbiao = $(thisRow).find(".canbiao").val() * 1;
                var RenshuA = $(thisRow).find(".shuliang").eq(0).val() * 1;

                var Jiushui = $(thisRow).find(".jiushui").val() * 1;
                var RenshuB = $(thisRow).find(".shuliang").eq(1).val() * 1;

                Total += Canbiao * RenshuA + Jiushui * RenshuB;
            }
        }
    })
    return Total;

}
//////////////////////////////////////////////////////////////////////////
/// CHECK
//////////////////////////////////////////////////////////////////////////
function CheckAll() {
    return CheckTableEvent() && CheckPayInfo();
}

function CheckTableEvent() {
    var tableEvent = $("#tableEvent");
    var rows = tableEvent.find("tr");
    var result = true;
    var JsonResult = "";
    var strJsonPay = "";
    for (var i = 1; i < rows.length; i++) {
        var typeEvent = $(rows[i]).find(".selType").val();
        var thisRow = $(rows[i]);
        if (typeEvent == "会议") {
            var venue = thisRow.find(".selVenue").val();
            var Dajianfei = thisRow.find(".Dajianfei").val();
            var Changdifei = thisRow.find(".Changdifei").val();
            if ($.trim(venue) == "") {
                alert("请选择场地");
                result = false;
                break;
            }

            if ($.trim(Dajianfei)=="" || $.trim(Changdifei)=="" ||isNaN(Dajianfei) || isNaN(Changdifei)) {
                alert("场地费或搭建费必须为数字，若无请填写 0");
                result = false;
                break;
            }
            strJsonPay = "{'EventType':'" + typeEvent + "','Venue':'" + venue + "','Dajianfei':'" + Dajianfei + "','Changdifei':'" + Changdifei + "'}";
            JsonResult += strJsonPay + ",";

        }
        else if (typeEvent == "用餐") {
            var venue = thisRow.find(".selVenue").val();
            var yongcan = thisRow.find(".Yongcan").val();
            var canbiao = thisRow.find(".canbiao").val();
            var shuliangA = thisRow.find(".shuliang").eq(0).val();
            var danweiA = thisRow.find(".ddDanwei").eq(0).val();
            var jiushui = thisRow.find(".jiushui").val();
            var shuliangB = thisRow.find(".shuliang").eq(1).val();
            var danweiB = thisRow.find(".ddDanwei").eq(1).val();
            if ($.trim(venue) == "") {
                alert("请选择场地");
                result = false;
                break;
            }
            if ($.trim(canbiao) == "" || isNaN(canbiao) || $.trim(shuliangA) == "" || isNaN(shuliangA)) {
                alert("餐标必须位数字，若无请填写 0");
                result = false;
                break;
            }
            if ($.trim(jiushui) == "" || isNaN(jiushui) || $.trim(shuliangB) == "" || isNaN(shuliangB)) {
                alert("餐标必须位数字，若无请填写 0");
                result = false;
                break;
            }
          strJsonPay = "{'EventType':'" + typeEvent + "','Yongcan':'"+yongcan+"','Venue':'" + venue + "','canbiao':'" + canbiao + "','shuliangA':'"+shuliangA+"','danweiA':'"+danweiA+"','jiushui':'" + jiushui + "','shuliangB':'"+shuliangB+"','danweiB':'"+danweiB+"'}";
           JsonResult += strJsonPay + ",";
        }
        else {
            alert("请选择活动类型");
            result = false;
            break;
        }
    }
    if (!result) {
     return result;
    }
    JsonResult = "["+ JsonResult.substring(0,JsonResult.length - 1) + "]";
    $("#MainContent_hdTableEventJsons").val(JsonResult);
    return result;
}

function CheckPayInfo() {
    if ($.trim($("#MainContent_tbDabaoFee").val()) == "" && $.trim($("#MainContent_tbDabaoRen").val()) == "") {
        alert("打包单价格式错误");
        return false;
    }
    if ($.trim($("#MainContent_OtherFeeName1").val()) != "" || $.trim($("#MainContent_OtherFeeValue1").val()) != "") {
        if ($.trim($("#MainContent_OtherFeeValue1").val()) == "" || isNaN($.trim($("#MainContent_OtherFeeValue1").val())) || $.trim($("#MainContent_OtherFeeName1").val()) == "") {
            alert("其他费用1的格式错误，请更正");
            return false;
        }
    }
    if ($.trim($("#MainContent_OtherFeeName2").val()) != "" || $.trim($("#MainContent_OtherFeeValue2").val()) != "") {
        if ($.trim($("#MainContent_OtherFeeValue2").val()) == "" || isNaN($.trim($("#MainContent_OtherFeeValue2").val())) || $.trim($("#MainContent_OtherFeeName2").val()) == "") {
            alert("其他费用1的格式错误，请更正");
            return false;
        }
    }
    if ($.trim($("#MainContent_OtherFeeName3").val()) != "" || $.trim($("#MainContent_OtherFeeValue3").val()) != "") {
        if ($.trim($("#MainContent_OtherFeeValue3").val()) == "" || isNaN($.trim($("#MainContent_OtherFeeValue3").val())) || $.trim($("#MainContent_OtherFeeName3").val()) == "") {
            alert("其他费用1的格式错误，请更正");
            return false;
        }
    }

    if ($.trim($("#MainContent_tbZhekou").val()) == "" || isNaN($("#MainContent_tbZhekou").val())) {
        alert("折扣金额必须为数字，若无请填写0");
        return false;
    }

    if (isNaN($("#MainContent_tbFanyongPer").val())) {
        alert("返佣率必须为数字，若无请填写0");
        return false;
    }
    if (($.trim($("#MainContent_tbFanyongAmount").val()) == "" || isNaN($("#MainContent_tbFanyongAmount").val()))) {
        alert("返佣金额必须为数字，若无请填写0");
        return false;
    }

}

function checkNewRequestForm() {
    $('#MainForm').bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            'ctl00$MainContent$tbHetongRiqi': {
                validators: {
                    notEmpty: {
                        message: '合同日期不能为空'
                    }
                }
            },
            'ctl00$MainContent$ddKehu': {
                validators: {
                    notEmpty: {
                        message: '客户不能为空'
                    }
                }
            },
            'ctl00$MainContent$tbHetongBianhao': {
                validators: {
                    notEmpty: {
                        message: '合同编号不能为空'
                    },
                    remote: {
                        url: "Shangwu.ashx?method=checkHetongBianhao&id=" + $("#MainContent_hdID").val() + "&dt=" + new Date().getTime(),
                        type: "get",
                        message: '合同号已被用',
                        delay: 2000
                    }
                }

            },

            'ctl00$MainContent$tbLianxiren1': {
                validators: {
                    notEmpty: {
                        message: '联系人1不能为空'
                    }

                }
            },
            'ctl00$MainContent$tbCompany': {
                validators: {
                    notEmpty: {
                        message: '公司名不能为空'
                    }
                }
            },
            'ctl00$MainContent$tbLianxiren1_Shouji': {
                validators: {
                    notEmpty: {
                        message: '手机不能为空'
                    }

                }
            },
            'ctl00$MainContent$ddEventVenue': {
                validators: {
                    notEmpty: {
                        message: '活动地点不能为空'
                    }

                }
            },
            'ct100$MainContent$tbEventName': {
                validators: {
                    notEmpty: {
                        message: '活动名称不能为空'
                    }

                }
            },
            'ctl00$MainContent$tbEventRiqi': {
                validators: {
                    notEmpty: {
                        message: '活动时间不能为空'
                    }

                }
            },
            'ctl00$MainContent$ddSales': {
                validators: {
                    notEmpty: {
                        message: '销售不能为空'
                    }

                }
            },
            'ctl00$MainContent$ddEventType': {
                validators: {
                    notEmpty: {
                        message: '活动类型不能为空'
                    }

                }
            }
        }
    });

};

//////////////////////////////////////////////////////////////////////////
/// Other
//////////////////////////////////////////////////////////////////////////
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
