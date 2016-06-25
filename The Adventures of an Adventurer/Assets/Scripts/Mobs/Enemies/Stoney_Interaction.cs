﻿using UnityEngine;
using System.Collections;

public class Stoney_Interaction : MonoBehaviour {

    public GameObject keyInfo;
    public GameObject Stoney_Awake;
    public GameObject Stoney;
    public GameObject Stoney_without_Axe;

    private GameObject player;
    private bool displayKeyInfo;


    // Use this for initialization
    void Start()
    {
        displayKeyInfo = false;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        keyInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (displayKeyInfo)
        {
            if (Input.GetButtonDown("Interact"))
            {
                Stoney.GetComponent<SpriteRenderer>().sprite = Stoney_without_Axe.GetComponent<SpriteRenderer>().sprite;
                Stoney_Awake.GetComponent<Stoney_Awake>().SetAxRemovedTrue();
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
        float posx = player.transform.position.x;
        float posy = player.transform.position.y;

        keyInfo.transform.position = new Vector3(posx, (posy + 2), transform.position.z);
    }
}
