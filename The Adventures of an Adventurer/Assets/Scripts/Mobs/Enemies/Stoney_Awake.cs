using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stoney_Awake : MonoBehaviour {

    public GameObject Stoney;
    public GameObject Stoney_Cracked;

    private bool axeRemoved;
    private GameObject textfield;
    private GameObject inventory;

	// Use this for initialization
	void Start () {
        axeRemoved = false;
        textfield = GameObject.FindGameObjectWithTag("TextFieldUI");
        inventory = GameObject.FindGameObjectWithTag("InventoryUI");
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SetAxRemovedTrue()
    {
        axeRemoved = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player") && axeRemoved)
        {
            Stoney.GetComponent<SpriteRenderer>().sprite = Stoney_Cracked.GetComponent<SpriteRenderer>().sprite;
            textfield.SetActive(true);
            textfield.GetComponentInChildren<Text>().text = "Stoney: \"Hey!, warte mal\"";
            textfield.GetComponentInChildren<Image>().sprite = Stoney_Cracked.GetComponent<SpriteRenderer>().sprite;
            print("Stoney: \"Hey!, warte mal\"");
        }
    }

    private void StartConversation()
    {

    }
}
