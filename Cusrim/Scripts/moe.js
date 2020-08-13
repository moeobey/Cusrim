$(document).ready(function () {
  
    var table = $("#table").DataTable();
    setTimeout(function () {
                $(".alert-success").fadeOut(100, null);
            },
        3000);
    setTimeout(function () {
        $(".alert-danger").fadeOut(100, null);
    },
        3000);

    });

