using UnityEngine;
using System.Collections;

public class WitchTreeSquirel : MonoBehaviour {

    private GameObject acorn;
    private Animator anim;
    private GameObject player;
    private float a;
    private float t;
    private float g;

	// Use this for initialization
	void Start () {
        acorn = Resources.Load("Mobs/Bosses/0_WitchTree/Acorn", typeof(GameObject)) as GameObject;
        anim = this.gameObject.GetComponent<Animator>();
        anim.SetBool("throw", false);
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("ThrowNuts");
	}

    private IEnumerator ThrowNuts()
    {
        while (true)
        {
            anim.SetBool("throw", true);
            yield return new WaitForSeconds(0.3f);

            
            float x = (player.GetComponent<Transform>().transform.position.x);
            float y = (player.GetComponent<Transform>().transform.position.y);
            //float v0 = (Mathf.Pow(x,2)*y) * (1/Mathf.Pow(x,2))*(9.81f/2);

            Vector3 currentPosition = new Vector3(this.gameObject.transform.position.x + 0.1f, this.gameObject.transform.position.y + 0.1f, this.gameObject.transform.position.z);
            GameObject tmpObj = GameObject.Instantiate(acorn, currentPosition, Quaternion.identity) as GameObject;
            tmpObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(2 * x, 2* y));  
            yield return new WaitForSeconds(1);
            anim.SetBool("throw", false);
            yield return new WaitForSeconds(2);
            
        }
    }
}
