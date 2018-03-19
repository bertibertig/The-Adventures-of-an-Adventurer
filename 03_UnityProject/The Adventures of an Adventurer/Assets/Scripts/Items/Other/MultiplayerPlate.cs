using UnityEngine;
using System.Collections;
using System;

public class MultiplayerPlate : MonoBehaviour {

    public static void ItemFunction(object parameter)
    {
        if (GameObject.FindGameObjectWithTag("MultiplayerController") == null)
            Instantiate(Resources.Load("Multiplayer/#Multiplayer_Manager") as GameObject, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject.FindGameObjectWithTag("MultiplayerController").GetComponent<Main>().MultiplayerComplete.SetActive(true);
        GameObject.FindGameObjectWithTag("InventoryUI").GetComponentInChildren<Inventory_Main>().DisableInventory();
        GameObject.FindGameObjectWithTag("MultiplayerController").GetComponent<Main>().ConnectAfterOpening();
    }
}
