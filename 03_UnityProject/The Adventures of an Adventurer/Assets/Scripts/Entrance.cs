using UnityEngine;
//using UnityEditor;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class Entrance : MonoBehaviour {

    public GameObject keyInfo;

    private GameObject player;
    private bool displayKeyInfo;
    private GameObject EventList;

	// Use this for initialization
	void Start () {
        SearchForGameObjects searchForPlayer = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
        searchForPlayer.PlayerFoundEventHandler += PlayerFound;
        displayKeyInfo = false;
        keyInfo.SetActive(false);
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PlayerFound(object sender, EventArgs e)
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        if (displayKeyInfo)
        {
            if (Input.GetButtonDown("Up"))
            {
                gameObject.GetComponent<ChangeLevel>().LoadLevel();
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
        if (player != null)
        {
            float posx = player.transform.position.x;
            float posy = player.transform.position.y;

            keyInfo.transform.position = new Vector3(posx, (posy + 2), transform.position.z);
        }
    }
}
