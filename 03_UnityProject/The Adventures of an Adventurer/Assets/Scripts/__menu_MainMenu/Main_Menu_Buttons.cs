using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Main_Menu_Buttons : MonoBehaviour {

	public string levelToLoad;
    public GameObject debugButton;

    private GameObject tempMusic;
    private AudioSource Menu_Music;
    private static bool Music_is_playing;
	private GameObject[] UIElements;
    private KeyCode[] konamiCode = { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A };
    private int codeCounter = 0;
    private bool debugUnlocked = false;

    void Start()
    {
		UIElements = GameObject.FindGameObjectsWithTag("UI");
        tempMusic = GameObject.FindGameObjectWithTag("Music");
        //Menu_Music = tempMusic.GetComponent<AudioSource>();
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
        if (debugButton != null)
            StartCoroutine("CheckForDebugKeysInput");

        if (SystemInfo.operatingSystemFamily.ToString() == "Other")
            debugButton.SetActive(true);
        //DontDestroyOnLoad(Menu_Music);
    }

    private IEnumerator CheckForDebugKeysInput()
    {
        while (!debugUnlocked)
        {
            
            if (Input.GetKeyDown(konamiCode[codeCounter]))
            {
                codeCounter++;
                if(codeCounter >= konamiCode.Length)
                {
                    debugUnlocked = true;
                    debugButton.SetActive(true);
                }
                if(!debugUnlocked)
                    while (Input.GetKeyUp(konamiCode[codeCounter]))
                    {
                        yield return null;
                    }
            }
            else if (Input.anyKeyDown)
            {
                codeCounter = 0;
            }
            yield return null;
        }
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

	public void Networking()
	{
        SceneManager.LoadScene("Main_Menu_Networking");
	}


    public void DebugMenu()
    {
        SceneManager.LoadScene("Main_Menu_Debug");
    }
}
