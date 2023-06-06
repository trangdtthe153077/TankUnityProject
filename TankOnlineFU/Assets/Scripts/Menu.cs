using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Btn_Play()
    {
        Application.LoadLevel("Play");
    }
    public void Btn_Construction()
    {
        Application.LoadLevel("Construction");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
