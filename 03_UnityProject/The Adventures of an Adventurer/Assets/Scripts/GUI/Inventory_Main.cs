using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Inventory_Main : MonoBehaviour {

    public GameObject infoBox;
    public int NUMBER_OF_SLOTS;
    public GameObject inventoryUI;
    public string slotTag;
    public bool FillInventoryWithTempItems = false;

    private UnityEngine.Color emptyBrown;
    private bool slotsLoaded;
    private ItemInfo[] itemInfo;
    private Inventory_Database inventoryDatabase;
    private List<Inventory_Database.Item> itemList = new List<Inventory_Database.Item>();
    private bool inventoryOpen;
    private bool slotArrayLoaded;

    public bool InterfereOpeningOfInventory { get; set; }
    public ItemInfo[] GetItemInfo { get { return this.itemInfo; } }
    public bool InventoryDisabled { get; set; }

    public class ItemInfo
    {
        private Inventory_Database.Item item;
        private GameObject slot;

        public Inventory_Database.Item Item { get { return this.item; } set { this.item = value; } }
        public GameObject Slot { get { return this.slot; } set { this.slot = value; } }

        public ItemInfo(Inventory_Database.Item item, GameObject slot)
        {
            this.item = item;
            this.slot = slot;
        }
    }

    // Use this for initialization
    void Start ()
    {
        InterfereOpeningOfInventory = false;
        if (NUMBER_OF_SLOTS <= 0)
            NUMBER_OF_SLOTS = 50;
        if (slotTag == "")
            slotTag = "Slot";
        slotArrayLoaded = false;
        inventoryDatabase = GameObject.FindGameObjectWithTag("InventoryUI").GetComponentInChildren<Inventory_Database>();
        itemList = inventoryDatabase.GetItemDatabase;;
        slotsLoaded = false;
        itemInfo = new ItemInfo[NUMBER_OF_SLOTS];
        //DontDestroyOnLoad(inventoryUI);
        //DontDestroyOnLoad(this);
        inventoryOpen = false;
        InventoryDisabled = false;
        //inventarUI.SetActive(false);   
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    { 
        if(inventoryDatabase.GetItemInfoLoaded && !slotArrayLoaded)
        {
            itemList = inventoryDatabase.GetItemDatabase;
            itemInfo = GetSlotArray(slotTag,NUMBER_OF_SLOTS);
            emptyBrown = itemInfo[0].Slot.GetComponent<Image>().color;
            if(FillInventoryWithTempItems)
                FillInventoryTemporarely();
            inventoryUI.SetActive(false);
            slotArrayLoaded = true;
        }
        if(Input.GetButtonDown("Tab") && !InterfereOpeningOfInventory && !InventoryDisabled)
        {
            if(infoBox.activeSelf)
            {
                infoBox.SetActive(false);
            }
            inventoryOpen = !inventoryOpen;
            inventoryUI.SetActive(inventoryOpen);
        }
	}

    public void EnableDisableInventory()
    {
        if (infoBox.activeSelf)
        {
            infoBox.SetActive(false);
        }
        inventoryOpen = !inventoryOpen;
        inventoryUI.SetActive(inventoryOpen);
    }

    private void FillInventoryTemporarely()
    {
        //ChangeItem(itemInfo[SlotID], itemList[ItemID]); -> How To use ChangeItem
        ChangeItem(itemInfo[0], itemList[1]);
        ChangeItem(itemInfo[1], itemList[2]);
        ChangeItem(itemInfo[2], itemList[3]);
        ChangeItem(itemInfo[3], itemList[5]);
        //AddItem(itemList.Where(i => i.GetID == 4).FirstOrDefault().GetID);
        //print(itemInfo[5].Item.GetName);
    }

    public void ChangeItem(ItemInfo slot, Inventory_Database.Item item)
    {
        if (item.GetID == 0)
        {
            slot.Slot.GetComponent<Image>().color = emptyBrown;
            slot.Item = item;
        }
        else
        {
            slot.Slot.GetComponent<Image>().color = UnityEngine.Color.white;
            slot.Slot.transform.localScale = new Vector3(item.X / 40, item.Y / 40);
            slot.Slot.GetComponent<Image>().sprite = item.GetSprite;
            slot.Item = item;
        }
    }

    public void AddItem(int id)
    {
        bool itemAdded = false;
        foreach (ItemInfo i in itemInfo)
        {
            if (i.Item.GetID == 0 && !itemAdded)
            {
                ChangeItem(i, itemList[id]);
                itemAdded = true;
            }
        }
    }

    public void AddItem(string name)
    {
        bool itemAdded = false;
        Inventory_Database.Item item = new Inventory_Database.Item(0, "Empty", null, "Nothing", 0, "Null", 0, 0, "", "");
        foreach (Inventory_Database.Item i in itemList)
        {
            if (i.GetName == "name")
            {
                item = i;
            }
        }
        foreach (ItemInfo i in itemInfo)
        {
            if (i.Item.GetID == 0 && !itemAdded)
            {
                ChangeItem(i, item);
                itemAdded = true;
            }
        }
    }

    public ItemInfo[] GetSlotArray(string slotTagMethod, int itemInfoSize)
    {
        GameObject[] slots;
        ItemInfo[] ii = new ItemInfo[itemInfoSize];
        slots = GameObject.FindGameObjectsWithTag(slotTagMethod).OrderBy(go => go.name).ToArray();
        for (int i = 0; i < slots.Length; i++)
        {
            ii[i] = new ItemInfo(itemList[0], slots[i]);
        }
        return ii;
    }

    public void DisableInventory()
    {
        inventoryOpen = false;
        inventoryUI.SetActive(inventoryOpen);
    } 
}
