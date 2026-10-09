$(document).ready(function () {
    // Hide progress indicator initially[span_1](start_span)[span_1](end_span)
    hideLoadingSpinner();

    // Configure global AJAX caching[span_2](start_span)[span_2](end_span)
    $.ajaxSetup({ cache: false });
});

/**
 * Utility to display the modern loading overlay
 */
function showLoadingSpinner() {
    $("#progress")
        .removeClass("displaynone")
        .addClass("displayblock")
        .fadeIn(150);
}

/**
 * Utility to hide the modern loading overlay
 */
function hideLoadingSpinner() {
    $("#progress")
        .fadeOut(150, function () {
            $(this).removeClass("displayblock").addClass("displaynone");
        });
}

