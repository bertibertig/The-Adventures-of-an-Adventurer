using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogeClassControllerStartSequence : MonoBehaviour {

	public static void StopSpawningOfVillagers(object parameter)
    { 
        SpawnRunningVillagers spawnRunningVillagers = GameObject.FindGameObjectsWithTag("Sequences").Where(g => g.name == "StartSequence").FirstOrDefault().GetComponent<SpawnRunningVillagers>();
        spawnRunningVillagers.SpawnVillagers = false;
    }
}
