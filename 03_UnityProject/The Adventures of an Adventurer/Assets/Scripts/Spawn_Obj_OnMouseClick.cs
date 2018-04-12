using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Obj_OnMouseClick : Photon.MonoBehaviour {

    public GameObject SpawnAtObject;
    public float objLifespan = 5.0f;
    public bool facingCursor = true;
    public GameObject projectile;
    public bool selected = false;
    public string projectilePath = "Spells/Projectiles/Fireball";
    public bool online = true;

    private GameObject projectileClone;

    public bool Selected
    {
        get { return selected; }
        set {
            firstSelection = true;
            selected = value;
        }
    }

    public float DamageMultiplayer { get; set; }

    private bool firstSelection = true;
    private Vector3 lastClickCoordinate;
    private Vector3 normalizedDirection;

	// Use this for initialization
	void Start () {
        if (SpawnAtObject == null && GameObject.FindGameObjectWithTag("Spirit"))
            SpawnAtObject = GameObject.FindGameObjectWithTag("Spirit");
        if (projectile == null)
            projectile = Resources.Load(projectilePath) as GameObject;
        //StartCoroutine("GetPhotonViewCoRoutine");
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0) && selected && !firstSelection)
        {
            lastClickCoordinate = Input.mousePosition;
            normalizedDirection = ((Vector2)Camera.main.ScreenToWorldPoint(lastClickCoordinate) - (Vector2)transform.position).normalized;
            if(!online)
                SpawnProjectile();
            else
                this.photonView.RPC("SpawnProjectileRPC", PhotonTargets.All);
        }
        else if(Input.GetMouseButtonUp(0) && selected && firstSelection)
        {
            firstSelection = false;
        }
	}

    public void SpawnProjectile()
    {
        GameObject projectileClone = new GameObject();

        if (Input.GetMouseButton(0) && RectTransformUtility.RectangleContainsScreenPoint(this.gameObject.GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
        {
            print("Clicked Outside of Pannel");
        }
        if (facingCursor)
            projectileClone = Instantiate(projectile, transform.position, Quaternion.FromToRotation(Vector3.down, normalizedDirection));
        else
            projectileClone = Instantiate(projectile, transform.position, Quaternion.identity);

        projectileClone.SetActive(true);

        if (projectileClone.GetComponent<Move_Indefinitely>() != null)
            projectileClone.GetComponent<Move_Indefinitely>().Move_To_Direction(normalizedDirection);

        Destroy(projectileClone, objLifespan);
    }

    [PunRPC]
    public void SpawnProjectileRPC()
    {
        projectileClone = new GameObject();
        if (Input.GetMouseButton(0) && RectTransformUtility.RectangleContainsScreenPoint(this.gameObject.GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
        {
            print("Clicked Outside of Pannel");
        }
        if (facingCursor)
            projectileClone = PhotonNetwork.Instantiate(projectilePath, transform.position, Quaternion.FromToRotation(Vector3.down, normalizedDirection), 0);
        else
            projectileClone = PhotonNetwork.Instantiate(projectilePath, transform.position, Quaternion.identity, 0);

        projectileClone.SetActive(true);

        if (projectileClone.GetComponent<Move_Indefinitely>() != null)
            projectileClone.GetComponent<Move_Indefinitely>().Move_To_Direction(normalizedDirection);

        Destroy(projectileClone, objLifespan);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        /*if (stream.isWriting)
        {
            if(projectileClone != null)
                stream.SendNext(this.projectileClone.transform.position);
        }
        else if (stream.isReading)
        {
            if (projectileClone != null)
                projectileClone.transform.position = (Vector2)stream.ReceiveNext();
        }*/
    }
}
