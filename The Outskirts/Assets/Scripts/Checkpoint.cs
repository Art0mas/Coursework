using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject respawnPointActive;
    [SerializeField] private GameObject respawnPointPassive;
    [SerializeField] private Transform newPoint;
    void Start()
    {
        respawnPointActive.SetActive(false);
        respawnPointPassive.SetActive(true);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerRespawn player = collision.GetComponent<PlayerRespawn>();
        if (collision.CompareTag("Player"))
        {
            player.SetCheckpoint(newPoint);
            respawnPointActive.SetActive(true);
            respawnPointPassive.SetActive(false);
        }
    }
}
