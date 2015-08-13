$(document).ready(function () {
    $('#login-trigger').click(function (e) {
        e.preventDefault();
        $(this).next('#login-content').slideToggle();
        $(this).toggleClass('active');

        if ($(this).hasClass('active')) $(this).find('span').html('&#x25B2;')
        else $(this).find('span').html('&#x25BC;')

        if (!$('#register-trigger').hasClass('active')) {
            $('#register-trigger').find('span').html('&#x25B2;');
            $('#register-trigger').next('#register-content').slideToggle();
            $('#register-trigger').toggleClass('active');
        }
    })
    $('#register-trigger').click(function (e) {
        e.preventDefault();
        $(this).next('#register-content').slideToggle();
        $(this).toggleClass('active');

        if ($(this).hasClass('active')) $(this).find('span').html('&#x25B2;')
        else $(this).find('span').html('&#x25BC;')

        if (!$('#login-trigger').hasClass('active')) {
            $('#login-trigger').find('span').html('&#x25B2;');
            $('#login-trigger').next('#login-content').slideToggle();
            $('#login-trigger').toggleClass('active');
        }
    })

    $('#login-content form').submit(AjaxAccountRequest);
    $('#register-content form').submit(AjaxAccountRequest);
});

function AjaxAccountRequest(e) {
    e.preventDefault();
    $.ajax({
        url: $(this).attr('action'),
        type: "POST",
        data: $(this).serialize(),
        success: function (data) {
            if ("Error" != data) {
                var account = '<ul>\
                                <li>\
                                    <label for="User">' + data + '</label>\
                                </li>\
                                <li>\
                                    <form action="/Account/LogOff" method="post"><input id="submit" value="LogOff" type="submit"></form>\
                                </li>\
                                </ul>';
                $('nav').empty();
                $('nav').append(account);
            }
            else {
                $('#inputs #username').val('');
                $('#inputs #username').attr('placeholder', 'Wrong e-mail or password, try again');
            }
        }
    })
}