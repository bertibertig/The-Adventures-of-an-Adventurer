using UnityEngine;
using System.Collections;
using System;

public class WitchTreeStart : MonoBehaviour {

    public float textSpeed;
    public string language;
    public string[] germanDialoge;
    public string[] englishDialoge;

    private AudioSource player_Talking;
    private Textfield dialoge;
    private Sprite player_Sprite;
    private GameObject player;
    private bool CoRoutineStarted;
    private string[] usedDialoge;
    private Player_Movement movement;

    void Start()
    {
        dialoge = GameObject.FindGameObjectWithTag("TextFieldUI").GetComponent<Textfield>();
        player = GameObject.FindGameObjectWithTag("Player");
        player_Talking = player.GetComponentInChildren<AudioSource>();
        player_Sprite = player.GetComponent<SpriteRenderer>().sprite;
        movement = player.GetComponent<Player_Movement>();
        if (textSpeed <= 0)
            textSpeed = 0.05f;
        CoRoutineStarted = false;
        if (language == "german")
            usedDialoge = germanDialoge;
        else
            usedDialoge = englishDialoge;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !CoRoutineStarted)
        {
            dialoge.Enable();
            CoRoutineStarted = true;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            movement.MovementDisabled = true;
            player.GetComponent<Player_Attack>().enabled = false;
            StartCoroutine("Conversation");
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
        dialoge.Disable();
        player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        movement.MovementDisabled = false;
        player.GetComponent<Player_Attack>().enabled = true;
    }
}
