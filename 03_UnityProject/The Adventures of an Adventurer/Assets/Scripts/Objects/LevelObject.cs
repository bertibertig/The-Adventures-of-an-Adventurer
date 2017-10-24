using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour {



    public int Id { get; set; }
    public string LevelName { get; set; }
    public string LevelToLoad { get; set; }
    public GameObject LevelGameObject { get; set; }
    public SpriteRenderer LevelFrame { get; set; }
    public SpriteRenderer LevelObjectItem { get; set; }

    public LevelObject(int id, string levelName, string levelToLoad, GameObject levelitem)
    {
        this.Id = id;
        this.LevelName = levelName;
        this.LevelToLoad = LevelToLoad;
        this.LevelGameObject = levelitem;
        if (LevelGameObject.GetComponentsInChildren<SpriteRenderer>().Length >= 2)
        {
            this.LevelFrame = levelitem.GetComponentsInChildren<SpriteRenderer>()[1];
            this.LevelObjectItem = levelitem.GetComponentsInChildren<SpriteRenderer>()[0];
        }
        
    }

}
