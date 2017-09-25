$(document).ready(function () {
    $('#InputUsername').prop('disabled', false);
    $('#InputEmail').prop('disabled', false);

    $('#InputEmail').keyup(function() {
        var email = $(this).val();

        //validate the length
        if (email.length === 0) {
            $('#InputUsername').prop('disabled', false);
        } else {
            $('#InputUsername').val('');
            $('#InputUsername').prop('disabled', true);
        }
    });

    $('#InputUsername').keyup(function () {
        var username = $(this).val();

        //validate the length
        if (username.length === 0) {
            $('#InputEmail').prop('disabled', false);
        } else {
            $('#InputEmail').val('');
            $('#InputEmail').prop('disabled', true);
        }
    });
    
});