﻿using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class WitchTreeStart : MonoBehaviour {

    public float textSpeed;
    public string[] germanDialoge;
    public string[] englishDialoge;

    private string language;
    private AudioSource player_Talking;
    private Textfield dialoge;
    private Sprite player_Sprite;
    private Sprite enemy_Sprite;
    private GameObject player;
    private bool CoRoutineStarted;
	private bool ConversationEnded;
	private bool ThrowCoroutineStarted;
	private bool attackWasActive;
    private bool playerIsInRange;
    private string[] usedDialoge;
    private Player_Movement movement;
    System.Collections.Generic.List<GameObject> birdsWP;

    private Throw_On_Target throwScript;
	private Animator squirrelAnim;

    void Start()
    {
        SearchForGameObjects searchForPlayer = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
        searchForPlayer.PlayerFoundEventHandler += PlayerFound;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player_Talking = player.GetComponentInChildren<AudioSource>();
            player_Sprite = (Resources.Load("Player") as GameObject).GetComponent<SpriteRenderer>().sprite;
            movement = player.GetComponent<Player_Movement>();
        }

        dialoge = GameObject.FindGameObjectWithTag("TextFieldUI").GetComponent<Textfield>();
        enemy_Sprite = gameObject.GetComponentInParent<SpriteRenderer>().sprite;
		ConversationEnded = false;
		ThrowCoroutineStarted = false;
		attackWasActive = false;

		throwScript = transform.parent.gameObject.GetComponentInChildren<Throw_On_Target>();
		squirrelAnim = transform.parent.FindChild ("Squirrel").GetComponent<Animator> ();

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

        birdsWP = GameObject.FindGameObjectsWithTag("Waypoint").Where(x => x.name.Contains("Bird")).ToList();
    }

	void Update()
	{
		if (ConversationEnded && !ThrowCoroutineStarted) {
			StartCoroutine ("BossAttack");
			ThrowCoroutineStarted = true;
		}
	}

    public void PlayerFound(object sender, EventArgs e)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_Talking = player.GetComponentInChildren<AudioSource>();
        player_Sprite = (Resources.Load("Player") as GameObject).GetComponent<SpriteRenderer>().sprite;
        movement = player.GetComponent<Player_Movement>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
			if (!CoRoutineStarted) 
			{
				dialoge.EnableText ();
				CoRoutineStarted = true;
				player.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
				movement.MovementDisabled = true;
				if (player.GetComponent<Player_Attack> ().isActiveAndEnabled) {
					player.GetComponent<Player_Attack> ().enabled = false;
					attackWasActive = true;
				}
				StartCoroutine ("Conversation");
			}
			playerIsInRange = true;
            throwScript.ThrowIsActive = true;
			squirrelAnim.enabled = true;
        }
    }

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag ("Player") && CoRoutineStarted) 
		{
			playerIsInRange = false;
            throwScript.ThrowIsActive = false;
			squirrelAnim.enabled = false;
		}
	}

    private IEnumerator Conversation()
    {
        dialoge.ChangeTalker(player_Sprite);
        dialoge.ChangeTalkerName("Adventurer");
        dialoge.PrintText(usedDialoge[0], textSpeed, player_Talking);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        dialoge.StopPrintText();
        dialoge.PrintWholeText();
        if (!dialoge.FinishedPrintingText)
        {
            yield return new WaitForSeconds(0.1f);
            while (!Input.GetButtonDown("Interact"))
            {
                yield return null;
            }
            dialoge.StopPrintText();
        }

        dialoge.PrintText(usedDialoge[1], textSpeed, player_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        dialoge.StopPrintText();
        dialoge.PrintWholeText();
        if (!dialoge.FinishedPrintingText)
        {
            yield return new WaitForSeconds(0.1f);
            while (!Input.GetButtonDown("Interact"))
            {
                yield return null;
            }
            dialoge.StopPrintText();
        }

        dialoge.ChangeTalker(enemy_Sprite);
        dialoge.ChangeTalkerName("Legit Witch");
        dialoge.PrintText(usedDialoge[2], textSpeed * 20, player_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        dialoge.StopPrintText();
        dialoge.PrintWholeText();
        if (!dialoge.FinishedPrintingText)
        {
            yield return new WaitForSeconds(0.1f);
            while (!Input.GetButtonDown("Interact"))
            {
                yield return null;
            }
            dialoge.StopPrintText();
        }

        dialoge.ChangeTalker(player_Sprite);
        dialoge.ChangeTalkerName("Adventurer");
        dialoge.PrintText(usedDialoge[3], textSpeed, player_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        dialoge.StopPrintText();
        dialoge.PrintWholeText();
        if (!dialoge.FinishedPrintingText)
        {
            yield return new WaitForSeconds(0.1f);
            while (!Input.GetButtonDown("Interact"))
            {
                yield return null;
            }
            dialoge.StopPrintText();
        }


        dialoge.StopPrintText();
        dialoge.DisableText();
        player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        movement.MovementDisabled = false;
		if (attackWasActive) {
			player.GetComponent<Player_Attack> ().enabled = true;
		}

		ConversationEnded = true;
    }

	public IEnumerator BossAttack()
	{
        System.Random rdm = new System.Random();

        int i;
        Spawn_GameObject spawnScript;

        while (true) {
            if (playerIsInRange)
            {
                i = rdm.Next(1, 11);

                Debug.Log(i);

                yield return new WaitForSeconds(1.65f);
                if (i <= 8)
                {
                    squirrelAnim.SetBool("throw", true);

                    yield return new WaitForSeconds(0.35f);
                    throwScript.ThrowProjectile();

                    yield return new WaitForSeconds(1);
                    squirrelAnim.SetBool("throw", false);
                }
                else if (i > 8)
                {
                    foreach (GameObject bird in birdsWP)
                    {
                        spawnScript = bird.transform.Find("WP_A").GetComponent<Spawn_GameObject>();
                        spawnScript.enabled = true;
                        spawnScript.enabled = false;
                    }
                }
            }
            yield return 0;
		}
	}
}