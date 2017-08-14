using UnityEngine;
using System.Collections;

public class DisplayKeyInfo : MonoBehaviour {

    private GameObject player;
    private bool follow;
    public GameObject keyInfo;

    // Use this for initialization
    void Start () {
        follow = false;
        player = GameObject.FindGameObjectWithTag("Player");
        keyInfo = GameObject.Instantiate(Resources.Load("GUI/Buttons/E", typeof(GameObject)) as GameObject, new Vector3(0, 0, 100), Quaternion.identity) as GameObject;
        keyInfo.AddComponent<DontDestroyOnLoad>();
        Disable();
    }
	
	// Update is called once per frame
	void Update () {
        if (keyInfo != null)
            FollowPlayer();
	}

    public void FollowPlayer()
    {
        if (follow)
        {
            float posx = player.transform.position.x;
            float posy = player.transform.position.y;
            keyInfo.transform.position = new Vector3(posx, (posy + 2), transform.position.z);
        }
    }

    public void Enable()
    {
        follow = true;
        keyInfo.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Disable()
    {
        follow = false;
        keyInfo.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void SetKeyInfo(string path, string name = "GUI Element")
    {
        Destroy(keyInfo.gameObject);
        keyInfo = GameObject.Instantiate(Resources.Load(path, typeof(GameObject)) as GameObject, new Vector3(0,0,100), Quaternion.identity) as GameObject;
        keyInfo.AddComponent<DontDestroyOnLoad>();
        Disable();
    }
}
