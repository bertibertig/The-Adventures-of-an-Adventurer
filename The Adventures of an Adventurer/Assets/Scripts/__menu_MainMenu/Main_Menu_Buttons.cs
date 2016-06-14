using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Main_Menu_Buttons : MonoBehaviour {

	public string levelToLoad;

    private GameObject tempMusic;
    private AudioSource Menu_Music;
    private static bool Music_is_playing;
	private GameObject[] UIElements;

    void Start()
    {
		UIElements = GameObject.FindGameObjectsWithTag("UI");
        tempMusic = GameObject.FindGameObjectWithTag("Music");
        Menu_Music = tempMusic.GetComponent<AudioSource>();
		if (UIElements != null) {
			foreach (GameObject go in UIElements)
				go.SetActive (false);
		}
        if (!Music_is_playing)
        {
            if (tempMusic == null)
            {
                print("ERROR, Music Missing (Game wasn't started from the Main Menu)");
            }
            else
            {
                Menu_Music.Play();
                Music_is_playing = true;
            }
        }
        DontDestroyOnLoad(Menu_Music);
    }

	public void New_Game()
	{
        Menu_Music.Stop();
        Music_is_playing = false;
        foreach (GameObject go in UIElements)
            //go.SetActive (true);
            Destroy(go);
		SceneManager.LoadScene(levelToLoad);
	}

    public void Options()
    {
        SceneManager.LoadScene("Main_Menu_Options");
    }
	
	public void Quit()
	{
        Menu_Music.Stop();
        Destroy(tempMusic);
		Application.Quit();
	}

}
