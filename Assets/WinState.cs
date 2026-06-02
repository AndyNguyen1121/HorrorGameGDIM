using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinState : MonoBehaviour
{
    public GameObject winScreen;
    private bool won;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !won)
        {
            SceneManager.LoadScene("WinScene");
            won = true;
        }
    }
}
