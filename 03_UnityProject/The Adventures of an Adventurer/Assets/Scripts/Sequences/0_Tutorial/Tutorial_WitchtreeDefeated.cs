using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_WitchtreeDefeated : MonoBehaviour {

    public string id;

    DialogeHandler dHandler;

    public void StartSequence()
    {
        dHandler = GameObject.FindGameObjectWithTag("DialogesDB").GetComponent<XMLReader>().GetDialougeHandlerByName(id).GetComponent<DialogeHandler>();
        dHandler.StartConversation();
    }
}
