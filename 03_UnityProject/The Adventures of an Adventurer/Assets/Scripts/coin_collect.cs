using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class coin_collect : MonoBehaviour
{

    private GameObject player;
    private Inventory inventory;
    private float goldCounter;

    public AudioSource audio;

    private Sound_Play sound_player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("player");
        inventory = (Inventory)player.GetComponent(typeof(Inventory));
        sound_player = gameObject.GetComponentInParent<Sound_Play>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            inventory.numberOfGoldCoins++;
            gameObject.SetActive(false);
            sound_player.Audio_play(audio);
        }
    }
}