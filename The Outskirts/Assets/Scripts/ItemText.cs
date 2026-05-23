using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemText : MonoBehaviour
{
    [SerializeField] private GameObject activateText;
    [SerializeField] private GameObject itemText;

    private bool isPlayerNear = false;
    private bool isActivateItemText = false;
    void Start()
    {
        activateText.SetActive(false);
        itemText.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            isActivateItemText = !isActivateItemText;
            itemText.SetActive(isActivateItemText);
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
            activateText.SetActive(false);
            isPlayerNear = false;
            itemText.SetActive(false);
        }
    }
}
