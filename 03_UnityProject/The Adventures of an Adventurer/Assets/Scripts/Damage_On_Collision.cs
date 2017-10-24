using UnityEngine;
using System.Collections;

public class Damage_On_Collision : MonoBehaviour {

    public float damage;



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == true)
        {
			other.SendMessage("SetEnemyPlayerGotHitBy", this.gameObject);
            other.SendMessage("ApplyDamage", damage);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") == true)
        {
			other.SendMessage("SetEnemyPlayerGotHitBy", this.gameObject);
            other.SendMessage("ApplyDamage", damage);
        }
    }
}