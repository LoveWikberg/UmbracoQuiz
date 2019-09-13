$(function () {
    $('[data-open-modal]').click(function () {
        var modalId = $(this).data('open-modal');
        $('#' + modalId).removeClass('hide');
    });

    $('[data-close-modal]').click(function () {
        $(this).closest('.custom-modal-container').addClass('hide');
    });

});