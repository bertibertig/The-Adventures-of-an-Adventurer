using UnityEngine;
using System.Collections;

public class Stoney_Interaction : MonoBehaviour {

    public GameObject keyInfo;
    public GameObject Stoney_Awake;
    public GameObject Stoney;

    private GameObject player;
    private bool displayKeyInfo;
    private bool axeRemovedOnce;
    private Animator anim;
    private EventList eventList;
    private Inventory_Main inventory;


    // Use this for initialization
    void Start()
    {
        axeRemovedOnce = false;
        displayKeyInfo = false;
        eventList = GameObject.FindGameObjectWithTag("EventList").GetComponent<EventList>();
        inventory = GameObject.FindGameObjectWithTag("InventoryUI").GetComponentInChildren<Inventory_Main>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        keyInfo.SetActive(false);
        anim = gameObject.GetComponentInParent<Animator>();
        if (eventList.GetEvent("StoneyTriggered") != null)
        {
            if (eventList.GetEvent("StoneyTriggered").HasHappened)
            {
                axeRemovedOnce = true;
                Stoney_Awake.GetComponent<Stoney_Awake>().ConversatonHappened();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (displayKeyInfo)
        {
            if (Input.GetButtonDown("Interact") && !axeRemovedOnce)
            {
                //Stoney.GetComponent<SpriteRenderer>().sprite = Stoney_without_Axe.GetComponent<SpriteRenderer>().sprite;
                anim.SetBool("axeRemoved", true);
                inventory.AddItem(1);
                Stoney_Awake.GetComponent<Stoney_Awake>().SetAxeRemovedTrue();
                Stoney.GetComponents<PolygonCollider2D>()[0].enabled = false;
                Stoney.GetComponents<PolygonCollider2D>()[1].enabled = true;
                keyInfo.SetActive(false);
                axeRemovedOnce = true;
                eventList.AddEvent("StoneyTriggered", true,"True when the Stoney Cutcene is triggered. Prevents the Stoney Cutcene from appearing multiple times.");
            }

            FollowPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !axeRemovedOnce)
        {
            keyInfo.SetActive(true);
            displayKeyInfo = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !axeRemovedOnce)
        {
            keyInfo.SetActive(false);
            displayKeyInfo = false;
        }
    }

    public void FollowPlayer()
    {
        float posx = player.transform.position.x;
        float posy = player.transform.position.y;

        keyInfo.transform.position = new Vector3(posx, (posy + 2), transform.position.z);
    }
}
