using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour {

    public GameObject levelTemp;
    public GameObject level1;

    private int position = 0;

    private List<LevelObject> levels;

    private bool levelSelected;

	// Use this for initialization
	void Start () {
        levelSelected = false;
        levels = new List<LevelObject>();

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
            yield return null;
        } while (!levelSelected);
    }

    private void ChangeSelection(int changer)
    {
        levels[position].LevelFrame.enabled = false;
        levels[position].LevelObjectItem.GetComponent<SpriteRenderer>().enabled = false;
        position += changer;
        print(position);
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
}
