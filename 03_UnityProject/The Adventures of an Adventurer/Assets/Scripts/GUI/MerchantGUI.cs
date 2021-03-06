﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MerchantGUI : MonoBehaviour {

    public GameObject infoBox;
    public int NUMBER_OF_SLOTS;
    public GameObject MerchantScrollAndSlots;
    public string slotTag;
    public int merchantGold;

    private int[] idOfItems;
    private GameObject InventoryUI;
    private Inventory_Main inventoryMainDriver;
    private GameObject MerchantUI;
    private Inventory_Database Main_Database;
    private Inventory_Main.ItemInfo[] itemInfo;
    //private List<Inventory_Database.Item> Item_Database;
    private List<Inventory_Database.Item> merchantInventory;
    private bool inventoryCleared;
    private bool gotItemInfo;

    public Inventory_Main.ItemInfo[] GetItemInfo { get { return itemInfo; } }
    public bool GetItemInfoLoaded { get { return gotItemInfo; } }

    // Use this for initialization
    void Start () {
        gotItemInfo = false;
        inventoryCleared = true;
        if (slotTag == "")
            slotTag = "MerchantSlot";
        InventoryUI = GameObject.FindGameObjectWithTag("InventoryUI");
        inventoryMainDriver = InventoryUI.GetComponentInChildren<Inventory_Main>();
        Main_Database = GameObject.FindGameObjectWithTag("InventoryUI").GetComponentInChildren<Inventory_Database>();
        MerchantUI = GameObject.FindGameObjectWithTag("MerchantUI");
        StartCoroutine("CheckIfItemListLoaded");
        //Item_Database = Main_Database.GetItemDatabase;
    }

    public void AddNewItemsToMerchantInventoryByIdArray(int[] items)
    {
        this.idOfItems = items;
        StartCoroutine("AddItemsToMerchantInventory");
    }

    public void ClearMerchantInventory()
    {
        StartCoroutine("ClearMerchantInventoryCoRoutine");
    }
    private IEnumerator CheckIfItemListLoaded()
    {
        do
        {
            yield return null;
        } while (!Main_Database.GetItemInfoLoaded);
        itemInfo = inventoryMainDriver.GetSlotArray(slotTag, NUMBER_OF_SLOTS);
        MerchantScrollAndSlots.SetActive(false);
        gotItemInfo = true;
    }

    private IEnumerator AddItemsToMerchantInventory()
    {
        do
        {
            yield return null;
        } while (!Main_Database.GetItemInfoLoaded && !inventoryCleared);
        merchantInventory = new List<Inventory_Database.Item>();
        foreach (int i in idOfItems)
        {
            merchantInventory.Add(Main_Database.GetItemWithID(i));
            yield return null;
        }
        for (int i = 0; i < itemInfo.Length; i++)
        {
            if (i < merchantInventory.Count)
                inventoryMainDriver.ChangeItem(itemInfo[i], merchantInventory[i]);
            else
                inventoryMainDriver.ChangeItem(itemInfo[i], new Inventory_Database.Item(0, "Empty", null, "Nothing", 0, "Null", 0, 0, "", ""));
            yield return null;
        }
    }

    private IEnumerator ClearMerchantInventoryCoRoutine()
    {
        inventoryCleared = false;
        do
        {
            yield return null;
        } while (!Main_Database.GetItemInfoLoaded);
        merchantInventory = new List<Inventory_Database.Item>();
        for (int i = 0; i < itemInfo.Length; i++)
        {
            inventoryMainDriver.ChangeItem(itemInfo[i], new Inventory_Database.Item(0, "Empty", null, "Nothing", 0, "Null", 0, 0, "", ""));
            yield return null;
        }
        inventoryCleared = true;
    }

    public void OpenMechantUI()
    {
        InventoryUI.transform.localScale = new Vector3(0.87f, 0.87f, 0.87f);
        InventoryUI.transform.position = new Vector3(155,100);
        inventoryMainDriver.EnableDisableInventory();
        MerchantScrollAndSlots.SetActive(true);
        inventoryMainDriver.InterfereOpeningOfInventory = true;
        
    }

    public void CloseMechantUI()
    {
        inventoryMainDriver.EnableDisableInventory();
        InventoryUI.transform.localScale = new Vector3(1, 1, 1);
        InventoryUI.transform.position = new Vector3(0, 0);
        MerchantScrollAndSlots.SetActive(false);
        inventoryMainDriver.InterfereOpeningOfInventory = false;
        
    }
}
