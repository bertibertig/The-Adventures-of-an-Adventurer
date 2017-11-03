using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Obj_Facing_Cursor : MonoBehaviour {

    public float objLifespan = 5.0f;
    public bool shootingIsActive = false;
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
        GameObject projectileClone = Instantiate(projectile, transform.position, Quaternion.FromToRotation(Vector3.down, normalizedDirection));
        projectileClone.SetActive(true);

        if (projectileClone.GetComponent<Move_Indefinitely>() != null)
            projectileClone.GetComponent<Move_Indefinitely>().Move_To_Direction(normalizedDirection);

        Destroy(projectileClone, objLifespan);
    }
}
