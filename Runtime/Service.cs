using FunkySheep.Events;
using UnityEngine;

namespace FunkySheep.Network.Services
{
    [CreateAssetMenu(menuName = "FunkySheep/Network/Service")]
    public class Service : ScriptableObject
    {
        public JSONNodeEvent onReceptionEvent;
    }
}
