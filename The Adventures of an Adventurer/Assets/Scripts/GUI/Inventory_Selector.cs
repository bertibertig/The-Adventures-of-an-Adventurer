using UnityEngine;
using System.Collections;

public class Inventory_Selector : MonoBehaviour {

    public bool Selected { get; set; }
    public Inventory_Main.ItemInfo ItemInfo { get; set; }

    // Use this for initialization
    void Start () {
        Selected = false;
    }
}
