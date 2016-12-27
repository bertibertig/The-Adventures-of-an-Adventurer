using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using System.Reflection;

public class Inventory_MouseOver : MonoBehaviour{

    public GameObject inventoryDatabase;

    private List<Inventory_Database.Item> itemList = new List<Inventory_Database.Item>();
    private Inventory_Database.ItemInfo[] itemInfo;
    private bool mouseOverObject;
    private bool informationLoaded;
    private string mousebutton;
    private Inventory_Database.Item item;

    public GameObject infoBox;
    //public GameObject infoBoxBackground;

    void Start()
    {
        mouseOverObject = false;
        informationLoaded = false;
        //print("loadedMouseover");
    }

    public void MouseEnter()
    {
        infoBox.SetActive(true);
        //infoBoxBackground.transform.localScale = new Vector2(infoBox.transform.localScale.x + 10, infoBox.transform.localScale.y + 10);
        //infoBox.GetComponentInChildren<Text>().text = "Hello World";
        if (informationLoaded)
            StartCoroutine("GetItemName");
        //print("Entered");
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
            //infoBox.GetComponentInChildren<Text>().text = "Test";
        }
        if (mouseOverObject)
        {
            infoBox.transform.position = new Vector3(Input.mousePosition.x+20, Input.mousePosition.y, 0);
        }
        if (Input.GetMouseButtonDown(0)) mousebutton = "Pressed left click.";
        if (Input.GetMouseButtonDown(1)) mousebutton = "Pressed right click.";
        if (Input.GetMouseButtonDown(2)) mousebutton = "Pressed middle click.";
    }

    public IEnumerator GetItemName()
    {
        for (int i = 0; i < 20; i++ )
        {
            //print(itemInfo[i].Item.GetName);
            if (itemInfo[i].Slot.Equals(this.gameObject))
            {
                infoBox.GetComponentInChildren<Text>().text = itemInfo[i].Item.GetName;
                item = itemInfo[i].Item;
            }
            yield return null;
        }
    }

    public void MouseExit()
    {
        if (mouseOverObject)
        {
            mouseOverObject = false;
            infoBox.SetActive(false);
        }
    }

    public void MouseClick()
    {
        print(item.GetClassString + "." + item.GetFunction);
        if (item.GetClassString != "" && item.GetFunction != "")
        {
            Type type = Type.GetType(item.GetClassString);
            MethodInfo m = type.GetMethod(item.GetFunction);
            m.Invoke(m, null);
        }
    }
}
