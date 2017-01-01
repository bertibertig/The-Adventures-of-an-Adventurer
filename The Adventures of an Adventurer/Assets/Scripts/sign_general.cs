using UnityEngine;
using System.Collections;

public class sign_general : MonoBehaviour {

    public float textSpeed;
    public string language;
    public GameObject keyInfo;
    public string[] germanDialoge;
    public string[] englishDialoge;

    private bool displayKeyInfo;
    private bool talking;
    private string talker;
    private string[] usedDialoge;
    private Textfield textfield;
    private GameObject player;
    private Player_Movement movement;
    private AudioSource signSound;

    // Use this for initialization
    void Start () {
        displayKeyInfo = false;
        talking = false;
        if (textSpeed <= 0)
            textSpeed = 0.05f;
        textfield = GameObject.FindGameObjectWithTag("TextFieldUI").GetComponent<Textfield>();
        textfield.DisableText();
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<Player_Movement>();

        if (language == "german")
        {
            usedDialoge = germanDialoge;
            talker = "Schild";
        }
        else
        {
            usedDialoge = englishDialoge;
            talker = "sign";
        }
    }

    void Update()
    {
        if (displayKeyInfo)
        {
            if (Input.GetButtonDown("Interact") && !talking)
            {
                talking = true;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                movement.MovementDisabled = true;
                player.GetComponent<Player_Attack>().enabled = false;
                print("talking");
                StartCoroutine("Read");
            }

            if (Input.GetButtonDown("Submit") && talking)
            {
                textSpeed = 0.01f;
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

    private IEnumerator Read()
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
            
        }

        textfield.GetComponent<Textfield>().DisableText();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        movement.MovementDisabled = false;
        player.GetComponent<Player_Attack>().enabled = true;
        talking = false;
    }

    public void FollowPlayer()
    {
        float posx = player.transform.position.x;
        float posy = player.transform.position.y;

        keyInfo.transform.position = new Vector3(posx, (posy + 2), transform.position.z);
    }
}
