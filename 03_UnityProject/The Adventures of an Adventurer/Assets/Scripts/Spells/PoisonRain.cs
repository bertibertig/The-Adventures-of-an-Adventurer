using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonRain : MonoBehaviour {

    private GameObject raindrop;
    private Vector3 bottomLeft;
    private Vector3 bottomRight;

    // Use this for initialization
    void Start () {
        raindrop = Resources.Load("Spells/PoisonRain/Raindrop") as GameObject;
        GetCords();
        StartCoroutine("Rain");
	}

    private IEnumerator Rain()
    {
        for(int i = 0; i < 3; i++)
        {
            float x = Random.Range(bottomLeft.x, bottomRight.x);
            float y = Random.Range(bottomLeft.y, bottomRight.y);
            Vector3 dropPos = new Vector3(x,y);
            raindrop.transform.position = dropPos;
            Instantiate(raindrop);
            yield return new WaitForSeconds(1);
        }
    }

    private void GetCords()
    {
        float width = this.GetComponent<SpriteRenderer>().bounds.size.x;
        float height = this.GetComponent<SpriteRenderer>().bounds.size.y;

        bottomRight = this.transform.position;
        bottomLeft = this.transform.position;

        bottomRight.x += width / 2;
        bottomRight.y -= height / 2;

        bottomLeft.x -= width / 2;
        bottomLeft.y -= height / 2;
    }
}
