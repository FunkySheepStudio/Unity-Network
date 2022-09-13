using FunkySheep.Events;
using UnityEngine;

public enum ServiceTypes
{
  create,
  find,
  patch,
  get
}

namespace FunkySheep.Network.Services
{
    public abstract class Service : ScriptableObject
    {
        public ServiceTypes type;
        public string apiPath;
        public JSONNodeEvent onReceptionEvent;
    }
}
