using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadLevel : MonoBehaviour {

    // Use this for initialization
    void OnLevelWasLoaded(int level)
    {
        StartCoroutine("LevelLoaded");
    }

    private IEnumerator LevelLoaded()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("0_Level_Tutorial");
    }
}
