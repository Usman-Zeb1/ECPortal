$(document).ready(function () {
    var today = new Date().toISOString().split('T')[0];
    $('input[type="date"]').each(function () {
        if (!$(this).val()) {
            $(this).val(today);
        }
    });
});
