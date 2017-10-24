﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_WitchtreeDefeated : MonoBehaviour {

    public float textSpeed;
    public string[] germanDialoge;
    public string[] englishDialoge;

    Dialoge dialogeHandler;
    GameObject player;
    Textfield textfield;
    int length;

    public void StartSequence()
    {
        //TODO: Rewrite for JSON
        dialogeHandler = new Dialoge(textSpeed, germanDialoge, englishDialoge);
        print(englishDialoge[0]);
        player = GameObject.FindGameObjectWithTag("Player");
        textfield = GameObject.FindGameObjectWithTag("TextFieldUI").GetComponent<Textfield>();
        length = dialogeHandler.UsedDialoge.Length;
        StartCoroutine("WitchTreeDefeatetCutscene");
    }

    IEnumerator WitchTreeDefeatetCutscene()
    {
        dialogeHandler.StartDialoge();
        textfield.ChangeTalker(dialogeHandler.PlayerSprite);
        textfield.ChangeTalkerName("Adventurer");
        
        for(int i = 0; i < length; i++)
        {
            textfield.PrintText(dialogeHandler.UsedDialoge[i], textSpeed, dialogeHandler.Player_Talking);
            yield return new WaitForSeconds(0.1f);
            while (!Input.GetButtonDown("Interact"))
            {
                yield return null;
            }
            textfield.StopPrintText();
            print(i);
        }

        dialogeHandler.EndDialoge();
    }
}
