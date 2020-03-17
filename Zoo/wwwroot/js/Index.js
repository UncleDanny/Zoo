"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("Add").disabled = true;
document.getElementById("Feed").disabled = true;

connection.on("Refresh", function () {
    reloadAnimalPartial();
});

connection.start().then(function () {
    document.getElementById("Add").disabled = false;
    document.getElementById("Feed").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

window.onload = function () {
    reloadAnimalPartial()
}

function reloadAnimalPartial() {
    $('#reload').load('/Index?handler=AnimalPartial')
}

function validate() {
    document.getElementById('Add').disabled = document.getElementById('Name').value.length == 0;
}

function disableEnter() {
    if (event.keyCode == 13) {
        event.preventDefault();
        return false;
    }
}

function addAnimal() {
    var type = $('#SelectedAddAnimal').val();
    var name = $('input').val();
    var gender = $('#SelectedGenderAnimal').val();
    var animal = { "Name": name, "Type": type, "Gender": gender };
    connection.invoke("AddAnimal", animal).catch(function (err) {
        return console.error(err.toString());
    });
}

function feedAnimal() {
    var animal = $('#SelectedFeedAnimal').val();
    connection.invoke("FeedAnimal", animal).catch(function (err) {
        return console.error(err.toString());
    });
}