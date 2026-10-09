
$(document).ready(function () {
    var $progress = $("#progress");

    // Configure Ajax Defaults
    $.ajaxSetup({ cache: false });

    // Delegated Form Submission Handler
    $(document).on("click", ".btn-submit-form", function (e) {
        e.preventDefault();
        var targetFormId = $(this).data("form");

        if (targetFormId && $("#" + targetFormId).length) {
            $progress.removeClass("d-none").addClass("d-flex");
            $("#" + targetFormId).submit();
        }
    });
});
