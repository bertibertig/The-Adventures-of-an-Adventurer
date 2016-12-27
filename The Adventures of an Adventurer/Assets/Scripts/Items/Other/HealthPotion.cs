using UnityEngine;
using System.Collections;

public class HealthPotion : MonoBehaviour {

	public static void ItemFunction()
    {
        print("Begin Healing process...");
        GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Controller>().Heal(20);
    }
}
