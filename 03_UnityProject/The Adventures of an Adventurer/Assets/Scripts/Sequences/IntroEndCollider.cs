using UnityEngine;
using System.Collections;

public class IntroEndCollider : MonoBehaviour {

    private Intro intro;
    public Vector3 positionOfEntrance;

    public GameObject player;
    void Start()
    {
        intro = GameObject.FindGameObjectWithTag("Intro").GetComponent<Intro>();
        //DontDestroyOnLoad(player);
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("UI"));
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }


	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            player.GetComponent<Player_Movement>().MovementDisabled = false;
            player.GetComponent<Player_Attack>().enabled = true;
            intro.IntroEnd = true;
        }
    }

    void OnLevelWasLoaded(int level)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(positionOfEntrance.x, positionOfEntrance.y, positionOfEntrance.z); ;
    }

}
