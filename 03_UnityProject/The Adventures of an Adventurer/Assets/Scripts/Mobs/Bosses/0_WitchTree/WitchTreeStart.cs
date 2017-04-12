using UnityEngine;
using System.Collections;
using System;

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
    private string[] usedDialoge;
    private Player_Movement movement;

	private Throw_On_Target throwScript;
	private Animator squirrelAnim;

    void Start()
    {
        dialoge = GameObject.FindGameObjectWithTag("TextFieldUI").GetComponent<Textfield>();
        player = GameObject.FindGameObjectWithTag("Player");
        player_Talking = player.GetComponentInChildren<AudioSource>();
        player_Sprite = player.GetComponent<SpriteRenderer>().sprite;
        enemy_Sprite = gameObject.GetComponentInParent<SpriteRenderer>().sprite;
        movement = player.GetComponent<Player_Movement>();
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
    }

	void Update()
	{
		if (ConversationEnded && !ThrowCoroutineStarted) {
			StartCoroutine ("ThrowNuts");
			ThrowCoroutineStarted = true;
		}
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
			throwScript.ThrowIsActive = true;
			squirrelAnim.enabled = true;
        }
    }

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag ("Player") && CoRoutineStarted) 
		{
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

        dialoge.PrintText(usedDialoge[1], textSpeed, player_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        dialoge.StopPrintText();

        dialoge.ChangeTalker(enemy_Sprite);
        dialoge.ChangeTalkerName("Legit Witch");
        dialoge.PrintText(usedDialoge[2], textSpeed * 20, player_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        dialoge.StopPrintText();

        dialoge.ChangeTalker(player_Sprite);
        dialoge.ChangeTalkerName("Adventurer");
        dialoge.PrintText(usedDialoge[3], textSpeed, player_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        dialoge.StopPrintText();


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

	public IEnumerator ThrowNuts()
	{
		while (true) {
			yield return new WaitForSeconds (1.65f);
			squirrelAnim.SetBool ("throw", true);

			yield return new WaitForSeconds (0.35f);
			throwScript.ThrowProjectile ();

			yield return new WaitForSeconds (1);
			squirrelAnim.SetBool ("throw", false);
		}
	}
}