using System.Collections.Generic;
using UnityEngine;

namespace FunkySheep.Network.Services
{
    [CreateAssetMenu(menuName = "FunkySheep/Network/Services/Create")]
    public class Create : Service
    {
        public bool ack = false;
        public List<FunkySheep.Types.Type> fields;

        public void Execute()
        {
            Message msg = new Message(this.apiPath, "create");
            fill(msg);
            msg.Send();
        }

        private void fill(Message msg)
        {
            msg.body["params"]["ack"] = ack;

            fields.ForEach(field =>
            {
                msg.body["data"][field.apiName == "" ? field.name : field.apiName] = field.toJSONNode();
            });
        }
    }
}
