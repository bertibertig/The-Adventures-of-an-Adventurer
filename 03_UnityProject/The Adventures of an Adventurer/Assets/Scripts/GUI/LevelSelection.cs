using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour {

    public GameObject levelTemp;
    public GameObject level1;
    public GameObject player;
    public GameObject ui;

    private int position = 0;

    private List<LevelObject> levels;

    private bool levelSelected;

    public Animator rokingChair;
    public GameObject rockingChairGO;
    public GameObject inventoryUI;

    public bool PlayerSitting { get; set; }
    public bool LevelSelectionDisabled { get; set; }

    // Use this for initialization
    void Start () {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
            player.SetActive(false);

        inventoryUI.GetComponentInChildren<Inventory_Main>().InventoryDisabled = true;
        LevelSelectionDisabled = false;
        InitialiseScript();
    }

    private void Update()
    {
        /*if(Input.GetButtonDown("Cancel") && PlayerSitting && !LevelSelectionDisabled)
        {
            print("stop");
            StopCoroutine("LevelSelectionCoRoutine");
            rokingChair.SetBool("IsUsed",false);
            levels[position].LevelFrame.enabled = false;
            levels[position].LevelObjectItem.GetComponent<SpriteRenderer>().enabled = false;
            position = 0;
            PlayerSitting = false;
            inventoryUI.GetComponentInChildren<Inventory_Main>().InventoryDisabled = false;
            player.transform.position = rockingChairGO.transform.position;
            player.SetActive(true);
            ui.SetActive(true);
        }*/
    }

    private void InitialiseScript()
    {
        levelSelected = false;
        levels = new List<LevelObject>();
        PlayerSitting = true;

        if (level1 != null)
        {
            levels.Add(new LevelObject(levels.Count, "Level 1", "1_Level_Beginning", level1));
        }

        if (levelTemp != null)
        {
            levels.Add(new LevelObject(levels.Count, "Level Temp", "Temp", levelTemp));
        }

        //TODO: Implement further levels.

        StartCoroutine("LevelSelectionCoRoutine");
    }

    IEnumerator LevelSelectionCoRoutine()
    {
        do
        {
            if (!LevelSelectionDisabled)
            {
                if (Input.GetAxis("Horizontal") < 0)
                {
                    if (position - 1 != -1)
                        ChangeSelection(-1);
                    yield return new WaitForSeconds(0.5f);
                }
                if (Input.GetAxis("Horizontal") > 0)
                {
                    if (position + 1 != levels.Count)
                        ChangeSelection(1);
                    yield return new WaitForSeconds(0.5f);
                }
                if (Input.GetButtonDown("Interact"))
                    print("Selected Level: " + levels[position].LevelName);
                //TODO: Implement Level Startvfor Level 1
            }
            yield return null;
        } while (!levelSelected);
    }

    private void ChangeSelection(int changer)
    {
        levels[position].LevelFrame.enabled = false;
        levels[position].LevelObjectItem.GetComponent<SpriteRenderer>().enabled = false;
        position += changer;
        levels[position].LevelFrame.enabled = true;
        StartCoroutine("MakeBrighter");
        //levels[position].LevelItem.GetComponentInChildren<Renderer>().enabled = true;
    }

    IEnumerator MakeBrighter()
    {
        levels[position].LevelObjectItem.color = new Color(1f, 1f, 1f, 0f);
        print(levels[position].LevelObjectItem.color);
        levels[position].LevelObjectItem.enabled = true;
        float i = 0;
        while(i <= 1f)
        {
            levels[position].LevelObjectItem.color = new Color(1f, 1f, 1f, i); //new Color(color.r, color.g, color.b, i);
            print(i);
            i += 0.05f;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    public void RestartScript()
    {
        //TODO: BUG: Check why Adventurer is invisible on first frames of Animation. 
        PlayerSitting = true;
        position = 0;
        rokingChair.SetBool("IsUsed", true);
        levels[position].LevelFrame.enabled = true;
        levels[position].LevelObjectItem.GetComponent<SpriteRenderer>().enabled = true;
        inventoryUI.GetComponentInChildren<Inventory_Main>().InventoryDisabled = true;
        player.SetActive(false);
        ui.SetActive(false);
        StartCoroutine("LevelSelectionCoRoutine");
    }
}
