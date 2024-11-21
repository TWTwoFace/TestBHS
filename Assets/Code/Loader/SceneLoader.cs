using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Loader
{
    public sealed class SceneLoader : ILoader<AsyncOperation>
    {
        public async UniTask<AsyncOperation> Load(string name, Action<float> progressHangler = null)
        {
            var operation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

            operation.allowSceneActivation = false;

            do
            {
                progressHangler?.Invoke(operation.progress / 0.9f);

                await UniTask.Yield();
            }
            while (operation.progress != 0.9f);

            progressHangler?.Invoke(operation.progress / 0.9f);

            return operation;
        }
    }
}
