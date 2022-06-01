using FunkySheep.Events;
using UnityEngine;

namespace FunkySheep.Network.Services
{
    public abstract class Service : ScriptableObject
    {
        public string apiPath;
        public JSONNodeEvent onReceptionEvent;
    }
}
