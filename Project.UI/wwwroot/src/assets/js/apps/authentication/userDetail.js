function runOperation() {
    createUserClaimListTable();

   


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
        files: [
            {
                // the server file reference
                source: userDetail.profilPhoto.filePath.replace("wwwroot", ""),

                // set type to limbo to tell FilePond this is a temp file
                options: {
                    type: 'image/png',
                },
            }]
    }
);


function createUserClaimListTable() {
    $('#user-operation-claim-list').bootstrapTable({
        url: siteUrl + "/DataTableProvider/GetUserOperationClaimList?userId=" + userDetail.id,
        pagination: true,
        search: false,
        locale: 'tr-TR'
    })
}

function userModel() {

    var formdata = new FormData();
    pondFiles = pond.getFiles();
    console.log(pondFiles);
    debugger;
    if (pondFiles.length > 0) {
        formdata.append('File', pondFiles[0].file);

    }
    formdata.append('Id', userDetail.id);
    formdata.append('FirstName', $("#firstName").val());
    formdata.append('LastName', $("#lastName").val());
    formdata.append('UserName', $("#userName").val());
    formdata.append('Email', $("#email").val());
    formdata.append('Password', $("#password").val());
    formdata.append('ClaimId', $("#operationClaim").val());

    return formdata;

}

function saveUpdatedUser() {
    $("#Loading").show();
    console.log(userModel())
    $.ajax({
        url: siteUrl + '/AuthenticationOperation/UpdateUser',
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