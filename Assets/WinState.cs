using System;
using UnityEngine;

public class WinState : MonoBehaviour
{
    public GameObject winScreen;
    private bool won;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !won)
        {
            winScreen.SetActive(true);
            won = true;
        }
    }
}
