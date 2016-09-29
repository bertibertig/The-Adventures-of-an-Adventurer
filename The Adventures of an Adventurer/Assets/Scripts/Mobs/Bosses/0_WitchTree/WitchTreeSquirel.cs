using UnityEngine;
using System.Collections;

public class WitchTreeSquirel : MonoBehaviour {

    private GameObject acorn;
    private Animator anim;

	// Use this for initialization
	void Start () {
        acorn = Resources.Load("Mobs/Bosses/0_WitchTree/Acorn", typeof(GameObject)) as GameObject;
        anim = this.gameObject.GetComponent<Animator>();
        anim.SetBool("throw", false);
        StartCoroutine("ThrowNuts");
	}

    private IEnumerator ThrowNuts()
    {
        while (true)
        {
            anim.SetBool("throw", true);
            yield return new WaitForSeconds(0.3f);
            Vector3 currentPosition = new Vector3(this.gameObject.transform.position.x + 0.1f, this.gameObject.transform.position.y + 0.1f, this.gameObject.transform.position.z);
            GameObject tmpObj = GameObject.Instantiate(acorn, currentPosition, Quaternion.identity) as GameObject;
            tmpObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-70, 200), Random.Range(0, 200)));  
            yield return new WaitForSeconds(1);
            anim.SetBool("throw", false);
            yield return new WaitForSeconds(2);
            
        }
    }
}
