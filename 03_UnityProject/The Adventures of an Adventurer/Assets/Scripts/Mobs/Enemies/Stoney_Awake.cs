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
    private GameObject textfield;
    private GameObject inventory;
    private GameObject player;
    private Player_Movement movement;
    private Animator anim;
    private AudioSource player_Talking;

    public bool GetAxeRemoved { get { return this.axeRemoved; } }

	// Use this for initialization
	void Start () {
        if (textSpeed <= 0)
            textSpeed = 0.05f;
        axeRemoved = false;
        conversatonHappened = false;
        textfield = GameObject.FindGameObjectWithTag("TextFieldUI");
        inventory = GameObject.FindGameObjectWithTag("InventoryUI");
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<Player_Movement>();
        anim = Stoney.GetComponent<Animator>();
        player_Talking = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AudioSource>();

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
        if(col.CompareTag("Player") && axeRemoved && !conversatonHappened)
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
        textfield.GetComponent<Textfield>().ChangeTalker(Stoney_Silhouette.GetComponent<SpriteRenderer>().sprite);
        textfield.GetComponent<Textfield>().ChangeTalkerName("???");
        textfield.GetComponent<Textfield>().EnableText();
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[0], textSpeed, Stoney_Talking);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().DisableText();
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
        textfield.GetComponent<Textfield>().ChangeTalker(player.GetComponent<SpriteRenderer>().sprite);
        textfield.GetComponent<Textfield>().ChangeTalkerName("Adventurer");
        textfield.GetComponent<Textfield>().EnableText();
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[1], textSpeed, player_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().DisableText();
        textfield.GetComponent<Textfield>().ChangeTalker(Stoney_Cracked.GetComponent<SpriteRenderer>().sprite);
        textfield.GetComponent<Textfield>().ChangeTalkerName("Stoney");
        textfield.GetComponent<Textfield>().EnableText();
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[2], textSpeed, Stoney_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        textfield.GetComponent<Textfield>().DisableText();
        anim.SetBool("awake", true);
        textfield.GetComponent<Textfield>().StopPrintText();
        yield return new WaitForSeconds(2.3f);
        textfield.GetComponent<Textfield>().EnableText();
        textfield.GetComponent<Textfield>().ChangeTalkerName("Exklalibul");
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[3], textSpeed, Stoney_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().ChangeTalker(player.GetComponent<SpriteRenderer>().sprite);
        textfield.GetComponent<Textfield>().ChangeTalkerName("Adventurer");
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[4], textSpeed, player_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        Camera.main.gameObject.AddComponent<CameraShake>();
        Camera.main.gameObject.GetComponent<CameraShake>().StartToShake(0.7f, 0.7f, 0.5f);
        textfield.GetComponent<Textfield>().ChangeTalker(Stoney_Cracked.GetComponent<SpriteRenderer>().sprite);
        textfield.GetComponent<Textfield>().ChangeTalkerName("Exklalibul");
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[5], textSpeed / 2, Stoney_Talking);
        yield return new WaitForSeconds(0.7f);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[6], textSpeed, Stoney_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[7], textSpeed, Stoney_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().ChangeTalker(player.GetComponent<SpriteRenderer>().sprite);
        textfield.GetComponent<Textfield>().ChangeTalkerName("Adventurer");
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[8], textSpeed, player_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().ChangeTalker(Stoney_Cracked.GetComponent<SpriteRenderer>().sprite);
        textfield.GetComponent<Textfield>().ChangeTalkerName("Exklalibul");
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[9], textSpeed, Stoney_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().DisableText();
        yield return new WaitForSeconds(1);
        Stoney.gameObject.AddComponent<Rigidbody2D>();
        Stoney.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50f,0));
        yield return new WaitForSeconds(1);
        Stoney.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50f, 0));
        yield return new WaitForSeconds(1);
        Destroy(Stoney.GetComponent<Rigidbody2D>());
        textfield.GetComponent<Textfield>().EnableText();
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[10], textSpeed, Stoney_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().DisableText();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(75f, 0));
        yield return new WaitForSeconds(1);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        textfield.GetComponent<Textfield>().EnableText();
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[11], textSpeed, Stoney_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().DisableText();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(100f, 0));
        yield return new WaitForSeconds(1);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        textfield.GetComponent<Textfield>().EnableText();
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[12], textSpeed, Stoney_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().DisableText();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(125f, 0));
        yield return new WaitForSeconds(1);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        textfield.GetComponent<Textfield>().EnableText();
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[13], textSpeed, Stoney_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().DisableText();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(150f, 0));
        yield return new WaitForSeconds(2);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        textfield.GetComponent<Textfield>().EnableText();
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[14], textSpeed, Stoney_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().DisableText();
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
        textfield.GetComponent<Textfield>().EnableText();
        textfield.GetComponent<Textfield>().PrintText("...", textSpeed * 20, Stoney_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[15], textSpeed, Stoney_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[16], textSpeed, Stoney_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().PrintText(usedDialoge[17], textSpeed, Stoney_Talking);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        conversatonHappened = true;
        anim.SetBool("conversationEnded", conversatonHappened);

        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().DisableText();
        player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        movement.MovementDisabled = false;
        player.GetComponent<Player_Attack>().enabled = true;
    }
}
