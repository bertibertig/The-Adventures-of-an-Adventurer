using UnityEngine;
using System.Collections;

public class Player_Attack : MonoBehaviour {

    private bool isAttacking = false;
    private float attackTimer = 0;
    private float attackCooldown = 0.675f;

    public Collider2D attackTrigger;
    private Animator anim;

	public double AttackTimer
	{
		get { return attackTimer; }
	}

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        attackTrigger.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Attack1") && isAttacking == false)
        {
            isAttacking = true;
            attackTimer = attackCooldown;
            attackTrigger.enabled = true;
        }

        if (isAttacking == true)
        {
            if (attackTimer > 0)
            {
                attackTimer = attackTimer - Time.deltaTime;
            }
            else
            {
                isAttacking = false;
                attackTrigger.enabled = false;
            }
        }

        anim.SetBool("isAttacking", isAttacking);
    }
}
