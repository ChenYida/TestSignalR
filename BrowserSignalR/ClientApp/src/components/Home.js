import React, { Component } from "react";
import * as signalR from "@microsoft/signalr";

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <input id="input1" />
        <button onClick={send}>Send</button>
        <button onClick={clear}>Clear</button>
        <ul id="u1" />
      </div>
    );
  }
}

var connection = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:44316/")
  .withAutomaticReconnect()
  .build();

connection.on("Chat", function(message) {
  var li = document.createElement("li");
  li.textContent = message;
  document.getElementById("u1").appendChild(li);
});

connection.start().catch(err => console.error(err.toString()));

function send() {
  var message = document.getElementById("input1").value;
  connection.invoke("Chat", message).catch(function(err) {
    return console.error(err.toString());
  });
}

function clear() {
  document.getElementById("u1").innerHTML = "";
}
