$(document).ready(function () {
    // Hide progress bar initially
    $("#progress").hide().addClass("displaynone");

    // Configure global AJAX setup
    $.ajaxSetup({ cache: false });

    // Handle Form Action Button Clicks
    $(".btn-action-submit").on("click", function (e) {
        e.preventDefault();

        var formId = $(this).data("form-id");

        if (formId) {
            showLoadingSpinner();
            $("#" + formId).submit();
        }
    });
});

/**
 * Shows the full-screen loading overlay.
 */
function showLoadingSpinner() {
    $("#progress")
        .removeClass("displaynone")
        .addClass("displayblock")
        .fadeIn(150);
}

/**
 * Hides the full-screen loading overlay.
 */
function hideLoadingSpinner() {
    $("#progress")
        .fadeOut(150, function () {
            $(this).removeClass("displayblock").addClass("displaynone");
        });
}

