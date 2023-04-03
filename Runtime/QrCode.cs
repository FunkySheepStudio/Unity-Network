using ZXing;
using ZXing.QrCode;
using UnityEngine;
using FunkySheep.SimpleJSON;

namespace FunkySheep.Network
{
    [AddComponentMenu("FunkySheep/Network/QrCode")]
    public class QrCode : MonoBehaviour
    {
        public void Generate(JSONNode message)
        {
            string method = message["Method"];

			if (method == "SetUrl")
            {
                string url = message["Url"];
                this.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", generateQR(url));
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
