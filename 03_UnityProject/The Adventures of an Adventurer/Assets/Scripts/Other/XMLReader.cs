﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;

public class XMLReader : MonoBehaviour {

    public string filepath = @"xml\dialoges\0_Tutorial";

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

        GameObject.FindGameObjectWithTag("EventList").GetComponentInChildren<LanguageReader>().LanguageLoadedEventHandler += XMLReader_LanguageLoadedEventHandler;


        //StartCoroutine("LoadDialouge");
    }

    private void XMLReader_LanguageLoadedEventHandler(object sender, EventArgs e)
    {
        language = GameObject.FindGameObjectWithTag("EventList").GetComponentInChildren<LanguageReader>().Language;
        LoadDialouge();
    }

    public void PlayerFound(object sender, EventArgs e)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(!LoadedDialoge)
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
        int length = 0;
        List<string> dialoge = new List<string>();
        List<string> talkerNames = new List<string>();
        List<string> talkerRessources = new List<string>();
        List<float> textspeeds = new List<float>();
        List<string> methodesAfterEnd = new List<string>();

        String xmlString = (Resources.Load(filepath) as TextAsset).text;

        //String xmlString = new StreamReader(filepath).ReadToEnd();

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
                    textspeeds = new List<float>();
                    methodesAfterEnd = new List<string>();
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
                            reader.ReadToFollowing("textspeed");
                            string tmp = reader.ReadElementContentAsString();
                            if (tmp == "0" || tmp == "")
                                textspeeds.Add(0.05f);
                            else
                                textspeeds.Add(Convert.ToSingle(tmp));
                            reader.ReadToFollowing("methodeAfterEnd");
                            methodesAfterEnd.Add(reader.ReadElementContentAsString());
                            reader.ReadToFollowing(language);
                            dialoge.Add(reader.ReadElementContentAsString());
                        }
                        yield return null;
                    }
                    counter++;
                    CreateGO(id, dialoge, talkerNames, talkerRessources, textspeeds, methodesAfterEnd);
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

    private void CreateGO(string dialougeName, List<string> dialoge, List<string> talkerNames, List<string> talkerRessources,List<float> textSpeeds, List<string> methodesAfterEnd ,float defaultTextspeed = 0.05f)
    {
        GameObject tmpObj = new GameObject();
        tmpObj.name = dialougeName;
        tmpObj.AddComponent<DialogeHandler>();
        DialogeHandler dHFromGo = tmpObj.GetComponent<DialogeHandler>();

        dHFromGo.Talkers = new List<GameObject>();
        dHFromGo.Dialoge = new List<string>();
        dHFromGo.TalkerNames = new List<string>();
        dHFromGo.TextSpeeds = new List<float>();
        dHFromGo.TalkersSounds = new List<AudioSource>();
        dHFromGo.MethodesAfterEnd = new List<string>();

        dHFromGo.Talking = false;
        dHFromGo.Ready = false;
        dHFromGo.ConversationFinishedOnce = false;

        if (dHFromGo.DefaultTextSpeed <= 0)
            dHFromGo.DefaultTextSpeed = 0.05f;
        else
            dHFromGo.DefaultTextSpeed = defaultTextspeed;
        dHFromGo.Dialoge = dialoge;
        dHFromGo.DialougeName = dialougeName;
        dHFromGo.TalkerNames = talkerNames;
        dHFromGo.ResourcesAsString = talkerRessources;
        dHFromGo.TextSpeeds = textSpeeds;
        dHFromGo.MethodesAfterEnd = methodesAfterEnd;
        StartCoroutine("LoadRessources", dHFromGo);
        dHFromGo.Textfield = GameObject.FindGameObjectWithTag("TextFieldUI").GetComponent<Textfield>();

        if (GameObject.FindGameObjectsWithTag("EventList").Length <= 0)
            dHFromGo.Language = "english";
        else
            dHFromGo.Language = GameObject.FindGameObjectWithTag("EventList").GetComponentInChildren<LanguageReader>().Language;

        /*MULTIPLAYER_OWN*/
        dHFromGo.Player = player;
        dHFromGo.Movement = player.GetComponent<Player_Movement>();
        tmpObj.AddComponent<DontDestroyOnLoad>();
        tmpObj.transform.parent = this.gameObject.transform;

        dHandlerDB.Add(tmpObj);
    }


    //TODO: Check why Player has no Talkingsound => Because Gameobject does not "exist" (not Inisialised)
    IEnumerator LoadRessources(DialogeHandler dH)
    {
        foreach (string tR in dH.ResourcesAsString)
        {
            GameObject go = (Resources.Load(tR) as GameObject);
            dH.Talkers.Add(go);
            if (dH.Talkers[dH.Talkers.Count - 1].GetComponentInChildren<AudioSource>() != null)
            {
                dH.TalkersSounds.Add(dH.Talkers[dH.Talkers.Count - 1].GetComponentInChildren<AudioSource>());
            }
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
