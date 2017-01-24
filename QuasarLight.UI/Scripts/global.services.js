function getName(str, id) {
    var i;
    if (str.lastIndexOf("\\")) {
        i = str.lastIndexOf("\\") + 1;
    }
    else {
        i = str.lastIndexOf("/") + 1;
    }
    var filename = str.slice(i);

    $(id).html(filename);
}

function loadTrialModal() {
    var url;
    if (navigator.userAgent.match(/(iPod|iPhone|iPad)/)) {
        url = location.protocol + "//" + location.host + "/Lesson/TrialLessonOrder";
        window.location.replace(url);
    }
    url = location.protocol + "//" + location.host + $("#loadTrialLessonModalUrl").val();
    if ($("#addTrialLessonForm")[0] == null) {
        jQuery.ajax({
            url: url,
            type: "GET",
            success: function (data) {
                var scope = angular.element($("body")).scope();
                scope.trialLessonModalContainer = "";
                changeScope(scope);
                scope.trialLessonModalContainer = data;
                changeScope(scope);
                $("#addTrialLessonForm").attr("ng-submit", "submitForm()");
                $("#inputs-row input").val("");
                $("#lsComment").val("");
                setFieldMarginBottom();
            },
            error: function(data) {
                var _data = data;
            }
        });
    }
}

function addTrialLesson() {
    var stFullName;
    var stEmail;
    var lsDateTime;
    var stCity;
    var stAge;
    var stSkype;
    var lsLang;
    var lsLevel;
    var lsText = $("#lsComment").val();

    if ($(".narrow-width").css("display") !== "none") {
        stFullName = $(".narrow-width #stFullName").val();
        stEmail = $(".narrow-width #stEmail").val();
        lsDateTime = $(".narrow-width #lsDateTime").val();
        stCity = $(".narrow-width #stCity").val();
        stAge = $(".narrow-width #stAge").val();
        stSkype = $(".narrow-width #stSkype").val();
        lsLang = $(".narrow-width #lsLang").val();
        lsLevel = $(".narrow-width #lsLevel").val();
    } else {
        stFullName = $(".wide-width #stFullName").val();
        stEmail = $(".wide-width #stEmail").val();
        lsDateTime = $(".wide-width #lsDateTime").val();
        stCity = $(".wide-width #stCity").val();
        stAge = $(".wide-width #stAge").val();
        stSkype = $(".wide-width #stSkype").val();
        lsLang = $(".wide-width #lsLang").val();
        lsLevel = $(".wide-width #lsLevel").val();
    }

    var url = location.protocol + "//" + location.host + $("#addTrialLessonUrl").val();

    jQuery.ajax({
        url: url,
        type: "POST",
        data: {
            stFullName: stFullName,
            stEmail: stEmail,
            lsDateTime: lsDateTime,
            stCity: stCity,
            stAge: stAge,
            stSkype: stSkype,
            lsLang: lsLang,
            lsLevel: lsLevel,
            lsComment: lsText
        },
        success: function (data) {
            lessonSuccess(data);
        }
    });
}

function lessonSuccess(data) {
    var btnWidth = 150;
    if (data === "Success") {
        $("#lesson-popover").css({
            display: "block",
            width: (btnWidth * 1.5) + "px",
            top: window.scrollY + 2 + "px",
            left: "2px",
            "padding-right": "10px",
            "padding-top": "5px"
        });
        $("#lesson-popover").addClass("alert-success");
        $("#lesson-popover").removeClass("alert-danger");
        $("#lesson-popover .popover-content").html("<p>Вы успешно записаны на вводный урок!</p>");

        $.each($(".forms-dialogue input"), function (index, value) {
            if ($(value).attr("type") !== "button")
                $(value).val("");
        });
        $("#lsComment").val("");
    } else {
        $("#lesson-popover").css({
            display: "block",
            width: (btnWidth * 1.5) + "px",
            top: window.scrollY + 2 + "px",
            left: "2px",
            "padding-right": "10px",
            "padding-top": "5px"
        });
        $("#lesson-popover").removeClass("alert-success");
        $("#lesson-popover").addClass("alert-danger");
        $("#lesson-popover .popover-content").html("<p>Призошла ошибка! Попробуйте ещё раз.</p>");
    }
}

function widgetsPosition() {
    var btnWidth = 150;
    if ($("#lesson-popover").css("display") === "block") {
        $("#lesson-popover").css({
            width: (btnWidth * 1.5) + "px",
            top: window.scrollY + 2 + "px",
            left: "2px",
            "padding-right": "10px",
            "padding-top": "5px"
        });
    }
}

document.onclick = function () {

    if ($("#lesson-popover").css("display") === "block") {
        setTimeout(function () {
            $("#lesson-popover").css({
                display: "none"
            });
        }, 500);
    }
}

document.onscroll = function () {
    widgetsPosition();
}

$(document).ready(function () {
    jQuery("body").niceScroll({ mousescrollstep: "30", bouncescroll: false, horizrailenabled: false, preservenativescrolling: false });
    if (navigator.userAgent.match(/(iPod|iPhone|iPad)/)) {
        $(".modal-inp-date").attr("type", "text");
    }
});

function changeDateInputAttr() {
    if (navigator.userAgent.match(/(iPod|iPhone|iPad)/)) {
        $(".modal-inp-date").attr("type", "text");
        $("input").css("font-size", "16px !important");
    } else {
        $(".modal-inp-date").attr("type", "date");
    }
}

$("body").on("click", function () {
    jQuery("#bs-example-navbar-collapse-1").collapse("hide");
});

/* Angular init */

var app = angular.module("dialogue-app", []);

app.directive("compile", [
    "$compile", function ($compile) {
        return function (scope, element, attrs) {

            scope.$watch(
                function (scope) {
                    return scope.$eval(attrs.compile);
                },
                function (value) {
                    element.html(value);
                    $compile(element.contents())(scope);
                }
            );
        };
    }
]);

app.controller("validateCtrl", ["$scope", function ($scope) {
    $scope.selectedOption = null;
    $scope.options = [];
    $scope.logentries = [];
    var date = new Date();
    $scope.minDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() + 3);
}]);

function changeScope($scope) {
    var scope = angular.element($("body")).scope();
    scope.$apply(function () {
        scope = $scope;
    });
}

/* Angular init */

/* Modal interface services */
jQuery(window).resize(function () {
    setFieldMarginBottom();
});
jQuery(".trial-lesson-modal").attrchange({
    trackValues: true,
    callback: function (evnt) {
        if (evnt.attributeName === "class") {
            setFieldMarginBottom();
        }
    }
});

function setFieldMarginBottom() {
    var inputsBoxHeight = $(".narrow-width").css("height");
    $(".narrow-width input").css("cssText", "margin-bottom:" + parseFloat(inputsBoxHeight) / 100 * 1.5 + "px !important;");
    $(".narrow-width select").css("cssText", "margin-bottom:" + parseFloat(inputsBoxHeight) / 100 * 1.5 + "px !important;");
}
/* Modal interface services */