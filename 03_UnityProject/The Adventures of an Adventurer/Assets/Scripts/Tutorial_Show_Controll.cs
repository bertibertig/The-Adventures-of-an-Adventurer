using UnityEngine;
using System.Collections;

public class Tutorial_Show_Controll : MonoBehaviour {

    public GameObject Tutorial_Button;

    void Start()
    {
        Tutorial_Button.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            Tutorial_Button.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            Tutorial_Button.SetActive(false);
    }
}
