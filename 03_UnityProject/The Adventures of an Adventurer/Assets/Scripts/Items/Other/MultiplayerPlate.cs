using UnityEngine;
using System.Collections;
using System;

public class MultiplayerPlate : MonoBehaviour {

    public static void ItemFunction(object parameter)
    {
        GameObject.FindGameObjectWithTag("MultiplayerController").GetComponent<Main>().MultiplayerComplete.SetActive(true);
        GameObject.FindGameObjectWithTag("InventoryUI").GetComponentInChildren<Inventory_Main>().DisableInventory();
        GameObject.FindGameObjectWithTag("MultiplayerController").GetComponent<Main>().ConnectAfterOpening();
    }
}
