using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("2D Greybox");
    }

    public void OptionsMenu()
    {
        // decide later whether just set a settings panel active or load a new scene
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
