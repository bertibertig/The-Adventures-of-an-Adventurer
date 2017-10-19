using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;

public class XMLReader : MonoBehaviour {

    public string filepath = @"Assets\xml\dialoges\0_Tutorial.xml";

    private string language;
    private GameObject player;

    public List<GameObject> dHandlerDB { get; set; }
    public bool LoadedDialoge { get; set; }

    private void Start()
    {
        dHandlerDB = new List<GameObject>();
        LoadedDialoge = false;
        if (GameObject.FindGameObjectsWithTag("EventList").Length <= 0)
            language = "english";
        else
            language = GameObject.FindGameObjectWithTag("EventList").GetComponentInChildren<LanguageReader>().Language;

        SearchForGameObjects searchForPlayer = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
        searchForPlayer.PlayerFoundEventHandler += PlayerFound;


        //StartCoroutine("LoadDialouge");
    }

    public void PlayerFound(object sender, EventArgs e)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("LoadDialouge");
    }

    /*public DialogeHandler SpecificLoadDialouge(string id, string filepath)
    {
        int length = 0;
        output = new StringBuilder();
        List<string> dialoge = new List<string>();
        List<string> talkerNames = new List<string>();
        List<string> talkerRessources = new List<string>();

        String xmlString = new StreamReader(filepath).ReadToEnd();

        if (id == "")
            id = "default";

        if (xmlString != null)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                reader.ReadToFollowing(id);
                reader.MoveToFirstAttribute();
                length = Convert.ToInt32(reader.Value);
                for (int i = 0; i < length; i++)
                {
                    reader.ReadToFollowing("textblock");
                    reader.MoveToFirstAttribute();
                    if (reader.Value == (i + 1).ToString())
                    {
                        reader.ReadToFollowing("talker");
                        talkerNames.Add(reader.ReadElementContentAsString());
                        reader.ReadToFollowing("sprite");
                        talkerRessources.Add(reader.ReadElementContentAsString());
                        reader.ReadToFollowing(language);
                        dialoge.Add(reader.ReadElementContentAsString());
                    }
                }
            }
            tempdHandler = new DialogeHandler(id, dialoge, talkerNames, talkerRessources);
            return tempdHandler;
        }

        return null;
    }*/

    private IEnumerator LoadDialouge()
    {
        while (!GameObject.FindGameObjectWithTag("EventList").GetComponentInChildren<LanguageReader>().LanguageLoaded)
        {
            yield return null;
        }
        language = GameObject.FindGameObjectWithTag("EventList").GetComponentInChildren<LanguageReader>().Language;
        int length = 0;
        List<string> dialoge = new List<string>();
        List<string> talkerNames = new List<string>();
        List<string> talkerRessources = new List<string>();

        String xmlString = new StreamReader(filepath).ReadToEnd();

        if (xmlString != null)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                int counter = 1;
                reader.ReadToFollowing("dialoge");
                reader.MoveToFirstAttribute();
                string id = reader.Value;
                reader.MoveToNextAttribute();
                while (reader.NodeType != XmlNodeType.None)
                {
                    dialoge = new List<string>();
                    talkerNames = new List<string>();
                    talkerRessources = new List<string>();
                    length = Convert.ToInt32(reader.Value);
                    for (int i = 0; i < length; i++)
                    {
                        reader.ReadToFollowing("textblock");
                        reader.MoveToFirstAttribute();
                        if (reader.Value == (i + 1).ToString())
                        {
                            reader.ReadToFollowing("talker");
                            talkerNames.Add(reader.ReadElementContentAsString());
                            reader.ReadToFollowing("sprite");
                            talkerRessources.Add(reader.ReadElementContentAsString());
                            reader.ReadToFollowing(language);
                            dialoge.Add(reader.ReadElementContentAsString());
                        }
                        yield return null;
                    }
                    print(counter);
                    counter++;
                    CreateGO(id, dialoge, talkerNames, talkerRessources);
                    reader.ReadToFollowing("dialoge");
                    reader.MoveToFirstAttribute();
                    id = reader.Value;
                    reader.MoveToNextAttribute();
                    yield return null;
                }
            }
            LoadedDialoge = true;
        }
    }

    public GameObject GetDialougeHandlerByName(string dHandlerName)
    {
        if (dHandlerDB != null)
        {
            GameObject tempDH = dHandlerDB.Find(h => h.GetComponent<DialogeHandler>().DialougeName == dHandlerName);
            if (tempDH == null)
                return dHandlerDB[0];
            return tempDH;
        }
        return null;
    }

    private void CreateGO(string dialougeName, List<string> dialoge, List<string> talkerNames, List<string> talkerRessources, float textspeed = 0.05f)
    {
        GameObject tmpObj = new GameObject();
        tmpObj.name = dialougeName;
        tmpObj.AddComponent<DialogeHandler>();
        DialogeHandler dHFromGo = tmpObj.GetComponent<DialogeHandler>();

        dHFromGo.Talkers = new List<GameObject>();
        dHFromGo.Dialoge = new List<string>();
        dHFromGo.TalkerNames = new List<string>();
        dHFromGo.TalkersSounds = new List<AudioSource>();

        dHFromGo.Talking = false;
        dHFromGo.Ready = false;

        if (dHFromGo.TextSpeed <= 0)
            dHFromGo.TextSpeed = 0.05f;
        else
            dHFromGo.TextSpeed = textspeed;
        dHFromGo.Dialoge = dialoge;
        dHFromGo.DialougeName = dialougeName;
        dHFromGo.TalkerNames = talkerNames;
        dHFromGo.ResourcesAsString = talkerRessources;
        StartCoroutine("LoadSprites", dHFromGo);
        dHFromGo.Textfield = GameObject.FindGameObjectWithTag("TextFieldUI").GetComponent<Textfield>();

        if (GameObject.FindGameObjectsWithTag("EventList").Length <= 0)
            dHFromGo.Language = "english";
        else
            dHFromGo.Language = GameObject.FindGameObjectWithTag("EventList").GetComponentInChildren<LanguageReader>().Language;

        /*MULTIPLAYER_OWN*/
        dHFromGo.Player = player;
        dHFromGo.Movement = player.GetComponent<Player_Movement>();

        dHandlerDB.Add(tmpObj);
    }

    IEnumerator LoadSprites(DialogeHandler dH)
    {
        foreach (string tR in dH.ResourcesAsString)
        {
            GameObject go = (Resources.Load(tR) as GameObject);
            dH.Talkers.Add(go);
            if (dH.Talkers[dH.Talkers.Count - 1].GetComponentInChildren<AudioSource>() != null)
                dH.TalkersSounds.Add(dH.Talkers[dH.Talkers.Count - 1].GetComponentInChildren<AudioSource>());
            else
                dH.TalkersSounds.Add(null);
            yield return null;
        }
    }
}




// Old Load Script
/*              reader.ReadToFollowing("dialoge");
                reader.MoveToFirstAttribute();
                bool EOF = false;
                while (!EOF)
                {
                    string id = reader.Value;
                    reader.MoveToNextAttribute();
                    length = Convert.ToInt32(reader.Value);
                    int i = 0;
                    yield return null;
                    while (reader.ReadToFollowing("textblock"))
                    {
                        reader.MoveToFirstAttribute();
                        if (reader.Value == (i + 1).ToString())
                        {
                            reader.ReadToFollowing("talker");
                            talkerNames.Add(reader.ReadElementContentAsString());
                            reader.ReadToFollowing("sprite");
                            talkerRessources.Add(reader.ReadElementContentAsString());
                            reader.ReadToFollowing(language);
                            dialoge.Add(reader.ReadElementContentAsString());
                        }
                        i++;
                        yield return null;
                    }
                    print(counter);
                    counter++;
                    CreateGO(id, dialoge, talkerNames, talkerRessources);
                    print(reader.ReadOuterXml());
                    reader.ReadToFollowing("dialoge");
                    if (reader.NodeType == XmlNodeType.None)
                        EOF = true;
                    reader.MoveToFirstAttribute();
                }*/
