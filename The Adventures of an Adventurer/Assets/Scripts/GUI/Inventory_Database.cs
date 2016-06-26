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
    private GameObject[] itemSymbols;
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

        public int GetID { get { return this.ID; } }
        public Sprite GetSprite{ get { return this.itemSymbol; }  }
        public float X { get { return this.x; } }
        public float Y { get { return this.y; } }
        public string GetEngDescription { get { return this.descriptionEng; } }
        public string GetGerDescription { get { return this.descriptionGer; } }
        public string GetName { get { return this.name; } }


        public Item(int ID, string name, Sprite itemSymbol, string whatToDo, int price, string itemType, float x, float y, string descriptionEng, string descriptionGer)
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
        itemSymbols = GameObject.FindGameObjectsWithTag("ItemSymbol").OrderBy(go => go.name).ToArray();
        itemList.Add(new Item(0, "Empty", null, "Nothing", 0, "Null", 0, 0, "", ""));
        itemList.Add(new Item(1, "Axe", itemSymbols[0].GetComponent<SpriteRenderer>().sprite, "Nothing", 1, "Weapon", 19, 35, "A Basic Wodden Axe.", "Eine normale Holzaxt."));
        itemList.Add(new Item(2, "Battle Axe", itemSymbols[1].GetComponent<SpriteRenderer>().sprite, "Shockwave", 100, "Weapon", 30, 43, "A enchanted steelaxe which can cast a shokwave.", "Eine verzauberte Stahlaxt welche eine Schockwelle beschwören kann."));
    }
	
    private void FillInventoryTemporarely()
    {
        //ChangeItem(itemInfo[SlotID+1], itemList[ItemID]);
        ChangeItem(itemInfo[0], itemList[1]);
        ChangeItem(itemInfo[1], itemList[2]);
        //print(itemInfo[1].Item.GetName);
    }

    public void ChangeItem(ItemInfo iInfo, Item item)
    {
        if (item.GetID == 0)
        {
            iInfo.Slot.GetComponent<Image>().color = emptyBrown;
            iInfo.Item = item;
        }
        else
        {
            iInfo.Slot.GetComponent<Image>().color = UnityEngine.Color.white;
            iInfo.Slot.transform.localScale = new Vector3(item.X / 40, item.Y / 40);
            iInfo.Slot.GetComponent<Image>().sprite = item.GetSprite;
            iInfo.Item = item;
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
}
