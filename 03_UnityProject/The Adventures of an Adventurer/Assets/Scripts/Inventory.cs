using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public float numberOfGoldCoins;

    public Text inventory_GoldCoins;

    public GameObject gold_counter;
    public GameObject gold_icon;

    // Use this for initialization
    void Start()
    {
        numberOfGoldCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        inventory_GoldCoins.text = (numberOfGoldCoins.ToString());
    }

    public void ShowInventory(bool showInventory)
    {
        if (!showInventory)
        {
            gold_counter.SetActive(true);
            gold_icon.SetActive(true);
        }

        if (showInventory)
        {
            gold_counter.SetActive(false);
            gold_icon.SetActive(false);
        }
    }
}
