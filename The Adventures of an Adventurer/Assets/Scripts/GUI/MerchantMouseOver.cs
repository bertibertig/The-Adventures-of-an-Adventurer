using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MerchantMouseOver : MonoBehaviour {

    public GameObject infoBox;
    public GameObject merchantMainDriver;
    public GameObject inventoryMainDriver;

    private bool mouseOverObject;
    private bool informationLoaded;
    private List<Inventory_Database.Item> itemList = new List<Inventory_Database.Item>();
    private Inventory_Main.ItemInfo[] itemInfos;
    private Inventory_Main.ItemInfo itemInfo;
    private string mousebutton;

    // Use this for initialization
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
        {
            StartCoroutine("GetItemName");
        }
        //print("Entered");
        if (!mouseOverObject)
        {
            mouseOverObject = true;
        }
    }

    public IEnumerator GetItemName()
    {
        for (int i = 0; i < itemInfos.Length; i++)
        {
            //print(itemInfo[i].Item.GetName);
            if (itemInfos[i].Slot.Equals(this.gameObject))
            {
                infoBox.GetComponentInChildren<Text>().text = itemInfos[i].Item.GetName;
                itemInfo = itemInfos[i];
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

    // Update is called once per frame
    void Update ()
    {
        if (inventoryMainDriver.GetComponentInChildren<Inventory_Database>().GetItemInfoLoaded && !informationLoaded)
        {
            itemList = inventoryMainDriver.GetComponentInChildren<Inventory_Database>().GetItemDatabase;
            itemInfos = merchantMainDriver.GetComponent<MerchantGUI>().GetItemInfo;
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
}
