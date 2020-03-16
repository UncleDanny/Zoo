"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("Add").disabled = true;
document.getElementById("Feed").disabled = true;

connection.on("Refresh", function () {
    foo();  
});

connection.start().then(function () {
    document.getElementById("Add").disabled = false;
    document.getElementById("Feed").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});


function AddAnimal() {
    var type = $('#SelectedAddAnimal').val();
    var name = $('input').val();
    var animal = { "Name": name, "Type": type };
    connection.invoke("AddAnimal", animal).catch(function (err) {
        return console.error(err.toString());
    });
}

function FeedAnimal() {
    var animal = $('#SelectedFeedAnimal').val();
    connection.invoke("FeedAnimal", animal).catch(function (err) {
        return console.error(err.toString());
    });
}

function foo() {
    $('#reload').load('/Index?handler=AnimalPartial')
}

window.onload = function () {
    //setInterval(foo, 500);
    foo();
}

function add() {
    var type = $('#SelectedAddAnimal').val();
    var name = $('input').val();
    var animal = { "Name": name, "Type": type };
    $.ajax({
        url: '/?handler=Add',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("RequestVerificationToken",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: JSON.stringify(animal),
        success: function (result) {
            alert(result)
        },
        complete: function () {
            //do something on complete
        },
        failure: function (err) {
            alert(err); // Display error message
        }
    });
}

function feed() {
    var animal = $('#SelectedFeedAnimal').val();
    alert(animal);
    $.ajax({
        url: '/?handler=Feed',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("RequestVerificationToken",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: JSON.stringify(animal),
        success: function (result) {
            alert(result)
        },
        complete: function () {
            //do something on complete
        },
        failure: function (err) {
            alert(err); // Display error message
        }
    });
}