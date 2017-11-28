using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class WitchTreeStart : MonoBehaviour
{

    public string id = "WitchTree";

    private GameObject player;
    private bool ThrowCoroutineStarted;

    private DialogeHandler dHandler;

    private bool playerIsInRange;
    private Player_Movement movement;
    System.Collections.Generic.List<GameObject> birdsWP;

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
        squirrelAnim = transform.parent.FindChild("Squirrel").GetComponent<Animator>();
        birdsWP = GameObject.FindGameObjectsWithTag("Waypoint").Where(x => x.name.Contains("Bird")).ToList();
    }

    void Update()
    {
        if (dHandler != null && dHandler.Talking == false && dHandler.ConversationFinishedOnce == true && !ThrowCoroutineStarted)
        {
            StartCoroutine("BossAttack");
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
        if (col.CompareTag("Player"))
        {
            if(!dHandler.ConversationFinishedOnce)
                dHandler.StartConversation();
            playerIsInRange = true;
            throwScript.ThrowIsActive = true;
            squirrelAnim.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerIsInRange = false;
            throwScript.ThrowIsActive = false;
            squirrelAnim.enabled = false;
        }
    }

    public IEnumerator BossAttack()
    {
        System.Random rdm = new System.Random();

        int i;
        Spawn_GameObject spawnScript;

        while (true)
        {
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
                        yield return null;
                    }
                }
            }
            yield return 0;
        }
    }
}
