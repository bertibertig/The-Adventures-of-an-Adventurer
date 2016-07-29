using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GetItem : MonoBehaviour {

    public Text multiplaier;
    public Image item;

    private List<Inventory_Database.Item> itemList;

    void Start()
    {
        multiplaier.enabled = false;
        item.enabled = false;
    }

    public void ShowGetItem(int itemID, int number)
    {
        itemList = GameObject.FindGameObjectWithTag("InventoryUI").GetComponent<Inventory_Database>().GetItemDatabase;
        item.sprite = itemList[itemID].GetSprite;
        multiplaier.text = "x" + number.ToString();
        StartCoroutine("ShowItem");
    }

    IEnumerator ShowItem()
    {
        multiplaier.enabled = true;
        item.enabled = true;
        yield return new WaitForSeconds(5);
        multiplaier.enabled = false;
        item.enabled = false;
    }
}
