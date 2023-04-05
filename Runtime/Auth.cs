using UnityEngine;
using FunkySheep.SimpleJSON;

namespace FunkySheep.Network
{
    [AddComponentMenu("FunkySheep/Network/Auth")]
    public class Auth : MonoBehaviour
    {
        public void Generate(JSONNode message)
        {
            string method = message["Method"];

            if (method == "SetUrl")
            {
                string url = message["Url"];
                Texture2D qrCode = FunkySheep.QrCode.Manager.Generate(url);

                this.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", qrCode);
            }

            if (method == "AuthOk")
            {
                gameObject.SetActive(false);
            }
        }
    }
}
