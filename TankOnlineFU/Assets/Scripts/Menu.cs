using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Btn_Play()
    {
        Application.LoadLevel("MapSelected");
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
