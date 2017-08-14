using UnityEngine;
using System.Collections;

public class Entrance_SetPlayer : MonoBehaviour {

    public GameObject player;
    public GameObject eventList { get; set; }

    void Start()
    {
        eventList = GameObject.FindGameObjectWithTag("EventList");
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
        player.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 0.944361f, player.gameObject.transform.position.z);
    }
}
