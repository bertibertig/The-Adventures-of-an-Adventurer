using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_OnContact : MonoBehaviour {

    public GameObject destroyParticles;
    public bool playerIsObstacle = false;
    public bool enemyIsObstacle = true;

    private void OnTriggerEnter2D(Collider2D otherCol)
    {
        if (otherCol.gameObject.layer != LayerMask.NameToLayer("NoObstacle") && !otherCol.isTrigger)
        {
            if (otherCol.gameObject.tag == "Enemy" && enemyIsObstacle)
            {
                Debug.Log(otherCol.gameObject.name);
                Destroy();
            }
            else if (otherCol.gameObject.tag == "Player" && playerIsObstacle)
            {
                Debug.Log(otherCol.gameObject.name);
                Destroy();
            }
            else if(otherCol.gameObject.tag != "Enemy" && otherCol.gameObject.tag != "Player")
            {
                Debug.Log(otherCol.gameObject.name);
                Destroy();
            }
        }
    }

    private void Destroy()
    {
        if (destroyParticles != null)
            Instantiate(destroyParticles, transform.position, Quaternion.identity).SetActive(true);
        Destroy(transform.parent.gameObject);
    }
}
