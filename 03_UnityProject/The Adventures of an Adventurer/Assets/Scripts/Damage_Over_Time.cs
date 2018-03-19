using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Over_Time : MonoBehaviour {

    private int dmg;
    private float time;
    private float dur;
    private bool dotIsActive;
    private bool coroutinesAlreadyStarted = false;

    // Use this for initialization
    void Update () {
        if (dotIsActive && !coroutinesAlreadyStarted)
        {
            coroutinesAlreadyStarted = true;
            StartCoroutine("DoTDuration");
            StartCoroutine("DamageOverTime");
        }
	}

    public int Damage
    {
        set { dmg = value; }
    }

    public float Time
    {
        set { time = value; }
    }

    public float Duration
    {
        set { dur = value; }
    }

    public bool DotIsActive
    {
        get { return dotIsActive; }
        set { dotIsActive = value; }
    }


    private IEnumerator DamageOverTime()
    {
        while(dotIsActive)
        {
            yield return new WaitForSeconds(time);

            this.SendMessageUpwards("ApplyDamage", new object[2] { dmg, false });

            yield return null;
        }
    }

    private IEnumerator DoTDuration()
    {
        yield return new WaitForSeconds(dur);
        
        dotIsActive = false;
        coroutinesAlreadyStarted = false;
    }

    public void StopDoT()
    {
        StopAllCoroutines();
        dotIsActive = false;
        coroutinesAlreadyStarted = false;
    }
}