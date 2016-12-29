using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory_Database : MonoBehaviour {

    private List<Item> itemList = new List<Item>();
    private bool itemInfoLoaded;

    public List<Item> GetItemDatabase { get { return this.itemList; } }
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
        private float optionalIntParam;

        public int GetID { get { return this.ID; } }
        public Sprite GetSprite{ get { return this.itemSymbol; }  }
        public float X { get { return this.x; } }
        public float Y { get { return this.y; } }
        public string GetEngDescription { get { return this.descriptionEng; } }
        public string GetGerDescription { get { return this.descriptionGer; } }
        public string GetName { get { return this.name; } }
        public string GetFunction { get { return this.function; } }
        public string GetClassString { get { return this.classString; } }
        public float GetOptionalIntParam { get { return this.optionalIntParam; } }


        public Item(int ID, string name, Sprite itemSymbol, string whatToDo, int price, string itemType, float x, float y, string descriptionEng, string descriptionGer, string function = "", string classString = "", float optionalIntParam = 0)
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
            this.optionalIntParam = optionalIntParam;
        }
    }

	// Use this for initialization
	void Start ()
    {
        itemInfoLoaded = false;
        FillInventoryList();
        itemInfoLoaded = true;
    }

    private void FillInventoryList()
    {
        //print(GameObject.FindGameObjectsWithTag("ItemSymbol")[0].ToString());
        //itemSymbols = GameObject.FindGameObjectsWithTag("ItemSymbol").OrderBy(go => go.name).ToArray();
        itemList.Add(new Item(0, "Empty", null, "Nothing", 0, "Null", 0, 0, "", ""));
        itemList.Add(new Item(1, "Axe", (Resources.Load("Items/Weapons/Axe_01", typeof(GameObject)) as GameObject).GetComponent<SpriteRenderer>().sprite, "Nothing", 1, "Weapon", 19, 35, "A Basic Wodden Axe.", "Eine normale Holzaxt."));
        itemList.Add(new Item(2, "Battle Axe", (Resources.Load("Items/Weapons/Axe_02", typeof(GameObject)) as GameObject).GetComponent<SpriteRenderer>().sprite, "Shockwave", 100, "Weapon", 30, 43, "A enchanted steelaxe which can cast a shokwave.", "Eine verzauberte Stahlaxt welche eine Schockwelle beschwören kann."));
        itemList.Add(new Item(3, "Health Potion", (Resources.Load("Items/Other/Health_Potion", typeof(GameObject)) as GameObject).GetComponent<SpriteRenderer>().sprite, "HealsHP", 50, "Other", 26, 32, "A potion which heals 20 HP of the player", "Ein Trank welcher 20 HP wiederherstellt", "ItemFunction", "HealthPotion", 20));

    }

    public Item GetItemWithID(int id)
    {
        if (id > itemList.Count)
            return itemList[0];
        else
            return itemList[id];
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
