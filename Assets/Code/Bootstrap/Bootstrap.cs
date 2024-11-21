using Code.Loader;
using Code.UI.Handlers;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private bool _loadOnStart = false;
        [SerializeField] private Button _startLoadButton;

        [Header("Paths:")]
        [SerializeField] private string _webImageURL;
        [SerializeField] private string _resourceImagePath;
        [SerializeField] private string _sceneName;

        [Space, Header("Content containers:")]
        [SerializeField] private Image _webImage;
        [SerializeField] private Image _resourcesImage;
        [SerializeField] private Button _sceneButton;

        [Space, Header("Progress handlers:")]
        [SerializeField] private ProgressHandler _webProgressHandler;
        [SerializeField] private ProgressHandler _resourcesProgressHandler;
        [SerializeField] private ProgressHandler _sceneProgressHandler;

        private Scene _loadedScene;
        private AsyncOperation _sceneLoadOperation;

        private void Awake()
        {
            _startLoadButton.onClick.AddListener(Load);
            _sceneButton.onClick.AddListener(ActivateLoadedScene);
            _sceneButton.interactable = false;
        }

        private void Start()
        {
            if (_loadOnStart)
                Load();
        }

        private void Load()
        {
            _startLoadButton.interactable = false;
            LoadImage(new WebTextureLoader(), _webImageURL, _webImage, (float progress) => _webProgressHandler.SetProgress(progress));
            LoadImage(new ResourcesTextureLoader(), _resourceImagePath, _resourcesImage, (float progress) => _resourcesProgressHandler.SetProgress(progress));
            LoadScene(new SceneLoader(), _sceneName, _sceneButton, (float progress) => _sceneProgressHandler.SetProgress(progress));
        }

        private async void LoadImage(ILoader<Texture2D> loader, string path, Image image, Action<float> progressHandler = null)
        {
            Texture2D texture = await loader.Load(path, progressHandler);

            if (texture != null)
                image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }

        private async void LoadScene(ILoader<AsyncOperation> loader, string name, Button button, Action<float> progressHandler = null)
        {
            _sceneLoadOperation = await loader.Load(name, progressHandler);

            _loadedScene = SceneManager.GetSceneByName(name);

            if (!_loadedScene.IsValid())
                return;

            _sceneButton.interactable = true;
        }

        private async void ActivateLoadedScene()
        {
            if (!_loadedScene.IsValid() || _sceneLoadOperation == null)
                return;

            Scene currentScene= SceneManager.GetActiveScene();
            
            _sceneLoadOperation.allowSceneActivation = true;
            await _sceneLoadOperation;

            SceneManager.SetActiveScene(_loadedScene);

            await SceneManager.UnloadSceneAsync(currentScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }
    }
}