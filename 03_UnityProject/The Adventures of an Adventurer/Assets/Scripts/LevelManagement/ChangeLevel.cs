﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {

    public string levelToLoad = "Main_Menu";
    public Vector2 newPosition;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("PlayerPosition") == null)
        {
            GameObject tmpObj = GameObject.Instantiate(Resources.Load("Other/PlayerSetPositionForNextLevel"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
    }

    public void LoadLevel()
    {
        if (GameObject.FindGameObjectWithTag("PlayerPosition") != null)
        {
            GameObject.FindGameObjectWithTag("PlayerPosition").GetComponent<SetPositionForPlayer>().PositionOnNewLevel = newPosition;
            GameObject.FindGameObjectWithTag("PlayerPosition").GetComponent<SetPositionForPlayer>().ChangePositionOnLevelChange = true;
            if (GameObject.FindGameObjectWithTag("EventList"))
            {
                print("Restarting Search");
                GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>().RestartSearchPlayerManual();
            }
        }
        if (levelToLoad == "Main_Menu")
        {
            Destroy(GameObject.FindGameObjectWithTag("UI_OnlyOnce"));
            Destroy(GameObject.FindGameObjectWithTag("EventList"));
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
        if (this.gameObject.GetComponent<Entrance>() != null)
            this.gameObject.GetComponent<Entrance>().keyInfo.GetComponent<SpriteRenderer>().enabled = false;
        SceneManager.LoadScene(levelToLoad);
    }
}
