/// <reference path="../Scripts/jquery.min.js">
function SetVenueDateByCustomerID() {
    $("#MainContent_ddKehu").change(function () {
        $.ajax({
            url: "HunliAjax.ashx",
            data: { "method": "getVenueDate", "selectItem": $("#MainContent_ddKehu").val(), "dt": new Date().getTime() },
            success: function (data) {
                if (data != "") {
                    var res = eval("(" + data + ")");
                    $("#MainContent_tbHunliRiqi").val(res.riqi);
                    $("#MainContent_tbQudao").val(res.source);
                }
            }
        });
    })
}

function SetCaijinDanjia() {
        var obj = $("#MainContent_ddHunliTaocan").val();
        $.ajax({
            url: "HunliAjax.ashx",
            async:false,
            data: { "method": "getCaijinDanjia", "selectItem": obj, "dt": new Date().getTime() },
            success: function (data) {
                $("#MainContent_lbCaijinDanjia").val(data);
            }

        });
}

function SetCaijingZongjine() {
   
        var zhuoshu = $("#MainContent_tbCaijinZhuoshu").val();

        var danjia = $("#MainContent_lbCaijinDanjia").val();
        if (danjia == "") {
            danjia = $("#MainContent_lbCaijinDanjia").text();
            }
            $("#MainContent_lbCaijinZongjine").text((zhuoshu * danjia).toString());

        
}
function SetCaijingZhekou() {
  
        var zhekou = $("#MainContent_tbCaijinZhekou").val();
        var zongJine = $("#MainContent_lbCaijinZongjine").text();
        $("#MainContent_lbCaijinZhekoulv").text((zhekou / zongJine * 100).toFixed(1) + "%");
        $("#MainContent_lbCaijinZhehoujine").text((zongJine-zhekou).toString());
        
}

function SetJiushuiZongjine() {
   
        var danjia = $("#MainContent_tbJiushuiDanjia").val();
        var zhuoshu = $("#MainContent_tbJiushuiZhuoshu").val();
        $("#MainContent_lbJiushuiZongjine").text((danjia * zhuoshu).toString());
        
}

function SetJiushuiZhekou() {
   
        var zhekou = $("#MainContent_tbJiushuiZhekou").val();
        var zongjine = $("#MainContent_lbJiushuiZongjine").text();
        $("#MainContent_lbJiushuiZHekoulv").text((zhekou / zongjine * 100).toFixed(1) + "%");
        $("#MainContent_lbJiushuiZhehoujine").text((zongjine - zhekou).toString());
        
}

function SetHunyanZong() {
    var CaijingZong = $("#MainContent_lbCaijinZongjine").text();
    var CaijingZhekou = $("#MainContent_tbCaijinZhekou").val();

    var JiushuiZong = $("#MainContent_lbJiushuiZongjine").text();
    var JiushuiZhekou = $("#MainContent_tbJiushuiZhekou").val();

    var zongJine = CaijingZong * 1 + JiushuiZong * 1;
    var zongZhekou = CaijingZhekou * 1 + JiushuiZhekou * 1;

    $("#MainContent_lbZongjine").text(zongJine.toString());
    $("#MainContent_lbZongZhekou").text(zongZhekou.toString());
    $("#MainContent_lbZongZhekoulv").text((zongZhekou / zongJine * 100).toFixed(1) + "%");
    $("#MainContent_lbZhehouZongjine").text((zongJine - zongZhekou).toString());

    var Hunyanzong = zongJine - zongZhekou;
    $("#MainContent_hdHunyanZongjine").val(Hunyanzong.toString());
    $("#MainContent_lbHunyanZongjine").text(Hunyanzong.toString());
    $("#MainContent_lbDaxieHunyanZongjine").text(digit_uppercase(Hunyanzong));   
}


function setHunqinZong() {
    var hunqin = $("#MainContent_tbHunqin").val() * 1;
    var zhuohua = $("#MainContent_tbZhuohua").val() * 1;
    var Qita = $("#MainContent_tbQita").val() * 1;
    var Hunqinzong = hunqin + zhuohua + Qita;
    $("#MainContent_lbHunqinZongjine").text(Hunqinzong.toString());
    $("#MainContent_lbDaxieHunqinZongjine").text(digit_uppercase(Hunqinzong));
}

function setZong() {
    var hunyan = $("#MainContent_lbHunyanZongjine").text() * 1;
    var hunqin = $("#MainContent_lbHunqinZongjine").text() * 1;
    var zong = hunyan + hunqin;
    $("#MainContent_lbXieyiZongjine").text(zong.toString());
    $("#MainContent_lbDaxieXieyiZongjine").text(digit_uppercase(zong));

    setPay("J", "First", "HY");
    setPay("J", "Second", "HY");
    setPay("J", "Third", "HY");

    setPay("J", "First", "HQ");
    setPay("J", "Second", "HQ");
    setPay("J", "Third", "HQ");

}

function setZongxiaofei_Jinge() {
    var exHY = 0;
    var exHQ = 0;
    var xieyiHY = $("#MainContent_lbHunyanZongjine").text() * 1; //lbHunqinZongjine
    var xieyiHQ = $("#MainContent_lbHunqinZongjine").text() * 1;
    
    $("#MainContent_lbExJingeJS").text(($("#MainContent_tbExJiushui").val() * $("#MainContent_tbExJS_ZS").val()) * 1);
    $("#MainContent_lbExJingeCJ").text(($("#MainContent_tbExCaijin").val() * $("#MainContent_tbExCJ_ZS").val()) * 1);
    exHY = ($("#MainContent_tbExJiushui").val() * $("#MainContent_tbExJS_ZS").val() + $("#MainContent_tbExCaijin").val() * $("#MainContent_tbExCJ_ZS").val() - $("#MainContent_tbWeikuanDikou").val())*1;
    exHQ = ($("#MainContent_tbExZhuohua").val()*1 + $("#MainContent_tbExQita").val()*1);

    $("#MainContent_lbXiaojiHY").text(exHY.toFixed(1));
    $("#MainContent_lbXiaojiHQ").text(exHQ.toFixed(1));
    $("#MainContent_lbZXF_HY").text((xieyiHY + exHY).toFixed(1));
    $("#MainContent_lbZXF_HQ").text((xieyiHQ + exHQ).toFixed(1));
    $("#MainContent_lbZXF_heji").text((xieyiHQ + exHQ+ xieyiHY + exHY).toFixed(1));
    

}

function setPay(obj,orders,Htype) {
    var HyJine = $("#MainContent_tb" + orders + "PayJine" + Htype).val() * 1;
    var HyPercent = $("#MainContent_tb" + orders + "PayBai" + Htype).val() / 100;
    if (Htype == "HY") {
        var HyZong = $("#MainContent_lbHunyanZongjine").text() * 1;
    }
    if (Htype == "HQ") {
        var HyZong = $("#MainContent_lbHunqinZongjine").text() * 1;
    }
  
    if (obj == "P") {
        $("#MainContent_tb" + orders + "PayJine"+Htype).val((HyZong * HyPercent).toFixed(1).toString());
        $("#MainContent_lbDaixie" + orders + "pay" + Htype).text(digit_uppercase(HyZong * HyPercent));
        
    }
    if (obj == "J") {
        $("#MainContent_tb" + orders + "PayBai"+Htype).val((HyJine / HyZong * 100).toFixed(2).toString());
        $("#MainContent_lbDaixie" + orders + "pay" + Htype).text(digit_uppercase(HyJine));
//        if ($("#MainContent_tb" + orders + "PayBai" + Htype).val() == "0.00") {
//            $("#MainContent_tb" + orders + "PayJine" + Htype).val((HyZong * HyPercent).toFixed(1).toString());
//            $("#MainContent_lbDaixie" + orders + "pay" + Htype).text(digit_uppercase(HyZong * HyPercent));
//        }
    }
    
    
}



function ListenTextboxsOnVenueAndDate() {
    $("#MainContent_ddKehu").change(function () {
        setDateLevel();
    });
    $("#MainContent_tbHunliRiqi").change(function () {
        setDateLevel();
    });

    $("#MainContent_ddYanhuiting").change(function () {
        setDateLevel();
    })
}

function ListenTextboxsOnKuangXiang() {
    $("#MainContent_ddHunliTaocan").change(function () {
        SetCaijinDanjia();
        SetCaijingZongjine();
        SetCaijingZhekou();
        SetHunyanZong();
        setZong();

    })

    $("#MainContent_lbCaijinDanjia").bind('input propertychange', function () {
        SetCaijingZongjine();
        SetCaijingZhekou();
        SetHunyanZong()
        setZong();

    })

    $("#MainContent_tbCaijinZhuoshu").bind('input propertychange', function () {
        SetCaijingZongjine();
        SetCaijingZhekou();
        SetHunyanZong()
        setZong();

    })
    $("#MainContent_tbCaijinZhekou").bind('input propertychange', function () {
        SetCaijingZhekou();
        SetHunyanZong();
        setZong();
    })
    $("#MainContent_tbJiushuiZhuoshu").bind('input propertychange', function () {
        SetJiushuiZongjine();
        SetJiushuiZhekou();
        SetHunyanZong();
        setZong();
    })
    $("#MainContent_tbJiushuiZhekou").bind('input propertychange', function () {
        SetJiushuiZhekou();
        SetHunyanZong();
        setZong();
    });
    $("#MainContent_tbHunqin").bind('input propertychange', function () {
        setHunqinZong();
        setZong();
    });
    $("#MainContent_tbZhuohua").bind('input propertychange', function () {
        setHunqinZong();
        setZong();
    });
    $("#MainContent_tbQita").bind('input propertychange', function () {
        setHunqinZong();
        setZong();
    });
}

function ListenTextboxsOnExPay() {
    $("#MainContent_tbExJiushui").on("input propertychange", function () {
        setZongxiaofei_Jinge();
    });
    $("#MainContent_tbExJS_ZS").on("input propertychange", function () {
        setZongxiaofei_Jinge();
    });
    $("#MainContent_tbExCaijin").on("input propertychange", function () {
        setZongxiaofei_Jinge();
    });
    $("#MainContent_tbExCJ_ZS").on("input propertychange", function () {
        setZongxiaofei_Jinge();
    });
    $("#MainContent_tbWeikuanDikou").on("input propertychange", function () {
        setZongxiaofei_Jinge();
    });
    $("#MainContent_tbExZhuohua").on("input propertychange", function () {
        setZongxiaofei_Jinge();
    });

    $("#MainContent_tbExQita").on("input propertychange", function () {
        setZongxiaofei_Jinge();
    });
}

function initPageIfhasID() {
    SetCaijingZongjine();
    SetCaijingZhekou();
    SetJiushuiZongjine();
    SetJiushuiZhekou();
    SetHunyanZong();
    setHunqinZong();
    setZong();
    BindRevision();

    setDateLevel();
    AjaxForAuditNeed();
}

function ListenTextboxsOnPayment() {
    $("#MainContent_tbFirstPayBaiHY").bind('input propertychange', function () {
        setPay("P", "First", "HY");
    });
    $("#MainContent_tbFirstPayJineHY").bind('input propertychange', function () {
        setPay("J", "First", "HY");
    });

    $("#MainContent_tbSecondPayBaiHY").bind('input propertychange', function () {
        setPay("P", "Second", "HY");
    });
    $("#MainContent_tbSecondPayJineHY").bind('input propertychange', function () {
        setPay("J", "Second", "HY");
    });

    $("#MainContent_tbThirdPayBaiHY").bind('input propertychange', function () {
        setPay("P", "Third", "HY");
    });
    $("#MainContent_tbThirdPayJineHY").bind('input propertychange', function () {
        setPay("J", "Third", "HY");
    });


    $("#MainContent_tbFirstPayBaiHQ").bind('input propertychange', function () {
        setPay("P", "First", "HQ");
    });
    $("#MainContent_tbFirstPayJineHQ").bind('input propertychange', function () {
        setPay("J", "First", "HQ");
    });

    $("#MainContent_tbSecondPayBaiHQ").bind('input propertychange', function () {
        setPay("P", "Second", "HQ");
    });
    $("#MainContent_tbSecondPayJineHQ").bind('input propertychange', function () {
        setPay("J", "Second", "HQ");
    });

    $("#MainContent_tbThirdPayBaiHQ").bind('input propertychange', function () {
        setPay("P", "Third", "HQ");
    });
    $("#MainContent_tbThirdPayJineHQ").bind('input propertychange', function () {
        setPay("J", "Third", "HQ");
    });
}

function ThrowError(str)
{
    alert("发生错误，错误代码：" + str);
    SetVenueDateByCustomerID();
    $("#MainContent_ddKehu").trigger("change");
    SetCaijinDanjia();
    initPageIfhasID();
}

function SetButtonAddServices() {
    $('.tokenfield').tokenfield();
    $("#btPopoverHY").popover({
        html: true,
        container: 'body',
        placement: "bottom",
        content: "<div class=' form-group-sm'>\
                服务项目：<select id='fwxmHY' name='fwxmHY' class='form-control' style='width: 200px'></select>\
                备注1：<input type='text' class='form-control' style='width: 200px' id='HYbz1' />\
                <small>可输入项目的类别等</small><br />\
                备注2：<input type='text' class='form-control' style='width: 200px' id='HYbz2' />\
                <small>可输入项目的券号等</small><br />\
                <div class='btn btn-danger btn-sm' id='btAddHY' onclick=addServices('HY')> 确定</div>  <div class='btn btn-danger btn-sm' id='btCloseHY' onclick=\"closeServices('HY')\">关闭</div></div>"
    }).on('shown.bs.popover', function () {
        if ($("#fwxmHY").html() == "") {
            addOptionsForHYservices();
        }

    });

    $("#btPopoverHQ").popover({
        html: true,
        container: 'body',
        placement: "bottom",
        content: "<div class=' form-group-sm'>\
                    服务项目：<select id='fwxmHQ' name='fwxmHQ' class='form-control' style='width: 200px'></select>\
                    备注1：<input type='text' class='form-control' style='width: 200px' id='HQbz1' />\
                    <small>可输入项目的类别等</small><br />\
                    备注2：<input type='text' class='form-control' style='width: 200px' id='HQbz2' />\
                    <small>可输入项目的券号等</small><br />\
                    <div class='btn btn-danger btn-sm' id='btAddHQ' onclick=\"addServices('HQ')\">确定</div>  <div class='btn btn-danger btn-sm' id='btCloseHQ' onclick=\"closeServices('HQ')\">关闭</div></div>"
    }).on('shown.bs.popover', function () {
        if ($("#fwxmHQ").html() == "") {
            addOptionsForHQservices();
        }

    });
}

function BindRevision() {
    var r = AjaxRevision();
    if (r) {
        $("#lbRevision").popover({
            html: true,
            placement: 'bottom',
            content: $("#divRevision").html()
        });
    }
  
}

function AjaxRevision() {
    var result = false;
    if ($("#ulRevision").html() == undefined || $("#ulRevision").html() == "") {
        $.ajax({
            url: "HunliAjax.ashx",
            async: false,
            data: { "ID": $("#MainContent_hdID").val(), "method": "getRevision", "dt": new Date().getTime() },
            success: function (data) {
                if (data != "") {
                    var res = eval("(" + data + ")");
                    $.each(res.Revs, function (i, item) {
                        //<a href='#' class='list-group-item active'></a>
                        var node = "<a href='HunliEdit.aspx?id=" + item.id + "' class='list-group-item'>" + item.orderName + "</a>";
                        if (item.id == $("#MainContent_hdID").val()) {
                            node = "<a href='HunliEdit.aspx?id=" + item.id + "' class='list-group-item active'>" + item.orderName + "</a>";
                        }
                        $("#ulRevision").append($(node));
                    })
                    $("#spanNumRevision").text("(" + res.Revs.length + ")");
                    result = true;
                }
            }

        })
    }
    return result;
}



//检测
function checkForm() {
    $('#MainForm').bootstrapValidator({
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            'ctl00$MainContent$tbHetongBianhao': {
                validators: {
                    notEmpty: {
                        message: '合同编号不能为空'
                    },
                    remote: {
                        url: "HunliAjax.ashx?method=checkHetongBianhao&id=" + $("#MainContent_hdID").val() + "&dt=" + new Date().getTime(),
                        type: "get",
                        message: '合同号已被用',
                        delay: 2000
                    }
                }

            },
            'ctl00$MainContent$ddKehu': {
                validators: {
                    notEmpty: {
                        message: '请选择客户'
                    }
                }
            },
            'ctl00$MainContent$ddHunliDidian': {
                validators: {
                    notEmpty: {
                        message: '请选择婚礼地点'
                    }
                }
            },
            'ctl00$MainContent$ddSales': {
                validators: {
                    notEmpty: {
                        message: '请选择销售'
                    }
                }
            },
            'ctl00$MainContent$tbHetongRiqi': {
                validators: {
                    notEmpty: {
                        message: '合同日期不能为空'
                    }
                }
            },
            'ctl00$MainContent$tbXinLangName': {
                validators: {
                    notEmpty: {
                        message: '新郎名称不能为空'
                    }
                }
            },
            'ctl00$MainContent$tbXinLangShouji': {
                validators: {
                    notEmpty: {
                        message: '新郎手机不能为空'
                    }
                }
            },
            'ctl00$MainContent$tbXinNiangName': {
                validators: {
                    notEmpty: {
                        message: '新娘姓名不能为空'
                    }
                }
            },
            'ctl00$MainContent$tbXinNiangShouji': {
                validators: {
                    notEmpty: {
                        message: '新娘手机不能为空'
                    }
                }
            },
            'ctl00$MainContent$tbHunliRiqi': {
                validators: {
                    notEmpty: {
                        message: '婚礼日期不能为空'
                    }
                }
            },
            'ctl00$MainContent$ddYishiChangdi': {
                validators: {
                    notEmpty: {
                        message: '仪式场地不能为空'
                    }
                }
            },
            'ctl00$MainContent$ddWuWanCan': {
                validators: {
                    notEmpty: {
                        message: '午晚餐不能为空'
                    }
                }
            },
            'ctl00$MainContent$ddHunliTaocan': {
                validators: {
                    notEmpty: {
                        message: '婚礼套餐不能为空'
                    }
                }
            }
        }
    });

};

function setDateLevel() {
    var txtDate = $("#MainContent_tbHunliRiqi").val();
    var txtVenue = $("#MainContent_ddYanhuiting").val();
    if (txtDate == "" || txtVenue == "" || txtVenue == null) {
        $("#divDateLevel").fadeOut();
        return;
    }


    $.ajax({
        url: "HunliAjax.ashx",
        async: false,
        data: { "method": "getPriceByDateLevel", "Date": txtDate, "Venue": txtVenue.toString(), "dt": new Date().getTime() },
        success: function (data) {
            var res = eval("(" + data + ")");
            //$("#divDateLevel").html("当前选择的最低消费标准为：" + res.price + " ( " + res.venue + " 的 " + res.level + " 档期,当日为 " + res.cate + " )");
            //$("#divDateLevel").fadeIn(1000);
            $("#MainContent_hdStandPrice").val(res.price);
            if (res.error == "1") {
                $("#divDateLevel").html("当前档期有误，请联系管理员，订单无法提交 ");
                $("#MainContent_btSubmit").attr("disabled", "disabled");
            }
            else {
                $("#MainContent_btSubmit").removeAttr("disabled");
            }
        }
    });
}

function checkStandPrice() {
    var interval;
    var $originalContent = $("#MainContent_lbHunyanZongjine").text();
    interval = setInterval(function () {
        if ($originalContent != $("#MainContent_lbHunyanZongjine").text()) {
            //console.log('content changed');
            AjaxForAuditNeed();
            $originalContent = $("#MainContent_lbHunyanZongjine").text();
        };

    });
}

function AjaxForAuditNeed() {
    $.ajax({
        url: "HunliAjax.ashx",
        async: false,
        data: { "currentPrice": $("#MainContent_lbHunyanZongjine").text(), "standardPrice": $("#MainContent_hdStandPrice").val(), "method": "getAuditNeed" },
        success: function (data) {
            if (data != "") {
                $("#smallAlert").empty();
               
                 if ( data == "error") {
                     node = "<small style='color:Red'>提醒：审批标准价错误 不可提交！</small><br />";
                     $("#smallAlert").append($(node));
                }
                else {
                    var res = eval("(" + data + ")");
                    $.each(res.AuditNeed, function (i, item) {
                        var node = "";
                        node += " <small style='color:Red'>提醒：婚宴总金额 需" + item.Role + "审核。</small><br />";
                        //if (item.Role == "不可提交") {
                        //    node = " <small style='color:Red'>提醒：婚宴总金额 低于（" + item.Max + "） 不可提交！</small><br />"
                        //}
                        //else if (item.Role == "财务" || item.Role == "文员") {
                        //    node = " <small style='color:Red'>提醒：婚宴总金额 高于（" + res.StandardPrice + "） 需" + item.Role + "审核。</small><br />";
                        //}
                        //else {
                        //    node = " <small style='color:Red'>提醒：婚宴总金额 高于（" + item.Min + "），低于(" + item.Max + ") 需" + item.Role + "审核。</small><br />";
                        //}
                        $("#smallAlert").append($(node));

                    })
                }
            }
        }

    });

}

function checkPayment() {
    if ($("#MainContent_tbFirstPayDate").val() == "") {
        alert("请填写定金支付日期");
        return false;
    }
    if($("#MainContent_tbFirstPayBaiHY").val() == "" || isNaN($("#MainContent_tbFirstPayBaiHY").val())){
        alert("婚宴定金百分比错误");
        return false;
    }
    if ($("#MainContent_tbFirstPayJineHY").val() == "" || isNaN($("#MainContent_tbFirstPayJineHY").val())) {
        alert("婚宴定金金额错误");
        return false;
    }
    if ($("#MainContent_tbFirstPayBaiHQ").val() == "" || isNaN($("#MainContent_tbFirstPayBaiHQ").val())) {
        alert("婚庆定金百分比错误");
        return false;
    }
    if ($("#MainContent_tbFirstPayJineHQ").val() == "" || isNaN($("#MainContent_tbFirstPayJineHQ").val())) {
        alert("婚庆定金金额错误");
        return false;
    }

    if ($("#MainContent_tbSecondPayDate").val() == "") {
        alert("请填写中款支付日期");
        return false;
    }
    if ($("#MainContent_tbSecondPayBaiHY").val() == "" || isNaN($("#MainContent_tbSecondPayBaiHY").val())) {
        alert("婚宴中款百分比错误");
        return false
    }
    if ($("#MainContent_tbSecondPayJineHY").val() == "" || isNaN($("#MainContent_tbSecondPayJineHY").val())) {
        alert("婚宴中款金额错误");
        return false
    }
    if ($("#MainContent_tbSecondPayBaiHQ").val() == "" || isNaN($("#MainContent_tbSecondPayBaiHQ").val())) {
        alert("婚庆中款百分比错误");
        return false
    }
    if ($("#MainContent_tbSecondPayJineHQ").val() == "" || isNaN($("#MainContent_tbSecondPayJineHQ").val())) {
        alert("婚庆中款金额错误");
        return false
    }

    if ($("#MainContent_tbThirdPayDate").val() == "") {
        alert("请填写尾款支付日期");
        return false;
    }
    if ($("#MainContent_tbThirdPayBaiHY").val() == "" || isNaN($("#MainContent_tbThirdPayBaiHY").val())) {
        alert("婚宴尾款百分比错误");
        return false
    }
    if ($("#MainContent_tbThirdPayJineHY").val() == "" || isNaN($("#MainContent_tbThirdPayJineHY").val())) {
        alert("婚宴尾款金额错误");
        return false
    }
    if ($("#MainContent_tbThirdPayBaiHQ").val() == "" || isNaN($("#MainContent_tbThirdPayBaiHQ").val())) {
        alert("婚庆尾款百分比错误");
        return false
    }
    if ($("#MainContent_tbThirdPayJineHQ").val() == "" || isNaN($("#MainContent_tbThirdPayJineHQ").val())) {
        alert("婚庆尾款金额错误");
        return false
    }

}

////////
///公用
////////


function MakeSelect2() {
    $(".muSelect").select2({
        placeholder: "",
        allowClear: true
    })
    $(".SingleSelect").select2();
};
function Select2Sel(str) {
    $(".muSelect").val(str.split(",")).trigger("change");
};


function addOptionsForHYservices() {
   
    $.ajax({
        url: "HunliAjax.ashx",
        async: false,
        data: { "method": "getOptionsForHYservices" },
        success: function (data) {
            var obj = eval("(" + data + ")");
            $.each(obj.Options, function (i, item) {
                $("#fwxmHY").append("<option>"+item+"</option>"); 
            })
        }
    });
}

function addOptionsForHQservices() {

    $.ajax({
        url: "HunliAjax.ashx",
        async: false,
        data: { "method": "getOptionsForHQservices" },
        success: function (data) {
            var obj = eval("(" + data + ")");
            $.each(obj.Options, function (i, item) {
                $("#fwxmHQ").append("<option>" + item + "</option>");
            })
        }
    });
}

function addServices(obj) {
    if (obj == "HY") {
        var txt = $('#fwxmHY').val() + " " + $('#HYbz1').val() + " " + $('#HYbz2').val();
        $('#MainContent_tokenfieldHY').tokenfield('createToken', txt);
    }
    if (obj == "HQ") {
        var txt = $('#fwxmHQ').val() + " " + $('#HQbz1').val() + " " + $('#HQbz2').val();
        $('#MainContent_tokenfieldHQ').tokenfield('createToken', txt);
    }
}
function closeServices(obj) {
    if (obj == "HY") {
        $("#btPopoverHY").popover("hide");
    }
    if (obj == "HQ") {
        $("#btPopoverHQ").popover("hide");
    }
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