using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Constant;
using Cysharp.Threading.Tasks;
using EventStruct;
using Features.Utilities.ToastModal;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace LoadingModule
{
    public class LoadingManager : SingletonMonoBehaviour<LoadingManager>
    {
        [SerializeField] private GameObject overlayView;
        [SerializeField] private GameObject fullScreenView;

        private SceneIndexEnum _currentScene;

        private readonly List<LoadingEventData> _tasks = new();

        private bool IsLoadingShow { get; set; }

        public void Init(SceneIndexEnum defaultScene = SceneIndexEnum.ExploreScene)
        {
            EventManager.AddEventListener<LoadingEventData>(OnAddTask);
            EventManager.AddEventListener<LoadSceneEventData>(OnLoadScene);
            fullScreenView = Instantiate(fullScreenView, transform);
            overlayView = Instantiate(overlayView, transform);
            Hide();
            _tasks.Clear();
            LoadScene(defaultScene);
        }

        private void RemoveLoadedScenes()
        {
            int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.buildIndex != activeSceneIndex)
                {
                    SceneManager.UnloadSceneAsync(scene);
                }
            }
        }

        private void OnAddTask(LoadingEventData data)
        {
            Task.Run(async () => { await AddTask(data); });
        }

        private void OnLoadScene(LoadSceneEventData data)
        {
            if (data.replaceCurrentScene) UnloadScene(_currentScene);
            LoadScene(data.sceneIndexEnum);
        }

        private void Show(LoadingType loadingType = LoadingType.None)
        {
            IsLoadingShow = true;

            switch (loadingType)
            {
                case LoadingType.FullScreen:
                    fullScreenView.SetActive(true);
                    overlayView.SetActive(false);
                    break;
                case LoadingType.Overlay:
                    fullScreenView.SetActive(false);
                    overlayView.SetActive(true);
                    break;
                case LoadingType.None:
                default:
                    overlayView.SetActive(false);
                    fullScreenView.SetActive(false);
                    break;
            }
        }

        private void Hide()
        {
            IsLoadingShow = false;
            overlayView.SetActive(false);
            fullScreenView.SetActive(false);
        }

        private async Task AddTask(LoadingEventData task)
        {
            if (!IsLoadingShow)
            {
                await UniTask.SwitchToMainThread();
                Show(task.loadingType);
            }

            try
            {
                _tasks.Add(task);
                await task.task;
                _tasks.Remove(task);
                task.onComplete?.Invoke();
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                Debug.LogException(e);
#endif
                ToastSystem.Show("Failed to do task " + task.message);
                _tasks.Remove(task);
            }

            if (_tasks.Count == 0)
            {
                await UniTask.SwitchToMainThread();
                Hide();
            }
        }

        private void LoadScene(SceneIndexEnum sceneIndex)
        {
            _currentScene = sceneIndex;
            var task = SceneManager.LoadSceneAsync((int)sceneIndex, LoadSceneMode.Additive).ToUniTask().AsTask();
            Task.Run(async () =>
            {
                await AddTask(new LoadingEventData(task, loadingType: LoadingType.FullScreen,
                    message: $"Loading {sceneIndex}"));
            });
        }


        private void UnloadScene(SceneIndexEnum sceneIndex, LoadingType loadingType = LoadingType.FullScreen)
        {
            var task = SceneManager.UnloadSceneAsync((int)sceneIndex)?.ToUniTask().AsTask();
            if (task == null) return;
            Task.Run(async () =>
            {
                await AddTask(new LoadingEventData(task, loadingType, message: $"Unloading {sceneIndex}"));
            });
        }

        public enum LoadingType
        {
            FullScreen,
            Overlay,
            None
        }

        private void OnDisable()
        {
            EventManager.RemoveEventListener<LoadingEventData>(OnAddTask);
            EventManager.RemoveEventListener<LoadSceneEventData>(OnLoadScene);
        }
    }
}