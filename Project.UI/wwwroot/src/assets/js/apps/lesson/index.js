var isUpdate = false;

function runOperation() {
    createLessonTable();

    $("#lesson-list").on("click-cell.bs.table", function (field, value, row, $el) {

        if (value != "id") {

            window.location.href = siteUrl + "/Lesson/Posts?id=" + $el.id;

        }
    });

    $('#lessonModal').on('hidden.bs.modal', function (e) {
        $(this)
            .find("input,textarea,select")
            .val('')
            .end()
            .find("input[type=checkbox], input[type=radio]")
            .prop("checked", "")
            .end();
    })
}

runOperation();

function createLessonTable() {
    $('#lesson-list').bootstrapTable({
        url: siteUrl + "/DataTableProvider/GetLessonList",
        pagination: false,
        locale: 'tr-TR'
    })

}

function saveLesson() {
    $("#Loading").show();
    if (!isUpdate) {
        $.ajax({
            url: siteUrl + '/LessonOperation/InsertLesson',
            type: 'POST',
            dataType: 'json',
            data: lessonModel(),
            success: function (data) {
                successToast("Başarıyla Kaydedildi.");
                $('#lesson-list').bootstrapTable("refresh");
                $("#lessonModal").modal("hide");
                $("#Loading").hide();
                clearModal("lessonModal");
            },
            error: function (err) {
                $("#Loading").hide();
                errorToast(err.responseJSON.message);
            }
        });
    }
    else {
        $.ajax({
            url: siteUrl + '/LessonOperation/UpdateLesson',
            type: 'POST',
            dataType: 'json',
            data: lessonModel(),
            success: function (data) {
                successToast("Başarıyla Kaydedildi.");
                $('#lesson-list').bootstrapTable("refresh");
                $("#lessonModal").modal("hide");
                $("#Loading").hide();
                clearModal("lessonModal");
            },
            error: function (err) {
                $("#Loading").hide();
                errorToast(err.responseJSON.message);
            }
        });
    }


}

function deleteLesson(id) {
    Swal.fire({
        title: 'Data silinecek onaylıyor musunuz?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: "Vazgeç",
        confirmButtonText: 'Evet'
    }).then((result) => {

        if (result.isConfirmed) {
            $("#Loading").show();
            $.ajax({
                url: siteUrl + '/LessonOperation/DeleteLesson?id=' + id,
                type: 'GET',
                dataType: 'json',
                success: function (data) {

                    $("#lesson-list").bootstrapTable('removeByUniqueId', id);
                    $("#Loading").hide();
                    $("#lessonModal").modal("hide");
                    successToast(data.message);
                },
                error: function (err) {
                    $("#Loading").hide();
                    errorToast(err.responseJSON.message);
                }
            });
        }
    })



}

function lessonModel() {
    let result = {
        Id : $("#lessonId").val(),
        Name: $("#lessonName").val()
    }
    return result;
}

function showModal(id, type) {
    if (type) {
        $("#" + id + " #title").html("Ders Güncelle");
        
    }
    else
    {
        $("#" + id + " #title").html("Ders Ekle");
        
    }
    isUpdate = type;
    $("#" + id).modal("show");
}

function fillLessonAndShowModal(id) {

    $.ajax({
        url: siteUrl + '/LessonOperation/GetLessonById?id=' + id,
        type: 'GET',
        dataType: 'json',
        success: function (res) {

            $("#lessonName").val(res.data.name);
            $("#lessonId").val(res.data.id);

            showModal("lessonModal", true);
            $("#Loading").hide();

        },
        error: function (err) {
            $("#Loading").hide();
            errorToast(err.responseJSON.message);
        }
    });

}

function operationFormatter(value) {
    let updateString = '<a class="badge badge-light-primary text-start me-2 action-edit" onclick="fillLessonAndShowModal(' + value + ')"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-edit-3"><path d="M12 20h9"></path><path d="M16.5 3.5a2.121 2.121 0 0 1 3 3L7 19l-4 1 1-4L16.5 3.5z"></path></svg></a>';
    let deleteString = '<a class="badge badge-light-dark action-delete" onclick="deleteLesson(' + value + ')"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="red" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-trash"><polyline points="3 6 5 6 21 6"></polyline><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path></svg></a>';

    return updateString + deleteString;
}
