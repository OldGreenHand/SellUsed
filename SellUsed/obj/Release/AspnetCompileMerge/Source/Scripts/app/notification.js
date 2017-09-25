$(function () {
    // Proxy created on the fly
    var con = $.connection.notificationHub;


    // Declare a function on the job hub so the server can invoke it
    con.client.displayStatus = function () {
        getData();
    };

    // Start the connection
    $.connection.hub.start();
    getData();
});

function getData() {
    $.ajax({
        url: '../api/values',
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            if (data.length > 0) {
                if (sessionStorage.getItem("not_shown_unread") === null) {
                    sessionStorage.setItem("not_shown_unread", 0);
                };
                var totalUnreadMessage = 0;
                //initiate toasts array
                var toasts = [];

                for (var j = 0; j < data.length; j++) {
                    totalUnreadMessage += data[j].UnreadMessageNo;
                    if (totalUnreadMessage >= sessionStorage.getItem("not_shown_unread")) {
                        //create toast and push it into array
                        var msg = data[j].UnreadMessageNo + ' Unread Messages From ' + data[j].ToUserName;
                        toasts.push(new Toast('warning', 'toast-top-right', msg));
                    }
                };
                //sessionStorage.setItem("not_shown_unread", totalUnreadMessage);

                //display toasts
                Display(toasts);

                $('#unreadLabel').removeClass('label label-success').empty();
                $('#unreadLabel').addClass('label label-success').append(totalUnreadMessage);
            }
        }
    });
}


function Toast(type, css, msg) {
    this.type = type;
    this.css = css;
    this.msg = msg;
}

function Display(toasts) {
    var i = 0;
    var t = toasts;
    delayToasts();

    function delayToasts() {
        if (i === toasts.length) { return; }
        var delay = i === 0 ? 0 : 2100;
        window.setTimeout(function () { showToast(); }, delay);

    }

    function showToast() {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": true,
            "progressBar": true,
            "positionClass": t[i].css,
            "preventDuplicates": true,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
        toastr[t[i].type](t[i].msg);
        //toastr[t[i].type]("<div><p>" + t[i].msg + "</p><button type="button" id="surpriseBtn" class="btn btn- success" onclick="location.href = '@Url.Action("Index", "Chat")'" style="margin: 0 8px 0 8px">Chat</button></div>");
        i++;
        delayToasts();
    }
}