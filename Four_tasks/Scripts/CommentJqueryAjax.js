$(document).ready(function () {
    $('#commentSend').submit(function (e) {
        e.preventDefault();
        var data = $(this).serialize();
        var url = $('#commentSend').attr('action')
        $.ajax({
            url: url,
            type: 'POST',
            data: data,
            success: function (idComment) {
                $("#comments").empty();
                $("#commentTemplate").tmpl(idComment)
                .appendTo("#comments");
            }
        })
    });
});