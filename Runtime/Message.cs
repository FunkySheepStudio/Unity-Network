using FunkySheep.SimpleJSON;

namespace FunkySheep.Network
{
    public class Message
    {
        public JSONNode body = JSON.Parse("{}");

        public Message(string service, string function)
        {
            body["service"] = service;
            body["function"] = function;
        }

        public void Send()
        {
            Manager.Instance.Send(this.body.ToString());
        }
    }
}
