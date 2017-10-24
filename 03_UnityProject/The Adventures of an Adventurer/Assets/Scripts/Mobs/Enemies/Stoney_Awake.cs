using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stoney_Awake : MonoBehaviour {

    public GameObject Stoney;
    public GameObject Stoney_Cracked;
    public GameObject Stoney_Silhouette;
    public AudioSource Stoney_HitSound;
    public AudioSource Stoney_Talking;
    public float textSpeed;
    public string[] germanDialoge;
    public string[] englishDialoge;

    private string[] usedDialoge;
    private string language;
    private bool axeRemoved;
    private bool conversatonHappened;
    private Textfield textfield;
    private GameObject inventory;
    private GameObject player;
    private Sprite playerSprite;
    private Player_Movement movement;
    private Animator anim;
    private AudioSource player_Talking;
    private bool conversationStarted = false;

    public bool GetAxeRemoved { get { return this.axeRemoved; } }

	// Use this for initialization
	void Start () {
        if (textSpeed <= 0)
            textSpeed = 0.05f;
        axeRemoved = false;
        conversatonHappened = false;
        textfield = GameObject.FindGameObjectWithTag("TextFieldUI").GetComponent<Textfield>();
        inventory = GameObject.FindGameObjectWithTag("InventoryUI");
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<Player_Movement>();
        anim = Stoney.GetComponent<Animator>();
        player_Talking = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AudioSource>();
        playerSprite = (Resources.Load("Player") as GameObject).GetComponent<SpriteRenderer>().sprite;

        if (GameObject.FindGameObjectsWithTag("EventList").Length <= 0)
            language = "english";
        else
            language = GameObject.FindGameObjectWithTag("EventList").GetComponentInChildren<LanguageReader>().Language;

        if (language == "german")
            usedDialoge = germanDialoge;
        else
            usedDialoge = englishDialoge;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SetAxeRemovedTrue()
    {
        axeRemoved = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player") && axeRemoved && !conversatonHappened && !conversationStarted)
        {
            anim.SetBool("getCracked", true);
            StartConversation();
        }
    }

    public void ConversatonHappened()
    {
        conversatonHappened = true;
        anim.SetBool("AlreadyHappened", true);
        axeRemoved = true;
        Stoney.GetComponents<PolygonCollider2D>()[0].enabled = false;
        Stoney.GetComponents<PolygonCollider2D>()[1].enabled = true;
        Stoney.transform.localPosition = new Vector3(1.077963f, 0.7597189f, 0);

    }

    private void StartConversation()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        movement.MovementDisabled = true;
        player.GetComponent<Player_Attack>().enabled = false;
        StartCoroutine("Conversation");
    }

    private IEnumerator Conversation()
    {
        conversationStarted = true;
        textfield.ChangeTalker(Stoney_Silhouette.GetComponent<SpriteRenderer>().sprite);
        textfield.ChangeTalkerName("???");
        textfield.EnableText();
        textfield.PrintText(usedDialoge[0], textSpeed, Stoney_Talking);
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
        textfield.DisableText();
        yield return new WaitForSeconds(1);
        if (player.transform.localRotation == Quaternion.Euler(0, 0, 0))
        {
            player.transform.localRotation = Quaternion.Euler(0, 180, 0);
            yield return new WaitForSeconds(0.7f);
            player.transform.localRotation = Quaternion.Euler(0, 0, 0);
            yield return new WaitForSeconds(0.7f);
            player.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            player.transform.localRotation = Quaternion.Euler(0, 180, 0);
            yield return new WaitForSeconds(0.7f);
            player.transform.localRotation = Quaternion.Euler(0, 0, 0);
            yield return new WaitForSeconds(0.7f);
            player.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        textfield.ChangeTalker(playerSprite);
        textfield.ChangeTalkerName("Adventurer");
        textfield.EnableText();
        textfield.PrintText(usedDialoge[1], textSpeed, player_Talking);
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
        textfield.DisableText();
        textfield.ChangeTalker(Stoney_Cracked.GetComponent<SpriteRenderer>().sprite);
        textfield.ChangeTalkerName("Stoney");
        textfield.EnableText();
        textfield.PrintText(usedDialoge[2], textSpeed, Stoney_Talking);
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
        player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        textfield.DisableText();
        anim.SetBool("awake", true);
        textfield.StopPrintText();
        yield return new WaitForSeconds(2.3f);
        textfield.EnableText();
        textfield.ChangeTalkerName("Exklalibul");
        textfield.PrintText(usedDialoge[3], textSpeed, Stoney_Talking);
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
        textfield.ChangeTalker(playerSprite);
        textfield.ChangeTalkerName("Adventurer");
        textfield.PrintText(usedDialoge[4], textSpeed, player_Talking);
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
        Camera.main.gameObject.AddComponent<CameraShake>();
        Camera.main.gameObject.GetComponent<CameraShake>().StartToShake(0.7f, 0.7f, 0.5f);
        textfield.ChangeTalker(Stoney_Cracked.GetComponent<SpriteRenderer>().sprite);
        textfield.ChangeTalkerName("Exklalibul");
        textfield.PrintText(usedDialoge[5], textSpeed / 2, Stoney_Talking);
        yield return new WaitForSeconds(0.7f);
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
        textfield.PrintText(usedDialoge[6], textSpeed, Stoney_Talking);
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
        textfield.PrintText(usedDialoge[7], textSpeed, Stoney_Talking);
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
        textfield.ChangeTalker(playerSprite);
        textfield.ChangeTalkerName("Adventurer");
        textfield.PrintText(usedDialoge[8], textSpeed, player_Talking);
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
        textfield.ChangeTalker(Stoney_Cracked.GetComponent<SpriteRenderer>().sprite);
        textfield.ChangeTalkerName("Exklalibul");
        textfield.PrintText(usedDialoge[9], textSpeed, Stoney_Talking);
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
        textfield.DisableText();
        yield return new WaitForSeconds(1);
        Stoney.gameObject.AddComponent<Rigidbody2D>();
        Stoney.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50f,0));
        yield return new WaitForSeconds(1);
        Stoney.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50f, 0));
        yield return new WaitForSeconds(1);
        Destroy(Stoney.GetComponent<Rigidbody2D>());
        textfield.EnableText();
        textfield.PrintText(usedDialoge[10], textSpeed, Stoney_Talking);
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
        textfield.DisableText();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(75f, 0));
        yield return new WaitForSeconds(1);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        textfield.EnableText();
        textfield.PrintText(usedDialoge[11], textSpeed, Stoney_Talking);
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
        textfield.DisableText();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(100f, 0));
        yield return new WaitForSeconds(1);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        textfield.EnableText();
        textfield.PrintText(usedDialoge[12], textSpeed, Stoney_Talking);
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
        textfield.DisableText();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(125f, 0));
        yield return new WaitForSeconds(1);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        textfield.EnableText();
        textfield.PrintText(usedDialoge[13], textSpeed, Stoney_Talking);
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
        textfield.DisableText();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(150f, 0));
        yield return new WaitForSeconds(2);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        textfield.EnableText();
        textfield.PrintText(usedDialoge[14], textSpeed, Stoney_Talking);
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
        textfield.DisableText();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Stoney.gameObject.AddComponent<Rigidbody2D>();
        Stoney.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50f, 0));
        Stoney_HitSound.Play();
        yield return new WaitForSeconds(0.2f);
        Destroy(Stoney.GetComponent<Rigidbody2D>());
        player.GetComponent<Health_Controller>().KnockbackEnabled = false;

        float dmg = 10f;
        float plHealth = player.GetComponent<Health_Controller>().GetHealth;
        if (plHealth <= 10)
            dmg = plHealth - 1;
        player.GetComponent<Health_Controller>().ApplyDamage(dmg);

        yield return new WaitForSeconds(0.1f);
        player.GetComponent<Health_Controller>().KnockbackEnabled = true;
        yield return new WaitForSeconds(3);
        textfield.EnableText();
        textfield.PrintText("...", textSpeed * 20, Stoney_Talking);
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
        textfield.PrintText(usedDialoge[15], textSpeed, Stoney_Talking);
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
        textfield.PrintText(usedDialoge[16], textSpeed, Stoney_Talking);
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
        textfield.PrintText(usedDialoge[17], textSpeed, Stoney_Talking);
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
        conversatonHappened = true;
        anim.SetBool("conversationEnded", conversatonHappened);

        textfield.StopPrintText();
        textfield.DisableText();
        player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        movement.MovementDisabled = false;
        player.GetComponent<Player_Attack>().enabled = true;
    }
}
