using UnityEngine;
using System.Collections;

public class HealthPotion : MonoBehaviour {

	public static void ItemFunction()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Controller>().Heal(20);
    }
}
