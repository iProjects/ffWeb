
$(document).ready(function () {
    // Cache Selectors
    var $range = $("#DashBoardAmountrange");
    var $valueInput = $("#DashBoardAmountValue");
    var $amountDisplay = $("#AmountDisplay");
    var $progress = $("#progress");

    // Configure Ajax Defaults
    $.ajaxSetup({ cache: false });

    // Format number to Kenyan Shillings currency string
    function formatCurrency(val) {
        var numericVal = parseInt(val, 10);
        return "Ksh " + (isNaN(numericVal) ? "0" : numericVal.toLocaleString());
    }

    // Sync Slider Range to Text Input & Display Label
    $range.on("input change", function () {
        var currentVal = $(this).val();
        $valueInput.val(currentVal);
        $amountDisplay.text(formatCurrency(currentVal));
    });

    // Sync Text Input to Slider Range & Display Label
    $valueInput.on("input change", function () {
        var currentVal = $(this).val();
        $range.val(currentVal);
        $amountDisplay.text(formatCurrency(currentVal));
    });

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










