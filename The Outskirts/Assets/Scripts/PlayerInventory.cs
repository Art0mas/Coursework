using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI coinsCount;

    private int coins;

    void Start()
    {
        coins = 0;
        coinsCount.text = coins.ToString();
    }

    public void AddCoin()
    {
        coins++;
        coinsCount.text = coins.ToString();
    }

    public int GetCoins()
    {
        return coins;
    }
    public void LoadCoins(int loadedCoins)
    {
        coins = loadedCoins;
    }
}
