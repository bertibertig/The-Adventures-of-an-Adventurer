using UnityEngine;
using System.Collections;

public class ThrowSimulation : MonoBehaviour
{
    public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public Transform Projectile;
    private Transform myTransform;
    private GameObject acorn;

    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        print(Projectile);
        if(Projectile == null)
        {
            Vector3 currentPosition = new Vector3(this.gameObject.transform.position.x + 0.1f, this.gameObject.transform.position.y + 0.1f, this.gameObject.transform.position.z);
            acorn = Resources.Load("Mobs/Bosses/0_WitchTree/Acorn", typeof(GameObject)) as GameObject;
            GameObject tmpObj = GameObject.Instantiate(acorn, currentPosition, Quaternion.identity) as GameObject;
            Projectile = tmpObj.transform;
        }
        print(Physics2D.gravity);
        StartCoroutine("SimulateProjectile");
    }


    IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(1.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Projectile.position, Target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        print(Target.position);
        print(Projectile.position);
        print(Target.position - Projectile.position);
        Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);
        float elapse_time = 0;

        while (elapse_time < flightDuration + 0.1f)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
            //Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
            elapse_time += Time.deltaTime;
            yield return null;
        }
    }
}