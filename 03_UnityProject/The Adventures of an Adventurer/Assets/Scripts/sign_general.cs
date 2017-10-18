using UnityEngine;
using System.Collections;
using System;

public class sign_general : MonoBehaviour {

    public GameObject keyInfo;
    public string id = "default";

    private bool displayKeyInfo;
    private GameObject player;
    private Player_Movement movement;
    private DialogeHandler dHandler;

    // Use this for initialization
    void Start () {
        displayKeyInfo = false;
        SearchForGameObjects searchForPlayer = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
        searchForPlayer.PlayerFoundEventHandler += PlayerFound;

        if (player == null || movement == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            movement = player.GetComponent<Player_Movement>();
        }
        
    }

    void Update()
    {
        if(GameObject.FindGameObjectWithTag("DialogesDB").GetComponent<XMLReader>().LoadedDialoge && dHandler == null)
        {
            dHandler = GameObject.FindGameObjectWithTag("DialogesDB").GetComponent<XMLReader>().GetDialougeHandlerByName(id).GetComponent<DialogeHandler>();
        }

        if (displayKeyInfo)
        {
            if (Input.GetButtonDown("Interact"))
            {
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                movement.MovementDisabled = true;
                player.GetComponent<Player_Attack>().enabled = false;
                print("talking");
                dHandler.StartConversation();
            }
            FollowPlayer();
        }
    }
	
	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            keyInfo.SetActive(true);
            displayKeyInfo = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            keyInfo.SetActive(false);
            displayKeyInfo = false;
        }

    }

    public void PlayerFound(object sender, EventArgs e)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<Player_Movement>();
    }

    /*private IEnumerator Read()
    {
        textfield.ChangeTalker(this.gameObject.GetComponentInParent<SpriteRenderer>().sprite);
        textfield.ChangeTalkerName(talker);
        textfield.EnableText();
        print("CoRoutine started");
        for(int i = 0; i < usedDialoge.Length; i++)
        {
            textfield.PrintText(usedDialoge[i], textSpeed);
            yield return new WaitForSeconds(0.1f);
            while (!Input.GetButtonDown("Interact"))
            {
                yield return null;
            }
            textfield.StopPrintText();
            textfield.PrintWholeText();
            if (!textfield.FinishedPrintingText)
            {
                yield return new WaitForSeconds(0.1f);
                while (!Input.GetButtonDown("Interact"))
                {
                    yield return null;
                }
                textfield.StopPrintText();
            }
            
        }

        textfield.GetComponent<Textfield>().DisableText();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        movement.MovementDisabled = false;
        player.GetComponent<Player_Attack>().enabled = true;
        talking = false;
    }*/

    public void FollowPlayer()
    {
        float posx = player.transform.position.x;
        float posy = player.transform.position.y;

        keyInfo.transform.position = new Vector3(posx, (posy + 2), transform.position.z);
    }
}
