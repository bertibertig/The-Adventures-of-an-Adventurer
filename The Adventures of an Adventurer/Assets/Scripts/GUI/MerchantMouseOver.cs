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
    private bool waitingForActivation;

    // Use this for initialization
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
                
                itemInfo = itemInfos[i];
                infoBox.GetComponentInChildren<Text>().text = itemInfo.Item.GetName;
                infoBox.GetComponentsInChildren<Text>()[1].text = itemInfo.Item.GetPrice.ToString() + " Gold";
                if (GameObject.FindGameObjectWithTag("EventList").GetComponentInChildren<LanguageReader>().Language == "german")
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

    private IEnumerator WaitForActivation()
    {
        do
        {
            yield return new WaitForSeconds(1);
        } while (false);
        infoBox.SetActive(true);
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
        if (merchantMainDriver.GetComponentInChildren<MerchantGUI>().GetItemInfoLoaded && !informationLoaded)
        {
            itemList = inventoryMainDriver.GetComponentInChildren<Inventory_Database>().GetItemDatabase;
            itemInfos = merchantMainDriver.GetComponent<MerchantGUI>().GetItemInfo;
            informationLoaded = true;
            print(itemInfos.Length);
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
