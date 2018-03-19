using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer_OnActivate : MonoBehaviour {

    private Health_Controller playerHealthController;

    public int healingToDo = 20;

	// Use this for initialization
	void Awake () {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
            playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<Health_Controller>();
	}

    private void OnEnable()
    {
        if (!playerHealthController.IsAtFullHealth())
            playerHealthController.Heal(healingToDo);
    }
}
