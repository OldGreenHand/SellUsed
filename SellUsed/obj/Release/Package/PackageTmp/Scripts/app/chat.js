
$(document).ready(function () {
    //Scroll chatarea down to bottom automatically
    $(".chat_area").scrollTop($(".chat_area")[0].scrollHeight);
    //Set username that user chat with in chatheader
    var username = "Chatting with " + $('.list-group-item.active').find($('.chat-body.clearfix')).find($('.primary-font')).text();
    $("#userChatWithPlaceholders").text(username);

    //refresh char area per 3s
    setInterval(refreshChatArea, 3000);

    //set "Enter" key to submit
    $("#messageContent").keypress(function (e) {
        if (e.which === 13) {
            e.preventDefault();
            sendMessage();
        }
    });

    //select active conversation
    $('.list-group-item').click(function (e) {
        e.preventDefault();
        $(this).addClass('active').siblings().removeClass('active');
        $(this).children('.contact_sec').remove();
        refreshChatArea();
    });

    //send message
    $('#sendMessage').click(function (e) {
        e.preventDefault();
        sendMessage();

        //var chatbody = $('<li />', {
        //    "class": 'left clearfix admin_chat'
        //});

        //var imgSpan = $('<span />', {
        //    "class": 'chat-img1 pull-right'
        //});
        //var chatDiv = $('<div />', {
        //    "class": 'chat-body1 clearfix'
        //});

        ////add time
        //var now = new Date(Date.now());
        //var formatted = now.getHours() + ":" + now.getMinutes();
        //var timeDiv = $('<div />', {
        //    "class": 'chat_time pull-left',
        //    "html": formatted
        //});
        //timeDiv.innerHTML = formatted;
        //var img1 = $('<img />', {
        //    src: '/Images/user.png',
        //    "class": 'img-circle',
        //    alt: 'User Avatar'
        //});
        //$('.list-unstyled').append(chatbody.append(imgSpan.append(img1))
        //    .append(chatDiv.append($("<p></p>").text(content))
        //        .append(timeDiv)));

        //Scroll chatarea down to bottom automatically
        //$(".chat_area").scrollTop($(".chat_area")[0].scrollHeight);
    });

    //delete conversation
    $('#delete-Conversation').click(function(e) {
        var url = $(this).attr("data-url");
        var conId = $('.list-group-item.active').attr("data-conId");
        $.ajax({
            type: "POST",
            url: url,
            data: { conversationId: conId },
            dataType: "json"
        }).success(function (result) {
            alert("Delete conversation successfully!");
        });
        //remove this conversation
        $('.list-group-item.active').remove();

        //activate first child of conversation list
        if ($('#conversationList').children().length === 0) {
            $('.chat_area').empty();
            $("#userChatWithPlaceholders").text("Chat");
        } else {
            $('#conversationList').first().addClass('active');
            $('.list-group-item.active').children('.contact_sec').remove();

            refreshChatArea();
        }
    });

    function refreshChatArea() {
        $.ajax({
            type: "POST",
            url: globalUrlSettings.LoadChatAreaUrl,
            data: { conversationId: $('.list-group-item.active').attr("data-conId") },
            dataType: "html"
        }).success(function (result) {
            $('.chat_area').html(result);
            //Scroll chatarea down to bottom automatically
            $(".chat_area").scrollTop($(".chat_area")[0].scrollHeight);
            username = "Chatting with " + $('.list-group-item.active').find($('.chat-body.clearfix')).find($('.primary-font')).text();
            $('.list-group-item.active').remove(".contact_sec");
            $("#userChatWithPlaceholders").text(username);
        });
    };

    function sendMessage() {
        var content = $('#messageContent').val();
        if (content != "") {
            $('#messageContent').val("");
            var conId = $('.list-group-item.active').attr("data-conId");
            var toId = $('.list-group-item.active').attr("data-toId");
            var message = {
                Createtime: 'tmp',
                FromUserId: 'tmp',
                FromUserName: 'tmp',
                ToUserId: toId,
                ToUserName: 'tmp',
                Message: content,
                ConversationId1: conId,
                ConversationId2: conId
            };

            var url = $('#sendMessage').attr("data-url");
            var dataToPost = JSON.stringify(message);

            $.ajax({
                type: "POST",
                url: url,
                contentType: "application/json; charset=utf-8",
                data: dataToPost
            }).done(function() {
                refreshChatArea();
            });
        }
    }
})