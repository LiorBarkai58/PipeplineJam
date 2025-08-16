using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ScenesHelpers.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace ScenesHelpers
{
    public class SceneTransitionManager : MonoBehaviour
    {
        [Header("Prefab references")] [SerializeField]
        private CanvasGroup loadingUI;
        
        [Header("Project References")]
        [SerializeField] private SceneRequestChannel sceneRequestChannel;
        
        
        private static SceneTransitionManager _instance;

        private void Awake()
        {
            if (_instance && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
            sceneRequestChannel.OnSceneRequested += HandleSceneChangeRequest;
        }

        private void HandleSceneChangeRequest(SceneField scene)
        {
            _ = LoadSceneAsync(scene);
            
        }
        private async UniTaskVoid LoadSceneAsync(SceneField scene)
        {
            if(scene == null) return;
            loadingUI.gameObject.SetActive(true);
            loadingUI.alpha = 0;
            loadingUI.DOFade(1, 0.5f).SetEase(Ease.OutQuad);

            AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
            operation.allowSceneActivation = false;

            while (operation.progress < 0.9f)
            {
                await UniTask.Yield();
            }

            await UniTask.Delay(1000);
            
            operation.allowSceneActivation = true;

            while (!operation.isDone)
            {
                await UniTask.Yield();
            }

            loadingUI.DOFade(0, 0.5f).SetEase(Ease.OutQuad).OnComplete(() => loadingUI.gameObject.SetActive(false));
        }
    }
}