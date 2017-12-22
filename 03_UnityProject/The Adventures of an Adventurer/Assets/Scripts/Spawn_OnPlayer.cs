using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_OnPlayer : MonoBehaviour {

    private GameObject player;
    private Vector2 spawnLocation;
    private Vector2 clickCoordinate;

    public GameObject objectToSpawn;
    public float verticalOffset;
    public float horizontalOffset;
    public float objectLifespan;
    public bool spawnByPressOnPlayer = false;

	// Use this for initialization
	void Start () {
        if (objectLifespan == 0)
            objectLifespan = 3.0f;

        if(GameObject.FindGameObjectsWithTag("Player").Length == 1)
            player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		if(spawnByPressOnPlayer && objectToSpawn != null && Input.GetMouseButtonUp(0))
        {
            spawnLocation = new Vector2(player.transform.position.x + horizontalOffset, player.transform.position.y + verticalOffset);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.transform.CompareTag("Player"))
            {
                SpawnOnPlayer(objectToSpawn, objectLifespan);
            }
        }
	}

    public void SpawnOnPlayer(GameObject objToSpawn, float objLifespan)
    {
        GameObject objectCopy = Instantiate(objToSpawn, new Vector3(spawnLocation.x, spawnLocation.y, objToSpawn.transform.position.z), Quaternion.identity);
        objectCopy.transform.SetParent(player.transform);
        objectCopy.SetActive(true);

        for(int i = 0; i < objectCopy.transform.childCount; i++)
            objectCopy.transform.GetChild(i).gameObject.SetActive(true);

        Destroy(objectCopy, objLifespan);
    }
}