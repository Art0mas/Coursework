using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerRespawn playerRespawn = collision.GetComponent<PlayerRespawn>();
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if(collision.CompareTag("Player"))
        {
            playerHealth.LoseLives();
            playerRespawn.Respawn();
        }
    }
}
