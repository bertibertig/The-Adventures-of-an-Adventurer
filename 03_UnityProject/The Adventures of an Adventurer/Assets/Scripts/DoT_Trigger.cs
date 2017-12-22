using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoT_Trigger : MonoBehaviour {

    public int damagePerTick;
    public int timeBetweenTicks;
    public int dotDuration;
    public string hitText;
    Damage_Over_Time dot;

    void OnTriggerEnter2D(Collider2D otherCol)
    {
        if (otherCol.tag == "Enemy")
        {
            otherCol.GetComponent<Enemy_Health_Controller>().InitTextPopup(hitText);
            dot = otherCol.GetComponent<Damage_Over_Time>();
            if (!dot.DotIsActive)
            {
                ActivateDoT();
            }
            else
            {
                dot.StopDoT();
                ActivateDoT();
            }
        }
    }

    void ActivateDoT()
    {
        dot.Damage = damagePerTick;
        dot.Time = timeBetweenTicks;
        dot.Duration = dotDuration;
        dot.DotIsActive = true;
    }
}
