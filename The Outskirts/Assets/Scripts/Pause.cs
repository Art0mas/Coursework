using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject saveText;
    [SerializeField] private GameObject settingsPanel;

    private bool isPause = false;
    private void Start()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        saveText.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause) ResumeGame();
            else PauseGame();
        }
    }
    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }

    public async void SaveGame()
    {
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();

        if (playerHealth == null || playerInventory == null)
        {
            return; 
        }

        if (SaveGameManager.Instance != null)
        {
            await SaveGameManager.Instance.SaveProgressAsync(playerHealth, playerInventory);
            saveText.SetActive(true);
            await Task.Delay(2000);
            saveText.SetActive(false);
        }

    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void OpenSettings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
