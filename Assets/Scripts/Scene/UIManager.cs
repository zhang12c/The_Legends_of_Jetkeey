using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Scene
{
    public class UIManager : MonoBehaviour
    {
        [Header("游戏开始")]
        public Button gameStart;
        public Transform gameStartPanel;
        public Camera uiCamera;
        public string mainSceneName;
        public EventSystem EventSystem;
        private Coroutine _loadSceneEnumerator;
        

        private void Start()
        {
            gameStart.onClick.AddListener(OnGameStart);
        }
        private void OnGameStart()
        {
            gameStartPanel.gameObject.SetActive(false);
            uiCamera.gameObject.SetActive(false);
            EventSystem.gameObject.SetActive(false);
            if (_loadSceneEnumerator!= null)
            {
                StopCoroutine(_loadSceneEnumerator);
            }
            
            _loadSceneEnumerator = StartCoroutine(LoadSaveDataScene(mainSceneName));
        }
        
        
        private IEnumerator LoadSaveDataScene(string sceneName)
        {
            if (SceneManager.GetActiveScene().name != "UIScene")
            {
                yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            }

            yield return LoadSceneSetActive(sceneName);
        }
        
        private IEnumerator LoadSceneSetActive(string sceneName)
        {
            /// load sceneName 的场景进入当前的场景
            yield return SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
            // 只可以用这个方式获得场景中有多少个场景
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                UnityEngine.SceneManagement.Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == sceneName)
                {
                    SceneManager.SetActiveScene(scene);
                    break;
                }
            }
        
        }
        private IEnumerator UnloadScene()
        {
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}