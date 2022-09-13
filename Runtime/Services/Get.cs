using System.Collections.Generic;
using UnityEngine;

namespace FunkySheep.Network.Services
{
    [CreateAssetMenu(menuName = "FunkySheep/Network/Services/Get")]
    public class Get : Service
    {
        public FunkySheep.Types.Type recordKey;

        public void Execute()
        {
            Message msg = new Message(this.apiPath, "get");
            fill(msg);
            msg.Send();
        }

        private void fill(Message msg)
        {
            msg.body["data"]["key"] = recordKey.toJSONNode();
        }
    }
}
