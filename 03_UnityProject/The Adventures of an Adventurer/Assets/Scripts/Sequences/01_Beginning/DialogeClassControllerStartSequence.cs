using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogeClassControllerStartSequence : MonoBehaviour {

    private static GameObject player;

	public static void StopSpawningOfVillagers(object parameter)
    { 
        SpawnRunningVillagers spawnRunningVillagers = GameObject.FindGameObjectsWithTag("Sequences").Where(g => g.name == "StartSequence").FirstOrDefault().GetComponent<SpawnRunningVillagers>();
        spawnRunningVillagers.SpawnVillagers = false;
    }

    public static void MovePlayerOutOfTent(object parameter)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.rotation = new Quaternion(0, 180, 0, player.transform.rotation.w);
        player.GetComponent<Player_Movement>().MovePlayerToPosition(new Vector2(-35, 1.8f));
        player.GetComponent<Player_Movement>().MovementToPositionEndedHandler += DialogeClassControllerStartSequence_MovementToPositionEndedHandler;
    }

    private static void DialogeClassControllerStartSequence_MovementToPositionEndedHandler(object sender, System.EventArgs e)
    {
        DisableHalfTent();
        player.GetComponent<Player_Movement>().MovementToPositionEndedHandler -= DialogeClassControllerStartSequence_MovementToPositionEndedHandler;
        player.transform.rotation = new Quaternion(0, 0, 0, player.transform.rotation.w);
    }

    private static void DisableHalfTent()
    {
        GameObject halfTent = GameObject.FindGameObjectsWithTag("SequenceItems").Where(g => g.name == "halfTent").FirstOrDefault();
        halfTent.SetActive(false);
    }
}
