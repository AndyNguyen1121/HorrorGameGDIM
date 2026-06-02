using System;
using TMPro;
using UnityEngine;

namespace Player.UI
{
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject pressSpaceText;

        private void Start()
        {
            pressSpaceText.SetActive(false);
            PlayerManager.Instance.OnStruggleStart += EnablePressSpaceText;
            PlayerManager.Instance.OnStruggleEnd += DisablePressSpaceText;
        }
        
        private void EnablePressSpaceText()
        {
            pressSpaceText.SetActive(true);
        }

        private void DisablePressSpaceText()
        {
            pressSpaceText.SetActive(false);
        }
    }
}