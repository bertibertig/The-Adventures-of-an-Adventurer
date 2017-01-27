using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SignOnTrigger : MonoBehaviour {

    public string germanText;
    public string englishText;
    public string buttonToPress;
    public string language;
    public Text textForDialog;
    public Text keyInfo;
    public GameObject gameObject;
    private float x;
    private float y;
    private float z;

	// Use this for initialization
	void Start () {
        keyInfo.enabled = false;
        textForDialog.enabled = false;

        keyInfo.text = buttonToPress;

        if (language == "German")
        {
            textForDialog.text = germanText;
        }
        if (language == "English")
        {
            textForDialog.text = englishText;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (keyInfo.enabled)
        {
            x = gameObject.transform.position.x;
            y = gameObject.transform.position.y;
            z = gameObject.transform.position.z;
            keyInfo.rectTransform.position = new Vector3((x + 2), (y + 2), z);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            keyInfo.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            keyInfo.enabled = false;
        }
    }
}
