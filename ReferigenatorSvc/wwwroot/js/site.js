// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function () {
    if ($.validator && $.validator.unobtrusive) {
        $.validator.addMethod('comparaevalidator', function (value, element, params) {
            return parseFloat(value) <= parseFloat($("#" + params.value).val());
        });

        $.validator.unobtrusive.adapters.add('comparaevalidator', ['value'], function (options) {
            options.rules['comparaevalidator'] = options.params;
            options.messages['comparaevalidator'] = options.message;
        });

    }

   
})();

"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notify", {
    //skipNegotiation: true,
    //transport: signalR.HttpTransportType.WebSockets
}).build();


connection.on("ReceiveMessage", function (user, message) {
   
});

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    debugger;
//    connection.invoke("SendMessage", "user", "message").catch(function (err) {
//        return console.error(err.toString());
//    });
    
//    event.preventDefault();
//});
