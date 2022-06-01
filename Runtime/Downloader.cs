using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace FunkySheep.Network
{
    public static class Downloader
    {
        public static IEnumerator Download(string url, Action<string, byte[]> Callback)
        {
            byte[] file = null;
            string cachedFileName = FunkySheep.Crypto.Hash(url) + FunkySheep.Files.Utils.GetExtension(url);
            // Chech in cache
            file = FunkySheep.Files.Cache.Get(cachedFileName);
            if (file != null)
            {
                Callback(cachedFileName, file);
                yield break;
            }
            else
            { // Not in cache we download
                UnityWebRequest request = UnityWebRequest.Get(url);

                yield return request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(request.error);
                    Debug.Log(url);
                }
                else
                {
                    file = request.downloadHandler.data;
                    FunkySheep.Files.Cache.Set(file, cachedFileName);
                    Callback(cachedFileName, file);
                    yield break;
                }
            }
        }
    }
}
