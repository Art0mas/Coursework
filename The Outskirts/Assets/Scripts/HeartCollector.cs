using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeartCollector : MonoBehaviour
{
    [SerializeField] private float floatHeight = 0.2f;
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private AudioSource heartAudio;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (collision.CompareTag("Player"))
        {
            player.GetLives();
            heartAudio.Play();
            gameObject.SetActive(false);
        }
    }
}
