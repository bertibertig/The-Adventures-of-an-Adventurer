using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sign_OnTrigger : MonoBehaviour {

	public GameObject keyInfo;
	public Text text;
	public Image talker;
    public Image textField;

	public string germanText;
	public string englishText;
    public string TEMPLANGUAGE;
    private string usedText;

	private bool displayKeyInfo;
	private bool talking;
    private bool coroutineIsRunning;

    public float textSpeed;

	private Vector2 velocity;

    private GameObject player;

    public GameObject SpriteImage;


    void Start()
	{
		displayKeyInfo = false;
		talking = false;
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if (textSpeed <= 0)
            textSpeed = 0.05f;
		keyInfo.SetActive(false);
        text.enabled = false;
		talker.enabled = false;
        textField.enabled = false;
        coroutineIsRunning = false;
        SetLanguage();
    }

	void Update()
	{
		if (displayKeyInfo)
		{
			if (Input.GetButtonDown ("Interact"))
			{
                if (!talking)
                {
                    talking = true;
                    if(!coroutineIsRunning)
                    StartCoroutine(WriteText());

                    text.enabled = true;
                    talker.enabled = true;
                    textField.enabled = true;
                }

                else
                {
                    DisableText();
                }
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
			if (coroutineIsRunning) {
				
			}
            keyInfo.SetActive(true);
            displayKeyInfo = true;
        }
	}

    void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			keyInfo.SetActive (false);
			DisableText ();
			displayKeyInfo = false;
		}

	}

    void DisableText()
    {
		StopCoroutine(WriteText());
        text.enabled = false;
        talker.enabled = false;
        textField.enabled = false;
        talking = false;
    }

	public void FollowPlayer()
	{
		float posx = player.transform.position.x;
		float posy = player.transform.position.y;

		keyInfo.transform.position = new Vector3(posx, (posy + 2), transform.position.z);
	}

    public void SetLanguage()
    {
        if (TEMPLANGUAGE == "german")
            usedText = germanText;
        else
            usedText = englishText;
    }

    IEnumerator WriteText()
    {
        coroutineIsRunning = true;
        char[] letters = usedText.ToCharArray();
        text.text = "";
        for (int i = 0; i < usedText.Length; i++)
        {
            text.text += letters[i];
            yield return new WaitForSeconds(textSpeed);
        }
		textSpeed = 0.05f;
        coroutineIsRunning = false;
    }
}
