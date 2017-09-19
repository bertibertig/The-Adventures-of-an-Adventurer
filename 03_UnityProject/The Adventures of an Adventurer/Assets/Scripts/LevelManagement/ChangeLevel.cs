using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {

    public string levelToLoad = "Main_Menu";

    void OnTriggerEnter2D(Collider2D col)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
