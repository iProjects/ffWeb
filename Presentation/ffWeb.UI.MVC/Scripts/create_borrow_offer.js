document.addEventListener('DOMContentLoaded', function () {
    'use strict';

    // Handle Client-Side Validation Bootstrap styles
    const forms = document.querySelectorAll('.needs-validation');

    Array.from(forms).forEach(function (form) {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }

            form.classList.add('was-validated');
        }, false);
    });

    // Ensure disabled fields submit their selected values if required by backend logic
    const createForm = document.getElementById('create-borrow-offer-form');
    if (createForm) {
        createForm.addEventListener('submit', function () {
            const disabledInputs = createForm.querySelectorAll('select:disabled, input:disabled');
            disabledInputs.forEach(function (input) {
                input.disabled = false;
            });
        });
    }
});

