// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var commonfs = {
    showGeneralNotificationModal: function (title, message, action, icon, yesOrConfirmButtonText, noOrCloseButtonText) {
        if (message === null) return;

        if (title && title.length > 0) {
            $('#GeneralNotificationModal').find(".modal-title").text(title);
        }
        else {
            $('#GeneralNotificationModal').find(".modal-title").text("Message");
        }

        $('#GeneralNotificationModal').find("#notificationMessage").html(message);

        if (action && action.length > 0) {
            $('#GeneralNotificationModal').find('#btnConfimationOK').attr('disabled', false);
            $('#GeneralNotificationModal').find('#btnConfimationOK').attr("action", action);
            $('#GeneralNotificationModal').find('#btnConfimationOK').show();

            if (yesOrConfirmButtonText && yesOrConfirmButtonText.length > 0) {
                $('#GeneralNotificationModal').find('#btnConfimationOK').html(yesOrConfirmButtonText);
            }
            else {
                $('#GeneralNotificationModal').find('#btnConfimationOK').html('Yes');
            }
        }
        else {
            $('#GeneralNotificationModal').find('#btnConfimationOK').hide();
        }

        if (noOrCloseButtonText && noOrCloseButtonText.length > 0) {
            $('#GeneralNotificationModal').find('#btnCloseModal').text(noOrCloseButtonText);
        }
        else {
            $('#GeneralNotificationModal').find('#btnCloseModal').text('Close');
        }

        $('#GeneralNotificationModal').modal('show');
    },
    hideGeneralNotificationModal: function () {
        $('#GeneralNotificationModal').modal('hide');
        $('#GeneralNotificationModal').find(".modal-title").text('Notification');
        $('#GeneralNotificationModal').find("#notificationMessage").html('');
        $('#GeneralNotificationModal').find('#btnConfimationOK').attr("action", '');
        $('#GeneralNotificationModal').find('#btnConfimationOK').hide();
    },
    disableAndButtonByContainer: function (form, buttonType) {
        var btnSubmit = form.find(":submit");
        form.find('.btn').prop("disabled", true);
        //btnSubmit.prop("disabled", true);
        if (buttonType == buttonTypeConstants.Save) {
            btnSubmit.html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Saving');
        }
        else if (buttonType == buttonTypeConstants.DeleteConfirm) {
            btnSubmit.html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Deleting');
        }
        else if (buttonType == buttonTypeConstants.Import) {
            btnSubmit.html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Importing');
        }
    },
    enableButtonsByContainer: function (form, buttonType) {
        var btnSubmit = form.find(":submit");
        form.find('.btn').prop("disabled", false);
        //btnSubmit.prop("disabled", true);
        if (buttonType == buttonTypeConstants.Save) {
            btnSubmit.html('<i class="fal fa-save"></i> Save');
        }
        else if (buttonType == buttonTypeConstants.DeleteConfirm) {
            btnSubmit.html('<i class=" fal fa-check mr-1"></i>Confirm');
        }
        else if (buttonType == buttonTypeConstants.Import) {
            btnSubmit.html('<i class=" fal fa-save mr-1"></i>Import');
        }
    }
}
