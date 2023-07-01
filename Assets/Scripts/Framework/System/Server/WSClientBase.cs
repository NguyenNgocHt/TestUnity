#if SERVER
using Framework;
using Framework.SimpleJSON;
using Spine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using WebSocketSharp;

namespace Framework {
    public abstract class WSClientBase : Singleton<WSClientBase> 
    {
        public WebSocket ws;
        protected virtual void Start()
        {
            ws = new WebSocket(ServerConfig.WebSocketURL + "?id="+ ServerConfig.Id + "&token=");
            ws.Connect();
            ws.OnOpen += OnOpen;
            ws.OnMessage += OnMessage;
            ws.OnError += OnError;

        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            ws.OnOpen -= OnOpen;
            ws.OnMessage -= OnMessage;
            ws.OnError -= OnError;
        }
        public static void Send(JSONNode json)
        {
            Instance.ws.Send(json.ToString());
            Debug.Log(json);
        }
        public void OnOpen(object sender, EventArgs e)
        {
            Debug.Log("Open " + ((WebSocket)sender).Url);
        }
        public void OnMessage(object sender, MessageEventArgs e)
        {
            Debug.Log("Data " + JSON.Parse(e.Data)["id"] + " :" + e.Data);
            MainThreadDispatcher.ExecuteOnMainThread(() =>
            {
                JSONNode idJson = JSON.Parse(e.Data)["id"];
                if (idJson != null)
                {
                    GameServerEvent id = (GameServerEvent)int.Parse(idJson);
                    if (ServerMessenger.eventTable.ContainsKey(id))
                    {
                        ServerMessenger.Broadcast(id, JSON.Parse(e.Data));
                    }
                }
            });
        }
        public void OnError(object sender, ErrorEventArgs e)
        {
            Debug.Log("Error : " + e.Exception);
        }


    }
}
#endif