using ZXing;
using ZXing.QrCode;
using UnityEngine;
using FunkySheep.SimpleJSON;
using System;

namespace FunkySheep.Network
{
    [AddComponentMenu("FunkySheep/Network/QrCode")]
    public class QrCode : MonoBehaviour
    {
        public void Generate(JSONNode message)
        {
            if (message["function"] == "RegistrationOk")
            {
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
                url += "/?service=user-auth&token=" + message["data"]["token"];

                Debug.Log(url);

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
