﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            console.log(res);
            $("#modal-xl .modal-body").html(res);
            $("#modal-xl .modal-title").html(title);
            $("#modal-xl").modal("show");

        }
    })
}