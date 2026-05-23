using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    [SerializeField] private GameObject activateText;
    [SerializeField] private int correctState = 1;

    private bool isPlayerNear = false;
    private int currentState = 0;
    void Start()
    {
        activateText.SetActive(false);
    }

    void Update()
    {
        if (PuzzleActivate.isActivatePuzzle)
        {
            if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
            {
                RotateItem();
            }
        }
    }
    private void RotateItem()
    {
        transform.Rotate(0, 0, -90);
        currentState++;

        if(currentState >= 4)
        {
            currentState = 0;
        }

    }
    public bool IsCorrect()
    {
        return currentState == correctState;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PuzzleActivate.isActivatePuzzle)
        {
            if (collision.CompareTag("Player"))
            {
                activateText.SetActive(true);
                isPlayerNear = true;
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activateText.SetActive(false);
            isPlayerNear = false;
        }
    }
}

