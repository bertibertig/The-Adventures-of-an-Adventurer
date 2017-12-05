using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public class Main_Menu_Options_Buttons : MonoBehaviour {

    public Text language;

    GameObject eventList;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("EventList") == null)
            eventList = GameObject.Instantiate(Resources.Load("Other/EventList") as GameObject);
        else
            eventList = GameObject.FindGameObjectWithTag("EventList");

        eventList.GetComponentInChildren<LanguageReader>().LanguageLoadedEventHandler += Main_Menu_Options_Buttons_LanguageLoadedEventHandler;

        
    }

    private void Main_Menu_Options_Buttons_LanguageLoadedEventHandler(object sender, System.EventArgs e)
    {
        language.text = eventList.GetComponentInChildren<LanguageReader>().Language.First().ToString().ToUpper() + eventList.GetComponentInChildren<LanguageReader>().Language.Substring(1);
    }

    public void BackToMainMenu()
    {
        eventList.GetComponentInChildren<LanguageReader>().Language = language.text.ToLower();
        print(eventList.GetComponentInChildren<LanguageReader>().Language);
        SceneManager.LoadScene(0);
    }

    public void ChangeLanguage()
    {
        if (language.text == "English")
            language.text = "German";
        else
            language.text = "English";
        
    }
}
