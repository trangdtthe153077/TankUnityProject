using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    public GameObject pauseMenuUI;
    public GameObject audioMenu;
    private void Start()
    {
        // Đăng ký phương thức UpdateVolume khi giá trị của Slider thay đổi
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    public void UpdateVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void BackToPauseMenu()
    {
        audioMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
