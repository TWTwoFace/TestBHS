using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Code.Loader
{
    public sealed class WebTextureLoader : ILoader<Texture2D>
    {
        public async UniTask<Texture2D> Load(string uri, Action<float> progressHandler = null)
        {
            using (var request = UnityWebRequestTexture.GetTexture(uri))
            {
                UniTask task = request.SendWebRequest().ToUniTask();

                do
                {
                    progressHandler?.Invoke(request.downloadProgress * 0.9f);
                    await UniTask.Yield();
                }
                while (task.Status == UniTaskStatus.Pending);

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Unable to download image from {uri}");
                    return null;
                }

                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                progressHandler?.Invoke(request.downloadProgress);

                return texture; 
            }
        }
    }
}
