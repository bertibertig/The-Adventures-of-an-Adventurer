using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour {

    public event EventHandler PlayerEnter;
    public event EventHandler PlayerExiting;

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            print("Notifieing Subscribers (PlayerAproaching)");
            if (PlayerEnter != null)
                PlayerEnter(this, null);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            print("Notifieing Subscribers (PlayerExiting)");
            if (PlayerExiting != null)
                PlayerExiting(this, null);
        }
    }
}
