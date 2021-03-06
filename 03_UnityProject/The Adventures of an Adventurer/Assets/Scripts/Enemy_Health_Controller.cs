﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;

public class Enemy_Health_Controller : MonoBehaviour
{
    public float maxHealth;
    public float minGoldDrop;
    public float maxGoldDrop;
    public float xp;
    private float health;
    private int lastDamageTaken;
    private bool healthIsVisible = false;

    public Image healthGUI;
    public Image healthTransitionLayer;
    public GameObject DamageNumberPrefab;
    public GameObject healthBar;

    private Animator anim;
    private Canvas healthCanvas;
    private DropLoot dropLoot;
    private DropLoot loot;

    void Start()
    {
        if (maxHealth <= 0)
        {
            maxHealth = 100f;
        }
        if (minGoldDrop <= 0)
            minGoldDrop = 2;
        if (maxGoldDrop <= 0)
            maxGoldDrop = 3;
        if (xp <= 0)
            xp = 10;

        SearchForGameObjects searchForPlayer = GameObject.FindGameObjectWithTag("EventList").GetComponent<SearchForGameObjects>();
        searchForPlayer.PlayerFoundEventHandler += PlayerFound;

        /*if (loot == null)
            loot = GameObject.FindGameObjectWithTag("Player").GetComponent<DropLoot>();*/

        healthCanvas = transform.GetComponentInChildren<Canvas>();
        healthCanvas.enabled = false;
        health = maxHealth;
        anim = GetComponent<Animator>();
		if(anim != null)
			anim.SetFloat ("Health", health);
        UpdateGUI();
    }

    private void Update()
    {
        if (health == 0)
        {
            loot.EnemyDropGold(this.gameObject, minGoldDrop, maxGoldDrop);
            Destroy(healthBar);
            Destroy(this.gameObject);
        }
    }

    public void PlayerFound(object sender, EventArgs e)
    {
        loot = GameObject.FindGameObjectWithTag("Player").GetComponent<DropLoot>();
    }

    void ApplyDamage(object[] attackData)
    {
        if(anim != null)
		    anim.SetFloat ("Health", health);
        int damage = Convert.ToInt32(attackData[0]);
        bool isCrit = Convert.ToBoolean(attackData[1]);

        lastDamageTaken = damage;
        if (healthIsVisible == false)
        {
            healthCanvas.enabled = true;
            healthIsVisible = true;
        }
        health = health - damage;
        health = Mathf.Max(health, 0);

        InitDamageNumbers(damage.ToString(), isCrit);

        if (health != 0)
        {
            UpdateGUI();
        }
    }

    void UpdateGUI()
    {
        healthGUI.fillAmount = health / maxHealth;
        StartCoroutine("DamageTransition");
    }

    public float GetHealth()
    {
        return health;
    }

    private IEnumerator DamageTransition()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = lastDamageTaken; i > 0; i--)
        {
            healthTransitionLayer.fillAmount = (health + i) / maxHealth;
            yield return null;
        }
        healthTransitionLayer.fillAmount = health / maxHealth;
    }

    private void InitDamageNumbers(string dmg, bool isCrit)
    {
        GameObject dmgPref = Instantiate(DamageNumberPrefab) as GameObject;
        RectTransform rect = dmgPref.GetComponent<RectTransform>();
        dmgPref.transform.SetParent(transform.FindChild("EnemyCanvas"));
        rect.transform.localPosition = DamageNumberPrefab.transform.localPosition;
        rect.transform.localScale = DamageNumberPrefab.transform.localScale;
        if (isCrit == false)
        {
            dmgPref.GetComponent<Text>().text = dmg;
            dmgPref.GetComponent<Animator>().SetTrigger("Hit");
        }
        else
        {
            dmgPref.GetComponent<Text>().text = dmg + "\nCritical!";
            dmgPref.GetComponent<Animator>().SetTrigger("Crit");
        }
        Destroy(dmgPref.gameObject, 1.5f);
    }

    void InitTextPopup(string txt)
    {
        if (healthIsVisible == false)
        {
            healthCanvas.enabled = true;
            healthIsVisible = true;
        }

        GameObject dmgPref = Instantiate(DamageNumberPrefab) as GameObject;
        RectTransform rect = dmgPref.GetComponent<RectTransform>();
        dmgPref.transform.SetParent(transform.FindChild("EnemyCanvas"));
        rect.transform.localPosition = DamageNumberPrefab.transform.localPosition;
        rect.transform.localScale = DamageNumberPrefab.transform.localScale;

        dmgPref.GetComponent<Text>().text = txt;
        dmgPref.GetComponent<Animator>().SetTrigger("Hit");

        Destroy(dmgPref.gameObject, 1.5f);
    }
}