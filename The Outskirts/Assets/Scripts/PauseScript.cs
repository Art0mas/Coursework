using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsMenuPanel;
    [SerializeField] private GameObject saveStatusText;

    private static bool isGamePaused = false;
    public static bool IsGamePaused => isGamePaused;

    private void Start()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (settingsMenuPanel != null) settingsMenuPanel.SetActive(false);
        if (saveStatusText != null) saveStatusText.SetActive(false);

        Time.timeScale = 1f;
        isGamePaused = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenuPanel != null && settingsMenuPanel.activeSelf)
            {
                CloseSettings();
            }
            else
            {
                if (isGamePaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (settingsMenuPanel != null) settingsMenuPanel.SetActive(false);

        Time.timeScale = 1f; 
        isGamePaused = false;
    }

    public void Pause()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);

        Time.timeScale = 0f; 
        isGamePaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public async void Save()
    {

        if (saveStatusText != null)
        {
            saveStatusText.SetActive(true);
            await Task.Delay(2000); 
            if (saveStatusText != null) saveStatusText.SetActive(false);
        }
    }

    public void OpenSettings()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (settingsMenuPanel != null) settingsMenuPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsMenuPanel != null) settingsMenuPanel.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        isGamePaused = false;

        SceneManager.LoadScene(0);
    }
}
