using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Main_Menu_Options_Buttons : MonoBehaviour {

    public Text language;

    private string filepath; 
    private string mainLanguage;
    private string directory;
    private string lang;

    private string[] options = {
                               "###Language###",
                               "###language:",
                               "english"
                               };

    public string GetLanguage
    {
        get
        {
            CheckIfFolderExists();
            options = ReadOptions();
            return options[2];
        }
    }


    void Start()
    {
        filepath = Directory.GetCurrentDirectory() +  @"\Saves\options.txt";
        directory = Directory.GetCurrentDirectory() +  @"\Saves\";

        CheckIfFolderExists();
        options = ReadOptions();
        ApplyOptions(options);
    }

    string[] ReadOptions()
    {
        string[] temp = File.ReadAllLines(filepath);
        return temp;
    }

    bool CheckIfFolderExists()
    {
        if (File.Exists(filepath))
            return true;
        Directory.CreateDirectory(directory);
        File.WriteAllLines("filepath", options);
        return true;
    }

    public void BackToMainMenu()
    {
        print(lang);
        SaveOptions();
        Application.LoadLevel(0);
    }

    void SaveOptions()
    {
        options[2] = lang;
        File.WriteAllLines("filepath", options);
    }

    void ApplyOptions(string[] options)
    {
        if (options[2] == "german")
        {
            language.text = "Deutsch";
            lang = "german";
        }
        else
        {
            language.text = "English";
            lang = "english";
        }
    }

    public void ChangeLanguage()
    {
        if (language.text == "Deutsch")
        {
            language.text = "English";
            lang = "english";
        }
        else
        {
            language.text = "Deutsch";
            lang = "german";
        }
        SaveOptions();
    }

    public void SaveChanges()
    {
        print(filepath);
        StreamWriter sw = new StreamWriter(filepath);

        sw.WriteLine("###Language###");
        sw.WriteLine("###language:");
        sw.WriteLine(lang);

        sw.Close();
    }
}
