using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory_Database : MonoBehaviour {

    public int NUMBER_OF_SLOTS;
    public GameObject inventiryUI;

    private UnityEngine.Color emptyBrown;
    private bool slotsLoaded;
    private List<Item> itemList = new List<Item>();
    private ItemInfo[] itemInfo;
    private bool itemInfoLoaded;

    public List<Item> GetItemDatabase { get { return this.itemList; } }
    public ItemInfo[] GetItemInfo { get { return this.itemInfo; } }
    public bool GetItemInfoLoaded { get { return this.itemInfoLoaded; } }

    public class Item
    {
        private int ID;
        private string name;
        private Sprite itemSymbol;
        private string whatToDo;
        private int price;
        private string itemType;
        private float x;
        private float y;
        private string descriptionEng;
        private string descriptionGer;
        private string function;
        private string classString;

        public int GetID { get { return this.ID; } }
        public Sprite GetSprite{ get { return this.itemSymbol; }  }
        public float X { get { return this.x; } }
        public float Y { get { return this.y; } }
        public string GetEngDescription { get { return this.descriptionEng; } }
        public string GetGerDescription { get { return this.descriptionGer; } }
        public string GetName { get { return this.name; } }
        public string GetFunction { get { return this.function; } }
        public string GetClassString { get { return this.classString; } }


        public Item(int ID, string name, Sprite itemSymbol, string whatToDo, int price, string itemType, float x, float y, string descriptionEng, string descriptionGer, string function = "", string classString = "")
        {
            this.ID = ID;
            this.name = name;
            this.itemSymbol = itemSymbol;
            this.whatToDo = whatToDo;
            this.price = price;
            this.itemType = itemType;
            this.x = x;
            this.y = y;
            this.descriptionEng = descriptionEng;
            this.descriptionGer = descriptionGer;
            this.function = function;
            this.classString = classString;
        }
    }

    public class ItemInfo
    {
        private Item item;
        private GameObject slot;

        public Item Item { get { return this.item; } set { this.item = value; } }
        public GameObject Slot { get { return this.slot; } set { this.slot = value; } }

        public ItemInfo(Item item, GameObject slot)
        {
            this.item = item;
            this.slot = slot;
        }
    }

	// Use this for initialization
	void Start ()
    {
        if (NUMBER_OF_SLOTS <= 0)
            NUMBER_OF_SLOTS = 50;
        itemInfoLoaded = false;
        slotsLoaded = false;
        itemInfo = new ItemInfo[NUMBER_OF_SLOTS];
        FillInventoryList();
        GetSlotArray();
        emptyBrown = itemInfo[0].Slot.GetComponent<Image>().color;
        FillInventoryTemporarely();
        itemInfoLoaded = true;
        inventiryUI.SetActive(false);
	}

    private void FillInventoryList()
    {
        //print(GameObject.FindGameObjectsWithTag("ItemSymbol")[0].ToString());
        //itemSymbols = GameObject.FindGameObjectsWithTag("ItemSymbol").OrderBy(go => go.name).ToArray();
        itemList.Add(new Item(0, "Empty", null, "Nothing", 0, "Null", 0, 0, "", ""));
        itemList.Add(new Item(1, "Axe", (Resources.Load("Items/Weapons/Axe_01", typeof(GameObject)) as GameObject).GetComponent<SpriteRenderer>().sprite, "Nothing", 1, "Weapon", 19, 35, "A Basic Wodden Axe.", "Eine normale Holzaxt."));
        itemList.Add(new Item(2, "Battle Axe", (Resources.Load("Items/Weapons/Axe_02", typeof(GameObject)) as GameObject).GetComponent<SpriteRenderer>().sprite, "Shockwave", 100, "Weapon", 30, 43, "A enchanted steelaxe which can cast a shokwave.", "Eine verzauberte Stahlaxt welche eine Schockwelle beschwören kann."));
        itemList.Add(new Item(3, "Health Potion", (Resources.Load("Items/Other/Health_Potion", typeof(GameObject)) as GameObject).GetComponent<SpriteRenderer>().sprite, "HealsHP", 50, "Other", 26, 32, "A potion which heals 20 HP of the player", "Ein Trank welcher 20 HP wiederherstellt", "ItemFunction", "HealthPotion"));

    }

    private void FillInventoryTemporarely()
    {
        //ChangeItem(itemInfo[SlotID+1], itemList[ItemID]);
        ChangeItem(itemInfo[0], itemList[1]);
        ChangeItem(itemInfo[1], itemList[2]);
        print(itemInfo[5].Item.GetName);
    }

    public void ChangeItem(ItemInfo slot, Item item)
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

    public Item GetItemWithID(int id)
    {
        return itemList[id];
    }

    public ItemInfo GetItemInfoWithID(int id)
    {
        return itemInfo[id];
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
        Item item = new Item(0, "Empty", null, "Nothing", 0, "Null", 0, 0, "", "");
        foreach (Item i in itemList)
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


    private ItemInfo[] GetSlotArray()
    {
        GameObject[] slots;
        slots = GameObject.FindGameObjectsWithTag("Slot").OrderBy(go => go.name).ToArray();
        for(int i = 0; i < slots.Length; i++)
        {
            itemInfo[i] = new ItemInfo(itemList[0], slots[i]);
        }
        return this.itemInfo;
    }

    /*public bool AddViaIdToInventory(int id)
    {
        if (id >= itemList.Count)
            return false;
        return AddItemToInventory(itemList[id]);
    }
    public bool AddItemToInventory(Item item)
    {
        for(int i = 0; i < itemInfo.Length; i++)
            if (itemInfo[i].Item.GetID == 0)
            {
                ChangeItem(itemInfo[i], item);
                return true;
            }
        return false;
    }*/
}
