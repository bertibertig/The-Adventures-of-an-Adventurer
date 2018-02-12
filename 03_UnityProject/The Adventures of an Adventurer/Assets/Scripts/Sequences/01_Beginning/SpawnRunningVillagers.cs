using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRunningVillagers : MonoBehaviour {

    [Header ("Ressource Path of the Villagers e.g.: NPCs/Villagers")]
    public string ressourcePath = "NPCs/Villagers";

    [Header ("X and Y Axis of the Spawn Position for the Villagers")]
    public Vector2 villagersSpawnPosition = new Vector2(0,0);

    [Header ("0 = Right, 180 = Left")]
    public float roation = 180;

    [Header("Seconds until a new Villager is spawned")]
    public float minVillagerDelay = 5;
    public float maxVillagerDelay = 5;

    public bool SpawnVillagers { get; set; }

    private static GameObject[] villagers;
    private int lastSpawnedVillagerId = 0;


    private void Start()
    {
        SpawnVillagers = true;
        LoadVillagers();
        StartCoroutine("SpawnVillagersCoRoutine");
    }

    private void LoadVillagers()
    {
        object[] tmpVillagers= Resources.LoadAll(ressourcePath, typeof(GameObject));
        villagers = Array.ConvertAll(tmpVillagers, i => (GameObject) i);
    }

    private IEnumerator SpawnVillagersCoRoutine()
    {
        do
        {
            //Couple Chance
            int coupleRandom = UnityEngine.Random.Range(0, 10);
            //print(coupleRandom);
            if (coupleRandom == 1)
            {
                SpawnVillager();
                yield return new WaitForSeconds(0.3f);
            }
            SpawnVillager();

            float villagerDelay = UnityEngine.Random.Range(minVillagerDelay, maxVillagerDelay);
            yield return new WaitForSeconds(villagerDelay);
        } while (SpawnVillagers);
    }

	public void SpawnVillager()
    {
        int villagerId = UnityEngine.Random.Range(0, villagers.Length - 1);
        if (villagerId == lastSpawnedVillagerId)
            if (villagerId - 1 < 0)
                villagerId++;
            else
                villagerId--;
        Instantiate(villagers[villagerId], new Vector3(villagersSpawnPosition.x, villagersSpawnPosition.y), Quaternion.Euler(0, roation, 0));
        lastSpawnedVillagerId = villagerId;
    }
}
