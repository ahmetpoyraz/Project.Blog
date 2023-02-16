var lessonId = $("#lessonId").val();

function runOperation() {

    createLessonPostTable();
    $("#lesson-post-list").on("click-cell.bs.table", function (field, value, row, $el) {

        if (value != "id") {

            window.location.href = siteUrl + "/Lesson/PostDetails?id=" + $el.id;

        }
    });

   
}


runOperation();

function createLessonPostTable() {
    $('#lesson-post-list').bootstrapTable({
        url: siteUrl + "/DataTableProvider/GetLessonPostList?lessonId=" + lessonId,
        pagination: true,
        search: false,
        locale: 'tr-TR'
    })
}

function saveLessonPost() {
    $("#Loading").show();
   
    $.ajax({
        url: siteUrl + '/LessonOperation/InsertLessonPost',
        type: 'POST',
        dataType: 'json',
        data: lessonPostModel(),
        success: function (data) {
            successToast("Başarıyla Kaydedildi.");

            $('#lesson-post-list').bootstrapTable("refresh");
            $("#lessonPostModal").modal("hide");
            clearModal("lessonPostModal");
            $("#Loading").hide();
           
        },
        error: function (err) {
            $("#Loading").hide();
            errorToast(err.responseJSON.message);
        }
    });

}

function deleteLessonPost(id) {
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
                url: siteUrl + '/LessonOperation/DeleteLessonPost?id=' + id,
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    $("#lesson-post-list").bootstrapTable('removeByUniqueId', id);
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

function lessonPostModel() {
    let result = {
        LessonId: lessonId,
        Caption: $("#lessonPostCaption").val(),
        Description: $("#lessonPostDescription").val(),
    }
    return result;
}

function showModal(id) {

    $("#" + id).modal("show");
}

function deleteFormatter(value) {
    return '<a class="badge badge-light-dark action-delete" onclick="deleteLessonPost(' + value + ')"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="red" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-trash"><polyline points="3 6 5 6 21 6"></polyline><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path></svg></a>';
}
