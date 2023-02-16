
function runOperation() {

    createUserListTable();
    $("#user-list").on("click-cell.bs.table", function (field, value, row, $el) {
        
        debugger;
        if (value != "id") {

            window.location.href = siteUrl + "/Authentication/UserDetail?id=" + $el.id;

        }
    });

}

runOperation();

FilePond.registerPlugin(
    FilePondPluginFileValidateType,
    FilePondPluginImageExifOrientation,
    FilePondPluginImagePreview,
    FilePondPluginImageCrop,
    FilePondPluginImageResize,
    FilePondPluginImageTransform,
    //   FilePondPluginImageEdit
);


var pond = FilePond.create(
    document.querySelector('.filepond'),
    {
        imagePreviewHeight: 170,
        imageCropAspectRatio: '1:1',
        imageResizeTargetWidth: 200,
        imageResizeTargetHeight: 200,
        stylePanelLayout: 'compact circle',
        styleLoadIndicatorPosition: 'center bottom',
        styleProgressIndicatorPosition: 'right bottom',
        styleButtonRemoveItemPosition: 'left bottom',
        styleButtonProcessItemPosition: 'right bottom',
    }
);

function createUserListTable() {
    $('#user-list').bootstrapTable({
        url: siteUrl + "/DataTableProvider/GetUserList",
        pagination: true,
        search: false,
        locale: 'tr-TR'
    })
}

function saveUser() {
    $("#Loading").show();
    console.log(userModel())
    $.ajax({
        url: siteUrl + '/AuthenticationOperation/InsertUser',
        type: 'POST',
        processData: false,
        contentType: false,
        data: userModel(),
        success: function (data) {
            successToast("Başarıyla Kaydedildi.");

            $('#user-list').bootstrapTable("refresh");
            $("#userModal").modal("hide");
            clearModal("userModal");
            $("#Loading").hide();

        },
        error: function (err) {
            $("#Loading").hide();
            errorToast(err.responseJSON.message);
        }
    });

}

function deleteUser(id) {
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
                url: siteUrl + '/AuthenticationOperation/DeleteUser?id=' + id,
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    $("#user-list").bootstrapTable('removeByUniqueId', id);
                    $("#Loading").hide();
                    $("#userModal").modal("hide");
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

function userModel() {

    var formdata = new FormData();
    pondFiles = pond.getFiles();
    console.log(pondFiles);
    debugger;
    if (pondFiles.length>0) {
        formdata.append('File', pondFiles[0].file);
       
    }
    formdata.append('Id', $("#id").val());
    formdata.append('FirstName', $("#firstName").val());
    formdata.append('LastName', $("#lastName").val());
    formdata.append('UserName', $("#userName").val());
    formdata.append('Email', $("#email").val());
    formdata.append('Password', $("#password").val());

    return formdata;

}

function showModal(id) {

    $("#" + id).modal("show");
}

function deleteFormatter(value) {
    return '<a class="badge badge-light-dark action-delete" onclick="deleteUser(' + value + ')"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="red" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-trash"><polyline points="3 6 5 6 21 6"></polyline><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path></svg></a>';
}

function imageFormatter(value) {
 
    if (value) {
        value = value.replace("wwwroot/", siteUrl+"/");
    }
    var imageLink = '<img src="' + value + '" alt="avatar" >';
    return '<img src="' + value +'" alt="avatar" class="rounded-circle profile-img" style="width:40px; height:40px;">';
}