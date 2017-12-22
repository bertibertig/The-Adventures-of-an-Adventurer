using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Obj_OnMouseClick : MonoBehaviour {

    public float objLifespan = 5.0f;
    public bool shootingIsActive = false;
    public bool facingCursor = true;
    public GameObject projectile;

    private Vector3 lastClickCoordinate;
    private Vector3 normalizedDirection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0) && shootingIsActive)
        {
            lastClickCoordinate = Input.mousePosition;
            normalizedDirection = ((Vector2)Camera.main.ScreenToWorldPoint(lastClickCoordinate) - (Vector2)transform.position).normalized;
            SpawnProjectile();
        }
	}

    public void SpawnProjectile()
    {
        GameObject projectileClone;

        if (facingCursor)
            projectileClone = Instantiate(projectile, transform.position, Quaternion.FromToRotation(Vector3.down, normalizedDirection));
        else
            projectileClone = Instantiate(projectile, transform.position, Quaternion.identity);

        projectileClone.SetActive(true);
        for(int i = 0; i < projectileClone.transform.childCount; i++)
            projectileClone.transform.GetChild(i).gameObject.SetActive(true);

        if (projectileClone.GetComponent<Move_Indefinitely>() != null)
            projectileClone.GetComponent<Move_Indefinitely>().Move_To_Direction(normalizedDirection);

        Destroy(projectileClone, objLifespan);
    }
}
