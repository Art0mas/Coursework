using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerRespawn player = collision.GetComponent<PlayerRespawn>();
        if(collision.CompareTag("Player"))
        {
            player.Respawn();
        }
    }
}
