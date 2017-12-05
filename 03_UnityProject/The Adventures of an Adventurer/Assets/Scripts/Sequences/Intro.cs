using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public string id;
    public GameObject presentsText;
    public GameObject logo;
    public string levelToLoad;
    public float textSpeed;

    private GameObject player;
    private DialogeHandler dHandler;
    //private GameObject InventoryUI;
    public bool IntroEnd { get; set; }

    // Use this for initialization
    void Start()
    {
        
        if (levelToLoad == null)
            levelToLoad = "0_Level_Tutorial";

        GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>().PlayerFoundEventHandler += Intro_PlayerFoundEventHandler;
        GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>().DialogeDBFoundEventHandler += Intro_DialogeDBFoundEventHandler;

        /*if(GameObject.FindGameObjectsWithTag("InventoryUI").Length >= 0)
            GameObject.FindGameObjectWithTag("InventoryUI").GetComponentInChildren<Inventory_Main>().InventoryDisabled = true;*/


    }

    private void Intro_DialogeDBFoundEventHandler(object sender, System.EventArgs e)
    {
        dHandler = GameObject.FindGameObjectWithTag("DialogesDB").GetComponent<XMLReader>().GetDialougeHandlerByName(id).GetComponent<DialogeHandler>();
        dHandler.DialogeEndedEventHandler += DHandler_DialogeEndedEventHandler;
        dHandler.StartConversation();
    }

    private void DHandler_DialogeEndedEventHandler(object sender, System.EventArgs e)
    {
        StartCoroutine("DisplayLogosAndWalkTotrigger");
    }

    private void Intro_PlayerFoundEventHandler(object sender, System.EventArgs e)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player_Movement>().MovementDisabled = true;
    }

    // Update is called once per frame
    private IEnumerator DisplayLogosAndWalkTotrigger()
    {
        StartCoroutine("Logos");
        while (!IntroEnd)
        {
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50f, 0));
            yield return null;
        }

        logo.SetActive(false);
    }

    private IEnumerator Logos()
    {
        yield return new WaitForSeconds(2);

        presentsText.SetActive(true);
        yield return new WaitForSeconds(3);
        StartCoroutine("RemoveLogo");
        yield return new WaitForSeconds(3);
        presentsText.SetActive(false);
        logo.SetActive(true);
        yield return new WaitForSeconds(4);
        logo.GetComponent<Animator>().SetBool("Fade", true);
        yield return new WaitForSeconds(0.5f);
        logo.SetActive(false);
    }

    private IEnumerator RemoveLogo()
    {
        while (presentsText.GetComponent<Text>().text.Length > 0)
        {
            presentsText.GetComponent<Text>().text = presentsText.GetComponent<Text>().text.Remove(presentsText.GetComponent<Text>().text.Length - 1);
            yield return new WaitForSeconds(0.08f);
        }
    }
}
