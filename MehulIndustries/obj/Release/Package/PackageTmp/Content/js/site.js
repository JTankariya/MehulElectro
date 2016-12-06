﻿$(document).ajaxSend(function (evt, request, settings) {
    if (settings.url.indexOf('/CheckEmailId') == -1 &&
        settings.url.indexOf('/CheckUserName') == -1 &&
        settings.url.indexOf('/GetAllParties') == -1 &&
        settings.url.indexOf('/CheckDuplicateUserName') == -1 &&
        settings.url.indexOf('/CheckDuplicateName') == -1) {
        $('#loader-wrapper').fadeIn();
    }
});


$(document).bind('ajaxStop', function () {
    $('#loader-wrapper').fadeOut();
});

$(document).ajaxError(function (event, jqxhr, settings, thrownError) {
    if (jqxhr.responseText) {
        $.Notification.notify('black', 'top right', 'Server Error :(', jqxhr.responseText);
    }
});

function GetNumericValue(value) {
    if (isNaN(value) || value.length == 0) {
        return 0;
    }
    if (value.length > 0) {
        return parseFloat(value);
    }
}

$(document).ready(function () {
    $('body').on('keypress', '.numeric', function (evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
          && (charCode < 48 || charCode > 57))
            return false;
        if (charCode == 46 && this.value.indexOf('.') != -1) {
            return false;
        }
        if (this.value.indexOf('.') != -1 && this.value.split('.')[1].length > 3) {
            return false;
        }
        return true;
    });
});