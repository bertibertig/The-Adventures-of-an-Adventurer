using UnityEngine;
using System.Collections;
using System.Linq;

public class WitchTreeAcorn : MonoBehaviour {

    private GameObject[] witchTreeLeaveColliders;
    private Damage_On_Collision damageDealer;
    private bool onGround;

	// Use this for initialization
	void Start () {
        damageDealer = gameObject.GetComponentInChildren<Damage_On_Collision>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
            Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        onGround = false;
        //StartCoroutine("Disapear");
	}
	
	// Update is called once per frame
	void Update () {
        if(!onGround)
            transform.Rotate(new Vector3(0, 0, -(this.gameObject.transform.rotation.z + 5)));
        if (onGround || GameObject.FindGameObjectsWithTag("Enemy").Where(g => g.name == "WitchTree").FirstOrDefault() == null)
            Destroy(damageDealer);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground") || col.collider.CompareTag("Projectile"))
            onGround = true;
    }
    /*
    IEnumerator Disapear()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
    */
}