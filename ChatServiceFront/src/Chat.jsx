import React, { useState, useEffect } from "react";
import * as signalR from "@microsoft/signalr";
import axios from "axios";

const Chat = () => {
  const [connection, setConnection] = useState(null);
  const [messages, setMessages] = useState([]);
  const [onlineUsers, setOnlineUsers] = useState([]);
  const [user, setUser] = useState("");
  const [message, setMessage] = useState("");

  const API_URL = "https://localhost:5001/api/chat";
  const HUB_URL = "https://localhost:5001/chathub";

  useEffect(() => {
    if (!user) return;

    const connect = new signalR.HubConnectionBuilder()
      .withUrl(`${HUB_URL}?user=${encodeURIComponent(user)}`)
      .withAutomaticReconnect()
      .build();

    connect.on("ReceiveMessage", (msg) => {
      setMessages((prev) => [...prev, msg]);
    });

    connect.on("UpdateUserList", (users) => {
      setOnlineUsers(users);
    });

    connect.start()
      .then(() => setConnection(connect))
      .catch((err) => console.error("SignalR error:", err));

    return () => {
      connect.stop();
    };
  }, [user]);

  useEffect(() => {
    axios.get(`${API_URL}/all`)
      .then((res) => setMessages(res.data))
      .catch((err) => console.error("Error fetching messages:", err));
  }, []);

  const sendMessage = async () => {
    if (connection && user && message) {
      try {
        await connection.invoke("SendMessage", user, message);
        setMessage("");
      } catch (err) {
        console.error("Error sending message:", err);
      }
    }
  };

  return (
    <div className="p-4 max-w-lg mx-auto bg-white shadow-md rounded-xl">
      <h1 className="text-xl font-bold mb-4">Chat</h1>

      <input
        type="text"
        placeholder="Your name..."
        value={user}
        onChange={(e) => setUser(e.target.value)}
        className="border p-2 w-full mb-2 rounded"
      />

      <div className="mb-2 p-2 bg-gray-100 rounded">
        <strong>Online users:</strong>{" "}
        {onlineUsers.length === 0 ? "None" : onlineUsers.join(", ")}
      </div>

      <div className="border h-64 overflow-y-auto p-2 mb-2 rounded bg-gray-50">
        {messages.map((m, idx) => (
          <div key={idx} className="mb-1">
            <span className="font-semibold">{m.sender}: </span>
            <span>{m.text}</span>
            <span className="text-xs text-gray-500 ml-2">
              ({new Date(m.timestamp).toLocaleTimeString()})
            </span>
          </div>
        ))}
      </div>

      <div className="flex">
        <input
          type="text"
          placeholder="Type a message..."
          value={message}
          onChange={(e) => setMessage(e.target.value)}
          className="border p-2 flex-grow rounded-l"
        />
        <button
          onClick={sendMessage}
          className="bg-blue-500 text-white px-4 rounded-r"
        >
          Send
        </button>
      </div>
    </div>
  );
};

export default Chat;
