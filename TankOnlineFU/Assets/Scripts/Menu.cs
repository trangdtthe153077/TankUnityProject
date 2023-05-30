using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Btn_Play()
    {
        Application.LoadLevel("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
