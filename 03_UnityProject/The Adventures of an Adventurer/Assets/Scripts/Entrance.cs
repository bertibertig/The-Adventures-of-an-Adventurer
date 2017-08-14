using UnityEngine;
//using UnityEditor;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour {

    public GameObject keyInfo;
    public int levelToLoad;

    private GameObject player;
    private bool displayKeyInfo;
    private GameObject EventList;

	// Use this for initialization
	void Start () {
        displayKeyInfo = false;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        keyInfo.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (displayKeyInfo)
        {
            if (Input.GetButtonDown("Up"))
            {
                SceneManager.LoadScene(levelToLoad);
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
        float posx = player.transform.position.x;
        float posy = player.transform.position.y;

        keyInfo.transform.position = new Vector3(posx, (posy + 2), transform.position.z);
    }
}
