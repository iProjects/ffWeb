$(document).ready(function () {
    const $form = $('#createOfferForm');

    // Enhanced Mobile & Desktop Form Validation State Handling
    if ($form.length && $.validator) {
        $.validator.setDefaults({
            highlight: function (element) {
                $(element).addClass('is-invalid').removeClass('is-valid');
            },
            unhighlight: function (element) {
                $(element).removeClass('is-invalid').addClass('is-valid');
            },
            errorElement: 'span',
            errorClass: 'invalid-feedback d-block mt-1',
            errorPlacement: function (error, element) {
                if (element.parent('.input-group').length || element.parent('.form-floating').length) {
                    error.insertAfter(element.parent());
                } else {
                    error.insertAfter(element);
                }
            }
        });
    }

    // Touch-friendly smooth focus behavior for mobile inputs
    $('.form-control, .form-select').on('focus', function () {
        if ($(window).width() < 576) {
            $(this).get(0).scrollIntoView({ behavior: 'smooth', block: 'center' });
        }
    });
});