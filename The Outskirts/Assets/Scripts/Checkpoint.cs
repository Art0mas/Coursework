using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject respawnPointActive;
    [SerializeField] private GameObject respawnPointPassive;
    [SerializeField] private Transform newPoint;

    private bool isActivated = false;
    void Start()
    {
        respawnPointActive.SetActive(false);
        respawnPointPassive.SetActive(true);
    }
    async void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerRespawn player = collision.GetComponent<PlayerRespawn>();
        if (collision.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            player.SetCheckpoint(newPoint);
            respawnPointActive.SetActive(true);
            respawnPointPassive.SetActive(false);

            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();
            SaveGameManager saveGameManager = FindObjectOfType<SaveGameManager>();
            SaveGameManager.Instance.LastCheckpointPosition = transform.position;

            if (SaveGameManager.Instance != null)
            {
                await SaveGameManager.Instance.SaveProgressAsync(playerHealth, playerInventory);
            }
        }
    }
    public Transform GetLastCheckpoint()
    {
        return newPoint;
    }
}
