using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public void StoreEscaped()
    {
        Debug.Log("Escaped the Grocery Store!");
        SceneManager.LoadScene("WinScene");
    }

    public void Grabbed()
    {
        Debug.Log("You have been taken...");
        SceneManager.LoadScene("LoseScene");
    }
}
