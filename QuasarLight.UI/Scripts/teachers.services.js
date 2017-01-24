function loadTeacherModal(id) {

    var url = location.protocol + "//" + location.host + "/Teacher/GetTeacherModal";

    jQuery.ajax({
        url: url,
        type: "GET",
        data: { id: id },
        success: function (data) {
            $("#teacherModalContent").empty();
            $("#teacherModalContent").html(data);
        }
    });
    jQuery("#TeacherModal").modal("show");
}

$(document).ready(function () {

    jQuery("#TeacherModal").on("hidden.bs.modal", function () {
        $("#teacherModalContent").empty();
    });
});

jQuery(".teacher-info-modal").attrchange({
    trackValues: false,
    callback: function (event) {
        if (event.attributeName === "class") {
            if (jQuery(this).hasClass("in")) {
                if (jQuery(window).height() < $(this).find(".modal-dialog ").height() + 50) {
                    jQuery("html").getNiceScroll().hide();
                    jQuery(this).niceScroll({ zindex: "999", horizrailenabled: false });
                } else {
                    jQuery(this).getNiceScroll().remove();
                    jQuery("html").getNiceScroll().hide();
                }
            } else {
                jQuery("html").getNiceScroll().show();
                jQuery(this).getNiceScroll().remove();
            }
        }
    }
});