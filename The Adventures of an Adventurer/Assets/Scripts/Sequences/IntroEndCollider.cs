using UnityEngine;
using System.Collections;

public class IntroEndCollider : MonoBehaviour {

    private Intro intro;

    void Start()
    {
        intro = GameObject.FindGameObjectWithTag("Intro").GetComponent<Intro>();
    }


	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            intro.IntroEnd = true;
        }
    }

}
