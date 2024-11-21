using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Code.Loader
{
    public sealed class ResourcesTextureLoader : ILoader<Texture2D>
    {
        public async UniTask<Texture2D> Load(string path, Action<float> progressHangler = null)
        {
            var request = Resources.LoadAsync<Texture2D>(path);

            do
            {
                progressHangler?.Invoke(request.progress);
                await UniTask.Yield();
            }
            while (!request.isDone);

            progressHangler?.Invoke(request.progress);

            return request.asset as Texture2D;
        }
    }
}
