using UnityEngine;
using System.Collections;
using System;

public class WitchTreeStart : MonoBehaviour {

    public string id = "WitchTree";

    private GameObject player;
	private bool ThrowCoroutineStarted;
    private bool dialougeStarted;
    private Player_Movement movement;
    private DialogeHandler dHandler;

	private Throw_On_Target throwScript;
	private Animator squirrelAnim;

    void Start()
    {
        SearchForGameObjects searchForPlayer = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
        searchForPlayer.PlayerFoundEventHandler += PlayerFound;

        SearchForGameObjects searchForDialogeDB = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
        searchForDialogeDB.DialogeDBFoundEventHandler += DialogeDBFound;

        /*MULTIPLAYER_OWN
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player_Talking = player.GetComponentInChildren<AudioSource>();
            player_Sprite = (Resources.Load("Player") as GameObject).GetComponent<SpriteRenderer>().sprite;
            movement = player.GetComponent<Player_Movement>();
        }*/
        ThrowCoroutineStarted = false;

        throwScript = transform.parent.gameObject.GetComponentInChildren<Throw_On_Target>();
		squirrelAnim = transform.parent.FindChild ("Squirrel").GetComponent<Animator> ();
    }

	void Update()
	{
        if (dHandler != null && dHandler.Talking == false && dHandler.ConversationFinishedOnce == true && !ThrowCoroutineStarted)
        {
			StartCoroutine ("ThrowNuts");
			ThrowCoroutineStarted = true;
		}
	}

    public void PlayerFound(object sender, EventArgs e)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<Player_Movement>();
    }

    void DialogeDBFound(object sender, EventArgs e)
    {
        dHandler = GameObject.FindGameObjectWithTag("DialogesDB").GetComponent<XMLReader>().GetDialougeHandlerByName(id).GetComponent<DialogeHandler>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !dHandler.ConversationFinishedOnce)
        {
            dHandler.StartConversation();
        }
        throwScript.ThrowIsActive = true;
        squirrelAnim.enabled = true;
    }

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) 
		{
            throwScript.ThrowIsActive = false;
			squirrelAnim.enabled = false;
		}
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
 