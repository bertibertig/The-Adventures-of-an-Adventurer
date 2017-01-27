using UnityEngine;
using System.Collections;

public class Gameobject_Checker : MonoBehaviour {

    public string tagOfTheObject;
        // Use this for initialization
    void Start()
    {
        if(GameObject.FindGameObjectsWithTag(tagOfTheObject).Length > 1)
        {
            GameObject[] go = GameObject.FindGameObjectsWithTag("UI_OnlyOnce");
            for (int i = 1; i < go.Length; i++)
            {
                Destroy(go[i]);
            }
        }
    }
}
