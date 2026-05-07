using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLives = 5;
    [SerializeField] private GameObject[] fullHearts;
    [SerializeField] private GameObject[] emptyHearts;
 
    private int currentLives;

    void Start()
    {
        currentLives = maxLives;
        UpdateHeartsDisplay();
    }

    void Update()
    {

    }
    public void LoseLife()
    {
        PlayerRespawn player = GetComponent<PlayerRespawn>();
        currentLives--;
        if (currentLives <= 0)
        {
            player.Respawn();
        }
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
}
