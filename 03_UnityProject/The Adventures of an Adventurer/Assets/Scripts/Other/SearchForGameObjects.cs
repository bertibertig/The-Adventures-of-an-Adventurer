using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForGameObjects : MonoBehaviour {

    public event EventHandler PlayerFoundEventHandler;
    public event EventHandler GameobjectFoundHandler;

    private string gameObjectName;

    public GameObject Player { get; set; }

    public GameObject TempGO { get; set; }

    private void Start()
    {
        StartCoroutine("SerchForPlayer");
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

    public void RestartSearchPlayerManual()
    {
        StartCoroutine("SerchForPlayer");
    }

    private void PlayerFound()
    {
        if (PlayerFoundEventHandler != null)
            PlayerFoundEventHandler(this, null);
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
    }
}
