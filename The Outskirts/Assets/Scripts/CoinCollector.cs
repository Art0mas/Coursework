using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioSource coinAudio;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInventory player = collision.GetComponent<PlayerInventory>();
        if (collision.CompareTag("Player"))
        {
            player.AddCoin();
            coinAudio.Play();
            gameObject.SetActive(false);
        }
    }
}
