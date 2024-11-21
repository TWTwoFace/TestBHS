using Cysharp.Threading.Tasks;
using System;

namespace Code.Loader
{
    public interface ILoader<T>
    {
        UniTask<T> Load(string path, Action<float> progressHangler = null);
    }
}