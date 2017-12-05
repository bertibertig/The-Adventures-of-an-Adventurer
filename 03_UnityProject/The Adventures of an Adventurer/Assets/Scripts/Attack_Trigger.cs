using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Attack_Trigger : MonoBehaviour {

    public int dmg = 30;
    private int dmgOffset;
    private int minDmg;
    private int maxDmg;
    private bool isCrit;
	public static bool alreadyHit;
    object[] attackData = new object[2];

    void Start()
    {
        dmgOffset = Convert.ToInt32(Math.Max(Math.Ceiling(25-dmg*0.01), 1)*dmg/100);
        minDmg = dmg - dmgOffset;
        maxDmg = dmg + dmgOffset;
		alreadyHit = false;
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		isCrit = Critical ();
		if (isCrit == false) {
			attackData [0] = UnityEngine.Random.Range (minDmg, maxDmg);
		} else {
			attackData [0] = Convert.ToInt32 (dmg * 1.5);
		}
		attackData[1] = isCrit;

		if (col.isTrigger != true && col.CompareTag ("Enemy") && alreadyHit == false && col.gameObject.layer != LayerMask.NameToLayer("Invincible")) {
			alreadyHit = true;
			col.SendMessageUpwards ("ApplyDamage", attackData);
		}
    }

    bool Critical()
    {
        int random = UnityEngine.Random.Range(0, 100);
        if (random < 5)
        {
            return true;
        }
        return false;
    }

	public void MakeHitStateFalse()
	{
		alreadyHit = false;
	}
}
