const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
})

function errorToast(message) {
    Toast.fire({ icon: 'error', title: message});
}

function successToast(message) {
    Toast.fire({ icon: 'success', title: message });
}

function warningToast(message) {
    Toast.fire({ icon: 'warning', title: message });
}

function DateTimeFormatter(value) {
    return value ? moment(value).format('DD.MM.YYYY HH:mm') : null;
}

function clearModal(id) {
    $("#" + id).find('input').val(null);
}

