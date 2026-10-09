document.addEventListener('DOMContentLoaded', function () {
    initPartialNavigation();
});

function initPartialNavigation() {
    const loginPartialForm = document.getElementById('login-partial-form');
    const registerPartialForm = document.getElementById('register-partial-form');
    const logoffPartialForm = document.getElementById('log-off-partial-form');

    if (loginPartialForm) {
        loginPartialForm.addEventListener('submit', function () {
            showLoadingProgress();
        });
    }

    if (registerPartialForm) {
        registerPartialForm.addEventListener('submit', function () {
            showLoadingProgress();
        });
    }

    if (logoffPartialForm) {
        logoffPartialForm.addEventListener('submit', function () {
            showLoadingProgress();
        });
    }
}