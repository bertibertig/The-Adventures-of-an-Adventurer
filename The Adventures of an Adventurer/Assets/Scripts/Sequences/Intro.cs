﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

    public GameObject presentsText;
    public GameObject logo;
    public string levelToLoad;
    public float textSpeed;
    public string[] germanDialoge;
    public string[] englishDialoge;

    private AudioSource playerTalking;
    private string language;
    private Textfield dialoge;
    private Sprite playerSprite;
    private bool CoRoutineStarted;
    private string[] usedDialoge;
    private Player_Movement movement;
    private static GameObject player;
    public bool IntroEnd { get; set; }

    // Use this for initialization
    void Start()
    {
        dialoge = GameObject.FindGameObjectWithTag("TextFieldUI").GetComponent<Textfield>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialoge.ChangeTalker(player.GetComponent<SpriteRenderer>().sprite);
        playerTalking = player.GetComponentInChildren<AudioSource>();
        playerSprite = player.GetComponent<SpriteRenderer>().sprite;
        movement = player.GetComponent<Player_Movement>();
        if (levelToLoad == null)
            levelToLoad = "0_Level_Tutorial";
        if (textSpeed <= 0)
            textSpeed = 0.05f;
        CoRoutineStarted = false;

        if (GameObject.FindGameObjectsWithTag("EventList").Length <= 0)
            language = "english";
        else
            language = GameObject.FindGameObjectWithTag("EventList").GetComponentInChildren<LanguageReader>().Language;

        if (language == "german")
            usedDialoge = germanDialoge;
        else
            usedDialoge = englishDialoge;


        CoRoutineStarted = true;
        movement.MovementDisabled = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<Player_Attack>().enabled = false;
        dialoge.EnableText();
        player.transform.localRotation = Quaternion.Euler(0, 180, 0);
        StartCoroutine("Conversation");
    }

    // Update is called once per frame
    private IEnumerator Conversation()
    {
        print(dialoge.enabled);
        dialoge.ChangeTalker(playerSprite);
        dialoge.ChangeTalkerName("Adventurer");
        dialoge.PrintText(usedDialoge[0], textSpeed, playerTalking);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        dialoge.StopPrintText();
        StartCoroutine("Logos");
        while (!IntroEnd)
        {
            dialoge.DisableText();
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10f, 0));
            yield return null;
        }


        dialoge.StopPrintText();
        dialoge.DisableText();
        player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        logo.SetActive(false);
        SceneManager.LoadScene(levelToLoad);
    }

    private IEnumerator Logos()
    {
        yield return new WaitForSeconds(2);

        presentsText.SetActive(true);
        yield return new WaitForSeconds(3);
        StartCoroutine("RemoveLogo");
        yield return new WaitForSeconds(3);
        presentsText.SetActive(false);
        logo.SetActive(true);
        yield return new WaitForSeconds(4);
        logo.GetComponent<Animator>().SetBool("Fade", true);
        yield return new WaitForSeconds(0.5f);
        logo.SetActive(false);
    }

    private IEnumerator RemoveLogo()
    {
        while (presentsText.GetComponent<Text>().text.Length > 0)
        {
            presentsText.GetComponent<Text>().text = presentsText.GetComponent<Text>().text.Remove(presentsText.GetComponent<Text>().text.Length - 1);
            yield return new WaitForSeconds(0.08f);
        }
    }
}
