using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzleActivate : MonoBehaviour
{
    [SerializeField] private GameObject activateText;
    [SerializeField] private GameObject puzzleText;

 
    private bool isActivePuzzleText = false;
    private bool isPlayerNear = false;
    public static bool isActivatePuzzle = false;
    void Start()
    {
        activateText.SetActive(false);
        puzzleText.SetActive(false);
    }

    void Update()
    {
        if(isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            isActivePuzzleText = !isActivePuzzleText;
            puzzleText.SetActive(isActivePuzzleText);
            isActivatePuzzle = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activateText.SetActive(true);
            isPlayerNear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = false;
            activateText.SetActive(false);
            puzzleText.SetActive(false);
            isActivePuzzleText = false;
        }
    }
}
