///reference path="_reference.js" />



$(document).ajaxStart(function () {
    $("#progress").removeClass('displaynone');
    $("#progress").addClass('displayblock');
    $("#progress").show();


}).ajaxStop(function () {
    $("#progress").removeClass('displayblock');
    $("#progress").addClass('displaynone');
    $("#progress").hide();
});
 

$(document).ready(function () {

    $("#btnSubmitIndexForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#home-form").submit();
    });

    $("#btnSubmitListLendOffersForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#list-lend-offers-form").submit();
    });

    $("#btnSubmitListBorrowOffersForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#list-borrow-offers-form").submit();
    });

    $("#btnSubmitListMyOffersForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#list-my-offers-form").submit();
    });

    $("#btnSubmitMyInvestMentsForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#my-investments-form").submit();
    });

    $("#btnSubmitEditProfileSubForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#edit-profile-sub-form").submit();
    });

    $("#btnSubmitMyAccountsForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#my-accounts-form").submit();
    });

    $("#btnSubmitMyLoansForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#my-loans-form").submit();
    });

    $("#btnSubmitGetMyMailingGroupsForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#my-offer-groups-form").submit();
    });

    $("#btnSubmitOfferScoresForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#offer-scores-form").submit();
    });

    $("#btnSubmitChangePasswordForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#change-password-form").submit();
    });

    $("#btnSubmitDeRegisterForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#de-register-form").submit();
    });
       
    $("#btnSubmitWithDrawCashForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#withdraw-form").submit();
    });

    $("#btnSubmitHelpForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#help-form").submit();
    });

    $("#btnSubmitContactUsForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#contact-us-form").submit();
    });

    $("#btnSubmitContactsForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#contacts-form").submit();
    });

    $("#btnSubmitAboutForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#about-form").submit();
    });

    $("#btnSubmitLogOffPartialForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#log-off-partial-form").submit();
    });

    $("#btnSubmitRegisterPartialForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#register-partial-form").submit();
    });

    $("#btnSubmitLoginPartialForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#login-partial-form").submit();
    });

    

});

 

