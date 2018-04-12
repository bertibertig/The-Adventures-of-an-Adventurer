using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Indefinitely : Photon.MonoBehaviour {

    public float speed = 5f;
    public Transform target;

    private Vector3 direction;
    private Rigidbody2D rb2d;
    private Vector2 newPos = new Vector2(0,0);

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        Set_Target_Direction();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += direction * speed * Time.deltaTime;
        /*if (this.photonView != null)
            transform.position = newPos;*/
    }

    public void Move_To_Direction(Vector3 dir)
    {
        direction = dir;
    }

    public void Move_To_Target(GameObject target)
    {
        this.target = target.transform;
        Set_Target_Direction();
    }

    private void Set_Target_Direction()
    {
        if (target != null)
            direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        /*if (stream.isWriting)
        {
            stream.SendNext(transform.position);
        }
        else if (stream.isReading)
        {
            newPos = (Vector2)stream.ReceiveNext();
            transform.position = newPos;
        }*/
    }
}
