using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogeMPBoss : MonoBehaviour {

	public static void SummonSpirit(object parameter)
    {
        print("Summoning Spirit ...");
        Vector3 bossPos = GameObject.FindGameObjectWithTag("MPBoss").transform.position;
        Vector3 spiritPos = new Vector3(bossPos.x - 2.2f, bossPos.y + 2.2f, 0);
        GameObject tmpObj = GameObject.Instantiate(Resources.Load("Mobs/Bosses/1_MPBoss/BossSpirit"), spiritPos, Quaternion.identity) as GameObject;
        GameObject.FindGameObjectWithTag("MPBoss").GetComponentInChildren<MPBossFight>().Spirit = tmpObj;

    }
}
