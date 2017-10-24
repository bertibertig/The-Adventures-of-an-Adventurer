using UnityEngine;
using System.Collections;

public class Ground_Check : MonoBehaviour {

    public bool Gronded { get; set; }

    void OnTriggerEnter2D(Collider2D col)
    {
        Gronded = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Gronded = false;
    }
}
