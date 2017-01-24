/* ---- Teachers Panel ----- */

function afterAddTeacher() {
    jQuery("#addTeacherModal").modal("hide");
};

function processTeacher(action) {
    var bar = $(".progress-bar");
    var percent = $(".progress-bar");
    var statusC = $("#ajaxSendStatusC");
    var statusE = $("#ajaxSendStatusE");
    var teacherTable = $("#teacherTableBody");

    var options = {
        beforeSend: function () {
            statusC.empty();
            statusE.empty();
            var percentVal = "0%";
            bar.width(percentVal);
            percent.html(percentVal);
        },
        uploadProgress: function (event, position, total, percentComplete) {
            var percentVal = percentComplete + "%";
            bar.width(percentVal);
            percent.html(percentVal);
        },
        success: function (data) {
            if (action === 1) {
                statusC.removeClass("hidden");
                statusC.html("Преподаватель успешно добавлен!");
            }
            if (action === 2) {
                statusE.removeClass("hidden");
                statusE.html("Преподаватель успешно изменён!");
            }
            var percentVal = "100%";
            bar.width(percentVal);
            percent.html(percentVal);
            teacherTable.html(data);
        },
        error: function (data) {
            if (action === 1) {
                statusC.html(data);
            }
            if (action === 2) {
                statusE.html(data);
            }
        }
    };
    if (action === 1) {
        if ($("#addTeacherForm").valid()) {
            $("#addTeacherForm").ajaxForm(options);
            $("#addTeacherForm").submit();
        }
    }
    if (action === 2) {
        if ($("#editTeacherForm").valid()) {
            $("#editTeacherForm").ajaxForm(options);
            $("#editTeacherForm").submit();
        }
    }
}

function dellTeacher() {
    if (!confirm("Вы уверены?"))
        return;
    var teacher = window.event.currentTarget;
    var teacherId = teacher.id;
    teacher = teacher.parentElement;
    teacher = teacher.parentElement;
    teacher.remove();

    var url = location.protocol + "//" + location.host + "/Admin/DellTeacher";

    jQuery.ajax({
        url: url,
        type: "POST",
        data: { teacherId: teacherId }
    });
}

function getTeachers() {
    var urlGetTeachers = location.protocol + "//" + location.host + "/Admin/GetTeachers";
    jQuery.ajax({
        url: urlGetTeachers,
        type: "GET",
        success: function (data) {
            $("#teacherTableBody").empty();
            $("#teacherTableBody").html(data);
        }
    });
}

function editTeacherInitModal() {
    var teacher = window.event.currentTarget;
    var teacherId = teacher.id;

    var url = location.protocol + "//" + location.host + "/Admin/EditTeacherModal";

    jQuery.ajax({
        url: url,
        type: "GET",
        data: {id: teacherId},
        success: function (data) {
            $("#editTeacherModalBody").empty();
            $("#editTeacherModalBody").html(data);
        }
    });
}

/* ---- Teachers Panel ----- */

/* ---- Students Panel ----- */

function dellStudent() {
    if (!confirm("Вы уверены?"))
        return;
    var student = window.event.currentTarget;
    var studentId = student.id;
    student = student.parentElement;
    student = student.parentElement;

    var rootUrl = location.host;
    var url = location.protocol + "//" + rootUrl + "/Admin/DellStudent";

    jQuery.ajax({
        url: url,
        type: "POST",
        data: { studentId: studentId },
        success: function () {
            student.remove();
        }
    });
}

function editStudentInitModal() {
    var student = window.event.currentTarget;
    var studentId = student.id;

    var url = location.protocol + "//" + location.host + "/Admin/EditStudentModal";

    jQuery.ajax({
        url: url,
        type: "GET",
        data: { id: studentId },
        success: function(data) {
            $("#edit-student").empty();
            $("#edit-student").html(data);
        }
    });
}

function addStudent() {

    var fullName = $("#aStFullName").val();
    var email = $("#aStEmail").val();
    var skype = $("#aStSkype").val();

    var url = location.protocol + "//" + location.host + "/Admin/AddStudent";
    jQuery.ajax({
        url: url,
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            fullName: fullName,
            email: email,
            skype: skype
        }),
        success: function (data) {
            if (data === "Success") {
                getStudents();
                $("#addStMessage").text("Студент добавлен успешно!");
                $("#addStMessage").removeClass("hidden");
                $("#addStMessage").addClass("alert-success");
                setTimeout(function () {
                    $("#addStMessage").addClass("hidden");
                    $("#ModaladdSt").modal("hide");
                    $("#addStMessage").removeClass("alert-success");
                }, 2000);   
            }
        },
        error: function (data) {
            addStudentError(data);
        }
    });
}

function editStudent() {

    var id = $("#eStId").val();
    var fullName = $("#eStFullName").val();
    var email = $("#eStEmail").val();
    var skype = $("#eStSkype").val();
    var teacherId = $("#slStTeachers").val();
    var lenguageId = $("#slStLenguage").val();

    var url = location.protocol + "//" + location.host + "/Admin/EditStudent";
    jQuery.ajax({
        url: url,
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            Id: id,
            Email: email,
            FullName: fullName,
            Skype: skype,
            TeacherId: teacherId,
            LenguageId: lenguageId
        }),
        success: function (data) {
            if (data === "Success") {
                getStudents();
                $("#editStMessage").text("Студент успешно сохранён!");
                $("#editStMessage").removeClass("hidden");
                $("#editStMessage").addClass("alert-success");
                setTimeout(function () {
                    $("#editStMessage").addClass("hidden");
                    $("#edit-student").modal("hide");
                    $("#editStMessage").removeClass("alert-success");
                }, 2000);
            }
        },
        error: function (data) {
            editStudentError(data);
        }
    });
}

function addStudentBtn() {
    var isValid = $("#addStForm").valid();
    if (isValid === true) {
        addStudent();
    }
}

$("#addStForm").keypress(function (event) {
    var isValid = $("#addStForm").valid();
    if (isValid === true && event.keyCode === 13) {
        addStudent();
    }
});

function editStudentBtn() {
    var isValid = $("#editStAjaxForm").valid();
    if (isValid === true) {
        editStudent();
    }
}

$("#editStAjaxForm").keypress(function (event) {
    var isValid = $("#editStAjaxForm").valid();
    if (isValid === true && event.keyCode === 13) {
        editStudent();
    }
});

function getStudents() {
    var urlGetStudents = location.protocol + "//" + location.host + "/Admin/AdminStudentsList";
    jQuery.ajax({
        url: urlGetStudents,
        type: "GET",
        success: function (data) {
            $("#students-table tbody").empty();
            $("#students-table tbody").html(data);
        }
    });
}

function editStudentError(data) {
    if (data.responseText === "NonExistent") {
        $("#editStMessage").text("Такого студента не существует!");
        $("#editStMessage").removeClass("hidden");
        $("#editStMessage").addClass("alert-danger");
        setTimeout(function () {
            $("#editStMessage").addClass("hidden");
            $("#edit-student").modal("hide");
            $("#editStMessage").removeClass("alert-danger");
        }, 2000);
    }
    setTimeout(function () {
        $("#edit-student").modal("hide");
    }, 2000);
}

function addStudentError(data) {
    if (data.responseText === "AutorizationFailed") {
        $("#addStMessage").text("Доступ запрещён!");
        $("#addStMessage").removeClass("hidden");
        $("#addStMessage").addClass("alert-danger");
        setTimeout(function () {
            $("#addStMessage").addClass("hidden");
            $("#ModaladdSt").modal("hide");
            $("#addStMessage").removeClass("alert-danger");
        }, 2000);
    } else {
        $("#addStMessage").text("Ошибка!");
        $("#addStMessage").removeClass("hidden");
        $("#addStMessage").addClass("alert-danger");
        setTimeout(function () {
            $("#addStMessage").addClass("hidden");
            $("#ModaladdSt").modal("hide");
            $("#addStMessage").removeClass("alert-danger");
        }, 2000);
    }
}

/* ---- Students Panel ----- */

/* ---- Course Panel ----- */

$("#addCrAjaxForm").keypress(function (event) {
    var isValid = $("#addCrAjaxForm").valid();
    if (isValid === true && event.keyCode === 13) {
        processCourse(1);
    }
});

$("#editCrAjaxForm").keypress(function (event) {
    var isValid = $("#editCrAjaxForm").valid();
    if (isValid === true && event.keyCode === 13) {
        processCourse(2);
    }
});

function processCourse(action) {
    var bar = $(".progress-bar");
    var percent = $(".progress-bar");
    var statusC = $("#ajaxSendStatusAddCourse");
    var statusE = $("#ajaxSendStatusEditCourse");
    var lenguagesTable = $("#adminLanguagesList");

    var percentVal = "0" + "%";
    bar.width(percentVal);
    percent.html(percentVal);

    var options = {
        beforeSend: function () {
            statusC.empty();
            statusE.empty();
            var percentVal = "0%";
            bar.width(percentVal);
            percent.html(percentVal);
        },
        uploadProgress: function (event, position, total, percentComplete) {
            var percentVal = percentComplete + "%";
            bar.width(percentVal);
            percent.html(percentVal);
        },
        success: function (data) {
            if (action === 1) {
                statusC.removeClass("hidden");
                statusC.html("Курс успешно добавлен!");
            }
            if (action === 2) {
                statusE.removeClass("hidden");
                statusE.html("Курс успешно изменён!");
            }
            var percentVal = "100%";
            bar.width(percentVal);
            percent.html(percentVal);
            lenguagesTable.html(data);
        },
        error: function (data) {
            if (action === 1) {
                statusC.html(data);
            }
            if (action === 2) {
                statusE.html(data);
            }
        }
    };

    if (action === 1) {
        if ($("#addCrAjaxForm").valid()) {
            $("#addCrAjaxForm").ajaxForm(options);
            $("#addCrAjaxForm").submit();
        }
    }
    if (action === 2) {
        if ($("#editCrAjaxForm").valid()) {
            $("#editCrAjaxForm").ajaxForm(options);
            $("#editCrAjaxForm").submit();
        }
    }
}

function editLanguageInitModal() {
    var language = window.event.currentTarget;
    var languageId = language.id;

    var url = location.protocol + "//" + location.host + "/Admin/EditLanguageModal";

    jQuery.ajax({
        url: url,
        type: "GET",
        data: { id: languageId },
        success: function (data) {
            $("#edit-language").empty();
            $("#edit-language").html(data);
        }
    });
}

function dellCourse() {
    if (!confirm("Вы уверены?"))
        return;
    var course = window.event.currentTarget;
    var courseId = course.id;
    course = course.parentElement;
    course = course.parentElement;

    var url = location.protocol + "//" + location.host + "/Admin/DellCourse";

    jQuery.ajax({
        url: url,
        type: "POST",
        data: { id: courseId },
        success: function () {
            course.remove();
        }
    });
}

/* ---- Course Panel ----- */


document.onclick = function () {
    var current = window.event;
    if (current.target.className === "modal fade") {
        var timetable = document.getElementById("timetable");
        if (timetable.childNodes.length > 1) {
            for (var i = 0; i < timetable.childNodes.length; i++) {
                timetable.childNodes[i].className = "hidden";
            };
        }
    }
}

function setTimeTable(id) {
    document.getElementById("ttu-" + id).className = "text";
}

function processLesson() {
    if (!confirm("Обработать?"))
        return;
    var lesson = window.event.currentTarget;
    var lessonId = lesson.id;
    lesson = lesson.parentElement;
    lesson = lesson.parentElement;
    lesson.remove();

    var xhr = new XMLHttpRequest();

    var body = "lessonId=" + lessonId + "&isNew=" + "false";

    var rootUrl = location.host;
    var url = "http://" + rootUrl + "/Admin/EditLesson";

    xhr.open("POST", url, true);
    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

    xhr.send(body);
}

(function () {

    var bar = $(".progress-bar");
    var percent = $(".progress-bar");
    var status = $("#status");
    var teacherTable = $("#teacherTableBody");

    $("form").ajaxForm({
        beforeSend: function () {
            status.empty();
            var percentVal = "0%";
            bar.width(percentVal);
            percent.html(percentVal);
        },
        uploadProgress: function (event, position, total, percentComplete) {
            var percentVal = percentComplete + "%";
            bar.width(percentVal);
            percent.html(percentVal);
        },
        success: function (data) {
            var percentVal = "100%";
            bar.width(percentVal);
            percent.html(percentVal);
            teacherTable.html(data);
        },
        error: function (data) {
            status.html(data);
        }
    });

})();