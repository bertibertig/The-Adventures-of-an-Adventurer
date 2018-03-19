using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionForPlayer : MonoBehaviour {

    public Vector2 PositionOnNewLevel { get; set; }
    public bool ChangePositionOnLevelChange { get; set; }

    private void Start()
    {
        ChangePositionOnLevelChange = false;
    }

    private void OnLevelWasLoaded(int level)
    {
        print(PositionOnNewLevel);
        if (ChangePositionOnLevelChange)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
                GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(PositionOnNewLevel.x, PositionOnNewLevel.y, -2);
            ChangePositionOnLevelChange = false;
        }
    }
}
