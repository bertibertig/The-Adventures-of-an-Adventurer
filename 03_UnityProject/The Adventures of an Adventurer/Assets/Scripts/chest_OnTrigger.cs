﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class chest_OnTrigger : MonoBehaviour {

    public bool opened;
    public GameObject keyInfo;
    public AudioSource chest_open_1;
    public AudioSource chest_close_1;
    public int[] items_in_chest_id;

    private GameObject player;
    private bool displayKeyInfo;
    private bool gotContent;
    private Inventory_Main inventory;
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponentInParent<Animator>();
        inventory = GameObject.FindGameObjectWithTag("InventoryUI").GetComponentInChildren<Inventory_Main>();

        displayKeyInfo = false;
        keyInfo.SetActive(false);
        gotContent = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("opened", opened);

        if (displayKeyInfo)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (!opened)
                {
                    opened = true;
                    chest_open_1.Play();
                    anim.SetBool("opened", opened);
                    if (!gotContent)
                    {
                        gotContent = true;
                        inventory.AddItem(3);
                    }
                }

                else if(opened)
                {
                    opened = false;
                    chest_close_1.Play();
                    anim.SetBool("opened", opened);
                }
            }
            FollowPlayer();
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            keyInfo.SetActive(true);
            displayKeyInfo = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            keyInfo.SetActive(false);
            displayKeyInfo = false;
        }
    }

    public void FollowPlayer()
    {
        print(player);
        float posx = player.transform.position.x;
        float posy = player.transform.position.y;

        keyInfo.transform.position = new Vector3(posx, (posy + 2), transform.position.z);
    }
}
