using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSpell : MonoBehaviour {

    public GameObject Spell;

	// Use this for initialization
	void Start () {
		
	}
	
	public void SpellSelected()
    {
        if(Spell != null && Spell.GetComponent<Spawn_Obj_OnMouseClick>() != null)
            Spell.GetComponent<Spawn_Obj_OnMouseClick>().Selected = true;
    }
}
