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
    private Inventory_Main.ItemInfo endPosition;
    private Inventory_Main.ItemInfo otherItem;

    public GameObject infoBox;

    void Start()
    {
        waitingForActivation = false;
        mouseOverObject = false;
        informationLoaded = false;
    }

    public void MouseEnter()
    {
        if (!waitingForActivation)
        {
            waitingForActivation = true;
            StartCoroutine("WaitForActivation");
        }
        if (informationLoaded)
        {
            StartCoroutine("GetItemName");
        }
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
            if (itemInfo.Item.GetClassString != "" && itemInfo.Item.GetFunction != "")
            {
                Type type = Type.GetType(itemInfo.Item.GetClassString);
                MethodInfo m = type.GetMethod(itemInfo.Item.GetFunction);
                if (itemInfo.Item.GetOptionalParams != null)
                    m.Invoke(m, itemInfo.Item.GetOptionalParams);
                else
                    m.Invoke(m, new object[] { "" });
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
    }
}
