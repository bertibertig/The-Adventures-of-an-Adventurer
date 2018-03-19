using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class Spell_Database : MonoBehaviour
{
    private List<Spell> database = new List<Spell>();
    private JsonData spellData;

    void Start()
    {
        spellData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Spells.json"));
        FillSpellDB();

        Debug.Log(GetSpell(2).Desc);
    }

    public Spell GetSpell(int id)
    {
        foreach (Spell sp in database)
        {
            if (sp.Id == id)
            {
                return sp;
            }
        }
        return null;
    }

    void FillSpellDB()
    {
        for (int i = 0; i < spellData.Count; i++)
        {
            database.Add
            (
                new Spell
                (
                    (int)spellData[i]["id"],
                    spellData[i]["name"].ToString(),
                    spellData[i]["desc"].ToString(),
                    (int)spellData[i]["learnLevel"],
                    (int)spellData[i]["stats"]["damage"],
                    (int)spellData[i]["stats"]["healing"],
                    float.Parse(spellData[i]["stats"]["castTime"].ToString()),
                    float.Parse(spellData[i]["stats"]["cooldown"].ToString()),
                    (bool)spellData[i]["effect"],
                    spellData[i]["slug"].ToString()
                )
            );
        }
    }
}

public class Spell
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public int LearnLevel { get; set; }
    public int Damage { get; set; }
    public int Healing { get; set; }
    public float CastTime { get; set; }
    public float Cooldown { get; set; }
    public bool HasEffect { get; set; }
    public string Slug { get; set; }

    public Spell()
    {
        Id = -1;
    }

    public Spell(int id, string name, string desc, int lvl, int dmg, int heal, float castTime, float cooldown, bool hasEffect, string slug)
    {
        Id = id;
        Name = name;
        Desc = desc;
        LearnLevel = lvl;
        Damage = dmg;
        Healing = heal;
        CastTime = castTime;
        Cooldown = cooldown;
        HasEffect = hasEffect;
        Slug = slug;
    }
}