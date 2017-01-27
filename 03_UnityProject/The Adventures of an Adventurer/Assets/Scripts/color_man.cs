using UnityEngine;
using System.Collections;

public class color_man : MonoBehaviour {

    public float textSpeed;
    public int[] idOfItems;
    public string[] germanDialoge;
    public string[] englishDialoge;

    private string language;
    private string[] usedDialoge;
    private Textfield textfield;
    private GameObject inventory;
    private GameObject player;
    private Player_Movement movement;
    private DisplayKeyInfo displayKeyInfo;
    private MerchantGUI merchantGUI;
    private bool talking;

    // Use this for initialization
    void Start () {
        if (textSpeed <= 0)
            textSpeed = 0.05f;
        textfield = GameObject.FindGameObjectWithTag("TextFieldUI").GetComponent<Textfield>();
        inventory = GameObject.FindGameObjectWithTag("InventoryUI");
        player = GameObject.FindGameObjectWithTag("Player");
        merchantGUI = GameObject.FindGameObjectWithTag("MerchantUI").GetComponentInChildren<MerchantGUI>();
        displayKeyInfo = player.GetComponent<DisplayKeyInfo>();
        movement = player.GetComponent<Player_Movement>();
        talking = false;

        if (GameObject.FindGameObjectsWithTag("EventList").Length <= 0)
            language = "english";
        else
            language = GameObject.FindGameObjectWithTag("EventList").GetComponentInChildren<LanguageReader>().Language;

        if (language == "german")
            usedDialoge = germanDialoge;
        else
            usedDialoge = englishDialoge;
        merchantGUI.ClearMerchantInventory();
        merchantGUI.AddNewItemsToMerchantInventoryByIdArray(idOfItems);
    }

    void Update()
    {
        if (displayKeyInfo)
        {
            if (Input.GetButtonDown("Interact") && !talking)
            {
                StartConversation();
            }

            displayKeyInfo.FollowPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            displayKeyInfo.Enable();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            displayKeyInfo.Disable();
        }
    }

    private void StartConversation()
    {
        talking = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        movement.MovementDisabled = true;
        player.GetComponent<Player_Attack>().enabled = false;
        displayKeyInfo.Disable();
        StartCoroutine("Conversation");
    }

    private IEnumerator Conversation()
    {
        textfield.ChangeTalker((Resources.Load("NPCs/Portraits/ColorMan", typeof(GameObject)) as GameObject).GetComponent<SpriteRenderer>().sprite);
        textfield.ChangeTalkerName("Robin Loot");
        textfield.EnableText();
        textfield.PrintText(usedDialoge[0], textSpeed);
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        textfield.GetComponent<Textfield>().StopPrintText();
        textfield.GetComponent<Textfield>().DisableText();

        textfield.SetChoices(usedDialoge[1], usedDialoge[2], usedDialoge[3]);
        textfield.EnableChoise();
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            yield return null;
        }
        ChoiceInterpreter01();
        //EndConversation();
    }

    private void EndConversation()
    {
        textfield.DisableChoise();
        textfield.DisableText();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        movement.MovementDisabled = false;
        player.GetComponent<Player_Attack>().enabled = true;
        displayKeyInfo.Enable();
        talking = false;
    }

    private void ChoiceInterpreter01()
    {
        int choice = textfield.GetCurrentChoicePosition;
        if (choice == 1)
            StartCoroutine("OpenMerchantGUI");
        if (choice == 2)
            EndConversation();
    }

    private IEnumerator OpenMerchantGUI()
    {
        textfield.DisableChoise();
        textfield.DisableText();
        merchantGUI.OpenMechantUI();
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Tab"))
        {
            yield return null;
        }
        merchantGUI.CloseMechantUI();
        EndConversation();
    }
}
