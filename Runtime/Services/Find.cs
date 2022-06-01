using System.Collections.Generic;
using UnityEngine;
using FunkySheep.SimpleJSON;

namespace FunkySheep.Network.Services
{
    [CreateAssetMenu(menuName = "FunkySheep/Network/Services/Find")]
    public class Find : Service
    {
        public JSONNode query;

        public void Execute()
        {
            Message msg = new Message(this.apiPath, "find");
            fill(msg);
            msg.Send();
        }

        private void fill(Message msg)
        {
            msg.body["data"]["query"] = query;
        }
    }
}
