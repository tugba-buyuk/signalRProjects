// const { error } = require("jquery");

$(function () {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5153/myhub")
        .withAutomaticReconnect([1000,2000,4000]) // parantez içinde ms cisinden periyotlar belirlenmezse default olarak 0 2 10 30 s aralıklarla istek atar
        .build();

    connection.start();

    //bağlantı hiç kurulmadığında bu fonksiyon 2s aralıklarla sürekli bağlanma isteğinde bulunur
    async function startConnecion(){
        try{
            await connection.start();
        }catch(error){
            setTimeout(()=>startConnecion(),2000)
        }
    } 
    startConnecion();

    const status=$("#status");
    function statusAnimation(){
        status.fadeIn(2000,()=>{
            setTimeout(()=>{
                status.fadeOut(2000);
            },2000)
        });
    }

    //bağlantı kurulmadan önce
    connection.onreconnecting(error=>{
        status.css("background-color","blue");
        status.css("color","white");
        status.html("Connecting to server...");
        statusAnimation();
    });

    //bağlantı kurulduğunda
    connection.onreconnected(connectionId=>{
        status.css("background-color","green");
        status.css("color","white");
        status.html("Connected to server...");
        statusAnimation();
    })

    //bağlantı kesildiğinde
    connection.onclose(connectionId=>{
        status.css("background-color","red");
        status.css("color","white");
        status.html("Could not connect to server...");
        statusAnimation();
    })

    //when a client connected
    connection.on("userJoined",connectionId=>{
        status.html(`${connectionId} joined into server.`);
        status.css("background-color","green");
        statusAnimation();
    })

    //when a client leaved
    connection.on("userLeaved",connectionId=>{
        status.html(`${connectionId} leaved from server.`);
        status.css("background-color","red");
        statusAnimation();
    })

    connection.on("clients",clientsData=>{
        let text="";
        $.each(clientsData,(index,item)=>{
            text += `<li>${item}</li>`;
        });
            $("#clientList").append(text);
    });


    $("#btnSubmit").on("click", function () {
        let message = $("#txtMessage").val();
        connection.invoke("SendMessageAsync", message).catch(error => console.log(error));
    });
    connection.on("receiveMessage",message=>{
        $("#messages").append(message+ "<br>")
    })
});