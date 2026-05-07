using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }
    public void Respawn()
    {
        transform.position = respawnPoint.position;
        rb.velocity = Vector2.zero;
    }
    public void SetCheckpoint(Transform newPoint)
    {
        respawnPoint = newPoint;
    }
}
