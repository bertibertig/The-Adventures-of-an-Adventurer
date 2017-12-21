using UnityEngine;
using System.Collections;
using System;

public class Damage_On_Collision : MonoBehaviour {

    public float damage;
    public bool constantDamage = true;

    public event EventHandler DamagedPlayerEventHandler;

    void OnTriggerEnter2D(Collider2D other)
    {
        print(other.tag);
        if (other.CompareTag("Player") == true)
        {
			other.SendMessage("SetEnemyPlayerGotHitBy", this.gameObject);
            other.SendMessage("ApplyDamage", damage);
            DamagedPlayer();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") == true && constantDamage)
        {
			other.SendMessage("SetEnemyPlayerGotHitBy", this.gameObject);
            other.SendMessage("ApplyDamage", damage);
            DamagedPlayer();
        }
    }

    private void DamagedPlayer()
    {
        print("Notifieing Subscribers (DialogeDB)");
        if (DamagedPlayerEventHandler != null)
            DamagedPlayerEventHandler(this, null);
    }
}