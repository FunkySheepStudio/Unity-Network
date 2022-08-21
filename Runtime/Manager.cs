using FunkySheep.SimpleJSON;
using NativeWebSocket;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FunkySheep.Network
{
    [AddComponentMenu("FunkySheep/Network/Network manager")]
    public class Manager : FunkySheep.Types.Singleton<Manager>
    {
        public Connection connection;
        public FunkySheep.Events.SimpleEvent onConnect;
        public FunkySheep.Events.SimpleEvent onDisconnect;
        public List<Services.Service> services = new List<Services.Service>();
        public WebSocket webSocket;

        private void Start()
        {
            Connect();
        }

        private void Update()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            if (webSocket != null && webSocket.State == WebSocketState.Open)
            {
                webSocket.DispatchMessageQueue();
            }
#endif
        }

        public async void Connect()
        {
            if (connection != null)
            {
                webSocket = WebSocketFactory.CreateInstance(connection.address + ":" + connection.port);
                //  Binding the events
                webSocket.OnOpen += onConnectionOpen;
                webSocket.OnClose += onConnectionClose;
                webSocket.OnError += onConnectionError;
                webSocket.OnMessage += onMessage;
                await webSocket.Connect();
            }
        }

        private void onConnectionOpen()
        {
            if (onConnect != null)
            {
                onConnect.Raise();
            }
        }

        private void onConnectionClose(WebSocketCloseCode code)
        {
            if (onDisconnect != null)
            {
                onDisconnect.Raise();
            }
        }

        private void onConnectionError(string errMsg)
        {
        }

        private void onMessage(byte[] msg)
        {
            string strMsg = Encoding.UTF8.GetString(msg);
            JSONNode msgObject = JSON.Parse(strMsg);
            string msgService = msgObject["service"];
            string msgRequest = msgObject["request"];
            string msgServiceType = msgObject["method"];

            services.FindAll(service => service.apiPath == msgService && service.type.ToString() == msgServiceType)
              .ForEach(service =>
              {
                  //  Raise the event
                  if (service.onReceptionEvent)
                  {
                      service.onReceptionEvent.Raise(msgObject);
                  }
              });

            //Debug.Log("Received message: " + strMsg + this);
        }

        async void OnApplicationQuit()
        {
            if (webSocket != null && webSocket.State == WebSocketState.Open)
            {
                await webSocket.Close();
            }
        }

        public void Send(string message)
        {
            if (webSocket != null && webSocket.State == WebSocketState.Open)
            {
                webSocket.SendText(message);
            } else
            {
                Debug.Log("Network not connected");
            }
        }
    }
}
