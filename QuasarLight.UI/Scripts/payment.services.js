function sentRequisites() {
    var email = $("#stEmailReq").val();
    var name = $("#stFullNameReq").val();
    var url = location.protocol + "//" + location.host + "/Payment/SendPaymentRequisitesToStudent";
    jQuery.ajax({
        url: url,
        type: "POST",
        contentType: "application/x-www-form-urlencoded",
        data: {
            name: name,
            email: email
        },
        success: clearPayForm()
});
}

function sentRequisites2() {
    var email = $("#stEmailReq2").val();
    var name = $("#stNameReq2").val();
    var url = location.protocol + "//" + location.host + "/Payment/SendPaymentRequisitesToStudent";
    jQuery.ajax({
        url: url,
        type: "POST",
        contentType: "application/x-www-form-urlencoded",
        data: {
            name: name,
            email: email
        },
        success: clearPayForm()
    });
}

function clearPayForm() {
    $.each($(".content-modal-pay input"), function (index, value) {
        if ($(value).attr("type") !== "submit")
            $(value).val("");
    });
}

$(document).ready(function () {
    $("#order-lessons").addClass("page-scroll-active");
});

$(".orange-sq").on("click", function () {
    $(this).parents("ul").find(".orange-sq").removeClass("orange-sq-selected");
    $(this).addClass("orange-sq-selected");
});

$(".grey-sq").on("click", function () {
    $(this).parents("ul").find(".grey-sq").removeClass("grey-sq-selected");
    $(this).addClass("grey-sq-selected");
});

$(".btn-pay").on("click", function () {

    var url = location.protocol + "//" + location.host + $("#load-requisites-request-modal-url").val();
    jQuery.ajax({
        url: url,
        type: "GET",
        success: function (data) {
            var scope = angular.element($("body")).scope();
            scope.payRequestModalContainer = "";
            changeScope(scope);
            scope.payRequestModalContainer = data;
            changeScope(scope);
            $(".content-modal-pay .inputs input").val("");
        }
    });
});