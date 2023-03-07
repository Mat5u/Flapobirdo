using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Play()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Time.timeScale = 1.0f;
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void Settings()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Settings");
    }

    public void Back()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
    }
}
