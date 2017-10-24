using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevelOnAction : MonoBehaviour {

    public bool onTriggerEnter = false;
    public bool onButtonPress = false;
    public string button = "";

    public ChangeLevel ChLevel { get; set; }

	// Use this for initialization
	void Start () {
        ChLevel = gameObject.GetComponent<ChangeLevel>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onTriggerEnter && collision.CompareTag("Player"))
            ChLevel.LoadLevel();
    }

}
