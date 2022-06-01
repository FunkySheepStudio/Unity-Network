using FunkySheep.SimpleJSON;

namespace FunkySheep.Network
{
    public class Message
    {
        public JSONNode body = JSON.Parse("{}");

        public Message(string service, string request)
        {
            body["service"] = service;
            body["request"] = request;
            body["sentAt"] = Time.Now();
        }

        public void Send()
        {
            Manager.Instance.Send(this.body.ToString());
        }
    }
}
