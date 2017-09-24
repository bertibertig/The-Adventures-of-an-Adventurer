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
        private string ressourcePath;
        private string whatToDo;
        private int price;
        private string itemType;
        private float x;
        private float y;
        private string descriptionEng;
        private string descriptionGer;
        private string function;
        private string classString;
        private object[] optionalParams;

        public int GetID { get { return this.ID; } }
        public Sprite GetSprite{ get { return this.itemSymbol; }  }
        public string GetRessourcePath { get { return this.ressourcePath; } }
        public int GetPrice { get { return this.price; } }
        public float X { get { return this.x; } }
        public float Y { get { return this.y; } }
        public string GetEngDescription { get { return this.descriptionEng; } }
        public string GetGerDescription { get { return this.descriptionGer; } }
        public string GetName { get { return this.name; } }
        public string GetFunction { get { return this.function; } }
        public string GetClassString { get { return this.classString; } }
        public object[] GetOptionalParams { get { return this.optionalParams; } }


        public Item(int ID, string name, string ressourcePath, string whatToDo, int price, string itemType, float x, float y, string descriptionEng, string descriptionGer, string function = "", string classString = "", object[] optionalParams = null)
        {
            this.ID = ID;
            this.name = name;
            this.ressourcePath = ressourcePath;
            if(ressourcePath != "")
                this.itemSymbol = (Resources.Load(ressourcePath, typeof(GameObject)) as GameObject).GetComponent<SpriteRenderer>().sprite;
            this.whatToDo = whatToDo;
            this.price = price;
            this.itemType = itemType;
            this.x = x;
            this.y = y;
            this.descriptionEng = descriptionEng;
            this.descriptionGer = descriptionGer;
            this.function = function;
            this.classString = classString;
            if(optionalParams != null)
                this.optionalParams = ((IEnumerable)optionalParams).Cast<object>().Select(s => s == null ? s : s.ToString()).ToArray();
        }
    }

	// Use this for initialization
	void Start ()
    {
        itemInfoLoaded = false;
        FillInventoryList();
    }

    private void FillInventoryList()
    {
        //print(GameObject.FindGameObjectsWithTag("ItemSymbol")[0].ToString());
        //itemSymbols = GameObject.FindGameObjectsWithTag("ItemSymbol").OrderBy(go => go.name).ToArray();
        itemList.Add(new Item(0, "Empty", "", "Nothing", 0, "Null", 0, 0, "", ""));
        itemList.Add(new Item(1, "Axe", "Items/Weapons/Axe_01", "Nothing", 1, "Weapon", 19, 35, "A Basic Wodden Axe.", "Eine normale Holzaxt.", "ItemFunction", "Axe"));
        itemList.Add(new Item(2, "Battle Axe", "Items/Weapons/Axe_02", "Shockwave", 100, "Weapon", 30, 43, "A enchanted steelaxe which can cast a shokwave.", "Eine verzauberte Stahlaxt welche eine Schockwelle beschwören kann."));
        itemList.Add(new Item(3, "Health Potion", "Items/Other/Health_Potion", "HealsHP", 50, "Other", 26, 32, "A potion which heals 20 HP of the player", "Ein Trank welcher 20 HP wiederherstellt", "ItemFunction", "HealthPotion", new object[] { "20" }));
        itemList.Add(new Item(4, "Wood", "Items/Other/Wood", "Burns", 5, "Other", 40, 40, "The remains of the witch disguised as wood", "Die überreste der Hexe, welche als Holz getarnt wurden"));
        itemInfoLoaded = true;

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
