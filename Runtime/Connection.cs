using UnityEngine;

namespace FunkySheep.Network
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "FunkySheep/Network/Connection")]
    public class Connection : ScriptableObject
    {
        public string address;
        public int port;
    }
}
