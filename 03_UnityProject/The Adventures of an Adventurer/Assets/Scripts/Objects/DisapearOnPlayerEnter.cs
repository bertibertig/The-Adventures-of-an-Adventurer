using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearOnPlayerEnter : MonoBehaviour {

    public bool fadeInIfPlayerLeaves = true;

    private bool fadeInStarted = false;

    private void Start()
    {
        //StartCoroutine("FadeOut");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !fadeInStarted)
            StartCoroutine("FadeOut");
            
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && fadeInIfPlayerLeaves && !fadeInStarted)
        {
            fadeInStarted = true;
            StartCoroutine("FadeIn");
        }
    }

    IEnumerator FadeOut()
    {
        SpriteRenderer fadeOutObject = this.gameObject.GetComponent<SpriteRenderer>();
        do
        {
            fadeOutObject.color = new Color(fadeOutObject.color.r, fadeOutObject.color.g, fadeOutObject.color.b, fadeOutObject.color.a - 0.01f);
            yield return new WaitForSeconds(0.01f);
        } while (fadeOutObject.color.a > 0);
    }

    IEnumerator FadeIn()
    {
        SpriteRenderer fadeOutObject = this.gameObject.GetComponent<SpriteRenderer>();
        do
        {
            fadeOutObject.color = new Color(fadeOutObject.color.r, fadeOutObject.color.g, fadeOutObject.color.b, fadeOutObject.color.a + 0.01f);
            print(fadeOutObject.color.a);
            yield return new WaitForSeconds(0.01f);
        } while (fadeOutObject.color.a >= 1);
        fadeInStarted = false;
    }
}
