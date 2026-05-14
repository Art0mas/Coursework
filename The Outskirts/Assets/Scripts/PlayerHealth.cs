using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLives = 5;
    [SerializeField] private GameObject[] fullHearts;
    [SerializeField] private GameObject[] emptyHearts;

    [SerializeField] private GameObject gameOverPanel;
 
    private int currentLives;
    private PlayerRespawn playerRespawn;

    void Start()
    {
        currentLives = maxLives;
        playerRespawn = GetComponent<PlayerRespawn>();
        UpdateHeartsDisplay();
    }

    public void LoseLives()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            Die();
        }
        else
        {
            if (playerRespawn != null)
            {
                playerRespawn.Respawn();
            }
        }
        UpdateHeartsDisplay();
    }
    public void GetLives()
    {
        currentLives++;

        if(currentLives >= maxLives)
        {
            currentLives = maxLives;
        }
        UpdateHeartsDisplay();
    }
    
    public void UpdateHeartsDisplay()
    {
        for(int i = 0; i < maxLives; i++)
        {
            if (i < currentLives)
            {
                fullHearts[i].SetActive(true);
                emptyHearts[i].SetActive(false);
            }
            else
            {
                fullHearts[i].SetActive(false);
                emptyHearts[i].SetActive(true);
            }
        }
    }
    private void Die()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            LoseLives();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            LoseLives();
        }
    }
    
}
