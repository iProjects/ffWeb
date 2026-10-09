/**
 * Modern Register Page Logic: Password Strength, SVG Toggle, and Submission Handlers
 */

// SVG Icon paths for show/hide password states
const SVG_EYE_SLASH = '<path d="M13.359 11.238C15.06 9.72 16 8 16 8s-3-5.5-8-5.5a7.028 7.028 0 0 0-2.79.588l.77.771A5.944 5.944 0 0 1 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.134 13.134 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755-.165.165-.337.328-.517.486l.708.709z"/><path d="M11.297 9.176a3.5 3.5 0 0 0-4.474-4.474l.823.823a2.5 2.5 0 0 1 2.829 2.829l.822.822zm-2.943 1.299.822.822a3.5 3.5 0 0 1-4.474-4.474l.823.823a2.5 2.5 0 0 0 2.829 2.829z"/><path d="M3.35 5.47c-.18.16-.353.322-.518.487A13.134 13.134 0 0 0 1.172 8l.195.288c.335.48.83 1.12 1.465 1.755C4.121 11.332 5.881 12.5 8 12.5c.716 0 1.39-.133 2.02-.36l.77.772A7.029 7.029 0 0 1 8 13.5C3 13.5 0 8 0 8s.939-1.721 2.641-3.238l.708.709zm10.296 8.884-12-12 .708-.708 12 12-.708.707z"/>';
const SVG_EYE = '<path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8zM1.173 8a13.133 13.133 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.133 13.133 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5c-2.12 0-3.879-1.168-5.168-2.457A13.134 13.134 0 0 1 1.172 8z"/><path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5zM4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0z"/>';

$(document).ready(function () {
    hideProgressOverlay();
    $.ajaxSetup({ cache: false });

    initPasswordToggle();
    initPasswordStrength();
    initRegistrationSubmission();
});

function initPasswordToggle() {
    $(".btn-toggle-password").on("click", function (e) {
        e.preventDefault();
        const targetSelector = $(this).data("target");
        const $input = $(targetSelector);
        const $svg = $(this).find(".password-toggle-svg");

        if ($input.attr("type") === "password") {
            $input.attr("type", "text");
            $svg.html(SVG_EYE);
        } else {
            $input.attr("type", "password");
            $svg.html(SVG_EYE_SLASH);
        }
    });
}

function initPasswordStrength() {
    const $txtpassword = $("#txtpassword");
    const $container = $("#password-strength-container");
    const $bar = $("#password-strength-bar");
    const $text = $("#password-strength-text");
    const $feedback = $("#password-strength-feedback");

    $txtpassword.on("input", function () {
        const val = $(this).val();

        if (val.length === 0) {
            $container.addClass("d-none");
            return;
        }

        $container.removeClass("d-none");
        const evalResult = evaluatePasswordStrength(val);

        $bar.css("width", evalResult.percent + "%");
        $bar.removeClass("bg-danger bg-warning bg-info bg-success").addClass(evalResult.barClass);

        $text.text(evalResult.label);
        $text.removeClass("text-danger text-warning text-info text-success").addClass(evalResult.textClass);
        $feedback.text(evalResult.feedback);
    });
}

function evaluatePasswordStrength(password) {
    let score = 0;
    if (!password) return { percent: 0, label: "", barClass: "", textClass: "", feedback: "" };

    if (password.length >= 8) score += 25;
    if (password.length >= 12) score += 15;
    if (/[a-z]/.test(password)) score += 15;
    if (/[A-Z]/.test(password)) score += 15;
    if (/[0-9]/.test(password)) score += 15;
    if (/[^a-zA-Z0-9]/.test(password)) score += 15;

    if (score > 100) score = 100;

    if (score < 40) {
        return { percent: Math.max(score, 15), label: "Weak", barClass: "bg-danger", textClass: "text-danger", feedback: "Add numbers or special characters" };
    } else if (score < 70) {
        return { percent: score, label: "Fair", barClass: "bg-warning", textClass: "text-warning", feedback: "Mix uppercase & lowercase letters" };
    } else if (score < 85) {
        return { percent: score, label: "Good", barClass: "bg-info", textClass: "text-info", feedback: "Good password strength" };
    } else {
        return { percent: 100, label: "Strong", barClass: "bg-success", textClass: "text-success", feedback: "Strong password!" };
    }
}

function initRegistrationSubmission() {
    $("#register-form").on("submit", function (e) {
        if (!$(this).valid()) {
            return false;
        }
        showProgressOverlay();
    });

    $("#google-register-form").on("submit", function () {
        showProgressOverlay();
    });
}

function showProgressOverlay() {
    const $progress = $("#progress");
    if ($progress.length) {
        $progress.removeClass("displaynone").addClass("displayblock").show();
    }
}

function hideProgressOverlay() {
    const $progress = $("#progress");
    if ($progress.length) {
        $progress.removeClass("displayblock").addClass("displaynone").hide();
    }
}

