using UnityEngine;

namespace FunkySheep.Network
{
    public enum protocol
    {
        ws,
        wss
    }

    [System.Serializable]
    [CreateAssetMenu(menuName = "FunkySheep/Network/Connection")]
    public class Connection : ScriptableObject
    {
        public protocol protocol;
        public string address;
        public int port;
        public string path;
    }
}
