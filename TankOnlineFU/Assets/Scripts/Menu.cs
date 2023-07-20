using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public Canvas canvas;

    public void Start()
    {
        canvas.gameObject.SetActive(false);
    }
    public void Btn_Play()
    {
        Application.LoadLevel("MapSelected");
    }
    public void Btn_Construction()
    {
        Application.LoadLevel("Construction");
    }

    public void Btn_Store()
    {
        canvas.gameObject.SetActive(true);
    }

    public void Btn_Store_Close()
    {
        canvas.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
