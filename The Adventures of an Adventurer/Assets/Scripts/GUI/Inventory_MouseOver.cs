using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

public class Inventory_MouseOver : MonoBehaviour {

    public GameObject inventoryDatabase;

    private List<Inventory_Database.Item> itemList = new List<Inventory_Database.Item>();
    private Inventory_Database.ItemInfo[] itemInfo;
    private bool mouseOverObject;
    private bool informationLoaded;

    public GameObject infoBox;
    //public GameObject infoBoxBackground;

    void Start()
    {
        mouseOverObject = false;
        informationLoaded = false;
    }

    public void MouseEnter()
    {
        infoBox.SetActive(true);
        //infoBoxBackground.transform.localScale = new Vector2(infoBox.transform.localScale.x + 10, infoBox.transform.localScale.y + 10);
        //infoBox.GetComponentInChildren<Text>().text = "Hello World";
        if (informationLoaded)
            StartCoroutine("GetItemName");
        print("Entered");
        if (!mouseOverObject)
        {
            mouseOverObject = true;
        }
    }

    void Update()
    {
        if (inventoryDatabase.GetComponent<Inventory_Database>().GetItemInfoLoaded && !informationLoaded)
        {
            itemList = inventoryDatabase.GetComponent<Inventory_Database>().GetItemDatabase;
            itemInfo = inventoryDatabase.GetComponent<Inventory_Database>().GetItemInfo;
            informationLoaded = true;
            print(itemInfo[1].Item.GetName);
            //infoBox.GetComponentInChildren<Text>().text = "Test";
        }
        if (mouseOverObject)
        {
            infoBox.transform.position = new Vector3(Input.mousePosition.x+20, Input.mousePosition.y, 0);
        }
    }

    public IEnumerator GetItemName()
    {
        for (int i = 0; i < 20; i++ )
        {
            print(itemInfo[i].Item.GetName);
            if (itemInfo[i].Slot.Equals(this.gameObject))
                infoBox.GetComponentInChildren<Text>().text = itemInfo[i].Item.GetName;
            yield return null;
        }
    }

    public void MouseExit()
    {
        if (mouseOverObject)
        {
            print("Exit");
            mouseOverObject = false;
            infoBox.SetActive(false);
        }
    }
}
