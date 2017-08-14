using UnityEngine;
using System.Collections;
using System;

public class HealthPotion : MonoBehaviour {

	public static void ItemFunction(object parameter)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Controller>().Heal(Convert.ToSingle(parameter));
    }
}
