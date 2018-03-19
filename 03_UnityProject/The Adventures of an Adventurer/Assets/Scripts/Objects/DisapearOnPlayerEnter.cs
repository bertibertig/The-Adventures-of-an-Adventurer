using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearOnPlayerEnter : MonoBehaviour {

    public bool fadeInIfPlayerLeaves = true;

    private bool fadeStarted = false;
    private bool fadedIn = true;
    private bool fadeInEnded = true;

    private void Start()
    {
        //StartCoroutine("FadeOut");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !fadeStarted)
        {
            fadeStarted = true;
            StartCoroutine("FadeOut");
        }
            
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && fadeInIfPlayerLeaves && !fadeStarted)
        {
            fadeInEnded = false;
            fadeStarted = true;
            StartCoroutine("FadeIn");
        }
        if (col.CompareTag("Player") && !fadeInEnded)
        {
            StopCoroutine("FadeOut");
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
        fadeStarted = false;
        fadedIn = false;
    }

    IEnumerator FadeIn()
    {
        SpriteRenderer fadeOutObject = this.gameObject.GetComponent<SpriteRenderer>();
        do
        {
            fadeOutObject.color = new Color(fadeOutObject.color.r, fadeOutObject.color.g, fadeOutObject.color.b, fadeOutObject.color.a + 0.01f);
            yield return new WaitForSeconds(0.01f);
        } while (fadeOutObject.color.a < 1);
        fadeStarted = false;
        fadedIn = true;
        fadeInEnded = true;
    }
}
