$(document).ready(function () {
    $('.contactsLink a').click(otherPage);
});

function otherPage (e) {
    e.preventDefault();        
    var url = $(this).attr('href');
    $.ajax({
        url: url,
        type: 'POST',            
        success: function (contactsInfo) {
            var contacts = '';
            $('.contacts').empty();
            for (var i = 0; i < contactsInfo.contacts.length; i++) {
                contacts += '<div class="contactInfo">\
                    <div class="display-label">Age</div>\
                    <div class="display-field">'+ contactsInfo.contacts[i].Age + '</div>\
                    <div class="display-label">Name</div>\
                    <div class="display-field">'+ contactsInfo.contacts[i].Name + '</div>\
                    <div class="display-label">Phone Number</div>\
                    <div class="display-field">' + contactsInfo.contacts[i].Phone + '</div>\
                 </div>';
            }

            $('.contacts').append(contacts);

            var temp = '';
            if (contactsInfo.page > 1) temp += '<a href="/?Page=' + (contactsInfo.page - 1) + '">Back</a>';
            temp += '<span> Page : ' + contactsInfo.page + ' of ' + contactsInfo.pageCount + ' </span>';
            if (contactsInfo.page < contactsInfo.pageCount) temp += '<a href="/?Page=' + (contactsInfo.page + 1) + '">Next</a>';
            $('.contactsLink').empty();
            $('.contactsLink').append(temp);
            $('.contactsLink a').click(otherPage);
        }
    })
}