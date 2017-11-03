using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_OnContact : MonoBehaviour {

    public GameObject destroyParticles;

    private void OnTriggerEnter2D(Collider2D otherCol)
    {
        if (otherCol.gameObject.layer != LayerMask.NameToLayer("NoObstacle") && !otherCol.isTrigger)
        {
            Instantiate(destroyParticles, transform.position, Quaternion.identity);
            Destroy(transform.parent.gameObject);
        }
    }
}
