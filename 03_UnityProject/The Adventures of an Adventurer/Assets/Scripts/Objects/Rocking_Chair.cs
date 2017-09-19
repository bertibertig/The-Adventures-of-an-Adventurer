using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocking_Chair : MonoBehaviour {

    public LevelSelection LvSelection;

    private bool playerEntered = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (LvSelection != null)
        {
            if (col.CompareTag("Player"))
            {
                print("Player entered");
                playerEntered = true;
                StartCoroutine("CheckForInput");
            }
        } 
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerEntered = false;
    }

    IEnumerator CheckForInput()
    {
        while (playerEntered)
        {
            print("Waiting for Input");
            if (Input.GetButtonDown("Interact") && !LvSelection.PlayerSitting)
            {
                LvSelection.RestartScript();
            }
            yield return null;    
        }
    }
}
