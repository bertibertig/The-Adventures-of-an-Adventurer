using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory_Database : MonoBehaviour {

    public int NUMBER_OF_SLOTS;

    private UnityEngine.Color emptyBrown;
    private GameObject[] itemSymbols;
    private bool slotsLoaded;
    private List<Item> itemList = new List<Item>();
    private ItemInfo[] itemInfo;

    public List<Item> GetItemDatabase { get { return this.itemList; } }
    public ItemInfo[] GetItemInfo { get { return this.itemInfo; } }

    public class Item
    {
        private int ID;
        private string Name;
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

        public Item(int ID, string Name, Sprite itemSymbol, string whatToDo, int price, string itemType, float x, float y, string descriptionEng, string descriptionGer)
        {
            this.ID = ID;
            this.Name = Name;
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
        slotsLoaded = false;
        itemInfo = new ItemInfo[NUMBER_OF_SLOTS];
        FillInventoryList();
        GetSlotArray();
        emptyBrown = itemInfo[0].Slot.GetComponent<Image>().color;
        //FillInventoryTemporarely();
	}

    private void FillInventoryList()
    {
        //print(GameObject.FindGameObjectsWithTag("ItemSymbol")[0].ToString());
        itemSymbols = GameObject.FindGameObjectsWithTag("ItemSymbol").OrderBy(go => go.name).ToArray();
        itemList.Add(new Item(0, "Empty", null, "Nothing", 0, "Null", 0, 0, "", ""));
        itemList.Add(new Item(1, "Axe", itemSymbols[0].GetComponent<SpriteRenderer>().sprite, "Nothing", 1, "Weapon", 19, 35, "A Basic Wodden Axe.", "Eine normale Holzaxt."));
        itemList.Add(new Item(1, "Battle Axe", itemSymbols[1].GetComponent<SpriteRenderer>().sprite, "Shockwave", 100, "Weapon", 30, 43, "A enchanted steelaxe which can cast a shokwave.", "Eine verzauberte Stahlaxt welche eine Schockwelle beschwören kann."));
    }
	
    private void FillInventoryTemporarely()
    {
        ChangeItem(itemInfo[0].Slot, itemList[1]);
        ChangeItem(itemInfo[1].Slot, itemList[2]);
        //print(itemInfo[0].Slot.ToString());
    }

    private void ChangeItem(GameObject slot, Item item)
    {
        if (item.GetID == 0)
            slot.GetComponent<Image>().color = emptyBrown;
        else
        {
            slot.GetComponent<Image>().color = UnityEngine.Color.white;
            slot.transform.localScale = new Vector3(item.X/40, item.Y/40);
            slot.GetComponent<Image>().sprite = item.GetSprite;
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
}
