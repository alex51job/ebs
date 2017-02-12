/// <reference path="../Scripts/jquery.min.js">
$(function () {
    btAddBind();
    btAddExBind();
    btAddTKBind();
    makeRiSelect();
    $(document).on("click", ".btnSave", function () {
        var trThis = $(this).closest("tr");
        if ($(trThis).find("#ddlKuanXiang").val() == "") {
            alert("请选择款项");
            return false;
        }
        if ($(trThis).find("#PayDate").val() == "") {
            alert("请选择日期");
            return false;
        }
        if ($(trThis).find("#tbShishouHY").val() == "") {
            alert("请填写婚宴金额");
            return false;
        }
        if ($(trThis).find("#tbShishouHQ").val() == "") {
            alert("请填写婚庆金额");
            return false;
        }
        showLoad();
        $.ajax({
            url: "HLPaymentAjax.ashx?method=save&orderID=" + $("#MainContent_hdID").val() + "&dt=" + new Date().getTime(),
            type: "post",
            async: false,
            data: { "payType": $(trThis).find("#ddlKuanXiang").val(), "payDate": $(trThis).find("#PayDate").val(), "shishouHY": $(trThis).find("#tbShishouHY").val(), "shishouHQ": $(trThis).find("#tbShishouHQ").val(), "id": $(trThis).find("#payID").val(),"PayOrderNumber":$(trThis).find("#PayOrderNumber").val() },
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
                alert(e);
            }
        })
        hideLoad();
    });

    $(document).on("click", ".btnSubmit", function () {
        var trThis = $(this).closest("tr");
        if ($(trThis).find("#ddlKuanXiang").val() == "") {
            alert("请选择款项");
            return false;
        }
        if ($(trThis).find("#PayDate").val() == "") {
            alert("请选择日期");
            return false;
        }
        if ($(trThis).find("#tbShishouHY").val() == "") {
            alert("请填写婚宴金额");
            return false;
        }
        if ($(trThis).find("#tbShishouHQ").val() == "") {
            alert("请填写婚庆金额");
            return false;
        }
        if (confirm("确认提交财务审批吗？")) {
            showLoad();
            $.ajax({
                url: "HLPaymentAjax.ashx?method=submit&orderID=" + $("#MainContent_hdID").val() + "&dt=" + new Date().getTime(),
                type: "post",
                async: false,
                data: { "payType": $(trThis).find("#ddlKuanXiang").val(), "payDate": $(trThis).find("#PayDate").val(), "shishouHY": $(trThis).find("#tbShishouHY").val(), "shishouHQ": $(trThis).find("#tbShishouHQ").val(), "id": $(trThis).find("#payID").val(), "PayOrderNumber": $(trThis).find("#PayOrderNumber").val() },
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
                    url: "HLPaymentAjax.ashx?method=delete&dt=" + new Date().getTime(),
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
            reflashYingshou();
            hideLoad("slow");
        }

    });


})

function initPageIfhasID(data) {
    var res = data;
    $.each(res.payments, function (i, item) {
        if (item.payType == "e") {
            var node = "<tr style='vertical-align: middle' id='payContent'><td style='vertical-align: middle'>\
                                 增加项<input type='hidden' id='ddlKuanXiang' value='e'/>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                <input type='text' class='RiSelect OnlyBottom' id='PayDate' name='PayDate'   value='" + item.payDate + "'/>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                <input type='text' class='OnlyBottom' id='PayOrderNumber' name='PayOrderNumber'   value='" + item.PayOrderNumber + "'/>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                婚宴实收&nbsp;&nbsp;<input type='text' id='tbShishouHY' name='tbShishouHY' oninput='JisuanShishou(this,\"HY\")' onpropertychange=''JisuanShishou(this,\"HY\")' class='OnlyBottom tbShishouHY' value='" + item.shishouHY + "'/>\
                                <br />\
                                <br />\
                                婚庆实收&nbsp;&nbsp;<input type='text' id='tbShishouHQ' name='tbShishouHQ' oninput='JisuanShishou(this,\"HQ\")' onpropertychange=''JisuanShishou(this,\"HQ\")' class='OnlyBottom tbShishouHQ' value='" + item.shishouHQ + "'/>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                婚宴应收&nbsp;&nbsp;\
                                <label id='lbYingshouHY' name='lbYingshouHY' class='Label80px'>" + item.NeedPayHY + "</label>&nbsp;&nbsp;\
                                <label id='lbYingshouHYB' name='lbYingshouHYB' class='Label30px'>" + item.BaiHY + "</label>&nbsp;&nbsp;\
                                婚宴实收&nbsp;&nbsp;\
                                <label id='lbShishouHY' name='lbShishouHY' class='Label80px'></label>&nbsp;&nbsp;\
                                <label id='lbShishouHYB' class='Label30px'></label>\
                                <br />\
                                <br />\
                                婚庆应收&nbsp;&nbsp;\
                                <label id='lbYingshouHQ' name='lbYingshouHQ' class='Label80px'>" + item.NeedPayHQ + "</label>&nbsp;&nbsp;\
                                <label id='lbYingshouHQB' name='lbYingshouHQB' class='Label30px'>" + item.BaiHQ + "</label>&nbsp;&nbsp;\
                                婚庆实收&nbsp;&nbsp;\
                                <label id='lbShishouHQ' name='lbShishouHQ' class='Label80px'></label>&nbsp;&nbsp;\
                                <label id='lbShishouHQB' class='Label30px'></label>\
                            </td>\
                            <td style='vertical-align: middle'>\
                              <label id='zhuangtai'>" + item.zhuangtai + "</label>\
                            </td>\
                            <td style='vertical-align: middle;'>\
                              <input type='hidden' id='payID' name='payID' value='" + item.id + "' />\
                              <div id='btnSave' class='btn btn-default btn-sm btnSave " + (item.zhuangtai == '可编辑' ? "" : "hide") + "' title='保存'><i class='fa fa-save' style='display:block'></i></div>\
                                <div id='btnSubmit' class='btn btn-default btn-sm btnSubmit " + (item.zhuangtai == '可编辑' ? "" : "hide") + "' title='提交'><i class='fa fa-upload' style='display:block'></i></div>\
                                <div class='btn btn-default btn-sm btnDelete " + (item.zhuangtai == '审批完成' ? "hide" : "") + "' title='删除'><i class='fa fa-ban' style='display:block'></i></div>\
                            </td></tr>"
            $(node).insertBefore($("#SummaryAddtional"));
        } else {
            var node = "<tr style='vertical-align: middle' id='payContent'><td style='vertical-align: middle'>\
                                <select id='ddlKuanXiang' name='ddlKuanXiang' style='width: 60px;' onchange='setPaymentType(this)'>\
                                <option></option>\
                                <option value='1'" + (item.payType == '1' ? 'selected' : '') + ">首款</option>\
                                <option value='2' " + (item.payType == '2' ? 'selected' : '') + ">中款</option>\
                                <option value='3'" + (item.payType == '3' ? 'selected' : '') + ">尾款</option>\
                                </select>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                <input type='text' class='RiSelect OnlyBottom' id='PayDate' name='PayDate'   value='" + item.payDate + "'/>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                <input type='text' class='OnlyBottom' id='PayOrderNumber' name='PayOrderNumber'   value='" + item.PayOrderNumber + "'/>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                婚宴实收&nbsp;&nbsp;<input type='text' id='tbShishouHY' name='tbShishouHY' oninput='JisuanShishou(this,\"HY\")' onpropertychange=''JisuanShishou(this,\"HY\")' class='OnlyBottom tbShishouHY' value='" + item.shishouHY + "'/>\
                                <br />\
                                <br />\
                                婚庆实收&nbsp;&nbsp;<input type='text' id='tbShishouHQ' name='tbShishouHQ' oninput='JisuanShishou(this,\"HQ\")' onpropertychange=''JisuanShishou(this,\"HQ\")' class='OnlyBottom tbShishouHQ' value='" + item.shishouHQ + "'/>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                婚宴应收&nbsp;&nbsp;\
                                <label id='lbYingshouHY' name='lbYingshouHY' class='Label80px'>" + item.NeedPayHY + "</label>&nbsp;&nbsp;\
                                <label id='lbYingshouHYB' name='lbYingshouHYB' class='Label30px'>" + item.BaiHY + "</label>&nbsp;&nbsp;\
                                婚宴实收&nbsp;&nbsp;\
                                <label id='lbShishouHY' name='lbShishouHY' class='Label80px'></label>&nbsp;&nbsp;\
                                <label id='lbShishouHYB' class='Label30px'></label>\
                                <br />\
                                <br />\
                                婚庆应收&nbsp;&nbsp;\
                                <label id='lbYingshouHQ' name='lbYingshouHQ' class='Label80px'>" + item.NeedPayHQ + "</label>&nbsp;&nbsp;\
                                <label id='lbYingshouHQB' name='lbYingshouHQB' class='Label30px'>" + item.BaiHQ + "</label>&nbsp;&nbsp;\
                                婚庆实收&nbsp;&nbsp;\
                                <label id='lbShishouHQ' name='lbShishouHQ' class='Label80px'></label>&nbsp;&nbsp;\
                                <label id='lbShishouHQB' class='Label30px'></label>\
                            </td>\
                            <td style='vertical-align: middle'>\
                              <label id='zhuangtai'>" + item.zhuangtai + "</label>\
                            </td>\
                            <td style='vertical-align: middle;'>\
                              <input type='hidden' id='payID' name='payID' value='" + item.id + "' />\
                              <div id='btnSave' class='btn btn-default btn-sm btnSave " + (item.zhuangtai == '可编辑' ? "" : "hide") + "' title='保存'><i class='fa fa-save' style='display:block'></i></div>\
                                <div id='btnSubmit' class='btn btn-default btn-sm btnSubmit " + (item.zhuangtai == '可编辑' ? "" : "hide") + "' title='提交'><i class='fa fa-upload' style='display:block'></i></div>\
                                <div class='btn btn-default btn-sm btnDelete " + (item.zhuangtai == '审批完成' ? "hide" : "") + "' title='删除'><i class='fa fa-ban' style='display:block'></i></div>\
                            </td></tr>"
            $(node).insertBefore($("#SummaryKuanxiang"));
        }
       
    });
    reflashYingshou();
}

function initPageIfhasID_TK(data) {
    res = data;
    $.each(res.payments, function (i, item) {
        var node = "<tr style='vertical-align: middle' id='payContent'>\
                            <td style='vertical-align: middle'>\
                               <select id='ddlKuanXiang' name='ddlKuanXiang' style='width: 60px;' onchange='setPaymentType(this)'>\
                                <option></option>\
                                <option value='tk' " + (item.payType == 'tk' ? 'selected' : '') + ">退款</option>\
                                <option value='pk' " + (item.payType == 'pk' ? 'selected' : '') + ">赔款</option>\
                                </select>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                <input type='text' class='RiSelect OnlyBottom' id='PayDate' name='PayDate' value='" + item.payDate + "'/>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                婚宴金额&nbsp;&nbsp;<input type='text' id='tbShishouHY' name='tbtkHY'   class='OnlyBottom tbtkHY'  oninput='JisuanTuikuan()' onpropertychange='JisuanShishou()' value='" + item.shishouHY + "'/>&nbsp;&nbsp; 大写&nbsp;&nbsp; <label id='lbdxtbHY' name='lbdxtbHY' class='Label80px'></label>\
                                <br />\
                                <br />\
                                婚庆金额&nbsp;&nbsp;<input type='text' id='tbShishouHQ' name='tbtkHQ'  class='OnlyBottom tbtkHQ'   oninput='JisuanTuikuan()' onpropertychange='JisuanShishou()' value='" + item.shishouHQ + "'/>&nbsp;&nbsp; 大写&nbsp;&nbsp; <label id='lbdxtbHQ' name='lbdxtbHQ' class='Label80px'></label>\
                            </td>\
                            <td style='vertical-align: middle'>\
                               <label id='zhuangtai'>" + item.zhuangtai + "</label>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                  <input type='hidden' id='payID' name='payID' value='" + item.id + "' />\
                                 <div id='btnSave' class='btn btn-default btn-sm btnSave " + (item.zhuangtai == '可编辑' ? "" : "hide") + "' title='保存'><i class='fa fa-save' style='display:block'></i></div>\
                                <div id='btnSubmit' class='btn btn-default btn-sm btnSubmit " + (item.zhuangtai == '可编辑' ? "" : "hide") + "' title='提交'><i class='fa fa-upload' style='display:block'></i></div>\
                                <div id='btnDelete' class='btn btn-default btn-sm btnDelete " + (item.zhuangtai == '审批完成' ? "hide" : "") + "' title='删除'><i class='fa fa-ban' style='display:block'></i></div>\
                            </td>\
                        </tr>"
        $(node).insertBefore($("#SummaryTuikuan"));
    });
    JisuanTuikuan();
}

function reflashYingshou() {
    var Zong = $("#MainContent_hdZongjineHY").val();
    var Zongex = $("#MainContent_hdZongjineHYex").val();
    var Kuanglei = "";
    var zongFirst = 0;
    var zong = 0;
    var zongex = 0;
    var zongr = 0;
    $(".tbShishouHY").each(function (i) {
        if (Kuanglei == "") {
            Kuanglei = $(this).closest("tr").find("#ddlKuanXiang").val();
        }

        if (Kuanglei == $(this).closest("tr").find("#ddlKuanXiang").val()) {
            zongFirst += $(this).val() * 1;
        }
        else {
            zongFirst = $(this).val() * 1;
            Kuanglei = $(this).closest("tr").find("#ddlKuanXiang").val();
        }

        if (Kuanglei == "e") {
            zongex += $(this).val() * 1;
            $(this).closest("tr").find("#lbShishouHYB").text((zongFirst / Zongex * 100).toFixed(1).toString() + "%");
        }
        else if (Kuanglei == "r") {
            zongr += $(this).val() * 1;
        }
        else {
            zong += $(this).val() * 1;
            $(this).closest("tr").find("#lbShishouHYB").text((zongFirst / Zong * 100).toFixed(1).toString() + "%");
        }
        $(this).closest("tr").find("#lbShishouHY").text(zongFirst.toFixed(1));

    })

    $("#lbShishouZongHYex").text(zongex.toFixed(1));
    $("#lbShishouZongHYBex").text((zongex / Zongex * 100).toFixed(1).toString() + "%");


    //$("#lbShishouZongHYr").text(zongr.toFixed(1));
    //$("#lbShishouZongHYBr").text((zongr / Zongr * 100).toFixed(1).toString() + "%");

    $("#lbShishouZongHY").text(zong.toFixed(1));
    $("#lbShishouZongHYB").text((zong / Zong * 100).toFixed(1).toString() + "%");

    //
    Zong = $("#MainContent_hdZongjineHQ").val();
    Zongex = $("#MainContent_hdZongjineHQex").val();
    zong = 0;
    zongFirst = 0;
    zongex = 0;
    zongr = 0;
    $(".tbShishouHQ").each(function (i) {
        if (Kuanglei == "") {
            Kuanglei = $(this).closest("tr").find("#ddlKuanXiang").val();
        }
        if (Kuanglei == $(this).closest("tr").find("#ddlKuanXiang").val()) {
            zongFirst += $(this).val() * 1;
        }
        else {
            zongFirst = $(this).val() * 1;
            Kuanglei = $(this).closest("tr").find("#ddlKuanXiang").val();
        }

        if (Kuanglei == "e") {
            zongex += $(this).val() * 1;
            $(this).closest("tr").find("#lbShishouHQB").text((zongFirst / Zongex * 100).toFixed(1).toString() + "%");
        }
        else if (Kuanglei == "r") {
            zongr += $(this).val() * 1;
        }
        else {
            zong += $(this).val() * 1;
            $(this).closest("tr").find("#lbShishouHQB").text((zongFirst / Zong * 100).toFixed(1).toString() + "%");
        }
        $(this).closest("tr").find("#lbShishouHQ").text(zongFirst.toFixed(1));
    

    })

        $("#lbShishouZongHQex").text(zongex.toFixed(1));
        $("#lbShishouZongHQBex").text((zongex / Zongex * 100).toFixed(1).toString() + "%");

//        $("#lbShishouZongHQr").text(zongr.toFixed(1));
//        $("#lbShishouZongHQBr").text((zongr / Zongr * 100).toFixed(1).toString() + "%");

        $("#lbShishouZongHQ").text(zong.toFixed(1));
        $("#lbShishouZongHQB").text((zong / Zong * 100).toFixed(1).toString() + "%");

        //heji
        $("#lbhj_shishouHY").text($("#lbShishouZongHY").text() * 1 + $("#lbShishouZongHYex").text() * 1);
        $("#lbhj_shishouHQ").text($("#lbShishouZongHQ").text() * 1 + $("#lbShishouZongHQex").text() * 1);

}


function addPaymentRow() {
    var node = "<tr style='vertical-align: middle' id='payContent'>\
                            <td style='vertical-align: middle'>\
                                <select id='ddlKuanXiang' name='ddlKuanXiang' style='width: 60px;' onchange='setPaymentType(this)'>\
                                <option></option>\
                                <option value='1'>首款</option>\
                                <option value='2'>中款</option>\
                                <option value='3'>尾款</option>\
                                </select>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                <input type='text' class='RiSelect OnlyBottom' id='PayDate' name='PayDate' />\
                            </td>\
                            <td style='vertical-align: middle'>\
                                <input type='text' class='OnlyBottom' id='PayOrderNumber' name='PayOrderNumber'  />\
                            </td>\
                            <td style='vertical-align: middle'>\
                                婚宴实收&nbsp;&nbsp;<input type='text' id='tbShishouHY' name='tbShishouHY' oninput='JisuanShishou(this,\"HY\")' onpropertychange=''JisuanShishou(this,\"HY\")' class='OnlyBottom tbShishouHY' />\
                                <br />\
                                <br />\
                                婚庆实收&nbsp;&nbsp;<input type='text' id='tbShishouHQ' name='tbShishouHQ' oninput='JisuanShishou(this,\"HQ\")' onpropertychange=''JisuanShishou(this,\"HQ\")' class='OnlyBottom tbShishouHQ' />\
                            </td>\
                            <td style='vertical-align: middle'>\
                                婚宴应收&nbsp;&nbsp;\
                                <label id='lbYingshouHY' name='lbYingshouHY' class='Label80px'></label>&nbsp;&nbsp;\
                                <label id='lbYingshouHYB' name='lbYingshouHYB' class='Label30px'></label>&nbsp;&nbsp;\
                                婚宴实收&nbsp;&nbsp;\
                                <label id='lbShishouHY' name='lbShishouHY' class='Label80px'></label>&nbsp;&nbsp;\
                                <label id='lbShishouHYB' class='Label30px'></label>\
                                <br />\
                                <br />\
                                婚庆应收&nbsp;&nbsp;\
                                <label id='lbYingshouHQ' name='lbYingshouHQ' class='Label80px'></label>&nbsp;&nbsp;\
                                <label id='lbYingshouHQB' name='lbYingshouHQB' class='Label30px'></label>&nbsp;&nbsp;\
                                婚庆实收&nbsp;&nbsp;\
                                <label id='lbShishouHQ' name='lbShishouHQ' class='Label80px'></label>&nbsp;&nbsp;\
                                <label id='lbShishouHQB' class='Label30px'></label>\
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
                        </tr>"
    $(node).insertBefore($("#SummaryKuanxiang"));


};

function addPaymentexRow() {
    var node = "<tr style='vertical-align: middle' id='payContent'>\
                            <td style='vertical-align: middle'>\
                               增加项<input type='hidden' id='ddlKuanXiang' value='e'/>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                <input type='text' class='RiSelect OnlyBottom' id='PayDate' name='PayDate' />\
                            </td>\
                            <td style='vertical-align: middle'>\
                                <input type='text' class='OnlyBottom' id='PayOrderNumber' name='PayOrderNumber'/>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                婚宴实收&nbsp;&nbsp;<input type='text' id='tbShishouHY' name='tbShishouHY' oninput='JisuanShishou(this,\"HY\")' onpropertychange='JisuanShishou(this,\"HY\")' class='OnlyBottom tbShishouHY' />\
                                <br />\
                                <br />\
                                婚庆实收&nbsp;&nbsp;<input type='text' id='tbShishouHQ' name='tbShishouHQ' oninput='JisuanShishou(this,\"HQ\")' onpropertychange='JisuanShishou(this,\"HQ\")' class='OnlyBottom tbShishouHQ' />\
                            </td>\
                            <td style='vertical-align: middle'>\
                                婚宴应收&nbsp;&nbsp;\
                                <label id='lbYingshouHY' name='lbYingshouHY' class='Label80px'>" + $("#MainContent_hdZongjineHYex").val() + "</label>&nbsp;&nbsp;\
                                <label id='lbYingshouHYB' name='lbYingshouHYB' class='Label30px'>100%</label>&nbsp;&nbsp;\
                                婚宴实收&nbsp;&nbsp;\
                                <label id='lbShishouHY' name='lbShishouHY' class='Label80px'></label>&nbsp;&nbsp;\
                                <label id='lbShishouHYB' class='Label30px'></label>\
                                <br />\
                                <br />\
                                婚庆应收&nbsp;&nbsp;\
                                <label id='lbYingshouHQ' name='lbYingshouHQ' class='Label80px'>" + $("#MainContent_hdZongjineHQex").val() + "</label>&nbsp;&nbsp;\
                                <label id='lbYingshouHQB' name='lbYingshouHQB' class='Label30px'>100%</label>&nbsp;&nbsp;\
                                婚庆实收&nbsp;&nbsp;\
                                <label id='lbShishouHQ' name='lbShishouHQ' class='Label80px'></label>&nbsp;&nbsp;\
                                <label id='lbShishouHQB' class='Label30px'></label>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                <label id='zhuangtai'>编辑中</label>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                  <input type='hidden' id='payID' name='payID' value='0' />\
                                 <div id='btnSave' class='btn btn-default btn-sm btnSave' title='保存'><i class='fa fa-save' style='display:block'></i></div>\
                                <div  id='btnSubmit' class='btn btn-default btn-sm btnSubmit' title='提交'><i class='fa fa-upload' style='display:block'></i></div>\
                                <div id='btnDelete' class='btn btn-default btn-sm btnDelete' title='删除'><i class='fa fa-ban' style='display:block'></i></div>\
                            </td>\
                        </tr>"
    $(node).insertBefore($("#SummaryAddtional"));
}

function addTuikuanRow() {
    var node = "<tr style='vertical-align: middle' id='payContent'>\
                            <td style='vertical-align: middle'>\
                               <select id='ddlKuanXiang' name='ddlKuanXiang' style='width: 60px;' onchange='setPaymentType(this)'>\
                                <option></option>\
                                <option value='tk'>退款</option>\
                                <option value='pk'>赔款</option>\
                                </select>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                <input type='text' class='RiSelect OnlyBottom' id='PayDate' name='PayDate' />\
                            </td>\
                            <td style='vertical-align: middle'>\
                                婚宴金额&nbsp;&nbsp;<input type='text' id='tbShishouHY' name='tbtkHY'   class='OnlyBottom tbtkHY'  oninput='JisuanTuikuan()' onpropertychange='JisuanShishou()'/>&nbsp;&nbsp; 大写&nbsp;&nbsp; <label id='lbdxtbHY' name='lbdxtbHY' class='Label80px'></label>\
                                <br />\
                                <br />\
                                婚庆金额&nbsp;&nbsp;<input type='text' id='tbShishouHQ' name='tbtkHQ'  class='OnlyBottom tbtkHQ'   oninput='JisuanTuikuan()' onpropertychange='JisuanShishou()'/>&nbsp;&nbsp; 大写&nbsp;&nbsp; <label id='lbdxtbHQ' name='lbdxtbHQ' class='Label80px'></label>\
                            </td>\
                            <td style='vertical-align: middle'>\
                              <label id='zhuangtai'>编辑中</label>\
                            </td>\
                            <td style='vertical-align: middle'>\
                                  <input type='hidden' id='payID' name='payID' value='0' />\
                                 <div id='btnSave' class='btn btn-default btn-sm btnSave' title='保存'><i class='fa fa-save' style='display:block'></i></div>\
                                <div id='btnSubmit' class='btn btn-default btn-sm btnSubmit' title='提交'><i class='fa fa-upload' style='display:block'></i></div>\
                                <div id='btnDelete' class='btn btn-default btn-sm btnDelete' title='删除'><i class='fa fa-ban' style='display:block'></i></div>\
                            </td>\
                        </tr>"
    $(node).insertBefore($("#SummaryTuikuan"));
}
function setBtAddNotShow() {
    $("#btAdd").hide();
    $("#btAdd_Ex").hide();
}

function btAddExBind() {
    $("#btAdd_Ex").on("click", function () {
        addPaymentexRow();
    })
}

function btAddBind() {
    $("#btAdd").bind("click", function () {
        addPaymentRow();
    });
   
}

function btAddTKBind() {
    $("#btAdd_Tk").bind("click", function () {
        addTuikuanRow();
    });
}

function setPaymentType(obj) {
    var typeContent = $(obj).val();
    var id = $("#MainContent_hdID").val();
    if (typeContent != "" && id !=0) {
        $.ajax({
            url: "HunliAjax.ashx",
            async: false,
            data: { "method": "getPaymentByType", "type": typeContent, "id": id, "dt": new Date().getTime() },
            success: function (data) {
                var res = eval("(" + data + ")");
                $(obj).closest("tr").find("#lbYingshouHY").text(res.payAmountHY);
                $(obj).closest("tr").find("#lbYingshouHYB").text(res.payPercentHY);
                $(obj).closest("tr").find("#lbYingshouHQ").text(res.payAmountHQ);
                $(obj).closest("tr").find("#lbYingshouHQB").text(res.payPercentHQ);
            }
        });
    }
}

function HideButtons() {
    $("#btAdd").hide();
    $("#btAdd_Ex").hide();
    $("#btAdd_Tk").hide();
    $(".btnSave").hide();
    $(".btnSubmit").hide();
    $(".btnDelete").hide();

}

function test() {
alert("123")
}

function JisuanShishou(obj, types) {
    reflashYingshou();
}

function JisuanTuikuan() {
    var tuikuanZong = 0;
    $(".tbtkHY").each(function (i, item) {
        var trThis = $(this).closest("tr");
        trThis.find("#lbdxtbHY").text(digit_uppercase($(this).val()));
        tuikuanZong += $(this).val() * 1;
    })
    $("#lbZongTKHY").text(tuikuanZong.toFixed(1).toString());

    tuikuanZong = 0;
    $(".tbtkHQ").each(function (i, item) {
        var trThis = $(this).closest("tr");
        trThis.find("#lbdxtbHQ").text(digit_uppercase($(this).val()));
        tuikuanZong += $(this).val() * 1;
    })
    $("#lbZongTKHQ").text(tuikuanZong.toFixed(1).toString());
}

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