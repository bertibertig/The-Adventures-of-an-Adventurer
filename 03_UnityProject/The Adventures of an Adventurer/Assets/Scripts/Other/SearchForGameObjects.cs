using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForGameObjects : MonoBehaviour {

    public event EventHandler PlayerFoundEventHandler;
    public event EventHandler DialogeDBFoundEventHandler;
    public event EventHandler GameobjectFoundHandler;

    private string gameObjectName;

    public GameObject Player { get; set; }

    public GameObject DialogesDB { get; set; }

    public GameObject TempGO { get; set; }

    private void Start()
    {
        StartCoroutine("SerchForPlayer");
        StartCoroutine("SerchForDialogeDBAndWaitForDialogesLoaded");
    }

    IEnumerator SerchForPlayer()
    {
        while(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            yield return null;
        }
        PlayerFound();
    }

    IEnumerator SerchForDialogeDBAndWaitForDialogesLoaded()
    {
        DialogesDB = GameObject.FindGameObjectWithTag("DialogesDB");
        if (DialogesDB == null)
            DialogesDB = GameObject.Instantiate(Resources.Load("Other/DialogesDB"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        while (!DialogesDB.GetComponent<XMLReader>().LoadedDialoge)
        {
            yield return null;
        }
        DialogeDBFound();
    }


    public void RestartSearchPlayerManual()
    {
        StartCoroutine("SerchForPlayer");
    }

    private void PlayerFound()
    {
        print("Notifieing Subscribers (Player)");
        if (PlayerFoundEventHandler != null)
            PlayerFoundEventHandler(this, null);
    }

    private void DialogeDBFound()
    {
        print("Notifieing Subscribers (DialogeDB)");
        if (DialogeDBFoundEventHandler != null)
            DialogeDBFoundEventHandler(this, null);
    }

    IEnumerator SearchForGameObject()
    {
        while (TempGO == null)
        {
            TempGO = GameObject.FindGameObjectWithTag(gameObjectName);
            yield return null;
        }
    }

    private void GameObjectFound()
    {
        if (GameobjectFoundHandler != null)
            GameobjectFoundHandler(this, null);
    }

    private void OnLevelWasLoaded(int level)
    {
        StartCoroutine("SerchForPlayer");
        StartCoroutine("SerchForDialogeDBAndWaitForDialogesLoaded");
    }
}
