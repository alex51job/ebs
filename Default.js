/// <reference path="Scripts/jquery.min.js">
$(function () {
    setAdapter();
    bindStopMessage();
    setInterval(getDaiban, 60000);
})

function setAdapter() {
    var uls = $('.sidebar-nav > ul > *').clone();
    uls.addClass('visible-xs');
    $('#main-menu').append(uls.clone());
    $("[rel=tooltip]").tooltip();
}

function bindStopMessage() {
    $("#stopMessage").on("click", function () {
        message.clear();
    })
}

function getDaiban() {
    $.ajax({
        url: "Default.ashx",
        async: false,
        data: { d: new Date().getTime(), method: "getDaiban" },
        success: function (data) {
            var res = eval("(" + data + ")");
            //$("#ulMyTask").empty();
            $.each(res.list, function (i, item) {
                var node = "<li class='list-group-item'>" + item.content + "<span class='label label-danger'>" + item.num + "</span></li>";
                if ($("#ulMyTask").find(".label-danger").eq(i).length == 1) {
                    var n = $("#ulMyTask").find(".label-danger").eq(0).text();
                    if (item.num != n) {
                        message.show();
                    }
                    $("#ulMyTask").find("li").eq(0).remove();

                }
                $(node).appendTo($("#ulMyTask"));

            })

        },
        error: function (e) {
            document.location = document.location;
        }
    });

}

function HideLinkA() {
    $(".LinkA").hide();
}

function HideLinkB() {
    $(".LinkB").hide();
}

var message = {
    time: 0,
    title: document.title,
    timer: null,
    show: function () {
        var title = message.title.replace("【　　　】", "").replace("【新消息】", "");
        message.timer = setTimeout(function () {
            message.time++;
            message.show();
            if (message.time % 2 == 0) {
                document.title = "【新消息】" + title
            }

            else {
                document.title = "【　　　】" + title
            };
        }, 600);
        return [message.timer, message.title];
    },
    clear: function () {
        clearTimeout(message.timer);
        document.title = message.title;
    }
};

function HideSWorHY(str) {
    if (str == "HY") {
        $(".tab-pane").eq(0).addClass("active");
    }

    if (str == "SW") {
        $(".tab-pane").eq(1).addClass("active");
    }
}
