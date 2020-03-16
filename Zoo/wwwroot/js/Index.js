"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("Refresh", function () {
    reloadAnimalPartial();
});

connection.start()

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
    var animal = { "Name": name, "Type": type };
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