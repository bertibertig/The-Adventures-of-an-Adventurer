using UnityEngine;
using System.Collections;

public class Entrance_SetPlayer : MonoBehaviour {

    public Vector3 positionOfEntrance;

    public GameObject player;

    void Start()
    {
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("UI"));
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void OnLevelWasLoaded(int level)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(positionOfEntrance.x, positionOfEntrance.y, positionOfEntrance.z); ;
    }
}
