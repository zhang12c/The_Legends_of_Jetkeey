using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Scene
{
    public class GameEnd : MonoBehaviour
    {
        [Header("游戏结束")]
        public Transform gameEndPanel;
        public Button quitGameBtn;
        private void Start()
        {
            quitGameBtn.onClick.AddListener(DoGameEnd);
        }
        
        public void ShowGameEndPanel()
        {
            gameEndPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        
        private void DoGameEnd()
        {
            Debug.Log("游戏结束");
            Application.Quit();
        }
    }
}