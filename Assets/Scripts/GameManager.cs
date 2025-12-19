using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    

    public void GameOver()
    {
        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(1.5f);
        UiController.Instance.gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Pause()
    {
        if(UiController.Instance.pausePanel.activeSelf == false
            && UiController.Instance.gameOverPanel.activeSelf == false)
        {
            UiController.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
        } else
        {
            UiController.Instance.pausePanel.SetActive(false);  
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

        public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
