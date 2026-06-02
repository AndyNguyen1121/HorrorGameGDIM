using System;
using Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player.UI
{
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject pressSpaceText;
        [SerializeField] private GameObject gameOverMenu;

        private void Start()
        {
            pressSpaceText.SetActive(false);
            PlayerManager.Instance.OnStruggleStart += EnablePressSpaceText;
            PlayerManager.Instance.OnStruggleEnd += DisablePressSpaceText;
            ParentManager.Instance.OnEndGame += EnableGameOverMenu;
        }

        private void EnableGameOverMenu()
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameOverMenu.SetActive(true);
        }
        
        private void EnablePressSpaceText()
        {
            pressSpaceText.SetActive(true);
        }

        private void DisablePressSpaceText()
        {
            pressSpaceText.SetActive(false);
        }

        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}