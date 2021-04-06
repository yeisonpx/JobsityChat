// Write your JavaScript code.
const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
const MESSAGE_COUNT_LIMIT = 50;
const showMessage = (request) => {
    let messageContent = $(`<span class="messageText">${request.message}</span>`);
    let messageHeader = $(`<span class="messageHeader">${request.userName}</span>`);
    let messageItem = $(`<li class="messageItem"></li>`);
    let lastMsg = $("#message-container .message-list li.messageItem").last();
    let messageList = $(".messageText");
    if (messageList.length >= MESSAGE_COUNT_LIMIT) {
        messageList.first().remove();
    }

    if (lastMsg.attr("sender-id") == request.userName) {        
        lastMsg.append(messageContent)
    } else {
        messageItem.append(messageHeader);
        messageItem.append(messageContent);
        messageItem.attr("sender-id", request.userName);
        $("#message-container .message-list").append(messageItem);
    }
}
const sendMessage = () => {
    let txtMessage = $("#message-text");
    let messageText = txtMessage.val(); 
    let targetUser = $("#CurrentUserId").val();  
    if (messageText.length > 0) {
        console.log(messageText.length);
        connection.invoke("SendMessage", {
            message: messageText.replace(/(\r\n|\n|\r)/gm, ""),
            targetUser: targetUser,
        }).catch((err) => console.log(err));
        txtMessage.val("");
        txtMessage.focus(); 
    }
}

$(document).ready(function () {
    let btnSendMessage = $("#BtnSendMessage");
    btnSendMessage.attr("disabled", "true");
    connection.on("ReceivedMessage", (message) => {
        showMessage(message);
    });

    connection.start().then(function () {
        document.getElementById("btnSendMessage").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });
    $("#btnSendMessage").on("click", sendMessage);
    $("#message-text").keypress((e) => {
        if (e.keyCode == 13) {
            sendMessage();
        }
    });
});

