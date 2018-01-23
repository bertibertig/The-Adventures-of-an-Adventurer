using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

    [Header("Path + Prefabname without Number z.B.: Environment/Clouds/Level01/CloudL1")]
    public string cloudResourceName = "Environment/Clouds/Level01/CloudL1";

    //Min and Max Vector 2 Clouds can Spawn
    [Header("The Minimal and Maxiamal Position a Cloud should spawn")]
    public Vector2 minCloudsSpawnPosition;
    public Vector2 maxCloudsSpawnPosition;
    [Header("When a cloud should be deleted X-Axis")]
    public float cloudEndOnXAxis;
    [Header("Seconds to Wait until a new Random Cloud Spawns")]
    public float secondsUntilACloudSpawns = 10;

    private bool spawningActivated = true;


    private List<GameObject> clouds = new List<GameObject>();

	// Use this for initialization
	void Start () {
        StartCoroutine("InizialiseCloudsArray");
	}

    IEnumerator InizialiseCloudsArray()
    {
        int i = 0;
        GameObject cloud = new GameObject();
        cloud = Resources.Load(cloudResourceName + "_0" + i) as GameObject;
        if (cloud != null)
        {
            cloud.GetComponent<CloudMovement>().CloudEnd = cloudEndOnXAxis;
            i++;
            do
            {
                clouds.Add(cloud);
                if (i < 10)
                    cloud = Resources.Load(cloudResourceName + "_0" + i) as GameObject;
                else
                    cloud = Resources.Load(cloudResourceName + "_" + i) as GameObject;
                i++;
                yield return null;
            } while (cloud != null);
            StartCoroutine("SpawnClouds");
        }
        else
            print("ERROR: Cloud Ressource not found!");
    }

    IEnumerator SpawnClouds()
    {
        do
        {
            GameObject selectedCloud = clouds[Random.Range(0, clouds.Count - 1)];
            float xSpawnPosition = Random.Range(minCloudsSpawnPosition.x, maxCloudsSpawnPosition.x);
            float ySpawnPosition = Random.Range(minCloudsSpawnPosition.y, maxCloudsSpawnPosition.y);
            GameObject.Instantiate(selectedCloud, new Vector3(xSpawnPosition, ySpawnPosition), Quaternion.identity);
            yield return new WaitForSeconds(secondsUntilACloudSpawns);
        } while (spawningActivated);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
