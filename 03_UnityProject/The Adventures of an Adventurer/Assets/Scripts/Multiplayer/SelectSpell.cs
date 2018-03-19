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
        if (GameObject.FindGameObjectWithTag("Spirit") != null)
        {
            DeselectOldSpell();
            GameObject.FindGameObjectWithTag("Spirit").GetComponent<SpiritMagicController>().SelectedSlot = Spell.GetComponent<Spawn_Obj_OnMouseClick>();
        }
    }

    public void DeselectOldSpell()
    {
        if(GameObject.FindGameObjectWithTag("Spirit") != null)
        {
            SpiritMagicController spiritmagicController = GameObject.FindGameObjectWithTag("Spirit").GetComponent<SpiritMagicController>();
            if (spiritmagicController.SelectedSlot != null)
            {
                spiritmagicController.SelectedSlot.Selected = false;
            }
        }
    }
}
