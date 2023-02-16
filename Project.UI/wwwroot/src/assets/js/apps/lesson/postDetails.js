var id = $("#lessonPostId").val();
var lessonId = $("#lessonId").val();
fillOperationType();

function runOperation() {
    createLessonPostFileTable();
    fillLessonPost();
}

runOperation();

var quill = new Quill('#lesson-text', {
    modules: {
        toolbar: [
            [{ header: [1, 2, false] }],
            ['bold', 'italic', 'underline'],
            ['image', 'code-block']
        ]
    },
    placeholder: 'Bir seyler yaz...',
    theme: 'snow'  // or 'bubble',
});


FilePond.registerPlugin(
    FilePondPluginImagePreview,
    FilePondPluginImageExifOrientation,
    FilePondPluginFileValidateSize,

);

var pond = FilePond.create(document.querySelector('.file-upload-multiple'));


function getQuillText() {

    var delta = quill.getContents();
    var text = quill.getText();
    var justHtml = quill.root.innerHTML;

    return justHtml;
}

function createLessonPostFileTable() {
    $('#lesson-post-file-list').bootstrapTable({
        url: siteUrl + "/DataTableProvider/GetLessonPostLinkList?lessonPostId=" + id ,
        pagination: false,
        locale: 'tr-TR',
        width:100
    })

}

function saveLessonPost() {
    $("#Loading").show();

    $.ajax({
        url: siteUrl + '/LessonOperation/UpdateLessonPost',
        type: 'POST',
        dataType: 'json',
        data: lessonPostModel(),
        success: function (data) {
            $("#Loading").hide();
            successToast("Baþarýyla güncellendi.");
            window.location.href = siteUrl + "/Lesson/Posts?id=" + lessonId;

        },
        error: function (err) {
            $("#Loading").hide();
            errorToast(err.responseJSON.message);

        }
    });

}

function saveLessonPostFile() {
    $("#Loading").show();
    console.log("postFilee", lessonPostFileModel())
    $.ajax({
        url: siteUrl + '/LessonOperation/InsertLessonPostFile',
        type: 'POST',
        data: lessonPostFileModel(),
        cache: false,
        contentType: false,
        processData: false,
        success: function (res) {
            pond.removeFiles();
            $('#lesson-post-file-list').bootstrapTable("refresh");
            $("#Loading").hide();
            successToast(res.message);
        },
        error: function (err) {
        
            $("#Loading").hide();
            errorToast(err.responseJSON.message);
        }
    });

}

function deleteLessonPostFile(id) {
    Swal.fire({
        title: 'Data silinecek onaylýyor musunuz?',
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
                url: siteUrl + '/LessonOperation/DeleteLessonPostFile?id=' + id,
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    $("#lesson-post-file-list").bootstrapTable('removeByUniqueId', id);
                    $("#Loading").hide();
                  
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
        Id: $("#lessonPostId").val(),
        LessonId: $("#lessonId").val(),
        Caption: $("#lessonPostCaption").val(),
        Description: $("#lessonPostDescription").val(),
        Text: getQuillText()
    }
    return result;
}

function lessonPostFileModel() {
    var formdata = new FormData();
    pondFiles = pond.getFiles();
    if (pondFiles) {
        for (var i = 0; i < pondFiles.length; i++) {

            formdata.append('Files', pondFiles[i].file);
        }
        formdata.append('Id', $("#lessonPostId").val());
    }
    return formdata;

}


function fillLessonPost() {
    let id = $("#lessonPostId").val();
    $.ajax({
        url: siteUrl + '/LessonOperation/GetLessonPostById?id=' + id,
        type: 'GET',
        dataType: 'json',
        //data: {lessonId:id},
        success: function (res) {
            console.log("þeyin responsu", res);
            clearLessonPostForm();
            $('#lessonPostCaption').val(res.data.caption);
            $('#lessonPostDescription').val(res.data.description);
            if (res.data.text != null) {
                quill.pasteHTML(res.data.text);
            }

            console.log(res);
        },
        error: function (err) {
            alert("hata");
            console.log(err)
        }
    });

}

function fillLesson() {
    $("#lessonPostList").empty();
    let id = $("#lessonList").val();
    $.ajax({
        url: siteUrl + '/LessonOperation/GetLessonPostListByLessonId?lessonId=' + id,
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            $.each(res.data, function (i, item) {
                $('#lessonPostList').append(new Option(item.caption, item.id));
            });
            fillLessonPost();

            console.log(res);
        },
        error: function (err) {
            alert("hata");
            console.log(err)
        }
    });


}

function fillQuill(html) {
    quill.pasteHTML(html);
}

function fillOperationType() {
    debugger;
    if (id > 0) {
        isUpdate = true;
    }
    else {
        isUpdate = false;
    }
}

function clearLessonPostForm() {

    $('#lessonPostCaption').val(null);
    $('#lessonPostDescription').val(null);

    quill.pasteHTML("");

}

function clearLessonForm() {

    $('#lessonName').val(null);

}


function showModal(id) {
    $("#" + id).modal("show");
}

function changeUpdateButton() {
    if (isUpdate) {
        $("#lessonPostWrapper").css("display", "none");
        clearLessonPostForm();
        isUpdate = false;
    } else {
        $("#lessonPostWrapper").css("display", "block");
        isUpdate = true;
    }
}


function selectLocalImage() {
    const input = document.createElement('input');
    input.setAttribute('type', 'file');

    input.click();

    // Listen upload local image and save to server
    input.onchange = () => {
        const file = input.files[0];

        // file type is only image.
        if (/^image\//.test(file.type)) {
            saveToServer(file);
        } else {
            console.warn('You could only upload images.');
        }
    };
}

function saveToServer(file) {
    console.log("formFile", file)
    const fd = new FormData();

    fd.append('image', file);
    console.log("fd", fd);
    //$.ajax({
    //    url: siteUrl + "/LessonOperation/InsertFile",
    //    type: 'POST',
    //    data: { formFile: fd },
    //    processData: false,  // tell jQuery not to process the data
    //    contentType: false,  // tell jQuery not to set contentType
    //    success: function (data) {
    //        console.log(data);
    //        alert(data);
    //    }
    //});
    const xhr = new XMLHttpRequest();
    xhr.open('POST', 'https://localhost:7253/LessonOperation/InsertFile', true);
    xhr.onload = () => {
        if (xhr.status === 200) {
            // this is callback data: url
            console.log("xhr", xhr)
            //const url = JSON.parse(xhr.responseText).data;
            //console.log("url",url)
            insertToquill(xhr.responseText);
        }
    };
    xhr.send(fd);
}

function insertToquill(url) {
    // push image url to rich quill.
    const range = quill.getSelection();
    quill.insertEmbed(range.index, 'image', `https://localhost:7253/assets/img/lesson/${url}`);
}


//quill.getModule('toolbar').addHandler('image', () => {
//    selectLocalImage();
//});


function deleteFormatter(value) {
    return '<a class="badge badge-light-dark action-delete" onclick="deleteLessonPostFile(' + value + ')"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="red" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-trash"><polyline points="3 6 5 6 21 6"></polyline><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path></svg></a>';
}

