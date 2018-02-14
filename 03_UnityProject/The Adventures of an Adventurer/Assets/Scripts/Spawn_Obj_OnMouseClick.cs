using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Obj_OnMouseClick : MonoBehaviour {

    public GameObject SpawnAtObject;
    public float objLifespan = 5.0f;
    public bool facingCursor = true;
    public GameObject projectile;
    public bool selected = false;

    public bool Selected
    {
        get { return selected; }
        set {
            firstSelection = true;
            selected = value;
        }
    }

    private bool firstSelection = true;
    private Vector3 lastClickCoordinate;
    private Vector3 normalizedDirection;

	// Use this for initialization
	void Start () {
        if (SpawnAtObject == null && GameObject.FindGameObjectWithTag("Spirit"))
            SpawnAtObject = GameObject.FindGameObjectWithTag("Spirit");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0) && selected && !firstSelection)
        {
            lastClickCoordinate = Input.mousePosition;
            normalizedDirection = ((Vector2)Camera.main.ScreenToWorldPoint(lastClickCoordinate) - (Vector2)transform.position).normalized;
            SpawnProjectile();
        }
        else if(Input.GetMouseButtonUp(0) && selected && firstSelection)
        {
            firstSelection = false;
        }
	}

    public void SpawnProjectile()
    {
        GameObject projectileClone;

        if (Input.GetMouseButton(0) && RectTransformUtility.RectangleContainsScreenPoint(this.gameObject.GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
        {
            print("Clicked Outside of Pannel");
        }
        if (facingCursor)
            projectileClone = Instantiate(projectile, SpawnAtObject.transform.position, Quaternion.FromToRotation(Vector3.down, normalizedDirection));
        else
            projectileClone = Instantiate(projectile, SpawnAtObject.transform.position, Quaternion.identity);

        projectileClone.SetActive(true);
        for(int i = 0; i < projectileClone.transform.childCount; i++)
            projectileClone.transform.GetChild(i).gameObject.SetActive(true);

        if (projectileClone.GetComponent<Move_Indefinitely>() != null)
            projectileClone.GetComponent<Move_Indefinitely>().Move_To_Direction(normalizedDirection);

        Destroy(projectileClone, objLifespan);
    }
}
