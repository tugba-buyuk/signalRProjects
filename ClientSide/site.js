$(function () {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5153/myhub").build();

    connection.start();

    $("#btnSubmit").on("click", function () {
        let message = $("#txtMessage").val();
        connection.invoke("SendMessageAsync", message).catch(error => console.log(error));
    });
    connection.on("receiveMessage",message=>{
        $("div").append(message+ "<br>")
    })
});