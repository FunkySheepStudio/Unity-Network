using System.Collections.Generic;
using UnityEngine;
using FunkySheep.SimpleJSON;

namespace FunkySheep.Network.Services
{
    [CreateAssetMenu(menuName = "FunkySheep/Network/Services/Patch")]
    public class Patch : Service
    {
        public FunkySheep.Types.String id;
        public JSONNode parameters;
        public List<FunkySheep.Types.Type> fields;
        public bool ack = false;

        public void Execute()
        {
            Message msg = new Message(this.apiPath, "patch");
            fill(msg);
            msg.Send();
        }

        private void fill(Message msg)
        {
            msg.body["key"] = id.toJSONNode();
            msg.body["params"] = parameters;

            msg.body["params"]["ack"] = ack;

            fields.ForEach(field =>
            {
                msg.body["data"][field.apiName == "" ? field.name : field.apiName] = field.toJSONNode();
            });
        }
    }
}
