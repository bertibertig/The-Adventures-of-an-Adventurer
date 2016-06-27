﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health_Controller : MonoBehaviour {

    public float startHealth = 100f;
    private float health = 100f;
    private float maxHealth = 100f;

    public Image healthGUI;
    public Text deathText;

    private bool knockbackEnabled;
    private Animator anim;
    private Player_Movement player;
    private bool isDead = false;
    private bool isInvincible = false;

    public bool KnockbackEnabled { get { return this.knockbackEnabled; } set { this.knockbackEnabled = value; } }
    public float Health { get { return this.health; } }

	void Start () {
        anim = GetComponent<Animator>();
        player = GetComponent<Player_Movement>();
        knockbackEnabled = true;

        if (Application.loadedLevel == 2)
        {
            health = startHealth;
            maxHealth = startHealth;
        }
        else
        {
            health = PlayerPrefs.GetFloat("Health");
            maxHealth = PlayerPrefs.GetFloat("MaxHealth");
        }

        if (GameObject.FindGameObjectsWithTag("UI").Length >= 2)
        {
            Destroy(GameObject.FindGameObjectsWithTag("UI")[1]);
        }
        deathText.enabled = false;
        deathText.text = "";
        UpdateGUI();
	}

    public void ApplyDamage(float damage)
    {
        if (isInvincible == false)
        {
            health = health - damage;
            health = Mathf.Max(health, 0);
            if (isDead == false)
            {
                if (health == 0)
                {
                    isDead = true;
                    Death();
                }
                else
                {
                    Damage();
                }
                isInvincible = true;
                Invoke("ResetIsInvincible", 2);
            }
        }
    }

    void ResetIsInvincible()
    {
        isInvincible = false;
    }

    void Death()
    {
        deathText.enabled = true;
        anim.SetBool("isDying", true);
        player.enabled = false;
        UpdateGUI();
        deathText.text = "You died...";
        //Invoke("Respawn", 1);
        Invoke("Respawn", 5);
    }
		
    void Respawn()
    {
        deathText.enabled = false;
        health = startHealth;
        isDead = false;
        anim.SetBool("isDying", false);
        player.enabled = true;
		UpdateGUI();
        Destroy(GameObject.FindGameObjectWithTag("Music"));
		Application.LoadLevel(0);
        //generate world and reset player
    }

	/*
    void Respawn()
    {
        Application.LoadLevel(0);
    }
	*/

    void Damage()
    {
        if (knockbackEnabled)
            player.StartKnockback(0.02f, 350, player.transform.position);
        UpdateGUI();
    }

    void OnDestroy()
    {
        PlayerPrefs.SetFloat("Health", health);
        PlayerPrefs.SetFloat("MaxHealth", maxHealth);
    }

    void UpdateGUI()
    {
        healthGUI.fillAmount = health / maxHealth;
    }
}
