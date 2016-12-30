using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using System.Reflection;

public class Inventory_MouseOver : MonoBehaviour{

    public GameObject inventoryMainDriver;
    public GameObject yellow_Selecter;
    public GameObject dragItem;

    private List<Inventory_Database.Item> itemList = new List<Inventory_Database.Item>();
    private Inventory_Main.ItemInfo[] itemInfos;
    private Inventory_Main.ItemInfo itemInfo;
    private bool mouseOverObject;
    private bool informationLoaded;
    private bool gotItemName;
    private bool waitingForActivation;
    private string mousebutton;
    //private Vector3 startPosition;
    private Inventory_Main.ItemInfo endPosition;
    private Inventory_Main.ItemInfo otherItem;

    public GameObject infoBox;
    //public GameObject infoBoxBackground;

    void Start()
    {
        waitingForActivation = false;
        mouseOverObject = false;
        informationLoaded = false;
        //print("loadedMouseover");
    }

    public void MouseEnter()
    {
        if (!waitingForActivation)
        {
            waitingForActivation = true;
            StartCoroutine("WaitForActivation");
        }
        //infoBoxBackground.transform.localScale = new Vector2(infoBox.transform.localScale.x + 10, infoBox.transform.localScale.y + 10);
        //infoBox.GetComponentInChildren<Text>().text = "Hello World";
        if (informationLoaded)
        {
            StartCoroutine("GetItemName");
        }
        //print("Entered");
        if (!mouseOverObject)
        {
            mouseOverObject = true;
        }
    }

    void Update()
    {
        if (inventoryMainDriver.GetComponentInChildren<Inventory_Database>().GetItemInfoLoaded && !informationLoaded)
        {
            itemList = inventoryMainDriver.GetComponentInChildren<Inventory_Database>().GetItemDatabase;
            itemInfos = inventoryMainDriver.GetComponent<Inventory_Main>().GetItemInfo;
            informationLoaded = true;
            //infoBox.GetComponentInChildren<Text>().text = "Test";
        }
        if (mouseOverObject)
        {
            infoBox.transform.position = new Vector3(Input.mousePosition.x + 20, Input.mousePosition.y, 0);
        }
        if (Input.GetMouseButtonDown(0)) mousebutton = "Pressed left click.";
        if (Input.GetMouseButtonDown(1)) mousebutton = "Pressed right click.";
        if (Input.GetMouseButtonDown(2)) mousebutton = "Pressed middle click.";
    }

    private IEnumerator WaitForActivation()
    {
        do
        {
            yield return new WaitForSeconds(1);
        }while(false);
        infoBox.SetActive(true);
    }
    public IEnumerator GetItemName()
    {
        for (int i = 0; i < itemInfos.Length; i++ )
        {
            //print(itemInfo[i].Item.GetName);
            if (itemInfos[i].Slot.Equals(this.gameObject))
            {
                itemInfo = itemInfos[i];
                infoBox.GetComponentsInChildren<Text>()[0].text = itemInfo.Item.GetName;
                infoBox.GetComponentsInChildren<Text>()[1].text = itemInfo.Item.GetPrice.ToString() + " Gold";
                if(GameObject.FindGameObjectWithTag("EventList").GetComponentInChildren<LanguageReader>().Language == "german")
                    infoBox.GetComponentsInChildren<Text>()[2].text = itemInfo.Item.GetGerDescription;
                else
                    infoBox.GetComponentsInChildren<Text>()[2].text = itemInfo.Item.GetEngDescription;
                if (itemInfo.Item.GetID == 0)
                {
                    infoBox.GetComponentsInChildren<Image>()[1].color = itemInfo.Slot.GetComponent<Image>().color;
                    infoBox.GetComponentsInChildren<Image>()[1].sprite = itemList[0].GetSprite;
                }
                else
                {
                    infoBox.GetComponentsInChildren<Image>()[1].color = UnityEngine.Color.white;
                    infoBox.GetComponentsInChildren<Image>()[1].sprite = itemInfo.Item.GetSprite;
                }
            }
            yield return null;
        }
    }

    public void MouseExit()
    {
        StopCoroutine("WaitForActivation");
        waitingForActivation = false;
        if (mouseOverObject)
        {
            mouseOverObject = false;
            infoBox.SetActive(false);
        }
    }

    public void MouseClick()
    {
        if (mousebutton == "Pressed right click.")
        {
            print(itemInfo.Item.GetClassString + "." + itemInfo.Item.GetFunction);
            if (itemInfo.Item.GetClassString != "" && itemInfo.Item.GetFunction != "")
            {
                Type type = Type.GetType(itemInfo.Item.GetClassString);
                MethodInfo m = type.GetMethod(itemInfo.Item.GetFunction);
                m.Invoke(m, null);
            }
        }

        if (mousebutton == "Pressed left click." && !yellow_Selecter.GetComponent<Inventory_Selector>().Selected)
        {
            yellow_Selecter.GetComponent<Inventory_Selector>().Selected = true;
            yellow_Selecter.GetComponent<Inventory_Selector>().ItemInfo = itemInfo;
            yellow_Selecter.transform.position = itemInfo.Slot.transform.position;
            yellow_Selecter.GetComponent<Image>().enabled = true;
        }
        else if(mousebutton == "Pressed left click." && yellow_Selecter.GetComponent<Inventory_Selector>().Selected)
        {
            yellow_Selecter.GetComponent<Inventory_Selector>().Selected = false;
            yellow_Selecter.GetComponent<Image>().enabled = false;
            Vector3 oldPosition = this.gameObject.transform.position;
            this.gameObject.transform.position = yellow_Selecter.GetComponent<Inventory_Selector>().ItemInfo.Slot.transform.position;
            yellow_Selecter.GetComponent<Inventory_Selector>().ItemInfo.Slot.transform.position = oldPosition;
        }
        print(yellow_Selecter.GetComponent<Inventory_Selector>().Selected);
    }

    /*public void BeginDrag()
    {
        if (mousebutton == "Pressed left click.")
        {
            isBeeingDraged = true;
            infoBox.SetActive(false);
            dragItem = gameObject;
            startPosition = transform.position;
        }
    }

    public void Drag()
    {
        if (mousebutton == "Pressed left click.")
        {
            transform.position = Input.mousePosition;
            print("Mouse position:" + Input.mousePosition);
            print("Slot1 position:" + itemInfo[0].Slot.transform.position);
        }
    }

    public void EndDrag()
    {
        if (mousebutton == "Pressed left click.")
        {
            endPosition = null;
            StartCoroutine("GetSlot");
            print(endPosition.Slot.ToString());
            isBeeingDraged = false;
            dragItem = null;
            transform.position = endPosition.Slot.transform.position;
        }
    }

    private IEnumerator GetSlot()
    {
        foreach (Inventory_Database.ItemInfo item in itemInfo)
        {
            Vector3 leftUpperCorner = new Vector3(item.Slot.transform.position.x / 2, item.Slot.transform.position.y / 2);
            //print(leftUpperCorner);
            if (item.Slot.transform.position == Input.mousePosition)
            {
                endPosition = item;
            }
            yield return null;
        }
    }*/
}
