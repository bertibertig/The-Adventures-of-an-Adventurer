using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Inventory_MouseOver : MonoBehaviour {

    public GameObject inventoryDatabase;

    private List<Inventory_Database.Item> itemList = new List<Inventory_Database.Item>();
    private Inventory_Database.ItemInfo[] itemInfo;
    private bool mouseOverObject;

    public GameObject infoBox;

    void Start()
    {
        mouseOverObject = false;
        itemList = inventoryDatabase.GetComponent<Inventory_Database>().GetItemDatabase;
        itemInfo = inventoryDatabase.GetComponent<Inventory_Database>().GetItemInfo;
    }

    public void MouseEnter()
    {
        infoBox.SetActive(true);
        infoBox.GetComponentInChildren<Text>().text = "Hello World";
        mouseOverObject = true;
        StartCoroutine("MouseOver");
    }

    public IEnumerator MouseOver()
    {
        while (mouseOverObject)
        {
            infoBox.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            yield return null;
        }
    }

    public void MouseExit()
    {
        mouseOverObject = false;
        StopCoroutine("MouseOver");
        infoBox.SetActive(false);
    }
}
