using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    [SerializeField] private PuzzleItem sword;
    [SerializeField] private PuzzleItem arrow;
    [SerializeField] private PuzzleItem hayFork;
    [SerializeField] private Animator anim;
    [SerializeField] private bool testOpen = false;
    
    private bool isOpened = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isOpened)
        {
            if((sword.IsCorrect() && arrow.IsCorrect() && hayFork.IsCorrect()) || testOpen)
            {
                OpenDoor();
            }
        }
    }
    private void OpenDoor()
    {
        isOpened = true;
        anim.SetTrigger("Activate");
    } 
}
