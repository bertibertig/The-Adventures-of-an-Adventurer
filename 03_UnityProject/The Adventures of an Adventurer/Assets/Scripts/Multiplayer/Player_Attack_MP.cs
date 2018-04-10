using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack_MP : Photon.MonoBehaviour {

    private bool isAttacking = false;
    private float attackTimer = 0;
    private float attackCooldown = 0.675f;

    public Collider2D attackTrigger;
    private Animator anim;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        attackTrigger.enabled = false;
    }

    void Update()
    {
        if (this.photonView.isMine)
        {
            if (Input.GetButtonDown("Attack1") && isAttacking == false)
            {
                isAttacking = true;
                attackTimer = attackCooldown;
                attackTrigger.enabled = true;
                //this.photonView.RPC("OnAttack", PhotonTargets.All);
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
                    Attack_Trigger.alreadyHit = false;
                    //this.photonView.RPC("OnAttack", PhotonTargets.All);
                }
            }
            //this.photonView.RPC("OnAttack", PhotonTargets.All);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(isAttacking);
        }
        else if (stream.isReading)
        {
            isAttacking = (bool)stream.ReceiveNext();
        }
    }

    [PunRPC]
    void OnAttack()
    {
        if(anim != null)
        {
            anim.SetBool("isAttacking", isAttacking);
        }
    }
}
