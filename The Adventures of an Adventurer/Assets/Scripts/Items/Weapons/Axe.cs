using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Axe : MonoBehaviour {

    private static Inventory_Main.ItemInfo[] ii;

    public static void ItemFunction()
    {
        ii = GameObject.FindGameObjectWithTag("InventoryUI").GetComponentInChildren<Inventory_Main>().GetItemInfo;
        Inventory_Main.ItemInfo[] iteminfo = (Inventory_Main.ItemInfo[])ii;
        Inventory_Main.ItemInfo axe = iteminfo.Where(n => n.Item.GetName == "Axe").FirstOrDefault();
        if (axe.Slot.GetComponent<Image>().color == UnityEngine.Color.white)
            axe.Slot.GetComponent<Image>().color = UnityEngine.Color.blue;
        else
            axe.Slot.GetComponent<Image>().color = UnityEngine.Color.white;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Attack>().enabled = !GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Attack>().enabled;
    }
}
