using ZXing;
using ZXing.QrCode;
using UnityEngine;
using FunkySheep.SimpleJSON;

namespace FunkySheep.Network
{
    [AddComponentMenu("FunkySheep/Network/QrCode")]
    public class QrCode : MonoBehaviour
    {
        public FunkySheep.Types.String socketId;
        public void Generate(JSONNode message)
        {
            string method = message["Method"];

			if (method == "SetId")
            {
                socketId.value = message["ConnectionId"];

				FunkySheep.Network.Manager manager = FunkySheep.Network.Manager.Instance;
                string url = "";
                if (manager.connection.protocol == protocol.ws)
                {
                    url += "http://";
                } else if (manager.connection.protocol == protocol.wss)
                {
                    url += "https://";
                }

                url += manager.connection.address + ":" + manager.connection.port;
                url += "/?service=user-auth&token=" + socketId.value;

                this.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", generateQR(url));
            }
        }

        public void GetUser(JSONNode message)
        {
            if (message["function"] == "GetUser")
            {
                PlayerPrefs.SetString("user", message["data"]["user"]);
                PlayerPrefs.SetString("device", message["data"]["device"]);
                PlayerPrefs.Save();

                gameObject.SetActive(false);
            }
        }

        private static Color32[] Encode(string textForEncoding, int width, int height)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width
                }
            };
            return writer.Write(textForEncoding);
        }
        public Texture2D generateQR(string text)
        {
            var encoded = new Texture2D(256, 256);
            var color32 = Encode(text, encoded.width, encoded.height);
            encoded.SetPixels32(color32);
            encoded.Apply();
            return encoded;
        }
    }
}
