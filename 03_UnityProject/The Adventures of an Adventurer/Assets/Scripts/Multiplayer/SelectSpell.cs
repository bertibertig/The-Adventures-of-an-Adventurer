using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectSpell : MonoBehaviour {

    public GameObject Spell;
    [Header("If spell GO can't be selected at start (Name of the Spell GO)")]
    public string spellName;
    public bool unused = true;

    private void Start()
    {
        if (spellName == "ReForma")
            StartCoroutine("SearchForReforma");
        if (spellName != null && Spell == null && !unused)
            StartCoroutine("SearchForSpellCoRoutine");
    }

    public void SpellSelected()
    {
        if(Spell != null && Spell.GetComponent<Spawn_Obj_OnMouseClick>() != null)
            Spell.GetComponent<Spawn_Obj_OnMouseClick>().Selected = true;
        if (GameObject.FindGameObjectWithTag("Spirit") != null)
        {
            DeselectOldSpell();
            if (Spell.GetComponent<Spawn_Obj_OnMouseClick>() != null)
                GameObject.FindGameObjectWithTag("Spirit").GetComponent<SpiritMagicController>().SelectedSlot = Spell;       
        }
    }

    public void ReFormaSelect()
    {
        DeselectOldSpell();
        GameObject.FindGameObjectWithTag("Spirit").GetComponent<ReFormaSpell>().Selected = true;
        GameObject.FindGameObjectWithTag("Spirit").GetComponent<SpiritMagicController>().SelectedSlot = Spell;
    }

    public void DeselectOldSpell()
    {
        GameObject SelectedSlot = GameObject.FindGameObjectWithTag("Spirit").GetComponent<SpiritMagicController>().SelectedSlot;
        if (GameObject.FindGameObjectWithTag("Spirit") != null&& SelectedSlot != null && SelectedSlot.GetComponent<Spawn_Obj_OnMouseClick>() != null)
        {
            SpiritMagicController spiritmagicController = GameObject.FindGameObjectWithTag("Spirit").GetComponent<SpiritMagicController>();
            if (spiritmagicController.SelectedSlot != null && spiritmagicController.SelectedSlot.GetComponent<Spawn_Obj_OnMouseClick>() != null)
            {
                spiritmagicController.SelectedSlot.GetComponent<Spawn_Obj_OnMouseClick>().Selected = false;
            }
        }
        else
        {
            GameObject.FindGameObjectWithTag("Spirit").GetComponent<ReFormaSpell>().Selected = false;
        }
    }

    private IEnumerator SearchForSpellCoRoutine()
    {
        do
        {
            if (GameObject.FindGameObjectsWithTag("SpiritSpells").Where(g => g.name == spellName).Count() > 0)
                Spell = GameObject.FindGameObjectsWithTag("SpiritSpells").Where(gameObject => gameObject.name == spellName).FirstOrDefault();
            yield return null;
        } while(Spell == null);
        print(Spell.name);
    }

    private IEnumerator SearchForReforma()
    {
        do
        {
            if (GameObject.FindGameObjectsWithTag("Spirit").Length > 0)
                Spell = GameObject.FindGameObjectWithTag("Spirit");
            yield return null;
        } while (Spell == null);
    }
}
